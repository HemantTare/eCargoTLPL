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
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;


using Raj.EC.ControlsView;
/// <summary>
/// author Dinesh
/// created on 07 oct 08
/// this is main user control in which other 2 user control of PetrolPump are kept
/// </summary>
/// 
public partial class EC_Master_WucPetrolPump : System.Web.UI.UserControl, IPetrolPumpView
{
    PetrolPumpPresenter objPetrolPumpPresenter;
    PageControls pc = new PageControls();

    #region Validation
    public bool validateUI()
    {
        //Boolean IsValid;
        //IsValid = false;

        //IsValid = ValidateBeforeSave();
        //return IsValid;

        bool IsValid = false;

        if (WucPetrolPumpGeneral1.validateUI() == false)
        {
            MP_PetrolPump.SelectedIndex = 0;
            TB_PetrolPump.SelectedTab = TB_PetrolPump.Tabs[0];
            //MP_PetrolPump.GoFirst();
            //MP_PetrolPump.GoFirst();
        }
        else if (WucPetrolPumpFinanceDetails1.validateUI() == false)
        {
            MP_PetrolPump.SelectedIndex = 1;
            TB_PetrolPump.SelectedTab = TB_PetrolPump.Tabs[1];
            //MP_PetrolPump.GoLast();
            //MP_PetrolPump.GoLast();

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


    //public Boolean ValidateBeforeSave()
    //{

    //    bool IsValid = false;

    //    if (WucPetrolPumpGeneral1.validateUI() == false)
    //    {
    //        MP_PetrolPump.SelectedIndex = 0;
    //        TB_PetrolPump.SelectedTab = TB_PetrolPump.Tabs[0];
    //    }
    //    else if (WucPetrolPumpFinanceDetails1.validateUI() == false)
    //    {
    //        MP_PetrolPump.SelectedIndex = 1;
    //        TB_PetrolPump.SelectedTab = TB_PetrolPump.Tabs[1];
    //    }
    //    else
    //    {
    //        IsValid = true;
    //    }

    //    return IsValid;
    //}

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

    public IPetrolPumpGeneralView PetrolPumpGeneralView
    {
        get { return (IPetrolPumpGeneralView)WucPetrolPumpGeneral1  ; }
    }

    public IPetrolPumpFinanceDetailsView PetrolPumpFinanceDetailsView 
    {
        get { return (IPetrolPumpFinanceDetailsView  )WucPetrolPumpFinanceDetails1   ; }
    }

    
    #endregion

    #region events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack==false)
            pc.AddAttributes(this.Controls);
       
       TB_PetrolPump.SiteMapXmlFile = "~/XML/PetrolPump.xml";
       
       
        
        objPetrolPumpPresenter = new PetrolPumpPresenter(this, IsPostBack);
        
        //WucAttachments1.AttachmentFormId = 4;
        btn_save_attributes();
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
        //if (ValidateBeforeSave())
        //{
        //    objPetrolPumpPresenter.Save();
        //}
        //else
        //{
        //    btn_Save.Enabled = true;
        //}

        objPetrolPumpPresenter.Save();
    }
    #endregion
}
