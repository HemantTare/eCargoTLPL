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
using Raj.EC.ControlsPresenter;
using Raj.EC.ControlsView;
using ClassLibraryMVP.General;
using Raj.EC;

public partial class CommonControls_wucTDSApp : System.Web.UI.UserControl,ITDSAppView
{

    TDSAppPresenter objTDSAppPresenter;


    #region ClassVariables

    private int _Call_From = 0;
    private int _ID = 0;

    #endregion




    #region ControlValues

    public string DeducteeTypeName 
    {
        get {return ddl_DeducteeType.SelectedItem.Text ;}
    }

    public bool IsTDSApp 
    {
        get {return chk_IsTDSApplicable.Checked ;}
        set { chk_IsTDSApplicable.Checked = value;}
    }

    public bool IsLower 
    {
        get {return chk_IsLowerNoDeductionApplicable.Checked ;}
        set { chk_IsLowerNoDeductionApplicable.Checked = value;}
    }

    public bool IsIgnore 
    {
        get { return chk_IgnoreSurchargeExemptionLimit.Checked ;}
        set { chk_IgnoreSurchargeExemptionLimit.Checked = value;}
    }

    public int DeducteeTypeID 
    {
        get { return Util.String2Int(ddl_DeducteeType.SelectedValue);}
        set { ddl_DeducteeType.SelectedValue = value.ToString();}
    }

    public DataSet BindDeducteeType 
    {
        set 
        { 
            ddl_DeducteeType.DataSource = value;
            ddl_DeducteeType.DataTextField = "TDS_Deductee_Type_Name";
            ddl_DeducteeType.DataValueField = "TDS_Deductee_Type_Id";
            ddl_DeducteeType.DataBind();

            Raj.EC.Common.InsertItem(ddl_DeducteeType);
        }
    }

    public string sectionNo 
    {
        get {return ddl_Section_Number.SelectedValue ;}
        set {ddl_Section_Number.SelectedValue = value ;}
    }
    
    public decimal LowerRate 
    {
        get {return Util.String2Decimal(txt_TDSLowerRate.Text) ;}
        set { txt_TDSLowerRate.Text = value.ToString();}
    }

    public int Call_From 
    {
        get { return _Call_From; ;}
        set { _Call_From = value;}
    }

    public int ID 
    {
        get {return _ID;}
        set {_ID = value;}
    }


    public string SetTDCaptionWidth
    {
        set
        {
            td_lblIgnore.Style.Add("width", value);
            td_lblIsLower.Style.Add("width", value);
            td_lblSectionNo.Style.Add("width", value);
            td_lblTDSLowerRate.Style.Add("width", value);
            td_lblDeducteeType.Style.Add("width", value);
            td_lblIsTDSApp.Style.Add("width", value);
        }
    }

    public string SetTDDataWidth
    {
        set
        {
            td_chkIgnore.Style.Add("width", value);
            td_chkIsLower.Style.Add("width", value);
            td_ddlSectionNo.Style.Add("width", value);
            td_txtTDSLowerRate.Style.Add("width", value);
            td_lblDeducteeType.Style.Add("width", value);
            td_chkIsTDSApp.Style.Add("width", value);
        }
    }

    #endregion

    #region IView

    public int keyID
    { 
        get{ return Util.DecryptToInt(Request.QueryString["Id"]);}
             
    }

    public bool validateUI()
    {
       return true;
    
    }

    public string errorMessage
    {
        set { ;}
    
    }

    #endregion






    protected void Page_Load(object sender, EventArgs e)
    {
        objTDSAppPresenter = new TDSAppPresenter(this, IsPostBack);
        
    }

    public void Enable_Disable_Controls(bool Enalble_Disable)
    {
        chk_IsTDSApplicable.Enabled = Enalble_Disable;
        chk_IsLowerNoDeductionApplicable.Enabled = Enalble_Disable;
        chk_IgnoreSurchargeExemptionLimit.Enabled = Enalble_Disable;
        ddl_DeducteeType.Enabled = Enalble_Disable;
        ddl_Section_Number.Enabled = Enalble_Disable;
        txt_TDSLowerRate.Enabled = Enalble_Disable; 
    
    }

    public bool ValidateWucTDSApp(Label lbl_Error)
    {
        if (chk_IsTDSApplicable.Checked == true)
        {
            if (ddl_DeducteeType.SelectedValue == "0")
            {
                lbl_Error.Text = "Please Select Deductee Type";
                return false;
            }

            if (chk_IsLowerNoDeductionApplicable.Checked == true)
            {

                if (ddl_Section_Number.SelectedValue == "0")
                {
                    lbl_Error.Text = "Please Select Section Number";
                    return false;
                }

                if (ddl_Section_Number.SelectedValue == "197")
                {
                    if (txt_TDSLowerRate.Text.Trim() == "")
                    {
                        lbl_Error.Text = "Please Enter TDS%";
                        return false;
                    }
                }

            }

            return true;
        }
        else
        {
            return true;
        }
    
        
    }

}
