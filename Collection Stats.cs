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
using JefBar.Billing.Reports;
using JefBar.Core.Business;
using JefBar.Core.Data;
using JefBar.Core.Report;
using JefBar.Core.Utilities;

namespace FastReport
{
  public class ReportScript
  {
    public string companyCode;
    public DateTime startDate = new DateTime();
    public System.Collections.Generic.Dictionary <string,string> runs = new System.Collections.Generic.Dictionary <string,string>(); 
    public double varChargesPreviousPeriod = 0;
    public double varPaymentsPreviousPeriod = 0;
    public double varWriteOffsPreviousPeriod = 0;
    public double varBadDebtPreviousPeriod = 0;
    public double varRefundsPreviousPeriod = 0;
    public double varRevenueAdjustmentsPreviousPeriod = 0;
    public double varChargeAdjustmentsPreviousPeriod = 0;
    public double varTotalPreviousPeriod = 0;
    private void Data1_AfterData(object sender, EventArgs e)
    {
      
      string name = ((String)Report.GetColumnValue("Selections.Property.Name"));
      if (name == "Transaction Date")
      {
        ComparisonOperator op = ((ComparisonOperator)Report.GetColumnValue("Selections.ComparisonOperator"));
        if ( op==ComparisonOperator.GreaterThanOrEqual )
        {
          Report.SetParameterValue("StartDate", new SmartDate(((string)Report.GetColumnValue("Selections.ValueAsString"))).Date);
          startDate = ((DateTime)Report.GetParameterValue("StartDate"));
        }
        if ( op==ComparisonOperator.LessThanOrEqual )
          Report.SetParameterValue("EndDate", new SmartDate(((string)Report.GetColumnValue("Selections.ValueAsString"))).Date);
      }

      if (name == "Company Code")
        companyCode = (string)Report.GetColumnValue("Selections.Value");
      //MessageBox.Show(startDate.ToString() + "  " + companyCode);
      
      if(!System.String.IsNullOrEmpty(companyCode) && (startDate != System.DateTime.MinValue))
      {       
        //MessageBox.Show(startDate.ToString() + "  " + companyCode + " in if statement");
        varChargesPreviousPeriod = getPreviousChargesfromDB(companyCode,startDate);   
        varPaymentsPreviousPeriod = getPreviousPaymentsfromDB(companyCode,startDate);
        varWriteOffsPreviousPeriod = getPreviousWriteOffsfromDB(companyCode,startDate);
        varBadDebtPreviousPeriod = getPreviousBadDebtfromDB(companyCode,startDate);
        varRefundsPreviousPeriod = getPreviousRefundsfromDB(companyCode,startDate);
        varRevenueAdjustmentsPreviousPeriod = getPreviousRevenueAdjustmentsfromDB(companyCode,startDate);
        varChargeAdjustmentsPreviousPeriod = getPreviousChargeAdjustmentsfromDB(companyCode,startDate);
        varTotalPreviousPeriod = getPreviousTotalfromDB(companyCode,startDate);
      }
    }
    
    private void Data4_AfterData(object sender, EventArgs e)
    {                                                    
      string runNumber = (string)Report.GetColumnValue("ARTransactions.RunNumber");             
      SmartDate tranDate = (SmartDate)Report.GetColumnValue("ARTransactions.TransactionDate");
      string tranMonthYear = tranDate.ToString("yyyyMM");
      //MessageBox.Show(tranMonthYear + "  " + runNumber);
      
      if(!runs.ContainsKey(runNumber))
        runs.Add(runNumber,tranMonthYear);    
    }
    
    public double getPreviousChargesfromDB(string company, DateTime previousDate)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where CompanyCode = @CompanyCode"
          + " and TransactionDate < @TransactionDate"
          + " and TransactionTypeGroupCode = 'CH'";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = company;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
        
