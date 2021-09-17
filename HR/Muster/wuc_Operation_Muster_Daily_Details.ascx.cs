using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Operation_Muster_wuc_Operation_Muster_Daily_Details : System.Web.UI.UserControl
{
    private static int Monthtemp = DateTime.Today.Month;
    private static int Yeartemp = DateTime.Today.Year;

    public string EmployeeID
    {
        get { return (Request.QueryString["Employee_ID"]); }
    }
    public string EmployeeName
    {
        get { return (Request.QueryString["EmployeeName"]); }
    }
    private string EmployeeCode
    {
        get { return (Request.QueryString["EmployeeCode"]); }
    }

    public string Day
    {
        get { return (Request.QueryString["Day"]); }
    }
    public int OtHrs
    {
        get { return Convert.ToInt32(Request.QueryString["OtHrs"]); }
    }
    public int ExtHrs
    {
        get { return Convert.ToInt32(Request.QueryString["ExtHrs"]); }
    }
    public int Dates
    {
        get { return Convert.ToInt32(Request.QueryString["Dates"]); }
    }
      
  
    protected void Page_Load(object sender, EventArgs e)
    {
       // DataSet ds = new DataSet();
        DataSet ds = (DataSet)Session["MusterEntryDaily"];
        DataView dv = ds.Tables[0].DefaultView;
        DataTable table = new DataTable();
        dv.RowFilter = "Employee_Id =" + EmployeeID;
        table = dv.ToTable();

      
        DateTime dt = Convert.ToDateTime(Dates + "/" + Monthtemp + "/" + Yeartemp);
        //lbl_Employee_Name.Text = EmployeeName.ToString();
        //lbl_Employee_Code.Text = EmployeeCode.ToString();
        string EmpName = table.Rows[0]["Employee_Name"].ToString();
        EmpName = EmpName.Replace("'", "");
        lbl_Employee_Name.Text = EmpName.ToString();

        lbl_Employee_Code.Text = table.Rows[0]["Emp_Code"].ToString();

        lbl_Day.Text = Day.ToString();
        //lbl_Ot_Hrs.Text = OtHrs.ToString();
        //lbl_Ext_Hrs.Text = ExtHrs.ToString();
        lbl_Ot_Hrs.Text = table.Rows[0]["ot_" + Dates.ToString()].ToString();
        lbl_Ext_Hrs.Text = table.Rows[0]["ExtHrs_" + Dates.ToString()].ToString();
        lbl_Date.Text = dt.ToString("dd MMM yyyy");
        dv.RowFilter = "";
    }
}
