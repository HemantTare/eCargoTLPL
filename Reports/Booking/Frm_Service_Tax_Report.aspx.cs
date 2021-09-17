using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class Reports_CL_Reach_Booking_Frm_Service_Tax_Report : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    Common objcommon = new Common();

    decimal Freight = 0, TaxAbate = 0, SubTotal = 0, ServiceTax = 0, Round_Off = 0,Round_Off_Service_Tax=0;
    decimal del_Freight = 0, del_TaxAbate = 0, del_SubTotal = 0, del_ServiceTax = 0, del_Round_Off = 0, del_Round_Off_Service_Tax = 0;
    int Total =0,del_Total =0;
    public DataSet SessionServiceTax
    {
        set { StateManager.SaveState("ServiceTax", value); }
        get { return StateManager.GetState<DataSet>("ServiceTax"); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ds = null;
        if (!IsPostBack)
        {
            Panel1.Visible = false;
            //Panel2.Visible = false;
            //Panel3.Visible = false;

            objcommon.SetStandardCaptionForGrid(dg_BkgSTGrid);
            //objcommon.SetStandardCaptionForGrid(dg_DelSTGrid);

            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "reach")
            {
                btn_export_to_excel.Visible = false;
            }
            fillPaymentType();
        }
    }
    
    private void fillPaymentType()
    {
        string Query = "";

 
        Query = "select Payment_Type_Id, Payment_Type from ec_master_payment_type where Payment_Type_Id in (1,2,3) order by Payment_Type";
        ds = objcommon.EC_Common_Pass_Query(Query);
        ddl_payment_type.DataSource = ds;
        ddl_payment_type.DataTextField = "Payment_Type";
        ddl_payment_type.DataValueField = "Payment_Type_Id";
        ddl_payment_type.DataBind();

        ddl_payment_type.Items.Insert(0, new ListItem("Select All", "0"));    

    }

    //protected void dg_DelSTGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    //{
    //    dg_DelSTGrid.CurrentPageIndex = e.NewPageIndex;
    //    BindGrid("form", e);
    //}
    protected void dg_BkgSTGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_BkgSTGrid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    protected void dg_BkgSTGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
        {
            e.Item.CssClass = "LOWPRIORITY";
        }
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_Total, lbl_TotalCount, lbl_Freight, lbl_TaxAbate, lbl_SubTotal, lbl_ServiceTax, lbl_Round_Off, lbl_Round_Off_Service_Tax;
            e.Item.CssClass = "URGENTPRIORITY";
            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_TotalCount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalCount");
            lbl_Freight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Freight");
            lbl_TaxAbate = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TaxAbate");
            lbl_SubTotal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SubTotal");
            lbl_ServiceTax = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ServiceTax");
            lbl_Round_Off = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Round_Off");
            lbl_Round_Off_Service_Tax = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Round_Off_Service_Tax");

            lbl_Total.Text = "Total :";
            lbl_TotalCount.Text = Total.ToString();
            lbl_Freight.Text = Freight.ToString();
            lbl_TaxAbate.Text = TaxAbate.ToString();
            lbl_SubTotal.Text = SubTotal.ToString();
            lbl_ServiceTax.Text = ServiceTax.ToString();
            lbl_Round_Off.Text = Round_Off.ToString();
            lbl_Round_Off_Service_Tax.Text = Round_Off_Service_Tax.ToString();


        }
    }
    //protected void dg_DelSTGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    //{
    //    if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
    //    {
    //        e.Item.CssClass = "LOWPRIORITY";
    //    }
    //    if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
    //    {
    //        Label lbl_Total, lbl_TotalCount, lbl_Freight, lbl_TaxAbate, lbl_SubTotal, lbl_ServiceTax, lbl_Round_Off, lbl_Round_Off_Service_Tax;
    //        e.Item.CssClass = "URGENTPRIORITY";
    //        lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
    //        lbl_TotalCount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalCount");
    //        lbl_Freight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Freight");
    //        lbl_TaxAbate = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TaxAbate");
    //        lbl_SubTotal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SubTotal");
    //        lbl_ServiceTax = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ServiceTax");
    //        lbl_Round_Off = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Round_Off");
    //        lbl_Round_Off_Service_Tax = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Round_Off_Service_Tax");
 
    //        lbl_Total.Text = "Total :";
    //        lbl_TotalCount.Text = del_Total.ToString();
    //        lbl_Freight.Text = del_Freight.ToString();
    //        lbl_TaxAbate.Text = del_TaxAbate.ToString();
    //        lbl_SubTotal.Text = del_SubTotal.ToString();
    //        lbl_ServiceTax.Text = del_ServiceTax.ToString();
    //        lbl_Round_Off.Text = del_Round_Off.ToString();
    //        lbl_Round_Off_Service_Tax.Text = del_Round_Off_Service_Tax.ToString();
    //    }
    //}

    protected void dg_TotalBooking_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_Total, lbl_TotFreight, lbl_TotTaxAbate, lbl_TotSubTotal, lbl_TotServiceTax, lbl_TotRound_Off, lbl_TotRound_Off_Service_Tax;
            e.Item.CssClass = "HIGHPRIORITY";
            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_TotFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotFreight");
            lbl_TotTaxAbate = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotTaxAbate");
            lbl_TotSubTotal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotSubTotal");
            lbl_TotServiceTax = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotServiceTax");
            lbl_TotRound_Off = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotRound_Off");
            lbl_TotRound_Off_Service_Tax = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotRound_Off_Service_Tax");

            lbl_Total.Text = "Total : ";
            lbl_TotFreight.Text = Util.Decimal2String(Freight + del_Freight );
            lbl_TotTaxAbate.Text = Util.Decimal2String(TaxAbate + del_TaxAbate) ;
            lbl_TotSubTotal.Text = Util.Decimal2String(SubTotal + del_SubTotal);
            lbl_TotServiceTax.Text = Util.Decimal2String(ServiceTax + del_ServiceTax);
            lbl_TotRound_Off.Text = Util.Decimal2String(Round_Off + del_Round_Off);
            lbl_TotRound_Off_Service_Tax.Text = Util.Decimal2String(Round_Off_Service_Tax + del_Round_Off_Service_Tax);
        }
    }
    private void calculate_totals()
    {
        DataTable ObjDT = ds.Tables[0];
        Total = ObjDT.Rows.Count;
        if (Total > 0)
        {
            Freight = Convert.ToDecimal(ObjDT.Compute("Sum(Total_Freight)", ""));
            TaxAbate = Convert.ToDecimal(ObjDT.Compute("Sum(Tax_Abate)", ""));
            SubTotal = Convert.ToDecimal(ObjDT.Compute("Sum(Sub_Total)", ""));
            ServiceTax = Convert.ToDecimal(ObjDT.Compute("Sum(Service_Tax)", ""));
            Round_Off = Convert.ToDecimal(ObjDT.Compute("Sum(Round_Off)", ""));
            Round_Off_Service_Tax = Convert.ToDecimal(ObjDT.Compute("Sum(Round_Off_Service_Tax)", ""));
         }
        //DataTable ObjDT1 = ds.Tables[1];

        //del_Total = ObjDT1.Rows.Count;
        //if (del_Total > 0)
        //{
        //    del_Freight = Convert.ToDecimal(ObjDT1.Compute("Sum(Total_Freight)", ""));
        //    del_TaxAbate = Convert.ToDecimal(ObjDT1.Compute("Sum(Tax_Abate)", ""));
        //    del_SubTotal = Convert.ToDecimal(ObjDT1.Compute("Sum(Sub_Total)", ""));
        //    del_ServiceTax = Convert.ToDecimal(ObjDT1.Compute("Sum(Service_Tax)", ""));
        //}
        Panel1.Visible = false;
        //Panel2.Visible = false;
        //Panel3.Visible = false;

        if (Total > 0)
            Panel1.Visible = true;
        //if (del_Total > 0)
        //    Panel2.Visible = true;
        //if (Total > 0 || del_Total > 0)
        //if (Total > 0)
        //Panel3.Visible = true;
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";
            dg_BkgSTGrid.Visible = true;
            //dg_DelSTGrid.Visible = true;
            dg_BkgSTGrid.CurrentPageIndex = 0;
            //dg_DelSTGrid.CurrentPageIndex = 0;
            BindGrid("form", e);
        }
        else
        {
            lbl_Error.Text = msg;
            dg_BkgSTGrid.Visible = false;
            //dg_DelSTGrid.Visible = false;
        }
    }
    //private void BindTotalGrid()
    //{
    //    DataTable objdt = new DataTable();
       
    //    dg_TotalBooking.DataSource = objdt;
    //    dg_TotalBooking.DataBind();
    //}
    private void BindGrid(object sender, EventArgs e)
    {
       
        string CallFrom = (string)(sender);
      
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
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),
            objDAL.MakeInParams("@Payment_Type_Id", SqlDbType.Int,0,Util.String2Int(ddl_payment_type.SelectedValue))
        };

        objDAL.RunProc("[EC_RPT_Service_Tax_Register]", objSqlParam, ref ds);

        calculate_totals();
        SessionServiceTax = ds;
        objcommon.ValidateReportForm(dg_BkgSTGrid, ds.Tables[0], CallFrom, lbl_Error);
        //objcommon.ValidateReportForm(dg_DelSTGrid, ds.Tables[1], CallFrom, lbl_Error);
        //BindTotalGrid();
    }
    protected void btn_export_to_excel_Click(object sender, EventArgs e)
    {
        ds = null;
        BindGrid("form", e);

        string FileName = "ServiceTaxRegister";
        dg_BkgSTGrid.AllowPaging = false;
        dg_BkgSTGrid.DataSource = ds.Tables[0];
        dg_BkgSTGrid.DataBind();

        //dg_DelSTGrid.AllowPaging = false;
        //dg_DelSTGrid.DataSource = ds.Tables[1];
        //dg_DelSTGrid.DataBind();

        //BindTotalGrid();

        StringWriter SW = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(SW);
        dg_BkgSTGrid.RenderControl(htmlWrite);
        //dg_DelSTGrid.RenderControl(htmlWrite); 
        Response.Clear();
        Response.Charset = "";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".xls");
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Write(SW.ToString());
        Response.End();
    }
    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_export_to_excel_br_Click(object sender, EventArgs e)
    {

        string FileName = "ServiceTax Booking";
        Session["ExportToExcel"] = SessionServiceTax.Tables[0];
        Response.Redirect("../../Finance/Utilities/frm_Infra_Grid_Common_ExportToExcel.aspx");
     }
    //protected void btn_export_to_excel_Del_Click(object sender, EventArgs e)
    //{
    //    string FileName = "ServiceTax Delivery";
    //    Session["ExportToExcel"] = SessionServiceTax.Tables[1];
    //    Response.Redirect("../../../Finance/Utilities/frm_Infra_Grid_Common_ExportToExcel.aspx");
    //}
    protected void btn_export_to_excel_Total_Click(object sender, EventArgs e)
    {

        string FileName = "ServiceTax Total";
        Session["ExportToExcel"] = GetDTForOpeningDetailsXML();
        Response.Redirect("../../Finance/Utilities/frm_Infra_Grid_Common_ExportToExcel.aspx");
     }
    private DataTable GetDTForOpeningDetailsXML()
    {
        DataTable tempDT = null;
        DataRow drTemp = null;

        tempDT = new DataTable();
        tempDT.Columns.Add("Total_Freight");
        tempDT.Columns.Add("Tax_Abate");
        tempDT.Columns.Add("Sub_Total");
        tempDT.Columns.Add("Service_Tax_Payable");
        tempDT.Columns.Add("Round_Off");
        tempDT.Columns.Add("Round_Off_Service_Tax");
        
        DataTable dtbooking = SessionServiceTax.Tables[0];
        //DataTable dtDelivery = SessionServiceTax.Tables[1];

        drTemp = tempDT.NewRow();
        //drTemp["Total_Freight"] = Convert.ToDecimal(dtbooking.Rows.Count > 0 ? Convert.ToDecimal(dtbooking.Compute("Sum(Total_Freight)", "")).ToString() : "0") + Convert.ToDecimal(dtDelivery.Rows.Count > 0 ? Convert.ToDecimal(dtDelivery.Compute("Sum(Total_Freight)", "")).ToString() : "0") ;
        //drTemp["Tax_Abate"] = Convert.ToDecimal(dtbooking.Rows.Count > 0 ? Convert.ToDecimal(dtbooking.Compute("Sum(Tax_Abate)", "")).ToString() : "0") + Convert.ToDecimal(dtDelivery.Rows.Count > 0 ? Convert.ToDecimal(dtDelivery.Compute("Sum(Tax_Abate)", "")).ToString() : "0") ;
        //drTemp["Sub_Total"] = Convert.ToDecimal(dtbooking.Rows.Count > 0 ? Convert.ToDecimal(dtbooking.Compute("Sum(Sub_Total)", "")).ToString() : "0") + Convert.ToDecimal(dtDelivery.Rows.Count > 0 ? Convert.ToDecimal(dtDelivery.Compute("Sum(Sub_Total)", "")).ToString() : "0") ;
        //drTemp["Service_Tax_Payable"] = Convert.ToDecimal(dtbooking.Rows.Count > 0 ? Convert.ToDecimal(dtbooking.Compute("Sum(Service_Tax)", "")).ToString() : "0") + Convert.ToDecimal(dtDelivery.Rows.Count > 0 ? Convert.ToDecimal(dtDelivery.Compute("Sum(Service_Tax)", "")).ToString() : "0");

        drTemp["Total_Freight"] = Convert.ToDecimal(dtbooking.Rows.Count > 0 ? Convert.ToDecimal(dtbooking.Compute("Sum(Total_Freight)", "")).ToString() : "0") ;
        drTemp["Tax_Abate"] = Convert.ToDecimal(dtbooking.Rows.Count > 0 ? Convert.ToDecimal(dtbooking.Compute("Sum(Tax_Abate)", "")).ToString() : "0");
        drTemp["Sub_Total"] = Convert.ToDecimal(dtbooking.Rows.Count > 0 ? Convert.ToDecimal(dtbooking.Compute("Sum(Sub_Total)", "")).ToString() : "0");
        drTemp["Service_Tax_Payable"] = Convert.ToDecimal(dtbooking.Rows.Count > 0 ? Convert.ToDecimal(dtbooking.Compute("Sum(Service_Tax)", "")).ToString() : "0");
        drTemp["Round_Off"] = Convert.ToDecimal(dtbooking.Rows.Count > 0 ? Convert.ToDecimal(dtbooking.Compute("Sum(Round_Off)", "")).ToString() : "0");
        drTemp["Round_Off_Service_Tax"] = Convert.ToDecimal(dtbooking.Rows.Count > 0 ? Convert.ToDecimal(dtbooking.Compute("Sum(Round_Off_Service_Tax)", "")).ToString() : "0");
   
        tempDT.Rows.Add(drTemp);

        return tempDT;

    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}
