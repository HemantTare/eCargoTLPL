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
public partial class Operations_Booking_NewGC_FrmNewGCConsigneeDDAddress : System.Web.UI.Page
{
    PageControls pc = new PageControls();
    bool isValid = false;

    private string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
   
    public bool validateUI()
    {
        isValid = false;
        errorMessage = "";

        if (Txt_Consignee_AddressLine1.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Address Line 1";
            scm_DDAddress.SetFocus(Txt_Consignee_AddressLine1);
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
        string ConsigneeName = Request.QueryString["ConsigneeName"].ToString();

        if (!IsPostBack)
        {
            pc.Txtbox_Add_Attributes(this.Controls);
            lbl_Consignee_Name_Value.Text = ConsigneeName;
            Txt_Consignee_AddressLine1.Text = Request.QueryString["DDAddressLine1"].ToString();
            Txt_Consignee_AddressLine2.Text = Request.QueryString["DDAddressLine2"].ToString();
        }
    }
   
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            string _Msg;
            _Msg = "Saved SuccessFully";

            String popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();UpdateConsigneeDDAddress('" + Txt_Consignee_AddressLine1.Text.Trim() + "','" + Txt_Consignee_AddressLine2.Text.Trim() + "');</script>";
            ScriptManager.RegisterStartupScript(UpdatePanel2, typeof(String), "PopupScript1", popupScript.ToString(), false);
        }
    }
    
    protected void btn_Exit_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}
