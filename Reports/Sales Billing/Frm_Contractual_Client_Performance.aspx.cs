using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
public partial class Reports_Sales_Billing_Frm_Contractual_Client_Performance : System.Web.UI.Page
{
    
    private DataSet ds;
    private DAL objDAL = new DAL();
    int Region_Id, Area_Id, Branch_Id, Division_ID ;
    DateTime From_Date, To_date;
    string str;
    string Append_Client = "";
    decimal weight, freight, promised_wt, promised_fr, overdue, total_rec;

  
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "ContractualClientPerformance";
        lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
        lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

     }

    protected void btn_Fill_Client_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";
            ChkBoxLst_Client.Visible = true;
            Fill_Client();
        }
        else
        {
            lbl_Error.Text = msg;
            ChkBoxLst_Client.Visible = false;
        }
    }
    
    protected void Fill_Client()
    {
        ds = Get_Client_Names(Region_Id, Area_Id, Branch_Id);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ChkBoxLst_Client.Visible = true;
            lbl_Error.Text = "";
            ChkBoxLst_Client.DataSource = ds;
            ChkBoxLst_Client.DataTextField = "Client_Name";
            ChkBoxLst_Client.DataValueField = "client_Id";
            ChkBoxLst_Client.DataBind();
            Session["Client_Id"] = ds;
        }
        else
        {
            ChkBoxLst_Client.Visible = false;
            lbl_Error.Text = "No Clients Found";
        }
        //str = ds.GetXml();
    }
    
    protected DataSet Get_Client_Names(int region_id, int area_id, int branch_id)
    {
        Region_Id = Wuc_Region_Area_Branch1.RegionID;
        Area_Id = Wuc_Region_Area_Branch1.AreaID;
        Branch_Id = Wuc_Region_Area_Branch1.BranchID;
        From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,region_id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),          
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),
          };
        objDAL.RunProc("[EC_Rpt_get_Contractual_Client_Performance]", objSqlParam, ref ds);
        return ds;
    }
    
    protected string Check_Selected_Client()
    {    

        //for (int i = 0; i <= ChkBoxLst_Client.Items.Count - 1; i++)
        //{
        //    if (ChkBoxLst_Client.Items[i].Selected == true)
        //    {
        //        if (Append_Client == "")
        //        {
        //            Append_Client = "'" + Convert.ToString(ChkBoxLst_Client.Items[i].Value) + "'";
        //        }
        //        else
        //        {
        //            Append_Client = Append_Client + "," + "'" + Convert.ToString(ChkBoxLst_Client.Items[i].Value) + "'";
        //        }
        //    }
        //}
        //Append_Client = Append_Client;
        //return Append_Client;

        DataSet _objDs = new DataSet();
        //_objDs.Tables.Add(SessionRequiredForms.Copy());

        DataTable dt = null;
        dt = new DataTable();
        dt.Columns.Add("client_ID");
        DataRow dr;
        for (int i = 0; i <= ChkBoxLst_Client.Items.Count - 1; i++)
        {
            if (ChkBoxLst_Client.Items[i].Selected == true)
            {
                dr = dt.NewRow();
                dr["client_ID"] = ChkBoxLst_Client.Items[i].Value;
                dt.Rows.Add(dr);
            }
        }

        _objDs.Tables.Add(dt);

        _objDs.Tables[0].TableName = "Client_Details";
        return _objDs.GetXml().ToLower();

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
        Region_Id = Wuc_Region_Area_Branch1.RegionID;
        Area_Id = Wuc_Region_Area_Branch1.AreaID;
        Branch_Id = Wuc_Region_Area_Branch1.BranchID;

        From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        Session["Client_Id"] = Check_Selected_Client();

        Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_Id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_Id),              
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),          
            objDAL.MakeInParams("@Is_Status", SqlDbType.Int,0,Rbtn_type.SelectedValue),    
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),
            objDAL.MakeInParams("@Xml",SqlDbType.Xml ,0,Check_Selected_Client()),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("[Ec_Rpt_Contractual_Client_performance]", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();
        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }
    
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        total_rec = Util.String2Decimal(dr["Total Recievable"].ToString());
        overdue = Util.String2Decimal(dr["Overdue"].ToString());
        weight = Util.String2Decimal(dr["Weight"].ToString());
        freight = Util.String2Decimal(dr["Freight"].ToString());
        promised_wt= Util.String2Decimal(dr["Promissed Weight"].ToString());
        promised_fr = Util.String2Decimal(dr["Promissed Freight"].ToString());
    }

    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total, lbl_Total_recievable,lbl_overdue,lbl_weight,lbl_freight,
                lbl_promised_wt,lbl_promised_fr;

            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_Total_recievable = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_recievable");
            lbl_overdue = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Overdue");
            lbl_weight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Weight");
            lbl_freight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Freight");
            lbl_promised_wt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Promissed_Wt");
            lbl_promised_fr = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Promissed_Freight");

            lbl_Total.Text = "Total";
            lbl_Total_recievable.Text = total_rec.ToString();
            lbl_overdue.Text = overdue.ToString();
            lbl_weight.Text = weight.ToString();
            lbl_freight.Text=freight.ToString();
            lbl_promised_wt.Text=promised_wt.ToString();
            lbl_promised_fr.Text=promised_fr.ToString();
        }
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    private void PrepareDTForExportToExcel()
    {
        DataRow dr;
        dr = ds.Tables[0].NewRow();
        dr["Client Name"] = "Total";
        dr["Total Recievable"] = total_rec.ToString();
        dr["Overdue"] = overdue.ToString();
        dr["weight"] = weight.ToString();
        dr["freight"] = freight.ToString();
        dr["Promissed Weight"] = promised_wt.ToString();
        dr["Promissed Freight"] = promised_fr.ToString();
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
