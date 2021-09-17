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
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class FrmIncomingTrucksAlert : System.Web.UI.Page
{
    #region ClassVariables
    private DataSet ds;
    private DAL objDAL = new DAL();
    private Decimal LoadedWeight,LoadedWtForUs;
    private int LoadedArticle, LoadedArticleForUs,Count;  
    #endregion

    #region ControlsValue
    public string Is_FromDesktop
    {
        get
        {
            //if (Request.QueryString["IsFromDesktop"] == null)
            //    return "N";
            //else
            //{
                return (Request.QueryString["IsFromDesktop"]);
            //}
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Region_Area_Branch1.AfterLoad += new EventHandler(Wuc_Region_Area_Branch_PostLoad);
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "IncommingTruckAlertsDetails";

        if (IsPostBack == false)
        {         
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);

            BindGrid("form_and_pageload", e);
            WucFilter1.setddldatasource(ds);
        }
    }

    private void Wuc_Region_Area_Branch_PostLoad(object sender, EventArgs e)
    {
        btn_view_Click(sender, e);
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_LoadedArticles,lbl_LoadedWeight,lbl_Total,lbl_TotalCountFrom;
            System.Web.UI.WebControls.Label lbl_LoadedArticleForUs, lbl_LoadedWtForUs,lbl_TotalCountTo;

            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_LoadedArticles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_LoadedArticles");
            lbl_LoadedWeight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_LoadedWeight");
            lbl_LoadedArticleForUs = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_LoadedArticleForUs");
            lbl_LoadedWtForUs = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_LoadedWtForUs");
            lbl_TotalCountFrom = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalCountFrom");
            lbl_TotalCountTo = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalCountTo");

            lbl_Total.Text = "Total";
            lbl_LoadedArticles.Text = LoadedArticle.ToString();
            lbl_LoadedWeight.Text = LoadedWeight.ToString();
            lbl_LoadedArticleForUs.Text = LoadedArticleForUs.ToString();
            lbl_LoadedWtForUs.Text = LoadedWtForUs.ToString();
            lbl_TotalCountFrom.Text = Count.ToString();
            lbl_TotalCountTo.Text = Count.ToString();

        }
    }    
  
    
      protected void btn_view_Click(object sender, EventArgs e)
    {       
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);
       
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        LoadedArticle = Util.String2Int(dr["Loaded Articles"].ToString());
        LoadedWeight = Util.String2Decimal(dr["Loaded Weight"].ToString());
        LoadedArticleForUs = Util.String2Int(dr["Loaded Articles For Us"].ToString());
        LoadedWtForUs = Util.String2Decimal(dr["Loaded Weight For Us"].ToString());
        Count = Util.String2Int(dr["Total"].ToString());
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);
        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        int RegionId = Wuc_Region_Area_Branch1.RegionID;
        int AreaId = Wuc_Region_Area_Branch1.AreaID;
        int BranchId = Wuc_Region_Area_Branch1.BranchID;


        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@Region_ID",SqlDbType.Int,0,RegionId),   
            objDAL.MakeInParams("@Area_ID",SqlDbType.Int,0,AreaId),   
            objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,BranchId),
            objDAL.MakeInParams("@TruckNo",SqlDbType.VarChar,20,""),            
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@is_for_userdesk",SqlDbType.VarChar,1,"n"),

            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),
            objDAL.MakeInParams("@colid",SqlDbType.Int,0,WucFilter1.colid),
            objDAL.MakeInParams("@datatype_id",SqlDbType.Int,0,WucFilter1.Datatype_ID),
            objDAL.MakeInParams("@criteria_id",SqlDbType.Int,0,WucFilter1.criteriaid),
            objDAL.MakeInParams("@Filtered_Text",SqlDbType.VarChar,50,WucFilter1.Filtered_Text),
            objDAL.MakeInParams("@Filtered_Date",SqlDbType.DateTime,0,WucFilter1.Filtered_Date),
            objDAL.MakeInParams("@Filtered_Bit",SqlDbType.Bit,0,WucFilter1.Filtered_bit)

        };

        objDAL.RunProc("[dbo].[EC_RPT_Incoming_Trucks_Alert_Excel]", objSqlParam, ref ds);

        if (CallFrom == "form_and_pageload") return;


        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();
        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }

    }



    private void PrepareDTForExportToExcel()
    {

        DataRow dr;
        dr = ds.Tables[0].NewRow();
        dr["Total"] = Count;
        dr["Loaded Articles"] = LoadedArticle;
        dr["Loaded Weight"] = LoadedWeight;
        dr["Loaded Articles For Us"] = LoadedArticleForUs;
        dr["Loaded Weight For Us"] = LoadedWtForUs;
        ds.Tables[0].Rows.Add(dr);      

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }
    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}



