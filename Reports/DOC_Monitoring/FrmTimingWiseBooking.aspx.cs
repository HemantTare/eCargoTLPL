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

public partial class Reports_DOC_Monitoring_FrmTimingWiseBooking : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;  
    string Crypt;   
    #endregion 
     

    public int Region_Id
    {
        get { return Convert.ToInt32(ViewState["_RegionID"]); }
        set { ViewState["_RegionID"] = value; }
    }
    public int Area_Id
    {
        get { return Convert.ToInt32(ViewState["_AreaID"]); }
        set { ViewState["_AreaID"] = value; }
    }
    public int Branch_Id
    {
        get { return Convert.ToInt32(ViewState["_BranchID"]); }
        set { ViewState["_BranchID"] = value; }
    }
    public string From_Date
    {
        get { return Convert.ToString(ViewState["_From_Date"]); }
        set { ViewState["_From_Date"] = value; }
    }
    public string To_Date
    {
        get { return Convert.ToString(ViewState["_To_Date"]); }
        set { ViewState["_To_Date"] = value; }
    }
    public string RegionText
    {
        get { return Convert.ToString(ViewState["_RegionText"]); }
        set
        {
            ViewState["_RegionText"] = value; 
        }
    }
    public string AreaText
    {
        get { return Convert.ToString(ViewState["_AreaText"]); }
        set
        {
            ViewState["_AreaText"] = value;
        }
    }
    public string BranchText
    {
        get { return Convert.ToString(ViewState["_BranchText"]); }
        set
        {
            ViewState["_BranchText"] = value;
        }
    }
    public string link_Text
    {
        get { return Convert.ToString(ViewState["_link_Text"]); }
        set
        {
            ViewState["_link_Text"] = value;
        }
    }

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "TimingWiseBooking";

        if (IsPostBack == false)
        {
            Crypt = Request.QueryString["Region_Id"];
            Region_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["Area_Id"];
            Area_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["Branch_Id"];
            Branch_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            From_Date = Request.QueryString["From_Date"];
            To_Date = Request.QueryString["To_Date"];

            Crypt = Request.QueryString["RegionText"];
            RegionText = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["AreaText"];
            AreaText = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["BranchText"];
            BranchText = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["link_Text"];
            link_Text = ClassLibraryMVP.Util.DecryptToString(Crypt); 
 
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);

            BindGrid("form_and_pageload", e); 
        }
    }
    
    //protected void btn_view_Click(object sender, EventArgs e)
    //{
        //DateCommon objDateCommon = new DateCommon();
        //string msg = "";
        //if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        //{
        //    lbl_Error.Text = "";
        //    dg_Grid.Visible = true;
        //    dg_Grid.CurrentPageIndex = 0;
        //    BindGrid("form", e);
        //}
        //else
        //{
        //    lbl_Error.Text = msg;
        //    dg_Grid.Visible = false;
        //}
    //}

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

        //int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        //int Area_id = Wuc_Region_Area_Branch1.AreaID;
        //int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        //DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        //DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate; 
        
        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_Id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_Id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_Date ", SqlDbType.DateTime,0,From_Date),  
            objDAL.MakeInParams("@link_Text",SqlDbType.VarChar,50,link_Text),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize) 
        };



        objDAL.RunProc("[EC_RPT_Timing_Wise_Booking_Register_GRD]", objSqlParam, ref ds);

        //if (CallFrom == "form_and_pageload") return;

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[1].Rows[0][0].ToString();
       
        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom,lbl_Error,TotalRecords);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }          
    }

    private void PrepareDTForExportToExcel()
    { 

        ds.Tables[0].Columns.Remove("Sr No.");
        ds.Tables[0].Columns.Remove("GC_ID"); 

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

     
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        //if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        //{
        //    System.Web.UI.WebControls.Label lbl_ChargedWeight, lbl_ActualWeight, lbl_Articles, lbl_BasicFreight, lbl_Discount;
        //    System.Web.UI.WebControls.Label lbl_FOVCharges, lbl_ODACharges, lbl_OtherCharges, lbl_SubFreight, lbl_STaxAmt;
        //    System.Web.UI.WebControls.Label lbl_TotalFreight, lbl_InvoiceValue, lbl_HamaliCharge, lbl_DDCharge, lbl_BiltiCharges;

        //    lbl_ChargedWeight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ChargedWeight");
        //    //lbl_ActualWeight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ActualWeight");
        //    lbl_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Articles");
        //    lbl_BasicFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_BasicFreight");
        //    lbl_Discount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Discount");
        //    lbl_FOVCharges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_FOVCharges");
        //    lbl_ODACharges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ODACharges");
        //    lbl_OtherCharges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_OtherCharges");
        //    lbl_SubFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SubFreight");
        //    lbl_STaxAmt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_STaxAmt");
        //    lbl_TotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalFreight");
        //    lbl_InvoiceValue = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_InvoiceValue");
        //    lbl_HamaliCharge = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_HamaliCharge");
        //    lbl_DDCharge = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_DDCharge");
        //    lbl_BiltiCharges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_BiltiCharges");
           

        //    lbl_ChargedWeight.Text = ChargedWeight.ToString();
        //    //lbl_ActualWeight.Text = Actual_Weight.ToString();
        //    lbl_Articles.Text = Articles.ToString();
        //    lbl_BasicFreight.Text = Basic_Freight.ToString();
        //    lbl_Discount.Text = Discount.ToString();
        //    lbl_FOVCharges.Text = FOV_Charges.ToString();
        //    lbl_ODACharges.Text = ODA_Charges.ToString();
        //    lbl_OtherCharges.Text = Other_Charges.ToString();
        //    lbl_SubFreight.Text = Sub_Freight.ToString();
        //    lbl_STaxAmt.Text = STax_Amt.ToString();
        //    lbl_TotalFreight.Text =Total_Freight.ToString();
        //    lbl_InvoiceValue.Text =Invoice_Value.ToString();
        //    lbl_HamaliCharge.Text =Hamali_Charge.ToString();
        //    lbl_DDCharge.Text =DD_Charge.ToString();
        //    lbl_BiltiCharges.Text =Bilti_Charges.ToString();
        //}
        //if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        //{
        //    int GC_ID;
        //    LinkButton lnk_GC_No;

        //    GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());
        //    lnk_GC_No = (LinkButton)e.Item.FindControl("lnk_GC_No");

        //    lnk_GC_No.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(GC_ID) + "')");
        //}
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
