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
using System.Text;

public partial class Reports_CL_Nandwana_User_Desk_FrmPendingInwardList : System.Web.UI.Page
{
    #region ClassVariables
    private DataSet ds;
    private DAL objDAL = new DAL();
    private Decimal LoadedWeight,LoadedWtForUs;
    private int Branch_ID, Area_ID, Region_ID;
    private string InwardBranch_Name;
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


        Branch_ID = Convert.ToInt32(Request.QueryString["Branch_ID"].ToString());
        Area_ID = Convert.ToInt32(Request.QueryString["Area_ID"].ToString());
        Region_ID = Convert.ToInt32(Request.QueryString["Region_ID"].ToString());

        InwardBranch_Name = Request.QueryString["InwardBranch_Name"].ToString();

        lbl_Branch.Text = InwardBranch_Name;


        if (IsPostBack == false)
        {
            BindGrid("form_and_pageload", e);
        }

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "PendingInward";


        StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());
        PathF4 = new StringBuilder(Util.GetBaseURL());
        PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingInwardDlyBranchWiseViewer.aspx?Branch_ID=" + Branch_ID);
        lnk_btnPrintPivot.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");


        if (UserManager.getUserParam().HierarchyCode != "BO")
        {
            lnk_btnPrintPivot.Visible = false;
        }

    }
 

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
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

         SqlParameter[] objSqlParam = {objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,Branch_ID),
            objDAL.MakeInParams("@Area_ID",SqlDbType.Int,0,Area_ID),
            objDAL.MakeInParams("@Region_ID",SqlDbType.Int,0,Region_ID),
            objDAL.MakeInParams("@ReportTypeID",SqlDbType.Int,0,4)};

         objDAL.RunProc("dbo.COM_UserDesk_Pending_Process_Details", objSqlParam, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                dg_Grid.DataSource = ds.Tables[0];
                dg_Grid.DataBind(); 
            }

            if (CallFrom == "exporttoexcelusercontrol")
            {
                PrepareDTForExportToExcel();
            }
    }

    private void PrepareDTForExportToExcel()
    {
        
        ds.Tables[0].Columns.Remove("Memo_ID");
        ds.Tables[0].Columns.Remove("LHPO_ID");
        ds.Tables[0].Columns.Remove("Vehicle_ID");
        
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
    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item) || (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem))
        {
            if ((e.Item.Cells[0].Text) == "Total : ")
            {
                e.Item.BackColor = System.Drawing.Color.Green;
                e.Item.ForeColor = System.Drawing.Color.WhiteSmoke;
                e.Item.Font.Bold = true;
            }
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            int VehicleId,LHPOId,Memo_Id;
            LinkButton lbtn_VehicleNo, lbtn_View, lbtn_Print;

            VehicleId  = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Vehicle_Id").ToString());
            lbtn_VehicleNo = (LinkButton)e.Item.FindControl("lbtn_VehicleNo");

            lbtn_View = (LinkButton)e.Item.FindControl("lbtn_View");

            lbtn_Print = (LinkButton)e.Item.FindControl("lbtn_Print");

            LHPOId = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "LHPO_ID").ToString());

            Memo_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Memo_Id").ToString());

            string _hierarchyCode = UserManager.getUserParam().HierarchyCode;
            
            lbtn_View.Attributes.Add("onclick", "return viewwindow_Memo('" + ClassLibraryMVP.Util.EncryptInteger(Memo_Id) + "')");

            lbtn_Print.Attributes.Add("onclick", "return printwindow_Memo('" + Memo_Id + "')");

            if (_hierarchyCode == "BO")
            {
                lbtn_VehicleNo.Attributes.Add("onclick", "return viewwindow_AUS('" + ClassLibraryMVP.Util.EncryptInteger(VehicleId) + "','" + ClassLibraryMVP.Util.EncryptInteger(LHPOId)  + "')");
            }
        }

    }
}



