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



public partial class Finance_Accounting_Vouchers_FrmOnAccountAdjustmenNew : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    string TotalUnAdjusted, TotalToBeAdjusted;

    #endregion

    #region EventClick



    public DataTable SessionBindGridOnAccount
    {
        get { return StateManager.GetState<DataTable>("OnAccountDetails"); }
        set { StateManager.SaveState("OnAccountDetails", value); }
    }

    public DataTable SessionBindGridPendingReferences
    {
        get { return StateManager.GetState<DataTable>("PendingReferencesDetails"); }
        set { StateManager.SaveState("PendingReferencesDetails", value); }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {

            //Common objcommon = new Common();
            //BindGrid("form_and_pageload", e);

            //tr_References.Attributes.Add("style", "display:none");
            //tr_ReferencesSave.Attributes.Add("style", "display:none");
            //tr_ReferencesTotal.Attributes.Add("style", "display:none");

            txt_Client.Focus();
        }

        lst_Client.Style.Add("visibility", "hidden");

    }


    protected void btn_hidden_Click(object sender, EventArgs e)
    {
        BindGrid("form_and_pageload", e);
    }


    protected void dg_OnAccountAdjustment_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {

        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            TextBox txtReceivedAmount = (TextBox)(e.Item.FindControl("txtReceivedAmount"));
            TextBox txtTDS = (TextBox)(e.Item.FindControl("txtTDS"));
            TextBox txtFrtDeduction = (TextBox)(e.Item.FindControl("txtFrtDeduction"));
            TextBox txtClaimDeduction = (TextBox)(e.Item.FindControl("txtClaimDeduction"));
            TextBox txtPendingAmount = (TextBox)(e.Item.FindControl("txtPendingAmount"));

            if (Util.String2Int(SessionBindGridPendingReferences.Rows[e.Item.ItemIndex]["Att"].ToString()) == 0)
            {
                txtReceivedAmount.Enabled = false;
                txtTDS.Enabled = false;
                txtFrtDeduction.Enabled = false;
                txtClaimDeduction.Enabled = false;

            }
            else
            {
                txtReceivedAmount.Enabled = true;
                txtTDS.Enabled = true;
                txtFrtDeduction.Enabled = true;
                txtClaimDeduction.Enabled = true;
            }



            HiddenField hdn_ReceivedAmount = (HiddenField)(e.Item.FindControl("hdn_ReceivedAmount"));
            HiddenField hdn_TDS = (HiddenField)(e.Item.FindControl("hdn_TDS"));
            HiddenField hdn_FrtDeduction = (HiddenField)(e.Item.FindControl("hdn_FrtDeduction"));
            HiddenField hdn_ClaimDeduction = (HiddenField)(e.Item.FindControl("hdn_ClaimDeduction"));
            HiddenField hdn_TDPercent = (HiddenField)(e.Item.FindControl("hdn_TDPercent"));



            txtReceivedAmount.Attributes.Add("onblur", "txtbox_onlostfocus(this); Onblur_ReceivedAmount('" + txtReceivedAmount.ClientID + "','" + hdn_ReceivedAmount.ClientID
                + "','" + txtTDS.ClientID + "','" + hdn_TDS.ClientID + "','" + txtFrtDeduction.ClientID + "','" + hdn_FrtDeduction.ClientID
                + "','" + txtClaimDeduction.ClientID + "','" + hdn_ClaimDeduction.ClientID
                + "','" + txtPendingAmount.ClientID + "','" + hdn_TDPercent.ClientID + "','" + dg_OnAccountAdjustment.ClientID + "');");

            txtTDS.Attributes.Add("onblur", "txtbox_onlostfocus(this); Onblur_TDS('" + txtReceivedAmount.ClientID + "','" + hdn_ReceivedAmount.ClientID
                + "','" + txtTDS.ClientID + "','" + hdn_TDS.ClientID + "','" + txtFrtDeduction.ClientID + "','" + hdn_FrtDeduction.ClientID
                + "','" + txtClaimDeduction.ClientID + "','" + hdn_ClaimDeduction.ClientID
                + "','" + txtPendingAmount.ClientID + "','" + hdn_TDPercent.ClientID + "','" + dg_OnAccountAdjustment.ClientID + "');");

            txtFrtDeduction.Attributes.Add("txtbox_onlostfocus(this); onblur", "Onblur_FrtDeduction('" + txtReceivedAmount.ClientID + "','" + hdn_ReceivedAmount.ClientID
                + "','" + txtTDS.ClientID + "','" + hdn_TDS.ClientID + "','" + txtFrtDeduction.ClientID + "','" + hdn_FrtDeduction.ClientID
                + "','" + txtClaimDeduction.ClientID + "','" + hdn_ClaimDeduction.ClientID
                + "','" + txtPendingAmount.ClientID + "','" + hdn_TDPercent.ClientID + "','" + dg_OnAccountAdjustment.ClientID + "');");

            txtClaimDeduction.Attributes.Add("txtbox_onlostfocus(this); onblur", "Onblur_ClaimDeduction('" + txtReceivedAmount.ClientID + "','" + hdn_ReceivedAmount.ClientID
                + "','" + txtTDS.ClientID + "','" + hdn_TDS.ClientID + "','" + txtFrtDeduction.ClientID + "','" + hdn_FrtDeduction.ClientID
                + "','" + txtClaimDeduction.ClientID + "','" + hdn_ClaimDeduction.ClientID
                + "','" + txtPendingAmount.ClientID + "','" + hdn_TDPercent.ClientID + "','" + dg_OnAccountAdjustment.ClientID + "');");

        }
    }


    protected void dg_Adjusted_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {

        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            TextBox txtAdjustedAmount = (TextBox)(e.Item.FindControl("txtAdjustedAmount"));


            if (Util.String2Int(SessionBindGridOnAccount.Rows[e.Item.ItemIndex]["Att"].ToString()) == 0)
            {
                txtAdjustedAmount.Enabled = false;
            }
            else
            {
                txtAdjustedAmount.Enabled = true;
            }

        }
    }



    #endregion



    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);



        SqlParameter[] objSqlParam ={objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0,Util.String2Int(hdn_ClientId.Value)),
        objDAL.MakeInParams("@Main_ID", SqlDbType.Int, 0,UserManager.getUserParam().MainId),
        objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar ,5,UserManager.getUserParam().HierarchyCode)};

        objDAL.RunProc("FA_Opr_CreditDebit_Customer_Balance_Bill_Details_New_From_Voucher", objSqlParam, ref ds);

        calculate_totals();


        SessionBindGridPendingReferences = ds.Tables[1];
        SessionBindGridOnAccount = ds.Tables[0];



        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Adjusted, ds.Tables[0], CallFrom, lbl_Error);

        objcommon.ValidateReportForm(dg_OnAccountAdjustment, ds.Tables[1], CallFrom, lbl_Error);



    }


    private void calculate_totals()
    {
        DataRow dr = ds.Tables[2].Rows[0];
        TotalUnAdjusted = dr["TotalUnAdjusted"].ToString();
        TotalToBeAdjusted = dr["TotalToBeAdjusted"].ToString();

        lbl_TotalBillAmunt.Text = TotalToBeAdjusted;
        lbl_TotalPendingAmount.Text = TotalUnAdjusted;
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



    public bool validateUIAdjustment()
    {
        bool ATS;
        ATS = false;

        if (Util.String2Int (hdn_ClientId.Value) <= 0)
        {
            lbl_Error.Text = "Select Client.";
            txt_Client.Focus();
        }

        else if (Util.String2Decimal(hdn_TotalToBeAdjusted.Value) <= 0)
        {
            lbl_Error.Text = "Please Select Any One Reference From OnAccount";
        }
        else if (Util.String2Decimal(hdn_TotalRecTDSFrtDeduction.Value) <= 0)
        {
            lbl_Error.Text = "Please Select Any One Reference From Pending Refrences";
        }
        else if (Util.String2Decimal(hdn_TotalReceivedAmount.Value) != Util.String2Decimal(hdn_TotalToBeAdjusted.Value))
        {
            lbl_Error.Text = "Amount in OnAccount References And Adjusted References Dose Not Match";
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }



    private void SetSessionDetailsFromGrid()
    {

        int i;

        CheckBox chk_Adjusted;
        TextBox txtAdjustedAmount;

        if (dg_Adjusted.Items.Count > 0)
        {
            for (i = 0; i <= dg_Adjusted.Items.Count - 1; i++)
            {

                chk_Adjusted = (CheckBox)dg_Adjusted.Items[i].FindControl("chk_Adjusted");
                txtAdjustedAmount = (TextBox)dg_Adjusted.Items[i].FindControl("txtAdjustedAmount");

                SessionBindGridOnAccount.Rows[i]["Att"] = chk_Adjusted.Checked;
                SessionBindGridOnAccount.Rows[i]["AdjustedAmount"] = txtAdjustedAmount.Text == "" ? "0" : txtAdjustedAmount.Text;

            }
        }

        CheckBox chk_Unadjusted;
        TextBox txtReceivedAmount, txtTDS, txtFrtDeduction, txtClaimDeduction;

        if (dg_OnAccountAdjustment.Items.Count > 0)
        {
            for (i = 0; i <= dg_OnAccountAdjustment.Items.Count - 1; i++)
            {

                chk_Unadjusted = (CheckBox)dg_OnAccountAdjustment.Items[i].FindControl("chk_Unadjusted");
                txtReceivedAmount = (TextBox)dg_OnAccountAdjustment.Items[i].FindControl("txtReceivedAmount");
                txtTDS = (TextBox)dg_OnAccountAdjustment.Items[i].FindControl("txtTDS");
                txtFrtDeduction = (TextBox)dg_OnAccountAdjustment.Items[i].FindControl("txtFrtDeduction");
                txtClaimDeduction = (TextBox)dg_OnAccountAdjustment.Items[i].FindControl("txtClaimDeduction");


                SessionBindGridPendingReferences.Rows[i]["Att"] = chk_Unadjusted.Checked;
                SessionBindGridPendingReferences.Rows[i]["ReceivedAmount"] = txtReceivedAmount.Text == "" ? "0" : txtReceivedAmount.Text;
                SessionBindGridPendingReferences.Rows[i]["TDSAmount"] = txtTDS.Text == "" ? "0" : txtTDS.Text;
                SessionBindGridPendingReferences.Rows[i]["FrtDeduction"] = txtFrtDeduction.Text == "" ? "0" : txtFrtDeduction.Text;
                SessionBindGridPendingReferences.Rows[i]["ClaimDeduction"] = txtClaimDeduction.Text == "" ? "0" : txtClaimDeduction.Text;
            }
        }


    }


    public String DetailsXMLOnAccount
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBindGridOnAccount.Copy());
            _objDs.Tables[0].TableName = "SessionBindGridOnAccount";
            return _objDs.GetXml().ToLower();
        }
    }

    public String DetailsXMLPendingReferences
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBindGridPendingReferences.Copy());
            _objDs.Tables[0].TableName = "SessionBindGridPendingReferences";
            return _objDs.GetXml().ToLower();
        }
    }

    protected void btn_SaveAdjustment_Click(object sender, EventArgs e)
    {
        SetSessionDetailsFromGrid();

        if (validateUIAdjustment())
        {
            SaveAdjustment();
        }
    }

    private Message SaveAdjustment()
    {

        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@Year_Code",SqlDbType.Int ,0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar ,5,UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Main_Id",SqlDbType.Int  ,0,UserManager.getUserParam().MainId ),
            objDAL.MakeInParams("@Client_ID",SqlDbType.Int ,0,Util.String2Int(hdn_ClientId.Value)), 
            objDAL.MakeInParams("@DetailsXMLOnAccount",SqlDbType.Xml,99999,DetailsXMLOnAccount),
            objDAL.MakeInParams("@DetailsXMLPendingReferences",SqlDbType.Xml,99999,DetailsXMLPendingReferences),
            objDAL.MakeInParams("@UserId",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.FA_Opr_CreditDebit_Customer_Adjustment_NewVoucher_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {

            String popupScript = "";
            string _Msg = "Saved SuccessFully";

            string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(153).LinkUrl;
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&DecryptUrl='No'");

            //System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(String), "PopupScript1", popupScript.ToString(), false);

        }
        else
        {
            lbl_Error.Text = objMessage.message;
        }

        return objMessage;
    }

}
