using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

/// <summary>
/// Author        : Sunil Bhoyar
/// Created On    : 22/11/2008
/// Description   : This Is The Form For Ledger BillByBill
/// </summary>


public partial class Finance_Accounting_Vouchers_VoucherBillByBill : System.Web.UI.UserControl,IVoucherBillByBillView 
{
    #region ClassVariables
    VoucherBillByBillPresenter objVoucherBillByBillPresenter; 
    Common objCommon = new Common();
    TextBox txt_Name;
    ClassLibrary.UIControl.DDLSearch ddl_Name;
    TextBox txt_Amount;
    TextBox txt_CreditDays;
    DropDownList ddl_dgDrCr;
    DropDownList ddl_RefType;
    DropDownList ddl_TDSLedger;
    const int AGST_REF_ID = 2;
     DAL objDAL = new  DAL();
    DataSet objDS = new DataSet();
    string CrDr = "";
    int DefaultCrPeriod = 0;

    #endregion
    #region ControlsValues

    public string Name
    {
        set
        {
            if (RefTypeId == AGST_REF_ID)
            {
                Raj.EC.Common.SetValueToDDLSearch(value, value, ddl_Name);
            }
            else
            {
                txt_Name.Text = value;
            }
        }
        get 
        {
            if (RefTypeId == AGST_REF_ID)
            {
                return ddl_Name.SelectedValue == null ? "" : ddl_Name.SelectedValue.Trim();
            }
            else
            {
                return txt_Name.Text.Trim();
            }
        }
    }

    public string LedgerName
    {
        get
        {
            return Util.DecryptToString(Request.QueryString["Ledger_Name"]);
            //return -1;
        }
    }

