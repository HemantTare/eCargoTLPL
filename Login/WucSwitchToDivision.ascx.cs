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
using ClassLibraryMVP.Security;
using System.Data.SqlClient;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP;
using Raj.EC;



public partial class Login_WucSwitchToDivision : System.Web.UI.UserControl
{
    #region ClassVaraibles
    private DAL objDAL = new DAL();
    DataSet objDS = new DataSet();
    string Query,Query1;
    Common objCommon = new Common();


    #endregion

    #region ControlsValue

    public int DivisionId
    {
        get { return Util.String2Int(ddl_SwitchDivision.SelectedValue); }
        set { ddl_SwitchDivision.SelectedValue = Util.Int2String(value); }
    }   
    #endregion

    #region ControlsBind

    public DataSet BindDivision
    {
        set
        {
            ddl_SwitchDivision.DataSource = value;
            ddl_SwitchDivision.DataTextField = "Division_Name";
            ddl_SwitchDivision.DataValueField = "Division_Id";
            ddl_SwitchDivision.DataBind();

            ddl_SwitchDivision.Items.Insert(0, new ListItem("Select One", "0"));

           

        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
   
        if (!IsPostBack) 
        {
            if (UserManager.getUserParam().IsDivisionReq == true)
            {
                 tr_SwitchDivision.Visible = true;              
                    SqlParameter[] param ={                                
                                  objDAL.MakeInParams("@UserId",SqlDbType.Int,0,UserManager.getUserParam().UserId),
                                  objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,UserManager.getUserParam().DivisionId)                               
                               };

                    objDAL.RunProc("[dbo].[EC_Adm_SwitchDivision]", param, ref objDS);

                    if (objDS.Tables[0].Rows.Count > 0)
                    {
                        BindDivision = objDS;
                    }
                    else
                    {
                        tr_SwitchDivision.Visible = false; 
                    }
          
            }
            else
            {
                tr_SwitchDivision.Visible = false;
             }
            
        }

    }
    protected void ddl_SwitchDivision_SelectedIndexChanged(object sender, EventArgs e)
    {

        string username = UserManager.getUserParam().UserName;
        string userpassword = Session["PWD"].ToString();
        userpassword = Util.EncryptString(userpassword);

        System.Web.HttpContext.Current.Session.RemoveAll();
        System.Web.HttpContext.Current.Session.Abandon();

        

        Response.Redirect("~/FrmLogin.aspx?SwitchTo=" + ddl_SwitchDivision.SelectedValue + "&username=" +username+ "&userpassword=" + userpassword);

    }
}

