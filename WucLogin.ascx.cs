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
using Raj.EC.LoginView;
using Raj.EC.LoginPresenter;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess ;
using System.Data.SqlClient;

public partial class WucLogin : System.Web.UI.UserControl,ILoginView
{
    private DataSet _ds;
    private cLoginPresenter _loginPresenter;
    #region initInterface
    
    public int YearCode
    {
        get { return Util.String2Int(ddl_Year.SelectedValue); }
    }

    public int DivisionId
    {
        get { return Util.String2Int(ddl_Division.SelectedValue); }
    }

    public string UserName
    {
        get { return (txt_login.Text.Trim()); }
    }

    public string Password
    {
        get {return (txt_password.Text.Trim());}
    }
    public string Mac_Id
    {
        get { return myCPUid.Value; }
    }
    public string ErrorMessage
    {
        set{lbl_Error_Message.Text = value;}
        get { return (lbl_Error_Message.Text.Trim()); }
    }

    public string ErrorMessageWrongdivisionLogin
    {
        get { return (GetLocalResourceObject("Msg_WrongdivisionLogin").ToString()); }
    }

    public bool IsDivisionReq
    {
        set {TRDivision.Visible = value;}

    }

    public DataTable BindDivisionDdl
    {
        set
        {
            ddl_Division.DataSource = value;
            ddl_Division.DataTextField = "Division_Name";
            ddl_Division.DataValueField = "Division_ID";
            ddl_Division.DataBind();
        }
    }

    public DataSet BindYearDdl
    {
        set
        {
            ddl_Year.DataSource = value;
            ddl_Year.DataTextField = "Year";
            ddl_Year.DataValueField = "Year_Code";
            ddl_Year.DataBind();
        }
    }
    #endregion
    protected void btn_login_Click(object sender, EventArgs e)
    {


         ErrorMessage = "";
         if (Session["UserParam"] == null)
         {
             _ds = _loginPresenter.GetLoginDetails();

             if (_ds.Tables.Count > 0 && ErrorMessage == "")
             {
                 Session["UID"] = txt_login.Text.Trim();
                 Session["PWD"] = txt_password.Text.Trim();

                 DataRow dr_user_details = _ds.Tables[0].Rows[0];
                 DataRow dr_company_details = _ds.Tables[1].Rows[0];

                 UserManager.InitUser(
                    Util.String2Int(dr_user_details["User_ID"].ToString()),
                     dr_user_details["Hierarchy_Code"].ToString(),
                     Util.String2Int(dr_user_details["Main_Id"].ToString()),
                     Util.String2Int(dr_user_details["Profile_Id"].ToString()),
                     Convert.ToDateTime(dr_user_details["Start_Date"].ToString()),
                     Convert.ToDateTime(dr_user_details["End_Date"].ToString()),
                     Convert.ToDateTime(dr_user_details["Bkg_Start_Time"].ToString()),
                     Convert.ToDateTime(dr_user_details["Bkg_End_Time"].ToString()),
                     Util.String2Bool(dr_user_details["AllowBooking"].ToString()),
                     dr_user_details["User_Name"].ToString(),
                     Util.String2Int(dr_user_details["Year_Code"].ToString()),
                     Util.String2Int(dr_user_details["Division_Id"].ToString()),
                     1,
                     dr_user_details["First_Name"].ToString(),
                     dr_user_details["Last_Name"].ToString(),
                     Util.String2Bool(dr_company_details["Is_Activate_Divisions"].ToString()),
                     Util.String2Bool(dr_company_details["Is_Account_Transfer_Required"].ToString()),
                     Util.String2Bool(dr_company_details["Is_Co_Loader_Business"].ToString()),
                     Util.String2Bool(dr_company_details["Is_Memo_Series_Required"].ToString()),
                     Util.String2Bool(dr_company_details["Is_LHPO_Series_Required"].ToString()),
                     Util.String2Bool(dr_user_details["Is_CSA"].ToString()),
                     Util.String2Int(dr_company_details["Standard_Basic_Freight_Unit_ID"].ToString()),
                     Util.String2Decimal(dr_company_details["Standard_Freight_Rate_Per"].ToString()),
                     dr_user_details["Main_Name"].ToString(),
                     dr_company_details["Company_Name"].ToString(),
                     dr_company_details["GC_Caption"].ToString(),
                     dr_company_details["LHPO_Caption"].ToString(),
                     dr_user_details["Division_Name"].ToString(),
                     Convert.ToDateTime(dr_user_details["TodaysDate"].ToString()),
                     dr_user_details["LoginTime"].ToString()
                     );

                 if (Request.QueryString["username"] == null)
                 {
                     String Machine_IP = Request.ServerVariables["REMOTE_ADDR"];
                     DAL objDAL = new DAL();

                     SqlParameter[] param ={ objDAL.MakeInParams("@Actual_Logger", SqlDbType.Int, 0, UserManager.getUserParam().UserId),
                                            objDAL.MakeInParams("@Logged_in_As",SqlDbType.Int,0,UserManager.getUserParam().UserId),
                                            objDAL.MakeInParams("@division_id",SqlDbType.Int,0,Util.String2Int(ddl_Division.SelectedValue)),
                                            objDAL.MakeInParams("@Machine_IP",SqlDbType.VarChar,50,Machine_IP),
                                            objDAL.MakeInParams("@Is_login", SqlDbType.Bit, 0, true)};

                     objDAL.RunProc("[dbo].[Ec_Master_LoginAs_Add]", param);
                 }

                 if (UserName.ToUpper() == Password.ToUpper())
                     Response.Redirect("~/Login/frmLoginChangePwd.aspx");
                 else
                     Response.Redirect("~/Display/frmDisplay.aspx");
             }
         }
         else
             ErrorMessage = "Sorry!! Session already exist, You can not relogin.";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ErrorMessage = "";
        
        _loginPresenter = new cLoginPresenter(this, IsPostBack);
        
        if (TRDivision.Visible == true)
        {
            ImgLogin.Height = 212;
            ddl_Division.Focus();
        }
        else
        {
            ImgLogin.Height = 190;
            ddl_Year.Focus();
        }

        if (Request.QueryString["username"] != null)
        {
            ddl_Division.SelectedValue = Request.QueryString["SwitchTo"];
            txt_login.Text = Request.QueryString["username"];
            txt_password.Text = Util.DecryptToString(Request.QueryString["userpassword"]);
            ddl_Year.SelectedValue = Request.QueryString["useryearcode"];
            myCPUid.Value = Request.QueryString["macid"];
            btn_login_Click(sender, e);
        }
        //if (IsDebug)
        //{
        //    txt_login.Text = "BO0001";
        //    txt_password.Text = "123";
        //}

        
        //btn_login_Click(sender, e);
    }
}
