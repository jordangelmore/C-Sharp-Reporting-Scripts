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
    public string czCode;
    public string companyCode;
    public DateTime startDate;
    public System.Collections.Generic.List<string> distinctRuns = new System.Collections.Generic.List<string>();
    public System.Collections.Generic.Dictionary<string,System.Collections.Generic.Dictionary<string,System.Collections.Generic.Dictionary<int,string>>> tranDateCounts = new System.Collections.Generic.Dictionary<string,System.Collections.Generic.Dictionary<string,System.Collections.Generic.Dictionary<int,string>>>();
    public System.Collections.Generic.Dictionary<string,int> czCounts = new System.Collections.Generic.Dictionary<string,int>();
    public System.Collections.Generic.Dictionary<string,System.Collections.Generic.Dictionary<string,int>> cbCounts = new System.Collections.Generic.Dictionary<string,System.Collections.Generic.Dictionary<string,int>>();
    public System.Collections.Generic.Dictionary<string,int> companyCounts = new System.Collections.Generic.Dictionary<string,int>();
    public System.Collections.Generic.Dictionary<string,double> chargesPreviousPeriod = new System.Collections.Generic.Dictionary<string,double>();
    public System.Collections.Generic.Dictionary<string,double> paymentsPreviousPeriod = new System.Collections.Generic.Dictionary<string,double>();
    public System.Collections.Generic.Dictionary<string,double> writeOffsPreviousPeriod = new System.Collections.Generic.Dictionary<string,double>();
    public System.Collections.Generic.Dictionary<string,double> badDebtPreviousPeriod = new System.Collections.Generic.Dictionary<string,double>();
    public System.Collections.Generic.Dictionary<string,double> refundsPreviousPeriod = new System.Collections.Generic.Dictionary<string,double>();
    public System.Collections.Generic.Dictionary<string,double> revenueAdjustmentsPreviousPeriod = new System.Collections.Generic.Dictionary<string,double>();
    public System.Collections.Generic.Dictionary<string,double> chargeAdjustmentsPreviousPeriod = new System.Collections.Generic.Dictionary<string,double>();
    public System.Collections.Generic.Dictionary<string,double> czTotalPreviousPeriod = new System.Collections.Generic.Dictionary<string,double>();
    public System.Collections.Generic.Dictionary<string,double> monthTotalPreviousPeriod = new System.Collections.Generic.Dictionary<string,double>();
    
    private void Data1_AfterData(object sender, EventArgs e)
    {
      string name = ((String)Report.GetColumnValue("Selections.Property.Name"));
      if (name == "Transaction Date")
      {
        ComparisonOperator op = ((ComparisonOperator)Report.GetColumnValue("Selections.ComparisonOperator"));
        if ( op==ComparisonOperator.GreaterThanOrEqual )
          Report.SetParameterValue("StartDate", new SmartDate(((string)Report.GetColumnValue("Selections.ValueAsString"))).Date);
        if ( op==ComparisonOperator.LessThanOrEqual )
          Report.SetParameterValue("EndDate", new SmartDate(((string)Report.GetColumnValue("Selections.ValueAsString"))).Date);
        startDate = ((DateTime)Report.GetParameterValue("StartDate"));
      }

      if (name == "Company Code")
        companyCode = (string)Report.GetColumnValue("Selections.Value");
      //MessageBox.Show(startDate.ToString() + "  " + companyCode);
    }
    
    private void Data3_AfterData(object sender, EventArgs e)
    {
      string run = (string)Report.GetColumnValue("ARTransactions.RunNumber");
      int arID = (int)Report.GetColumnValue("ARTransactions.ARTransactionId");
      czCode = (string)Report.GetColumnValue("ARTransactions.ChargeZoneCode");
      string cbCode = (string)Report.GetColumnValue("ARTransactions.CompanyBranchCode");     
      SmartDate smartTranDate = (SmartDate)Report.GetColumnValue("ARTransactions.TransactionDate");
      string tranDate = smartTranDate.ToString("yyyyMM");
      //tranDateCounts
      if(!tranDateCounts.ContainsKey(czCode))
      {        
        tranDateCounts.Add(czCode,new System.Collections.Generic.Dictionary<string,System.Collections.Generic.Dictionary<int,string>>());
        tranDateCounts[czCode].Add(cbCode,new System.Collections.Generic.Dictionary<int,string>());
        tranDateCounts[czCode][cbCode].Add(arID,tranDate);
      }
      else
      {
        if(!tranDateCounts[czCode].ContainsKey(cbCode)/* && !distinctRuns.Contains(run)*/)
        {
          tranDateCounts[czCode].Add(cbCode,new System.Collections.Generic.Dictionary<int,string>());
          tranDateCounts[czCode][cbCode].Add(arID,tranDate);
        }
        else
        {
          if(!tranDateCounts[czCode][cbCode].ContainsKey(arID) && !distinctRuns.Contains(run))
          {
            tranDateCounts[czCode][cbCode].Add(arID,tranDate);
          }  
        }
      }
      //czCounts  
      if(!czCounts.ContainsKey(czCode))
      {                                
        czCounts.Add(czCode,1);
      }
      else
      { 
        if(!distinctRuns.Contains(run))
          czCounts[czCode]++;
      }
      //cbCounts
      if(!cbCounts.ContainsKey(czCode))
      {        
        cbCounts.Add(czCode,new System.Collections.Generic.Dictionary<string,int>());
        cbCounts[czCode].Add(cbCode,1);          
      }
      else
      {
        if(!cbCounts[czCode].ContainsKey(cbCode) && !distinctRuns.Contains(run))
        {
          cbCounts[czCode].Add(cbCode,1);
        }
        else
        {
          if(!distinctRuns.Contains(run))
            cbCounts[czCode][cbCode]++; 
        }
      }
      //cpCounts
      if(!companyCounts.ContainsKey(companyCode))
        companyCounts.Add(companyCode,1);
      else 
      {
        if(!distinctRuns.Contains(run))
          companyCounts[companyCode]++;
      }
      //update distinctRuns
      if(!distinctRuns.Contains(run))
      {
        distinctRuns.Add(run);
      }
      //Money
      if(!chargesPreviousPeriod.ContainsKey(czCode))
        chargesPreviousPeriod.Add(czCode,getPreviousChargesfromDB(czCode,startDate,companyCode));
      if(!paymentsPreviousPeriod.ContainsKey(czCode))	
        paymentsPreviousPeriod.Add(czCode,getPreviousPaymentsfromDB(czCode,startDate,companyCode));
      if(!writeOffsPreviousPeriod.ContainsKey(czCode))	
        writeOffsPreviousPeriod.Add(czCode,getPreviousWriteOffsfromDB(czCode,startDate,companyCode));
      if(!badDebtPreviousPeriod.ContainsKey(czCode))	
        badDebtPreviousPeriod.Add(czCode,getPreviousBadDebtfromDB(czCode,startDate,companyCode));
      if(!refundsPreviousPeriod.ContainsKey(czCode))	
        refundsPreviousPeriod.Add(czCode,getPreviousRefundsfromDB(czCode,startDate,companyCode));
      if(!revenueAdjustmentsPreviousPeriod.ContainsKey(czCode))	
        revenueAdjustmentsPreviousPeriod.Add(czCode,getPreviousRevenueAdjustmentsfromDB(czCode,startDate,companyCode));
      if(!chargeAdjustmentsPreviousPeriod.ContainsKey(czCode))	
        chargeAdjustmentsPreviousPeriod.Add(czCode,getPreviousChargeAdjustmentsfromDB(czCode,startDate,companyCode));
      if(!czTotalPreviousPeriod.ContainsKey(czCode))	
        czTotalPreviousPeriod.Add(czCode,getPreviousCZTotalfromDB(czCode,startDate,companyCode));
      if(!monthTotalPreviousPeriod.ContainsKey(czCode+"-"+tranDate))	
        monthTotalPreviousPeriod.Add(czCode+"-"+tranDate,getPreviousMonthTotalfromDB(czCode,DateTime.ParseExact(tranDate+"01", "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),companyCode));
    }
    
    public double getPreviousChargesfromDB(string cz, DateTime previousDate, string cp)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where ChargeZoneCode = @ChargeZoneCode"
          + " and TransactionDate < @TransactionDate"
          + " and CompanyCode = @CompanyCode"
          + " and TransactionTypeGroupCode = 'CH'";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@ChargeZoneCode", SqlDbType.NVarChar);
          cmd.Parameters["@ChargeZoneCode"].Value = cz;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = cp;
        
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
    
    public double getPreviousPaymentsfromDB(string cz, DateTime previousDate, string cp)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where ChargeZoneCode = @ChargeZoneCode"
          + " and TransactionDate < @TransactionDate"
          + " and CompanyCode = @CompanyCode"
          + " and TransactionTypeGroupCode = 'PAY'";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@ChargeZoneCode", SqlDbType.NVarChar);
          cmd.Parameters["@ChargeZoneCode"].Value = cz;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = cp;
        
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
    
    public double getPreviousWriteOffsfromDB(string cz, DateTime previousDate, string cp)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where ChargeZoneCode = @ChargeZoneCode"
          + " and TransactionDate < @TransactionDate"
          + " and CompanyCode = @CompanyCode"
          + " and TransactionTypeGroupCode = 'WD'";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@ChargeZoneCode", SqlDbType.NVarChar);
          cmd.Parameters["@ChargeZoneCode"].Value = cz;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = cp;
        
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
    
    public double getPreviousBadDebtfromDB(string cz, DateTime previousDate, string cp)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where ChargeZoneCode = @ChargeZoneCode"
          + " and TransactionDate < @TransactionDate"
          + " and CompanyCode = @CompanyCode"
          + " and TransactionTypeGroupCode = 'CDISC'";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@ChargeZoneCode", SqlDbType.NVarChar);
          cmd.Parameters["@ChargeZoneCode"].Value = cz;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = cp;
        
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
    
    public double getPreviousRefundsfromDB(string cz, DateTime previousDate, string cp)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where ChargeZoneCode = @ChargeZoneCode"
          + " and TransactionDate < @TransactionDate"
          + " and CompanyCode = @CompanyCode"
          + " and TransactionTypeGroupCode = 'RERPT'";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@ChargeZoneCode", SqlDbType.NVarChar);
          cmd.Parameters["@ChargeZoneCode"].Value = cz;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = cp;
        
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
    
    public double getPreviousRevenueAdjustmentsfromDB(string cz, DateTime previousDate, string cp)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where ChargeZoneCode = @ChargeZoneCode"
          + " and TransactionDate < @TransactionDate"
          + " and CompanyCode = @CompanyCode"
          + " and TransactionTypeGroupCode = 'RVADJ'";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@ChargeZoneCode", SqlDbType.NVarChar);
          cmd.Parameters["@ChargeZoneCode"].Value = cz;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = cp;
        
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
    
    public double getPreviousChargeAdjustmentsfromDB(string cz, DateTime previousDate, string cp)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where ChargeZoneCode = @ChargeZoneCode"
          + " and TransactionDate < @TransactionDate"
          + " and CompanyCode = @CompanyCode"
          + " and TransactionTypeGroupCode = 'CHADJ'";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@ChargeZoneCode", SqlDbType.NVarChar);
          cmd.Parameters["@ChargeZoneCode"].Value = cz;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = cp;
        
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
    
    public double getPreviousCZTotalfromDB(string cz, DateTime previousDate, string cp)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where ChargeZoneCode = @ChargeZoneCode"
          + " and TransactionDate < @TransactionDate"
          + " and CompanyCode = @CompanyCode";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@ChargeZoneCode", SqlDbType.NVarChar);
          cmd.Parameters["@ChargeZoneCode"].Value = cz;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = cp;
        
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
    
    public double getPreviousMonthTotalfromDB(string cz, DateTime previousDate, string cp)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where ChargeZoneCode = @ChargeZoneCode"
          + " and TransactionDate < @TransactionDate"
          + " and CompanyCode = @CompanyCode";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@ChargeZoneCode", SqlDbType.NVarChar);
          cmd.Parameters["@ChargeZoneCode"].Value = cz;
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = cp;
        
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
    
    public double getPreviousGrandTotalfromDB(DateTime previousDate, string cp)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select cast(isnull(sum(Amount),0) as float) from bill_t_ARTransaction ar"
          + " join bill_t_TripTicket tt on ar.RunNumber=tt.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where TransactionDate < @TransactionDate"
          + " and CompanyCode = @CompanyCode";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {                                               
          cmd.Parameters.Add("@TransactionDate", SqlDbType.Date);
          cmd.Parameters["@TransactionDate"].Value = previousDate;
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = cp;
        
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
    
    public int MonthlyRunCount(string reportCZCode, string reportCBCode, string reportTranDate)
    {
      int count = 0;
      if(tranDateCounts.ContainsKey(reportCZCode))
      {
        if(tranDateCounts[reportCZCode].ContainsKey(reportCBCode))
        {
          foreach(int entry in tranDateCounts[reportCZCode][reportCBCode].Keys)
          {  
            if(tranDateCounts[reportCZCode][reportCBCode].ContainsKey(entry))
            {  
              if(tranDateCounts[reportCZCode][reportCBCode][entry]==reportTranDate)
                count++;
            }         
          } 
        }
      }
      return count;
    }

    public int ChargeZoneRunCount(string reportCZCode)
    {
      if(czCounts.ContainsKey(reportCZCode))
        return czCounts[reportCZCode];
      return 0;
    }

    public int CompanyBranchRunCount(string reportCZCode, string reportCBCode)
    {
      if(cbCounts.ContainsKey(reportCZCode))
      {
        if(cbCounts[reportCZCode].ContainsKey(reportCBCode))
          return cbCounts[reportCZCode][reportCBCode];
      }
      return 0;
    }
    
    public int CompanyRunCount(string reportCompanyCode)
    {
      if(companyCounts.ContainsKey(reportCompanyCode))
        return companyCounts[reportCompanyCode];
      return 0;
    }
    
    public double ChargesPreviousPeriod(string reportCZCode)
    {
      if(chargesPreviousPeriod.ContainsKey(reportCZCode))
        return chargesPreviousPeriod[reportCZCode];
      return 0;
    }
    
    public double PaymentsPreviousPeriod(string reportCZCode)
    {
      if(paymentsPreviousPeriod.ContainsKey(reportCZCode))
        return paymentsPreviousPeriod[reportCZCode];
      return 0;
    }
    
    public double WriteOffsPreviousPeriod(string reportCZCode)
    {
      if(writeOffsPreviousPeriod.ContainsKey(reportCZCode))
        return writeOffsPreviousPeriod[reportCZCode];
      return 0;
    }
    
    public double BadDebtPreviousPeriod(string reportCZCode)
    {
      if(badDebtPreviousPeriod.ContainsKey(reportCZCode))
        return badDebtPreviousPeriod[reportCZCode];
      return 0;
    }
    
    public double RefundsPreviousPeriod(string reportCZCode)
    {
      if(refundsPreviousPeriod.ContainsKey(reportCZCode))
        return refundsPreviousPeriod[reportCZCode];
      return 0;
    }
    
    public double RevenueAdjustmentsPreviousPeriod(string reportCZCode)
    {
      if(revenueAdjustmentsPreviousPeriod.ContainsKey(reportCZCode))
        return revenueAdjustmentsPreviousPeriod[reportCZCode];
      return 0;
    }
    
    public double ChargeAdjustmentsPreviousPeriod(string reportCZCode)
    {
      if(chargeAdjustmentsPreviousPeriod.ContainsKey(reportCZCode))
        return chargeAdjustmentsPreviousPeriod[reportCZCode];
      return 0;
    }
    
    public double ChargeZoneTotalPreviousPeriod(string reportCZCode)
    {
      if(czTotalPreviousPeriod.ContainsKey(reportCZCode))
        return czTotalPreviousPeriod[reportCZCode];
      return 0;
    } 
    
    public double TranMonthTotalPreviousPeriod(string reportCZCode, string reportTranDate)
    {
      if(monthTotalPreviousPeriod.ContainsKey(reportCZCode+"-"+reportTranDate))
        return monthTotalPreviousPeriod[reportCZCode+"-"+reportTranDate];
      return 0;
    }
    
    public double ChargesGrandTotal()
    {
      double retVal = 0;
      foreach(KeyValuePair<string, double> entry in chargesPreviousPeriod)
      {
        retVal += entry.Value;
      }                                            
      return retVal;
    }

    public double PaymentsGrandTotal()
    {
      double retVal = 0;
      foreach(KeyValuePair<string,double> entry in paymentsPreviousPeriod)
        retVal += entry.Value;
      return retVal;
    }

    public double WriteOffsGrandTotal()
    {
      double retVal = 0;
      foreach(KeyValuePair<string,double> entry in writeOffsPreviousPeriod)
        retVal += entry.Value;
      return retVal;
    }

    public double BadDebtGrandTotal()
    {
      double retVal = 0;
      foreach(KeyValuePair<string,double> entry in badDebtPreviousPeriod)
        retVal += entry.Value;
      return retVal;
    }

    public double RefundsGrandTotal()
    {
      double retVal = 0;
      foreach(KeyValuePair<string,double> entry in refundsPreviousPeriod)
        retVal += entry.Value;
      return retVal;
    }

    public double RevenueAdjustmentsGrandTotal()
    {
      double retVal = 0;
      foreach(KeyValuePair<string,double> entry in revenueAdjustmentsPreviousPeriod)
        retVal += entry.Value;
      return retVal;
    }

    public double ChargeAdjustmentsGrandTotal()
    {
      double retVal = 0;
      foreach(KeyValuePair<string,double> entry in chargeAdjustmentsPreviousPeriod)
        retVal += entry.Value;
      return retVal;
    }

    public double GrandTotal()
    {
      double retVal = 0;
      foreach(KeyValuePair<string,double> entry in czTotalPreviousPeriod)
        retVal += entry.Value;
      return retVal;
    }

    private void GroupFooter3_AfterPrint(object sender, EventArgs e)
    {
      Report.Dictionary.Totals.FindByName("Ending AR").Clear();
    }
  }
}
