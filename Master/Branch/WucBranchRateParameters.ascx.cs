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
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;
 
/// <summary>
/// Author        : Shiv kumar mishra
/// Modified      : Mandatory condition is modify by Ankit Champaneriya
/// Created On    : 10/10/2008
/// Description   : This Page is For Master Branch Rate Parameters Details
/// </summary>
/// 

public partial class Master_Branch_WucBranchRateParameters : System.Web.UI.UserControl, IBranchRateParametersView
{
    #region ClassVariables
    BranchRateParametersPresenter objBranchRateParametersPresenter;
    PageControls pc = new PageControls();
    private Boolean _Is_FOV_Calcualted_As_Per_Standard;
    #endregion

    #region ControlsValues
    public int ToBranchID
    {
        //set { ddl_ToBranch.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_ToBranch.SelectedValue); }
    }
    public int FromBranchID
    {
        //set { ddl_ToBranch.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_CopyFrom.SelectedValue); }
    }
    public string ToBranchName
    {
        set { lbl_Branch_Name.Text = value; }
    }
    public decimal MinChargeWt
    {
        set { txt_MinChrgeWt.Text = Util.Decimal2String(value); }
        get
        {
            if (txt_MinChrgeWt.Text == string.Empty)
                return 0;
            else
                return Util.String2Decimal(txt_MinChrgeWt.Text);
        }
    }
    public decimal MaxBiltyCharges
    {
        set { txt_maxbiltycharge.Text = Util.Decimal2String(value); }
        get
        {
            if (txt_maxbiltycharge.Text == string.Empty)
                return 0;
            else
                return Util.String2Decimal(txt_maxbiltycharge.Text);
        }
    }
    public decimal BiltyCharges
    {
        set { txt_BiltyCharges.Text = Util.Decimal2String(value); }
        get
        {
            if (txt_BiltyCharges.Text == string.Empty)
                return 0;
            else
                return Util.String2Decimal(txt_BiltyCharges.Text);
            //return txt_BiltyCharges.Text == string.Empty ? 0 : Util.String2Decimal(txt_BiltyCharges.Text);
        }
    }
    public decimal FOVPercent
    {
        set { txt_FOVPercent.Text = Util.Decimal2String(value); }
        get { return txt_FOVPercent.Text == string.Empty ? 0 : Util.String2Decimal(txt_FOVPercent.Text); }
    }
    public decimal MinFOV
    {
        set { txt_MinFOV.Text = Util.Decimal2String(value); }
        get { return txt_MinFOV.Text == string.Empty ? 0 : Util.String2Decimal(txt_MinFOV.Text); }
    }
    public decimal Hamali
    {
        set { txt_HamaliPercent.Text = Util.Decimal2String(value); }
        get { return txt_HamaliPercent.Text == string.Empty ? 0 : Util.String2Decimal(txt_HamaliPercent.Text); }
    }

    public decimal HamaliArticle
    {
        set { txt_HamaliArticle.Text = Util.Decimal2String(value); }
        get { return txt_HamaliArticle.Text == string.Empty ? 0 : Util.String2Decimal(txt_HamaliArticle.Text); }
    }
    public decimal MinHamali
    {
        set { txt_MinHamali.Text = Util.Decimal2String(value); }
        get { return txt_MinHamali.Text == string.Empty ? 0 : Util.String2Decimal(txt_MinHamali.Text); }
    }
    public decimal DoorDeliveryCharges
    {
        get { return txt_DoorDeliveryCharges.Text == string.Empty ? 0 : Util.String2Decimal(txt_DoorDeliveryCharges.Text); }
        set { txt_DoorDeliveryCharges.Text = Util.Decimal2String(value); }
    }

    public decimal ToPayCharges
    {
        set { txt_ToPayCharges.Text = Util.Decimal2String(value); }
        get { return txt_ToPayCharges.Text == string.Empty ? 0 : Util.String2Decimal(txt_ToPayCharges.Text); }
    }
    public decimal DACCCharges
    {
        set { txt_DACCCharges.Text = Util.Decimal2String(value); }
        get { return txt_DACCCharges.Text == string.Empty ? 0 : Util.String2Decimal(txt_DACCCharges.Text); }
    }
    public int CFTFactor
    {
        set { txt_CFTFactor.Text = Util.Decimal2String(value); }
        get { return txt_CFTFactor.Text == string.Empty ? 0 : Util.String2Int(txt_CFTFactor.Text); }
    }
    public decimal ServiceTax
    {
        set { lbl_SeviceTax.Text = Util.Decimal2String(value); }
        get { return lbl_SeviceTax.Text == string.Empty ? 0 : Util.String2Decimal(lbl_SeviceTax.Text); }
    }

