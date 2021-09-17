using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Text;


public partial class Operations_GTLB_Loading_Frm_Rpt_GTLB_Loading_FORM_5 : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds,dsMonth,dsYear;
  
    DAL objDAL = new DAL();
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            Fill_AllDropDown(); 
            Fill_Year();
        }
    }



    private void Fill_AllDropDown()
    {
        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@MonthID", SqlDbType.Int,0,0) 
        };

        objDAL.RunProc("EC_Master_Get_Finacial_Month", objSqlParam, ref dsMonth);

        ddl_Month.DataSource = dsMonth;
        ddl_Month.DataTextField = "MonthName";
        ddl_Month.DataValueField = "MonthID";
        ddl_Month.DataBind();

    }

    private void Fill_Year()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@YearID", SqlDbType.Int,0,0) 
        };

        objDAL.RunProc("EC_Master_Get_Year", objSqlParam, ref dsYear);

        ddl_Year.DataSource = dsYear;
        ddl_Year.DataTextField = "YearName";
        ddl_Year.DataValueField = "YearID";
        ddl_Year.DataBind();
    }

    
    protected void btn_Print_Click(object sender, EventArgs e)
    {
        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/");

        Path.Append("Operations/GTLB Loading/Frm_Rpt_GTLB_Loading_FORM_5_Viewer.aspx?Month=" + ddl_Month.SelectedValue + "&Year=" + ddl_Year.SelectedValue);

        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("Open_FORM5_Window('" + Path + "')");
        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());

    }



   
    
    #endregion 

    public void ClearVariables()
    {
        ds = null;
    }

   
}
