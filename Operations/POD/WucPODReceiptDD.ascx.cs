using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;

using Raj.EC;
using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;

using Raj.EC.ControlsView;
using Raj.EC.ControlsPresenter;

//Created : Ankit champaneriya
//Date : 2/12/08
//Desc : Pod Receipt DD Details

public partial class Operations_POD_WucPODReceiptDD : System.Web.UI.UserControl, IPODReceiptDDView
{
    #region variables

    PODReceiptDDPresenter _PODReceiptDDPresenter;
    Common objCommon = new Common();
    string _flag = "";
    string Mode = "0";
    #endregion

    #region ControlBind

    public int GCNo
    {
        get { return Util.String2Int(ddl_GCNo.SelectedValue); }
    }

    public string BookingType
    {
        set { lbl_BookingTypeDisplay.Text = value; }
    }

    public string BookingDate
    {
        set { lbl_BookingDateDisplay.Text = value; }

    }

    public string BookingBranch
    {
        set { lbl_BookingBranchDisplay.Text = value; }
    }

    public string PaymentType
    {
        set { lbl_PaymentTypeDisplay.Text = value; }
    }
    public string Flag
    {
        get { return _flag; }
    }
    public string DeliveredDate
    {
        set { lbl_DeliveredDateDisplay.Text = value; }
    }

    public string DeliveredBranch
    {
        set { lbl_DeliveryBranchDisplay.Text = value; }
    }

    public string DeliveredRemark
    {
        set { lbl_DeliveryRemarksDisplay.Text = value; }
    }

    public DateTime PODReceiptDate
    {
        get { return WucDatePicker1.SelectedDate; }
        set { WucDatePicker1.SelectedDate = value; }
    }

    public IPODSentByView PODSentByView
    {
        get { return (IPODSentByView)WucPODSentBy1; }
    }

    public string Remarks
    {
        get { return txt_Remark.Text; }
        set { txt_Remark.Text = value; }
    }

    public void SetGCNo(string text, string value)
    {
        ddl_GCNo.DataTextField = "gc_no";
        ddl_GCNo.DataValueField = "gc_id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_GCNo);
    }

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (keyID <=0 && GCNo <= 0)
        {
            errorMessage = "Please Enter " + CompanyManager.getCompanyParam().GcCaption + "  No:";
            //ddl_GCNo.Focus();
        }
        else if (WucDatePicker1.SelectedDate.Date < Convert.ToDateTime(lbl_DeliveredDateDisplay.Text).Date)
        {
            errorMessage = "Received Date should grater than or equal to Delivery Date";
            //WucDatePicker1.Focus();
        }
        else if (!WucPODSentBy1.validateWUCPODDetails(lbl_Errors))
        {
            _isValid = false;
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }

    #endregion

    #region PAGE Load
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            //WucDatePicker1.disableForView = false;
            TD_Calender.Visible = false;
            WucDatePicker1.Enabled = false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        ddl_GCNo.DataTextField = "gc_no";
        ddl_GCNo.DataValueField = "gc_id";

        if (!IsPostBack)
        {
            WucDatePicker1.SelectedDate = DateTime.Now;
        }
        _PODReceiptDDPresenter = new PODReceiptDDPresenter(this, IsPostBack);

        btn_Save.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        //if (!IsPostBack)
        //{
        if (keyID > 0)
        {
            ddl_GCNo.Visible = false;
            lbl_GCNoDisplay.Visible = true;
            lbl_GCNoDisplay.Text = ddl_GCNo.SelectedText;
            WucDatePicker1.AutoPostBackOnSelectionChanged = false;
        }
        else
        {
            lbl_GCNoDisplay.Visible = false;
        }
        //}
        WucPODSentBy1.SetLabelSentBy = "Received By :";
        WucPODSentBy1.SetCallFrom = "PODDD";
        ddl_GCNo.OtherColumns = WucDatePicker1.SelectedDate.ToString();

        SetStandardCaption();
        //WucDatePicker1.DateSelectionChanged += new EventHandler(DateChange);


    }

    private void SetStandardCaption()
    {
        lbl_GCNo.Text = CompanyManager.getCompanyParam().GcCaption + "  No :";
    }

    //private void DateChange(object obj, EventArgs e)
    //{
    //    if (WucDatePicker1.IsAutoPostBack == true)
    //    {
    //        lbl_BookingBranchDisplay.Text = "";
    //        lbl_BookingDateDisplay.Text = "";
    //        lbl_BookingTypeDisplay.Text = "";
    //        lbl_DeliveredDateDisplay.Text = "";
    //        lbl_DeliveryBranchDisplay.Text = "";
    //        lbl_DeliveryRemarksDisplay.Text = "";
    //        lbl_PaymentTypeDisplay.Text = "";
    //        PODSentByView.SentByID =  0;
    //        PODSentByView.CourierDocketNo = "";
    //        PODSentByView.CourierName = "";
    //        PODSentByView.VehicleID = 0;
    //        PODSentByView.SetEmployeeId("", "0");
    //        //_iPODReceiptDDView.PODSentByView.SentByID = Util.String2Int(dr["Received_Through_ID"].ToString());
    //        //_iPODReceiptDDView.PODSentByView.CourierDocketNo = dr["Courier_Docket_No"].ToString();
    //        //_iPODReceiptDDView.PODSentByView.CourierName = dr["Courier_Name"].ToString();
    //        //_iPODReceiptDDView.PODSentByView.VehicleID = Util.String2Int(dr["Vehicle_ID"].ToString());
    //        //_iPODReceiptDDView.PODSentByView.SetEmployeeId(dr["Emp_Name"].ToString(), dr["Emp_ID"].ToString());
    //        txt_Remark.Text = "";

    //        SetGCNo("", "0");
    //    }
    //}

    #endregion

    #region Event click

    protected void ddl_GCNo_TxtChange(object sender, EventArgs e)
    {
        _PODReceiptDDPresenter.FillGCDetails();
        Upd_Pnl_GCDetail.Update();
    }
    
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        _PODReceiptDDPresenter.Save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        _PODReceiptDDPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    #endregion

    protected void WucDatePicker1_SelectionChanged(object sender, EventArgs e)
    {
        if(keyID <= 0)
        {
            lbl_BookingBranchDisplay.Text = "";
            lbl_BookingDateDisplay.Text = "";
            lbl_BookingTypeDisplay.Text = "";
            lbl_DeliveredDateDisplay.Text = "";
            lbl_DeliveryBranchDisplay.Text = "";
            lbl_DeliveryRemarksDisplay.Text = "";
            lbl_PaymentTypeDisplay.Text = "";
            PODSentByView.SentByID = 0;
            PODSentByView.CourierDocketNo = "";
            PODSentByView.CourierName = "";
            PODSentByView.VehicleID = 0;
            PODSentByView.SetEmployeeId("", "0");
            //_iPODReceiptDDView.PODSentByView.SentByID = Util.String2Int(dr["Received_Through_ID"].ToString());
            //_iPODReceiptDDView.PODSentByView.CourierDocketNo = dr["Courier_Docket_No"].ToString();
            //_iPODReceiptDDView.PODSentByView.CourierName = dr["Courier_Name"].ToString();
            //_iPODReceiptDDView.PODSentByView.VehicleID = Util.String2Int(dr["Vehicle_ID"].ToString());
            //_iPODReceiptDDView.PODSentByView.SetEmployeeId(dr["Emp_Name"].ToString(), dr["Emp_ID"].ToString());
            txt_Remark.Text = "";

            SetGCNo("", "0");
        }
    }
}