using System.Data.SqlClient;
using Csla;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Text;
using FastReport;
using FastReport.Data;
using FastReport.Dialog;
using FastReport.Barcode;
using FastReport.Table;
using FastReport.Utils;
using JefBar.Billing.WPF;
using System.Windows;


namespace FastReport
{
  public class ReportScript
  {

    /*private void Text19_Click(object sender, EventArgs e)
    {
      TextObject text = (TextObject)sender;
      System.Windows.Application app = ((System.Windows.Application)Report.GetParameterValue("Application"));
      app.Dispatcher.BeginInvoke((Action)(()=> TripTicketEditView.Show(text.Text)));
    }*/
    
    StringBuilder errors = new StringBuilder();
    
    public string Errors
    {
      get { return errors.ToString(); }
    }

    public DateTime InitialBilledDate(string runNumber)
    {
      DateTime dbDate = new DateTime();
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select isnull(min(IssuedDate),'0001-01-01') as InitialBilledDate from bill_t_TripTicket tt"
          + " join bill_t_TripTicketPayer tp on tt.RunNumber=tp.RunNumber"
          + " join bill_t_TripTicketBillSchedule tb on tp.TripTicketPayerId=tb.TripTicketPayerId"
          + " where tt.RunNumber=@RunNumber";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@RunNumber", SqlDbType.NVarChar);
          cmd.Parameters["@RunNumber"].Value = runNumber;
          
          try {
            dbDate = (DateTime)cmd.ExecuteScalar();
          } catch (Exception ex) {
            errors.AppendFormat("Ticket {0} initial billed date could not be found. Error: {1} \r\n", runNumber, ex.Message);
          }                           
        }
      }
      return dbDate;  
    }

    public decimal? InitialBilledAmount(string runNumber)
    {
      decimal? dbAmount = null;
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select top 1 isnull(ar.Amount,0) as FirstPaymentDate from bill_t_TripTicket tt"
          + " join bill_t_ARTransaction ar on tt.RunNumber=ar.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where tt.RunNumber=@RunNumber"
          + " and SystemTransactionType=0"
          + " order by ar.EnteredDate"; 
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@RunNumber", SqlDbType.NVarChar);
          cmd.Parameters["@RunNumber"].Value = runNumber;
          
          try {
            dbAmount = (decimal?)cmd.ExecuteScalar();
          } catch (Exception ex) {
            errors.AppendFormat("Ticket {0} initial billed amount could not be found. Error: {1} \r\n", runNumber, ex.Message);
          }                           
        }
      }
      if(dbAmount==null)
        dbAmount=0;  
      return dbAmount;
    }

    public DateTime FirstPaymentDate(string runNumber)
    {
      DateTime dbDate = new DateTime();
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select isnull(min(ar.EnteredDate),'0001-01-01') as FirstPaymentDate from bill_t_TripTicket tt"
          + " join bill_t_ARTransaction ar on tt.RunNumber=ar.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where tt.RunNumber=@RunNumber"
          + " and SystemTransactionType=1";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@RunNumber", SqlDbType.NVarChar);
          cmd.Parameters["@RunNumber"].Value = runNumber;
          
          try {
            dbDate = (DateTime)cmd.ExecuteScalar();
          } catch (Exception ex) {
            errors.AppendFormat("Ticket {0} first payment date could not be found. Error: {1} \r\n", runNumber, ex.Message);
          }                           
        }
      }
      return dbDate;  
    }

    public decimal? FirstPaymentAmount(string runNumber)
    {
      decimal? dbAmount = null;
      using (SqlConnection conn = new SqlConnection(((SqlConnectionStringBuilder)Report.GetParameterValue("Connection")).ConnectionString))
      {
        conn.Open();
        string sql = "select top 1 isnull(ar.Amount,0) as FirstPaymentDate from bill_t_TripTicket tt"
          + " join bill_t_ARTransaction ar on tt.RunNumber=ar.RunNumber"
          + " join bill_c_TransactionType ty on ar.TransactionTypeCode=ty.TransactionTypeCode"
          + " where tt.RunNumber=@RunNumber"
          + " and SystemTransactionType=1"
          + " order by ar.EnteredDate"; 
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
          cmd.Parameters.Add("@RunNumber", SqlDbType.NVarChar);
          cmd.Parameters["@RunNumber"].Value = runNumber;
          
          try {
            dbAmount = (decimal?)cmd.ExecuteScalar();
          } catch (Exception ex) {
            errors.AppendFormat("Ticket {0} first payment amount could not be found. Error: {1} \r\n", runNumber, ex.Message);
          }                           
        }
      }
      if(dbAmount==null)
        dbAmount=0;  
      return dbAmount;
    }
	
    public double totalDays = 0;
    public double totalEntDays = 0;

    public int DaysBetweenEnteredAndBilled(DateTime enteredDate, DateTime billedDate)
    {
      int retDays = 0;
      TimeSpan numDays = billedDate.Date-enteredDate.Date;
      retDays = numDays.Days;
      totalEntDays += retDays;
      return retDays;
    }
       
    public int DaysBetweenEnteredAndPaid(string enteredDate, DateTime paidDate)
    {
      int retDays = 0;
      DateTime newEnteredDate = DateTime.ParseExact(enteredDate, "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture);
      TimeSpan numDays = paidDate.Date-newEnteredDate.Date;
      retDays = numDays.Days;
	
      return retDays;
    }

    public int DaysBetweenBilledAndPaid(DateTime billedDate, DateTime paidDate)
    {
      int retDays = 0;
      TimeSpan numDays = paidDate.Date-billedDate.Date;
      retDays = numDays.Days;
      totalDays += retDays;
      return retDays;
    }    
    
    public double avgDays = 0;
    public double avgBilledDays = 0;
    public double avgDays2 = 0;

    
    public double getAverageAR()
    {
      if ((InitialBilledDate((string)Report.GetColumnValue("TripTickets.RunNumber"))).ToString("MM-dd-yyyy") == "01-01-0001" || (FirstPaymentDate((string)Report.GetColumnValue("TripTickets.RunNumber"))).ToString("MM-dd-yyyy") == "01-01-0001")
      {
        return 0;
      }
      else 
      {
        DateTime billedDate = (InitialBilledDate((string)Report.GetColumnValue("TripTickets.RunNumber")));
        DateTime paidDate = ((FirstPaymentDate((string)Report.GetColumnValue("TripTickets.RunNumber"))));
        TimeSpan diff = paidDate.Date - billedDate.Date;

        if (diff.Days < 0)
        {
          return 0;
        }
        else
        {
          totalDays = 0;
          DaysBetweenBilledAndPaid(billedDate, paidDate);
          Report.SetParameterValue("TotalDays",totalDays);
          avgDays = (totalDays / (double) Report.GetTotalValue("TotalTickets"));
          return avgDays;              
        } 
      }

    }

    public double getAverageBE()
    {
      if ((InitialBilledDate((string)Report.GetColumnValue("TripTickets.RunNumber")).ToString("MM-dd-yyyy") == "01-01-0001" || ((SmartDate)Report.GetColumnValue("TripTickets.EnteredDate"))== "0001-01-01"))
      {
        return 0;
      }
      else
      {
        DateTime billedDate = (InitialBilledDate((string)Report.GetColumnValue("TripTickets.RunNumber")));
        DateTime enteredDate = ((SmartDate)Report.GetColumnValue("TripTickets.EnteredDate"));
        enteredDate.ToString("yyyy-MM-dd");        
        TimeSpan diff = billedDate.Date - enteredDate.Date;
        if (diff.Days < 0)
        {
          return 0;
        }
        else
        {
          totalEntDays = 0;
          DaysBetweenBilledAndPaid(billedDate,paidDate);
          Report.SetParameterValue("TotalEntDays",totalEntDays);
          avgDays = (totalEntDays / (double) Report.GetTotalValue("TotalTickets"));
          return avgDays;                        
        }
      }
    } 
        

  }
}
