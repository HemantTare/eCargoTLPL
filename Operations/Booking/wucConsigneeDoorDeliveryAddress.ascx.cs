
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
using Raj.EC.MasterView;
using Raj.EC.MasterPresenter;
using ClassLibraryMVP;
//using Raj.EC;

public partial class Operations_Booking_wucConsigneeDoorDeliveryAddress : System.Web.UI.UserControl 
{   
    bool isValid = false;

    #region IView Members

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    #endregion 
 

    #region Controls 
         
  
    public String Session_ConsigneeAddressLine1
    {
        get { return StateManager.GetState<String>("ConsigneeAddressLine1"); }
        set { StateManager.SaveState("ConsigneeAddressLine1", value); }
    }
    
    public String Session_ConsigneeAddressLine2
    {
        get { return StateManager.GetState<String>("ConsigneeAddressLine2"); }
        set { StateManager.SaveState("ConsigneeAddressLine2", value); }
    }

    public String Session_ConsigneeActualAddressLine1
    {
        get { return StateManager.GetState<String>("ConsigneeActualAddressLine1"); }
        set { StateManager.SaveState("ConsigneeActualAddressLine1", value); }
    }

    public String Session_ConsigneeActualAddressLine2
    {
        get { return StateManager.GetState<String>("ConsigneeActualAddressLine2"); }
        set { StateManager.SaveState("ConsigneeActualAddressLine2", value); }
    }

    public String Session_ConsigneeName
    {
        get { return StateManager.GetState<String>("ConsigneeName"); }
        set { StateManager.SaveState("ConsigneeName", value); }
    }

    public Int32 Session_Consignee_Id
    {
        get { return StateManager.GetState<Int32>("Consignee_Id"); }
        set { StateManager.SaveState("Consignee_Id", value); }
    }

    #endregion

    #region Function

    public bool validateUI()
    {
        isValid = false;

        errorMessage = "";

        if (Txt_Consignee_AddressLine1.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Address Line 1...";
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
        hdn_Mode.Value = Request.QueryString["Mode"].ToString();

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
                
        if (!IsPostBack)
        {
            Txt_Consignee_AddressLine1.Text = Session_ConsigneeAddressLine1;
            Txt_Consignee_AddressLine2.Text = Session_ConsigneeAddressLine2;
            lbl_Consignee_Name_Value.Text = Session_ConsigneeName;

            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();

            sbValid.Append("if (Allow_To_Exit() == false) { return false; }");
            sbValid.Append(Page.ClientScript.GetPostBackEventReference(btn_Exit, ""));
            sbValid.Append(";");
            btn_Exit.Attributes.Add("onclick", sbValid.ToString());
        }

        if (hdn_Mode.Value == "4")
        {
            Btn_Save.Visible = false;
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Session_ConsigneeAddressLine1 = Txt_Consignee_AddressLine1.Text ;
            Session_ConsigneeAddressLine2=Txt_Consignee_AddressLine2.Text ;
            
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
}
