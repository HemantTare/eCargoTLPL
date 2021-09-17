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
//using Raj.eCargo.Init;
//using ClassLibrary;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Infragistics.WebUI.UltraWebGrid;
using System.Drawing;

public partial class CRM_DashBoard_Wuc_CRM_Statistics : System.Web.UI.UserControl
{
    Common objCommon = new Common();
    DataSet dsGridDetails;
    DataSet dsExport;
    DataSet ds;
    System.Type t;
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Region_Area_Branch1.HoVisibility = true;
        if (!IsPostBack)
        {           
            From_Date.SelectedDate = DateTime.Now.Date;
            To_Date.SelectedDate = DateTime.Now.Date;
            Session["GridDetails"] = null;
            Session["Total"] = null;
            Fill_Complaint();          
        }
    }
    private void Fill_Complaint()
    {
        DDL_Report_Type.DataSource = objCommon.Get_Values_Where("EC_CRM_Dashboard_Reports", "Report_ID,Report_Name", "Link_ID =129", "Report_ID", false);
        DDL_Report_Type.DataValueField = "Report_ID";
        DDL_Report_Type.DataTextField = "Report_Name";
        DDL_Report_Type.DataBind();
    }
    protected void btn_Display_Click(object sender, EventArgs e)
    {
        Fill_Grid();
        Grid_Initialize();
    }
    private void Fill_Grid()
    {
        DAL objDAL = new DAL();
        DataSet dsgrid = new DataSet();

        dsgrid = null;
        Session["GridDetails"] = dsgrid;
        int Report_ID = 0;
        int HO;
        Report_ID = Convert.ToInt32(DDL_Report_Type.SelectedValue);
        
       
        SqlParameter[] sqlpar = {
              objDAL.MakeInParams("@Region_Id", SqlDbType.Int, 0,Wuc_Region_Area_Branch1.RegionID),
              objDAL.MakeInParams("@Area_Id", SqlDbType.Int, 0,Wuc_Region_Area_Branch1.AreaID),
              objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0,Wuc_Region_Area_Branch1.BranchID),
              objDAL.MakeInParams("@From_Date", SqlDbType.DateTime, 0,From_Date.SelectedDate),
              objDAL.MakeInParams("@To_Date", SqlDbType.DateTime, 0, To_Date.SelectedDate),
              objDAL.MakeInParams("@Is_HO", SqlDbType.Bit, 0,Wuc_Region_Area_Branch1.IsHo)};
     
        if (Wuc_Region_Area_Branch1.IsHo==true && Report_ID==30)
        {
               objDAL.RunProc("dbo.EC_Rpt_CRM_Stastistics_HO_wise", sqlpar, ref dsgrid);                
        }
        else  
            if (Report_ID == 30)
            {
                if (Wuc_Region_Area_Branch1.BranchID == 0 && Wuc_Region_Area_Branch1.AreaID !=0)
                {
                     objDAL.RunProc("dbo.EC_Rpt_CRM_Stastistics_Area_wise", sqlpar, ref dsgrid);
                }
                else if (Wuc_Region_Area_Branch1.BranchID != 0 && Report_ID == 30)
                {
                    objDAL.RunProc("dbo.EC_Rpt_CRM_Stastistics_Branch_Wise", sqlpar, ref dsgrid);
                }
                else
                {
                    objDAL.RunProc("dbo.EC_Rpt_CRM_Stastistics_Region_wise", sqlpar, ref dsgrid);
                }
               
            }
            else if (Report_ID == 31)
            {
                objDAL.RunProc("dbo.EC_Rpt_CRM_Stastistics_Complaint_Nature_Wise", sqlpar, ref dsgrid);
            }
            else if (Report_ID == 32)
            {
                objDAL.RunProc("dbo.EC_Rpt_CRM_Stastistics_Priority_Wise", sqlpar, ref dsgrid);
            }
            else if (Report_ID == 33)
            {
                objDAL.RunProc("dbo.EC_Rpt_CRM_Stastistics_Assignor_Wise", sqlpar, ref dsgrid);
            }           
            BindGridDetails = dsgrid.Tables[0];
            Session["GridDetails"] = dsgrid;

            if (dsgrid.Tables[0].Rows.Count > 0)
            {
                int No_of_Complaints = Convert.ToInt32(dsgrid.Tables[0].Compute("sum([No of Complaints Registered])", "").ToString());
                int Open = Convert.ToInt32(dsgrid.Tables[0].Compute("sum([Open])", "").ToString());
                int Pending_for_Agt = Convert.ToInt32(dsgrid.Tables[0].Compute("sum([Pending for Assignment])", "").ToString());
                int Assigned = Convert.ToInt32(dsgrid.Tables[0].Compute("sum(Assigned)", "").ToString());
                int In_Progress = Convert.ToInt32(dsgrid.Tables[0].Compute("sum([In Progress])", "").ToString());
                int Closed = Convert.ToInt32(dsgrid.Tables[0].Compute("sum([Closed])", "").ToString());
                int Archived = Convert.ToInt32(dsgrid.Tables[0].Compute("sum(Archived)", "").ToString());

                GridDetails.Columns[1].Header.Caption = GridDetails.Columns[1].Header.Caption + " " + "(" + No_of_Complaints.ToString() + ")";
                GridDetails.Columns[2].Header.Caption = GridDetails.Columns[2].Header.Caption + " " + "(" + Open.ToString() + ")";
                GridDetails.Columns[3].Header.Caption = GridDetails.Columns[3].Header.Caption + " " + "(" + Pending_for_Agt.ToString() + ")";
                GridDetails.Columns[4].Header.Caption = GridDetails.Columns[4].Header.Caption + " " + "(" + Assigned.ToString() + ")";
                GridDetails.Columns[5].Header.Caption = GridDetails.Columns[5].Header.Caption + " " + "(" + In_Progress.ToString() + ")";
                GridDetails.Columns[6].Header.Caption = GridDetails.Columns[6].Header.Caption + " " + "(" + Closed.ToString() + ")";
                GridDetails.Columns[7].Header.Caption = GridDetails.Columns[7].Header.Caption + " " + "(" + Archived.ToString() + ")";

            }     
    }
    protected void GridDetails_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        dsGridDetails = (DataSet)Session["GridDetails"];
        if (dsGridDetails != null)
        {
            GridDetails.DataSource = dsGridDetails.Tables[0];
            GridDetails.DataBind();
        }
    }
    protected void GridDetails_PageIndexChanged(object sender, PageEventArgs e)
    {
        dsGridDetails = (DataSet)Session["GridDetails"];
        GridDetails.DataSource = dsGridDetails.Tables[0];
        GridDetails.DataBind();
    }

    public DataTable BindGridDetails
    {
        set
        {
            GridDetails.Clear();
            GridDetails.DisplayLayout.Bands[0].SortedColumns.Clear();
            GridDetails.DataSource = value;
            GridDetails.DataBind();
        
        }
    }
    protected void IB_ExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        
        dsExport = (DataSet)Session["GridDetails"];
        if (dsGridDetails != null)
        {
            if (dsGridDetails.Tables[0].Rows.Count > 0)
            {
                ExportGrid.DataSource = dsGridDetails.Tables[0];
                ExportGrid.DataBind();
                UltraWebGridExcelExporter1.Export(ExportGrid);             
                                     
            }
        }
    }


    public void Grid_Initialize()
    {
        ds = (DataSet)Session["GridDetails"];

        this.GridDetails.DisplayLayout.LoadOnDemand = LoadOnDemand.Xml;
        this.GridDetails.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.OutlookGroupBy;
        this.GridDetails.DisplayLayout.StationaryMargins = StationaryMargins.Header;
        this.GridDetails.DisplayLayout.ColFootersVisibleDefault = ShowMarginInfo.Yes;
        this.GridDetails.DisplayLayout.HeaderStyleDefault.ForeColor = System.Drawing.Color.MediumBlue;


        foreach (ColumnHeader c in this.GridDetails.Bands[0].HeaderLayout)
        {
            c.RowLayoutColumnInfo.OriginY = 1;
        }
        if (ds != null)
        {
            if (ds.Tables[0].Columns.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    GridDetails.Columns[i].Header.Style.Wrap = true;
                    GridDetails.Columns[i].CellStyle.Wrap = true;

                    try
                    {
                        string intUserInput = Convert.ToString(ds.Tables[0].Rows[0][i]);

                        t = ds.Tables[0].Rows[0][i].GetType();
                      
                        if ((t.FullName == "System.Int32") || (t.FullName == "System.Int64") || (t.FullName == "System.Int16") || (t.FullName == "System.Double") || (t.FullName == "System.Decimal"))
                        {
                          
                            this.GridDetails.DisplayLayout.Bands[0].Columns[i].Header.Style.HorizontalAlign = HorizontalAlign.Right;
                            this.GridDetails.DisplayLayout.Bands[0].Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            this.GridDetails.DisplayLayout.Bands[0].Columns[i].Footer.Style.HorizontalAlign = HorizontalAlign.Right;
                            this.GridDetails.DisplayLayout.Bands[0].Columns[i].Footer.Style.ForeColor = Color.MediumBlue;
                            this.GridDetails.DisplayLayout.Bands[0].Columns[i].Footer.Total = SummaryInfo.Sum;
                        }
                    }
                    catch
                    {
                        //invalid user input
                    }
                }
            }
        }
    }
    protected void UltraWebGrid1_InitializeLayout(object sender, LayoutEventArgs e)
    {

    }
}
