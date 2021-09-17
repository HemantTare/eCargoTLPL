using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;

public partial class Finance_Accounting_Vouchers_WucSupplementaryBill : System.Web.UI.UserControl,ISupplementaryBillView
{

    #region ClassVariables
    SupplementaryBillPresenter objSupplementaryBillPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common();
    
    string Mode = "0";
    String _flag = "";
    private int _Document_Allocation_ID = 0;
    private int _Start_No = 0;
    private int _End_No = 0;
    private int _Next_No = 0;
    private string _Padded_Next_No = "";
    string _GC_No_XML;
    #endregion


    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        DataSet Checkds = new DataSet();
        Checkds = objSupplementaryBillPresenter.Check_BackDatedEntry();
        if (Checkds.Tables[0].Rows.Count > 0)
        {
            errorMessage = Checkds.Tables[0].Rows[0]["errorMessage"].ToString();
        }
        //else if (Datemanager.IsValidProcessDate("SUP_BILL", BillDate) == false)
        //{
        //    errorMessage = "Please Select Valid Bill Date";
        //}

        else if (BillDate < UserManager.getUserParam().StartDate || BillDate > UserManager.getUserParam().EndDate)
        {
            errorMessage = "Bill Date should be in current Financial Date";
        }

        else if (keyID <= 0 && Is_SeriesNo_Required == true && (Util.String2Int(BillNo) < Start_No || Util.String2Int(BillNo) > End_No))
        {
            errorMessage = "Bill No. Should be Between " + Start_No + " and " + End_No;
            lbl_SupplementaryBillNo.Focus();
        }
        else if (Client_ID <= 0)
        {
            errorMessage = "Please Select Client";
        }
        else if (SessionBillGrid.Rows.Count <= 0)
        {
            errorMessage = "Please Enter Atleast one " + CompanyManager.getCompanyParam().GcCaption;
        }
        else if (!ValidateGrid())
        { }
        else if (!ValidateDate())
        { }
        else if (Total_No_Of_GC <= 0)
        {
            errorMessage = "Please Select Atleast One " + CompanyManager.getCompanyParam().GcCaption;
        }
        else if (GrandTotal <= 0)
        {
            errorMessage = "Total " + CompanyManager.getCompanyParam().GcCaption + " Grand Total should be greater than Zero";
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

    #region Control Values

    public int Client_ID
    {
        get { return Util.String2Int(ddl_Client.SelectedValue); }
    }

    public string Flag
    {
        get { return _flag; }
    }

    public int Msg
    {
        get
        {
            if (Session["Msg"] == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(Session["Msg"]);
            }
        }
        set
        {
            Session["Msg"] = value;
        }
    }


    private bool Is_SeriesNo_Required
    {
        get { return Util.String2Bool(hdn_Is_Series_Required.Value); }
    }

    public DateTime BillDate
    {
        set { dtp_BillDate.SelectedDate = value; }
        get { return dtp_BillDate.SelectedDate; }
    }

    public string BillNo
    {
        set { lbl_SupplementaryBillNo.Text = value; }
        get { return lbl_SupplementaryBillNo.Text; }
    }

    public int Total_No_Of_GC
    {
        set{ hdn_totalgc.Value = Util.Int2String(value);  }
        get { return Util.String2Int(hdn_totalgc.Value); }
    }

    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }

    private string Start_End_No
    {
        get { return lbl_Start_End_No.Text; }
        set { lbl_Start_End_No.Text = value; }
    }
    public int Document_Allocation_ID
    {
        set { hdn_Document_Allocation_ID.Value = value.ToString(); }
        get { return Util.String2Int(hdn_Document_Allocation_ID.Value); }
    }

    public decimal GrandTotal
    {
        set { 
                lbl_GrandTotalValue.Text = value.ToString("0.00");
                hdn_GrandTotalValue.Value = value.ToString("0.00");
            }
        get { return hdn_GrandTotalValue.Value == "" ? 0 : Convert.ToDecimal(hdn_GrandTotalValue.Value); }
    }

    public decimal TotalOtherCharge
    {
        set { 
                lbl_OtherCharge.Text = value.ToString("0.00");
                hdn_OtherCharge.Value = value.ToString("0.00");
            }
        get { return hdn_OtherCharge.Value == "" ? 0 : Convert.ToDecimal(hdn_OtherCharge.Value); }
    }

