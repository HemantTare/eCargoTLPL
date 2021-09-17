using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

//Author : Ankit champaneriya
//Desc   : Booking register Report
//Date   : 03-01-09

public partial class Finance_Reports_FrmDailyToPayAccounting : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    string Crypt;
    string CallFrom;// = (string)(sender);
    #endregion


     public int RegionID
     {
         get { return Convert.ToInt32(ViewState["_RegionID"]); }
         set 
         {
             ViewState["_RegionID"] = value;
             
         }
     }
     public string RegionText
     {
         get { return Convert.ToString(txtRegion.Text); }
         set 
         {
             txtRegion.Text = value; 
         }
     }
     public int AreaID
     {
         get { return Convert.ToInt32(ViewState["_AreaID"]); }
         set { ViewState["_AreaID"] = value; }
     }
    public string AreaText
     {
         get { return Convert.ToString(txtArea.Text); }
         set
         {
             txtArea.Text = value;
         }
     }
     public int BranchID
     {
         get { return Convert.ToInt32(ViewState["_BranchID"]); }
         set { ViewState["_BranchID"] = value; }
     }
    public string BranchText
     {
         get { return Convert.ToString(txtBranch.Text); }
         set
         {
             txtBranch.Text = value;
         }
     }
    public DateTime AsOnDate
     {
         get { return Convert.ToDateTime(ViewState["_AsOnDate"]); }
         set 
         {
             ViewState["_AsOnDate"] = value;
             txtAsonDate.Text = value.ToString("dd MMM yyyy"); 
         }
     }

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Common objcommon = new Common();
          
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "DailyToPayAccounting";

        if (IsPostBack == false)
        {

            objcommon.SetStandardCaptionForGrid(dg_GridToPay);
            objcommon.SetStandardCaptionForGrid(dg_GridDispatchToPay); 
            Dtp_AsOnDate.SelectedDate = Convert.ToDateTime(System.DateTime.Now.Date); 

            CallFrom = Request.QueryString["CallFrom"];

            if (CallFrom != "DailyBookingStatement")
            {
                CallFrom = "form_and_pageload";
                tab_DailyBookingStatement.Visible = false;
                tab_DailyToPayAccounting.Visible = true;
            }
            else
            {
                tab_DailyBookingStatement.Visible = true;
                tab_DailyToPayAccounting.Visible = false;

                RegionID= Convert.ToInt32(Request.QueryString["RegionID"]);
                //Crypt  = ClassLibraryMVP.Util.DecryptToInt(Crypt);

                RegionText = Request.QueryString["RegionText"];
                //Crypt = ClassLibraryMVP.Util.DecryptToString(Crypt);

                AreaID = Convert.ToInt32(Request.QueryString["AreaID"]);
                //Crypt = ClassLibraryMVP.Util.DecryptToInt(Crypt);

                AreaText = Request.QueryString["AreaText"];
                //Crypt = ClassLibraryMVP.Util.DecryptToString(Crypt);

                BranchID = Convert.ToInt32(Request.QueryString["BranchID"]);
                //Crypt = ClassLibraryMVP.Util.DecryptToInt(Crypt);

                BranchText = Request.QueryString["BranchText"];
                //Crypt = ClassLibraryMVP.Util.DecryptToString(Crypt);

                AsOnDate = Convert.ToDateTime(Request.QueryString["AsOnDate"]);

            
            }

            BindGrid(CallFrom, e);
            //BindGrid("form_and_pageload", e); 
        } 

            Wuc_Region_Area_Branch1.RegionIndexChange += new EventHandler(ClearGrid);
            Wuc_Region_Area_Branch1.AreaIndexChange += new EventHandler(ClearGrid);
            Wuc_Region_Area_Branch1.BranchIndexChange += new EventHandler(ClearGrid);

    }

    private void ClearGrid(object sender, EventArgs e)
    {
        dg_GridToPay.DataSource = null;
        dg_GridToPay.DataBind();
        dg_GridDispatchToPay.DataSource = null;
        dg_GridDispatchToPay.DataBind();
        lblToPay.Visible = false;
        lbl_Paid.Visible = false;
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        
        lbl_Error.Text = "";
        dg_GridToPay.Visible = true;
        dg_GridToPay.CurrentPageIndex = 0;
        dg_GridDispatchToPay.Visible = true;
        dg_GridDispatchToPay.CurrentPageIndex = 0;
        BindGrid("form", e);
        
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        if (CallFrom != "DailyBookingStatement")
        {
            CallFrom = (string)(sender);
        }
        ////string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_GridToPay.CurrentPageIndex;
        int grid_PageSize = dg_GridToPay.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }


        if (CallFrom == "form_and_pageload") return;

        //int RegionID;
        //int AreaID;
        //int BranchID;
        //DateTime AsOnDate;

        //int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        //int Area_Id = Wuc_Region_Area_Branch1.AreaID;
        //int Branch_Id = Wuc_Region_Area_Branch1.BranchID;
        //DateTime AsOnDate = Dtp_AsOnDate.SelectedDate;

        if (CallFrom != "DailyBookingStatement")
        {
            RegionID = Wuc_Region_Area_Branch1.RegionID;
            AreaID = Wuc_Region_Area_Branch1.AreaID;
            BranchID = Wuc_Region_Area_Branch1.BranchID;
            AsOnDate = Dtp_AsOnDate.SelectedDate; 
        }

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@Region_Id", SqlDbType.Int,0,RegionID),
            objDAL.MakeInParams("@Area_Id", SqlDbType.Int,0,AreaID),
            objDAL.MakeInParams("@Branch_Id", SqlDbType.Int,0,BranchID), 
            objDAL.MakeInParams("@AsOnDate", SqlDbType.DateTime,0,AsOnDate),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize) 
        };

        objDAL.RunProc("EC_RPT_Daily_To_Pay_Accounting_GRD", objSqlParam, ref ds);
         
        calculate_totals();
        if (ds.Tables[0].Rows.Count > 0)
        {
            dg_GridToPay.DataSource = ds.Tables[0];
            dg_GridToPay.DataBind();
            lbl_Paid.Visible = true;
        }
        else
        {
            dg_GridToPay.DataSource = null;
            dg_GridToPay.DataBind();
            lbl_Paid.Visible = false;
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            dg_GridDispatchToPay.DataSource = ds.Tables[1];
            dg_GridDispatchToPay.DataBind();
            lblToPay.Visible = true;
        }
        else
        {
            dg_GridDispatchToPay.DataSource = null;
            dg_GridDispatchToPay.DataBind();
            lblToPay.Visible = false; 
        }

         
        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }

    }

    private void PrepareDTForExportToExcel()
    {
        ds.Tables[0].Merge(ds.Tables[1]);
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void dg_GridToPay_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_GridToPay.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void calculate_totals()
    {
     
    }
    #endregion


    protected void dg_GridToPay_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if ((e.Item.Cells[0].Text) == "Todays Booking")
        {
            e.Item.BackColor = System.Drawing.Color.LightGray;
            e.Item.ForeColor = System.Drawing.Color.Black;
            e.Item.Font.Bold = true;
        }
        if ((e.Item.Cells[0].Text) == "Total Stock")
        {
            e.Item.BackColor = System.Drawing.Color.Yellow;
            e.Item.ForeColor = System.Drawing.Color.Black;
            e.Item.Font.Bold = true;
        }
        if ((e.Item.Cells[3].Text) == "ToPay Recovery")
        {  
            e.Item.Font.Bold = true;
        }
        

    }
    protected void dg_GridDispatchToPay_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType== ListItemType.AlternatingItem) //== DataControlRowType.DataRow)            
        {
            int Region_Id = Wuc_Region_Area_Branch1.RegionID;
            int Area_Id = Wuc_Region_Area_Branch1.AreaID;
            int Branch_Id = Wuc_Region_Area_Branch1.BranchID;

            if (CallFrom != "DailyBookingStatement")
            {
                 RegionText = Wuc_Region_Area_Branch1.SelectedRegionText;
                 AreaText = Wuc_Region_Area_Branch1.SelectedAreaText;
                 BranchText = Wuc_Region_Area_Branch1.SelectedBranchText;
                 AsOnDate = Dtp_AsOnDate.SelectedDate; 
            }
            HiddenField hdn_Dest_Branch_ID;
            hdn_Dest_Branch_ID = (HiddenField)e.Item.FindControl("hdn_Dest_Branch_ID");
 
            LinkButton lbtn_DirectToPay;
            lbtn_DirectToPay = (LinkButton)e.Item.FindControl("lbtn_DirectToPay");
            lbtn_DirectToPay.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(Region_Id) + "','" + ClassLibraryMVP.Util.EncryptInteger(Area_Id) + "','" + ClassLibraryMVP.Util.EncryptInteger(Branch_Id) + "','" + ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(hdn_Dest_Branch_ID.Value)) + "','" + ClassLibraryMVP.Util.EncryptString(lbtn_DirectToPay.Text) + "','" + AsOnDate + "','" + ClassLibraryMVP.Util.EncryptString(RegionText) + "','" + ClassLibraryMVP.Util.EncryptString(AreaText) + "','" + ClassLibraryMVP.Util.EncryptString(BranchText) + "')");

        }
        if ((e.Item.Cells[0].Text) == "Total To Pay Recovery Stock")
        {
            e.Item.BackColor = System.Drawing.Color.Yellow;
            e.Item.ForeColor = System.Drawing.Color.Black;
            e.Item.Font.Bold = true;
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


}
