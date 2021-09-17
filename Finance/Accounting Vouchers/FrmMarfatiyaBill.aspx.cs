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
using Raj.EC.FinancePresenter;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.Security;
using ClassLibraryMVP.DataAccess;
using Raj.EC.FinanceView;
using Raj.EC;
using System.Data.SqlClient;


public partial class Finance_Accounting_Vouchers_FrmMarfatiyaBill : ClassLibraryMVP.UI.Page
{

    #region ClassVariables
     
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable DT = new DataTable();
    DataSet objDS = new DataSet();
    private DAL objDAL = new DAL();
    string _flag = "";
    string Mode = "0";
    string _GC_No_XML;
    PageControls pc = new PageControls();
    Raj.EC.MarfatiyaBill objMarBill = new Raj.EC.MarfatiyaBill();
    //DataTable SessionBindMarfatiyaGrid { set;}

 


    #endregion

    #region ControlsValues

    public string BillNo
    {
        set { lbl_BillNo.Text = value; }
        get { return lbl_BillNo.Text; }

    }
    public DateTime BillDate
    {
        set { dtp_Marfatiya_Date.SelectedDate = value; }
        get { return dtp_Marfatiya_Date.SelectedDate; }
    }
    
    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
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
        
        string Branch_Name = UserManager.getUserParam().MainName;
        int Branch_Id = UserManager.getUserParam().MainId;
        Set_DebitTo_BranchID(Branch_Name.ToString(), Branch_Id.ToString());
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.FinanceModel.MRDeliveryModel));

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Close, btn_Save_Print));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Close, btn_Save_Print));
        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Print, btn_Save_Exit, btn_Save, btn_Close));

        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        if (!IsPostBack)
        {
            Assign_Hidden_Values_For_Reset();
            Set_DebitTo_LedgerID("", "0");
            BillDate = DateTime.Now; 
            if (keyID <= 0)
            {
                Next_Bill_Number(); 
            } 
                fillDataGrid();
            
        }
    }


    private void Assign_Hidden_Values_For_Reset()
    {
        hdn_Total_GC.Value = "0";
        hdn_ToPay_LR_Total.Value = "0";
        hdn_Delivery_Charges.Value = "0";
        hdn_Delivery_Service_Tax.Value = "0";
        hdn_Memo_Total.Value = "0";
        hdnKeyID.Value = "0";

        lbl_Total_GC.Text = "0";
        lbl_ToPay_LR_Total.Text = "0";
        lbl_Delivery_Charges.Text = "0";
        lbl_Delivery_Service_Tax.Text = "0";
        lbl_Memo_Total.Text = "0";
        lbl_Total_GC.Text = "0";
 
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "2")
        {
            dtp_Marfatiya_Date.Enabled = false;
            ddl_DebitTo.Enabled = false; 
        }
        else if (Mode == "4")
        {
            dtp_Marfatiya_Date.Enabled = false;
            ddl_DebitTo.Enabled = false;
            txt_Remarks.Enabled = false;
            dg_Marfatiya.Enabled = false; 
        }

    }

   

    private void fillDataGrid()
    {
        

        if (keyID <= 0)
        {
            objDS = objMarBill.Get_EC_Opr_Marfatiya_ReadValues(Convert.ToInt32(ddl_DebitTo.SelectedValue), -1, Convert.ToDateTime(dtp_Marfatiya_Date.SelectedDate));
         dg_Marfatiya.DataSource = objDS.Tables[0] ;
         dg_Marfatiya.DataBind();
        }
        else if (keyID > 0)
        {
            objDS = objMarBill.Get_EC_Opr_Marfatiya_ReadValues(0, keyID, Convert.ToDateTime(dtp_Marfatiya_Date.SelectedDate));
            dg_Marfatiya.DataSource = objDS.Tables[0];
            dg_Marfatiya.DataBind();
            BindData(objDS.Tables[1]);
        } 
        
    }

    private void BindData(DataTable dt)
    {
        //BillNo = objComm.Get_Next_Number();
        lbl_Bill_No.Text = dt.Rows[0]["Bill_No_For_Print"].ToString();
        dtp_Marfatiya_Date.SelectedDate =  Convert.ToDateTime(dt.Rows[0]["Bill_Date"]);
        Set_DebitTo_LedgerID(dt.Rows[0]["Ledger_Name"].ToString(), dt.Rows[0]["Ledger_Id"].ToString());
        txt_Remarks.Text = dt.Rows[0]["Remarks"].ToString();
        lbl_ToPay_LR_Total.Text = dt.Rows[0]["Total_ToPay"].ToString();
        lbl_Delivery_Charges.Text = dt.Rows[0]["Total_Delivery_Charges"].ToString();
        lbl_Delivery_Service_Tax.Text = dt.Rows[0]["Total_Service_Tax"].ToString();
        lbl_Memo_Total.Text = dt.Rows[0]["Bill_Total_Amount"].ToString();
        lbl_Total_GC.Text = dt.Rows[0]["Total_LR"].ToString();
        hdn_Total_GC.Value = dt.Rows[0]["Total_LR"].ToString();
    }
    private void Next_Bill_Number()
    {
        BillNo = objComm.Get_Next_Number();
    }

    public void Set_DebitTo_LedgerID(string text, string value)
    {
        ddl_DebitTo.DataTextField = "Ledger_Name";
        ddl_DebitTo.DataValueField = "Ledger_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_DebitTo);
    }

    public void Set_DebitTo_BranchID(string text, string value)
    {
        ddl_BillingBranch.DataTextField = "Branch_Name";
        ddl_BillingBranch.DataValueField = "Branch_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_BillingBranch);
    }
    protected void dg_Marfatiya_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
       
    }

    public bool validateUI()
    {
        bool ATS;
        ATS = false;

        DataTable DT = (DataTable)(Session["MarfatiyaDetails"]);

        //if (ddlRateDiscountFor.SelectedValue == "2" && Util.String2Int(ddlParty.SelectedValue) <= 0)
        //{
        //    lblErrors.Text = "Please select party";
        //    ddlParty.Focus();
        //}
        //else if (ddlRateDiscountFor.SelectedValue == "1" && Util.String2Int(ddlDiscountBranch.SelectedValue) <= 0)
        //{
        //    lblErrors.Text = "Please select discount branch";
        //    ddlDiscountBranch.Focus();
        //}
        //else if (dtpValidFrom.SelectedDate > dtpValidUpto.SelectedDate)
        //{
        //    lblErrors.Text = "Valid Upto must be greater than Valid from date";
        //}
        //else if (DT.Rows.Count <= 0)
        //{
        //    lblErrors.Text = "Please enter discount percent";
        //}
        //else
        //{
        ATS = true;
        //}

        return ATS;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        if (validateUI())
        {
            Save(_flag);
        }
        
    }

    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        if (validateUI())
        {
            Save(_flag);
        }
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        if (validateUI())
        {
            Save(_flag);
        }
    }

    private Message Save(string _flag)
    {


        int Total_No_Of_GC = 0;
        CheckBox chk;
        int Credit_Memo_ID, GC_ID, Ledger_ID;
        string Credit_Memo_No, Credit_Memo_Date;
        DateTime Marfatiya_Date, BillDate;
        TextBox txt_GC_No_For_Print, txt_ToPay_LR_Total, txt_Delivery_Charges, txt_Delivery_Service_Tax, txt_Memo_Total;
        int i;

        if (dg_Marfatiya.Items.Count > 0)
        {
            DT.Columns.AddRange(new DataColumn[9] { new DataColumn("Credit_Memo_ID"), new DataColumn("GC_ID"), new DataColumn("Credit_Memo_No"), new DataColumn("Credit_Memo_Date"), new DataColumn("GC_No_For_Print"), new DataColumn("ToPay_LR_Total"), new DataColumn("Delivery_Charges"), new DataColumn("Delivery_Service_Tax"), new DataColumn("Memo_Total") });
            
            
            for (i = 0; i <= dg_Marfatiya.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_Marfatiya.Items[i].FindControl("Chk_Attach");
                
                if (chk.Checked == true)
                {
                    Credit_Memo_ID = Convert.ToInt32(dg_Marfatiya.Items[i].Cells[1].Text);
                    GC_ID = Convert.ToInt32(dg_Marfatiya.Items[i].Cells[2].Text);
                    Credit_Memo_No = dg_Marfatiya.Items[i].Cells[3].Text;
                    Credit_Memo_Date = dg_Marfatiya.Items[i].Cells[4].Text;

                    txt_GC_No_For_Print = (TextBox)dg_Marfatiya.Items[i].FindControl("txt_GC_No_For_Print");
                    txt_ToPay_LR_Total = (TextBox)dg_Marfatiya.Items[i].FindControl("txt_ToPay_LR_Total");
                    txt_Delivery_Charges = (TextBox)dg_Marfatiya.Items[i].FindControl("txt_Delivery_Charges");
                    txt_Delivery_Service_Tax = (TextBox)dg_Marfatiya.Items[i].FindControl("txt_Delivery_Service_Tax");
                    txt_Memo_Total = (TextBox)dg_Marfatiya.Items[i].FindControl("txt_Memo_Total");

                    DataRow newCustomersRow = DT.NewRow();
                    newCustomersRow["Credit_Memo_ID"] = Credit_Memo_ID;
                    newCustomersRow["GC_ID"] = GC_ID;
                    newCustomersRow["Credit_Memo_No"] = Credit_Memo_No;
                    newCustomersRow["Credit_Memo_Date"] = Credit_Memo_Date;
                    newCustomersRow["GC_No_For_Print"] = txt_GC_No_For_Print.Text;
                    newCustomersRow["ToPay_LR_Total"] = Util.String2Decimal(txt_ToPay_LR_Total.Text);
                    newCustomersRow["Delivery_Charges"] = Util.String2Decimal(txt_Delivery_Charges.Text);
                    newCustomersRow["Delivery_Service_Tax"] = Util.String2Decimal(txt_Delivery_Service_Tax.Text);
                    newCustomersRow["Memo_Total"] = Util.String2Decimal(txt_Memo_Total.Text);


                    DT.Rows.Add(newCustomersRow);

                    
                }
            }

           
        }
 
        DT.TableName = "MarfatiyaDetails";
        DataTable DT1 = DT.Copy();

        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);

        string MarfatiyaDetailsXML = ds.GetXml().ToLower();

        Ledger_ID  =  Convert.ToInt32(ddl_DebitTo.SelectedValue);
        Marfatiya_Date = Convert.ToDateTime(dtp_Marfatiya_Date.SelectedDate);
        int Voucher_ID = 0;
        Decimal lbl_ToPay_LR_Total = Util.String2Decimal(this.lbl_ToPay_LR_Total.Text);
        Decimal lbl_Delivery_Charges = Util.String2Decimal(this.lbl_Delivery_Charges.Text);
        Decimal lbl_Delivery_Service_Tax = Util.String2Decimal(this.lbl_Delivery_Service_Tax.Text);
        Decimal lbl_Memo_Total = Util.String2Decimal(this.lbl_Memo_Total.Text);   

        Message objMessage = new Message();
        SqlParameter[] objSqlParam = {
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode),    
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0, UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@Bill_Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Bill_Main_ID", SqlDbType.Int, 0, UserManager.getUserParam().MainId),  
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, UserManager.getUserParam().UserId), 
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@Bill_Date", SqlDbType.DateTime,0,Marfatiya_Date),
            objDAL.MakeInParams("@MarfatiyaID", SqlDbType.Int, 0,Ledger_ID), 
            objDAL.MakeInParams("@Voucher_ID", SqlDbType.Int, 0,Voucher_ID),  
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,txt_Remarks.Text),
            objDAL.MakeInParams("@MarfatiyaDetailsXML", SqlDbType.Xml,0,MarfatiyaDetailsXML), 
            objDAL.MakeInParams("@Bill_ID", SqlDbType.Int,0, keyID),
            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000)};

        objDAL.RunProc("dbo.FA_Opr_Marfatiya_Bill_Save", objSqlParam);

        objMessage.messageID = Convert.ToInt32(objSqlParam[13].Value);
        objMessage.message = Convert.ToString(objSqlParam[14].Value);
         
        string _Msg;
        _Msg = "Saved SuccessFully";
        if (_flag == "SaveAndNew")
        {
            int MenuItemId = Common.GetMenuItemId();
            string Mode = HttpContext.Current.Request.QueryString["Mode"];
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Finance/Accounting Vouchers/FrmMarfatiyaBill.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
        }
        else if (_flag == "SaveAndExit")
        {
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else if (_flag == "SaveAndPrint")
        {
            int MenuItemId = Common.GetMenuItemId();
            string Mode = HttpContext.Current.Request.QueryString["Mode"];
            int Document_ID = Convert.ToInt32(objSqlParam[11].Value);
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
        }
        else
        {
            lblErrors.Text = objMessage.message;

        }

        lblErrors.Text = objMessage.message;
        hdnKeyID.Value = Convert.ToString(objSqlParam[11].Value);
        return objMessage;
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    
    protected void dtp_Marfatiya_Date_SelectionChanged(object sender, EventArgs e)
    {
        //Assign_Hidden_Values_For_Reset();

        //_GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
        //objPDSPresenter.fillgrid();
    }
    protected void ddl_MarfatiyaName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //WucVehicleSearch1.VehicleID = 0;
        //VendorName = "";
        //VendorID = 0;
        //DeliveryModeChange();
    }

    protected void ddl_DebitTo_TxtChange(object sender, EventArgs e)
    {
        fillDataGrid();
    }
}
