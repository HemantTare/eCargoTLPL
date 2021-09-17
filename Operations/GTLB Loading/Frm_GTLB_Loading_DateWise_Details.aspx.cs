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
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Operations_GTLB_Loading_Frm_GTLB_Loading_DateWise_Details : System.Web.UI.Page
{

    #region Declaration

    private DataSet ds;
    private DAL objDAL = new DAL();

    decimal TotalLoading, TotalThappi;

    TextBox txt;
    #endregion

    public bool Is_Complete
    {
        set { chk_Is_Complete.Checked = value; }
        get { return chk_Is_Complete.Checked; }
    }

    public DataTable SessionBindGridDetails
    {
        get { return StateManager.GetState<DataTable>("GetDetails"); }
        set { StateManager.SaveState("GetDetails", value); }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        lbl_Date.Text = Request.QueryString["Date"].ToString();

        if (!IsPostBack)
        {

            GetDetails("form_and_pageload", e);
        }

    }


    public void GetDetails(object sender, EventArgs e)
    {

        string CallFrom = (string)(sender);


        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Date", SqlDbType.DateTime, 0, Convert.ToDateTime(lbl_Date.Text)) };

        objDAL.RunProc("dbo.EC_Opr_GTLB_LoadingDetails_DateWise_Details", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            SessionBindGridDetails = ds.Tables[0];

            Is_Complete = Util.String2Bool(ds.Tables[0].Rows[0]["IsComplete"].ToString());

            Common objcommon = new Common();

            objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lblErrors);

            if (Is_Complete == true)
            {
                btnSave.Enabled  = false;
                //dg_Details.Enabled = false;
                chk_Is_Complete.Enabled = false;
            }

        }
        else
        {
            chk_Is_Complete.Visible = false;
            btnSave.Visible = false;
            lblErrors.Text = "Summary Posting Is Not Done Yet. Please Save The Summary First.";
        }

        CalculateTotal();
    }

    protected void dg_Details_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            TextBox txtTotalWt = (TextBox)(e.Item.FindControl("txtTotalWt"));
            TextBox txtOutsidePickupWt = (TextBox)(e.Item.FindControl("txtOutsidePickupWt"));

            TextBox txtLoading = (TextBox)(e.Item.FindControl("txtLoading"));
            TextBox txtThappiWt = (TextBox)(e.Item.FindControl("txtThappiWt"));



            if (Util.String2Bool(SessionBindGridDetails.Rows[e.Item.ItemIndex]["Att"].ToString()) == false)
            {
                txtTotalWt.Enabled = false;
                txtOutsidePickupWt.Enabled = false;
                txtLoading.Enabled = false;
                txtThappiWt.Enabled = false; 

            }
            else
            {
                txtTotalWt.Enabled = true;
                txtOutsidePickupWt.Enabled = true;
                txtLoading.Enabled = true;
                txtThappiWt.Enabled = true;
            }


            if (Util.String2Bool(SessionBindGridDetails.Rows[e.Item.ItemIndex]["OnlyLoading"].ToString()) == true)
            {
                txtTotalWt.Enabled = false;
                txtOutsidePickupWt.Enabled = false;

                txtLoading.ReadOnly = false;

                txtTotalWt.Text = "0";
                txtOutsidePickupWt.Text = "0";

            }
            else
            {
                txtTotalWt.Enabled = true;
                txtOutsidePickupWt.Enabled = true;

                txtLoading.ReadOnly  = true;
            }


            HiddenField hdnLoading = (HiddenField)(e.Item.FindControl("hdnLoading"));

            txtLoading.Attributes.Add("onblur", "Onblur_Loading('" + txtLoading.ClientID + "','" + hdnLoading.ClientID + "'); jsCalculateTotal('dg_Details');");
            //txtLoading.Attributes.Add("onblur", "return jsCalculateTotal('dg_Details')");



            LinkButton lbtn_Vehicle;

            int Vehicle_Id;

            Vehicle_Id  = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Vehicle_ID").ToString());

            lbtn_Vehicle = (LinkButton)e.Item.FindControl("lbtn_Vehicle");

            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Operations/GTLB Loading/FrmMemoList.aspx?Vehicle_Id=" + Vehicle_Id + "&LoadingDate=" + lbl_Date.Text + "&VehicleNo=" + lbtn_Vehicle.Text + "&ToBeIncludeDate=" + DataBinder.Eval(e.Item.DataItem, "ToBeIncludeDate").ToString());
            lbtn_Vehicle.Attributes.Add("onclick", "return OpenWindowMemoList('" + PathF4 + "')");

        }

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        SetSessionDetailsFromGrid();

        if (AllowToSave())
        {
            Save();
        }
    }


    private void SetSessionDetailsFromGrid()
    {

        int i;

        CheckBox Chk_Attach, Chk_OnlyLoading;
        TextBox txtTotalWt, txtEmptyVehWt, txtLoadWt, txtOutsidePickupWt, txtLoading, txtThappiWt, txtThappiWeightMaster;
        HiddenField hdnLoading;

        if (dg_Details.Items.Count > 0)
        {
            for (i = 0; i <= dg_Details.Items.Count - 1; i++)
            {

                Chk_Attach = (CheckBox)dg_Details.Items[i].FindControl("Chk_Attach");
                Chk_OnlyLoading = (CheckBox)dg_Details.Items[i].FindControl("Chk_OnlyLoading");

                txtTotalWt = (TextBox)dg_Details.Items[i].FindControl("txtTotalWt");
                txtEmptyVehWt = (TextBox)dg_Details.Items[i].FindControl("txtEmptyVehWt");
                txtLoadWt = (TextBox)dg_Details.Items[i].FindControl("txtLoadWt");
                txtOutsidePickupWt = (TextBox)dg_Details.Items[i].FindControl("txtOutsidePickupWt");
                txtLoading = (TextBox)dg_Details.Items[i].FindControl("txtLoading");

                hdnLoading = (HiddenField)dg_Details.Items[i].FindControl("hdnLoading");
                
                
                txtThappiWt = (TextBox)dg_Details.Items[i].FindControl("txtThappiWt");
                txtThappiWeightMaster = (TextBox)dg_Details.Items[i].FindControl("txtThappiWeightMaster");

                txtTotalWt.Text = (txtTotalWt.Text == string.Empty ? "0" : txtTotalWt.Text);
                txtOutsidePickupWt.Text = (txtOutsidePickupWt.Text == string.Empty ? "0" : txtOutsidePickupWt.Text);

                txtLoading.Text = hdnLoading.Value;

                SessionBindGridDetails.Rows[i]["Att"] = Chk_Attach.Checked;
                SessionBindGridDetails.Rows[i]["OnlyLoading"] = Chk_OnlyLoading.Checked;


                SessionBindGridDetails.Rows[i]["TotalWt"] = txtTotalWt.Text;
                SessionBindGridDetails.Rows[i]["EmptyVehWt"] = txtEmptyVehWt.Text;

               
                SessionBindGridDetails.Rows[i]["OutSidePickupWt"] = txtOutsidePickupWt.Text;


                if (Chk_Attach.Checked == true)
                {
                    if (Chk_OnlyLoading.Checked == false)
                    {
                        if (Util.String2Int(txtTotalWt.Text) > 0)
                        {
                            SessionBindGridDetails.Rows[i]["LoadWt"] = Util.String2Int(txtTotalWt.Text) - Util.String2Int(txtEmptyVehWt.Text);
                            txtLoadWt.Text = SessionBindGridDetails.Rows[i]["LoadWt"].ToString();


                            if (Util.String2Int(txtOutsidePickupWt.Text) >= 3000)
                            {
                                SessionBindGridDetails.Rows[i]["Loading"] = Util.String2Int(SessionBindGridDetails.Rows[i]["LoadWt"].ToString()) - Util.String2Int(txtOutsidePickupWt.Text);
                            }
                            else
                            {
                                SessionBindGridDetails.Rows[i]["Loading"] = SessionBindGridDetails.Rows[i]["LoadWt"];
                            }

                            txtLoading.Text = SessionBindGridDetails.Rows[i]["Loading"].ToString();

                        }
                        else
                        {
                            SessionBindGridDetails.Rows[i]["LoadWt"] = "0";
                            txtLoadWt.Text = "0";

                            SessionBindGridDetails.Rows[i]["Loading"] = "0";
                            txtLoading.Text = "0";
                        }

                        if (Util.String2Int(txtOutsidePickupWt.Text) == 0 || Util.String2Int(txtOutsidePickupWt.Text) >= 3000)
                        {
                            SessionBindGridDetails.Rows[i]["ThappiWt"] = txtThappiWeightMaster.Text;
                        }
                        else
                        {
                            SessionBindGridDetails.Rows[i]["ThappiWt"] = Util.String2Int(txtThappiWeightMaster.Text) - Util.String2Int(txtOutsidePickupWt.Text);
                        }

                        txtThappiWt.Text = SessionBindGridDetails.Rows[i]["ThappiWt"].ToString();

                    }

                    else
                    {

                        if (Util.String2Int(txtOutsidePickupWt.Text) == 0 || Util.String2Int(txtOutsidePickupWt.Text) >= 3000)
                        {
                            SessionBindGridDetails.Rows[i]["ThappiWt"] = txtThappiWeightMaster.Text;
                        }
                        else
                        {
                            SessionBindGridDetails.Rows[i]["ThappiWt"] = Util.String2Int(txtThappiWeightMaster.Text) - Util.String2Int(txtOutsidePickupWt.Text);
                        }

                        txtThappiWt.Text = SessionBindGridDetails.Rows[i]["ThappiWt"].ToString();

                        SessionBindGridDetails.Rows[i]["LoadWt"] = txtLoadWt.Text;
                        SessionBindGridDetails.Rows[i]["Loading"] = txtLoading.Text;
                        SessionBindGridDetails.Rows[i]["ThappiWt"] = txtThappiWt.Text;                    
                    }
                }
                else
                {
                    SessionBindGridDetails.Rows[i]["LoadWt"] = "0";
                    SessionBindGridDetails.Rows[i]["Loading"] = "0";
                    SessionBindGridDetails.Rows[i]["ThappiWt"] = "0";

                    txtLoadWt.Text = "0";
                    txtLoading.Text = "0";
                    txtThappiWt.Text = "0";

                }
            }
        }
    }

    public String DetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBindGridDetails.Copy());
            _objDs.Tables[0].TableName = "SessionBindGridDetails";
            return _objDs.GetXml().ToLower();
        }
    }

    private bool grid_validation()
    {
        int i;
        bool ATS = true;

        TextBox txtTotalWt, txtOutsidePickupWt, txtThappiWt, txtEmptyVehWt;
        CheckBox Chk_Attach;

        if (dg_Details.Items.Count > 0)
        {
            for (i = 0; i <= dg_Details.Items.Count - 1; i++)
            {
                txtTotalWt = (TextBox)dg_Details.Items[i].FindControl("txtTotalWt");
                txtOutsidePickupWt = (TextBox)dg_Details.Items[i].FindControl("txtOutsidePickupWt");
                Chk_Attach = (CheckBox)dg_Details.Items[i].FindControl("Chk_Attach");
                txtThappiWt = (TextBox)dg_Details.Items[i].FindControl("txtThappiWt");
                txtEmptyVehWt = (TextBox)dg_Details.Items[i].FindControl("txtEmptyVehWt");

                if (Chk_Attach.Checked == true && Util.String2Decimal(txtEmptyVehWt.Text) == 0)
                {
                    lblErrors.Text = "Empty Vehicle Wt. Can No Be Zero";
                    ScriptManager.SetFocus(txtEmptyVehWt);
                    ATS = false;
                    break;
                }
                else if (Chk_Attach.Checked == true && Util.String2Decimal(txtThappiWt.Text) == 0)
                {
                    lblErrors.Text = "Please Enter Thappi Wt.";
                    ScriptManager.SetFocus(txtThappiWt);
                    ATS = false;
                    break;
                }
                else
                {
                    ATS = true;
                }
            }
        }
        return ATS;
    }

    private bool AllowToSave()
    {
        bool ATS = false;

        if (grid_validation() == false)
        {
            ATS = false;
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }

    private Message Save()
    {

        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@Date",SqlDbType.DateTime,0,Convert.ToDateTime(lbl_Date.Text)), 
            objDAL.MakeInParams("@IsComplete",SqlDbType.Bit,0,Is_Complete), 
            objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,20000,DetailsXML), 
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.EC_Opr_GTLB_LoadingDetails_DateWise_Detail_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {


            String popupScript = "";
            string _Msg = "Saved SuccessFully";

            string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(341).LinkUrl;
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&DecryptUrl='No'");

            //System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(String), "PopupScript1", popupScript.ToString(), false);

        }
        else
        {
            lblErrors.Text = objMessage.message;
        }

        return objMessage;
    }

    protected void txtTotalWt_TextChanged(object sender, EventArgs e)
    {


        TextBox txtTotalWt, txtOutsidePickupWt, txtEmptyVehWt, txtLoadWt, txtLoading, txtThappiWt, txtThappiWeightMaster;

        txt = (TextBox)sender;
        DataGridItem _item = (DataGridItem)txt.Parent.Parent;

        txtTotalWt = (TextBox)_item.FindControl("txtTotalWt");
        txtOutsidePickupWt = (TextBox)_item.FindControl("txtOutsidePickupWt");
        txtEmptyVehWt = (TextBox)_item.FindControl("txtEmptyVehWt");
        txtLoadWt = (TextBox)_item.FindControl("txtLoadWt");
        txtLoading = (TextBox)_item.FindControl("txtLoading");
        txtThappiWt = (TextBox)_item.FindControl("txtThappiWt");
        txtThappiWeightMaster = (TextBox)_item.FindControl("txtThappiWeightMaster");



        int LoadWt, Loading, ThappiWt;

        LoadWt = 0;
        Loading = 0;
        ThappiWt = 0;

        LoadWt = Util.String2Int(txtTotalWt.Text) - Util.String2Int(txtEmptyVehWt.Text);
        txtLoadWt.Text = LoadWt.ToString();


        if (Util.String2Int(txtOutsidePickupWt.Text) >= 3000)
        {
            Loading = Util.String2Int(txtLoadWt.Text) - Util.String2Int(txtOutsidePickupWt.Text);
        }
        else
        {
            Loading = Util.String2Int(txtLoadWt.Text);
        }
        txtLoading.Text = Loading.ToString();

        if (Util.String2Int(txtOutsidePickupWt.Text) == 0 || Util.String2Int(txtOutsidePickupWt.Text) >= 3000)
        {
            ThappiWt = Util.String2Int(txtThappiWeightMaster.Text);
        }
        else
        {
            ThappiWt = Util.String2Int(txtThappiWeightMaster.Text) - Util.String2Int(txtOutsidePickupWt.Text);
        }

        txtThappiWt.Text = ThappiWt.ToString();

    }



    protected void btnFORM1_Click(object sender, EventArgs e)
    {

        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/");

        Path.Append("Operations/GTLB Loading/Frm_Rpt_GTLB_Loading_FORM_1_Viewer.aspx?LoadingDate=" + lbl_Date.Text);

        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("Open_FORM1_Window('" + Path + "')");
        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());

    }

    private void CalculateTotal()
    {

        TotalThappi  = 0;
        TotalLoading  = 0;
        int i;

        TextBox txtLoading, txtThappiWt;

        if (dg_Details.Items.Count > 0)
        {
            for (i = 0; i <= dg_Details.Items.Count - 1; i++)
            {
                txtLoading = (TextBox)dg_Details.Items[i].FindControl("txtLoading");
                txtThappiWt = (TextBox)dg_Details.Items[i].FindControl("txtThappiWt");

                if (txtLoading.Text == "")
                    txtLoading.Text = "0";

                if (txtThappiWt.Text == "")
                    txtThappiWt.Text = "0";

                TotalThappi = TotalThappi + Convert.ToDecimal(txtThappiWt.Text);
                TotalLoading = TotalLoading + Convert.ToDecimal(txtLoading.Text);
            }

            lbl_TotalThappi.Text = Convert.ToString(TotalThappi);
            lbl_TotalLoading.Text = Convert.ToString(TotalLoading);

        }
    }

}
