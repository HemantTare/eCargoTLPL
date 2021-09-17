using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;
using Ionic.Zip;
using System.Collections.Generic;
public partial class Reports_Operation_Frm_eWayBill_Bulk_Updation2 : System.Web.UI.Page
{
    private DataSet ds, ds1, ds2;
    private DAL objDAL = new DAL();

    string TotalRecords;
    Boolean isFromJson;


    public DateTime LoadingDate
    {
        set
        {
            dtpApplicableFrom.SelectedDate = value;
            //hdn_Date.Value = dtpApplicableFrom.SelectedDate.ToString();
        }
        get { return dtpApplicableFrom.SelectedDate; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.FileName = "eWayBillBulkUpdation";
        if (IsPostBack == false)
        {
            Wuc_Region_Area_Branch1.SetDDLBranchAutoPostback = true;

            dtpApplicableFrom.SelectedDate = DateTime.Now;
            dtpApplicableTo.SelectedDate = DateTime.Now;

            //Fill_Vehicles();

            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
        }
        Wuc_Region_Area_Branch1.BranchIndexChange += new EventHandler(FillVehicles);

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
        //if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        //{
        lbl_Error.Text = "";
        dg_Grid.Visible = true;
        dg_Grid.CurrentPageIndex = 0;
        BindGrid("form", e);
        //}
        //else
        //{
        //    lbl_Error.Text = msg;
        //    dg_Grid.Visible = false;
        //}
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

        int VehicleID = Convert.ToInt32(ddl_Vehicle.SelectedItem.Value);

        DateTime From_Date = dtpApplicableFrom.SelectedDate;
        DateTime To_date = dtpApplicableTo.SelectedDate;

        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),            
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@Todate ", SqlDbType.DateTime,0,To_date),   
            objDAL.MakeInParams("@VehicleID ", SqlDbType.Int,0,VehicleID),     
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("[EC_Rpt_eWay_Bill_Bulk_Updation_New3]", objSqlParam, ref ds);

        if (CallFrom == "form_and_pageload") return;

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
        TotalRecords = ds.Tables[1].Rows[0][0].ToString();

        //if (Util.String2Int(TotalRecords) > 0)
        //{
        //    txtGSTNo.Text = ds.Tables[2].Rows[0][0].ToString();
        //}

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    private void PrepareDTForExportToExcel()
    {


        ds.Tables[3].Columns.Remove("SrNo");
        ds.Tables[3].Columns.Remove("BkgStateGstNo");
        ds.Tables[3].Columns.Remove("transMode");
        ds.Tables[3].Columns.Remove("fromstate");
        ds.Tables[3].Columns.Remove("reason");
        ds.Tables[3].Columns.Remove("reasonname");
        ds.Tables[3].Columns.Remove("remark");

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[3];
    }

    public void ClearVariables()
    {
        ds = null;
        ds1 = null;
        ds2 = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    protected void dtpApplicableFrom_SelectionChanged(object sender, EventArgs e)
    {
        Fill_Vehicles();
    }


    private void FillVehicles(object sender, EventArgs e)
    {
        Fill_Vehicles();
    }


    private void Fill_Vehicles()
    {
        DAL objDAL = new DAL();

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@LoadingDate", SqlDbType.DateTime, 0, LoadingDate),
        objDAL.MakeInParams("@RegionID", SqlDbType.Int,0,Region_Id),
        objDAL.MakeInParams("@AreaID", SqlDbType.Int,0,Area_id),
        objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,Branch_id)         
        };
        objDAL.RunProc("EC_Mst_SMS_COnsolidated_eWayBill_FillValues2", objSqlParam, ref ds);


        ddl_Vehicle.DataSource = ds;
        ddl_Vehicle.DataTextField = "Vehicle_No";
        ddl_Vehicle.DataValueField = "VehicleID";
        ddl_Vehicle.DataBind();

        ddl_Vehicle.Items.Insert(0, new ListItem("All Vehicles", "0"));
    }


