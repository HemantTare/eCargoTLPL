using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Web.UI.WebControls;

public partial class Reports_DOC_Monitoring_Frm_Document_Statistics : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    
    int No_Of_Documents, Cancelled_Docs;

    private DataSet SessionDocumentStatistics
    {
        get { return StateManager.GetState<DataSet>("DocumentStatistics"); }
        set { StateManager.SaveState("DocumentStatistics", value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.FileName = "DocumentStatistics";

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
        }
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
    }

    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex != -1)
                {
                    Label lbl_Name = (Label)e.Item.FindControl("lbl_Name");
                    HiddenField hdn_Id = (HiddenField)e.Item.FindControl("hdn_Id");

                    if (Util.String2Int(hdn_Id.Value) == 1)
                    {
                        lbl_Name.Text = CompanyManager.getCompanyParam().GcCaption;
                    }
                    else if (Util.String2Int(hdn_Id.Value) == 5)
                    {
                        lbl_Name.Text = CompanyManager.getCompanyParam().LHPOCaption;
                    }
                }
            }
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbl_Total, lbl_No_Of_documents, lbl_Cancelled_Docs;

            lbl_Total = (Label)e.Item.FindControl("lbl_Total");
            lbl_No_Of_documents = (Label)e.Item.FindControl("lbl_No_Of_documents");
            lbl_Cancelled_Docs = (Label)e.Item.FindControl("lbl_Cancelled_Docs");
            
            lbl_Total.Text = "Total";
            lbl_No_Of_documents.Text = No_Of_Documents.ToString();
            lbl_Cancelled_Docs.Text = Cancelled_Docs.ToString();
        }
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);
        }
        else
        {
            lbl_Error.Text = msg;
            dg_Grid.Visible = false;
        }
    }

    private void calculate_totals()
    {
        No_Of_Documents = Util.String2Int(ds.Tables[0].Compute("sum([No Of Document])", "").ToString());
        Cancelled_Docs = Util.String2Int(ds.Tables[0].Compute("sum([Cancelled Docs])", "").ToString());
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

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;

        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_ID", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_ID", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int,0,Branch_id),   
            objDAL.MakeInParams("@Date1", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@Date2 ", SqlDbType.DateTime,0,To_date),           
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID)
        };

        objDAL.RunProc("[EC_RPT_Document_Statistics]", objSqlParam, ref ds);

        
        calculate_totals();
        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

        //dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

      

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
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
      private void PrepareDTForExportToExcel()
    {

        DataRow dr;
        dr = ds.Tables[0].NewRow();
        dr["Name"] = "Total";
        dr["No Of Document"] = No_Of_Documents;
        dr["Cancelled Docs"] = Cancelled_Docs;
        ds.Tables[0].Rows.Add(dr);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }
}
