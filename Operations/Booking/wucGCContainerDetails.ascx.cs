
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
public partial class Operations_Booking_wucGCContainerDetails : System.Web.UI.UserControl 
{    
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
   
    public string ContainerDetailsErrorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public Int32 Session_ContainerTypeId
    {
        get { return StateManager.GetState<Int32>("ContainerTypeId"); }
        set { StateManager.SaveState("ContainerTypeId", value); }
    }
    
    public String  Session_ContainerNoPart1 
    {
        get { return StateManager.GetState<String>("ContainerNoPart1"); }
        set { StateManager.SaveState("ContainerNoPart1", value); }
    }
    
    public String Session_ContainerNoPart2
    {
        get { return StateManager.GetState<String>("ContainerNoPart2"); }
        set { StateManager.SaveState("ContainerNoPart2", value); }
    }

    public String Session_SealNo
    {
        get { return StateManager.GetState<String>("SealNo"); }
        set { StateManager.SaveState("SealNo", value); }
    }

    public Int32  Session_ReturnToYardId
    {
        get { return StateManager.GetState<Int32>("ReturnToYardId"); }
        set { StateManager.SaveState("ReturnToYardId", value); }
    }

    public String Session_ReturnToYardName
    {
        get { return StateManager.GetState<String>("ReturnToYardName"); }
        set { StateManager.SaveState("ReturnToYardName", value); }
    }

    public String Session_NFormNo
    {
        get { return StateManager.GetState<String>("NFormNo"); }
        set { StateManager.SaveState("NFormNo", value); }
    }

    public DataTable Session_ContainerType
    {
        get { return StateManager.GetState<DataTable>("ContainerType"); }
        set { StateManager.SaveState("ContainerType", value); }
    }
 
    #endregion
    #region Function
    
    public bool validateUI()
    {
        bool isValid = false;

        errorMessage = "";

        if (Util.String2Int( ddl_ContainerType.SelectedValue ) <=0)
        {
            errorMessage = "Please Select Container Type.";
            SM_GCContainerDetails.SetFocus(ddl_ContainerType); 
        }
        else if (txt_ContainerNoPart1.Text.Trim() == String.Empty)
        {
            errorMessage = "Please Enter Container No.";
            SM_GCContainerDetails.SetFocus(txt_ContainerNoPart1); 
        }
        else if (txt_ContainerNoPart1.Text.Trim().Length < 4 )
        {
            errorMessage = "Container No. Should Have 4 Character Only.";
            SM_GCContainerDetails.SetFocus(txt_ContainerNoPart1);
        }
        else if (txt_ContainerNoPart2.Text.Trim() == String.Empty)
        {
            errorMessage = "Please Enter Container No.";
            SM_GCContainerDetails.SetFocus(txt_ContainerNoPart2); 
        }
        else if (txt_ContainerNoPart2.Text.Trim().Length < 7)
        {
            errorMessage = "Container No. Should Have 7 Digit Only.";
            SM_GCContainerDetails.SetFocus(txt_ContainerNoPart2);
        }
        else if (txt_SealNo.Text.Trim() == String.Empty)
        {
            errorMessage = "Please Enter Seal No.";
            SM_GCContainerDetails.SetFocus(txt_SealNo); 
        }
        else if (txt_SealNo.Text.Trim().Length < 6)
        {
            errorMessage = "Seal No. Should Have 6 Character Only.";
            SM_GCContainerDetails.SetFocus(txt_SealNo);
        }
        else if (Util.String2Int(ddl_ReturnToYard.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Return To Yard.";
            SM_GCContainerDetails.SetFocus(ddl_ReturnToYard ); 
        }
        else if (txt_NFormNo.Text.Trim() == String.Empty)
        {
            errorMessage = "Please Enter N Form No.";
            SM_GCContainerDetails.SetFocus(txt_NFormNo); 
        }
        else
        {
            isValid = true;
        }
        return isValid;
    }
     #endregion

    public void Bind_ContainerType()
    {
        ddl_ContainerType.DataSource = Session_ContainerType;

        ddl_ContainerType.DataTextField = "Container_Type";
        ddl_ContainerType.DataValueField = "Container_Type_ID";
        ddl_ContainerType.DataBind();

        ddl_ContainerType.Items.Insert(0, new ListItem("Select One", "0"));
    }

    public void SetReturnToYard(string text, string value)
    {
        Common.SetValueToDDLSearch(text, value, ddl_ReturnToYard );
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        hdn_Mode.Value = Request.QueryString["Mode"].ToString();


        ddl_ReturnToYard.DataTextField = "From_Location_Name";
        ddl_ReturnToYard.DataValueField = "From_Location_ID";

        ddl_ReturnToYard.OtherColumns = "0";

        if (!IsPostBack)
        {
            Bind_ContainerType();
            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();

            sbValid.Append("if (Allow_To_Exit() == false) { return false; }");
            sbValid.Append(Page.ClientScript.GetPostBackEventReference(btn_Exit, ""));
            sbValid.Append(";");
            btn_Exit.Attributes.Add("onclick", sbValid.ToString());            
        }

        if (!IsPostBack)
        {
            if (StateManager.IsValidSession("ContainerTypeId"))
            {
                ddl_ContainerType.SelectedValue = Util.Int2String(Session_ContainerTypeId);//= Util.String2Int(ddl_ContainerType.SelectedValue);
                txt_ContainerNoPart1.Text = Session_ContainerNoPart1 ;
                txt_ContainerNoPart2.Text = Session_ContainerNoPart2 ;
                txt_SealNo.Text = Session_SealNo;
                txt_NFormNo.Text = Session_NFormNo;

                SetReturnToYard(Session_ReturnToYardName, Session_ReturnToYardId.ToString());
            }
            else
            {
                txt_ContainerNoPart1.Text = "";
                txt_ContainerNoPart2.Text = "";
                txt_SealNo.Text = "";
                txt_NFormNo.Text = "";
                SetReturnToYard("", "0");
            }
        }
        SetStandardCaption();
        //Response.Expires = 0;
    }
    
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Session_ContainerTypeId = Util.String2Int(ddl_ContainerType.SelectedValue);  
            Session_ContainerNoPart1 = txt_ContainerNoPart1.Text.Trim();
            Session_ContainerNoPart2 = txt_ContainerNoPart2.Text.Trim();
            Session_SealNo = txt_SealNo.Text.Trim();

            Session_ReturnToYardId = Util.String2Int( ddl_ReturnToYard.SelectedValue);
            Session_NFormNo = txt_NFormNo.Text.Trim();
            Session_ReturnToYardName = ddl_ReturnToYard.SelectedText;
            
            string _Msg;
            _Msg = "Saved SuccessFully";

            String popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();</script>";

            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel2, typeof(String), "PopupScript1", popupScript.ToString(), false);
        }
    }

    protected void ddl_ReturnToYard_TxtChange(object sender, EventArgs e)
    {
        hdn_ReturnToYardId.Value = ddl_ReturnToYard.SelectedValue;
        ddl_ReturnToYard.OtherColumns = "0";
        
        SM_GCContainerDetails.SetFocus(ddl_ReturnToYard);
    }

    protected void btn_Exit_Click(object sender, EventArgs e)
    {     
        Response.Write("<script language='javascript'> { self.close() }</script>");
    }

    private void SetStandardCaption()
    {
        lbl_ContainerDetails.Text = " Container Details";
    }
}
