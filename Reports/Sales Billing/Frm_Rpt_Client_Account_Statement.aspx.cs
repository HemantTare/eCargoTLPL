using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using System.Text;
using Raj.EC;
using System.Web.UI.WebControls;
using ClassLibraryMVP.General;


public partial class Reports_SalesBilling_Frm_Rpt_Client_Account_Statement : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    int _Menu_Item_Id;
    #endregion

    #region ControlsValues

    public DateTime From_Date
    {
        set
        {
            Wuc_From_To_Datepicker1.SelectedFromDate = value;
        }
        get
        {
            return Wuc_From_To_Datepicker1.SelectedFromDate;
        }
    }

    public DateTime To_Date
    {
        set
        {
            Wuc_From_To_Datepicker1.SelectedToDate = value;

        }
        get { return Wuc_From_To_Datepicker1.SelectedToDate; }
    }

    public int Menu_Item_Id
    {
        set { _Menu_Item_Id = value; }
        get { return _Menu_Item_Id; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        Menu_Item_Id = Raj.EC.Common.GetMenuItemId();
      
        if (IsPostBack == false)
        {
            ddlParty.DataTextField = "Client_Name";
            ddlParty.DataValueField = "Client_Id";
            ddlParty.OtherColumns = "2";
        }
    }
 
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();

            lbl_Error.Text = "";

            StringBuilder Path = new StringBuilder(Util.GetBaseURL());
            Path.Append("/");

            Path.Append("Reports/Sales Billing/Frm_Rpt_Client_Account_StatementViewer.aspx?BillingClientID=" + Util.EncryptInteger(Convert.ToInt32(ddlParty.SelectedValue)) + "&BillingClientName=" + Util.EncryptString(ddlParty.SelectedText)
             + "&Fromdate=" + From_Date + "&Todate=" + To_Date);

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("Open_Details_Window('");
            sb.Append(Path);
            sb.Append("');");
            sb.Append("</script>");

            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    } 

    
    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

}