          try
          {
            return (double)cmd.ExecuteScalar();
          }
          catch (Exception ex)
          {
            MessageBox.Show("Can't get previous charges - " + ex.Message);
          }
        }
      }
      return 0;
    }
    
    public double getPreviousPaymentsfromDB(string company, DateTime previousDate)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where CompanyCode = @CompanyCode"
          + " and TransactionDate < @TransactionDate"
          + " and TransactionTypeGroupCode = 'PAY'";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = company;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
        
          try
          {
            return (double)cmd.ExecuteScalar();
          }
          catch (Exception ex)
          {
            MessageBox.Show("Can't get previous payments - " + ex.Message);
          }
        }
      }
      return 0;
    }
    
    public double getPreviousWriteOffsfromDB(string company, DateTime previousDate)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where CompanyCode = @CompanyCode"
          + " and TransactionDate < @TransactionDate"
          + " and TransactionTypeGroupCode = 'WD'";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = company;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
        
          try
          {
            return (double)cmd.ExecuteScalar();
          }
          catch (Exception ex)
          {
            MessageBox.Show("Can't get previous write offs - " + ex.Message);
          }
        }
      }
      return 0;
    }
    
    public double getPreviousBadDebtfromDB(string company, DateTime previousDate)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where CompanyCode = @CompanyCode"
          + " and TransactionDate < @TransactionDate"
          + " and TransactionTypeGroupCode = 'CDISC'";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = company;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
        
          try
          {
            return (double)cmd.ExecuteScalar();
          }
          catch (Exception ex)
          {
            MessageBox.Show("Can't get previous bad debt - " + ex.Message);
          }
        }
      }
      return 0;
    }
    
    public double getPreviousRefundsfromDB(string company, DateTime previousDate)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where CompanyCode = @CompanyCode"
          + " and TransactionDate < @TransactionDate"
          + " and TransactionTypeGroupCode = 'RERPT'";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = company;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
        
          try
          {
            return (double)cmd.ExecuteScalar();
          }
          catch (Exception ex)
          {
            MessageBox.Show("Can't get previous refunds - " + ex.Message);
          }
        }
      }
      return 0;
    }
    
    public double getPreviousRevenueAdjustmentsfromDB(string company, DateTime previousDate)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where CompanyCode = @CompanyCode"
          + " and TransactionDate < @TransactionDate"
          + " and TransactionTypeGroupCode = 'RVADJ'";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = company;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
        
          try
          {
            return (double)cmd.ExecuteScalar();
          }
          catch (Exception ex)
          {
            MessageBox.Show("Can't get previous revenue adjustments - " + ex.Message);
          }
        }
      }
      return 0;
    }
    
    public double getPreviousChargeAdjustmentsfromDB(string company, DateTime previousDate)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where CompanyCode = @CompanyCode"
          + " and TransactionDate < @TransactionDate"
          + " and TransactionTypeGroupCode = 'CHADJ'";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = company;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
        
          try
          {
            return (double)cmd.ExecuteScalar();
          }
          catch (Exception ex)
          {
            MessageBox.Show("Can't get previous charge adjustments - " + ex.Message);
          }
        }
      }
      return 0;
    }
    
    public double getPreviousTotalfromDB(string company, DateTime previousDate)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where CompanyCode = @CompanyCode"
          + " and TransactionDate < @TransactionDate";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = company;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
        //3863927.78
          try
          {
            return (double)cmd.ExecuteScalar();
          }
          catch (Exception ex)
          {
            MessageBox.Show("Can't get previous total - " + ex.Message);
          }
        }
      }
      return 0;
    }   

    public int RunTotalForMonth(string reportTranMonthYear)
    {
      int runTotal = 0;                         
      
      foreach(KeyValuePair<string, string> entry in runs)
      {                                               
        if(entry.Value==reportTranMonthYear)
          runTotal++;
      }
      
      return runTotal;
    }
    
    public int RunTotalForCompany()
    {
      int compTotal = 0;
      
      foreach(KeyValuePair<string, string> entry in runs)
        compTotal++;
      
      return compTotal;
    }   
  }
}
