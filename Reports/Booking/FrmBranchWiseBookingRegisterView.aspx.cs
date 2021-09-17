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

public partial class Reports_Booking_FrmBranchWiseBookingRegister : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal ChargedWeight, Actual_Weight, Articles, Basic_Freight, FOV_Charges, Discount;
    decimal ODA_Charges,Other_Charges,Sub_Freight,STax_Amt,Total_Freight,Invoice_Value;
    decimal Hamali_Charge, DD_Charge, Bilti_Charges, Local_Charges;
    int TotalGC;
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        if (rdl_BookingRegister.SelectedValue == "0")
            Wuc_Export_To_Excel1.FileName = "BranchWiseBookingRegister";
        else
            Wuc_Export_To_Excel1.FileName = "DeliveryBranchWiseBookingRegister";


        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);

            BindGrid("form_and_pageload", e);
            WucFilter1.setddldatasource(ds);
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
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        int BookingTypeId = Wuc_GC_Parameters1.Booking_Type_ID;
        int DeliveryTypeId = Wuc_GC_Parameters1.Delivery_Type_ID;
        int PaymentTypeId = Wuc_GC_Parameters1.Payment_Type_ID;
        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@Booking_Type_Id", SqlDbType.Int,0,BookingTypeId),
            objDAL.MakeInParams("@Delivery_Type_Id", SqlDbType.Int,0,DeliveryTypeId),
            objDAL.MakeInParams("@Payment_Type_Id",SqlDbType.Int,0,PaymentTypeId),
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),

            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),
            objDAL.MakeInParams("@colid",SqlDbType.Int,0,WucFilter1.colid),
            objDAL.MakeInParams("@datatype_id",SqlDbType.Int,0,WucFilter1.Datatype_ID),
            objDAL.MakeInParams("@criteria_id",SqlDbType.Int,0,WucFilter1.criteriaid),
            objDAL.MakeInParams("@Filtered_Text",SqlDbType.VarChar,50,WucFilter1.Filtered_Text),
            objDAL.MakeInParams("@Filtered_Date",SqlDbType.DateTime,0,WucFilter1.Filtered_Date),
            objDAL.MakeInParams("@Filtered_Bit",SqlDbType.Bit,0,WucFilter1.Filtered_bit)
        };


        if (rdl_BookingRegister.SelectedValue == "0")
        {
            objDAL.RunProc("[EC_RPT_Branchwise_Booking_Register_GRD]", objSqlParam, ref ds);
        }
        else
        {
            objDAL.RunProc("[EC_RPT_Del_Branchwise_Booking_Register_GRD]", objSqlParam, ref ds);
        }

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
        dr["Pay Mode"] = "Total";
        dr["Charged Weight"] = ChargedWeight;
        //dr["Actual Weight"] = Actual_Weight;
        dr["Articles"] = Articles;
        dr["Basic Freight"] = Basic_Freight;
        dr["Discount"] = Discount;
        dr["FOV Charges"] = FOV_Charges;
        dr["ODA Charges"] = ODA_Charges;
        dr["Other Charges"] = Other_Charges;
        dr["Sub Freight"] = Sub_Freight;
        dr["STax Amt"] = STax_Amt;
        dr["Total Freight"] = Total_Freight;
        dr["Invoice Value"] = Invoice_Value;
        dr["Hamali Charge"] = Hamali_Charge;
        dr["DD Charge"] = DD_Charge;
        dr["Local Charges"] = Local_Charges;
        dr["Bilti Charges"] = Bilti_Charges;
        ds.Tables[0].Rows.Add(dr);

        ds.Tables[0].Columns.Remove("Sr No.");
        ds.Tables[0].Columns.Remove("Region Name");
        ds.Tables[0].Columns.Remove("Area Name");
        ds.Tables[0].Columns.Remove("From Location Name");
        ds.Tables[0].Columns.Remove("Cnee Tel No");
        ds.Tables[0].Columns.Remove("Cnr Tel No");
        ds.Tables[0].Columns.Remove("Booking Mode"); 
        //ds.Tables[0].Columns.Remove("Local Charges");
        ds.Tables[0].Columns.Remove("ODA Charges");
        ds.Tables[0].Columns.Remove("Other Charges");
        ds.Tables[0].Columns.Remove("Sub Freight");
        //ds.Tables[0].Columns.Remove("DD Charge");
        ds.Tables[0].Columns.Remove("Bilti Charges");
        ds.Tables[0].Columns.Remove("GC_ID");
        ds.Tables[0].Columns.Remove("Dly Branch");

        ds.Tables[0].Columns["gc_caption Date"].SetOrdinal(1);
        ds.Tables[0].Columns["gc_caption No"].SetOrdinal(2);
        ds.Tables[0].Columns["Bkg Branch"].SetOrdinal(3);
        ds.Tables[0].Columns["Dly Location Name"].SetOrdinal(4);
        ds.Tables[0].Columns["Cnr Name"].SetOrdinal(5);
        ds.Tables[0].Columns["Cnee Name"].SetOrdinal(6);
        ds.Tables[0].Columns["DlyArea"].SetOrdinal(7);
        ds.Tables[0].Columns["IsNewClient"].SetOrdinal(8);
        ds.Tables[0].Columns["Articles"].SetOrdinal(9);
        ds.Tables[0].Columns["Description"].SetOrdinal(10);
        ds.Tables[0].Columns["Size"].SetOrdinal(11);
        ds.Tables[0].Columns["Total Freight"].SetOrdinal(12);
        ds.Tables[0].Columns["Pay Mode"].SetOrdinal(13);
        ds.Tables[0].Columns["eWayBillNo"].SetOrdinal(14);
        ds.Tables[0].Columns["Created_On"].SetOrdinal(15);
        ds.Tables[0].Columns["Updated_On"].SetOrdinal(16);
        ds.Tables[0].Columns["BillingClient"].SetOrdinal(17);
        ds.Tables[0].Columns["WholeSalerBroker"].SetOrdinal(18);
        ds.Tables[0].Columns["Cnr_Mobile_No"].SetOrdinal(19);
        ds.Tables[0].Columns["ConsignorGSTNo"].SetOrdinal(20);
        ds.Tables[0].Columns["Cne_Mobile_No"].SetOrdinal(21);
        ds.Tables[0].Columns["ConsigneeGSTNo"].SetOrdinal(22);
        ds.Tables[0].Columns["Basic Freight"].SetOrdinal(23);
        ds.Tables[0].Columns["Discount"].SetOrdinal(24);
        ds.Tables[0].Columns["Hamali Charge"].SetOrdinal(25);
        ds.Tables[0].Columns["FOV Charges"].SetOrdinal(26);
        ds.Tables[0].Columns["Invoice Value"].SetOrdinal(27);
        ds.Tables[0].Columns["STax Amt"].SetOrdinal(28);

        ds.Tables[0].Columns["Charged Weight"].SetOrdinal(29);

        ds.Tables[0].Columns["STax Amt"].ColumnName = "GST";
	

        //ds.Tables[0].Columns["STax Amt"].SetOrdinal(24);
        //ds.Tables[0].Columns["Bkg Branch Name"].SetOrdinal(25); 

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
        ChargedWeight = Util.String2Decimal(dr["Total_Charged_Wt"].ToString());
        //Actual_Weight = Util.String2Decimal(dr["Total_Actual_Wt"].ToString());
        Articles = Util.String2Decimal(dr["Total_Articles"].ToString());
        Basic_Freight = Util.String2Decimal(dr["Total_Basic_Freight"].ToString());
        Discount = Util.String2Decimal(dr["Discount"].ToString());
        FOV_Charges = Util.String2Decimal(dr["Total_Fov_Charge"].ToString());
        ODA_Charges = Util.String2Decimal(dr["Total_ODA_Charge"].ToString());
        Other_Charges = Util.String2Decimal(dr["Total_Other_Charge"].ToString());
        Sub_Freight = Util.String2Decimal(dr["Total_Sub_Freight"].ToString());
        STax_Amt = Util.String2Decimal(dr["Total_Service_Tax"].ToString());
        Total_Freight = Util.String2Decimal(dr["Total_Freight"].ToString());
        Invoice_Value = Util.String2Decimal(dr["Total_Invoice_Value"].ToString());
        Hamali_Charge = Util.String2Decimal(dr["Total_Hamali_Charge"].ToString());
        DD_Charge = Util.String2Decimal(dr["Total_DD_Charge"].ToString());
        Local_Charges = Util.String2Decimal(dr["Local_Charges"].ToString());
        Bilti_Charges = Util.String2Decimal(dr["Total_Bilti_Charges"].ToString());
        
    }
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_ChargedWeight, lbl_ActualWeight, lbl_Articles, lbl_BasicFreight, lbl_Discount;
            System.Web.UI.WebControls.Label lbl_FOVCharges, lbl_ODACharges, lbl_OtherCharges, lbl_SubFreight, lbl_STaxAmt;
            System.Web.UI.WebControls.Label lbl_TotalFreight, lbl_InvoiceValue, lbl_HamaliCharge, lbl_DDCharge, lbl_BiltiCharges, lbl_LocalCharges;

            lbl_ChargedWeight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ChargedWeight");
            //lbl_ActualWeight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ActualWeight");
            lbl_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Articles");
            lbl_BasicFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_BasicFreight");
            lbl_Discount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Discount");
            lbl_FOVCharges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_FOVCharges");
            lbl_ODACharges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ODACharges");
            lbl_OtherCharges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_OtherCharges");
            lbl_SubFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SubFreight");
            lbl_STaxAmt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_STaxAmt");
            lbl_TotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalFreight");
            lbl_InvoiceValue = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_InvoiceValue");
            lbl_HamaliCharge = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_HamaliCharge");
            lbl_DDCharge = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_DDCharge");
            lbl_LocalCharges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_LocalCharges");
            lbl_BiltiCharges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_BiltiCharges");
           

            lbl_ChargedWeight.Text = ChargedWeight.ToString();
            //lbl_ActualWeight.Text = Actual_Weight.ToString();
            lbl_Articles.Text = Articles.ToString();
            lbl_BasicFreight.Text = Basic_Freight.ToString();
            lbl_Discount.Text = Discount.ToString();
            lbl_FOVCharges.Text = FOV_Charges.ToString();
            lbl_ODACharges.Text = ODA_Charges.ToString();
            lbl_OtherCharges.Text = Other_Charges.ToString();
            lbl_SubFreight.Text = Sub_Freight.ToString();
            lbl_STaxAmt.Text = STax_Amt.ToString();
            lbl_TotalFreight.Text =Total_Freight.ToString();
            lbl_InvoiceValue.Text =Invoice_Value.ToString();
            lbl_HamaliCharge.Text =Hamali_Charge.ToString();
            lbl_DDCharge.Text =DD_Charge.ToString();
            lbl_LocalCharges.Text = Local_Charges.ToString();
            lbl_BiltiCharges.Text =Bilti_Charges.ToString();
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int GC_ID;
            LinkButton lnk_GC_No;

            if ((e.Item.Cells[4].Text) == "NEW")
            {    
                e.Item.Cells[4].ForeColor = System.Drawing.Color.Red;
                e.Item.Cells[4].Font.Bold = true;
            }


            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());
            lnk_GC_No = (LinkButton)e.Item.FindControl("lnk_GC_No");

            lnk_GC_No.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(GC_ID) + "')");
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
