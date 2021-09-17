using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Raj.EC;

//Author : Ankit champaneriya
//Desc   : Booking register Report
//Date   : 03-01-09

public partial class Reports_Booking_FrmClientWiseBookingSummary : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds,dsClientGroup;

    int TotLR, Tot_Art, SubTotal, TotST, TotRoundOff, TotGCAmt, TotDiscount;
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        if (rdl_ConsignorConsignee.SelectedValue == "1")
            Wuc_Export_To_Excel1.FileName = "ConsignorRegister";
        else
            Wuc_Export_To_Excel1.FileName = "ConsigneeRegister";


        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);

            //BindGrid("form_and_pageload", e);

            Fill_ClientGroup();
             
        }
    }
    
    protected void btn_view_Click(object sender, EventArgs e)
    {
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
        string CallFrom= (string)(sender);

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
        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@IsConsignorWise", SqlDbType.Int,0,Convert.ToBoolean(Convert.ToInt32(rdl_ConsignorConsignee.SelectedValue))),
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@ToDate", SqlDbType.DateTime,0,To_Date),
            objDAL.MakeInParams("@StartDate", SqlDbType.DateTime,0,Convert.ToDateTime(From_Date)),
            objDAL.MakeInParams("@EndDate", SqlDbType.DateTime,0,Convert.ToDateTime(To_Date)), 
            objDAL.MakeInParams("@Name", SqlDbType.VarChar,100,txtName.Text), 
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID), 
            objDAL.MakeInParams("@CallFrom",SqlDbType.Int,0,1),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize), 
            objDAL.MakeInParams("@Client_Group_Id",SqlDbType.Int,0,ddl_ClientGroup.SelectedValue), 
            objDAL.MakeInParams("@MobileNo",SqlDbType.VarChar,20,txtMobileNo.Text)  
        };


        //if (rdl_ConsignorConsignee.SelectedValue == "0")

        objDAL.RunProc("EC_RPT_Clientwise_Monthly_Booking_Summary_GRD", objSqlParam, ref ds);
         

        if (CallFrom == "form_and_pageload") return;

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[2].Rows[0][0].ToString();
        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom,lbl_Error,TotalRecords);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }          
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["ClientName"] = "Total";
        
        dr["TotLR"] = TotLR;
        dr["Tot_Art"] = Tot_Art;
        dr["SubTotal"] = SubTotal;
        dr["TotST"] = TotST;
        dr["TotRoundOff"] = TotRoundOff;
        dr["TotGCAmt"] = TotGCAmt;
        dr["TotDiscount"] = TotDiscount; 
        ds.Tables[0].Rows.Add(dr);

        ds.Tables[0].Columns.Remove("SRNO");
        ds.Tables[0].Columns.Remove("MONTHID");
        ds.Tables[0].Columns.Remove("MONTHNAME");
        ds.Tables[0].Columns.Remove("YearID");
        ds.Tables[0].Columns.Remove("IsConsignorWise"); 

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];

        TotLR = Util.String2Int(dr["TotalTotLR"].ToString());
        Tot_Art = Util.String2Int(dr["TotalTot_Art"].ToString());
        SubTotal = Util.String2Int(dr["TotalSubTotal"].ToString());
        TotST = Util.String2Int(dr["TotalTotST"].ToString());
        TotRoundOff = Util.String2Int(dr["TotalTotRoundOff"].ToString());
        TotGCAmt = Util.String2Int(dr["TotalTotGCAmt"].ToString());
        TotDiscount = Util.String2Int(dr["TotalTotDiscount"].ToString()); 
        
    }
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_TotLR, lbl_Tot_Art, lbl_SubTotal;
            System.Web.UI.WebControls.Label lbl_TotST, lbl_TotRoundOff, lbl_TotGCAmt, lbl_TotDiscount; 
 
            lbl_TotLR = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotLR");
            lbl_Tot_Art = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Tot_Art");
            lbl_SubTotal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SubTotal");
            lbl_TotST = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotST");
            lbl_TotRoundOff = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotRoundOff");
            lbl_TotGCAmt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotGCAmt");
            lbl_TotDiscount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotDiscount"); 
            
            lbl_TotLR.Text = TotLR.ToString();
            lbl_Tot_Art.Text = Tot_Art.ToString();
            lbl_SubTotal.Text = SubTotal.ToString();
            lbl_TotST.Text = TotST.ToString();
            lbl_TotRoundOff.Text = TotRoundOff.ToString();
            lbl_TotGCAmt.Text = TotGCAmt.ToString();
            lbl_TotDiscount.Text = TotDiscount.ToString(); 
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int RegionID, AreaID, BranchID, Client_ID;
            Boolean IsConsignorWise;
            LinkButton lnk_ClientName;

            RegionID = Wuc_Region_Area_Branch1.RegionID;
            AreaID = Wuc_Region_Area_Branch1.AreaID;
            BranchID = Wuc_Region_Area_Branch1.BranchID;
            IsConsignorWise = Convert.ToBoolean(Convert.ToInt32(rdl_ConsignorConsignee.SelectedValue));
            DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
            DateTime To_Date = Wuc_From_To_Datepicker1.SelectedToDate;
           
            int Division_ID = WucDivisions1.Division_ID;

            //Client_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Client_ID").ToString());
            DateTime StartDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "StartDate").ToString());
            DateTime EndDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "EndDate").ToString());

            lnk_ClientName = (LinkButton)e.Item.FindControl("lnk_ClientName");
            string Name = lnk_ClientName.Text;
            lnk_ClientName.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(RegionID) + "','" + ClassLibraryMVP.Util.EncryptInteger(AreaID) + "','" + ClassLibraryMVP.Util.EncryptInteger(BranchID) + "','" + IsConsignorWise + "','" + From_Date + "','" + To_Date + "','" + StartDate + "','" + EndDate + "','" + ClassLibraryMVP.Util.EncryptString(Name) + "','" + ClassLibraryMVP.Util.EncryptInteger(Division_ID) + "','" + ClassLibraryMVP.Util.EncryptString(lnk_ClientName.Text) + "')");
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

    private void Fill_ClientGroup()
    {
        DAL objDAL = new DAL();

        objDAL.RunProc("EC_Master_Fill_ClientGroup",  ref dsClientGroup);

        ddl_ClientGroup.DataSource = dsClientGroup ;
        ddl_ClientGroup.DataTextField = "Client_Group_Name";
        ddl_ClientGroup.DataValueField = "Client_Group_ID";
        ddl_ClientGroup.DataBind();


        ddl_ClientGroup.Items.Insert(0, new ListItem("All", "0"));

    }

}