    public int DemurrageDays
    {
        set { txt_DemurrageDays.Text = Util.Int2String(value); }
        get { return txt_DemurrageDays.Text == string.Empty ? 0 : Util.String2Int(txt_DemurrageDays.Text); }
    }

    public decimal DemurrageRate
    {
        set { txt_DemurrageRate.Text = Util.Decimal2String(value); }
        get { return txt_DemurrageRate.Text == string.Empty ? 0 : Util.String2Decimal(txt_DemurrageRate.Text); }
    }

    public decimal OctroiFormCharges
    {
        set { txt_OctroiFormCharges.Text = Util.Decimal2String(value); }
        get { return txt_OctroiFormCharges.Text == string.Empty ? 0 : Util.String2Decimal(txt_OctroiFormCharges.Text); }
    }
    public decimal OctroiServiceCharge
    {
        set { txt_OctroiServiceCharges.Text = Util.Decimal2String(value); }
        get { return txt_OctroiFormCharges.Text == string.Empty ? 0 : Util.String2Decimal(txt_OctroiServiceCharges.Text); }
    }
    public decimal GICharges
    {
        set { txt_GICharges.Text = Util.Decimal2String(value); }
        get { return txt_GICharges.Text == string.Empty ? 0 : Util.String2Decimal(txt_GICharges.Text); }
    }

    public decimal DeliveryCommission
    {
        set { txt_DeliveryCommission.Text = Util.Decimal2String(value); }
        get { return txt_DeliveryCommission.Text == string.Empty ? 0 : Util.String2Decimal(txt_DeliveryCommission.Text); }
    }
    public int FirstNoticeDays
    {
        set { txt_FirstNoticeDays.Text = Util.Int2String(value); }
        get { return txt_FirstNoticeDays.Text == string.Empty ? 0 : Util.String2Int(txt_FirstNoticeDays.Text); }
    }
    public int SecontNoticeDays
    {
        set { txt_SecondNoticeDays.Text = Util.Int2String(value); }
        get { return txt_SecondNoticeDays.Text == string.Empty ? 0 : Util.String2Int(txt_SecondNoticeDays.Text); }
    }
    public int ThirdNoticeDays
    {
        set { txt_ThirdNoticeDays.Text = Util.Int2String(value); }
        get { return txt_ThirdNoticeDays.Text == string.Empty ? 0 : Util.String2Int(txt_ThirdNoticeDays.Text); }
    }
    public decimal CashLimit
    {
        set { txt_BankLimit.Text = Util.Decimal2String(value); }
        get { return txt_BankLimit.Text == string.Empty ? 0 : Util.String2Decimal(txt_BankLimit.Text); }
    }
    public decimal BankLimit
    {
        set { txt_CashLimit.Text = Util.Decimal2String(value); }
        get { return txt_CashLimit.Text == string.Empty ? 0 : Util.String2Decimal(txt_CashLimit.Text); }
    }


