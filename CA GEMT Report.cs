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
    public int Q1FFSCount = 0; 
    public int A0427Q1FFSCount = 0; 
    public int A0429Q1FFSCount = 0;
    public int A0433Q1FFSCount = 0;
    public int Q1MCCount = 0;
    public int A0427Q1MCCount = 0; 
    public int A0429Q1MCCount = 0;
    public int A0433Q1MCCount = 0;
    public int Q1MedCount;
    public int A0427Q1MedCount = 0; 
    public int A0429Q1MedCount = 0;
    public int A0433Q1MedCount = 0;
    public int Q1SecondaryRunCount = 0;
    public int A0427Q1SecondaryRunCount = 0;
    public int A0429Q1SecondaryRunCount = 0;
    public int A0433Q1SecondaryRunCount = 0;
    public int Q1OtherRunCount = 0;
    public int A0427Q1OtherRunCount = 0; 
    public int A0429Q1OtherRunCount = 0;
    public int A0433Q1OtherRunCount = 0;
    public int Q1TotalRunCount = 0;
    public int A0427Q1TotalRunCount = 0; 
    public int A0429Q1TotalRunCount = 0;
    public int A0433Q1TotalRunCount = 0;
    public int Q2FFSCount = 0;
    public int A0427Q2FFSCount = 0; 
    public int A0429Q2FFSCount = 0;
    public int A0433Q2FFSCount = 0;
    public int Q2MCCount = 0;
    public int A0427Q2MCCount = 0; 
    public int A0429Q2MCCount = 0;
    public int A0433Q2MCCount = 0;
    public int Q2SecondaryRunCount = 0;
    public int A0427Q2SecondaryRunCount = 0;
    public int A0429Q2SecondaryRunCount = 0;
    public int A0433Q2SecondaryRunCount = 0;
    public int Q2MedCount;
    public int A0427Q2MedCount = 0; 
    public int A0429Q2MedCount = 0;
    public int A0433Q2MedCount = 0;
    public int Q2OtherRunCount = 0;
    public int A0427Q2OtherRunCount = 0; 
    public int A0429Q2OtherRunCount = 0;
    public int A0433Q2OtherRunCount = 0;
    public int Q2TotalRunCount = 0;
    public int A0427Q2TotalRunCount = 0; 
    public int A0429Q2TotalRunCount = 0;
    public int A0433Q2TotalRunCount = 0;
    public int Q3FFSCount = 0;
    public int A0427Q3FFSCount = 0; 
    public int A0429Q3FFSCount = 0;
    public int A0433Q3FFSCount = 0;
    public int Q3MCCount = 0;
    public int A0427Q3MCCount = 0; 
    public int A0429Q3MCCount = 0;
    public int A0433Q3MCCount = 0;
    public int Q3SecondaryRunCount = 0;
    public int A0427Q3SecondaryRunCount = 0;
    public int A0429Q3SecondaryRunCount = 0;
    public int A0433Q3SecondaryRunCount = 0;
    public int Q3MedCount;
    public int A0427Q3MedCount = 0; 
    public int A0429Q3MedCount = 0;
    public int A0433Q3MedCount = 0;
    public int Q3OtherRunCount = 0;
    public int A0427Q3OtherRunCount = 0; 
    public int A0429Q3OtherRunCount = 0;
    public int A0433Q3OtherRunCount = 0;
    public int Q3TotalRunCount = 0;
    public int A0427Q3TotalRunCount = 0; 
    public int A0429Q3TotalRunCount = 0;
    public int A0433Q3TotalRunCount = 0;
    public int Q4FFSCount = 0;
    public int A0427Q4FFSCount = 0; 
    public int A0429Q4FFSCount = 0;
    public int A0433Q4FFSCount = 0;
    public int Q4MCCount = 0;
    public int A0427Q4MCCount = 0; 
    public int A0429Q4MCCount = 0;
    public int A0433Q4MCCount = 0;
    public int Q4SecondaryRunCount = 0;
    public int A0427Q4SecondaryRunCount = 0;
    public int A0429Q4SecondaryRunCount = 0;
    public int A0433Q4SecondaryRunCount = 0;
    public int Q4MedCount;
    public int A0427Q4MedCount = 0; 
    public int A0429Q4MedCount = 0;
    public int A0433Q4MedCount = 0;
    public int Q4OtherRunCount = 0;
    public int A0427Q4OtherRunCount = 0; 
    public int A0429Q4OtherRunCount = 0;
    public int A0433Q4OtherRunCount = 0;
    public int Q4TotalRunCount = 0;
    public int A0427Q4TotalRunCount = 0; 
    public int A0429Q4TotalRunCount = 0;
    public int A0433Q4TotalRunCount = 0;
    public int YearFFSCount = 0;
    public int A0427YearFFSCount = 0; 
    public int A0429YearFFSCount = 0;
    public int A0433YearFFSCount = 0;
    public int YearMCCount = 0;
    public int A0427YearMCCount = 0; 
    public int A0429YearMCCount = 0;
    public int A0433YearMCCount = 0;
    public int YearSecondaryRunCount = 0;
    public int A0427YearSecondaryRunCount = 0;
    public int A0429YearSecondaryRunCount = 0;
    public int A0433YearSecondaryRunCount = 0;
    public int YearMedCount;
    public int A0427YearMedCount = 0; 
    public int A0429YearMedCount = 0;
    public int A0433YearMedCount = 0;
    public int YearOtherRunCount = 0;
    public int A0427YearOtherRunCount = 0; 
    public int A0429YearOtherRunCount = 0;
    public int A0433YearOtherRunCount = 0;
    public int YearTotalRunCount = 0;
    public int A0427YearTotalRunCount = 0; 
    public int A0429YearTotalRunCount = 0;
    public int A0433YearTotalRunCount = 0;
    
    public decimal Q1MCRevenue;
    public decimal A0427Q1MCRevenue;
    public decimal A0429Q1MCRevenue;
    public decimal A0433Q1MCRevenue;
    public decimal Q1FFSRevenue;
    public decimal A0427Q1FFSRevenue;
    public decimal A0429Q1FFSRevenue;
    public decimal A0433Q1FFSRevenue;
    public decimal Q1MedRevenue;
    public decimal A0427Q1MedRevenue;
    public decimal A0429Q1MedRevenue;
    public decimal A0433Q1MedRevenue;
    public decimal Q1OtherRevenue;
    public decimal A0427Q1OtherRevenue;
    public decimal A0429Q1OtherRevenue;
    public decimal A0433Q1OtherRevenue;
    public decimal Q1TotalRevenue;
    public decimal A0427Q1TotalRevenue;
    public decimal A0429Q1TotalRevenue;
    public decimal A0433Q1TotalRevenue;
    public decimal Q2MCRevenue;
    public decimal A0427Q2MCRevenue;
    public decimal A0429Q2MCRevenue;
    public decimal A0433Q2MCRevenue;
    public decimal Q2FFSRevenue;
    public decimal A0427Q2FFSRevenue;
    public decimal A0429Q2FFSRevenue;
    public decimal A0433Q2FFSRevenue;
    public decimal Q2MedRevenue;
    public decimal A0427Q2MedRevenue;
    public decimal A0429Q2MedRevenue;
    public decimal A0433Q2MedRevenue;
    public decimal Q2OtherRevenue;
    public decimal A0427Q2OtherRevenue;
    public decimal A0429Q2OtherRevenue;
    public decimal A0433Q2OtherRevenue;
    public decimal Q2TotalRevenue;
    public decimal A0427Q2TotalRevenue;
    public decimal A0429Q2TotalRevenue;
    public decimal A0433Q2TotalRevenue;
    public decimal Q3MCRevenue;
    public decimal A0427Q3MCRevenue;
    public decimal A0429Q3MCRevenue;
    public decimal A0433Q3MCRevenue;
    public decimal Q3FFSRevenue;
    public decimal A0427Q3FFSRevenue;
    public decimal A0429Q3FFSRevenue;
    public decimal A0433Q3FFSRevenue;
    public decimal Q3MedRevenue;
    public decimal A0427Q3MedRevenue;
    public decimal A0429Q3MedRevenue;
    public decimal A0433Q3MedRevenue;
    public decimal Q3OtherRevenue;
    public decimal A0427Q3OtherRevenue;
    public decimal A0429Q3OtherRevenue;
    public decimal A0433Q3OtherRevenue;
    public decimal Q3TotalRevenue;
    public decimal A0427Q3TotalRevenue;
    public decimal A0429Q3TotalRevenue;
    public decimal A0433Q3TotalRevenue;
    public decimal Q4MCRevenue;
    public decimal A0427Q4MCRevenue;
    public decimal A0429Q4MCRevenue;
    public decimal A0433Q4MCRevenue;
    public decimal Q4FFSRevenue;
    public decimal A0427Q4FFSRevenue;
    public decimal A0429Q4FFSRevenue;
    public decimal A0433Q4FFSRevenue;
    public decimal Q4MedRevenue;
    public decimal A0427Q4MedRevenue;
    public decimal A0429Q4MedRevenue;
    public decimal A0433Q4MedRevenue;
    public decimal Q4OtherRevenue;
    public decimal A0427Q4OtherRevenue;
    public decimal A0429Q4OtherRevenue;
    public decimal A0433Q4OtherRevenue;
    public decimal Q4TotalRevenue;
    public decimal A0427Q4TotalRevenue;
    public decimal A0429Q4TotalRevenue;
    public decimal A0433Q4TotalRevenue;
    public decimal YearMCRevenue;
    public decimal A0427YearMCRevenue;
    public decimal A0429YearMCRevenue;
    public decimal A0433YearMCRevenue;
    public decimal YearFFSRevenue;
    public decimal A0427YearFFSRevenue;
    public decimal A0429YearFFSRevenue;
    public decimal A0433YearFFSRevenue;
    public decimal YearMedRevenue;
    public decimal A0427YearMedRevenue;
    public decimal A0429YearMedRevenue;
    public decimal A0433YearMedRevenue;
    public decimal YearOtherRevenue;
    public decimal A0427YearOtherRevenue;
    public decimal A0429YearOtherRevenue;
    public decimal A0433YearOtherRevenue;
    public decimal YearTotalRevenue;
    public decimal A0427YearTotalRevenue;
    public decimal A0429YearTotalRevenue;
    public decimal A0433YearTotalRevenue;
    
    public List<string> FFSPayers = new List<string>();
    public List<string> MCPayers = new List<string>();
    public List<string> MedicarePrimaryPayers = new List<string>(); 
    //public List<string> OtherPayers = new List<string>();
    public List<string> ValidProcCodes = new List<string>();

    private void Data1_AfterData(object sender, EventArgs e)
    {
      string name = ((String)Report.GetColumnValue("Selections.Property.Name"));
      if (name == "Date Of Service")
      {
        ComparisonOperator op = ((ComparisonOperator)Report.GetColumnValue("Selections.ComparisonOperator"));
        if ( op==ComparisonOperator.GreaterThanOrEqual )
          Report.SetParameterValue("StartDate", new SmartDate(((string)Report.GetColumnValue("Selections.ValueAsString"))).Date);
      }

      if (name == "Company")
        Report.SetParameterValue("CompanyCode", ((Object)Report.GetColumnValue("Selections.Value")));
      
      if(!FFSPayers.Contains("MCAID"))
        FFSPayers.Add("MCAID");
      if(!MCPayers.Contains("MDHMO"))
        MCPayers.Add("MDHMO");
      if(!MedicarePrimaryPayers.Contains("MCARE"))
        MedicarePrimaryPayers.Add("MCARE");
      if(!MedicarePrimaryPayers.Contains("MCHMO"))
        MedicarePrimaryPayers.Add("MCHMO");
      
      if(!ValidProcCodes.Contains("A0427"))
        ValidProcCodes.Add("A0427");
      if(!ValidProcCodes.Contains("A0429"))
        ValidProcCodes.Add("A0429");
      if(!ValidProcCodes.Contains("A0433"))
        ValidProcCodes.Add("A0433");
    }
    
    public string getMedicaidSecRunFromDB(string runNumber, DateTime beginDate, string company)
    {
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select tt.RunNumber"
          + " from bill_t_TripTicket tt"
          + " join bill_t_TripTicketPayer tp1 on tt.RunNumber=tp1.RunNumber"
          + " and tp1.PayerCode in ("
          + " select top 1 tpiii.PayerCode"
          + " from bill_t_TripTicketPayer tpiii"
          + " join bill_c_Payer paiii on tpiii.PayerCode=paiii.PayerCode"
          + " and tt.RunNumber=tpiii.RunNumber"
          + " order by tpiii.Priority"
          + " )"
          + " join bill_c_Payer pa1 on tp1.PayerCode=pa1.PayerCode"
          + " and pa1.PayerGroupCode in ('MCARE','MCHMO')"
          + " join bill_t_TripTicketPayer tp2 on tt.RunNumber=tp2.RunNumber"
          + " and tp2.PayerCode in ("
          + " select top 1 tbl1.PayerCode"
          + "   from ("
          + " select top 2 tpii.PayerCode, tpii.Priority"
          + "   from bill_t_TripTicketPayer tpii"
          + " join bill_c_Payer paii on tpii.PayerCode=paii.PayerCode"
          + " and tt.RunNumber=tpii.RunNumber"
          + " order by tpii.Priority"
          + " ) as tbl1"
          + " order by tbl1.Priority desc"
          + " )"
          + " join bill_c_Payer pa2 on tp2.PayerCode=pa2.PayerCode"
          + " and pa2.PayerGroupCode in ('MCAID','MDHMO')"
          + " where CompanyCode = @CompanyCode"
          + " and DateOfService between @StartDate and DATEADD(day,-1,DATEADD(quarter,4,@StartDate))"
          + " and tt.RunNumber = @RunNumber"; 
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {                                    
          cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
          cmd.Parameters["@CompanyCode"].Value = company;
          cmd.Parameters.Add("@StartDate", SqlDbType.Date);
          cmd.Parameters["@StartDate"].Value = beginDate;
          cmd.Parameters.Add("@RunNumber", SqlDbType.NVarChar);
          cmd.Parameters["@RunNumber"].Value = runNumber;
          
          try
          {
            return (string)cmd.ExecuteScalar();
          }
          catch (Exception ex)
          {
            MessageBox.Show("Can't get run number - " + ex.Message);
          }
        }
      }
      return "";
    }
    
    public int Q1FFSRunCounts()
    {
      return Q1FFSCount;
    }
    
    public int A0427Q1FFSRunCounts()
    {
      return A0427Q1FFSCount;
    }
    
    public int A0429Q1FFSRunCounts()
    {
      return A0429Q1FFSCount;
    }
    
    public int A0433Q1FFSRunCounts()
    {
      return A0433Q1FFSCount;
    }
    
    public int Q1MCRunCounts()
    {
      return Q1MCCount;
    }
    
    public int A0427Q1MCRunCounts()
    {
      return A0427Q1MCCount;
    }
    
    public int A0429Q1MCRunCounts()
    {
      return A0429Q1MCCount;
    }
    
    public int A0433Q1MCRunCounts()
    {
      return A0433Q1MCCount;
    }
    
    public int Q1MCAIDSecRunCounts()
    {
      return Q1SecondaryRunCount;
    }
    
    public int A0427Q1MCAIDSecRunCounts()
    {
      return A0427Q1SecondaryRunCount;
    }
    
    public int A0429Q1MCAIDSecRunCounts()
    {
      return A0429Q1SecondaryRunCount;
    }
    
    public int A0433Q1MCAIDSecRunCounts()
    {
      return A0433Q1SecondaryRunCount;
    }
    
    public int Q1MedRunCounts()
    {
      return Q1MedCount;
    }
    
    public int A0427Q1MedRunCounts()
    {
      return A0427Q1MedCount;
    }
    
    public int A0429Q1MedRunCounts()
    {
      return A0429Q1MedCount;
    }
    
    public int A0433Q1MedRunCounts()
    {
      return A0433Q1MedCount;
    }
    
    public int Q1OtherRunCounts()
    {
      return Q1OtherRunCount;
    }
    
    public int A0427Q1OtherRunCounts()
    {
      return A0427Q1OtherRunCount;
    }
    
    public int A0429Q1OtherRunCounts()
    {
      return A0429Q1OtherRunCount;
    }
    
    public int A0433Q1OtherRunCounts()
    {
      return A0433Q1OtherRunCount;
    }
    
    public int Q1RunTotals()
    {
      return Q1TotalRunCount; 
    }
    
    public int A0427Q1RunTotals()
    {
      return A0427Q1TotalRunCount; 
    }
    
    public int A0429Q1RunTotals()
    {
      return A0429Q1TotalRunCount; 
    }
    
    public int A0433Q1RunTotals()
    {
      return A0433Q1TotalRunCount; 
    }
    
    public int Q2FFSRunCounts()
    {
      return Q2FFSCount;
    }
    
    public int A0427Q2FFSRunCounts()
    {
      return A0427Q2FFSCount;
    }
    
    public int A0429Q2FFSRunCounts()
    {
      return A0429Q2FFSCount;
    }
    
    public int A0433Q2FFSRunCounts()
    {
      return A0433Q2FFSCount;
    }
    
    public int Q2MCRunCounts()
    {
      return Q2MCCount;
    }
    
    public int A0427Q2MCRunCounts()
    {
      return A0427Q2MCCount;
    }
    
    public int A0429Q2MCRunCounts()
    {
      return A0429Q2MCCount;
    }
    
    public int A0433Q2MCRunCounts()
    {
      return A0433Q2MCCount;
    }
    
    public int Q2MCAIDSecRunCounts()
    {
      return Q2SecondaryRunCount;
    }
    
    public int A0427Q2MCAIDSecRunCounts()
    {
      return A0427Q2SecondaryRunCount;
    }
    
    public int A0429Q2MCAIDSecRunCounts()
    {
      return A0429Q2SecondaryRunCount;
    }
    
    public int A0433Q2MCAIDSecRunCounts()
    {
      return A0433Q2SecondaryRunCount;
    }
    
    public int Q2MedRunCounts()
    {
      return Q2MedCount;
    }
    
    public int A0427Q2MedRunCounts()
    {
      return A0427Q2MedCount;
    }
    
    public int A0429Q2MedRunCounts()
    {
      return A0429Q2MedCount;
    }
    
    public int A0433Q2MedRunCounts()
    {
      return A0433Q2MedCount;
    }
    
    public int Q2OtherRunCounts()
    {
      return Q2OtherRunCount;
    }
    
    public int A0427Q2OtherRunCounts()
    {
      return A0427Q2OtherRunCount;
    }
    
    public int A0429Q2OtherRunCounts()
    {
      return A0429Q2OtherRunCount;
    }
    
    public int A0433Q2OtherRunCounts()
    {
      return A0433Q2OtherRunCount;
    }
    
    public int Q2RunTotals()
    {
      return Q2TotalRunCount; 
    }
    
    public int A0427Q2RunTotals()
    {
      return A0427Q2TotalRunCount; 
    }
    
    public int A0429Q2RunTotals()
    {
      return A0429Q2TotalRunCount; 
    }
    
    public int A0433Q2RunTotals()
    {
      return A0433Q2TotalRunCount; 
    }
    
    public int Q3FFSRunCounts()
    {
      return Q3FFSCount;
    }
    
    public int A0427Q3FFSRunCounts()
    {
      return A0427Q3FFSCount;
    }
    
    public int A0429Q3FFSRunCounts()
    {
      return A0429Q3FFSCount;
    }
    
    public int A0433Q3FFSRunCounts()
    {
      return A0433Q3FFSCount;
    }
    
    public int Q3MCRunCounts()
    {
      return Q3MCCount;
    }
    
    public int A0427Q3MCRunCounts()
    {
      return A0427Q3MCCount;
    }
    
    public int A0429Q3MCRunCounts()
    {
      return A0429Q3MCCount;
    }
    
    public int A0433Q3MCRunCounts()
    {
      return A0433Q3MCCount;
    }
    
    public int Q3MCAIDSecRunCounts()
    {
      return Q3SecondaryRunCount;
    }
    
    public int A0427Q3MCAIDSecRunCounts()
    {
      return A0427Q3SecondaryRunCount;
    }
    
    public int A0429Q3MCAIDSecRunCounts()
    {
      return A0429Q3SecondaryRunCount;
    }
    
    public int A0433Q3MCAIDSecRunCounts()
    {
      return A0433Q3SecondaryRunCount;
    }
    
    public int Q3MedRunCounts()
    {
      return Q3MedCount;
    }
    
    public int A0427Q3MedRunCounts()
    {
      return A0427Q3MedCount;
    }
    
    public int A0429Q3MedRunCounts()
    {
      return A0429Q3MedCount;
    }
    
    public int A0433Q3MedRunCounts()
    {
      return A0433Q3MedCount;
    }
    
    public int Q3OtherRunCounts()
    {
      return Q3OtherRunCount;
    } 
    
    public int A0427Q3OtherRunCounts()
    {
      return A0427Q3OtherRunCount;
    }
    
    public int A0429Q3OtherRunCounts()
    {
      return A0429Q3OtherRunCount;
    }
    
    public int A0433Q3OtherRunCounts()
    {
      return A0433Q3OtherRunCount;
    }
    
    public int Q3RunTotals()
    {
      return Q3TotalRunCount; 
    } 
    
    public int A0427Q3RunTotals()
    {
      return A0427Q3TotalRunCount; 
    }
    
    public int A0429Q3RunTotals()
    {
      return A0429Q3TotalRunCount; 
    }
    
    public int A0433Q3RunTotals()
    {
      return A0433Q3TotalRunCount; 
    }
    
    public int Q4FFSRunCounts()
    {
      return Q4FFSCount;
    }
    
    public int A0427Q4FFSRunCounts()
    {
      return A0427Q4FFSCount;
    }
    
    public int A0429Q4FFSRunCounts()
    {
      return A0429Q4FFSCount;
    }
    
    public int A0433Q4FFSRunCounts()
    {
      return A0433Q4FFSCount;
    }
    
    public int Q4MCRunCounts()
    {
      return Q4MCCount;
    }
    
    public int A0427Q4MCRunCounts()
    {
      return A0427Q4MCCount;
    }
    
    public int A0429Q4MCRunCounts()
    {
      return A0429Q4MCCount;
    }
    
    public int A0433Q4MCRunCounts()
    {
      return A0433Q4MCCount;
    }
    
    public int Q4MCAIDSecRunCounts()
    {
      return Q4SecondaryRunCount;
    }
    
    public int A0427Q4MCAIDSecRunCounts()
    {
      return A0427Q4SecondaryRunCount;
    }
    
    public int A0429Q4MCAIDSecRunCounts()
    {
      return A0429Q4SecondaryRunCount;
    }
    
    public int A0433Q4MCAIDSecRunCounts()
    {
      return A0433Q4SecondaryRunCount;
    }
    
    public int Q4MedRunCounts()
    {
      return Q4MedCount;
    }
    
    public int A0427Q4MedRunCounts()
    {
      return A0427Q4MedCount;
    }
    
    public int A0429Q4MedRunCounts()
    {
      return A0429Q4MedCount;
    }
    
    public int A0433Q4MedRunCounts()
    {
      return A0433Q4MedCount;
    }
    
    public int Q4OtherRunCounts()
    {
      return Q4OtherRunCount;
    }
    
    public int A0427Q4OtherRunCounts()
    {
      return A0427Q4OtherRunCount;
    }
    
    public int A0429Q4OtherRunCounts()
    {
      return A0429Q4OtherRunCount;
    }
    
    public int A0433Q4OtherRunCounts()
    {
      return A0433Q4OtherRunCount;
    }
    
    public int Q4RunTotals()
    {
      return Q4TotalRunCount; 
    }
    
    public int A0427Q4RunTotals()
    {
      return A0427Q4TotalRunCount; 
    }
    
    public int A0429Q4RunTotals()
    {
      return A0429Q4TotalRunCount; 
    }
    
    public int A0433Q4RunTotals()
    {
      return A0433Q4TotalRunCount; 
    }
    
    public int YearFFSRunCounts()
    {
      return YearFFSCount;
    }
    
    public int A0427YearFFSRunCounts()
    {
      return A0427YearFFSCount;
    }
    
    public int A0429YearFFSRunCounts()
    {
      return A0429YearFFSCount;
    }
    
    public int A0433YearFFSRunCounts()
    {
      return A0433YearFFSCount;
    }
    
    public int YearMCRunCounts()
    {
      return YearMCCount;
    }
    
    public int A0427YearMCRunCounts()
    {
      return A0427YearMCCount;
    }
    
    public int A0429YearMCRunCounts()
    {
      return A0429YearMCCount;
    }
    
    public int A0433YearMCRunCounts()
    {
      return A0433YearMCCount;
    }
    
    public int YearMCAIDSecRunCounts()
    {
      return YearSecondaryRunCount;
    }
    
    public int A0427YearMCAIDSecRunCounts()
    {
      return A0427YearSecondaryRunCount;
    }
    
    public int A0429YearMCAIDSecRunCounts()
    {
      return A0429YearSecondaryRunCount;
    }
    
    public int A0433YearMCAIDSecRunCounts()
    {
      return A0433YearSecondaryRunCount;
    }
    
    public int YearMedRunCounts()
    {
      return YearMedCount;
    }
    
    public int A0427YearMedRunCounts()
    {
      return A0427YearMedCount;
    }
    
    public int A0429YearMedRunCounts()
    {
      return A0429YearMedCount;
    }
    
    public int A0433YearMedRunCounts()
    {
      return A0433YearMedCount;
    }
    
    public int YearOtherRunCounts()
    {
      return YearOtherRunCount;
    }
    
    public int A0427YearOtherRunCounts()
    {
      return A0427YearOtherRunCount;
    }
    
    public int A0429YearOtherRunCounts()
    {
      return A0429YearOtherRunCount;
    }
    
    public int A0433YearOtherRunCounts()
    {
      return A0433YearOtherRunCount;
    }
    
    public int YearRunTotals()
    {
      return YearTotalRunCount; 
    }
    
    public int A0427YearRunTotals()
    {
      return A0427YearTotalRunCount; 
    }
    
    public int A0429YearRunTotals()
    {
      return A0429YearTotalRunCount; 
    }
    
    public int A0433YearRunTotals()
    {
      return A0433YearTotalRunCount; 
    }
    
    public decimal Q1FFSPayments()
    {
      return Q1FFSRevenue; 
    }
    
    public decimal A0427Q1FFSPayments()
    {
      return A0427Q1FFSRevenue; 
    }
    
    public decimal A0429Q1FFSPayments()
    {
      return A0429Q1FFSRevenue; 
    }
    
    public decimal A0433Q1FFSPayments()
    {
      return A0433Q1FFSRevenue; 
    }
    
    public decimal Q1MCPayments()
    {
      return Q1MCRevenue; 
    }
    
    public decimal A0427Q1MCPayments()
    {
      return A0427Q1MCRevenue; 
    }
    
    public decimal A0429Q1MCPayments()
    {
      return A0429Q1MCRevenue; 
    }
    
    public decimal A0433Q1MCPayments()
    {
      return A0433Q1MCRevenue; 
    }
    
    public decimal Q1MedPayments()
    {
      return Q1MedRevenue; 
    }
    
    public decimal A0427Q1MedPayments()
    {
      return A0427Q1MedRevenue; 
    }
    
    public decimal A0429Q1MedPayments()
    {
      return A0429Q1MedRevenue; 
    }
    
    public decimal A0433Q1MedPayments()
    {
      return A0433Q1MedRevenue; 
    }
    
    public decimal Q1OtherPayments()
    {
      return Q1OtherRevenue; 
    }
    
    public decimal A0427Q1OtherPayments()
    {
      return A0427Q1OtherRevenue; 
    }
    
    public decimal A0429Q1OtherPayments()
    {
      return A0429Q1OtherRevenue; 
    }
    
    public decimal A0433Q1OtherPayments()
    {
      return A0433Q1OtherRevenue; 
    }
    
    public decimal Q1TotalPayments()
    {
      return Q1TotalRevenue; 
    }
    
    public decimal A0427Q1TotalPayments()
    {
      return A0427Q1TotalRevenue; 
    }
    
    public decimal A0429Q1TotalPayments()
    {
      return A0429Q1TotalRevenue; 
    }
    
    public decimal A0433Q1TotalPayments()
    {
      return A0433Q1TotalRevenue; 
    }
    
    public decimal Q2FFSPayments()
    {
      return Q2FFSRevenue; 
    }
    
    public decimal A0427Q2FFSPayments()
    {
      return A0427Q2FFSRevenue; 
    }
    
    public decimal A0429Q2FFSPayments()
    {
      return A0429Q2FFSRevenue; 
    }
    
    public decimal A0433Q2FFSPayments()
    {
      return A0433Q2FFSRevenue; 
    }
    
    public decimal Q2MCPayments()
    {
      return Q2MCRevenue; 
    }
    
    public decimal A0427Q2MCPayments()
    {
      return A0427Q2MCRevenue; 
    }
    
    public decimal A0429Q2MCPayments()
    {
      return A0429Q2MCRevenue; 
    }
    
    public decimal A0433Q2MCPayments()
    {
      return A0433Q2MCRevenue; 
    }
    
    public decimal Q2MedPayments()
    {
      return Q2MedRevenue; 
    }
    
    public decimal A0427Q2MedPayments()
    {
      return A0427Q2MedRevenue; 
    }
    
    public decimal A0429Q2MedPayments()
    {
      return A0429Q2MedRevenue; 
    }
    
    public decimal A0433Q2MedPayments()
    {
      return A0433Q2MedRevenue; 
    }
    
    public decimal Q2OtherPayments()
    {
      return Q2OtherRevenue; 
    }
    
    public decimal A0427Q2OtherPayments()
    {
      return A0427Q2OtherRevenue; 
    }
    
    public decimal A0429Q2OtherPayments()
    {
      return A0429Q2OtherRevenue; 
    }
    
    public decimal A0433Q2OtherPayments()
    {
      return A0433Q2OtherRevenue; 
    }
    
    public decimal Q2TotalPayments()
    {
      return Q2TotalRevenue; 
    }
    
    public decimal A0427Q2TotalPayments()
    {
      return A0427Q2TotalRevenue; 
    }
    
    public decimal A0429Q2TotalPayments()
    {
      return A0429Q2TotalRevenue; 
    }
    
    public decimal A0433Q2TotalPayments()
    {
      return A0433Q2TotalRevenue; 
    }
    
    public decimal Q3FFSPayments()
    {
      return Q3FFSRevenue; 
    }
    
    public decimal A0427Q3FFSPayments()
    {
      return A0427Q3FFSRevenue; 
    }
    
    public decimal A0429Q3FFSPayments()
    {
      return A0429Q3FFSRevenue; 
    }
    
    public decimal A0433Q3FFSPayments()
    {
      return A0433Q3FFSRevenue; 
    }
    
    public decimal Q3MCPayments()
    {
      return Q3MCRevenue; 
    }
    
    public decimal A0427Q3MCPayments()
    {
      return A0427Q3MCRevenue; 
    }
    
    public decimal A0429Q3MCPayments()
    {
      return A0429Q3MCRevenue; 
    }
    
    public decimal A0433Q3MCPayments()
    {
      return A0433Q3MCRevenue; 
    }
    
    public decimal Q3MedPayments()
    {
      return Q3MedRevenue; 
    }
    
    public decimal A0427Q3MedPayments()
    {
      return A0427Q3MedRevenue; 
    }
    
    public decimal A0429Q3MedPayments()
    {
      return A0429Q3MedRevenue; 
    }
    
    public decimal A0433Q3MedPayments()
    {
      return A0433Q3MedRevenue; 
    }
    
    public decimal Q3OtherPayments()
    {
      return Q3OtherRevenue; 
    }
    
    public decimal A0427Q3OtherPayments()
    {
      return A0427Q3OtherRevenue; 
    }
    
    public decimal A0429Q3OtherPayments()
    {
      return A0429Q3OtherRevenue; 
    }
    
    public decimal A0433Q3OtherPayments()
    {
      return A0433Q3OtherRevenue; 
    }
    
    public decimal Q3TotalPayments()
    {
      return Q3TotalRevenue; 
    }
    
    public decimal A0427Q3TotalPayments()
    {
      return A0427Q3TotalRevenue; 
    }
    
    public decimal A0429Q3TotalPayments()
    {
      return A0429Q3TotalRevenue; 
    }
    
    public decimal A0433Q3TotalPayments()
    {
      return A0433Q3TotalRevenue; 
    }
    
    public decimal Q4FFSPayments()
    {
      return Q4FFSRevenue; 
    }
    
    public decimal A0427Q4FFSPayments()
    {
      return A0427Q4FFSRevenue; 
    }
    
    public decimal A0429Q4FFSPayments()
    {
      return A0429Q4FFSRevenue; 
    }
    
    public decimal A0433Q4FFSPayments()
    {
      return A0433Q4FFSRevenue; 
    }
    
    public decimal Q4MCPayments()
    {
      return Q4MCRevenue; 
    }
    
    public decimal A0427Q4MCPayments()
    {
      return A0427Q4MCRevenue; 
    }
    
    public decimal A0429Q4MCPayments()
    {
      return A0429Q4MCRevenue; 
    }
    
    public decimal A0433Q4MCPayments()
    {
      return A0433Q4MCRevenue; 
    }
    
    public decimal Q4MedPayments()
    {
      return Q4MedRevenue; 
    }
    
    public decimal A0427Q4MedPayments()
    {
      return A0427Q4MedRevenue; 
    }
    
    public decimal A0429Q4MedPayments()
    {
      return A0429Q4MedRevenue; 
    }
    
    public decimal A0433Q4MedPayments()
    {
      return A0433Q4MedRevenue; 
    }
    
    public decimal Q4OtherPayments()
    {
      return Q4OtherRevenue; 
    }
    
    public decimal A0427Q4OtherPayments()
    {
      return A0427Q4OtherRevenue; 
    }
    
    public decimal A0429Q4OtherPayments()
    {
      return A0429Q4OtherRevenue; 
    }
    
    public decimal A0433Q4OtherPayments()
    {
      return A0433Q4OtherRevenue; 
    }
    
    public decimal Q4TotalPayments()
    {
      return Q4TotalRevenue; 
    }
    
    public decimal A0427Q4TotalPayments()
    {
      return A0427Q4TotalRevenue; 
    }
    
    public decimal A0429Q4TotalPayments()
    {
      return A0429Q4TotalRevenue; 
    }
    
    public decimal A0433Q4TotalPayments()
    {
      return A0433Q4TotalRevenue; 
    }
    
    public decimal YearFFSPayments()
    {
      return YearFFSRevenue; 
    }
    
    public decimal A0427YearFFSPayments()
    {
      return A0427YearFFSRevenue; 
    }
    
    public decimal A0429YearFFSPayments()
    {
      return A0429YearFFSRevenue; 
    }
    
    public decimal A0433YearFFSPayments()
    {
      return A0433YearFFSRevenue; 
    }
    
    public decimal YearMCPayments()
    {
      return YearMCRevenue; 
    }
    
    public decimal A0427YearMCPayments()
    {
      return A0427YearMCRevenue; 
    }
    
    public decimal A0429YearMCPayments()
    {
      return A0429YearMCRevenue; 
    }
    
    public decimal A0433YearMCPayments()
    {
      return A0433YearMCRevenue; 
    }
    
    public decimal YearMedPayments()
    {
      return YearMedRevenue; 
    }
    
    public decimal A0427YearMedPayments()
    {
      return A0427YearMedRevenue; 
    }
    
    public decimal A0429YearMedPayments()
    {
      return A0429YearMedRevenue; 
    }
    
    public decimal A0433YearMedPayments()
    {
      return A0433YearMedRevenue; 
    }
    
    public decimal YearOtherPayments()
    {
      return YearOtherRevenue; 
    }
    
    public decimal A0427YearOtherPayments()
    {
      return A0427YearOtherRevenue; 
    }
    
    public decimal A0429YearOtherPayments()
    {
      return A0429YearOtherRevenue; 
    }
    
    public decimal A0433YearOtherPayments()
    {
      return A0433YearOtherRevenue; 
    }
    
    public decimal YearTotalPayments()
    {
      return YearTotalRevenue; 
    }
    
    public decimal A0427YearTotalPayments()
    {
      return A0427YearTotalRevenue; 
    }
    
    public decimal A0429YearTotalPayments()
    {
      return A0429YearTotalRevenue; 
    }
    
    public decimal A0433YearTotalPayments()
    {
      return A0433YearTotalRevenue; 
    }
    
    private void GroupHeader2_AfterData(object sender, EventArgs e)
    {                                          
        SmartDate InitDos = new SmartDate();                             
        InitDos = (SmartDate)Report.GetColumnValue("TripTickets.DateOfService");  
        DateTime startDate = new DateTime();
        DateTime dos = new DateTime();
        startDate = (DateTime)Report.GetParameterValue("StartDate");
        dos = DateTime.Parse(InitDos.ToString());
        string currentProcCode = (string)Report.GetColumnValue("TripTickets.BaseCharge.ProcedureCode");  
      
        bool isEmergent = (bool)Report.GetColumnValue("TripTickets.Emergency");
        
        string curTicket = (string)Report.GetColumnValue("TripTickets.RunNumber");
      
        string companyCode = (string)Report.GetParameterValue("CompanyCode"); 
      
      if(isEmergent)
      {  
        if(ValidProcCodes.Contains(currentProcCode))
        {                     
          if(dos>=startDate && dos<=startDate.AddYears(1).AddDays(-1))
          {
            if(dos>=startDate && dos<=startDate.AddMonths(3).AddDays(-1))
            { 
              if(curTicket==getMedicaidSecRunFromDB(curTicket, startDate, companyCode))
              {
                Q1SecondaryRunCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q1SecondaryRunCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q1SecondaryRunCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q1SecondaryRunCount++;
                }
              }
              else if(FFSPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
              {      
                Q1FFSCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q1FFSCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q1FFSCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q1FFSCount++;
                } 
              }
              else if(MCPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
              {    
                Q1MCCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q1MCCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q1MCCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q1MCCount++;
                }    
              }  
              else if(MedicarePrimaryPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
              {
                Q1MedCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q1MedCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q1MedCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q1MedCount++;
                } 
              }
              else
              {
                Q1OtherRunCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q1OtherRunCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q1OtherRunCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q1OtherRunCount++;
                }
              }
              Q1TotalRunCount++;
              if(currentProcCode=="A0427")
              {
                A0427Q1TotalRunCount++;
              }
              else if(currentProcCode=="A0429")
              {
                A0429Q1TotalRunCount++;
              }
              else if(currentProcCode=="A0433")
              {
                A0433Q1TotalRunCount++;
              }
            }
            else if(dos>=startDate.AddMonths(3) && dos<=startDate.AddMonths(6).AddDays(-1))
            {
              if(curTicket==getMedicaidSecRunFromDB(curTicket, startDate, companyCode))
              {
                Q2SecondaryRunCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q2SecondaryRunCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q2SecondaryRunCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q2SecondaryRunCount++;
                }
              }
              else if(FFSPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
              {
                Q2FFSCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q2FFSCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q2FFSCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q2FFSCount++;
                }
              }
              else if(MCPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
              {
                Q2MCCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q2MCCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q2MCCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q2MCCount++;
                }
              }
              else if(MedicarePrimaryPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
              {
                Q2MedCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q2MedCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q2MedCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q2MedCount++;
                } 
              }
              else
              {
                Q2OtherRunCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q2OtherRunCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q2OtherRunCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q2OtherRunCount++;
                }
              }
              Q2TotalRunCount++;
              if(currentProcCode=="A0427")
              {
                A0427Q2TotalRunCount++;
              }
              else if(currentProcCode=="A0429")
              {
                A0429Q2TotalRunCount++;
              }
              else if(currentProcCode=="A0433")
              {
                A0433Q2TotalRunCount++;
              }
            }
            else if(dos>=startDate.AddMonths(6) && dos<=startDate.AddMonths(9).AddDays(-1))
            {
              if(curTicket==getMedicaidSecRunFromDB(curTicket, startDate, companyCode))
              {
                Q3SecondaryRunCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q3SecondaryRunCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q3SecondaryRunCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q3SecondaryRunCount++;
                }
              }
              else if(FFSPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
              {
                Q3FFSCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q3FFSCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q3FFSCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q3FFSCount++;
                }
              }
              else if(MCPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
              {
                Q3MCCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q3MCCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q3MCCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q3MCCount++;
                }
              }
              else if(MedicarePrimaryPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
              {
                Q3MedCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q3MedCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q3MedCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q3MedCount++;
                } 
              }
              else
              {
                Q3OtherRunCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q3OtherRunCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q3OtherRunCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q3OtherRunCount++;
                }
              }
              Q3TotalRunCount++;
              if(currentProcCode=="A0427")
              {
                A0427Q3TotalRunCount++;
              }
              else if(currentProcCode=="A0429")
              {
                A0429Q3TotalRunCount++;
              }
              else if(currentProcCode=="A0433")
              {
                A0433Q3TotalRunCount++;
              }
            }
            else if(dos>=startDate.AddMonths(9) && dos<=startDate.AddMonths(12).AddDays(-1))
            {
              if(curTicket==getMedicaidSecRunFromDB(curTicket, startDate, companyCode))
              {
                Q4SecondaryRunCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q4SecondaryRunCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q4SecondaryRunCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q4SecondaryRunCount++;
                }
              }
              else if(FFSPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
              {
                Q4FFSCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q4FFSCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q4FFSCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q4FFSCount++;
                }
              }
              else if(MCPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
              {
                Q4MCCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q4MCCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q4MCCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q4MCCount++;
                }
              }
              else if(MedicarePrimaryPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
              {
                Q4MedCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q4MedCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q4MedCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q4MedCount++;
                } 
              }
              else
              {
                Q4OtherRunCount++;
                if(currentProcCode=="A0427")
                {
                  A0427Q4OtherRunCount++;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q4OtherRunCount++;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q4OtherRunCount++;
                }
              }
              Q4TotalRunCount++;
              if(currentProcCode=="A0427")
              {
                A0427Q4TotalRunCount++;
              }
              else if(currentProcCode=="A0429")
              {
                A0429Q4TotalRunCount++;
              }
              else if(currentProcCode=="A0433")
              {
                A0433Q4TotalRunCount++;
              }
            }
            // Year Totals
            if(curTicket==getMedicaidSecRunFromDB(curTicket, startDate, companyCode))
            {
              YearSecondaryRunCount++;
              if(currentProcCode=="A0427")
              {
                A0427YearSecondaryRunCount++;
              }
              else if(currentProcCode=="A0429")
              {
                A0429YearSecondaryRunCount++;
              }
              else if(currentProcCode=="A0433")
              {
                A0433YearSecondaryRunCount++;
              }
            }
            else if(FFSPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
            {
              YearFFSCount++;
              if(currentProcCode=="A0427")
              {
                A0427YearFFSCount++;
              }
              else if(currentProcCode=="A0429")
              {
                A0429YearFFSCount++;
              }
              else if(currentProcCode=="A0433")
              {
                A0433YearFFSCount++;
              }
            }
            else if(MCPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
            {
              YearMCCount++;
              if(currentProcCode=="A0427")
              {
                A0427YearMCCount++;
              }
              else if(currentProcCode=="A0429")
              {
                A0429YearMCCount++;
              }
              else if(currentProcCode=="A0433")
              {
                A0433YearMCCount++;
              }
            }
            else if(MedicarePrimaryPayers.Contains((string)Report.GetColumnValue("TripTickets.Payers.Payer.PayerGroupCode")))
            {
              YearMedCount++;
              if(currentProcCode=="A0427")
              {
                A0427YearMedCount++;
              }
              else if(currentProcCode=="A0429")
              {
                A0429YearMedCount++;
              }
              else if(currentProcCode=="A0433")
              {
                A0433YearMedCount++;
              }
            }
            else
            {
              YearOtherRunCount++;
              if(currentProcCode=="A0427")
              {
                A0427YearOtherRunCount++;
              }
              else if(currentProcCode=="A0429")
              {
                A0429YearOtherRunCount++;
              }
              else if(currentProcCode=="A0433")
              {
                A0433YearOtherRunCount++;
              }
            }
            YearTotalRunCount++;
            if(currentProcCode=="A0427")
            {
              A0427YearTotalRunCount++;
            }
            else if(currentProcCode=="A0429")
            {
              A0429YearTotalRunCount++;
            }
            else if(currentProcCode=="A0433")
            {
              A0433YearTotalRunCount++;
            }
          }
        }
      }
     }

    private void Data2_AfterData(object sender, EventArgs e)
    {
      SmartDate InitAdjudicationDate = new SmartDate();
      DateTime startAdjDate = new DateTime();
      DateTime adjudicationDate = new DateTime();
      startAdjDate = (DateTime)Report.GetParameterValue("StartDate");
      InitAdjudicationDate = (SmartDate)Report.GetColumnValue("TripTickets.AR.AdjudicationDate");
      adjudicationDate = DateTime.Parse(InitAdjudicationDate.ToString());
      JefBar.Billing.Business.SystemTransactionType paymentType = SystemTransactionType.Payment;
      string currentProcCode = (string)Report.GetColumnValue("TripTickets.BaseCharge.ProcedureCode");
      decimal amount = (decimal)Report.GetColumnValue("TripTickets.AR.Amount"); 
      bool isEmergent = (bool)Report.GetColumnValue("TripTickets.Emergency");
      
      if(isEmergent)
      {
        if(ValidProcCodes.Contains(currentProcCode))
        {
          if(paymentType == (SystemTransactionType)Report.GetColumnValue("TripTickets.AR.SystemTransactionType"))
          {
            if(adjudicationDate>=startAdjDate && adjudicationDate<=startAdjDate.AddYears(1).AddDays(-1))
            {
              if(adjudicationDate>=startAdjDate && adjudicationDate<=startAdjDate.AddMonths(3).AddDays(-1))
              {
                if(FFSPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
                {
                  Q1FFSRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q1FFSRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q1FFSRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q1FFSRevenue+=amount;
                  }
                }
                else if(MCPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
                {
                  Q1MCRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q1MCRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q1MCRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q1MCRevenue+=amount;
                  }
                }
                else if(MedicarePrimaryPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
                {
                  Q1MedRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q1MedRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q1MedRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q1MedRevenue+=amount;
                  }
                }
                else
                {
                  Q1OtherRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q1OtherRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q1OtherRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q1OtherRevenue+=amount;
                  }
                }
                Q1TotalRevenue+=amount;
                if(currentProcCode=="A0427")
                {
                  A0427Q1TotalRevenue+=amount;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q1TotalRevenue+=amount;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q1TotalRevenue+=amount;
                }
              }
              else if(adjudicationDate>=startAdjDate.AddMonths(3) && adjudicationDate<=startAdjDate.AddMonths(6).AddDays(-1))
              {
                if(FFSPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
                {
                  Q2FFSRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q2FFSRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q2FFSRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q2FFSRevenue+=amount;
                  }
                }
                else if(MCPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
                {
                  Q2MCRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q2MCRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q2MCRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q2MCRevenue+=amount;
                  }
                }
                else if(MedicarePrimaryPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
                {
                  Q2MedRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q2MedRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q2MedRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q2MedRevenue+=amount;
                  }
                }
                else
                {
                  Q2OtherRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q2OtherRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q2OtherRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q2OtherRevenue+=amount;
                  }
                }
                Q2TotalRevenue+=amount;
                if(currentProcCode=="A0427")
                {
                  A0427Q2TotalRevenue+=amount;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q2TotalRevenue+=amount;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q2TotalRevenue+=amount;
                }
              }
              else if(adjudicationDate>=startAdjDate.AddMonths(6) && adjudicationDate<=startAdjDate.AddMonths(9).AddDays(-1))
              {
                if(FFSPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
                {
                  Q3FFSRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q3FFSRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q3FFSRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q3FFSRevenue+=amount;
                  }
                }
                else if(MCPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
                {
                  Q3MCRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q3MCRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q3MCRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q3MCRevenue+=amount;
                  }
                }
                else if(MedicarePrimaryPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
                {
                  Q3MedRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q3MedRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q3MedRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q3MedRevenue+=amount;
                  }
                } 
                else
                {
                  Q3OtherRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q3OtherRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q3OtherRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q3OtherRevenue+=amount;
                  }
                }
                Q3TotalRevenue+=amount;
                if(currentProcCode=="A0427")
                {
                  A0427Q3TotalRevenue+=amount;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q3TotalRevenue+=amount;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q3TotalRevenue+=amount;
                }
              }
              else if(adjudicationDate>=startAdjDate.AddMonths(9) && adjudicationDate<=startAdjDate.AddMonths(12).AddDays(-1))
              {
                if(FFSPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
                {
                  Q4FFSRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q4FFSRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q4FFSRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q4FFSRevenue+=amount;
                  }
                }
                else if(MCPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
                {
                  Q4MCRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q4MCRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q4MCRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q4MCRevenue+=amount;
                  }
                }
                else if(MedicarePrimaryPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
                {
                  Q4MedRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q4MedRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q4MedRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q4MedRevenue+=amount;
                  }
                }
                else
                {
                  Q4OtherRevenue+=amount;
                  if(currentProcCode=="A0427")
                  {
                    A0427Q4OtherRevenue+=amount;
                  }
                  else if(currentProcCode=="A0429")
                  {
                    A0429Q4OtherRevenue+=amount;
                  }
                  else if(currentProcCode=="A0433")
                  {
                    A0433Q4OtherRevenue+=amount;
                  }  
                }
                Q4TotalRevenue+=amount;
                if(currentProcCode=="A0427")
                {
                  A0427Q4TotalRevenue+=amount;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429Q4TotalRevenue+=amount;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433Q4TotalRevenue+=amount;
                }
              }
              //Total Revenue
              if(FFSPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
              {
                YearFFSRevenue+=amount;
                if(currentProcCode=="A0427")
                {
                  A0427YearFFSRevenue+=amount;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429YearFFSRevenue+=amount;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433YearFFSRevenue+=amount;
                }
              }
              else if(MCPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
              {
                YearMCRevenue+=amount;
                if(currentProcCode=="A0427")
                {
                  A0427YearMCRevenue+=amount;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429YearMCRevenue+=amount;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433YearMCRevenue+=amount;
                }
              }
              else if(MedicarePrimaryPayers.Contains((string)Report.GetColumnValue("TripTickets.AR.Payer.PayerGroupCode")))
              {
                YearMedRevenue+=amount;
                if(currentProcCode=="A0427")
                {
                  A0427YearMedRevenue+=amount;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429YearMedRevenue+=amount;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433YearMedRevenue+=amount;
                }
              }
              else
              {
                YearOtherRevenue+=amount;
                if(currentProcCode=="A0427")
                {
                  A0427YearOtherRevenue+=amount;
                }
                else if(currentProcCode=="A0429")
                {
                  A0429YearOtherRevenue+=amount;
                }
                else if(currentProcCode=="A0433")
                {
                  A0433YearOtherRevenue+=amount;
                }
              }
              YearTotalRevenue+=amount;
              if(currentProcCode=="A0427")
              {
                A0427YearTotalRevenue+=amount;
              }
              else if(currentProcCode=="A0429")
              {
                A0429YearTotalRevenue+=amount;
              }
              else if(currentProcCode=="A0433")
              {
                A0433YearTotalRevenue+=amount;
              }
            }
          }
        }
      }
    }  
  }
}
