
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

public partial class Operations_Booking_wucRequireForm : System.Web.UI.UserControl 
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

    #endregion 
    
    #region Controls 
    
    public DataSet Bind_RequireFroms
    {
        set
        {
            lst_RequireForms.DataSource = value;             
            lst_RequireForms.DataTextField = "Form_Name";
            lst_RequireForms.DataValueField = "Form_Id";            
            lst_RequireForms.DataBind();
        }
    }
      
    public DataSet   Session_RequireForms
    {
        get { return StateManager.GetState<DataSet>("RequireForms"); }
        set { StateManager.SaveState("RequireForms", value); }
    }
  
    public String Session_DeliveryBranchName
    {
        get { return StateManager.GetState<String>("DeliveryBranchName"); }
        set { StateManager.SaveState("DeliveryBranchName", value); }
    }
    
    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lst_RequireForms.Visible = true ;
        lbl_RequireForm.Visible = true ;
        lbl_Errors.Visible = false ;

        if (StateManager.IsValidSession("DeliveryBranchName"))
        {
            lbl_Delivery_Branch_Name_Value.Text = Session_DeliveryBranchName;
        }

        if (StateManager.IsValidSession("RequireForms"))
        {
            Bind_RequireFroms = Session_RequireForms;
        }
        if (StateManager.IsValidSession("RequireForms"))
        {
            if (Session_RequireForms.Tables[0].Rows.Count <= 0)
            {
                lbl_RequireForm.Visible = false;
                lst_RequireForms.Visible = false;

                lbl_Errors.Visible = true;
                errorMessage = " No Require Forms Require.";
            }
        }
        else
        {
            lbl_RequireForm.Visible = false;
            lst_RequireForms.Visible = false;

            lbl_Errors.Visible = true;
            errorMessage = " No Require Forms Require.";
        }
        
        if (!IsPostBack)
        {
            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();

            sbValid.Append("if (Allow_To_Exit() == false) { return false; }");
            sbValid.Append(Page.ClientScript.GetPostBackEventReference(btn_Exit, ""));
            sbValid.Append(";");
            btn_Exit.Attributes.Add("onclick", sbValid.ToString());
        }
    }    
     
    protected void btn_Exit_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'> { self.close() }</script>");
    }
}