    public decimal Bkg_Freight
    {
        set { txt_Freight.Text = Util.Decimal2String(value); }
        get { return txt_Freight.Text == string.Empty ? 0 : Util.String2Decimal(txt_Freight.Text); }
    }
    public decimal Bkg_HamaliofBooking
    {
        set { txt_HamaliofBooking.Text = Util.Decimal2String(value); }
        get { return txt_HamaliofBooking.Text == string.Empty ? 0 : Util.String2Decimal(txt_HamaliofBooking.Text); }
    }
    public decimal Bkg_FovofBooking
    {
        set { txt_FovofBooking.Text = Util.Decimal2String(value); }
        get { return txt_FovofBooking.Text == string.Empty ? 0 : Util.String2Decimal(txt_FovofBooking.Text); }
    }
    public decimal Bkg_TpCharge
    {
        set { txt_TpCharge.Text = Util.Decimal2String(value); }
        get { return txt_TpCharge.Text == string.Empty ? 0 : Util.String2Decimal(txt_TpCharge.Text); }
    }
    public decimal Bkg_Ddcharge
    {
        set { txt_Ddcharge.Text = Util.Decimal2String(value); }
        get { return txt_Ddcharge.Text == string.Empty ? 0 : Util.String2Decimal(txt_Ddcharge.Text); }
    }
    public decimal Dly_Octroiformchargepercent
    {
        set { txt_Octroiformchargepercent.Text = Util.Decimal2String(value); }
        get { return txt_Octroiformchargepercent.Text == string.Empty ? 0 : Util.String2Decimal(txt_Octroiformchargepercent.Text); }
    }
    public decimal Dly_Octroiservicechargepercent
    {
        set { txt_Octroiservicechargepercent.Text = Util.Decimal2String(value); }
        get { return txt_Octroiservicechargepercent.Text == string.Empty ? 0 : Util.String2Decimal(txt_Octroiservicechargepercent.Text); }
    }
    public decimal Dly_GichargesofDel
    {
        set { txt_GichargesofDel.Text = Util.Decimal2String(value); }
        get { return txt_GichargesofDel.Text == string.Empty ? 0 : Util.String2Decimal(txt_GichargesofDel.Text); }
    }
    public decimal Dly_HamaliofDel
    {
        set { txt_HamaliofDel.Text = Util.Decimal2String(value); }
        get { return txt_HamaliofDel.Text == string.Empty ? 0 : Util.String2Decimal(txt_HamaliofDel.Text); }
    }
    public decimal Dly_Demurrage
    {
        set { txt_Demurrage.Text = Util.Decimal2String(value); }
        get { return txt_Demurrage.Text == string.Empty ? 0 : Util.String2Decimal(txt_Demurrage.Text); }
    }
    public decimal FOVRate
    {
        set { txt_FOVRate.Text= Util.Decimal2String(value); }
        get { return txt_FOVRate.Text == string.Empty ? 0 : Util.String2Decimal(txt_FOVRate.Text); }
    }
    public decimal InvoiceRate
    {
        set { txt_InvoiceRate.Text = Util.Decimal2String(value); }
        get { return txt_InvoiceRate.Text == string.Empty ? 0 : Util.String2Decimal(txt_InvoiceRate.Text); }
    }
    public decimal InvoicePerHowManyRs
    {
        set { txt_InvoicePerHowManyRs.Text = Util.Decimal2String(value); }
        get { return txt_InvoicePerHowManyRs.Text == string.Empty ? 0 : Util.String2Decimal(txt_InvoicePerHowManyRs.Text); }
    }
    public Boolean IsFOVCalculatedAsPerStandard
    {
        get { return _Is_FOV_Calcualted_As_Per_Standard; }
        set { _Is_FOV_Calcualted_As_Per_Standard = value; }
    }
    public decimal AOCPercent
    {
        set { txt_AOC_Percent.Text = Util.Decimal2String(value); }
        get { return txt_AOC_Percent.Text == string.Empty ? 0 : Util.String2Decimal(txt_AOC_Percent.Text); }
    }

    #endregion

    #region ControlsBind

