using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;

//Create : Ankit champaneriya
//Date   : 05/12/08
//Desc   : Contains only Excess Details.

public partial class Operations_ShortExcess_WucAddExcessDetails : System.Web.UI.UserControl
{
    #region Declaration
    private DAL objDAL = new DAL();
    #endregion


    public IAUSExcessDetailsView AUSExcessDetailsView
    {
        get { return (IAUSExcessDetailsView)WucAUSExcessDetails1; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        Save();
    }
    //Added : Anita On : 19 Feb 09
    public void ClearVariables()
    {
        WucAUSExcessDetails1.SessionBindCommodityDropdown = null;
        WucAUSExcessDetails1.SessionBindExcessGrid = null;
        WucAUSExcessDetails1.SessionBindPackingTypeDropdown = null;
        WucAUSExcessDetails1.SessionBindItemDropdown = null;
    }

    protected void btn_null_session_Click(object sender, EventArgs e) //added :Ankit : 21-02-09
    {
        ClearVariables();
    }

    private Message Save()
    {
        Message objMessage = new Message();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@Login_Branch_ID",SqlDbType.Int,0, UserManager.getUserParam().MainId  ),
            objDAL.MakeInParams("@Excess_Details_Xml",SqlDbType.Xml,0, AUSExcessDetailsView.Excess_Details_Xml),
            };
        if (AUSExcessDetailsView.TotalExcessArticles != 0)
        {
            objDAL.RunProc("EC_Opr_Excess_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
        }
        else
        {
            lbl_Errors.Text = "Please mention atleast one record";
        }
        return objMessage;
    }
}