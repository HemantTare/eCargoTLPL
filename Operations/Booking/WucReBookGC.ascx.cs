
using System;
using System.Data;
using System.Web.UI.WebControls;
using ClassLibraryMVP;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
using Raj.EC;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Author        : Dinesh Mahajan
/// Created On    : 27/01/2009
/// Description   : ReBook GC
/// </summary>
/// 

public partial class Operations_Booking_WucReBookGC : System.Web.UI.UserControl, IReBookGCView
{
    #region ClassVariables
    ReBookGCPresenter objReBookGCPresenter;
    Common ObjCommon = new Raj.EC.Common();
    #endregion

    #region ControlsValues

    public Boolean Is_ReBookGC_Octroi_Updated
    {
        set{hdn_Is_ReBook_GC_Octroi_Updated.Value = Convert.ToString(value);}
        get{ return Convert.ToBoolean(hdn_Is_ReBook_GC_Octroi_Updated.Value);}
    }
    public Boolean Is_ReBookGC_Octroi_Applicable
    {
        set{hdn_Is_ReBook_GC_Octroi_Applicable.Value = Convert.ToString(value); }
        get{ return Convert.ToBoolean(hdn_Is_ReBook_GC_Octroi_Applicable.Value);}
    }
    public String No_For_Padd
    {
        set{ hdn_No_For_Padd.Value = value; }
        get{return hdn_No_For_Padd.Value; }
    }    
    public String GC_No_For_Print
    {
        set{ txt_GC_No_For_Print.Text = value; }
        get{ txt_GC_No_For_Print.Text = txt_GC_No_For_Print.Text.Trim() == string.Empty ? "0" : txt_GC_No_For_Print.Text.Trim();

           return Convert.ToInt32(txt_GC_No_For_Print.Text.Trim()).ToString(No_For_Padd); 
        }
    }
    public int GC_No_Length
    {
        set{hdn_GC_No_Length.Value = Util.Int2String(value); }
        get{return ValueOfHdn_Int(hdn_GC_No_Length); }
    }  
    public int ReBook_GC_Id
    {
        get { return Util.String2Int(hdn_ReBook_GC_ID.Value.Trim()); }
        set { hdn_ReBook_GC_ID.Value  = Util.Int2String(value); }
    }
    #endregion
    
    #region IView
    public bool validateUI()
    {
        errorMessage = "";
        bool Is_Valid = false;
        btn_Go.Enabled = Is_Valid;

        txt_GC_No_For_Print.Text = txt_GC_No_For_Print.Text.Trim() == string.Empty ? "0" : txt_GC_No_For_Print.Text.Trim();
        txt_GC_No_For_Print.Text = Convert.ToInt32(txt_GC_No_For_Print.Text.Trim()).ToString(No_For_Padd);

        if (txt_GC_No_For_Print.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter "  + CompanyManager.getCompanyParam().GcCaption + " No.";
        }
        else if (txt_GC_No_For_Print.Text.Trim().Length < Util.String2Int(No_For_Padd))
        {
            errorMessage = CompanyManager.getCompanyParam().GcCaption +  " Number Should have " + No_For_Padd + " Digits Only.";
        }
        else
        {
            Is_Valid = true;
            errorMessage = "";
        }

        if (Is_Valid)
        {
            Is_Valid = false;
            Allow_To_ReBook();

            if (Allow_To_ReBook()== true && Is_ReBookGC_Octroi_Applicable != Is_ReBookGC_Octroi_Updated)
            {
                errorMessage = "Please Update Octroi.";
                txt_GC_No_For_Print.Focus();
                btn_Go.Enabled = Is_Valid;
            }
            else if (Allow_To_ReBook()== false)
            {
                errorMessage = "Can't ReBook.";
                txt_GC_No_For_Print.Focus();
                btn_Go.Enabled = Is_Valid;
            }
            else
            {
                Is_Valid = true;
                errorMessage = "";
                btn_Go.Enabled = true;
            }
        }        
        return Is_Valid;
    }

    public Int32 ValueOfHdn_Int(HiddenField H)
    {
        return Util.String2Int(H.Value.Trim() == string.Empty ? "0" : H.Value.Trim());
    }

    public void Get_No_To_Padd()
    {
        string No_To_Padd = "";
        int i;
        No_For_Padd = "";

        for (i = 0; i < GC_No_Length; i++)
        {
            No_To_Padd = No_To_Padd + "0";
        }
        No_For_Padd = No_To_Padd;
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get{ return Util.DecryptToInt(Request.QueryString["Id"]);}
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        objReBookGCPresenter = new ReBookGCPresenter(this, IsPostBack);
        ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Go);

        if (!IsPostBack)
        {
            txt_GC_No_For_Print.MaxLength = GC_No_Length;
            StateManager.SaveState("QueryString", "2");
           ObjCommon.CheckFormRights(btn_Go );
        }

         SetStandardCaption();
         Get_No_To_Padd();
    }

    private void SetStandardCaption()
    {
        lbl_Heading.Text = " ReBook " + CompanyManager.getCompanyParam().GcCaption;
        lbl_GCNoFrom.Text = CompanyManager.getCompanyParam().GcCaption + "  No :";
    }

    public bool Allow_To_ReBook()
    {
       return objReBookGCPresenter.Allow_To_ReBook();
    } 

    protected void btn_Go_Click(object sender, EventArgs e)
    {
        if (validateUI() == true)
        {
            StateManager.SaveState("QueryString", "2");

            string Path = "";

            if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "nandwana")
            {
                Path = ObjCommon.getBaseURL() + "/" +
                             "Operations/Booking/FrmShortGC.aspx?Menu_Item_Id=MQA4ADQA&Mode=MQA=&ReBook_GC_ID=" +
                             ReBook_GC_Id + "&Rectification_GC_ID=0&ReBook_GC_No_For_Print="+ GC_No_For_Print +
                             "&Rectification_GC_No_For_Print=0&Id=" + ReBook_GC_Id;

            }
            else
            {
                Path = ObjCommon.getBaseURL() + "/" +
                             "Operations/Booking/FrmGC.aspx?Menu_Item_Id=MQA4ADQA&Mode=MQA=&ReBook_GC_ID=" +
                             ReBook_GC_Id + "&Rectification_GC_ID=0&ReBook_GC_No_For_Print=" + GC_No_For_Print +
                             "&Rectification_GC_No_For_Print=0&Id=" + ReBook_GC_Id;
            }

            String popupScript = "<script language='javascript'>GC_Details('" + Path + "');</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel2, typeof(String), "PopupScript1", popupScript.ToString(), false);
        }
    }

    protected void btn_ValidateGCNo_Click(object sender, EventArgs e)
    {
        validateUI();        
    }     
}
