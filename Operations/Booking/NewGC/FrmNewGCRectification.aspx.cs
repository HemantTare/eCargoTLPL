using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;

public partial class Operations_Booking_NewGC_FrmNewGCRectification : System.Web.UI.Page
{
    Common ObjCommon = new Raj.EC.Common();
    private DAL objDAL = new DAL();
    private DataSet objDS;

    public string No_For_Padd
    {
        set { hdn_No_For_Padd.Value = value; }
        get { return hdn_No_For_Padd.Value.Trim(); }
    }
    public string GC_No_For_Print
    {
        set { txt_GC_No_For_Print.Text = value; }
        get { return txt_GC_No_For_Print.Text == string.Empty ? "0" : txt_GC_No_For_Print.Text.Trim(); }
    }
    public int GC_No_Length
    {
        set { hdn_GC_No_Length.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_GC_No_Length.Value); }
    }
    //public int Rectification_GC_Id
    //{
    //    set { hdn_Rectification_GC_ID.Value = Util.Int2String(value); }
    //    get { return Util.String2Int(hdn_Rectification_GC_ID.Value); }
    //}
    public string Rectification_GC_Id
    {
        set { hdn_Rectification_GC_ID.Value = value; }
        get { return hdn_Rectification_GC_ID.Value; }
    }
    public bool validateUI()
    {
        errorMessage = "";
        bool Is_Valid = false;

        txt_GC_No_For_Print.Text = txt_GC_No_For_Print.Text.Trim() == string.Empty ? "0" : txt_GC_No_For_Print.Text.Trim();
        txt_GC_No_For_Print.Text = Convert.ToInt32(txt_GC_No_For_Print.Text.Trim()).ToString(No_For_Padd); 

        if (txt_GC_No_For_Print.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter " + CompanyManager.getCompanyParam().GcCaption + " No.";
            txt_GC_No_For_Print.Focus();
        }
        //else if (txt_GC_No_For_Print.Text.Trim().Length < GC_No_Length)
        //{
        //    errorMessage = CompanyManager.getCompanyParam().GcCaption + " Number Should have " + No_For_Padd + " Digits Only.";
        //    txt_GC_No_For_Print.Focus();
        //}
        else if (Util.String2Int(GC_No_For_Print) <= 0)
        {
            errorMessage = "Please Enter Valid " + CompanyManager.getCompanyParam().GcCaption + " No.";
            txt_GC_No_For_Print.Focus();
        }
        else if (Allow_To_Rectify() == false)
        {
            txt_GC_No_For_Print.Focus();
        }
        else
        {
            Is_Valid = true;
        }

        btn_Go.Enabled = Is_Valid;
        return Is_Valid;
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
   
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Go);

        if (!IsPostBack)
        {
            Get_Company_GC_Parameter();
            txt_GC_No_For_Print.MaxLength = GC_No_Length;
            ObjCommon.CheckFormRights(btn_Go);
        }

        SetStandardCaption();
        Get_No_To_Padd();
    }

    private void SetStandardCaption()
    {
        lbl_Heading.Text = CompanyManager.getCompanyParam().GcCaption + " Rectification ";
        lbl_GCNoFrom.Text = CompanyManager.getCompanyParam().GcCaption + "  No :";
    } 

    protected void btn_Go_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            string Path = ObjCommon.getBaseURL() + "/Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=" + Util.EncryptInteger(Common.GetMenuItemId()) + "&Mode=MgA=&Id=" + Rectification_GC_Id;

            String popupScript = "<script language='javascript'>OPenGCPopUpRectify('" + Path + "');</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel2, typeof(String), "PopupScript1", popupScript.ToString(), false);
        }
    }

    private void Get_Company_GC_Parameter()
    {
        objDAL.RunProc("Get_Company_GC_Parameter", ref objDS);
        GC_No_Length = Util.String2Int(objDS.Tables[0].Rows[0]["GC_No_Length"].ToString());
    }

    public bool Allow_To_Rectify()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                                        objDAL.MakeOutParams("@Is_Allow_To_Rectify", SqlDbType.Bit,0),
                                        objDAL.MakeOutParams("@Rectification_GC_Id", SqlDbType.Int,0),
                                        objDAL.MakeInParams("@GC_No_For_Print", SqlDbType.VarChar,20,GC_No_For_Print),
                                        objDAL.MakeInParams("@Branch_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId),
                                        objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,5,UserManager.getUserParam().HierarchyCode),
                                        objDAL.MakeInParams("@year_code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
                                        objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,UserManager.getUserParam().DivisionId)};

        objDAL.RunProc("Ec_Opr_GC_Allow_To_Rectify", objSqlParam, ref objDS);

        errorMessage = objSqlParam[0].Value.ToString();
        Rectification_GC_Id = Util.EncryptInteger(Util.String2Int(objSqlParam[2].Value.ToString()));

        return Util.String2Bool(objSqlParam[1].Value.ToString());
    }

    protected void btn_ValidateGCNo_Click(object sender, EventArgs e)
    {
        validateUI();
    }
}
