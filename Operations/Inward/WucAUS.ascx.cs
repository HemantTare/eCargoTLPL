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

using ClassLibrary;

using ClassLibraryMVP;
using Raj.EC.OperationPresenter  ;
using Raj.EC.OperationView  ;


using Raj.EC.ControlsView;

public partial class Operations_Inward_WucAUS : System.Web.UI.UserControl, IAUSView  
{
    AUSPresenter objAUSPresenter;
    PageControls pc = new PageControls();
    Raj.EC.Common objComm = new Raj.EC.Common();
    string _flag = "";
    string Mode = "0";
    #region Validation
    public bool validateUI()
    {
        //Boolean IsValid;
        //IsValid = false;

        //IsValid = ValidateBeforeSave();
        //return IsValid;

        bool IsValid = false;

        if (WucAUSUnloadingDetails1.validateUI() == false)
        {
            MP_AUS.SelectedIndex = 0;
            TB_AUS.SelectedTab = TB_AUS.Tabs[0];
            
        }
        else if (WucAUSExcessDetails1.validateUI() == false)
        {
            MP_AUS.SelectedIndex = 1;
            TB_AUS.SelectedTab = TB_AUS.Tabs[1];
        }
        else
        {
            IsValid = true;
        }

        if (IsValid == false)
        {
            btn_Save.Enabled = true;
        }

        return IsValid;
    }

    #endregion

    #region IView Implementation
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public string Flag
    {
        get { return _flag; }
    }

    public IAUSUnloadingDetailsView AUSUnloadingDetailsView
    {
        get { return (IAUSUnloadingDetailsView  )WucAUSUnloadingDetails1  ; }
    }

    public IAUSExcessDetailsView AUSExcessDetailsView
    {
        get { return (IAUSExcessDetailsView)WucAUSExcessDetails1  ; }
    }

    #endregion

    #region events
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            btn_Print_Label.Enabled = true;
        }
        if (Mode != "1")
        {
            btn_Print_Label.Visible = true;
        }
        else
        {
            btn_Print_Label.Visible = false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (IsPostBack == false)
            pc.AddAttributes(this.Controls);

        objAUSPresenter = new AUSPresenter  (this, IsPostBack);
        WucAUSExcessDetails1.TotalExcessArticlesChange += new EventHandler( WucAUSUnloadingDetails1.SetTotalExcessDetails   );
        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Save_Print,btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save,btn_Save_Print, btn_Close));
        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Print, btn_Save_Exit, btn_Save, btn_Close));
        WucAUSUnloadingDetails1.SetScriptManager = scm_AUS;

        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
       // btn_save_attributes();
        if (Mode != "1")
        {
            btn_Print_Label.Visible = true; 
        }
        else
        {
            btn_Print_Label.Visible = false;
        }
        
    }
    private void btn_save_attributes()
    {
        System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
        sbValid.Append("if (typeof(Page_ClientValidate) == 'function'){");
        sbValid.Append("if (Allow_to_Save() == false) { return false; }}");
        sbValid.Append("this.value = 'Wait...';");
        sbValid.Append("this.disabled = true;");
        sbValid.Append(Page.ClientScript.GetPostBackEventReference(btn_Save, ""));
        sbValid.Append(";");
        btn_Save.Attributes.Add("onclick", sbValid.ToString());
    }    

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
       objAUSPresenter.Save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        objAUSPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {

        _flag = "SaveAndPrint";
        objAUSPresenter.Save();

    }

    //Added : Anita On : 19 Feb 09
    public void ClearVariables()
    {
        WucAUSUnloadingDetails1.SessionReceivedCondition = null;
        WucAUSUnloadingDetails1.SessionUnloadingDetailsGrid = null;

        WucAUSExcessDetails1.SessionBindCommodityDropdown = null;
        WucAUSExcessDetails1.SessionBindExcessGrid = null;
        WucAUSExcessDetails1.SessionBindPackingTypeDropdown = null;
        WucAUSExcessDetails1.SessionBindItemDropdown = null;        
    }

    #endregion
    protected void btn_Print_Label_Click(object sender, EventArgs e)
    {
        if (Mode != "1")
        {
            objAUSPresenter.AUSPrintLabel();
        }
    }
}
