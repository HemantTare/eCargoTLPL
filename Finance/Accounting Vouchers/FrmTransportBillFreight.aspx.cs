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
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Finance_Accounting_Vouchers_FrmTransportBillFreight : ClassLibraryMVP.UI.Page
{
    Common objComm = new Common();
    int ItemIndex = 0;
    DataTable SessionDT = null;
    string Mode, attached;
    int QueryString;

    PageControls pc = new PageControls();


    #region ControlsValues
    private string GCNo
    {
        set { lbl_GCNo.Text = value; }
    }
    private decimal Freight
    {
        set { txt_Freight.Text = Util.Decimal2String(value); }
        get { return txt_Freight.Text.Trim() == "0" ? 0 : Util.String2Decimal(txt_Freight.Text); }
    }
    private decimal Local_Charge
    {
        set { txt_LocalCharge.Text = Util.Decimal2String(value); }
    }
    private decimal Loading_Charge
    {
        set { txt_LoadingCharges.Text = Util.Decimal2String(value); }
    }
    private decimal Stationary_Charge
    {
        set { txt_StationaryCharges.Text = Util.Decimal2String(value); }
    }
    private decimal FOVRisk_Charge
    {
        set { txt_FovRiskCharges.Text = Util.Decimal2String(value); }
    }
    //public decimal Surcharge
    //{
    //    set { txt_Surcharge.Text = Util.Decimal2String(Math.Round(value, 2)); }
    //}
    //public decimal DKCharge
    //{
    //    set { txt_DKCharge.Text = Util.Decimal2String(Math.Round(value, 2)); }
    //}
    //private decimal Topay_Charge
    //{
    //    set { txt_TopayCharges.Text = Util.Decimal2String(value); }
    //}
    private decimal DD_Charge
    {
        set { txt_DDCharges.Text = Util.Decimal2String(value); }
    }

    private decimal DACC_Charges
    {
        set { txt_COD.Text = Util.Decimal2String(value); }
    }

    //private decimal Length_Charge
    //{
    //    set { txt_LengthCharges.Text = Util.Decimal2String(value); }
    //}

    private decimal Other_Charge
    {
        set { lbtn_OtherCharges.Text = Util.Decimal2String(value); }
    }
    private decimal SubTotal
    {
        set 
        { 
            lbl_SubTotal.Text = Util.Decimal2String(value);
            hdn_SubTotal.Value = Util.Decimal2String(value);
        }
        get { return hdn_SubTotal.Value == "0" ? 0 : Util.String2Decimal(hdn_SubTotal.Value); }
    }
    public decimal ServiceTax
    {
        set
        {
            lbl_ServiceTaxValue.Text = Util.Decimal2String(value);
            hdn_ServiceTax.Value = Util.Decimal2String(value);
        }
        get { return hdn_ServiceTax.Value == string.Empty ? 0 : Util.String2Decimal(hdn_ServiceTax.Value); }
    }
    public decimal Round_Off
    {
        set
        {
            txt_Round_Off.Text = Util.Decimal2String(value);
            hdn_Round_Off.Value = Util.Decimal2String(value);
        }
        get { return hdn_Round_Off.Value == string.Empty ? 0 : Util.String2Decimal(hdn_Round_Off.Value); }
    }
    public decimal TotalGCAmount
    {
        set
        {
            lbl_TotalGCAmountValue.Text = Util.Decimal2String(value);
            hdn_TotalGCAmount.Value = Util.Decimal2String(value);
        }
        get { return hdn_TotalGCAmount.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TotalGCAmount.Value); }
    }
   
  #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ItemIndex = Util.DecryptToInt(Request.QueryString["ItemIndex"].ToString());
        Mode = Request.QueryString["Mode"].ToString();

        attached = Request.QueryString["Attched"];

        QueryString = Util.DecryptToInt(Mode);

        SessionDT = StateManager.GetState<DataTable>("BindBillGrid");

        if (QueryString == 4) //For View Mode
        {
            btn_Save.Visible = false;
            txt_Freight.Enabled = false;
            txt_LocalCharge.Enabled = false;
            txt_LoadingCharges.Enabled = false;
            txt_StationaryCharges.Enabled = false;
            txt_FovRiskCharges.Enabled = false;
            //txt_TopayCharges.Enabled = false;
            txt_DDCharges.Enabled = false;
            txt_IBACharges.Enabled = false;
            //txt_LengthCharges.Enabled = false;
            txt_DDCharges.Enabled = false;
            //txt_Surcharge.Enabled = false;
            txt_COD.Enabled = false;
            //txt_DKCharge.Enabled = false;
            txt_Round_Off.Enabled = false; 
        }
        if (!IsPostBack)
        {
            Set_All_Text();
            Set_Standard_Caption();
            pc.AddAttributes(this.Controls);
        }

        string scripts;
        scripts = "";
        scripts = "<script type='text/javascript' language='javascript'>" +
                    " Hide_Controls();" +
                    "</script>";

        ScriptManager.RegisterStartupScript(Page, typeof(string), "ss", scripts, false);


    }
    private void Set_Standard_Caption()
    {
        Lbl_GC_Text.Text = CompanyManager.getCompanyParam().GcCaption + " No :";
    } 
    private void Set_All_Text()
    {
        DataRow dr = SessionDT.Rows[ItemIndex];
        GCNo = dr["GC_No_For_Print"].ToString();
        Freight = Util.String2Decimal(dr["Freight_Amt"].ToString());
        //Surcharge = Util.String2Decimal(dr["Surcharge"].ToString());
        Local_Charge = Util.String2Decimal(dr["Local_Charges"].ToString());
        Loading_Charge = Util.String2Decimal(dr["Hamali_Charges"].ToString());
        //DKCharge = Util.String2Decimal(dr["DKCharge"].ToString());
        Stationary_Charge = Util.String2Decimal(dr["Bilti_Charges"].ToString());
        FOVRisk_Charge = Util.String2Decimal(dr["FOV"].ToString());
        //Topay_Charge = Util.String2Decimal(dr["TP_Charges"].ToString());
        DD_Charge = Util.String2Decimal(dr["DD_Charges"].ToString());
        Other_Charge = Util.String2Decimal(dr["GC_Other_Charges"].ToString());

        DACC_Charges = Util.String2Decimal(dr["DACC_Charges"].ToString());
        //Length_Charge = Util.String2Decimal(dr["Length_Charge"].ToString());

        SubTotal = Util.String2Decimal(dr["Actual_Sub_Total"].ToString());
        ServiceTax = Util.String2Decimal(dr["GCService_Tax_Amount"].ToString());
        Round_Off = Util.String2Decimal(dr["Round_Off"].ToString());
        TotalGCAmount = Util.String2Decimal(dr["Total_GC_Amount"].ToString());

        hdn_Is_IBA.Value = dr["Is_DACC"].ToString();

        if (Util.String2Int(dr["Delivery_Type_Id"].ToString()) == 1)
        {
            txt_DDCharges.Enabled = false;
        }

        //lbtn_OtherCharges.Attributes.Add("onclick", "return Show_GC_OtherCharge('" + ClassLibraryMVP.Util.EncryptInteger(ItemIndex) + "','" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(dr["GC_Id"].ToString())) + "&Mode=" + Mode + "');");

    }
    private void updateparentdataset()
    {
        string popupScript = "<script language='javascript'>updateparentdataset();</script>";

        System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(String), "PopupScript", popupScript.ToString(), false);
    }
    protected void btn_Exit_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (AllowToSave() == true)
        {
            update_Session();
            updateparentdataset();

            string popupScript1 = "<script language='javascript'>{self.close()}</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(String), "PopupScript1", popupScript1.ToString(), false);
        }

    }
    private void update_Session()
    {



        decimal Sub_Total = Util.String2Decimal(txt_Freight.Text) + Util.String2Decimal(txt_LocalCharge.Text) +
                            Util.String2Decimal(txt_LoadingCharges.Text) + Util.String2Decimal(txt_StationaryCharges.Text) +
                            Util.String2Decimal(txt_FovRiskCharges.Text) +
                             Util.String2Decimal(txt_DDCharges.Text) + Util.String2Decimal(lbtn_OtherCharges.Text) +
                             Util.String2Decimal(txt_COD.Text);// +Util.String2Decimal(txt_Surcharge.Text) + Util.String2Decimal(txt_DKCharge.Text);

        
        SessionDT.Rows[ItemIndex]["Att"] = Util.String2Bool(attached);
        SessionDT.Rows[ItemIndex]["Sub_Total"] = Sub_Total;
        SessionDT.Rows[ItemIndex]["Freight_Amt"] = Util.String2Decimal(txt_Freight.Text);
        SessionDT.Rows[ItemIndex]["Local_Charges"] = Util.String2Decimal(txt_LocalCharge.Text);
        SessionDT.Rows[ItemIndex]["Hamali_Charges"] = Util.String2Decimal(txt_LoadingCharges.Text);
        SessionDT.Rows[ItemIndex]["Bilti_Charges"] = Util.String2Decimal(txt_StationaryCharges.Text);
        SessionDT.Rows[ItemIndex]["FOV"] = Util.String2Decimal(txt_FovRiskCharges.Text);
        //SessionDT.Rows[ItemIndex]["TP_Charges"] = Util.String2Decimal(txt_TopayCharges.Text);
        SessionDT.Rows[ItemIndex]["DD_Charges"] = Util.String2Decimal(txt_DDCharges.Text);
        SessionDT.Rows[ItemIndex]["DACC_Charges"] = Util.String2Decimal(txt_COD.Text);
        //SessionDT.Rows[ItemIndex]["Surcharge"] = Util.String2Decimal(txt_Surcharge.Text);
        //SessionDT.Rows[ItemIndex]["DKCharge"] = Util.String2Decimal(txt_DKCharge.Text);
        SessionDT.Rows[ItemIndex]["ServiceTax"] = ServiceTax;
        SessionDT.Rows[ItemIndex]["Round_Off"] = Util.String2Decimal(txt_Round_Off.Text);
        SessionDT.Rows[ItemIndex]["TotalGCAmount"] = TotalGCAmount; 
 
        //SessionDT.Rows[ItemIndex]["Length_Charge"] = Util.String2Decimal(txt_LengthCharges.Text);
    }


    private bool AllowToSave()
    {
        bool ATS = false;
        if (Freight <= 0)
        {
            lbl_Errors.Text = "Freight must be greater than Zero";
            scm_freight.SetFocus(txt_Freight);
        }
        else if (txt_LocalCharge.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Local Charge";
            scm_freight.SetFocus(txt_LocalCharge);
        }
        else if (txt_LoadingCharges.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Loading Charges";
            scm_freight.SetFocus(txt_LoadingCharges);
        }
        else if (txt_StationaryCharges.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Stationary Charges";
            scm_freight.SetFocus(txt_StationaryCharges);
        }
        else if (txt_FovRiskCharges.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Fov/Risk Charges";
            scm_freight.SetFocus(txt_FovRiskCharges);
        }
        //else if (txt_TopayCharges.Text.Trim() == string.Empty)
        //{
        //    lbl_Errors.Text = "Please Enter Topay Charges";
        //    scm_freight.SetFocus(txt_TopayCharges);
        //}
        else if (txt_DDCharges.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter DD Charges";
            scm_freight.SetFocus(txt_DDCharges);
        }
        else if (txt_IBACharges.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter IBA Charges";
            scm_freight.SetFocus(txt_IBACharges );
        }
        //else if (txt_LengthCharges.Text.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_LengthCharges) == true)
        //{
        //    lbl_Errors.Text = "Please Enter Length Charges";
        //    scm_freight.SetFocus(txt_LengthCharges);
        //}
        else if (SubTotal <= 0)
        {
            lbl_Errors.Text = "Sub Total must be greater than Zero";
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }
    protected void btn_update_grid_Click(object sender, EventArgs e)
    {
        Set_All_Text();
    }
}
