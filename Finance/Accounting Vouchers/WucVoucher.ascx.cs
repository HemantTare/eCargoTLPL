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
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC;

using ClassLibraryMVP.Security;
/// <summary>
/// Author        : Sunil Bhoyar
/// Created On    : 22/11/2008
/// Description   : This Is The Form For Leder Voucher Details
/// </summary>


public partial class Finance_Accounting_Vouchers_Voucher : System.Web.UI.UserControl, IVoucherView
{
    #region ClassVariables
    Common objCommon = new Common();
    VoucherPresenter objVoucherPresenter;

    ClassLibrary.UIControl.DDLSearch ddl_Ledger;
    TextBox txt_Debit;
    TextBox txt_Credit;
    DropDownList ddl_dgDrCr;
    int _dgRowCount;
    private int _voucherTypeIdFromDB;
    #endregion
    #region ControlsValues

    public string Mode
    {
        get {return Request.QueryString["Mode"] ;}
    }

    public string Menu_Item_Id
    {
        get { return Request.QueryString["Menu_Item_Id"];}
    }

   public string RefNo
    {
        set { txt_RefNo.Text = value; }
        get { return txt_RefNo.Text; }
    }

   public string VoucherNo
    {
        set { txt_VoucherNo.Text = value; }
        get { return txt_VoucherNo.Text; }
    }

 public string Narration
    {
        set { txt_Narration.Text = value; }
        get { return txt_Narration.Text; }
    }

    public string VoucherTypeName
    {
        set {lbl_VoucherType.Text = value;}
        get { return lbl_VoucherType.Text; }

    }


     
   public string VoucherType
    {
        get 
        {
            string type;
            switch (VoucherTypeID)
            {
                case 2:
                    if (IBTVoucherFlag.Trim() == string.Empty)
                    {
                        type = "CreditNote";
                    }
                    else
                    {
                        type = "IBTCreditNote";
                    }
                    break;
                case 3:
                    if (IBTVoucherFlag.Trim() == string.Empty)
                    {
                        type = "DebitNote";
                    }
                    else
                    {
                        type = "IBTDebitNote";
                    }
                    break;
                case 1:
                    type = "Contra";
                    break;
                case 6:
                    type = "Journal";
                    break;
                case 8:
                    type = "Payment";
                    break;
                case 14:
                    type = "Receipt";
                    break;
                case 2222:
                    type = "IBTCreditNote";
                    break;
                case 3333:
                    type = "IBTDebitNote";
                    break;
                default:
                    type = "";
                    break;
            }
            return type;
        }

      
    }


