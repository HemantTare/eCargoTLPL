
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

using ClassLibraryMVP;
using Raj.EC;
public partial class Operations_Booking_wucGCInsurenceDetails : System.Web.UI.UserControl 
{    

    public Common CommonObj = new Common();

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

    public DateTime PolicyExpDate
    {
        set
        {
            wuc_PolicyExpDate.SelectedDate = value;
            hdn_PolicyExpDate.Value = wuc_PolicyExpDate.SelectedDate.ToString();
        }
        get
        {
            return wuc_PolicyExpDate.SelectedDate;
        }
    }

    #endregion 

    #region Controls 
   
    public string InsurenceDetailsErrorMessage
    {
        set { lbl_Errors.Text = value; }
    } 

    public Decimal Session_PolicyAmount
    {
        get { return StateManager.GetState<Decimal>("PolicyAmount"); }
        set { StateManager.SaveState("PolicyAmount", value); }
    }

    public Decimal Session_RiskAmount
    {
        get { return StateManager.GetState<Decimal>("RiskAmount"); }
        set { StateManager.SaveState("RiskAmount", value); }
    }

    public DateTime Session_PolicyExpDate
    {
        get { return StateManager.GetState<DateTime>("PolicyExpDate"); }
        set { StateManager.SaveState("PolicyExpDate", value); }
    }

    public String Session_InsuranceCompany
    {
        get { return StateManager.GetState<String>("InsuranceCompany"); }
        set { StateManager.SaveState("InsuranceCompany", value); }
    }

    public String Session_PolicyNo
    {
        get { return StateManager.GetState<String>("PolicyNo"); }
        set { StateManager.SaveState("PolicyNo", value); }
    }

    public int Session_GCRiskId
    {
        get { return StateManager.GetState<int>("GCRiskId"); }
        set { StateManager.SaveState("GCRiskId", value); }
    }   

    public Decimal ValueOfHdn_Decimal(HiddenField H)
    {
        return Util.String2Decimal(H.Value.Trim() == string.Empty ? "0" : H.Value.Trim());
    }
 
    #endregion
    #region Function
    
    public bool validateUI()
    {
        bool isValid = false;
        isValid = false;

        errorMessage = "";

        if (txt_InsuranceCompany.Text.Trim() == String.Empty  )
        {
            errorMessage = "Please Enter Insurence Company.";
            txt_InsuranceCompany.Focus(); 
        }
        else if (txt_PolicyNo.Text.Trim() == String.Empty)
        {
            errorMessage = "Please Enter Insurence Policy No.";
            txt_PolicyNo.Focus();
        }
        else if (Util.String2Decimal(txt_PolicyAmount.Text.Trim() == string.Empty ? "0" : txt_PolicyAmount.Text.Trim()) <= 0)
        {
            errorMessage = "Insurence Policy Amount Should Be Greater Than Zero.";
            txt_PolicyAmount.Focus(); 
        }
        else if (Util.String2Decimal(txt_RiskAmount.Text.Trim() == string.Empty ? "0" : txt_RiskAmount.Text.Trim()) <= 0)
        {
            errorMessage = "Risk Amount Should Be Greater Than Zero.";
            txt_RiskAmount.Focus(); 
        }
        else if ( wuc_PolicyExpDate.SelectedDate   < DateTime.Now.Date  )
        {
            errorMessage = "Policy Expiry Date Should Not Be Less Than Current Date.";
            wuc_PolicyExpDate.Focus();
        }
        else
        {
            isValid = true;
        }

        return isValid;
    }

     #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        hdn_Mode.Value = Request.QueryString["Mode"].ToString();

        Session_GCRiskId =  Util.String2Int( Request.QueryString["GCRiskId"].ToString());

        if (!IsPostBack)
        {
            
            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();

            sbValid.Append("if (Allow_To_Exit() == false) { return false; }");
            sbValid.Append(Page.ClientScript.GetPostBackEventReference(btn_Exit, ""));
            sbValid.Append(";");
            btn_Exit.Attributes.Add("onclick", sbValid.ToString());
            PolicyExpDate = DateTime.Now;

            if (hdn_Mode.Value == "4")
            {
                Btn_Save.Visible = false;
            }
        }     

        if (!IsPostBack)
        {
            if (StateManager.IsValidSession("InsuranceCompany"))            
            {
                txt_InsuranceCompany.Text = Session_InsuranceCompany;
                txt_PolicyAmount.Text = Session_PolicyAmount.ToString();
                txt_RiskAmount.Text = Session_RiskAmount.ToString();
                txt_PolicyNo.Text = Session_PolicyNo;
                wuc_PolicyExpDate.SelectedDate = Session_PolicyExpDate; 
            }
            else
            {
                txt_InsuranceCompany.Text = "";
                txt_PolicyAmount.Text = "0";

                txt_RiskAmount.Text = "0";
                txt_PolicyNo.Text = "";
            }
        }
        SetStandardCaption();
    }

    protected void wuc_PolicyExpDate_SelectionChanged(object sender, EventArgs e)
    {
        hdn_PolicyExpDate.Value = wuc_PolicyExpDate.SelectedDate.ToString();
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {

            Session_InsuranceCompany = txt_InsuranceCompany.Text.Trim();
            Session_PolicyAmount = Util.String2Decimal(txt_PolicyAmount.Text.Trim() == string.Empty ? "0" : txt_PolicyAmount.Text.Trim());

            Session_RiskAmount = Util.String2Decimal(txt_RiskAmount.Text.Trim() == string.Empty ? "0" : txt_RiskAmount.Text.Trim());

            Session_PolicyNo  = txt_PolicyNo.Text.Trim();

            Session_PolicyExpDate = wuc_PolicyExpDate.SelectedDate;

            string _Msg;
            _Msg = "Saved SuccessFully";

            String popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();</script>";

            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel2, typeof(String), "PopupScript1", popupScript.ToString(), false);
        }
    }

    protected void btn_Exit_Click(object sender, EventArgs e)
    {     
        Response.Write("<script language='javascript'> { self.close() }</script>");
    }

    private void SetStandardCaption()
    {
        //lbl_InsurenceDetails.Text = CompanyManager.getCompanyParam().GcCaption + " Insurence Details";
        lbl_InsurenceDetails.Text = " Insurence Details";
    }
}
