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

public partial class Operations_Booking_NewGC_FrnNewGCInsuranceDetails : System.Web.UI.Page
{
    PageControls pc = new PageControls();
    public string errorMessage
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
    public bool validateUI()
    {
        bool isValid = false;
        errorMessage = "";

        if (txt_InsuranceCompany.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Insurence Company.";
            SM_GCInsurenceDetails.SetFocus(txt_InsuranceCompany);
        }
        else if (txt_PolicyNo.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Insurence Policy No.";
            SM_GCInsurenceDetails.SetFocus(txt_PolicyNo);
        }
        else if (Util.String2Decimal(txt_PolicyAmount.Text.Trim() == string.Empty ? "0" : txt_PolicyAmount.Text.Trim()) <= 0)
        {
            errorMessage = "Insurence Policy Amount Should Be Greater Than Zero.";
            SM_GCInsurenceDetails.SetFocus(txt_PolicyAmount);
        }
        else if (Util.String2Decimal(txt_RiskAmount.Text.Trim() == string.Empty ? "0" : txt_RiskAmount.Text.Trim()) <= 0)
        {
            errorMessage = "Risk Amount Should Be Greater Than Zero.";
            SM_GCInsurenceDetails.SetFocus(txt_RiskAmount);
        }
        else if (wuc_PolicyExpDate.SelectedDate < DateTime.Now.Date)
        {
            errorMessage = "Policy Expiry Date Should Not Be Less Than Current Date.";
        }
        else
        {
            isValid = true;
        }

        return isValid;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        hdn_Mode.Value = Request.QueryString["Mode"].ToString();

        if (!IsPostBack)
        {
            pc.Txtbox_Add_Attributes(this.Controls);
            wuc_PolicyExpDate.SelectedDate = DateTime.Now.Date;

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
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Session_InsuranceCompany = txt_InsuranceCompany.Text.Trim();
            Session_PolicyAmount = Util.String2Decimal(txt_PolicyAmount.Text.Trim() == string.Empty ? "0" : txt_PolicyAmount.Text.Trim());
            Session_RiskAmount = Util.String2Decimal(txt_RiskAmount.Text.Trim() == string.Empty ? "0" : txt_RiskAmount.Text.Trim());
            Session_PolicyNo = txt_PolicyNo.Text.Trim();
            Session_PolicyExpDate = wuc_PolicyExpDate.SelectedDate;

            string _Msg = "Saved SuccessFully";

            String popupScript = "<script language='javascript'>alert('" + _Msg + "');updateInsuranceDetails(1);self.close();</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel2, typeof(String), "PopupScript1", popupScript.ToString(), false);
        }
    }

    protected void btn_Exit_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'> { self.close() }</script>");
    }
}