    public string VoucherType
    {
        get
        {
            return Util.DecryptToString(Request.QueryString["Voucher_Type"]);
            //return -1;
        }
    }
    public int VoucherId
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Voucher_Id"]);
            //return -1;
        }
    }
 
    
    public bool IsTDSApplicable
    {
        get { return Convert.ToBoolean(ViewState["IsTDSApplicable"]); }
        set { ViewState["IsTDSApplicable"] = value;}
    }

    private int dgDrCr
    {
        set 
        {
            ddl_dgDrCr.SelectedValue = value==-1 ? "Dr" : "Cr";
        }
        get
        {
            return ddl_dgDrCr.SelectedValue == "Dr" ? -1 : 1;
        }
    }

    private int RefTypeId
    {
        get { return Convert.ToInt32(ddl_RefType.SelectedValue); }
        set { ddl_RefType.SelectedValue = value.ToString(); }
    }

    private int TDSLedgerId
    {
        get { return Convert.ToInt32(ddl_TDSLedger.SelectedValue); }
        set { ddl_TDSLedger.SelectedValue = value.ToString(); }
    }


    public decimal Amount
    {
        set { txt_Amount.Text = convertToAbs(value); }
        get { return convertToDecimal(txt_Amount.Text) * dgDrCr; }
    }

    public int CreditDays
    {
        set { txt_CreditDays.Text = value.ToString(); }
        get { return convertToInt(txt_CreditDays.Text); }
    }


    public string convertToDrCr(object value)
    {
        if (Math.Sign(convertToDecimal(value))==-1)
        { return "Dr"; }
        else{return "Cr";}
    }

    public string convertToAbs(object value)
    {
        return Convert.ToString(Math.Abs(Convert.ToDecimal(value)));
    }

    public decimal convertToDecimal(object value)
    {
        if (Convert.IsDBNull(value) || value.ToString().Trim() == string.Empty)
        { return 0;}
        else { return Convert.ToDecimal(value); }
    }

    public int convertToInt(object value)
    {
        if (value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Convert.ToInt32(value); }
    }

    

    #endregion

    #region ControlsBind

    public DateTime BillDate
    {
        set { ViewState["BillDate"] = value;}
        get { return Convert.ToDateTime(ViewState["BillDate"]); }
    }

    public DataTable SessionBillByBillDT
    {
        set { StateManager.SaveState("VoucherBillByBill_DT", value); }
        get { return StateManager.GetState<DataTable>("VoucherBillByBill_DT"); }
    }

    public DataTable SessionBillByBill_New
    {
        set { StateManager.SaveState("SesssionBillByBill_New", value); }
        get { return StateManager.GetState<DataTable>("SesssionBillByBill_New"); }
    }

    public DataTable SessionDropDownRefType
    {
        set { StateManager.SaveState("SessionDropDownRefType_DT", value); }
        get { return StateManager.GetState<DataTable>("SessionDropDownRefType_DT"); }
    }

    public DataTable SessionTDSLedger
    {
        set { StateManager.SaveState("SessionTDSLedger_DT", value); }
        get { return StateManager.GetState<DataTable>("SessionTDSLedger_DT"); }
    }

    public DataTable Bind_ddl_RefType
    {
        set
        {
            ddl_RefType.DataTextField = "Ref_Type";
            ddl_RefType.DataValueField = "Ref_Type_Id";
            ddl_RefType.DataSource = value;
            ddl_RefType.DataBind();
        }
    }

    public DataTable Bind_ddl_TDSLedger
    {
        set
        {
            ddl_TDSLedger.DataTextField = "Ledger_Name";
            ddl_TDSLedger.DataValueField = "Ledger_Id";
            ddl_TDSLedger.DataSource = value;
            ddl_TDSLedger.DataBind();
        }
    }

    public DataTable Bind_dg_BillByBill
    {
        set
        {
            dg_BillByBill.DataSource = value;
            dg_BillByBill.DataBind();
            SessionBillByBill_New = value;
        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (convertToDecimal(SessionBillByBill_New.Compute("Sum(Amount)",""))!= UptoAmount)
        {
            errorMessage = "Amount Not Match";
        }
        else if (objVoucherBillByBillPresenter.IsDupicateRefNo()==false)
        {

        }
        else
        {
            _isValid = true;
        }
        return _isValid;  
    }
    //private bool IsDuplicateRef_No()
    //{
    //    DataSet ds = new DataSet();        
    //    ds.Tables.Add(SessionBillByBill_New.Copy());
    //    string errMsg="";
    //    SqlParameter[] objSqlParam = { objDAL.MakeOutParams("@IsDuplicate", SqlDbType.Bit, 0), 
    //                                   objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,0,UserManager.getUserParam().HierarchyCode),
    //                                   objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,UserManager.getUserParam().MainId),
    //                                   objDAL.MakeInParams("@Division_Id", SqlDbType.Int,0,UserManager.getUserParam().DivisionId),
    //                                   objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,LedgerId),
    //                                   objDAL.MakeInParams("@RefNo_XML", SqlDbType.Xml,0,ds.GetXml().ToLower())               
    //                                     };

    //     objDAL.RunProc("FA_Opr_Voucher_IsDuplicateBill", objSqlParam, ref objDS);
               
        
    //    if (Convert.ToBoolean(objSqlParam[0].Value)== true)
    //    {
    //        for (int i = 0; i < objDS.Tables[0].Rows.Count;i++ )
    //        {
    //            errMsg =errMsg  + objDS.Tables[0].Rows[i]["Ref_No"].ToString() + " ,";                    
    //        }                        
    //        errMsg=errMsg.Remove(errMsg.Length - 1);
    //        errorMessage ="Duplicate Ref No :  " + errMsg;
    //        return false;
    //    }
    //    return true;

    //}

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }


    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Ledger_Id"]);
          // return -1;
        }
    }
    
    public int LedgerId
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Ledger_Id"]);
            // return -1;
        }
    }
    #endregion


    #region OtherProperties

    public decimal TotalAmount
    {
        get { return convertToDecimal(SessionBillByBill_New.Compute("Sum(Amount)", "")); }
    }

    public decimal Debit
    {
        get { return Math.Abs(convertToDecimal(Util.DecryptToDecimal(Request.QueryString["Debit"]))); }
    }

    public decimal Credit
    {
        get { return Math.Abs(convertToDecimal(Util.DecryptToDecimal(Request.QueryString["Credit"]))); }
    }

    public decimal UptoAmount
    {
        get 
        {
            if (Debit > 0)
            { return -1 * Debit; }
            else { return Credit;}
        }
    }
    #endregion


    #region OtherMethods


   
    private void InsertUpdateBillByBill(DataGridCommandEventArgs e)
    {
        findControls(e.Item);
        ddl_Name.OtherColumns = LedgerId.ToString() + "Ö" + VoucherId.ToString();
        if (ValidateBillByBillValues() == false)
        { return; }

        DataTable objDT = SessionBillByBill_New;
        DataRow objDR = null;

        if (e.CommandName == "Add")
        {
            objDR = objDT.NewRow();
        }
        else
        {
            objDR = objDT.Rows[e.Item.ItemIndex];
        }


        try
        {
            objDR["Ref_No"] = Name;
            objDR["Ref_Type"] = ddl_RefType.SelectedItem.Text;
            objDR["Ref_Type_Id"] = RefTypeId;
            objDR["Credit_Days"] = CreditDays;
            objDR["Amount"] =Amount;
            objDR["Bill_Date"] = RefTypeId == 2 ? BillDate : DateTime.Now;


            if (VoucherType == "Journal" && IsTDSApplicable==true && RefTypeId!=AGST_REF_ID)
            {
                objDR["TDS_Ledger_Id"] = TDSLedgerId;
                objDR["TDS_Ledger_Name"] = ddl_TDSLedger.SelectedItem.Text;
            }
            else 
            {
                objDR["TDS_Ledger_Id"] = "0";
                objDR["TDS_Ledger_Name"] ="";
            }
            
            if (e.CommandName == "Add")
            {
                objDR["Ledger_Id"] = LedgerId;

                objDT.Rows.Add(objDR);
            }

            if (e.CommandName == "Update")
            {
                dg_BillByBill.EditItemIndex = -1;
                dg_BillByBill.ShowFooter = true;
            }
            objDT.AcceptChanges();
            Bind_dg_BillByBill = objDT;
           
            if (e.CommandName == "Add")
            {
                Amount = UptoAmount != TotalAmount ? UptoAmount - TotalAmount : 0;
            }
        }
        catch (ConstraintException)
        {

            if (e.CommandName == "Edit")
            {
                objDR.RejectChanges();
            }
            errorMessage = "Duplicate Name";
        }
    }

    private bool ValidateBillByBillValues()
    {
        bool _isValid = false;
        if (Name.Trim()==string.Empty)
        {
            errorMessage = RefTypeId==AGST_REF_ID ? "Please Select Name From List" : "Please Enter Name";
        }
        //else if (CreditDays==0)
        //{
        //    errorMessage = "Please Enter Credit Days";
        //}
        else if (Amount==0)
        {
            errorMessage = "Please Enter Amount";
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }


    #endregion


    #region  ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Save.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

       
        if (!IsPostBack)
        {
            //CrDr = Util.DecryptToString(Request.QueryString["CrDr"]);

            lbl_LedgerName.Text = LedgerName;
            lbl_Upto.Text = Debit > 0 ? Debit.ToString() + " Dr" : Credit.ToString() + " Cr";

            //SqlParameter[] objSqlParam = { 
            //                                objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,LedgerId)
            //                             };

            //objDAL.RunProc("EC_FA_Mst_VoucherBillByBill_FillValues", objSqlParam, ref objDS);

            //SessionTDSLedger = objDS.Tables[0];
            //IsTDSApplicable = Convert.ToBoolean(objDS.Tables[1].Rows[0]["Is_TDS_Applicable"]);
            //DefaultCrPeriod = Convert.ToInt32(objDS.Tables[1].Rows[0]["Default_Credit_Period"]);


            //SessionBillByBill_New = objCommon.Get_View_Table(SessionBillByBillDT, "Ledger_Id=" + LedgerId).ToTable();
            //Common.SetPrimaryKeys(new string[] { "Ledger_Id", "Ref_No" }, SessionBillByBill_New);
            //Bind_dg_BillByBill = SessionBillByBill_New;

            Session["Bill_Voucher_Id"] = VoucherId.ToString();
            Session["Bill_Ledger_Id"] = LedgerId.ToString();          
            
        }
        objVoucherBillByBillPresenter = new VoucherBillByBillPresenter(this, IsPostBack);

        if (VoucherType == "Journal" && IsTDSApplicable == true)
        { dg_BillByBill.Columns[4].Visible = true; }
        else { dg_BillByBill.Columns[4].Visible = false; }

        if (!IsPostBack)
        {
            SetTotalLabel();
        }
    }

    #endregion


    #region BillByBill_GridEvents
    protected void dg_BillByBill_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        findControls(e.Item);
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            InsertUpdateBillByBill(e);
        }

        else if (e.CommandName == "Edit")
        {
            dg_BillByBill.EditItemIndex = e.Item.ItemIndex;
            Bind_dg_BillByBill = SessionBillByBill_New;
            dg_BillByBill.ShowFooter = false;
        }

        else if (e.CommandName == "Cancel")
        {
            dg_BillByBill.EditItemIndex = -1;
            Bind_dg_BillByBill = SessionBillByBill_New;
            dg_BillByBill.ShowFooter = true;
        }

        else if (e.CommandName == "Delete")
        {
            SessionBillByBill_New.Rows[e.Item.ItemIndex].Delete();
            SessionBillByBill_New.AcceptChanges();
            Bind_dg_BillByBill = SessionBillByBill_New;
        }

        SetTotalLabel();
    }

    protected void dg_BillByBill_ItemDataBound(object source, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            findControls(e.Item);
            Bind_ddl_RefType = SessionDropDownRefType;
            Bind_ddl_TDSLedger = SessionTDSLedger;


            ddl_Name.DataTextField = "Ref_No";
            ddl_Name.DataValueField = "Ref_No1";

            if (e.Item.ItemType == ListItemType.EditItem)
            {
               
                RefTypeId =Util.String2Int(SessionBillByBill_New.Rows[e.Item.ItemIndex]["Ref_Type_Id"].ToString());
                Name = SessionBillByBill_New.Rows[e.Item.ItemIndex]["Ref_No"].ToString();

                TDSLedgerId = Util.String2Int(SessionBillByBill_New.Rows[e.Item.ItemIndex]["TDS_Ledger_Id"].ToString());
		BillDate = Convert.ToDateTime(SessionBillByBill_New.Rows[e.Item.ItemIndex]["Bill_Date"].ToString());
            }






                if(e.Item.ItemType == ListItemType.Footer)
                {
                    dgDrCr = Debit > 0 ? -1 : 1;
                    CreditDays = DefaultCrPeriod;

                    if (!IsPostBack)
                    {
                       if (dgDrCr == 1)
                        {
                            Amount = Credit;
                        }
                        else
                        {
                            Amount = Debit;
                        }
             
                    }
                }
            

            desableControls();

        }
    }

    #endregion

    

    private void findControls(DataGridItem item)
    {
        ddl_dgDrCr = (DropDownList)item.FindControl("ddl_DrCr");
        ddl_Name = (ClassLibrary.UIControl.DDLSearch)item.FindControl("ddl_Name");
        ddl_RefType = (DropDownList)item.FindControl("ddl_RefType");
        txt_Amount = (TextBox)item.FindControl("txt_Amount");
        txt_Name = (TextBox)item.FindControl("txt_Name");
        txt_CreditDays = (TextBox)item.FindControl("txt_CreditDays");
        ddl_TDSLedger = (DropDownList)item.FindControl("ddl_TDSLedger");
    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            foreach (DataRow Dr in SessionBillByBillDT.Rows)
            {
                if (Util.String2Int(Dr["Ledger_Id"].ToString()) == LedgerId)
                {
                    Dr.Delete();
                }
            }

            SessionBillByBillDT.AcceptChanges();
            SessionBillByBillDT.Merge(SessionBillByBill_New);
            SessionBillByBillDT.AcceptChanges();
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Close", "self.close();", true);
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx");

        }
    }

    

    protected void ddl_RefType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RefType = (DropDownList)sender;
        DataGridItem item = (DataGridItem)ddl_RefType.Parent.Parent;

        findControls(item);

        desableControls();        
    }


    private void desableControls()
    {
        ddl_Name.OtherColumns = LedgerId.ToString() + "Ö" + VoucherId.ToString();
        if (RefTypeId == AGST_REF_ID)
        {
            ddl_Name.Visible = true;
            txt_Name.Visible = false;
            txt_CreditDays.Enabled = false;
            ddl_TDSLedger.Visible = false;
        }
        else
        {
            ddl_Name.Visible = false;
            txt_Name.Visible = true;
            txt_CreditDays.Enabled = true;
            ddl_TDSLedger.Visible = true;
        }
    }

    protected void ddl_Name_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal PenAmount, RemBillAmount;
        TextBox txt = (TextBox)sender;
        DataGridItem item = (DataGridItem)txt.Parent.Parent.Parent;

        findControls(item);

        if (Name != "")
        {
            objDS = objVoucherBillByBillPresenter.SetCreditDaysAmount();

            if (objDS.Tables[0].Rows.Count != 0)
            {
                DataRow Dr = objDS.Tables[0].Rows[0];

                CreditDays = Util.String2Int(Dr["Credit_Days"].ToString());
                BillDate = Convert.ToDateTime(Dr["Bill_Date"].ToString());

                PenAmount = Convert.ToDecimal(Dr["Amount"].ToString());

                RemBillAmount = (Debit + Credit)- TotalAmount;

                if (PenAmount * -1 > RemBillAmount)
                { Amount = RemBillAmount; }
                else
                { Amount = PenAmount * -1; }

            }

        }

        ddl_Name.OtherColumns = LedgerId.ToString() + "Ö" + "1";
    }
    public void SetTotalLabel()
    {
        decimal TotalAmount = 0;
        if (SessionBillByBill_New.Rows.Count > 0)
        {
            TotalAmount = (decimal)SessionBillByBill_New.Compute("Sum(Amount)", "");
            lblTotalAmount.Text = convertToAbs(TotalAmount).ToString();
        }
        if (TotalAmount <= 0)
        {
            lblDrCr.Text = " Dr";
        }
        else
        {
            lblDrCr.Text = " Cr";
        }
    }
      
}














 


   
 

