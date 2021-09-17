
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
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;
using ClassLibraryMVP;
using Raj.EC;
public partial class Operations_Booking_wucGCOtherCharges : System.Web.UI.UserControl 
{
    TextBox txt_Description;
    TextBox txt_Amount;
    CheckBox chk;

    #region IView Members

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]); 
        }
    }

    #endregion 
    #region Controls 
    
    public DataTable  Bind_GCOtherChargeHead 
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
  
    public String Session_DeliveryBranchName
    {
        get { return StateManager.GetState<String>("DeliveryBranchName"); }
        set { StateManager.SaveState("DeliveryBranchName", value); }
    }

    public Int32 Session_DeliveryBranchId
    {
        get { return StateManager.GetState<Int32>("DeliveryBranchId"); }
        set { StateManager.SaveState("DeliveryBranchId", value); }
    }
    
    #endregion
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
            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();

            sbValid.Append("if (Allow_To_Exit() == false) { return false; }");
            sbValid.Append(Page.ClientScript.GetPostBackEventReference(btn_Exit, ""));
            sbValid.Append(";");
            btn_Exit.Attributes.Add("onclick", sbValid.ToString());

            if (hdn_Mode.Value == "4")
            {
                Btn_Save.Visible = false;
            }
        }
        SetStandardCaption();
    }

    protected void dg_GCOtherChargeHead_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        {
            if (e.Item.ItemIndex != -1)
            {
                txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
                chk = (CheckBox )(e.Item.FindControl("chk_Checked"));
                chk.Attributes.Add("OnClick", "Calculate_Other_Charges_Total('" + dg_GCOtherChargeHead.ClientID + "')");
                txt_Amount.Attributes.Add("onchange", "Calculate_Other_Charges_Total('" + dg_GCOtherChargeHead.ClientID + "')");
            }
        }
    }

    private void Get_Other_Charges_Details()
    {
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
                    Session_GCOtherChargeHead.Rows[i]["checked"] = true;
                    Session_GCOtherChargeHead.Rows[i]["Description"] = txt_Description.Text;
                    Session_GCOtherChargeHead.Rows[i]["Amount"] = Util.String2Decimal(txt_Amount.Text);
                }
                else
                {
                    Session_GCOtherChargeHead.Rows[i]["checked"] = false ;
                    Session_GCOtherChargeHead.Rows[i]["Description"] = "";
                    Session_GCOtherChargeHead.Rows[i]["Amount"] = 0;
                }               
            }
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {            
            Get_Other_Charges_Details();

            string _Msg;
            _Msg = "Saved SuccessFully";
            
            String popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();UpdateOtherCharges();</script>";

            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel2, typeof(String), "PopupScript1", popupScript.ToString(), false);
        }
    }

    protected void btn_Exit_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'> { self.close() }</script>");
    }

    private void SetStandardCaption()
    {
        lbl_GCOtherCharges.Text  = CompanyManager.getCompanyParam().GcCaption + " Other Charges";     
        pnl_GCOtherChargeHead.GroupingText = CompanyManager.getCompanyParam().GcCaption + " Other Charges";
    }
}
