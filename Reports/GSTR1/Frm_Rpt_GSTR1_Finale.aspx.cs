using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;


public partial class Reports_GSTR1_Frm_Rpt_GSTR1_Finale : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal Total_B2B, Total_B2C, Total_Exempted; 

    public DateTime FromDate
    {
        set
        {
            Wuc_From_To_Datepicker1.SelectedFromDate = value;
        }
        get
        {
            return Wuc_From_To_Datepicker1.SelectedFromDate;
        }
    }

    public DateTime ToDate
    {
        set
        {
            Wuc_From_To_Datepicker1.SelectedToDate = value;

        }
        get { return Wuc_From_To_Datepicker1.SelectedToDate; }
    }
    #endregion



    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {
            Fill_State();
        }

    }

    private void Fill_State()
    {
        DAL objDAL = new DAL();

        DataSet dsState = new DataSet();

        objDAL.RunProc("EC_RPT_GSTR1_Fill_State", ref dsState);

        ddl_State.DataSource = dsState;
        ddl_State.DataTextField = "State_Name";
        ddl_State.DataValueField = "State_ID";
        ddl_State.DataBind();
    }

    private void GetDetails_B2B()
    {
        DAL objDAL = new DAL();


        int grid_currentpageindex = 0;
        int grid_PageSize = 0;



        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@StateID", SqlDbType.Int, 0,ddl_State.SelectedValue),
            objDAL.MakeInParams("@FromDate",SqlDbType.DateTime,0,FromDate),
            objDAL.MakeInParams("@ToDate",SqlDbType.DateTime,0,ToDate),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex ),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("EC_RPT_GSTR1_B2B_StateWise", objSqlParam, ref ds);

    }

    private void GetDetails_B2C()
    {
        DAL objDAL = new DAL();


        int grid_currentpageindex = 0;
        int grid_PageSize = 0;



        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@StateID", SqlDbType.Int, 0,ddl_State.SelectedValue),
            objDAL.MakeInParams("@FromDate",SqlDbType.DateTime,0,FromDate),
            objDAL.MakeInParams("@ToDate",SqlDbType.DateTime,0,ToDate),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex ),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("EC_RPT_GSTR1_B2C_StateWise", objSqlParam, ref ds);

    }

    private void GetDetails_Exempt()
    {
        DAL objDAL = new DAL();


        int grid_currentpageindex = 0;
        int grid_PageSize = 0;



        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@StateID", SqlDbType.Int, 0,ddl_State.SelectedValue),
            objDAL.MakeInParams("@FromDate",SqlDbType.DateTime,0,FromDate),
            objDAL.MakeInParams("@ToDate",SqlDbType.DateTime,0,ToDate),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex ),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("EC_RPT_GSTR1_Exempted_StateWise", objSqlParam, ref ds);

    }

    private void GetDetails_HSN()
    {
        DAL objDAL = new DAL();


        int grid_currentpageindex = 0;
        int grid_PageSize = 0;



        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@StateID", SqlDbType.Int, 0,ddl_State.SelectedValue),
            objDAL.MakeInParams("@FromDate",SqlDbType.DateTime,0,FromDate),
            objDAL.MakeInParams("@ToDate",SqlDbType.DateTime,0,ToDate),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex ),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("EC_RPT_GSTR1_HSN_StateWise", objSqlParam, ref ds);

    }

    private void GetDetails_Series()
    {
        DAL objDAL = new DAL();


        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@StateID", SqlDbType.Int, 0,ddl_State.SelectedValue),
            objDAL.MakeInParams("@FromDate",SqlDbType.DateTime,0,FromDate),
            objDAL.MakeInParams("@ToDate",SqlDbType.DateTime,0,ToDate)};

        objDAL.RunProc("EC_RPT_GSTR1_Series_StateWise", objSqlParam, ref ds);

    }

    #endregion

    #region Other Function

    protected void btn_ExportToExcelB2B_Click(object sender, EventArgs e)
    {
        GetDetails_B2B();


        DataRow dr = ds.Tables[1].Rows[0];
        Total_B2B = Util.String2Decimal(dr["TotalSubTotal"].ToString());


        DataRow dr1 = ds.Tables[0].NewRow();
        dr1["LR_No"] = "Total : ";
        dr1["SubTotal"] = Total_B2B;

        ds.Tables[0].Rows.Add(dr1);

        Session["ExportToExcel"] = ds.Tables[0].Copy();

        Response.Redirect("~/Finance/Utilities/frm_Infra_Grid_Common_ExportToExcel.aspx");
    }

    protected void btn_ExportToExcelB2C_Click(object sender, EventArgs e)
    {
        GetDetails_B2C();


        DataRow dr = ds.Tables[3].Rows[0];
        Total_B2C = Util.String2Decimal(dr["TotalSubTotal"].ToString());


        DataRow dr1 = ds.Tables[0].NewRow();
        dr1["SupplyType"] = "Total : ";
        dr1["SubTotal"] = Total_B2C;

        ds.Tables[0].Rows.Add(dr1);

        Session["ExportToExcel"] = ds.Tables[0].Copy();

        Response.Redirect("~/Finance/Utilities/frm_Infra_Grid_Common_ExportToExcel.aspx");
    }

    protected void btn_ExportToExcelExempt_Click(object sender, EventArgs e)
    {
        GetDetails_Exempt();


        DataRow dr = ds.Tables[3].Rows[0];
        Total_Exempted = Util.String2Decimal(dr["TotalSubTotal"].ToString());


        DataRow dr1 = ds.Tables[0].NewRow();
        dr1["Particulars"] = "Total : ";
        dr1["Exempted"] = Total_Exempted;

        ds.Tables[0].Rows.Add(dr1);

        Session["ExportToExcel"] = ds.Tables[0].Copy();

        Response.Redirect("~/Finance/Utilities/frm_Infra_Grid_Common_ExportToExcel.aspx");
    }

    protected void btn_ExportToExcelHSN_Click(object sender, EventArgs e)
    {
        GetDetails_HSN();

        Session["ExportToExcel"] = ds.Tables[3].Copy();

        Response.Redirect("~/Finance/Utilities/frm_Infra_Grid_Common_ExportToExcel.aspx");
    }

    protected void btn_ExportToExcelSeries_Click(object sender, EventArgs e)
    {
        GetDetails_Series();

        Session["ExportToExcel"] = ds.Tables[0].Copy();

        Response.Redirect("~/Finance/Utilities/frm_Infra_Grid_Common_ExportToExcel.aspx");
    }
#endregion


}
