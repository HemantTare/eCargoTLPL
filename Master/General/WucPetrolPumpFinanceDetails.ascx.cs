
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
 

using Raj.EC.MasterView;
using Raj.EC.MasterPresenter;
using Raj.EC.ControlsView;
using Raj.EC.ControlsPresenter;

using System.Data.SqlClient;


public partial class EC_Master_WucPetrolPumpFinanceDetails : System.Web.UI.UserControl, IPetrolPumpFinanceDetailsView
{

    #region ClassVariables
    PetrolPumpFinanceDetailsPresenter objPetrolPumpFinanceDetailsPresenter;
    TDSAppPresenter objTDSAppPresenter;


    DataSet objDS = new DataSet();

    #endregion

    #region ControlsValues

    public ITDSAppView TDSAppView
    {
        get { return (ITDSAppView)WucTDSApp1; }
    }

    public int LedgerGroupId
    {
        set
        {
            ddl_LedgerGroup.SelectedValue = Util.Int2String(value);
            //hdn_Ledger_Group_Id.Value = Util.Int2String(value);
        }
        get
        {
            //return Util.String2Int(hdn_Ledger_Group_Id.Value);
            return Util.String2Int(ddl_LedgerGroup.SelectedValue);
        }
    }


    public int LedgerId
    {
        set
        {
            ddl_Ledger.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_Ledger.SelectedValue);
        }
    }


    public Boolean Is_Use_Existing_Ledger
    {
        set
        {
            hdn_Use_Existing_Ledger.Value = Convert.ToString(value);
        }
        get
        {
            return Convert.ToBoolean(hdn_Use_Existing_Ledger.Value);
        }
    }



    public Boolean Is_Service_Tax_Applicable
    {
        set
        {
            chk_IsServiceTaxApplicable.Checked = Convert.ToBoolean(value);
        }
        get
        {
            return Convert.ToBoolean(chk_IsServiceTaxApplicable.Checked);
        }
    }





    //public Boolean Is_Lower_No_Deduction_Applicable
    //{
    //    set
    //    {
    //        chk_IsLowerNoDeductionApplicable.Checked = Convert.ToBoolean(value);

    //    }
    //    get
    //    {
    //        return Convert.ToBoolean(chk_IsLowerNoDeductionApplicable.Checked);
    //    }
    //}

    //public Boolean Is_Ignore_Exemption_Limit
    //{
    //    set
    //    {
    //        chk_IgnoreSurchargeExemptionLimit.Checked = Convert.ToBoolean(value);
    //    }
    //    get
    //    {
    //        return Convert.ToBoolean(chk_IgnoreSurchargeExemptionLimit.Checked);
    //    }
    //}

    //public Boolean Is_TDS_Applicable
    //{
    //    set
    //    {
    //        chk_IsTDSApplicable.Checked = Convert.ToBoolean(value);
    //    }
    //    get
    //    {
    //        return Convert.ToBoolean(chk_IsTDSApplicable.Checked);
    //    }
    //}

    public Boolean Is_Exempted
    {
        set
        {
            chk_IsExempted.Checked = Convert.ToBoolean(value);
        }
        get
        {
            return Convert.ToBoolean(chk_IsExempted.Checked);
        }
    }


    public Boolean Use_Existing_Ledger
    {
        set
        {
            rbl_CreateNewLedger.Checked = false;
            rbl_UseExistingLedger.Checked = false;

            if (Convert.ToBoolean(value))
            {
                rbl_UseExistingLedger.Checked = true;
            }
            else
            {
                rbl_CreateNewLedger.Checked = true;
            }

        }
        get
        {
            if (rbl_UseExistingLedger.Checked)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
    }

    //public String Section_Number
    //{
    //    set
    //    {
    //        ddl_Section_Number.SelectedValue = value;
    //    }
    //    get
    //    {
    //        return ddl_Section_Number.SelectedValue;
    //    }
    //}




    //public String TDS_Deductee_Type_Name
    //{        
    //    get
    //    {
    //        return ddl_DeducteeType.SelectedItem.Text ;
    //    }
    //}

    public String Notification_Detail
    {
        set
        {
            txt_NotificationDetails.Text = value;
        }
        get
        {
            return txt_NotificationDetails.Text;
        }
    }

    public int CreditDays
    {
        set
        {
            txt_CreditDays.Text = Convert.ToString(value);
        }
        get
        {
            return Convert.ToInt32(txt_CreditDays.Text.Trim() == string.Empty ? "0" : txt_CreditDays.Text.Trim());
        }
    }

    public Decimal CreditLimit
    {
        set
        {
            txt_CreditLimit.Text = Convert.ToString(value);
        }
        get
        {
            return Convert.ToDecimal(txt_CreditLimit.Text.Trim() == string.Empty ? "0" : txt_CreditLimit.Text.Trim());
        }
    }

    //public int TDS_Deductee_Type_ID
    //{
    //    set
    //    {
    //        ddl_DeducteeType.SelectedValue = Util.Int2String(value);
    //    }
    //    get
    //    {
    //        return Util.String2Int(ddl_DeducteeType.SelectedValue);
    //    }
    //}


    //public Decimal  TDS_Lower_Rate
    //{
    //    set
    //    {
    //        txt_TDSLowerRate.Text = Util.Decimal2String (value);

    //    }
    //    get
    //    {
    //        return Util.String2Decimal (txt_TDSLowerRate.Text  );
    //    }
    //}

    #endregion

    #region ControlsBind


    public DataTable BindLedgerGroup
    {
        set
        {
            ddl_LedgerGroup.DataSource = value;
            ddl_LedgerGroup.DataTextField = "Ledger_Group_Name";
            ddl_LedgerGroup.DataValueField = "Ledger_Group_Id";
            ddl_LedgerGroup.DataBind();

            Raj.EC.Common.InsertItem(ddl_LedgerGroup);
        }
    }


    public DataTable BindLedger
    {
        set
        {
            ddl_Ledger.DataSource = value;
            ddl_Ledger.DataTextField = "Ledger_Name";
            ddl_Ledger.DataValueField = "Ledger_Id";
            ddl_Ledger.DataBind();

            Raj.EC.Common.InsertItem(ddl_Ledger);
        }
    }


    //public DataTable BindTDSDeducteeType
    //{
    //    set
    //    {
    //        ddl_DeducteeType.DataSource = value;
    //        ddl_DeducteeType.DataTextField = "TDS_Deductee_Type_Name";
    //        ddl_DeducteeType.DataValueField = "TDS_Deductee_Type_Id";
    //        ddl_DeducteeType.DataBind();
    //    }
    //}

    public DataTable BindDivision
    {
        set
        {
            ChkLst_Division.DataSource = value;
            ChkLst_Division.DataTextField = "Division_Name";
            ChkLst_Division.DataValueField = "Division_ID";
            ChkLst_Division.DataBind();

            DataSet ds_Applicable_Divisions_Details = new DataSet();

            ds_Applicable_Divisions_Details.Tables.Add(value.Copy());

            Session_Applicable_Divisions_Details = ds_Applicable_Divisions_Details;
        }
    }

    public string Applicable_Divisions_Details_Xml
    {
        get { return Session_Applicable_Divisions_Details.GetXml().ToLower(); }
    }


    public DataSet Session_Applicable_Divisions_Details
    {
        get { return StateManager.GetState<DataSet>("Applicable_Divisions_Details"); }
        set { StateManager.SaveState("Applicable_Divisions_Details", value); }
    }


    #endregion


    #region IView
    public bool validateUI()
    {
        Boolean ValidateBeforeSave = false;
        ValidateBeforeSave = false;

        errorMessage = "";

        if (LedgerGroupId <= 0 )
        {
            errorMessage = "Please Select Ledger Group";
            ddl_LedgerGroup.Focus();
           
        }
        else if (LedgerId <= 0 && ddl_Ledger.Visible == true)
        {
            errorMessage = "Please Select Ledger ";
            ddl_Ledger.Focus();
        }
        //else if (txt_CreditDays.Text.Trim() == string.Empty)
        //{
        //    errorMessage = "Please Enter Credit Days.";
        //    txt_CreditDays.Focus(); 
        //}
        //else if (txt_CreditLimit.Text.Trim() == string.Empty)
        //{
        //    errorMessage = "Please Enter Credit Limit.";
        //    txt_CreditLimit.Focus();
        //}

        else if (!WucTDSApp1.ValidateWucTDSApp(lbl_Errors))
        {

        }

        else
        {
            ValidateBeforeSave = true;
        }


        //if (ddl_Section_Number.Visible == true && ddl_Section_Number.SelectedItem.Text == "197" && ValidateBeforeSave)
        //{

        //    if (txt_TDSLowerRate.Text.Trim() == string.Empty)
        //    {
        //        errorMessage = "Please Enter Lower Rate.";
        //        txt_TDSLowerRate.Focus();
        //        //ValidateBeforeSave = true;
        //        ValidateBeforeSave = false;
        //    }
        //    else
        //    {
        //        ValidateBeforeSave = true;
        //    }
        //}


        //if (UserManager.getUserParam().IsDivisionReq)
        //{
        //    Get_Applicable_Divisions_Details();
        //}
        //else
        //{
        //    DataSet ds_ApplicableDivision = new DataSet();
        //    DataRow dr;


        //    ds_ApplicableDivision = Session_Applicable_Divisions_Details.Clone();

        //    dr = ds_ApplicableDivision.Tables[0].NewRow();
        //    dr["Division_Id"] = "1";
        //    dr["Division_Name"] = "Sundry";
        //    ds_ApplicableDivision.Tables[0].Rows.Add(dr);

        //    Session_Applicable_Divisions_Details = ds_ApplicableDivision;

        //}


        if (UserManager.getUserParam().IsDivisionReq && ValidateBeforeSave == true)
        {
            Get_Applicable_Divisions_Details();
            if (Session_Applicable_Divisions_Details.Tables[0].Rows.Count <= 0)
            {
                errorMessage = "Please Select Atleast ONE SBU.";
                ValidateBeforeSave = false;
            }
            else
            {
                ValidateBeforeSave = true;
            }
        }
        else
        {
            DataSet ds_ApplicableDivision = new DataSet();
            DataRow dr;


            ds_ApplicableDivision = Session_Applicable_Divisions_Details.Clone();

            dr = ds_ApplicableDivision.Tables[0].NewRow();
            dr["Division_Id"] = "1";
            dr["Division_Name"] = "Sundry";
            ds_ApplicableDivision.Tables[0].Rows.Add(dr);

            Session_Applicable_Divisions_Details = ds_ApplicableDivision;
            //isValid = true;
        }

        return ValidateBeforeSave;


        //return (ValidityBeforeSave( ));
    }


    public string errorMessage
    {
        set
        {
            //errorMessage = value;
            lbl_Errors.Text = value;
        }
    }


    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return 42009;
        }
    }

    #endregion


    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {

        objPetrolPumpFinanceDetailsPresenter = new PetrolPumpFinanceDetailsPresenter(this, IsPostBack);
        objTDSAppPresenter = new TDSAppPresenter(WucTDSApp1, IsPostBack);
        if (!IsPostBack)
        {
            // Enable_Desable_Controls(false);
            if (ddl_LedgerGroup.Items.Count > 0)
            {
                ddl_LedgerGroup_SelectedIndexChanged(sender, e);
                if (ddl_Ledger.Items.Count > 0)
                {
                    ddl_Ledger_SelectedIndexChanged(sender, e);
                }
            }

            if (keyID <= 0)
            {
                Enable_Desable_Controls(false); //added by Ankit : 08/11/08
                rbl_UseExistingLedger.Checked = true;
                rbl_CreateNewLedger.Checked = false;
            }

            Show_Hide_Controls();

            if (keyID > 0)
            {
                SetApplicableDivisionsDetails();
                rbl_CreateNewLedger.Enabled = false;

                ddl_LedgerGroup.Enabled = false;
                ddl_Ledger.Enabled = false;

                if (rbl_CreateNewLedger.Checked == true)
                {
                    ddl_Ledger.Visible = false;
                    lbl_Ledger.Visible = false;
                    lbl_LedgerMandatory.Visible = false;
                }

                //if (!Convert.ToBoolean(hdn_Use_Existing_Ledger.Value))
                //{
                //    ddl_Ledger.Visible = false;
                //    lbl_Ledger.Visible = false;
                //}

                if (rbl_CreateNewLedger.Checked == false)
                {
                    Enable_Desable_Controls(false);
                    ddl_LedgerGroup.Enabled = true;
                    ddl_Ledger.Enabled = true;
                    lbl_LedgerMandatory.Visible = true;
                }
                else
                {
                    Enable_Desable_Controls(false);
                    ddl_LedgerGroup.Enabled = false;
                    ddl_Ledger.Enabled = false;
                }
            }
        }

       
        

        String Script = "<script type='text/javascript'>Hide_Control(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);


    }




    protected void ddl_LedgerGroup_SelectedIndexChanged(Object sender, System.EventArgs e)
    {
        objPetrolPumpFinanceDetailsPresenter.Fill_Ledger();

        DataSet ds = new DataSet();

        ds = objPetrolPumpFinanceDetailsPresenter.Get_LedgerDetails();
        SetLedgerDetails(ds);
        TDSAppView.Call_From = 2;
        TDSAppView.ID = LedgerId;
        objTDSAppPresenter.FillDetails();

        Show_Hide_Controls();


    }

    protected void ddl_Ledger_SelectedIndexChanged(Object sender, System.EventArgs e)
    {
      
        
            objDS = objPetrolPumpFinanceDetailsPresenter.Get_LedgerDetails();
            SetLedgerDetails(objDS);

            TDSAppView.Call_From = 2;
            TDSAppView.ID = LedgerId;
            objTDSAppPresenter.FillDetails();

            Show_Hide_Controls();

            if (ddl_Ledger.SelectedValue == "0")
            {
                Cleare_Ledger_Details();
            }

            
        
    }

    public void SetApplicableDivisionsDetails()
    {
        DataSet objDS_PetrolPumpDivisionsDetails = new DataSet();
        objDS_PetrolPumpDivisionsDetails = objPetrolPumpFinanceDetailsPresenter.ReadDivisionsValues();

        int lst_cnt = 0;
        int ds_cnt = 0;

        for (lst_cnt = 0; lst_cnt <= ChkLst_Division.Items.Count - 1; lst_cnt++)
        {
            ChkLst_Division.Items[lst_cnt].Selected = false;
        }

        if (objDS_PetrolPumpDivisionsDetails.Tables[0].Rows.Count > 0)
        {
            for (ds_cnt = 0; ds_cnt <= objDS_PetrolPumpDivisionsDetails.Tables[0].Rows.Count - 1; ds_cnt++)
            {

                for (lst_cnt = 0; lst_cnt <= ChkLst_Division.Items.Count - 1; lst_cnt++)
                {
                    if (Convert.ToInt32(ChkLst_Division.Items[lst_cnt].Value) == Convert.ToInt32(objDS_PetrolPumpDivisionsDetails.Tables[0].Rows[ds_cnt]["Division_ID"]))
                    {
                        ChkLst_Division.Items[lst_cnt].Selected = true;
                    }
                }
            }
        }

    }

    public void SetLedgerDetails(DataSet objDs_Ledger_Details)
    {

        //Enable_Desable_Controls(false);

        if (objDs_Ledger_Details.Tables[0].Rows.Count > 0)
        {

            txt_CreditDays.Text = Convert.ToString(objDs_Ledger_Details.Tables[0].Rows[0]["Default_Credit_Period"]);

            txt_CreditLimit.Text = Convert.ToString(objDs_Ledger_Details.Tables[0].Rows[0]["Credit_Limit"]);

            chk_IsServiceTaxApplicable.Checked = Convert.ToBoolean(objDs_Ledger_Details.Tables[0].Rows[0]["Is_Service_Tax_Applicable"]);

            chk_IsExempted.Checked = Convert.ToBoolean(objDs_Ledger_Details.Tables[0].Rows[0]["Is_Exempted"]);

            txt_NotificationDetails.Text = Convert.ToString(objDs_Ledger_Details.Tables[0].Rows[0]["Notification_Detail"]);

            //chk_IsTDSApplicable.Checked = Convert.ToBoolean(objDs_Ledger_Details.Tables[0].Rows[0]["Is_TDS_Applicable"]);

            //if (Convert.ToInt32(objDs_Ledger_Details.Tables[0].Rows[0]["TDS_Deductee_Type_Id"]) > 0)
            //{
            //    ddl_DeducteeType.SelectedValue = Convert.ToString(objDs_Ledger_Details.Tables[0].Rows[0]["TDS_Deductee_Type_Id"]);
            //}

            //chk_IsLowerNoDeductionApplicable.Checked = Convert.ToBoolean(objDs_Ledger_Details.Tables[0].Rows[0]["Is_Lower_Deduction"]);

            //if (Convert.ToString(objDs_Ledger_Details.Tables[0].Rows[0]["Section_Number"]) != "0")
            //{
            //    ddl_Section_Number.SelectedValue = Convert.ToString(objDs_Ledger_Details.Tables[0].Rows[0]["Section_Number"]);
            //}

            //chk_IgnoreSurchargeExemptionLimit.Checked = Convert.ToBoolean(objDs_Ledger_Details.Tables[0].Rows[0]["Ignore_Exemption_Limit"]);
        }
    }


    public void Enable_Desable_Controls(Boolean Enable_Desable)
    {

        txt_CreditDays.Enabled = Enable_Desable;

        txt_CreditLimit.Enabled = Enable_Desable;

        chk_IsServiceTaxApplicable.Enabled = Enable_Desable;

        chk_IsExempted.Enabled = Enable_Desable;

        txt_NotificationDetails.Enabled = Enable_Desable;

        WucTDSApp1.Enable_Disable_Controls(Enable_Desable);

        //chk_IsTDSApplicable.Enabled=Enable_Desable ;

        //ddl_DeducteeType.Enabled = Enable_Desable;

        //chk_IsLowerNoDeductionApplicable.Enabled=Enable_Desable ;

        //ddl_Section_Number.Enabled=Enable_Desable ;

        //chk_IgnoreSurchargeExemptionLimit.Enabled = Enable_Desable;

        //txt_TDSLowerRate.Enabled = Enable_Desable;
    }


    public void Cleare_Ledger_Details()
    {
        txt_CreditDays.Text = "";
        txt_CreditLimit.Text = "";

        chk_IsServiceTaxApplicable.Checked = false;
        chk_IsExempted.Checked = false;
        txt_NotificationDetails.Text = "";
        //chk_IsTDSApplicable.Checked = false;
        //chk_IsLowerNoDeductionApplicable.Checked = false;
        //chk_IgnoreSurchargeExemptionLimit.Checked = false;

        TDSAppView.IsTDSApp = false;
        Show_Hide_Controls();
    }


    public void Show_Hide_Controls()
    {

        Object sender = "";
        System.EventArgs e = null;


        chk_IsServiceTaxApplicable_CheckedChanged(sender, e);

        chk_IsExempted_CheckedChanged(sender, e);

        String Script = "<script type='text/javascript'>Hide_Control(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
        //chk_IsTDSApplicable_CheckedChanged(sender, e);
        //chk_IsLowerNoDeductionApplicable_CheckedChanged(sender, e);
        //ddl_Section_Number_SelectedIndexChanged(sender, e);

        //chk_IgnoreSurchargeExemptionLimit_CheckedChanged(sender, e);



        if (UserManager.getUserParam().IsDivisionReq)
        {
            tr_Divisions.Visible = true;
            ChkLst_Division.Visible = true;
            lbl_Divisions.Visible = true;

        }
        else
        {
            tr_Divisions.Visible = false;
            ChkLst_Division.Visible = false;
            lbl_Divisions.Visible = false;

            DataSet ds_ApplicableDivision = new DataSet();
            DataRow dr;


            ds_ApplicableDivision = Session_Applicable_Divisions_Details.Clone();

            dr = ds_ApplicableDivision.Tables[0].NewRow();
            dr["Division_Id"] = "1";
            dr["Division_Name"] = "Sundry";
            ds_ApplicableDivision.Tables[0].Rows.Add(dr);

            Session_Applicable_Divisions_Details = ds_ApplicableDivision;

        }



    }

    protected void chk_IsServiceTaxApplicable_CheckedChanged(Object sender, System.EventArgs e)
    {
        if (chk_IsServiceTaxApplicable.Checked && chk_IsServiceTaxApplicable.Visible == true)
        {
            lbl_IsExempted.Visible = true;
            chk_IsExempted.Visible = true;
        }
        else
        {
            lbl_IsExempted.Visible = false;
            chk_IsExempted.Visible = false;
            chk_IsExempted.Checked = false;


            //lbl_NotificationDetails.Visible = false;
            //txt_NotificationDetails.Visible = false;
            //txt_NotificationDetails.Text = "";

        }

        String Script = "<script type='text/javascript'>Hide_Control(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
    }



    protected void chk_IsExempted_CheckedChanged(Object sender, System.EventArgs e)
    {
        if (chk_IsExempted.Checked && chk_IsExempted.Visible == true)
        {
            lbl_NotificationDetails.Visible = true;
            txt_NotificationDetails.Visible = true;
        }
        else
        {
            //lbl_NotificationDetails.Visible = false;
            //txt_NotificationDetails.Visible = false;
            //txt_NotificationDetails.Text = "";
        }

        String Script = "<script type='text/javascript'>Hide_Control(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
    }
    //protected void chk_IsTDSApplicable_CheckedChanged(Object sender, System.EventArgs e)
    //{
    //    if (chk_IsTDSApplicable.Checked && chk_IsTDSApplicable.Visible == true)
    //    {
    //        ddl_DeducteeType.Visible = true;
    //        lbl_DeducteeType.Visible = true;

    //        chk_IsLowerNoDeductionApplicable.Visible = true;
    //        lbl_IsLowerNoDeductionApplicable.Visible = true;

    //        lbl_SectionNo.Visible = false;
    //        ddl_Section_Number.Visible = false;

    //        lbl_IgnoreSurchargeExemptionLimit.Visible = true;

    //        chk_IgnoreSurchargeExemptionLimit.Visible = true ;

    //        chk_IsLowerNoDeductionApplicable_CheckedChanged(sender, e);

    //    }
    //    else
    //    {
    //        ddl_DeducteeType.Visible = false;
    //        lbl_DeducteeType.Visible = false;

    //        chk_IsLowerNoDeductionApplicable.Visible = false;
    //        lbl_IsLowerNoDeductionApplicable.Visible = false;

    //        chk_IsLowerNoDeductionApplicable.Checked = false;

    //        lbl_SectionNo.Visible = false;
    //        ddl_Section_Number.Visible = false;

    //        lbl_IgnoreSurchargeExemptionLimit.Visible = false ;
    //        chk_IgnoreSurchargeExemptionLimit.Visible = false;
    //        chk_IgnoreSurchargeExemptionLimit.Checked = false;

    //        lbl_TDSLowerRate.Visible = false;
    //        txt_TDSLowerRate.Visible = false;

    //    }
    //}
    // protected void chk_IsLowerNoDeductionApplicable_CheckedChanged(Object sender, System.EventArgs e)
    //{
    //    if (chk_IsLowerNoDeductionApplicable.Checked && chk_IsLowerNoDeductionApplicable.Visible == true)
    //    {
    //        ddl_Section_Number.Visible = true;
    //        lbl_SectionNo.Visible = true;

    //        ddl_Section_Number_SelectedIndexChanged(sender, e);
    //    }
    //    else
    //    {

    //        ddl_Section_Number.Visible = false ;
    //        lbl_SectionNo.Visible = false ;

    //        //lbl_IgnoreSurchargeExemptionLimit.Visible = false;
    //        //chk_IgnoreSurchargeExemptionLimit.Visible = false;
    //        //chk_IgnoreSurchargeExemptionLimit.Checked = false;


    //        lbl_TDSLowerRate.Visible = false;
    //        txt_TDSLowerRate.Visible = false;
    //    }

    //}
    //protected void chk_IgnoreSurchargeExemptionLimit_CheckedChanged(Object sender, System.EventArgs e)
    //{

    //}


    //protected void ddl_Section_Number_SelectedIndexChanged(Object sender, System.EventArgs e)
    //{

    //    if (ddl_Section_Number.SelectedValue == "197" && chk_IsLowerNoDeductionApplicable.Checked)
    //    {
    //        //lbl_IgnoreSurchargeExemptionLimit.Visible = true;
    //        //chk_IgnoreSurchargeExemptionLimit.Visible = true;

    //        lbl_TDSLowerRate.Visible = true;
    //        txt_TDSLowerRate.Visible = true;

    //    }
    //    else
    //    {
    //        //lbl_IgnoreSurchargeExemptionLimit.Visible = false;
    //        //chk_IgnoreSurchargeExemptionLimit.Visible = false;
    //        //chk_IgnoreSurchargeExemptionLimit.Checked = false;

    //        lbl_TDSLowerRate.Visible = false;
    //        txt_TDSLowerRate.Visible = false;

    //    }

    //    // SM_Ledger_Master.SetFocus(ddl_Section_Number);

    //} 

    protected void btn_Save_Click(object sender, EventArgs e)
    {

        errorMessage = "";

        if (UserManager.getUserParam().IsDivisionReq)
        {
            Get_Applicable_Divisions_Details();
        }
        else
        {
            DataSet ds_ApplicableDivision = new DataSet();
            DataRow dr;


            ds_ApplicableDivision = Session_Applicable_Divisions_Details.Clone();

            dr = ds_ApplicableDivision.Tables[0].NewRow();
            dr["Division_Id"] = "1";
            dr["Division_Name"] = "Sundry";
            ds_ApplicableDivision.Tables[0].Rows.Add(dr);

            Session_Applicable_Divisions_Details = ds_ApplicableDivision;

        }

        objPetrolPumpFinanceDetailsPresenter.save();
        //if (validateUI())
        //{            
        //    errorMessage = "";
        //    objPetrolPumpFinanceDetailsPresenter.save();

        //}
        //else
        //{

        //}
    }


    private Boolean ValidityBeforeSave()
    {
        Boolean ValidateBeforeSave = false;
        ValidateBeforeSave = false;

        errorMessage = "";

        if (txt_CreditDays.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Credit Days.";
            txt_CreditDays.Focus();
            //ValidateBeforeSave = true;
        }
        else if (txt_CreditLimit.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Credit Limit.";
            txt_CreditLimit.Focus();
            //ValidateBeforeSave = true;
        }
        else
        {
            ValidateBeforeSave = true;
        }

        //if (ddl_Section_Number.Visible == true && ddl_Section_Number.SelectedItem.Text == "197" && ValidateBeforeSave)
        //{
        //    if (txt_TDSLowerRate.Text.Trim() == string.Empty)
        //    {
        //        errorMessage = "Please Enter Lower Rate.";
        //        txt_TDSLowerRate.Focus();
        //        //ValidateBeforeSave = true;
        //        ValidateBeforeSave = false;
        //    }
        //    else 
        //    {
        //        ValidateBeforeSave=true;
        //    }
        //}


        Get_Applicable_Divisions_Details();

        return ValidateBeforeSave;
    }


    private void Get_Applicable_Divisions_Details()
    {
        DataSet ds_ApplicableDivision = new DataSet();
        DataRow dr;

        ds_ApplicableDivision = Session_Applicable_Divisions_Details.Clone();

        int cnt = 0;
        for (cnt = 0; cnt <= ChkLst_Division.Items.Count - 1; cnt++)
        {
            if (ChkLst_Division.Items[cnt].Selected == true)
            {
                dr = ds_ApplicableDivision.Tables[0].NewRow();
                dr["Division_Id"] = ChkLst_Division.Items[cnt].Value;
                dr["Division_Name"] = ChkLst_Division.Items[cnt].Text;
                ds_ApplicableDivision.Tables[0].Rows.Add(dr);
            }
        }

        Session_Applicable_Divisions_Details = ds_ApplicableDivision;
    }

    #endregion

    protected void rbl_UseExistingLedger_CheckedChanged(object sender, EventArgs e)
    {
        rbl_CreateNewLedger.Checked = false;
        ddl_Ledger.Visible = true;
        lbl_Ledger.Visible = true;
        lbl_LedgerMandatory.Visible = true;

        Enable_Desable_Controls(false);

        ddl_LedgerGroup.Enabled = true;
        ddl_Ledger.Enabled = true;

        if (ddl_Ledger.Items.Count > 0)
        {
            ddl_Ledger_SelectedIndexChanged(sender, e);
        }

    }

    protected void rbl_CreateNewLedger_CheckedChanged(object sender, EventArgs e)
    {
        rbl_UseExistingLedger.Checked = false;
        ddl_Ledger.Visible = false;
        lbl_Ledger.Visible = false;
        lbl_LedgerMandatory.Visible = false;

        Enable_Desable_Controls(true);

        if (keyID <= 0)
        {
            Cleare_Ledger_Details();
        }
    }
}