    protected void btn_Create_jason_Click1(object sender, EventArgs e)
    {
        int VehicleID = Convert.ToInt32(ddl_Vehicle.SelectedItem.Value);

        if (VehicleID > 0)
        {
            isFromJson = true;

            string version, versionno, userGstin, userGstinNo, vehicleUpdts, tripSheets, tripSheetEwbBills, fromPlace, fromPlaceeWayBills;
            int found;

            found = 0;

            version = "version";
            //versionno = "1.0.0618";
            versionno = "1.0.1020";
            userGstin = "userGstin";
            userGstinNo = txtGSTNo.Text;
            vehicleUpdts = "vehicleUpdts";
            tripSheets = "tripSheets";
            tripSheetEwbBills = "tripSheetEwbBills";

            BindGrid("form", e);

            fromPlace = "";
            fromPlaceeWayBills = "";

            ds.Tables[4].Columns.Remove("SrNo");
            ds.Tables[4].Columns.Remove("BkgStateGstNo");
            ds.Tables[4].Columns.Remove("transModeName");
            ds.Tables[4].Columns.Remove("fromstatename");
            ds.Tables[4].Columns.Remove("reasonname");
            ds.Tables[4].Columns.Remove("reason");
            ds.Tables[4].Columns.Remove("remark");



            StringBuilder JsonString = new StringBuilder();
            if (ds != null && ds.Tables[4].Rows.Count > 0)
            {
                //FileStream fStream = new FileStream(@"C:\Jason\MyJason.json", FileMode.Create, FileAccess.Write);
                //StreamWriter sWriter = new StreamWriter(fStream);



                string sFileName = System.IO.Path.GetRandomFileName();
                string sGenName = "Consolidated_E-WayBill_JSON.json";

                using (System.IO.StreamWriter sWriter = new System.IO.StreamWriter(
                       Server.MapPath("~/JasonFiles/" + sFileName + ".txt")))
                {
                    //sWriter.WriteLine(txtText.Text);

                    JsonString.Append("{");
                    sWriter.WriteLine("{");

                    JsonString.Append("\"" + version + "\":" + "\"" + versionno + "\",");
                    sWriter.WriteLine("\"" + version + "\":" + "\"" + versionno + "\",");

                    JsonString.Append("\"");

                    JsonString.Append("\"" + tripSheets + "\":[");
                    sWriter.WriteLine("\"" + tripSheets + "\":[");

                    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                    {
                        sWriter.WriteLine("{");
                        JsonString.Append(" \"" + userGstin + "\":" + "\"" + userGstinNo + "\",");
                        sWriter.WriteLine(" \"" + userGstin + "\":" + "\"" + userGstinNo + "\",");


                        for (int j = 0; j < ds.Tables[4].Columns.Count; j++)
                        {
                            found = 0;
                            if (j < ds.Tables[4].Columns.Count - 1)
                            {

                                if (j == 0 || j == 1 || j == 6 || j == 7 || j == 2 || j == 3 || j == 5)
                                {
                                    if (j == 3)
                                    {
                                        fromPlace = ds.Tables[4].Rows[i][j].ToString();
                                    }

                                    JsonString.Append(" \"" + ds.Tables[4].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[4].Rows[i][j].ToString() + "\",");
                                    sWriter.WriteLine(" \"" + ds.Tables[4].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[4].Rows[i][j].ToString() + "\",");
                                }
                            }
                            else if (j == ds.Tables[4].Columns.Count - 1)
                            {
                                JsonString.Append(" \"" + ds.Tables[4].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[4].Rows[i][j].ToString() + "\",");
                                sWriter.WriteLine(" \"" + ds.Tables[4].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[4].Rows[i][j].ToString() + "\",");
                            }
                        }


                        JsonString.Append(" \"" + tripSheetEwbBills + "\":[");
                        sWriter.WriteLine(" \"" + tripSheetEwbBills + "\":[");

                        for (int l = 0; l < ds.Tables[4].Rows.Count; l++)
                        {
                            for (int k = 0; k < ds.Tables[4].Columns.Count; k++)
                            {
                                if (k == 3)
                                {
                                    fromPlaceeWayBills = ds.Tables[4].Rows[l][k].ToString();
                                }

                                if (k == 4)
                                {
                                    if (fromPlace == fromPlaceeWayBills)
                                    {
                                        if (found > 0)
                                        {
                                            JsonString.Append("},");
                                            sWriter.WriteLine("},");
                                            JsonString.Append("");
                                            sWriter.WriteLine("");

                                            i = i + 1;
                                        }

                                        JsonString.Append("{");
                                        sWriter.WriteLine("{");

                                        JsonString.Append(" \"" + ds.Tables[4].Columns[k].ColumnName.ToString() + "\":" + ds.Tables[4].Rows[l][k].ToString());
                                        sWriter.WriteLine(" \"" + ds.Tables[4].Columns[k].ColumnName.ToString() + "\":" + ds.Tables[4].Rows[l][k].ToString());

                                        found = found + 1;

                                    }
                                    else
                                    {
                                        found = 0;
                                    }
                                }
                            }
                        }
                        JsonString.Append("}");
                        sWriter.WriteLine("}");

                        JsonString.Append("");
                        sWriter.WriteLine("");

                        JsonString.Append("]");
                        sWriter.WriteLine("]");
                        sWriter.WriteLine("");

                        if (i == ds.Tables[4].Rows.Count - 1)
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

                    sWriter.Close();

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
        else
        {
            lbl_Error.Text = "Please Select Vehicle";
            ddl_Vehicle.Focus();
        }
    }


    protected void btn_Create_jason_Click1_Old(object sender, EventArgs e)
    {
        int VehicleID = Convert.ToInt32(ddl_Vehicle.SelectedItem.Value);

        if (VehicleID > 0)
        {
            isFromJson = true;

            string version, versionno, userGstin, userGstinNo, vehicleUpdts, tripSheets, tripSheetEwbBills, fromPlace, fromPlaceeWayBills;
            int found;

            found = 0;

            version = "version";
            //versionno = "1.0.0618";
            versionno = "1.0.1020";
            userGstin = "userGstin";
            userGstinNo = txtGSTNo.Text;
            vehicleUpdts = "vehicleUpdts";
            tripSheets = "tripSheets";
            tripSheetEwbBills = "tripSheetEwbBills";

            BindGrid("form", e);

            fromPlace = "";
            fromPlaceeWayBills = "";

            ds.Tables[0].Columns.Remove("SrNo");
            ds.Tables[0].Columns.Remove("BkgStateGstNo");
            ds.Tables[0].Columns.Remove("transModeName");
            ds.Tables[0].Columns.Remove("fromstatename");
            ds.Tables[0].Columns.Remove("reasonname");
            ds.Tables[0].Columns.Remove("reason");
            ds.Tables[0].Columns.Remove("remark");



            StringBuilder JsonString = new StringBuilder();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //FileStream fStream = new FileStream(@"C:\Jason\MyJason.json", FileMode.Create, FileAccess.Write);
                //StreamWriter sWriter = new StreamWriter(fStream);



                string sFileName = System.IO.Path.GetRandomFileName();
                string sGenName = "Consolidated_E-WayBill_JSON.json";

                using (System.IO.StreamWriter sWriter = new System.IO.StreamWriter(
                       Server.MapPath("~/JasonFiles/" + sFileName + ".txt")))
                {
                    //sWriter.WriteLine(txtText.Text);

                    JsonString.Append("{");
                    sWriter.WriteLine("{");

                    JsonString.Append("\"" + version + "\":" + "\"" + versionno + "\",");
                    sWriter.WriteLine("\"" + version + "\":" + "\"" + versionno + "\",");

                    JsonString.Append("\"");

                    JsonString.Append("\"" + tripSheets + "\":[");
                    sWriter.WriteLine("\"" + tripSheets + "\":[");

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        sWriter.WriteLine("{");
                        JsonString.Append(" \"" + userGstin + "\":" + "\"" + userGstinNo + "\",");
                        sWriter.WriteLine(" \"" + userGstin + "\":" + "\"" + userGstinNo + "\",");


                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            found = 0;
                            if (j < ds.Tables[0].Columns.Count - 1)
                            {

                                if (j == 0 || j == 1 || j == 6 || j == 7 || j == 2 || j == 3 || j == 5)
                                {
                                    if (j == 3)
                                    {
                                        fromPlace = ds.Tables[0].Rows[i][j].ToString();
                                    }

                                    JsonString.Append(" \"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                                    sWriter.WriteLine(" \"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                                }
                            }
                            else if (j == ds.Tables[0].Columns.Count - 1)
                            {
                                JsonString.Append(" \"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                                sWriter.WriteLine(" \"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                            }
                        }


                        JsonString.Append(" \"" + tripSheetEwbBills + "\":[");
                        sWriter.WriteLine(" \"" + tripSheetEwbBills + "\":[");

                        for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                        {
                            for (int k = 0; k < ds.Tables[0].Columns.Count; k++)
                            {
                                if (k == 3)
                                {
                                    fromPlaceeWayBills = ds.Tables[0].Rows[l][k].ToString();
                                }

                                if (k == 4)
                                {
                                    if (fromPlace == fromPlaceeWayBills)
                                    {
                                        if (found > 0)
                                        {
                                            JsonString.Append("},");
                                            sWriter.WriteLine("},");
                                            JsonString.Append("");
                                            sWriter.WriteLine("");
                                        }

                                        JsonString.Append("{");
                                        sWriter.WriteLine("{");

                                        JsonString.Append(" \"" + ds.Tables[0].Columns[k].ColumnName.ToString() + "\":" + ds.Tables[0].Rows[l][k].ToString());
                                        sWriter.WriteLine(" \"" + ds.Tables[0].Columns[k].ColumnName.ToString() + "\":" + ds.Tables[0].Rows[l][k].ToString());

                                        found = found + 1;
                                    }
                                    else
                                    {
                                        found = 0;
                                    }
                                }
                            }
                        }
                        JsonString.Append("}");
                        sWriter.WriteLine("}");

                        JsonString.Append("");
                        sWriter.WriteLine("");

                        JsonString.Append("]");
                        sWriter.WriteLine("]");
                        sWriter.WriteLine("");

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
        else
        {
            lbl_Error.Text = "Please Select Vehicle";
            ddl_Vehicle.Focus();
        }
    }

    protected void btn_Create_jasonAll_Click(object sender, EventArgs e)
    {
        string path;

        path = Server.MapPath("~");

        path = path + @"\JasonFiles";


        Array.ForEach(Directory.GetFiles(path), File.Delete);

        DAL objDAL = new DAL();

        int Region_Id2 = Wuc_Region_Area_Branch1.RegionID;
        int Area_id2 = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id2 = Wuc_Region_Area_Branch1.BranchID;

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@LoadingDate", SqlDbType.DateTime, 0, LoadingDate),
        objDAL.MakeInParams("@RegionID", SqlDbType.Int,0,Region_Id2),
        objDAL.MakeInParams("@AreaID", SqlDbType.Int,0,Area_id2),
        objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,Branch_id2),         
        };
        objDAL.RunProc("EC_Mst_SMS_COnsolidated_eWayBill_FillValues2", objSqlParam, ref ds1);

        int VehicleID2;
        string VehicleNo2;

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;


        DateTime From_Date = dtpApplicableFrom.SelectedDate;
        DateTime To_date = dtpApplicableTo.SelectedDate;

        int Division_ID = WucDivisions1.Division_ID;

        isFromJson = true;

        string version, versionno, userGstin, userGstinNo, vehicleUpdts, tripSheets, tripSheetEwbBills, fromPlace, fromPlaceeWayBills;
        int found;


        for (int h = 0; h < ds1.Tables[0].Rows.Count; h++)
        {
            VehicleID2 = Convert.ToInt32(ds1.Tables[0].Rows[h][0].ToString());
            VehicleNo2 = ds1.Tables[0].Rows[h][1].ToString();

            if (VehicleID2 > 0)
            {


                SqlParameter[] objSqlParam2 ={ 
                objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
                objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
                objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),            
                objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,From_Date),
                objDAL.MakeInParams("@Todate ", SqlDbType.DateTime,0,To_date),   
                objDAL.MakeInParams("@VehicleID ", SqlDbType.Int,0,VehicleID2),     
                objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,0),
                objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,2000)};

                objDAL.RunProc("[EC_Rpt_eWay_Bill_Bulk_Updation_New3]", objSqlParam2, ref ds2);



                found = 0;

                version = "version";
                //versionno = "1.0.0618";
                versionno = "1.0.1020";
                userGstin = "userGstin";
                userGstinNo = txtGSTNo.Text;
                vehicleUpdts = "vehicleUpdts";
                tripSheets = "tripSheets";
                tripSheetEwbBills = "tripSheetEwbBills";

                BindGrid("form", e);

                fromPlace = "";
                fromPlaceeWayBills = "";

                ds2.Tables[4].Columns.Remove("SrNo");
                ds2.Tables[4].Columns.Remove("BkgStateGstNo");
                ds2.Tables[4].Columns.Remove("transModeName");
                ds2.Tables[4].Columns.Remove("fromstatename");
                ds2.Tables[4].Columns.Remove("reasonname");
                ds2.Tables[4].Columns.Remove("reason");
                ds2.Tables[4].Columns.Remove("remark");



                StringBuilder JsonString = new StringBuilder();
                if (ds2 != null && ds2.Tables[4].Rows.Count > 0)
                {
                    //FileStream fStream = new FileStream(@"C:\Jason\MyJason.json", FileMode.Create, FileAccess.Write);
                    //StreamWriter sWriter = new StreamWriter(fStream);



                    string sFileName = "Consolidated_E-WayBill_JSON_" + VehicleNo2.Substring(VehicleNo2.Length - 4) + ".json";  //System.IO.Path.GetRandomFileName();
                    string sGenName = "Consolidated_E-WayBill_JSON_" + VehicleNo2.Substring(VehicleNo2.Length - 4) + ".json";

                    using (System.IO.StreamWriter sWriter = new System.IO.StreamWriter(
                           Server.MapPath("~/JasonFiles/" + sFileName)))
                    {
                        //sWriter.WriteLine(txtText.Text);

                        JsonString.Append("{");
                        sWriter.WriteLine("{");

                        JsonString.Append("\"" + version + "\":" + "\"" + versionno + "\",");
                        sWriter.WriteLine("\"" + version + "\":" + "\"" + versionno + "\",");

                        JsonString.Append("\"");

                        JsonString.Append("\"" + tripSheets + "\":[");
                        sWriter.WriteLine("\"" + tripSheets + "\":[");

                        for (int i = 0; i < ds2.Tables[4].Rows.Count; i++)
                        {
                            sWriter.WriteLine("{");
                            JsonString.Append(" \"" + userGstin + "\":" + "\"" + userGstinNo + "\",");
                            sWriter.WriteLine(" \"" + userGstin + "\":" + "\"" + userGstinNo + "\",");


                            for (int j = 0; j < ds2.Tables[4].Columns.Count; j++)
                            {
                                found = 0;
                                if (j < ds2.Tables[4].Columns.Count - 1)
                                {

                                    if (j == 0 || j == 1 || j == 6 || j == 7 || j == 2 || j == 3 || j == 5)
                                    {
                                        if (j == 3)
                                        {
                                            fromPlace = ds2.Tables[4].Rows[i][j].ToString();
                                        }

                                        JsonString.Append(" \"" + ds2.Tables[4].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds2.Tables[4].Rows[i][j].ToString() + "\",");
                                        sWriter.WriteLine(" \"" + ds2.Tables[4].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds2.Tables[4].Rows[i][j].ToString() + "\",");
                                    }
                                }
                                else if (j == ds2.Tables[4].Columns.Count - 1)
                                {
                                    JsonString.Append(" \"" + ds2.Tables[4].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds2.Tables[4].Rows[i][j].ToString() + "\",");
                                    sWriter.WriteLine(" \"" + ds2.Tables[4].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds2.Tables[4].Rows[i][j].ToString() + "\",");
                                }
                            }


                            JsonString.Append(" \"" + tripSheetEwbBills + "\":[");
                            sWriter.WriteLine(" \"" + tripSheetEwbBills + "\":[");

                            for (int l = 0; l < ds2.Tables[4].Rows.Count; l++)
                            {
                                for (int k = 0; k < ds2.Tables[4].Columns.Count; k++)
                                {
                                    if (k == 3)
                                    {
                                        fromPlaceeWayBills = ds2.Tables[4].Rows[l][k].ToString();
                                    }

                                    if (k == 4)
                                    {
                                        if (fromPlace == fromPlaceeWayBills)
                                        {
                                            if (found > 0)
                                            {
                                                JsonString.Append("},");
                                                sWriter.WriteLine("},");
                                                JsonString.Append("");
                                                sWriter.WriteLine("");

                                                i = i + 1;
                                            }

                                            JsonString.Append("{");
                                            sWriter.WriteLine("{");

                                            JsonString.Append(" \"" + ds2.Tables[4].Columns[k].ColumnName.ToString() + "\":" + ds2.Tables[4].Rows[l][k].ToString());
                                            sWriter.WriteLine(" \"" + ds2.Tables[4].Columns[k].ColumnName.ToString() + "\":" + ds2.Tables[4].Rows[l][k].ToString());

                                            found = found + 1;
                                        }
                                        else
                                        {
                                            found = 0;
                                        }
                                    }
                                }
                            }
                            JsonString.Append("}");
                            sWriter.WriteLine("}");

                            JsonString.Append("");
                            sWriter.WriteLine("");

                            JsonString.Append("]");
                            sWriter.WriteLine("]");
                            sWriter.WriteLine("");

                            if (i == ds2.Tables[4].Rows.Count - 1)
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
                                 sFileName), System.IO.FileMode.Open);
                        byte[] btFile = new byte[fs.Length];
                        fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                        fs.Close();
                        //Response.AddHeader("Content-disposition", "attachment; filename=" +
                        //                   sGenName);
                        //Response.ContentType = "application/octet-stream";
                        //Response.BinaryWrite(btFile);
                        //Response.End();
                    }
                }
                else
                {
                    //return null;
                    //txt_Jason.Text = JsonString.ToString();
                }

            }
        }

        DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/JasonFiles/"));

        FileInfo[] filesInFolder = directory.GetFiles("*.*", SearchOption.AllDirectories);

        if (filesInFolder.Length > 0)
        {
            foreach (FileInfo fileInfo in filesInFolder)
            {
                checkBoxList.Items.Add(fileInfo.Name);

            }

            ZipFile multipleFiles = new ZipFile();

            Response.AddHeader("Content-Disposition", "attachment; filename=Consolidated_eWay_Bills.zip");

            Response.ContentType = "application/zip";

            foreach (ListItem fileName in checkBoxList.Items)
            {

                if (fileName.Text != "")
                {

                    string filePath = Server.MapPath("~/JasonFiles/" + fileName.Value);

                    multipleFiles.AddFile(filePath, string.Empty);

                }

            }

            multipleFiles.Save(Response.OutputStream);

        }

        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }



    protected void btn_Create_jasonAll_Click_Old(object sender, EventArgs e)
    {
        string path;

        path = Server.MapPath("~");

        path = path + @"\JasonFiles";


        Array.ForEach(Directory.GetFiles(path), File.Delete);

        DAL objDAL = new DAL();

        int Region_Id2 = Wuc_Region_Area_Branch1.RegionID;
        int Area_id2 = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id2 = Wuc_Region_Area_Branch1.BranchID;

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@LoadingDate", SqlDbType.DateTime, 0, LoadingDate),
        objDAL.MakeInParams("@RegionID", SqlDbType.Int,0,Region_Id2),
        objDAL.MakeInParams("@AreaID", SqlDbType.Int,0,Area_id2),
        objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,Branch_id2),         
        };
        objDAL.RunProc("EC_Mst_SMS_COnsolidated_eWayBill_FillValues2", objSqlParam, ref ds1);

        int VehicleID2;
        string VehicleNo2;

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;


        DateTime From_Date = dtpApplicableFrom.SelectedDate;
        DateTime To_date = dtpApplicableTo.SelectedDate;

        int Division_ID = WucDivisions1.Division_ID;

        isFromJson = true;

        string version, versionno, userGstin, userGstinNo, vehicleUpdts, tripSheets, tripSheetEwbBills, fromPlace, fromPlaceeWayBills;
        int found;


        for (int h = 0; h < ds1.Tables[0].Rows.Count; h++)
        {
            VehicleID2 = Convert.ToInt32(ds1.Tables[0].Rows[h][0].ToString());
            VehicleNo2 = ds1.Tables[0].Rows[h][1].ToString();

            if (VehicleID2 > 0)
            {


                SqlParameter[] objSqlParam2 ={ 
                objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
                objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
                objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),            
                objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,From_Date),
                objDAL.MakeInParams("@Todate ", SqlDbType.DateTime,0,To_date),   
                objDAL.MakeInParams("@VehicleID ", SqlDbType.Int,0,VehicleID2),     
                objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,0),
                objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,2000)};

                objDAL.RunProc("[EC_Rpt_eWay_Bill_Bulk_Updation_New3]", objSqlParam2, ref ds2);



                found = 0;

                version = "version";
                //versionno = "1.0.0618";
                versionno = "1.0.1020";
                userGstin = "userGstin";
                userGstinNo = txtGSTNo.Text;
                vehicleUpdts = "vehicleUpdts";
                tripSheets = "tripSheets";
                tripSheetEwbBills = "tripSheetEwbBills";

                BindGrid("form", e);

                fromPlace = "";
                fromPlaceeWayBills = "";

                ds2.Tables[0].Columns.Remove("SrNo");
                ds2.Tables[0].Columns.Remove("BkgStateGstNo");
                ds2.Tables[0].Columns.Remove("transModeName");
                ds2.Tables[0].Columns.Remove("fromstatename");
                ds2.Tables[0].Columns.Remove("reasonname");
                ds2.Tables[0].Columns.Remove("reason");
                ds2.Tables[0].Columns.Remove("remark");



                StringBuilder JsonString = new StringBuilder();
                if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                {
                    //FileStream fStream = new FileStream(@"C:\Jason\MyJason.json", FileMode.Create, FileAccess.Write);
                    //StreamWriter sWriter = new StreamWriter(fStream);



                    string sFileName = "Consolidated_E-WayBill_JSON_" + VehicleNo2.Substring(VehicleNo2.Length - 4) + ".json";  //System.IO.Path.GetRandomFileName();
                    string sGenName = "Consolidated_E-WayBill_JSON_" + VehicleNo2.Substring(VehicleNo2.Length - 4) + ".json";

                    using (System.IO.StreamWriter sWriter = new System.IO.StreamWriter(
                           Server.MapPath("~/JasonFiles/" + sFileName)))
                    {
                        //sWriter.WriteLine(txtText.Text);

                        JsonString.Append("{");
                        sWriter.WriteLine("{");

                        JsonString.Append("\"" + version + "\":" + "\"" + versionno + "\",");
                        sWriter.WriteLine("\"" + version + "\":" + "\"" + versionno + "\",");

                        JsonString.Append("\"");

                        JsonString.Append("\"" + tripSheets + "\":[");
                        sWriter.WriteLine("\"" + tripSheets + "\":[");

                        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                        {
                            sWriter.WriteLine("{");
                            JsonString.Append(" \"" + userGstin + "\":" + "\"" + userGstinNo + "\",");
                            sWriter.WriteLine(" \"" + userGstin + "\":" + "\"" + userGstinNo + "\",");


                            for (int j = 0; j < ds2.Tables[0].Columns.Count; j++)
                            {
                                found = 0;
                                if (j < ds2.Tables[0].Columns.Count - 1)
                                {

                                    if (j == 0 || j == 1 || j == 6 || j == 7 || j == 2 || j == 3 || j == 5)
                                    {
                                        if (j == 3)
                                        {
                                            fromPlace = ds2.Tables[0].Rows[i][j].ToString();
                                        }

                                        JsonString.Append(" \"" + ds2.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds2.Tables[0].Rows[i][j].ToString() + "\",");
                                        sWriter.WriteLine(" \"" + ds2.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds2.Tables[0].Rows[i][j].ToString() + "\",");
                                    }
                                }
                                else if (j == ds2.Tables[0].Columns.Count - 1)
                                {
                                    JsonString.Append(" \"" + ds2.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds2.Tables[0].Rows[i][j].ToString() + "\",");
                                    sWriter.WriteLine(" \"" + ds2.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds2.Tables[0].Rows[i][j].ToString() + "\",");
                                }
                            }


                            JsonString.Append(" \"" + tripSheetEwbBills + "\":[");
                            sWriter.WriteLine(" \"" + tripSheetEwbBills + "\":[");

                            for (int l = 0; l < ds2.Tables[0].Rows.Count; l++)
                            {
                                for (int k = 0; k < ds2.Tables[0].Columns.Count; k++)
                                {
                                    if (k == 3)
                                    {
                                        fromPlaceeWayBills = ds2.Tables[0].Rows[l][k].ToString();
                                    }

                                    if (k == 4)
                                    {
                                        if (fromPlace == fromPlaceeWayBills)
                                        {
                                            if (found > 0)
                                            {
                                                JsonString.Append("},");
                                                sWriter.WriteLine("},");
                                                JsonString.Append("");
                                                sWriter.WriteLine("");
                                            }

                                            JsonString.Append("{");
                                            sWriter.WriteLine("{");

                                            JsonString.Append(" \"" + ds2.Tables[0].Columns[k].ColumnName.ToString() + "\":" + ds2.Tables[0].Rows[l][k].ToString());
                                            sWriter.WriteLine(" \"" + ds2.Tables[0].Columns[k].ColumnName.ToString() + "\":" + ds2.Tables[0].Rows[l][k].ToString());

                                            found = found + 1;
                                        }
                                        else
                                        {
                                            found = 0;
                                        }
                                    }
                                }
                            }
                            JsonString.Append("}");
                            sWriter.WriteLine("}");

                            JsonString.Append("");
                            sWriter.WriteLine("");

                            JsonString.Append("]");
                            sWriter.WriteLine("]");
                            sWriter.WriteLine("");

                            if (i == ds2.Tables[0].Rows.Count - 1)
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
                                 sFileName), System.IO.FileMode.Open);
                        byte[] btFile = new byte[fs.Length];
                        fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                        fs.Close();
                        //Response.AddHeader("Content-disposition", "attachment; filename=" +
                        //                   sGenName);
                        //Response.ContentType = "application/octet-stream";
                        //Response.BinaryWrite(btFile);
                        //Response.End();
                    }
                }
                else
                {
                    //return null;
                    //txt_Jason.Text = JsonString.ToString();
                }

            }
        }

        DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/JasonFiles/"));

        FileInfo[] filesInFolder = directory.GetFiles("*.*", SearchOption.AllDirectories);

        if (filesInFolder.Length > 0)
        {
            foreach (FileInfo fileInfo in filesInFolder)
            {
                checkBoxList.Items.Add(fileInfo.Name);

            }

            ZipFile multipleFiles = new ZipFile();

            Response.AddHeader("Content-Disposition", "attachment; filename=Consolidated_eWay_Bills.zip");

            Response.ContentType = "application/zip";

            foreach (ListItem fileName in checkBoxList.Items)
            {

                if (fileName.Text != "")
                {

                    string filePath = Server.MapPath("~/JasonFiles/" + fileName.Value);

                    multipleFiles.AddFile(filePath, string.Empty);

                }

            }

            multipleFiles.Save(Response.OutputStream);

        }

        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}