    public void SetControl()
    {
        if (keyID > 0)
        {
            //ddl_ToBranch.Style.Add("display","none");
            ddl_ToBranch.Visible = false;
            lbl_Branch_Name.Visible = true;
            lbl_Branch.Text = "Branch:";
        }
        else
        {
            //ddl_ToBranch.Style.Add("display", "inline");
            ddl_ToBranch.Visible = true;
            lbl_Branch_Name.Visible = false;
            lbl_Branch.Text = "Select Branch:";
        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        TextBox txt_ToBranch;
        txt_ToBranch = (TextBox)ddl_ToBranch.FindControl("txtBoxddl_ToBranch");
        bool _isValid = false;

        if (Util.String2Int(hdn_KeyId.Value) <= 0 && ToBranchID <= 0)
        {
            errorMessage = "Please Select Branch"; //GetLocalResourceObject("Msg_ddl_ToBranch").ToString();
            scm_brcrateparam.SetFocus(txt_ToBranch);
        }
        else if (MinChargeWt <= 0 && pc.Control_Is_Mandatory(txt_MinChrgeWt) == true)
        {
            errorMessage = "Please Enter Minimum Charge Weight";// GetLocalResourceObject("Msg_txt_MinchWt").ToString();
            scm_brcrateparam.SetFocus(txt_MinChrgeWt);
        }
        else if (BiltyCharges <= 0 && pc.Control_Is_Mandatory(txt_BiltyCharges) == true)
        {
            errorMessage = GetLocalResourceObject("Msg_txt_Biltycharge").ToString();
            scm_brcrateparam.SetFocus(txt_BiltyCharges);
        }
        else if (FOVPercent <= 0 && pc.Control_Is_Mandatory(txt_FOVPercent) == true)
        {
            errorMessage = "Please Enter FOV Percent"; //GetLocalResourceObject("Msg_txt_fovPercent").ToString();
            scm_brcrateparam.SetFocus(txt_FOVPercent);
        }
        else if (MinFOV <= 0 && pc.Control_Is_Mandatory(txt_MinFOV) == true)
        {
            errorMessage = "Please Enter Min  FOV";//GetLocalResourceObject("Msg_txt_Minfov").ToString();
            scm_brcrateparam.SetFocus(txt_MinFOV);
        }
        else if (Hamali <= 0 && pc.Control_Is_Mandatory(txt_HamaliPercent) == true)
        {
            errorMessage = "Please Enter Hamali"; //GetLocalResourceObject("Msg_txt_hamalipercent").ToString();
            scm_brcrateparam.SetFocus(txt_HamaliPercent);
        }
        else if (MinHamali <= 0 && pc.Control_Is_Mandatory(txt_MinHamali) == true)
        {
            errorMessage = "Please Enter Min Hamali";// GetLocalResourceObject("Msg_txt_Minhamali").ToString();
            scm_brcrateparam.SetFocus(txt_MinHamali);
        }
        else if (DoorDeliveryCharges <= 0 && pc.Control_Is_Mandatory(txt_DoorDeliveryCharges) == true)
        {
            errorMessage = "Please Enter DoorDeliveryCharges";// GetLocalResourceObject("MSG_txt_DoorDeliveryCharges").ToString();
            scm_brcrateparam.SetFocus(txt_DoorDeliveryCharges);
        }
        else if (ToPayCharges <= 0 && pc.Control_Is_Mandatory(txt_ToPayCharges) == true)
        {
            errorMessage = "Please Enter To Pay Charges";//GetLocalResourceObject("Msg_txt_TopayCharges").ToString();
            scm_brcrateparam.SetFocus(txt_ToPayCharges);
        }
        else if (DACCCharges <= 0 && pc.Control_Is_Mandatory(txt_DACCCharges) == true)
        {
            errorMessage = "Please Enter DACC Charges";//GetLocalResourceObject("Msg_txt_daccCharges").ToString();
            scm_brcrateparam.SetFocus(txt_DACCCharges);
        }
        else if (CFTFactor <= 0 && pc.Control_Is_Mandatory(txt_CFTFactor) == true)
        {
            errorMessage = "Please Enter CFT Factor";//GetLocalResourceObject("Msg_txt_CftFactor").ToString();
            scm_brcrateparam.SetFocus(txt_CFTFactor);
        }
        else if (DemurrageDays <= 0 && pc.Control_Is_Mandatory(txt_DemurrageDays) == true)
        {
            errorMessage = "Please Enter Demurrage Days"; //GetLocalResourceObject("Msg_txt_DemDays").ToString();
            scm_brcrateparam.SetFocus(txt_DemurrageDays);
        }
        else if (DemurrageRate <= 0 && pc.Control_Is_Mandatory(txt_DemurrageRate) == true)
        {
            errorMessage = "Please Enter Demurrage Rate";//GetLocalResourceObject("Msg_txt_DemRate").ToString();
            scm_brcrateparam.SetFocus(txt_DemurrageRate);
        }
        else if (OctroiFormCharges <= 0 && pc.Control_Is_Mandatory(txt_OctroiFormCharges) == true)
        {
            errorMessage = "Please Enter Octroi Form Charges";//GetLocalResourceObject("Msg_txt_OctroiFormCharge").ToString();
            scm_brcrateparam.SetFocus(txt_OctroiFormCharges);
        }
        else if (OctroiServiceCharge <= 0 && pc.Control_Is_Mandatory(txt_OctroiServiceCharges) == true)
        {
            errorMessage = "Please Enter Octroi Service Charges";//GetLocalResourceObject("Msg_txt_OctroiserCharge").ToString();
            scm_brcrateparam.SetFocus(txt_OctroiServiceCharges);
        }
        else if (GICharges <= 0 && pc.Control_Is_Mandatory(txt_GICharges) == true)
        {
            errorMessage = GetLocalResourceObject("Msg_txt_GICharge").ToString();
            scm_brcrateparam.SetFocus(txt_GICharges);
        }
        else if (DeliveryCommission <= 0 && pc.Control_Is_Mandatory(txt_DeliveryCommission) == true)
        {
            errorMessage = "Please Enter Delivery Commission"; //GetLocalResourceObject("Msg_txt_DelCommission").ToString();
            scm_brcrateparam.SetFocus(txt_DeliveryCommission);
        }
        else if (FirstNoticeDays <= 0 && pc.Control_Is_Mandatory(txt_FirstNoticeDays) == true)
        {
            errorMessage = "Please Enter First Notice Days"; //GetLocalResourceObject("Msg_txt_FND").ToString();
            scm_brcrateparam.SetFocus(txt_FirstNoticeDays);
        }
        else if (SecontNoticeDays <= 0 && pc.Control_Is_Mandatory(txt_SecondNoticeDays) == true)
        {
            errorMessage = "Please Enter Second Notice Days"; //GetLocalResourceObject("Msg_txt_SND").ToString();
            scm_brcrateparam.SetFocus(txt_SecondNoticeDays);
        }
        else if (ThirdNoticeDays <= 0 && pc.Control_Is_Mandatory(txt_ThirdNoticeDays) == true)
        {
            errorMessage = "Please Enter Third Notice Days";// GetLocalResourceObject("Msg_txt_TND").ToString();
            scm_brcrateparam.SetFocus(txt_ThirdNoticeDays);
        }
        else if (CashLimit <= 0 && pc.Control_Is_Mandatory(txt_CashLimit) == true)
        {
            errorMessage = "Please Enter Cash Limit"; //GetLocalResourceObject("Msg_txt_CashLimit").ToString();
            scm_brcrateparam.SetFocus(txt_CashLimit);
        }
        else if (BankLimit <= 0 && pc.Control_Is_Mandatory(txt_BankLimit) == true)
        {
            errorMessage = "Please Enter Bank Limit"; //GetLocalResourceObject("Msg_txt_BankLimit").ToString();
            scm_brcrateparam.SetFocus(txt_BankLimit);
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return 12;
        }
    }
    #endregion

    #region ControlEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        ddl_ToBranch.DataTextField = "Branch_Name";
        ddl_ToBranch.DataValueField = "Branch_ID";

        ddl_CopyFrom.DataTextField = "Branch_Name";
        ddl_CopyFrom.DataValueField = "Branch_ID";

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        tr_ServiceTax.Visible = false;
        if (IsFOVCalculatedAsPerStandard == false)
        {
            tr_Fov_Invoice_Rate.Visible = true;
            tr_InvoicePerHowManyRs.Visible = true;
        }
        else
        {
            tr_Fov_Invoice_Rate.Visible = false;
            tr_InvoicePerHowManyRs.Visible = false;
            FOVRate = 0;
            InvoiceRate = 0;
            InvoicePerHowManyRs = 0;
        }
        objBranchRateParametersPresenter = new BranchRateParametersPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            pc.AddAttributes(this.Controls);
            Raj.EC.Common ObjCommon = new Raj.EC.Common();
            hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/Branch/App_LocalResources/WucBranchRateParameters.ascx.resx");
            hdn_KeyId.Value = keyID.ToString();
            ServiceTax = Util.String2Decimal("10.30");
            SetControl();
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objBranchRateParametersPresenter.Save();
    }
    protected void ddl_CopyFrom_TxtChange(object sender, EventArgs e)
    {
        objBranchRateParametersPresenter.fillValuesAfterBranchSelection();
        upnl_RateCard.Update();
        UpdatePanel2.Update();
        UpdatePanel1.Update();
    }

    #endregion

}