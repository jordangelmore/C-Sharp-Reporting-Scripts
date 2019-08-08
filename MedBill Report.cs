using System.Data.SqlClient;
using Csla;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using FastReport;
using FastReport.Data;
using FastReport.Dialog;
using FastReport.Barcode;
using FastReport.Table;
using FastReport.Utils;
using JefBar.Billing.Business;
using JefBar.Core.Report;


namespace FastReport
{
  public class ReportScript
  {
    int collectionType = int.MinValue;
    string collectionDescription;
    
    const int UserSelection_Collections = 1;
    const int UserSelection_NonCollection = 2;        
    const string CollectionsPayerGroup = "MB";
   
    private void SetReportVariables()
    {
      if(collectionType != int.MinValue)
        return;
      
      string userSelection = ((String)Report.GetParameterValue("COLL"));
      collectionType = int.Parse(userSelection.Split(':')[0].Trim());
      collectionDescription = userSelection.Split(':')[1].Trim();
    }
    
    private bool CanShow()
    {
      SetReportVariables();
      if(collectionType != UserSelection_Collections && collectionType != UserSelection_NonCollection)
        return true;
      
      string payerGroupCode = ((String)Report.GetColumnValue("ARTransactions.CurrentPayerGroupCodeWhenEntered"));
      
      //User Chose Collections - Only Show if current payer is Collections
      if(payerGroupCode == CollectionsPayerGroup && collectionType == UserSelection_Collections)
        return true;
        //User Chose Non-Collections - Only show if the current payer is Non-Collections
      else if(payerGroupCode != CollectionsPayerGroup && collectionType == UserSelection_NonCollection)
        return true;
      
      return false;
    }
    
    private string UserSelectionDescription()
    {
      SetReportVariables();
      return collectionDescription;
    }
    
    
    private void Data5_AfterData(object sender, EventArgs e)
    {
      string name = ((String)Report.GetColumnValue("Selections.Property.Name"));
      if (name == "Transaction Date")
      {
        ComparisonOperator op = ((ComparisonOperator)Report.GetColumnValue("Selections.ComparisonOperator"));
        if ( op==ComparisonOperator.GreaterThanOrEqual || op==ComparisonOperator.GreaterThan )
          Report.SetParameterValue("StartDate", new SmartDate(((string)Report.GetColumnValue("Selections.ValueAsString"))).Date);
        if ( op==ComparisonOperator.LessThanOrEqual || op==ComparisonOperator.LessThan )
          Report.SetParameterValue("EndDate", new SmartDate(((string)Report.GetColumnValue("Selections.ValueAsString"))).Date);
      }
      
      if (name == "Company Code")
        Report.SetParameterValue("CompanyCode", ((Object)Report.GetColumnValue("Selections.Value")));
    }

    private void DataFooter1_BeforeLayout(object sender, EventArgs e)
    {
      SetReportVariables();
      string company = ((String)Report.GetParameterValue("CompanyCode"));
      using (SqlConnection conn = 
          new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        string sql = 
          @"SELECT ISNULL(SUM(Amount),0) FROM bill_t_ARTransaction a
          JOIN bill_t_TripTicket t ON a.RunNumber=t.RunNumber
          WHERE TransactionDate<@StartDate";    
        string sql2 = 
          @"SELECT ISNULL(SUM(Amount),0) FROM bill_t_ARTransaction a
          JOIN bill_t_TripTicket t ON a.RunNumber=t.RunNumber
          WHERE TransactionDate<=@StartDate";
        
        if (!String.IsNullOrWhiteSpace(company))
          sql += " AND CompanyCode=@Comp";
          sql2 += " AND CompanyCode=@Comp";
        

        string balFwdSql = sql;
        if (collectionType==UserSelection_Collections || collectionType==UserSelection_NonCollection)
        {
          balFwdSql += String.Format(
            @" AND a.RunNumber {0} IN (SELECT h.RunNumber FROM bill_t_TripTicketCurrentPayerHistory h
            JOIN bill_t_TripTicketPayer p ON h.TripTicketPayerId=p.TripTicketPayerId
            JOIN bill_c_Payer cp ON p.PayerCode=cp.PayerCode 
            WHERE FromDate<@StartDate AND (ToDate IS NULL OR ToDate>=@StartDate) AND PayerGroupCode='{1}')"
            ,collectionType==UserSelection_Collections ? "" : "NOT",CollectionsPayerGroup);
        }      
        
        string collSql = sql2;
        string collOutSql = sql;
        if (collectionType==UserSelection_Collections || collectionType==UserSelection_NonCollection)
        {
          collSql += String.Format(
            @" AND a.RunNumber IN (SELECT h.RunNumber FROM bill_t_TripTicketCurrentPayerHistory h
            JOIN bill_t_TripTicketPayer p ON h.TripTicketPayerId=p.TripTicketPayerId
            JOIN bill_c_Payer cp ON p.PayerCode=cp.PayerCode 
            WHERE FromDate>=@StartDate AND (ToDate IS NULL OR CONVERT(date,ToDate)>=@EndDate) AND PayerGroupCode='{0}')"
            ,CollectionsPayerGroup);
          
          collOutSql += String.Format(
            @" AND a.RunNumber IN (SELECT h.RunNumber FROM bill_t_TripTicketCurrentPayerHistory h
            JOIN bill_t_TripTicketPayer p ON h.TripTicketPayerId=p.TripTicketPayerId
            JOIN bill_c_Payer cp ON p.PayerCode=cp.PayerCode 
            WHERE FromDate<@StartDate AND (ToDate>=@StartDate AND CONVERT(date,ToDate)<=@EndDate) AND PayerGroupCode='{0}')"
            ,CollectionsPayerGroup);
        }
        
