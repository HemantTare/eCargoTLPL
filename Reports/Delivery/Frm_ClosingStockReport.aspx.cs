using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Text;

//Author : Ankit champaneriya
//Desc   : Booking register Report
//Date   : 03-01-09

public partial class Reports_Delivery_Frm_ClosingStockReport : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds, ds1, ds2;  
    int TotalLR, TotalPkgs, TotalToPay, TotalPDS;

    string Description, Query;

    DateTime AsOnDate; 
    LinkButton lbtn_Description;
    Common objcommon = new Common();

    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {   
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid); 
        Wuc_Export_To_Excel1.FileName = "ClosingStockReport";

        if (IsPostBack == false)
        { 
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_GridClosingStock);
            objcommon.SetStandardCaptionForGrid(dg_GridPendingPDS);

            Dtp_AsOnDate.SelectedDate = Convert.ToDateTime(System.DateTime.Now.Date);

            Fill_Branch();
            BindGrid("form_and_pageload", e); 
        }
    }
    private void Fill_Branch()
    {
        ddl_Branch.Items.Clear();
        Query = "";
        if (UserManager.getUserParam().HierarchyCode == "RO")
        {
            Query = "select branch_ID,branch_name from ec_master_branch ";
            Query = Query + " inner join ec_master_region r on b.region_id = r.region_id ";
            Query = Query + " where b.is_active = 1";
            Query = Query + " and r.region_id = " + UserManager.getUserParam().MainId;
            Query = Query + " order by branch_name";
        }
        else if (UserManager.getUserParam().HierarchyCode == "AO")
        {
            Query = "select branch_ID,branch_name from ec_master_branch ";
            Query = Query + " inner join ec_master_area a on b.area_id = a.area_id ";
            Query = Query + " where b.is_active = 1";
            Query = Query + " and a.area_id = " + UserManager.getUserParam().MainId;
            Query = Query + " order by branch_name";
        
        }
        else if (UserManager.getUserParam().HierarchyCode == "BO")
        {
            Query = "select branch_ID,branch_name from ec_master_branch where is_active = 1 ";
            Query = Query + " and branch_id = " + UserManager.getUserParam().MainId;
            Query = Query + " order by branch_name";
        }
        else
        { 
            Query = "select branch_ID,branch_name from ec_master_branch where is_active = 1 ";
            Query = Query + " order by branch_name";
        
        } 
            ddl_Branch.DataValueField = "branch_ID";
            ddl_Branch.DataTextField = "branch_name";
            ddl_Branch.DataSource = objcommon.EC_Common_Pass_Query(Query);
            ddl_Branch.DataBind(); 

            //ddl_Branch.Items.Insert(0, new ListItem("All", "0"));
 
 
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon(); 
         
            lbl_Error.Text = "";
            dg_GridClosingStock.Visible = true;
            dg_GridClosingStock.CurrentPageIndex = 0;
            dg_GridPendingPDS.Visible = true;
            dg_GridPendingPDS.CurrentPageIndex = 0;
        
        if (Convert.ToInt32(ddl_Branch.SelectedValue) > 0)
        {
            BindGrid("form", e);
        }
        else
        {
            lbl_Error.Text = "Please Select One Branch";
            dg_GridClosingStock.DataSource = null;
            dg_GridClosingStock.DataBind();
            lbl_ClosingStock.Visible = false;

            dg_GridPendingPDS.DataSource = null;
            dg_GridPendingPDS.DataBind();
            lblPendingPDS.Visible = false;
        }

    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom= (string)(sender);

        int grid_currentpageindex = dg_GridClosingStock.CurrentPageIndex;
        int grid_PageSize = dg_GridClosingStock.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }
         
        DateTime AsOnDate = Dtp_AsOnDate.SelectedDate;
       
            SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int,0,Convert.ToInt32(ddl_Branch.SelectedValue)),
            objDAL.MakeInParams("@As_on_Date", SqlDbType.DateTime,0,AsOnDate)   
            };

            objDAL.RunProc("EC_RPT_ClosingStockReport", objSqlParam, ref ds);


            if (CallFrom == "form_and_pageload") return;


            //calculate_totals();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dg_GridClosingStock.DataSource = ds.Tables[0];
                dg_GridClosingStock.DataBind();
                lbl_ClosingStock.Visible = true;
            }
            else
            {
                dg_GridClosingStock.DataSource = null;
                dg_GridClosingStock.DataBind();
                lbl_ClosingStock.Visible = false;
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                dg_GridPendingPDS.DataSource = ds.Tables[1];
                dg_GridPendingPDS.DataBind();
                lblPendingPDS.Visible = true;
            }
            else
            {
                dg_GridPendingPDS.DataSource = null;
                dg_GridPendingPDS.DataBind();
                lblPendingPDS.Visible = false;
            }


            if (CallFrom == "exporttoexcelusercontrol")
            {
                PrepareDTForExportToExcel();
            } 

    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow(); 
        DataRow dr1 = ds.Tables[0].NewRow(); 

        dr["Description"] = " "; 
        ds.Tables[0].Rows.Add(dr);

        dr1["Description"] = "PDS Date";
        dr1["TotalLR"] = "No Of PDS";
        ds.Tables[0].Rows.Add(dr1);  

        ds.Tables[0].Merge(ds.Tables[1]); 

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
        
    }

    protected void dg_GridClosingStock_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_GridClosingStock.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    protected void dg_GridClosingStock_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton lbtn_Description;
        HiddenField hdn_SrNo;

        if (e.Item.ItemIndex != -1)  
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                lbtn_Description = (LinkButton)e.Item.FindControl("lbtn_Description");
                hdn_SrNo = (HiddenField)e.Item.FindControl("hdn_SrNo");
                
                int Branch_ID = Convert.ToInt32(ddl_Branch.SelectedValue);
                DateTime AsOnDate = Dtp_AsOnDate.SelectedDate;


                if (hdn_SrNo.Value == "1")
                {
                    e.Item.Cells[0].Text = "Opening Stock"; 
                } 
                else if (hdn_SrNo.Value == "2")
                {
                    StringBuilder Path = new StringBuilder(Util.GetBaseURL());
                    Path.Append("/");
                    Path.Append("Operations/Delivery/FrmClosingStkRptDetails.aspx?SrNo=" + ClassLibraryMVP.Util.EncryptInteger(2) + "&Branch_ID=" + ClassLibraryMVP.Util.EncryptInteger(Branch_ID) + "&AsOnDate=" + AsOnDate);
                    lbtn_Description.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "')");
                }
                else if (hdn_SrNo.Value == "3")
                {
                    e.Item.Cells[0].Text = "Delivered";
                }
                else if (hdn_SrNo.Value == "4")
                {
                    StringBuilder Path = new StringBuilder(Util.GetBaseURL());
                    Path.Append("/");
                    Path.Append("Operations/Delivery/FrmClosingStkRptDetails.aspx?SrNo=" + ClassLibraryMVP.Util.EncryptInteger(4) + "&Branch_ID=" + ClassLibraryMVP.Util.EncryptInteger(Branch_ID) + "&AsOnDate=" + AsOnDate);
                    lbtn_Description.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "')");
                }
                else if (hdn_SrNo.Value == "5")
                {
                    StringBuilder Path = new StringBuilder(Util.GetBaseURL());
                    Path.Append("/");
                    Path.Append("Operations/Delivery/FrmClosingStkRptDetails.aspx?SrNo=" + ClassLibraryMVP.Util.EncryptInteger(5) + "&Branch_ID=" + ClassLibraryMVP.Util.EncryptInteger(Branch_ID) + "&AsOnDate=" + AsOnDate);
                    lbtn_Description.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "')");
                }
                else if (hdn_SrNo.Value == "6")
                {
                    StringBuilder Path = new StringBuilder(Util.GetBaseURL());
                    Path.Append("/");
                    Path.Append("Operations/Delivery/FrmClosingStkRptDetails.aspx?SrNo=" + ClassLibraryMVP.Util.EncryptInteger(6) + "&Branch_ID=" + ClassLibraryMVP.Util.EncryptInteger(Branch_ID) + "&AsOnDate=" + AsOnDate);
                    lbtn_Description.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "')");
                }
                else if (hdn_SrNo.Value == "7")
                {
                    e.Item.Cells[0].Text = "    Total : "; 
                }  
            }
        } 
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[2].Rows[0];
        Description = dr["Description"].ToString();
        TotalLR = Util.String2Int(dr["TotalLR"].ToString());
        TotalPkgs = Util.String2Int(dr["TotalPkgs"].ToString());
        TotalToPay = Util.String2Int(dr["TotalToPay"].ToString()); 

    }
    #endregion
 

    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    
    protected void dg_GridPendingPDS_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        //if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        //{
        //    System.Web.UI.WebControls.Label lbl_PDS_Date, lbl_TotalPDS;

        //    lbl_PDS_Date = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_PDS_Date");
        //    lbl_TotalPDS = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalPDS");

        //    lbl_PDS_Date.Text = PDS_Date.ToString();
        //    lbl_TotalPDS.Text = TotalPDS.ToString(); 

        //}
    }

}
