using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using ClassLibraryMVP.General;



public partial class Reports_GSTR1_Frm_Transfer_B2B_To_Exempt : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds, dsMonth;

    #endregion

    #region EventClick



    public DataTable SessionBindGridDT
    {
        get { return StateManager.GetState<DataTable>("SessionBindGridDS"); }
        set { StateManager.SaveState("SessionBindGridDS", value); }
    }

    private int TotalLR
    {
        get
        {
            return Util.String2Int(hdn_TotalLR.Value);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {

            hdn_YearCode.Value = Util.Int2String(UserManager.getUserParam().YearCode);

            Fill_Month();
            BindGrid("form_and_pageload", e);
            txt_GCNo.Focus();
        }
        
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);


        objDAL.RunProc("EC_Opr_GSTR1_Transfer_B2B_To_Exempt_BlankGrid",  ref ds);

        SessionBindGridDT = ds.Tables[0];

        hdn_TotalLR.Value = ds.Tables[0].Rows.Count.ToString();

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);


    }

    private void Fill_Month()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@MonthID", SqlDbType.Int,0,0) 
        };

        objDAL.RunProc("EC_Master_Get_Finacial_Month", objSqlParam, ref dsMonth);

        ddl_Month.DataSource = dsMonth;
        ddl_Month.DataTextField = "MonthName";
        ddl_Month.DataValueField = "MonthID";
        ddl_Month.DataBind();

        ddl_Month.SelectedValue = DateTime.Now.Month.ToString();
    }



    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {

        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {


        }
    }




    #endregion




    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    protected void btn_AddLR_Click(object sender, EventArgs e)
    {

        if (Util.String2Int(hdn_GC_ID.Value) > 0)
        {
            Bind_dg_GC_Details = makeDT();

            txt_GCNo.Text = "";
            btn_AddLR.Enabled = false;
        }
    }


    public bool validateUI()
    {
        bool ATS;
        ATS = false;

        if (TotalLR <= 0)
        {
            lbl_Error.Text = "Please Select Atleast One LR";
            txt_GCNo.Focus();
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }







    public String DetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBindGridDT.Copy());
            _objDs.Tables[0].TableName = "SessionBindGridDS";
            return _objDs.GetXml().ToLower();
        }
    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {

        if (validateUI())
        {
            Save();
        }
    }

    private Message Save()
    {

        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 8000), 
            objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,40000,DetailsXML),
            objDAL.MakeInParams("@UserId",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.EC_Opr_GSTR1_Transfer_B2B_To_Exempt_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {


            String popupScript = "";
            string _Msg = "Saved SuccessFully";

            string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(5263).LinkUrl;
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&DecryptUrl='No'");

            //System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(String), "PopupScript1", popupScript.ToString(), false);

        }
        else
        {
            lbl_Error.Text = objMessage.message;
        }

        return objMessage;
    }

    private DataTable makeDT()
    {
        DataTable objDT;

        objDT = SessionBindGridDT;

            DataRow objDR = objDT.NewRow();
            objDR["GC_ID"] = Util.String2Int(hdn_GC_ID.Value);
            objDR["GC_No"] = Util.String2Int(txt_GCNo.Text);
            objDR["GC_Date"] = hdn_GC_Date.Value;
            objDR["Consignor"] = hdn_Consignor.Value;
            objDR["Consignee"] = hdn_Consignee.Value;

            objDT.Rows.Add(objDR);

        return objDT;
    }

    public DataTable Bind_dg_GC_Details
    {
        set
        {
            SessionBindGridDT  = value;


            string CallFrom = "form_and_pageload";


            Common objcommon = new Common();

            objcommon.ValidateReportForm(dg_Grid, SessionBindGridDT, CallFrom, lbl_Error);

            hdn_TotalLR.Value = SessionBindGridDT.Rows.Count.ToString();

            lbl_TotalLR.Text = "Total LRs : " + hdn_TotalLR.Value; 

            hdn_GC_ID.Value = "0";
            hdn_Consignor.Value = "";
            hdn_Consignee.Value = "";

            btn_AddLR.Enabled = false;
            txt_GCNo.Focus();


        }
    }


}