        SqlCommand cmd = new SqlCommand(balFwdSql, conn);
        SqlCommand collCmd = new SqlCommand(collSql, conn);
        SqlCommand collOutCmd = new SqlCommand(collOutSql, conn);
        
        if (!String.IsNullOrWhiteSpace(company))
        {
          cmd.Parameters.Add("@Comp", SqlDbType.NVarChar);
          cmd.Parameters["@Comp"].Value = company;
          collCmd.Parameters.Add("@Comp", SqlDbType.NVarChar);
          collCmd.Parameters["@Comp"].Value = company;
          collOutCmd.Parameters.Add("@Comp", SqlDbType.NVarChar);
          collOutCmd.Parameters["@Comp"].Value = company;
        }
        cmd.Parameters.Add("@StartDate", SqlDbType.DateTime2);
        cmd.Parameters["@StartDate"].Value = ((DateTime)Report.GetParameterValue("StartDate"));
        collCmd.Parameters.Add("@StartDate", SqlDbType.DateTime2);
        collCmd.Parameters["@StartDate"].Value = ((DateTime)Report.GetParameterValue("StartDate"));
        collCmd.Parameters.Add("@EndDate", SqlDbType.DateTime2);
        collCmd.Parameters["@EndDate"].Value = ((DateTime)Report.GetParameterValue("EndDate"));
        collOutCmd.Parameters.Add("@StartDate", SqlDbType.DateTime2);
        collOutCmd.Parameters["@StartDate"].Value = ((DateTime)Report.GetParameterValue("StartDate"));
        collOutCmd.Parameters.Add("@EndDate", SqlDbType.DateTime2);
        collOutCmd.Parameters["@EndDate"].Value = ((DateTime)Report.GetParameterValue("EndDate"));
        try
        {
          conn.Open();
          Report.SetParameterValue("BalanceForward",(Decimal)cmd.ExecuteScalar());
          if (collectionType==UserSelection_Collections || collectionType==UserSelection_NonCollection)
          {
            decimal collections = (Decimal)collCmd.ExecuteScalar();
            if (collectionType==UserSelection_NonCollection)
              collections = -collections;
            decimal outOfCollections = (Decimal)collOutCmd.ExecuteScalar();
            if (collectionType==UserSelection_Collections)
              outOfCollections = -outOfCollections;
            Report.SetParameterValue("Collections",collections+outOfCollections);
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
        
        
      }
    }

    private void _StartReport(object sender, EventArgs e)
    {
      GlobalFilters.CompanyFilter = String.Empty;
    } 

    public System.Collections.Generic.List<int> arDict = new System.Collections.Generic.List<int>();
    public System.Collections.Generic.List<int> arDictSecondary = new System.Collections.Generic.List<int>();
    public System.Collections.Generic.List<int> arDictDup = new System.Collections.Generic.List<int>();
    public System.Collections.Generic.List<int> arDictDupSecondary = new System.Collections.Generic.List<int>(); // only needed for 2 AR bands
    private void Data3_AfterData(object sender, EventArgs e)
    {
      int currentARTransactionId = (int)Report.GetColumnValue("ARTransactions.ARTransactionId");
       
      if(arDict.Contains(currentARTransactionId))
      {
        arDictDup.Add(currentARTransactionId);
        arDictDupSecondary.Add(currentARTransactionId);      
      }
      else
      {
        arDict.Add(currentARTransactionId);
        arDictSecondary.Add(currentARTransactionId);
      } 
    }

    
    
    public bool CanShowPrimary(int arTransactionId) // each band needs a different function that calls a different Dup
    {
      bool show = false;
      
      if(arDict.Contains(arTransactionId) && arDictDup.Contains(arTransactionId))
      {
        show = true;
        arDict.Remove(arTransactionId);
        arDictDup.Remove(arTransactionId);
      }    
      else if(arDict.Contains(arTransactionId))
      {
        show = true;
        arDict.Remove(arTransactionId);
      }  
        
      //MessageBox.Show(show.ToString() + " Primary ");
      return show;
    }
    
    public bool CanShowSecondary(int arTransactionId) // each band needs a different function that calls a different Dup
    {
      bool show = false;
      if(arDictSecondary.Contains(arTransactionId) && arDictDupSecondary.Contains(arTransactionId))
      {
        show = true;
        arDictSecondary.Remove(arTransactionId);
        arDictDupSecondary.Remove(arTransactionId);
      }    
      else if(arDictSecondary.Contains(arTransactionId))
      {
        show = true;
        arDictSecondary.Remove(arTransactionId);
      } 
      //MessageBox.Show(show.ToString() + " Secondary " + arDictDupGroupSecondary.Contains(arTransactionId).ToString());
      return show;
    }        

    private void PageFooter1_AfterData(object sender, EventArgs e)
    {
      arDict.Clear();
      //arDictSecondary.Clear();
      arDictDup.Clear();
      //arDictDupSecondary.Clear();
    }
  } 
}
/*
AR Previous Balance: $32,839,771.77
 */
