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
using System.Data.SqlClient;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using ClassLibraryMVP;

public partial class Reports_CL_Nandwana_DOC_Monitoring_Frm_Rpt_Missing_document : System.Web.UI.Page
{
    DAL objDal = new DAL();
    DataSet ds = new DataSet();
    DataSet ds_Export = new DataSet();
    int main_id;
    string Hierarchy_Code;

    protected void Page_Load(object sender, EventArgs e)
    {
        Hierarchy_Code = (string)UserManager.getUserParam().HierarchyCode;
        main_id = (int)UserManager.getUserParam().MainId;
        if (!IsPostBack)
        {
            Wuc_Region_Area_Branch1.SetRegionCaption = "Region:";
            Wuc_Region_Area_Branch1.SetAreaCaption = "Area:";
            Wuc_Region_Area_Branch1.SetBranchCaption = "Branch:";

            if (Hierarchy_Code == "HO")
            {
                Wuc_Region_Area_Branch1.HoVisibility = true;
                Wuc_Region_Area_Branch1.SelectedLocationsOnlyVisibility = true;
            }
            if (Hierarchy_Code == "RO" || Hierarchy_Code == "AO")
            {
                Wuc_Region_Area_Branch1.SelectedLocationsOnlyVisibility = true;
            }
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            BindMenuHead = fillMenu(1);
        }
        Wuc_Export_To_Excel1.FileName = "Missing Document Register";
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
    }
    public DataTable BindMenuHead
    {
        set { Set_Common_DDL(ddl_Module, "Menu_Head_Name", "Menu_Head_Id", value, true); }
    }
    public DataTable BindMenuItem
    {
        set { Set_Common_DDL(ddl_Menu_Item, "Menuitem_name", "Menuitem_id", value, true); }
    }
    private void Set_Common_DDL(DropDownList DDl, string TextField, string ValueField, DataTable DT, bool Is_ZeroInex)
    {
        DDl.DataSource = DT;
        DDl.DataTextField = TextField;
        DDl.DataValueField = ValueField;
        DDl.DataBind();
        if (Is_ZeroInex)
            DDl.Items.Insert(0, new ListItem("Select One", "0"));
    }
    private DataTable fillMenu(int Flag)
    {
        DataSet ds_MenuItem = new DataSet();
        int i = ddl_Module.SelectedValue == "" ? 0 : Convert.ToInt32(ddl_Module.SelectedValue);
        SqlParameter[] sqlParam = {
            objDal.MakeInParams("@flag", SqlDbType.Int,0,Flag),
            objDal.MakeInParams("@Menu_Head_Id", SqlDbType.Int,0,i),
            };
        objDal.RunProc("EC_RPT_Fill_Missing_Document", sqlParam, ref ds_MenuItem);
        return ds_MenuItem.Tables[0];
    }
    private void BindGrid(object sender, EventArgs e)
    {
        string CallFrom = (string)(sender);
        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;


        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;

        }

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_Date = Wuc_From_To_Datepicker1.SelectedToDate;
        
        SqlParameter[] sqlParam = {
            objDal.MakeInParams("@Region_Id", SqlDbType.Int,0,Region_Id),
            objDal.MakeInParams("@Area_Id", SqlDbType.Int,0,Area_id),
            objDal.MakeInParams("@Branch_Id", SqlDbType.Int,0,Branch_id),
            objDal.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDal.MakeInParams("@To_date", SqlDbType.DateTime,0,To_Date),
            objDal.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDal.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDal.MakeInParams("@Type",SqlDbType.Int,0,Util.String2Int(ddl_Menu_Item.SelectedValue))
            };

        objDal.RunProc("[EC_RPT_Missing_Document]", sqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[1].Rows[0][0].ToString();
        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }
    protected void btn_View_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            if (ddl_Module.SelectedValue == "0")
            {
                lbl_Error.Text = "Please Select Module";
            }
            else if (ddl_Menu_Item.SelectedValue == "0")
            {
                lbl_Error.Text = "Please Select Document";
            }
            else
            {
                lbl_Error.Text = "";
                dg_Grid.CurrentPageIndex = 0;
                BindGrid("form", e);
            }
        }
        else
        {
            lbl_Error.Text = msg;
        }
    }
    private void PrepareDTForExportToExcel()
    {
        
        ds.Tables[0].Columns.Remove("Allocation id");
        ds.Tables[0].Columns.Remove("Document id");
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }
    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int Allocation_Id, Document_Id;
            string Branch_Name;
            LinkButton lnk_Balance;

            Allocation_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Allocation Id").ToString());
            Document_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Document Id").ToString());
            Branch_Name = DataBinder.Eval(e.Item.DataItem, "Branch Name").ToString();
            lnk_Balance = (LinkButton)e.Item.FindControl("lnk_Balance");

            lnk_Balance.Attributes.Add("onclick", "return viewwindow('" + Allocation_Id + "' , '" + Document_Id + "' , '" + Branch_Name + "')");
        }
    }
    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    protected void ddl_Module_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMenuItem = fillMenu(2);
    }
}
