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
using ClassLibraryMVP;
using Raj.EC;

public partial class Operations_Booking_NewGC_FrmNewGCOtherCharge : System.Web.UI.Page
{
    TextBox txt_Description,txt_Amount;
    CheckBox chk;
    decimal TotalOtherChargeAmt = 0;

    private string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public DataTable Bind_GCOtherChargeHead
    {
        set
        {
            dg_GCOtherChargeHead.DataSource = value;
            dg_GCOtherChargeHead.DataBind();

            lbl_TotalGCOtherCharges.Text = Session_GCOtherChargeHead.Compute("sum(Amount)", "").ToString();
            hdn_TotalGCOtherCharges.Value = Session_GCOtherChargeHead.Compute("sum(Amount)", "").ToString();
        }
    }
    public DataTable Session_GCOtherChargeHead
    {
        get { return StateManager.GetState<DataTable>("GCOtherChargeHead"); }
        set { StateManager.SaveState("GCOtherChargeHead", value); }
    }
    #region Function

    public bool validateUI()
    {
        bool isValid = true;
        errorMessage = "";

        int i = 0;

        if (dg_GCOtherChargeHead.Items.Count > 0 && StateManager.IsValidSession("GCOtherChargeHead"))
        {
            for (i = 0; i <= dg_GCOtherChargeHead.Items.Count - 1; i++)
            {
                txt_Description = (TextBox)dg_GCOtherChargeHead.Items[i].FindControl("txt_Description");
                txt_Amount = (TextBox)dg_GCOtherChargeHead.Items[i].FindControl("txt_Amount");
                chk = (CheckBox)dg_GCOtherChargeHead.Items[i].FindControl("chk_Checked");

                if (chk.Checked == true)
                {
                    if (Util.String2Decimal(txt_Amount.Text) > 0 && txt_Description.Text.Trim() == String.Empty)
                    {
                        errorMessage = "Please Enter Description for All Charges.";
                        scm_OtherCharges.SetFocus(txt_Description);
                        isValid = false;
                        break;
                    }
                }
            }
        }
        return isValid;
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        hdn_Mode.Value = Request.QueryString["Mode"].ToString();

        if (!IsPostBack)
        {
            if (StateManager.IsValidSession("GCOtherChargeHead"))
            {
                Bind_GCOtherChargeHead = Session_GCOtherChargeHead;
            }
        }
        SetStandardCaption();
    }
    protected void dg_GCOtherChargeHead_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            chk = (CheckBox)(e.Item.FindControl("chk_Checked"));
            txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));

            chk.Attributes.Add("OnClick", "Check_Single('" + dg_GCOtherChargeHead.ClientID + "')");
            txt_Amount.Attributes.Add("onchange", "Check_Single('" + dg_GCOtherChargeHead.ClientID + "')");
        }
    }

    private void Get_Other_Charges_Details()
    {
        int i = 0;
        if (dg_GCOtherChargeHead.Items.Count > 0 && StateManager.IsValidSession("GCOtherChargeHead"))
        {
            for (i = 0; i <= dg_GCOtherChargeHead.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_GCOtherChargeHead.Items[i].FindControl("chk_Checked");
                txt_Description = (TextBox)dg_GCOtherChargeHead.Items[i].FindControl("txt_Description");
                txt_Amount = (TextBox)dg_GCOtherChargeHead.Items[i].FindControl("txt_Amount");

                Session_GCOtherChargeHead.Rows[i]["checked"] = chk.Checked;
                Session_GCOtherChargeHead.Rows[i]["Description"] = chk.Checked == true ? txt_Description.Text : string.Empty;
                Session_GCOtherChargeHead.Rows[i]["Amount"] = chk.Checked == true ? Util.String2Decimal(txt_Amount.Text) : 0;
            }
        }
    }
    private void SetStandardCaption()
    {
        lbl_GCOtherCharges.Text = CompanyManager.getCompanyParam().GcCaption + " Other Charges";
        pnl_GCOtherChargeHead.GroupingText = CompanyManager.getCompanyParam().GcCaption + " Other Charges";
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Get_Other_Charges_Details();
            TotalOtherChargeAmt = Util.String2Decimal(Session_GCOtherChargeHead.Compute("sum(Amount)", "").ToString());
            string _Msg = "Saved SuccessFully";

            String popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();UpdateOtherCharges(" + TotalOtherChargeAmt + ");</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel2, typeof(String), "PopupScript1", popupScript.ToString(), false);
        }
    }

    protected void btn_Exit_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}