    public decimal TotalServiceTax
    {
        set { 
                lbl_ServiceTax.Text = value.ToString("0.00");
                hdn_ServiceTax.Value = value.ToString("0.00");
            }
        get { return hdn_ServiceTax.Value == "" ? 0 : Convert.ToDecimal(hdn_ServiceTax.Value); }
    }

    public int Service_Type_ID
    {
        set { ddl_Service_Type.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Service_Type.SelectedValue); }
    }

    public int Start_No
    {
        set { hdn_Start_No.Value = value.ToString(); }
        get { return Util.String2Int(hdn_Start_No.Value); }
    }
    public int End_No
    {
        set { hdn_End_No.Value = value.ToString(); }
        get { return Util.String2Int(hdn_End_No.Value); }
    }
    public int Next_No
    {
        set { hdn_Next_No.Value = value.ToString(); }
        get
        {
            if (Is_SeriesNo_Required == true)
            {
                return Util.String2Int(BillNo);
            }
            else
            {
                return Util.String2Int(hdn_Next_No.Value);
            }
        }
    } 
    public string Padded_Next_No
    {
        set { hdn_Padded_Next_No.Value = value; }
        get { return hdn_Padded_Next_No.Value; }
    }

    public string ReferenceNo
    {
        set { txt_Ref_No.Text = value; }
        get { return txt_Ref_No.Text.Trim(); }
    }

    public String BillingName
    {
        set { txt_BillingName.Text = value; }
        get { return txt_BillingName.Text.Trim(); }
    }
    public String ContactPerson
    {
        set { txt_ContactPerson.Text = value; }
        get { return txt_ContactPerson.Text.Trim(); }
    }
    public String BillingAddress
    {
        set { txt_BillingAddress.Text = value; }
        get { return txt_BillingAddress.Text.Trim(); }
    }
    public String ContactNo
    {
        set { txt_ContactNo.Text = value; }
        get { return txt_ContactNo.Text.Trim(); }
    }
    public String Email
    {
        set { txt_Email.Text = value; }
        get { return txt_Email.Text.Trim(); }
    }

    public DataTable SessionBillGrid
    {
        get { return StateManager.GetState<DataTable>("BindBillGrid"); }
        set { StateManager.SaveState("BindBillGrid", value); }
    }

    public DataTable SessionBillOtherChargeGrid
    {
        get { return StateManager.GetState<DataTable>("SessionBillOtherChargeGrid"); }
        set { StateManager.SaveState("SessionBillOtherChargeGrid", value); }
    } 
    public DataTable BindServiceType
    {
        set
        {
            ddl_Service_Type.DataTextField = "Service_Type";
            ddl_Service_Type.DataValueField = "Service_Type_ID";
            ddl_Service_Type.DataSource = value;
            ddl_Service_Type.DataBind(); 
        }
    }

    public DataTable BindGrid
    {
        set
        {
            SessionBillGrid = value;
            dg_SupplementaryGrid.DataSource = value;
            dg_SupplementaryGrid.DataBind();
            Calculate_Total();

            //GrandTotal = Convert.IsDBNull(value.Compute("Sum(Bill_Total_Amount)", "")) == true ? 0 : 
            //    Convert.ToDecimal(value.Compute("Sum(Bill_Total_Amount)", ""));

            //TotalOtherCharge = Convert.IsDBNull(value.Compute("Sum(FA_Other_Charges)", "")) == true ? 0 :
            //    Convert.ToDecimal(value.Compute("Sum(FA_Other_Charges)", ""));

            //TotalServiceTax = Convert.IsDBNull(value.Compute("Sum(ServiceTax)", "")) == true ? 0 :
            //    Convert.ToDecimal(value.Compute("Sum(ServiceTax)", ""));
        }
    }

    public string GetDetailGridXML
    {
        get
        {
            DataSet objDS = new DataSet();

            DataView view = objComm.Get_View_Table(SessionBillGrid, "Att = true");
            objDS.Tables.Add(view.ToTable().Copy());
            objDS.Tables[0].TableName = "billgrid";
            //objDS.Tables.Add(SessionBillGrid.Copy());
            //objDS.AcceptChanges();
            return objDS.GetXml();

        }
    }

    public string GetOtherChargeGridXML
    {
        get
        {
            DataSet objDS = new DataSet();
            SessionBillOtherChargeGrid.TableName = "otherchargegrid";
            objDS.Tables.Add(SessionBillOtherChargeGrid.Copy());
            objDS.AcceptChanges();
            return objDS.GetXml();
        }
    }

    public String GetGCNoXML
    {
        get
        {
            if (_GC_No_XML != null)
            {
                return _GC_No_XML.ToString().ToLower();
            }
            else
            {
                return "<NewDataSet/>";
            }
        }
        set { _GC_No_XML = value; }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        Set_Standard_Caption();

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        WucSelectedItem.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);
        WucSelectedItem.Set_GCCaption = CompanyManager.getCompanyParam().GcCaption;

        ddl_Client.DataTextField = "Client_Name";
        ddl_Client.DataValueField = "Client_Id";

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save));

        if (!IsPostBack)
        {
            Set_Hidden_and_LABEL_To_Zero();
        }


        objSupplementaryBillPresenter = new SupplementaryBillPresenter(this,IsPostBack);

        if (!IsPostBack)
        {

            //if (Msg == 1)
            //{
            //    Response.Write("<script language='Javascript'>alert('Saved Successfully')</script>");
            //}
            Msg = 0;

            hdn_Is_Series_Required.Value = objComm.Get_Values_Where("Ec_master_company_Parameters", "Is_Transport_Bill_Series_Required", "", "Is_Transport_Bill_Series_Required", false).Tables[0].Rows[0]["Is_Transport_Bill_Series_Required"].ToString();
            hdn_Max_Length.Value = objComm.Get_Values_Where("ec_master_company_gc_parameter", "GC_No_Length", "", "GC_No_Length", false).Tables[0].Rows[0]["GC_No_Length"].ToString();

            if (keyID <= 0)
            {
                if (Is_SeriesNo_Required)
                {
                    lbl_Start_End_No.Visible = true;
                    lbl_SupplementaryBillNo.ReadOnly = false;
                    lbl_SupplementaryBillNo.MaxLength = Util.String2Int(hdn_Max_Length.Value);
                    lbl_SupplementaryBillNo.Attributes.Add("onkeypress", "return Only_Integers(" + lbl_SupplementaryBillNo.ClientID + ",event)");

                    Get_Next_Series_No();
                    lbl_SupplementaryBillNo.CssClass = "TEXTBOX";
                }
                else
                {
                    Next_Bill_Number();
                    lbl_Start_End_No.Visible = false;

                    if (CompanyManager.getCompanyParam().ClientCode.ToLower() != "reach")
                    {
                        lbl_SupplementaryBillNo.ReadOnly = true;
                        lbl_SupplementaryBillNo.CssClass = "TEXTBOXASLABEL";
                    }
                }
            }
            else
            {
                ddl_Client.Enabled = false;
                lbl_SupplementaryBillNo.ReadOnly = true;
                tr_GC_No.Visible = false;
                ddl_Service_Type.Enabled = false;
            }

            if (keyID > 0)
            {
                btn_SaveNew.Style.Add("display", "none");
            }
            
        }
    }

    public void Get_Next_Series_No()
    {
        objComm.Get_Document_Allocation_Details(ref _Document_Allocation_ID, ref _Start_No, ref _End_No, ref _Next_No, 0, UserManager.getUserParam().MainId, 7);

        Document_Allocation_ID = _Document_Allocation_ID;
        Start_No = _Start_No;
        End_No = _End_No;
        Next_No = _Next_No;

        if (_Next_No <= 0)
        {
            Raj.EC.Common.DisplayErrors(1013);
        }

        _Padded_Next_No = _Next_No.ToString("0000000");
        Padded_Next_No = _Padded_Next_No;
        BillNo = Padded_Next_No;

        Start_End_No = "(" + Start_No + " - " + End_No + ")";
    }

    private void Next_Bill_Number()
    {
        BillNo = objComm.Get_Next_Number(143);  //Transport Bill
    }

    private void Set_Standard_Caption()
    {
        string Caption = CompanyManager.getCompanyParam().GcCaption;
        dg_SupplementaryGrid.Columns[2].HeaderText = Caption + " No";
        WucSelectedItem.SetFoundCaption = "Enter  " + Caption + "  Nos.:";
        WucSelectedItem.SetNotFoundCaption = Caption + "  Nos.Not Found :";
                            
        //const int GCNoCaption = 1;
        //hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
        //Label1.Text = "Total  " + hdn_GCCaption.Value + ":";
        //dg_Memo.Columns[GCNoCaption].HeaderText = hdn_GCCaption.Value + "  No";
    } 

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        calculate_griddetails();
        Calculate_Total();
        objSupplementaryBillPresenter.Save();
    }

    protected void ddl_Client_TxtChange(object sender, EventArgs e)
    {
        objSupplementaryBillPresenter.FillDetails();
        FillGrid();
    }

    private void OnGetGCXML(object sender, EventArgs e)
    {
        FillGrid();
    }

    public void FillGrid()
    {
        //if (WucSelectedItems1.EnterItem != string.Empty)
        //{
        //_IsFrom_Edit = false;
        //Assign_Hidden_Values_For_Reset();
        _GC_No_XML = WucSelectedItem.GetSelectedItemsXML;
        objSupplementaryBillPresenter.FillGrid();
        WucSelectedItem.dtdetails = SessionBillGrid;
        //Assign_Hidden_Values_To_TextBox();
        WucSelectedItem.Get_Not_Selected_Items();
        //}
        Set_Hidden_and_LABEL_To_Zero();
    }


    protected void dg_SupplementaryGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string OChargeUrl;

        if (e.Item.ItemIndex != -1)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_GCID = (Label)e.Item.FindControl("lbl_GCID");

                int GcID = Util.String2Int(lbl_GCID.Text);

                OChargeUrl = ClassLibraryMVP.Util.GetBaseURL() +
                             "/Finance/Accounting Vouchers/FrmTransportBillOtherCharge.aspx?ItemIndex=" +
                             ClassLibraryMVP.Util.EncryptInteger(e.Item.ItemIndex) +
                             "&GCID=" + ClassLibraryMVP.Util.EncryptInteger(GcID) +
                             "&Mode=" + Mode + "&IsFromGC=false&Menu_Item_Id=MgAzADQA";

                LinkButton lbtn_OtherCharge = (LinkButton)e.Item.FindControl("lbtn_OtherCharge");
                lbtn_OtherCharge.Attributes.Add("onclick", "return ShowPopUp('" + OChargeUrl + "');");
            }
        }
    }

    protected void btn_update_grid_Click(object sender, EventArgs e)
    {
        decimal Service_Tax_Percent = 0, Service_Tax_Amount = 0, Other_Charge = 0, Taxable_Amount = 0;
        decimal AbatePercentage = 0;
        bool Is_ST_Abatment = false;
        int service_type_id;

        foreach(DataRow dr in SessionBillGrid.Rows)
        {
            Service_Tax_Percent = Convert.ToDecimal(dr["Service_Tax_Percent"]);
            Other_Charge = Convert.ToDecimal(dr["FA_Other_Charges"]);
            service_type_id = Util.String2Int(dr["Service_Type_Id"].ToString());
            Is_ST_Abatment = Util.String2Bool(dr["Is_ST_Abatment"].ToString());
            AbatePercentage = Util.String2Decimal(dr["AbatePercentage"].ToString());


            if (service_type_id == 2 && Is_ST_Abatment == false)
            {
                Taxable_Amount = Other_Charge;
            }
            else
            {
                Taxable_Amount = Other_Charge * AbatePercentage;
                //Taxable_Amount = Other_Charge * Util.String2Decimal("0.25");
            }

            if (Util.String2Bool(dr["Is_Service_Tax_Applicable_GC"].ToString()) == false)
            {
                Service_Tax_Amount = 0;
            }
            else
            {
                Service_Tax_Amount = Taxable_Amount * Service_Tax_Percent / 100;
            }

            dr["ServiceTax"] = Math.Round(Service_Tax_Amount,0).ToString();
            dr["Bill_Total_Amount"] = Math.Round((Service_Tax_Amount + Other_Charge), MidpointRounding.AwayFromZero).ToString("0.00");
        }

        SessionBillGrid.AcceptChanges();
        BindGrid = SessionBillGrid;
        
    }

    public void ClearVariables()
    {
        SessionBillGrid = null;
        SessionBillOtherChargeGrid = null;
    }

    public bool ValidateGrid()
    {
        string GC_NO = "";
        decimal Other_Charge = 0;
        bool value = true;
        
        foreach(DataRow dr in SessionBillGrid.Rows)
        {
            Other_Charge = Convert.ToDecimal(dr["FA_Other_Charges"]);
            GC_NO = Convert.ToString(dr["GC_No_For_Print"]);

            if (Other_Charge <= 0)
            {
                errorMessage = "Enter Other Charge for " + CompanyManager.getCompanyParam().GcCaption + " " + GC_NO;
                value = false;
            }
            break;
        }

        return value;
    }

    public bool ValidateDate()
    {
        string GC_NO = "";
        DateTime GC_Date;
        bool value = true;

        foreach (DataRow dr in SessionBillGrid.Rows)
        {
            GC_Date = Convert.ToDateTime(dr["Bill_Date"]);
            GC_NO = Convert.ToString(dr["GC_No_For_Print"]);

            if (GC_Date > BillDate)
            {
                errorMessage = CompanyManager.getCompanyParam().GcCaption +
                            " Bill Date cannot be greater than Supplementary Bill Date for " + CompanyManager.getCompanyParam().GcCaption + " No. " + GC_NO;
                value = false;
            }
            break;
        }

        return value;
    }

    public void SetClientId(string text, string value)
    {
        ddl_Client.DataTextField = "Client_Name";
        ddl_Client.DataValueField = "Client_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Client);
    }

    protected void btn_SaveNew_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        calculate_griddetails();
        Calculate_Total();
        objSupplementaryBillPresenter.Save();
        
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    private void calculate_griddetails()
    {
        int i;
        CheckBox chk;

        if (dg_SupplementaryGrid.Items.Count > 0)
        {
            for (i = 0; i <= dg_SupplementaryGrid.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_SupplementaryGrid.Items[i].FindControl("Chk_Attach");
               
                SessionBillGrid.Rows[i]["Att"] = chk.Checked;
            }
        }
    }

    public void Set_Hidden_and_LABEL_To_Zero()
    {
        lbl_OtherCharge.Text = "0";
        lbl_ServiceTax.Text = "0";
        lbl_GrandTotalValue.Text = "0";

        hdn_totalgc.Value = "0";
        hdn_OtherCharge.Value = "0";
        hdn_ServiceTax.Value = "0";
        hdn_GrandTotalValue.Value = "0";
    }

    private void Calculate_Total()
    {
        Set_Hidden_and_LABEL_To_Zero();
        Total_No_Of_GC = 0;

        if (StateManager.IsValidSession("BindBillGrid"))
        {
            if (SessionBillGrid.Rows.Count > 0)
            {
                DataView dv = new DataView(SessionBillGrid, "Att = true", "Att", DataViewRowState.CurrentRows);
                DataTable dt = dv.ToTable();

                Total_No_Of_GC = dt.Rows.Count;
                TotalOtherCharge = Convert.IsDBNull(SessionBillGrid.Compute("sum(FA_Other_Charges)", "Att = true")) == true ? 0 : Convert.ToDecimal(SessionBillGrid.Compute("sum(FA_Other_Charges)", "Att = true"));
                TotalServiceTax = Convert.IsDBNull(SessionBillGrid.Compute("sum(ServiceTax)", "Att = true")) == true ? 0 : Convert.ToDecimal(SessionBillGrid.Compute("sum(ServiceTax)", "Att = true"));
                GrandTotal = Convert.IsDBNull(SessionBillGrid.Compute("sum(Bill_Total_Amount)", "Att = true")) == true ? 0 : Convert.ToDecimal(SessionBillGrid.Compute("sum(Bill_Total_Amount)", "Att = true"));
            }
            else
            {
                TotalOtherCharge = 0;
                TotalServiceTax = 0;
                GrandTotal = 0;
            }
        }
    }

    protected void ddl_Service_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        objSupplementaryBillPresenter.FillDetails();
        FillGrid();

    }
}