   public int VoucherTypeID
   { 
     get {
          int _typeid;

          if (Request.QueryString["Voucher_Type_Id"] != null)
          {
             _typeid = Util.DecryptToInt(Request.QueryString["Voucher_Type_Id"].ToString());
          }
          else if (Common.GetMode() == 4)///In View,Edit Mode Get Voucher Type From DataBase
         {
             return Convert.ToInt32(ViewState["_voucherTypeIdFromDB"]);
         }
         else
         {
             _typeid = Util.String2Int(Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).QueryString);
         }
            return _typeid;
       }
        set 
        {
            ViewState["_voucherTypeIdFromDB"] = value.ToString();
        }
   }

    public string IBTVoucherFlag
    {
        get
        {
 
            if (Request.QueryString["IBTVoucherFlag"] != null)
            {
                return Util.DecryptToString(Request.QueryString["IBTVoucherFlag"].ToString());
            }
            else
            {
                return "";
            }
        }
    }
   public string FBTPaymentType
   {
        get { return ddl_FBTPaymentType.SelectedValue; }
        set { ddl_FBTPaymentType.SelectedValue = value; }
   }

   public decimal TotalCredit
    {
        set { lbl_TotalCredit.Text = value.ToString();}
        get
        {
           return convertToDecimal(SessionVoucherDT.Compute("Sum(Credit)", ""));
        }
    }

    public decimal TotalDebit
    {
        set { lbl_TotalDebit.Text = value.ToString(); }
        get
        {
            return convertToDecimal(SessionVoucherDT.Compute("Sum(Debit)", ""));
        }
    }

    public decimal Credit
    {
        set { txt_Credit.Text = value.ToString(); }
        get {return dgDrCr=="Cr" ? convertToDecimal(txt_Credit.Text):0;}
    }

    public decimal Debit
    {
        set { txt_Debit.Text = value.ToString(); }
        get { return dgDrCr=="Dr" ? convertToDecimal(txt_Debit.Text): 0 ;}
    }


    public DateTime VoucherDate
    {
        get { return dtp_VoucherDate.SelectedDate; }
        set { dtp_VoucherDate.SelectedDate = value; }
    }
    private string dgDrCr
    {
        set { ddl_dgDrCr.SelectedValue = value.ToString(); }
        get { return ddl_dgDrCr.SelectedValue;}
    }

    public int LedgerId
    {
        get { return Util.String2Int(ddl_Ledger.SelectedValue);}
    }

    private bool IsBillByBill
    {
        get { return Convert.ToBoolean(ddl_Ledger.GetValueAt(1)); }
    }

    private bool IsFBTApplicable
    {
        get { return Convert.ToBoolean(ddl_Ledger.GetValueAt(2)); }
    }

    private bool IsBankApplicable
    {
        get { return Convert.ToBoolean(ddl_Ledger.GetValueAt(3)); }
    }

    private bool IsCostCentre
    {
        get { return Convert.ToBoolean(ddl_Ledger.GetValueAt(4)); }
    }

    private void SetLedgerId(string value,string text)
    {
        ddl_Ledger.DataTextField = "Ledger_Name";
        ddl_Ledger.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Ledger);
    }



    public string GetVoucherXML
    {
        get
        {
                DataSet objDS = new DataSet();
                //SessionVoucherDT.TableName = "Voucher";
                objDS.Tables.Add(SessionVoucherDT.Copy());
                objDS.Tables[0].TableName = "Voucher";
                objDS.AcceptChanges();
               return objDS.GetXml();
        }
    }

    public string GetVoucherCostCentreXML
    {
        get
        {
            DataSet objDS = new DataSet();
            SessionVoucherCostCentreDT.TableName = "CostCenter";
            Delete_UnwantedRows_in_SessionBillByBill(SessionVoucherCostCentreDT);
            objDS.Tables.Add(SessionVoucherCostCentreDT.Copy());
            objDS.AcceptChanges();
            return objDS.GetXml();
        }
    }


    public string GetVoucherBillByBillXML
    {
        get
        {
            DataSet objDS = new DataSet();
            SessionVoucherBillByBillDT.TableName = "BillByBill";
            Delete_UnwantedRows_in_SessionBillByBill(SessionVoucherBillByBillDT);
            objDS.Tables.Add(SessionVoucherBillByBillDT.Copy());
            objDS.AcceptChanges();
            return objDS.GetXml();
        }
    }

   

    public string convertToDrCr(object value)
    {
        if (convertToDecimal(value)>0)
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


    public DataTable SessionVoucherDT
    {
        set { StateManager.SaveState("Voucher_DT", value); }
        get { return StateManager.GetState<DataTable>("Voucher_DT"); }
    }

   

    public DataTable SessionVoucherBillByBillDT
    {
        set { StateManager.SaveState("VoucherBillByBill_DT", value); }
        get { return StateManager.GetState<DataTable>("VoucherBillByBill_DT"); }
    }

   

    public DataTable SessionVoucherCostCentreDT
    {
        set { StateManager.SaveState("VoucherCostCentre_DT", value); }
        get { return StateManager.GetState<DataTable>("VoucherCostCentre_DT"); }
    }


    public DataTable SessionDropDownCostCentre
    {
        set { StateManager.SaveState("DropDownCostCentre_DT", value); }
        get { return StateManager.GetState<DataTable>("DropDownCostCentre_DT"); }
    }

    public DataTable SessionDropDownRefType
    {
        set { StateManager.SaveState("SessionDropDownRefType_DT", value); }
        get { return StateManager.GetState<DataTable>("SessionDropDownRefType_DT"); }
    }

    public DataTable Bind_dg_Voucher
    {
        set
        {
            SessionVoucherDT = value;

            _dgRowCount = value.Rows.Count;

            dg_Voucher.DataSource = value;
            dg_Voucher.DataBind();
            TotalCredit = TotalCredit;
            TotalDebit = TotalDebit;
        }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
       
        if (TotalCredit!=TotalDebit)
        {
            errorMessage = "Credit and Debit not match !";
        }
        else if (SessionVoucherDT.Rows.Count==0)
        {
            errorMessage = "Please Enter Voucher Details";
        }
        else if (validateDetails() == false)
        {
            _isValid = false;
        }
        else if ((VoucherTypeID==2222 || VoucherTypeID==3333) && (objCommon.Get_View_Table(SessionVoucherDT, "IsBranchLedger=1").Count==0))
        {
            errorMessage = "Please Select Branch Ledger";
        } 
        else if (VoucherDate < UserManager.getUserParam().StartDate || VoucherDate > UserManager.getUserParam().EndDate)
        {
            errorMessage = "Voucher Date should be in current Financial Date";
        }      
        else
        {
            _isValid = true;
        }
        return _isValid;  
    }


    private bool validateDetails()
    {
       // bool _isValid=false;
        
        foreach (DataRow Dr in SessionVoucherDT.Rows)
        {
            DataView Dv;
            string filterStr="Ledger_Id=" + Dr["Ledger_Id"].ToString();

          if (Convert.ToBoolean(Dr["IsCostCentre"]))
          {
              if (convertToDecimal(Dr["Debit"]) > 0 && convertToDecimal(SessionVoucherCostCentreDT.Compute("Sum(Amount)", filterStr)) != convertToDecimal(Dr["Debit"]))
                {
                    errorMessage = "Please Enter Cost Centre Details for Ledger  '" + Dr["Ledger_Name"].ToString() + "'";
                     return false;
                }
                else if (convertToDecimal(Dr["Credit"]) > 0 && convertToDecimal(SessionVoucherCostCentreDT.Compute("Sum(Amount)", filterStr)) != convertToDecimal(Dr["Credit"]))
                {
                    errorMessage = "Please Enter Cost Centre Details for Ledger  '" + Dr["Ledger_Name"].ToString() + "'";
                    return false;

                }
          }
          else if (Convert.ToBoolean(Dr["IsBillByBill"]))
          {
              if (convertToDecimal(Dr["Debit"]) > 0 && Math.Abs(convertToDecimal(SessionVoucherBillByBillDT.Compute("Sum(Amount)", filterStr)))!= convertToDecimal(Dr["Debit"]))
              {
                  errorMessage = "Please Enter Bill By Bill Details for Ledger  '" + Dr["Ledger_Name"].ToString() + "'";
                  return false;

              }
              else if (convertToDecimal(Dr["Credit"]) > 0 && convertToDecimal(SessionVoucherBillByBillDT.Compute("Sum(Amount)", filterStr)) != convertToDecimal(Dr["Credit"]))
              {
                  errorMessage = "Please Enter Bill By Bill Details for Ledger  '" + Dr["Ledger_Name"].ToString() + "'";
                  return false;

              }
          }
          else if (Convert.ToBoolean(Dr["IsBankApplicable"]))
          {
              Dv = objCommon.Get_View_Table(SessionVoucherDT, filterStr);

              if (Dv.ToTable().Rows[0]["Cheque_No"].ToString() == string.Empty && Convert.ToBoolean(GetGlobalResourceObject("FA_Opr", "Validate_Voucher_ChequeNo").ToString()) == true)
              {
                  errorMessage = "Please Enter Bank Details for Ledger  '" + Dr["Ledger_Name"].ToString() + "'";
                  return false;
              }
          }
          //else if (Convert.ToBoolean(Dr["IsFBTApplicable"]))
          //{
          //    Dv = objCommon.Get_View_Table(SessionVoucherDT, filterStr);

          //    if (Dv.Count == 0)
          //    {
          //        errorMessage = "Please Enter FBT Details for Ledger  '" + Dr["Ledger_Name"].ToString() + "'";
          //        return false;
          //    }
          //}
          else if (Convert.ToBoolean(Dr["IsFBTPaymentType"]))
          {
              if (FBTPaymentType=="0")
              {
                  errorMessage = "Please select FBT Payment Type for ledger  '" + Dr["Ledger_Name"].ToString() + "'";
                  return false;
              }
          }
          else if (Narration == "")
          {
              errorMessage = "Please Enter Narration";
              return false;
          }
         

          //else
          //{
          //    _isValid = true;
          //}

          //if (_isValid == false)
          //{
          //    break;
          //}
    }
        return true;
   }

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
           return Util.DecryptToInt(Request.QueryString["Id"]);
           // return -1;// 434981;
        }
    }

    #endregion


    #region OtherProperties

    
    //public bool VisibleVoucher
    //{
    //    set { tbl_Voucher.Visible = value; }
    //}

    //public bool IsVoucher 
    //{
    //    set { ViewState["IsVoucher"]=value; }
    //    get { return Convert.ToBoolean(ViewState["IsVoucher"]);}
    //}
    //public bool IsBillWise
    //{
    //    set { ViewState["IsBillWise"] = value; }
    //    get { return Convert.ToBoolean(ViewState["IsBillWise"]); }
    //}

    #endregion


    #region OtherMethods

    public void ClearVariables()
    {
        SessionDropDownCostCentre = null;
        SessionDropDownRefType = null;
        SessionVoucherBillByBillDT = null;
        SessionVoucherCostCentreDT = null;
        SessionVoucherDT = null;
    }
   
    private void InsertUpdateVoucher(DataGridCommandEventArgs e)
    {
        findControls(e.Item);
        if (ValidateVoucherValues() == false)
        { return; }

        DataTable objDT = SessionVoucherDT;
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
            objDR["Ledger_Id"] = LedgerId;
            objDR["Ledger_Name"] = ddl_Ledger.SelectedItem;
            objDR["Debit"] = Debit;
            objDR["Credit"] = Credit;

            //objDR["IsBankApplicable"] = true;
            //objDR["IsBillByBill"] = true;
            //objDR["IsFBTApplicable"] = true;
            //objDR["IsCostCentre"] = true;

            DataSet Ds = objVoucherPresenter.GetLedgerParam();

            DataRow Dr = Ds.Tables[0].Rows[0];

            objDR["IsBankApplicable"] = Convert.ToBoolean(Dr["IsBankApplicable"]);
            objDR["IsBillByBill"] = Convert.ToBoolean(Dr["IsBillByBill"]);
            objDR["IsFBTApplicable"] = Convert.ToBoolean(Dr["IsFBTApplicable"]);
            objDR["IsCostCentre"] = Convert.ToBoolean(Dr["IsCostCentre"]);

            objDR["IsFBTPaymentType"] = Convert.ToBoolean(Dr["IsFBTPaymentType"]);

            objDR["IsBranchLedger"] = Convert.ToBoolean(Dr["IsBranchLedger"]);

            
            if (e.CommandName == "Add")
            {
                objDT.Rows.Add(objDR);
                objDR["FBT_Category_Id"] = Dr["FBT_Category_Id"].ToString();
                objDR["Cheque_Date"] = DateTime.Now;
            }

            if (e.CommandName == "Update")
            {
                if (Convert.ToInt32(objDT.Rows[e.Item.ItemIndex]["Ledger_Id"]) != LedgerId)
                {
                    objDR["FBT_Category_Id"] = Dr["FBT_Category_Id"].ToString();
                }

                dg_Voucher.EditItemIndex = -1;
                dg_Voucher.ShowFooter = true;
            }
            objDT.AcceptChanges();
            Bind_dg_Voucher = objDT;
        }
        catch (ConstraintException)
        {

            if (e.CommandName == "Edit")
            {
                objDR.RejectChanges();
            }
            errorMessage = "Duplicate Particulers";
        }
    }

    private bool ValidateVoucherValues()
    {
        bool _isValid = false;
        if (LedgerId <= 0)
        {
            errorMessage = "Please Enter Particulers";
        }
        else if (dgDrCr=="Cr" && Credit==0)
        {
            errorMessage = "Please Enter Credit Amount";
        }
        else if (dgDrCr == "Dr" && Debit == 0)
        {
            errorMessage = "Please Enter Debit Amount";
        }
        //else if (dgDrCr == "Cr" && objVoucherPresenter.IsLedgerCashInHand()==true && Credit>20000 && VoucherTypeID != 1) 
       // {
          //  errorMessage = "Cash In Hand Should be Less than 20000";
        //}

        //else if (IsCostCentre && objCommon.Get_View_Table(SessionVoucherCostCentreDT, "Ledger_Id=" + LedgerId).Count == 0)
        //{
        //    errorMessage = "Please Enter Cost Centre Details";
        //}
        //else if (IsBillByBill && SessionVoucherBillByBillDT.Rows.Find(LedgerId) == null)
        //{
        //    errorMessage = "Please Enter Bill By Bill Details";
        //}
        //else if (IsBankApplicable && SessionVoucherBankDT.Rows.Find(LedgerId) == null)
        //{
        //    errorMessage = "Please Enter Bank Details";
        //}
        //else if (IsFBTApplicable && SessionVoucherFBTDT.Rows.Find(LedgerId) == null)
        //{
        //    errorMessage = "Please FBT Details";
        //}
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
        btn_Save.Attributes.Add("onclick",objCommon.ClickedOnceScript_For_JS_Validation(Page,btn_Save));

        objVoucherPresenter = new VoucherPresenter(this, IsPostBack);
             
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                lbl_VoucherType.Text = VoucherType;
            }

            if (keyID <=0 || IBTVoucherFlag=="Accept")
            {
               txt_VoucherNo.Text = objCommon.Get_Next_Number();
            }

            btn_FBTHelper.Visible = false;
            btn_TDSHelper.Visible = false;
            if (VoucherTypeID == 8)  
            {
                btn_FBTHelper.Visible = true;
                btn_TDSHelper.Visible = true;

                btn_FBTHelper.Attributes.Add("onclick", "return OpenSmallPopup('" + Util.GetBaseURL() + "/Finance/Accounting Vouchers/FrmFBTHelper.aspx" + "')");
                btn_TDSHelper.Attributes.Add("onclick", "return OpenSmallPopup('" + Util.GetBaseURL() + "/Finance/Accounting Vouchers/FrmTDSHelper.aspx" + "')");
            }


            //txt_VoucherNo.Text = "HO-V001";
        }

        System.Web.HttpContext.Current.Session["FromHelper"] = null;
        //Page.Title = VoucherType.ToUpper();
    }

    #endregion


    #region Voucher_GridEvents
    protected void dg_Voucher_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            InsertUpdateVoucher(e);
        }

        else if (e.CommandName == "Edit")
        {
            dg_Voucher.EditItemIndex = e.Item.ItemIndex;
            Bind_dg_Voucher = SessionVoucherDT;
            dg_Voucher.ShowFooter = false;

            //findControls(e.Item);
            //desableDebitCredit();
        }

        else if (e.CommandName == "Cancel")
        {
            dg_Voucher.EditItemIndex = -1;
            Bind_dg_Voucher = SessionVoucherDT;
            dg_Voucher.ShowFooter = true;
        }

        else if (e.CommandName == "Delete")
        {
            
        string ledger_id=SessionVoucherDT.Rows[e.Item.ItemIndex]["Ledger_Id"].ToString();

        foreach (DataRow Dr in SessionVoucherBillByBillDT.Rows)
        {
            if (Dr["Ledger_Id"].ToString() == ledger_id)
            {
                Dr.Delete();    
            }
        }

        foreach (DataRow Dr in SessionVoucherCostCentreDT.Rows)
        {
            if (Dr["Ledger_Id"].ToString() == ledger_id)
            {
                Dr.Delete();
            }
        }

            SessionVoucherBillByBillDT.AcceptChanges();
            SessionVoucherCostCentreDT.AcceptChanges();
            SessionVoucherDT.Rows[e.Item.ItemIndex].Delete();

            SessionVoucherDT.AcceptChanges();
            Bind_dg_Voucher = SessionVoucherDT;
        }
    }

    

    protected void dg_Voucher_ItemDataBound(object source, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnk_CostCentre = (LinkButton)e.Item.FindControl("lnk_CostCentre");
            LinkButton lnk_BillbyBill = (LinkButton)e.Item.FindControl("lnk_BillbyBill");
            LinkButton lnk_Bank = (LinkButton)e.Item.FindControl("lnk_Bank");
            LinkButton lnk_FBTCategory = (LinkButton)e.Item.FindControl("lnk_FBTCategory");
            Label lbl_CrDr = (Label)e.Item.FindControl("lbl_CrDr");

            string LedgerName = SessionVoucherDT.Rows[e.Item.ItemIndex]["Ledger_Name"].ToString();
            decimal Dr =convertToDecimal(SessionVoucherDT.Rows[e.Item.ItemIndex]["Debit"]);
            decimal Cr =convertToDecimal(SessionVoucherDT.Rows[e.Item.ItemIndex]["Credit"]);
            int LedgerID =convertToInt(SessionVoucherDT.Rows[e.Item.ItemIndex]["Ledger_Id"]);
            string CrDr = lbl_CrDr.Text.Trim();

            string qryString = "Ledger_Id=" + Util.EncryptInteger(LedgerID) + "&Voucher_Id=" + Util.EncryptInteger(keyID) + "&Ledger_Name=" + Util.EncryptString(LedgerName) + "&Voucher_Type=" + Util.EncryptString(VoucherType) + "&Credit=" + Util.EncryptDecimal(Cr) + "&Debit=" + Util.EncryptDecimal(Dr) + "&CrDr=" + Util.EncryptString(CrDr) + "&Mode=" + Mode + "&Menu_Item_Id=" + Menu_Item_Id;  
           
            lnk_CostCentre.OnClientClick = " return OpenPopup('" + Util.GetBaseURL() + "/Finance/Accounting Vouchers/FrmVoucherCostCentre.aspx?" + qryString + "')";
            lnk_BillbyBill.OnClientClick = "return OpenPopup('" + Util.GetBaseURL() + "/Finance/Accounting Vouchers/FrmVoucherBillByBill.aspx?" + qryString + "')";
            lnk_Bank.OnClientClick = "return OpenSmallPopup('" + Util.GetBaseURL() + "/Finance/Accounting Vouchers/FrmVoucherBank.aspx?" + qryString + "')";
            lnk_FBTCategory.OnClientClick = "return OpenSmallPopup('" + Util.GetBaseURL() + "/Finance/Accounting Vouchers/FrmVoucherFBT.aspx?" + qryString + "')";


            if (IBTVoucherFlag == "Accept")
            {
                //LinkButton Edit = (LinkButton)e.Item.FindControl("Edit");
                //LinkButton lnk_Delete = (LinkButton)e.Item.FindControl("lnk_Delete");
                if (e.Item.ItemIndex == 0)
                {
                    e.Item.Enabled = false;
                }
                
             
            }

            //Added By Hemant On 31 Mar 2021 For Accept IBT Vouchers
            if (IBTVoucherFlag == "Accept" && LedgerID == 1)
            {
                if (e.Item.ItemIndex == 1)
                {
                    e.Item.Enabled = false;
                }
            }

        }


        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            findControls(e.Item);

            if (_dgRowCount == 0)
            {
                ddl_dgDrCr.Enabled = false;

                if (VoucherTypeID == 8 || VoucherTypeID == 16 || VoucherTypeID == 6 || VoucherTypeID == 3333 || VoucherTypeID == 3)
                {ddl_dgDrCr.SelectedValue = "Dr";}
                else if (VoucherTypeID == 14 || VoucherTypeID == 1 || VoucherTypeID == 2222 || VoucherTypeID == 2)
                { ddl_dgDrCr.SelectedValue = "Cr"; }
            }


            if (e.Item.ItemType == ListItemType.Footer)
            {
                desableDebitCredit();
            }


            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_Ledger.DataTextField = "Ledger_Name";
                ddl_Ledger.DataValueField = "Ledger_Id";
                SetLedgerId(SessionVoucherDT.Rows[e.Item.ItemIndex]["Ledger_Id"].ToString(), SessionVoucherDT.Rows[e.Item.ItemIndex]["Ledger_Name"].ToString());

                LinkButton lnk_Delete = (LinkButton)e.Item.FindControl("lnk_Delete");
                lnk_Delete.Visible = false;

                desableDebitCredit();
            }

        }
    }

    #endregion


    private void findControls(DataGridItem item)
    {
        ddl_dgDrCr = (DropDownList)item.FindControl("ddl_CrDr");
        ddl_Ledger = (ClassLibrary.UIControl.DDLSearch)item.FindControl("ddl_Ledger");
        txt_Debit = (TextBox)item.FindControl("txt_Debit");
        txt_Credit = (TextBox)item.FindControl("txt_Credit");
    }



    protected void btn_Save_Click(object sender, EventArgs e)
    {
        //SessionVoucherCostCentreDT.RejectChanges();
        //SessionVoucherBillByBillDT.RejectChanges();
        //SessionVoucherBankDT.RejectChanges();
        //SessionVoucherFBTDT.RejectChanges();

     

      if(validateUI())
       objVoucherPresenter.Save();
    }

    private void ff()
    {
        ddl_Ledger = (ClassLibrary.UIControl.DDLSearch)dg_Voucher.FindControl("ddl_Ledger");
        ddl_dgDrCr = (DropDownList)dg_Voucher.FindControl("ddl_CrDr");
        Session["IDsForVoucher"] = "Journal" + "Ö" + dgDrCr;
       // ddl_Ledger.OtherColumns = VoucherType + "Ö" + dgDrCr;
    }

    protected void ddl_dgDrCr_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList  _ddl = (DropDownList)sender;
      DataGridItem _item = (DataGridItem)_ddl.Parent.Parent;
      findControls(_item);
      desableDebitCredit();
      SetLedgerId("", ""); 
    }


    private void desableDebitCredit()
    {
        if (dgDrCr == "Cr")
        {
            txt_Credit.Enabled = true;
            txt_Debit.Enabled = false;
        }
        else
        {
            txt_Debit.Enabled = true;
            txt_Credit.Enabled = false;
        }

        Session["IDsForVoucher"] = VoucherType + "Ö" + dgDrCr;
        //ddl_Ledger.OtherColumns = VoucherType + "Ö" + dgDrCr;
    }

    public void Delete_UnwantedRows_in_SessionBillByBill(DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {
            int exist = 0;

            foreach (DataRow dr1 in SessionVoucherDT.Rows)
            {
                if (Convert.ToInt32(dr["Ledger_Id"]) == Convert.ToInt32(dr1["Ledger_Id"]))
                {
                    exist = 1;
                    break;
                }
            }

            if (exist == 0)
            {
                dr.Delete();
            }
        }
    }

    
}














 


   
 

