using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Text;
using System.IO;
public partial class Reports_Operation_Frm_eWayBill_Bulk_Updation : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    decimal GC_Total;
    string GC_No_For_Print, TotalRecords;
    Boolean isFromJson;


    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.FileName = "eWayBillBulkUpdation";
        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
        }
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
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
            System.Web.UI.WebControls.Label lbl_Total, lbl_GC_Total;
            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_GC_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_GC_Total");


                     

        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        isFromJson = false;
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

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);
        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol" || isFromJson == true)
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
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),            
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@Todate ", SqlDbType.DateTime,0,To_date),                 
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("[EC_Rpt_eWay_Bill_Bulk_Updation_New]", objSqlParam, ref ds);

        if (CallFrom == "form_and_pageload") return;

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
        TotalRecords = ds.Tables[1].Rows[0][0].ToString();

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


        ds.Tables[0].Columns.Remove("SrNo");
        ds.Tables[0].Columns.Remove("transMode");
        ds.Tables[0].Columns.Remove("vehicleType");
        ds.Tables[0].Columns.Remove("fromstate");
        ds.Tables[0].Columns.Remove("reason");

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

    protected void btn_Create_jason_Click1(object sender, EventArgs e)
    {

        isFromJson = true;
        string version,versionno,userGstin,userGstinNo,vehicleUpdts;

        version="version";
        versionno="1.0.0123";
        userGstin="userGstin";
        userGstinNo = txtGSTNo.Text;
        vehicleUpdts="vehicleUpdts";

        BindGrid("form", e);


        ds.Tables[0].Columns.Remove("SrNo");
        ds.Tables[0].Columns.Remove("transModeName");
        ds.Tables[0].Columns.Remove("vehicletypename");
        ds.Tables[0].Columns.Remove("fromstatename");
        ds.Tables[0].Columns.Remove("reasonname");



        StringBuilder JsonString = new StringBuilder();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //FileStream fStream = new FileStream(@"C:\Jason\MyJason.json", FileMode.Create, FileAccess.Write);
            //StreamWriter sWriter = new StreamWriter(fStream);



            string sFileName = System.IO.Path.GetRandomFileName();
            string sGenName = "VehicleUpdate_JSON.json";

            using (System.IO.StreamWriter sWriter = new System.IO.StreamWriter(
                   Server.MapPath("~/JasonFiles/" + sFileName + ".txt")))
            {
                //sWriter.WriteLine(txtText.Text);

                JsonString.Append("{");
                sWriter.WriteLine("{");

                JsonString.Append("\"" + version + "\":" + "\"" + versionno + "\",");
                sWriter.WriteLine("\"" + version + "\":" + "\"" + versionno + "\",");

                JsonString.Append("\"");


                JsonString.Append(userGstin + "\":" + "\"" + userGstinNo + "\",");
                sWriter.WriteLine("\"" + userGstin + "\":" + "\"" + userGstinNo + "\",");

                sWriter.WriteLine("");

                JsonString.Append("\"" + vehicleUpdts + "\":");
                sWriter.WriteLine("\"" + vehicleUpdts + "\":");

                JsonString.Append("[");
                sWriter.WriteLine("[");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    JsonString.Append("{");
                    sWriter.WriteLine("{");

                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j < ds.Tables[0].Columns.Count - 1)
                        {
                            if (j == 0 || j == 1 || j == 7 || j == 8)
                            {
                                JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + ds.Tables[0].Rows[i][j].ToString() + ",");
                                sWriter.WriteLine("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + ds.Tables[0].Rows[i][j].ToString() + ",");
                            }
                            else
                            {
                                JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                                sWriter.WriteLine("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                            }
                        }
                        else if (j == ds.Tables[0].Columns.Count - 1)
                        {
                            JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                            sWriter.WriteLine("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        JsonString.Append("}");
                        sWriter.WriteLine("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                        sWriter.WriteLine("},");
                        sWriter.WriteLine("");
                    }
                }
                JsonString.Append("]");
                sWriter.WriteLine("]");

                JsonString.Append("}");
                sWriter.WriteLine("}");

                //txt_Jason.Text = JsonString.ToString();

                sWriter.Close();
                //fStream.Close();

                System.IO.FileStream fs = null;
                fs = System.IO.File.Open(Server.MapPath("~/JasonFiles/" +
                         sFileName + ".txt"), System.IO.FileMode.Open);
                byte[] btFile = new byte[fs.Length];
                fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" +
                                   sGenName);
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(btFile);
                Response.End();
            }
        }
        else
        {
            //return null;
            //txt_Jason.Text = JsonString.ToString();
        }
    }


   
}
