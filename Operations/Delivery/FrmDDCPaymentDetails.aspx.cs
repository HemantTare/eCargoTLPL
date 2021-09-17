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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;
using Raj.EC;
using System.Data.SqlClient;

public partial class Operations_Delivery_FrmDDCPaymentDetails : System.Web.UI.Page
{
    DataTable objDT = new DataTable();
    DataTable SessionDT = null;
    DataTable SessionReasonForNonDelivery = null;
    int ds_Index,_menuItemId;
    string Crypt, Menuitem, DeliveryStatusID, Article_Id, GC_Id;
    DataRow dr;
    string Mode = "0";
    int DDCID = 0;
    DateTime hdn_DDS_Date;
   
    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    
   
    public int BillingPartyId
    {
        set { hdn_BillingPartyId.Value = Util.Int2String(value); }
        get { return hdn_BillingPartyId.Value == string.Empty ? 0 : Util.String2Int(hdn_BillingPartyId.Value); }
    }
    public string BillingParty
    {
        set
        {
            txt_BillingParty.Text = value.Trim();
            hdn_BillingParty.Value = value.Trim();
        }
    }
    public string BillingHierarchy
    {
        set
        {
            ddl_BillingHierarchy.SelectedValue = value;
            hdn_BillingHierarchy.Value = value.Trim();
        }
        get
        {
            return hdn_BillingHierarchy.Value;
        }
    }
    public int BillingLocationId
    {
        set { hdn_BillingLocationId.Value = Util.Int2String(value); }
        get { return hdn_BillingLocationId.Value == string.Empty ? 0 : Util.String2Int(hdn_BillingLocationId.Value); }
    }
    public string BillingLocation
    {
        set
        {
            txt_BillingLocation.Text = value.Trim();
            hdn_BillingLocation.Value = value.Trim();
        }
    }
    public decimal BillingParty_MinimumBalance
    {
        set { hdn_Billing_Party_MinimumBalance.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_Billing_Party_MinimumBalance.Value); }
    }
    public decimal Billing_Party_CreditLimit
    {
        set { hdn_Billing_Party_CreditLimit.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_Billing_Party_CreditLimit.Value); }
    }
    public decimal BillingParty_Ledger_Closing_Balance
    {
        set { hdn_Billing_Party_Ledger_Closing_Balance.Value = Util.Decimal2String(Math.Round(value, 2)); }
        get { return Util.String2Decimal(hdn_Billing_Party_Ledger_Closing_Balance.Value); }
    }
    public string BillingRemark
    {
        set
        { 
            hdn_BillingRemark.Value = value.Trim();
        }
        get { return hdn_BillingRemark.Value.Trim(); }
    }
    public DateTime ChequeDate
    {
        set { dtp_ChequeDate.SelectedDate = value; }
        get { return dtp_ChequeDate.SelectedDate; }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.OperationModel.NewGCSearch));
        Mode = Request.QueryString["Mode"].ToString();
        Crypt = Request.QueryString["ds_index"];
        Menuitem = Request.QueryString["_menuItemid"];
        DeliveryStatusID = Request.QueryString["DeliveryStatusID"];
        ds_Index = Util.DecryptToInt(Crypt);
        _menuItemId = Util.DecryptToInt(Menuitem);
        Article_Id = Request.QueryString["Article_Id"];
        DDCID = Util.String2Int(Request.QueryString["DDCId"]);

        if (_menuItemId == 82)  //Door Delivery Sheet
        {
            hdn_DDS_Date = Convert.ToDateTime(Request.QueryString["hdn_DDS_Date"]);
        }
        else if (_menuItemId == 80) //Godown Delivery Sheet
        {
            hdn_DDS_Date = Convert.ToDateTime(Request.QueryString["hdn_GDC_Date"]);
        }

        
        GC_Id = Request.QueryString["GC_Id"];
        if (_menuItemId == 82)  //Door Delivery Sheet
        {
            SessionDT = StateManager.GetState<DataTable>("BindDDSGrid");
        }
        else if (_menuItemId == 80) //Godown Delivery Sheet
        {
            SessionDT = StateManager.GetState<DataTable>("BindGDCGrid");
        }
           SessionReasonForNonDelivery = StateManager.GetState<DataTable>("BindDDLUndelReason");

        if (!IsPostBack)
        {
            //dtp_ChequeDate.AutoPostBackOnSelectionChanged = false;
            //dtp_ChequeDate.SelectedDate = DateTime.Now; 
            ChequeDate = DateTime.Now; 

            Disable_control(DeliveryStatusID);
            FillControls(DeliveryStatusID);

            if (Mode == "4")
            {
                btn_Save.Visible = false;
                pnl_Cheque.Enabled = false;
                pnl_Credit.Enabled = false;
                pnl_Return.Enabled = false;
                dtp_ChequeDate.Enabled = false;
            }
        }
    }

    private void GetCouponDetails()
    {
        if (txt_CouponNo.Text.Length  > 0)
        {
            int GCAmount, CouponValue, Balance;

            DAL objDAL = new DAL();
            DataSet ds = new DataSet();
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@GCID", SqlDbType.Int, 0, GC_Id),
            objDAL.MakeInParams("@DDCDate", SqlDbType.DateTime,0, hdn_DDS_Date),
            objDAL.MakeInParams("@CouponNo", SqlDbType.VarChar,10, txt_CouponNo.Text)};
            objDAL.RunProc("EC_Opr_Get_CouponDetails", objSqlParam, ref ds);


            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = ds.Tables[0].Rows[0];

                lbl_CouponValue.Text = objDR["CouponAmount"].ToString();
                CouponValue = Convert.ToInt32(objDR["CouponAmount"].ToString());
                GCAmount = Convert.ToInt32(objDR["Total_GC_Amount"].ToString());

                Balance = GCAmount - CouponValue;

                if (Balance > 0)
                {
                    lbl_ToBePaid.Text = Convert.ToString(Balance);

                    btn_Save.Focus();
                }
                else
                {
                    lbl_ToBePaid.Text = "0";
                }

                errorMessage = "";

                //Add_In_Dataset();

                //string popupScript = "<script language='javascript'>updateparent();</script>";
                //System.Web.UI.ScriptManager.RegisterStartupScript(lbl_Errors, typeof(String), "PopupScript", popupScript.ToString(), false);

                btn_Save.Focus();
            }
            else
            {
                txt_CouponNo.Text = "";
                lbl_CouponValue.Text = "0";

                lbl_ToBePaid.Text = lbl_GCAmount.Text;

                errorMessage = "Invalid Coupon No.";

                //txt_CouponNo.Focus();
            }
        }
    }

    protected void btn_ValidateCouponNo_Click(object sender, EventArgs e)
    {
        GetCouponDetails();
    }

    private void FillControls(string DeliveryStatusID)
    {
        //1	C Cash //2 Q Cheque //3 R Credit //4 T Return //7 P Pending
        if (DeliveryStatusID == "4" || DeliveryStatusID == "7")
        {
            DAL objDAL = new DAL();
            DataSet ds = new DataSet();
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@DeliveryStatusID", SqlDbType.Int, 0, Util.String2Int(DeliveryStatusID)),
            objDAL.MakeInParams("@DDCID", SqlDbType.Int, 0, DDCID),
            objDAL.MakeInParams("@GC_Id", SqlDbType.Int, 0, GC_Id)};
            objDAL.RunProc("EC_Opr_Get_UndeliveredReason", objSqlParam, ref ds);

            ddl_ReasonForNonDelivery.DataTextField = "Reason";
            ddl_ReasonForNonDelivery.DataValueField = "Reason_ID";
            ddl_ReasonForNonDelivery.DataSource = ds;
            ddl_ReasonForNonDelivery.DataBind();
            ddl_ReasonForNonDelivery.Items.Insert(0, new ListItem("Select One", "0"));
        }
        else if (DeliveryStatusID == "8" )
        {
            btn_Save.Text="Apply Coupon";
            btn_Exit.Text = "Close";
        }

        if (SessionDT.Rows.Count > 0)
        {
            for (int i = 0; i <= SessionDT.Rows.Count - 1; i++)
            {
                if (DeliveryStatusID == "2" && Convert.ToInt32(SessionDT.Rows[i]["GC_ID"]) == Convert.ToInt32(GC_Id))
                {
                    //if (Convert.ToInt32(SessionDT.Rows[i]["Dly_Pay_Mode_Id"]) == 2 && Convert.ToInt32(SessionDT.Rows[i]["GC_ID"]) == Convert.ToInt32(GC_Id))
                    //{
                        //dtGC_ID

                        txt_ChequeNo.Text = SessionDT.Rows[i]["ChequeNo"].ToString();
                        dtp_ChequeDate.SelectedDate = Convert.ToDateTime(SessionDT.Rows[i]["ChequeDate"].ToString());
                        txt_BankName.Text = SessionDT.Rows[i]["BankName"].ToString();
                        lbl_GCAmount.Text = Convert.ToString(SessionDT.Rows[i]["Total_GC_Amount"]);
                    //}
                }
                else if (DeliveryStatusID == "3" && Convert.ToInt32(SessionDT.Rows[i]["GC_ID"]) == Convert.ToInt32(GC_Id))
                {
                    txt_BillingParty.Text = SessionDT.Rows[i]["PartyName"].ToString();
                    hdn_BillingPartyId.Value = SessionDT.Rows[i]["Client_ID"].ToString();
                    txt_BillingLocation.Text = SessionDT.Rows[i]["PartyfromBranchLocation"].ToString();
                    hdn_BillingLocation.Value = SessionDT.Rows[i]["PartyfromBranchLocation"].ToString();

                    hdn_BillingLocationId.Value = SessionDT.Rows[i]["BillingLocationId"].ToString();
                    hdn_Billing_Party_MinimumBalance.Value = SessionDT.Rows[i]["MinimumBalance"].ToString();
                    hdn_Billing_Party_Ledger_Closing_Balance.Value = SessionDT.Rows[i]["Ledger_Closing_Balance"].ToString();
                    hdn_Billing_Party_CreditLimit.Value = SessionDT.Rows[i]["CreditLimit"].ToString();
                }
                else if ((DeliveryStatusID == "4" || DeliveryStatusID == "7") && Convert.ToInt32(SessionDT.Rows[i]["GC_ID"]) == Convert.ToInt32(GC_Id) && (_menuItemId == 82 || _menuItemId == 80))
                {

                    ddl_ReasonForNonDelivery.SelectedValue = SessionDT.Rows[i]["ReasonForNonDly"].ToString();
                }

                else if ((DeliveryStatusID == "8") && Convert.ToInt32(SessionDT.Rows[i]["GC_ID"]) == Convert.ToInt32(GC_Id))
                {

                    txt_CouponNo.Text = Convert.ToString(SessionDT.Rows[i]["CouponNo"]);
                    lbl_GCAmount.Text = Convert.ToString(SessionDT.Rows[i]["Total_GC_Amount"]);
                    lbl_CouponValue.Text = Convert.ToString(SessionDT.Rows[i]["CouponAmount"]);
                    lbl_ToBePaid.Text = Convert.ToString(SessionDT.Rows[i]["BalanceToBePaid"]);
                }
            }
        }

    }

    private void Disable_control(string DeliveryStatusID)
    {
        //1	C Cash //2 Q Cheque //3 R Credit //4 T Return 
        if (DeliveryStatusID == "2")
        {
            pnl_Cheque.Enabled = true;
            pnl_Credit.Enabled = false;
            pnl_Return.Enabled = false;
            pnl_Coupon.Enabled = false;

            pnl_Cheque.Visible = true;
            pnl_Credit.Visible = false;
            pnl_Return.Visible = false;
            pnl_Coupon.Visible = false;

            lbl_Heading.Text = "Cheque Details";

        }
        else if (DeliveryStatusID == "3")
        {
            pnl_Cheque.Enabled = false;
            pnl_Credit.Enabled = true;
            pnl_Return.Enabled = false;
            pnl_Coupon.Enabled = false;

            pnl_Cheque.Visible = false;
            pnl_Credit.Visible = true;
            pnl_Return.Visible = false;
            pnl_Coupon.Visible = false;

            lbl_Heading.Text = "Billing Party Details";
        }
        else if (DeliveryStatusID == "4")
        {
            pnl_Cheque.Enabled = false;
            pnl_Credit.Enabled = false;
            pnl_Return.Enabled = true;
            pnl_Coupon.Enabled = false;

            pnl_Cheque.Visible = false;
            pnl_Credit.Visible = false;
            pnl_Return.Visible = true;
            pnl_Coupon.Visible = false;

            lbl_Heading.Text = "Return Reason";
            lbl_ReasonForNonDelivery.Text = "Reason For Non Delivery:";
        }
        else if (DeliveryStatusID == "7")
        {
            pnl_Cheque.Enabled = false;
            pnl_Credit.Enabled = false;
            pnl_Return.Enabled = true;
            pnl_Coupon.Enabled = false;

            pnl_Cheque.Visible = false;
            pnl_Credit.Visible = false;
            pnl_Return.Visible = true;
            pnl_Coupon.Visible = false;

            lbl_Heading.Text = "Pending Freight";
            lbl_ReasonForNonDelivery.Text = "Reason For Pending Freight:";
        }

        else if (DeliveryStatusID == "8")
        {
            pnl_Cheque.Enabled = false;
            pnl_Credit.Enabled = false;
            pnl_Return.Enabled = false;
            pnl_Coupon.Enabled = true;

            pnl_Cheque.Visible = false;
            pnl_Credit.Visible = false;
            pnl_Return.Visible = false;
            pnl_Coupon.Visible = true;

            lbl_Heading.Text = "Discount Coupon";

        }

    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        
        if (AlowToSave() == true)
        {
            Add_In_Dataset();

            string popupScript = "<script language='javascript'>updateparent();</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(lbl_Errors, typeof(String), "PopupScript", popupScript.ToString(), false);

            if (DeliveryStatusID != "8")
            {
                Response.Write("<script language='javascript'>{self.close()}</script>");
            }
        }
    }

     private void Add_In_Dataset()
    {
        if (SessionDT.Rows.Count > 0)
        {
            dr = SessionDT.Rows[ds_Index];
 
            dr["is_updated"] = 1;
            if (_menuItemId == 82 || _menuItemId == 80) //Door && Godown Delivery
            {

                if (DeliveryStatusID == "2")
                {
                    dr["ChequeNo"] = txt_ChequeNo.Text;
                    dr["ChequeDate"] = ChequeDate.ToString("dd MMM yyyy");
                    dr["BankName"] = txt_BankName.Text;
                    dr["Client_ID"] = 0;  
                    dr["PartyName"] = "";
                    dr["BillingLocationId"] = 0;
                    dr["PartyfromBranchLocation"] = "";
                    dr["MinimumBalance"] = 0;
                    dr["Ledger_Closing_Balance"] = 0;
                    dr["CreditLimit"] = 0;
                    dr["ReasonForNonDly"] = 0;

                    dr["CouponNo"] = "";
                    dr["CouponAmount"] = 0;
                    dr["BalanceToBePaid"] = 0;

                    if (_menuItemId == 82)
                    { dr["Remark"] = ""; }
                }
                else if (DeliveryStatusID == "3")
                {
                    dr["ChequeNo"] = "";
                    dr["ChequeDate"] = ChequeDate.ToString("dd MMM yyyy");
                    dr["BankName"] = "";

                    if (Convert.ToDecimal(hdn_Billing_Party_MinimumBalance.Value)>0 && Convert.ToDecimal(hdn_Billing_Party_Ledger_Closing_Balance.Value) < Convert.ToDecimal(hdn_Billing_Party_MinimumBalance.Value))
                    {
                        errorMessage = "Aapka Balance Khatam Huva. Om Turanth Me Payment Bhejo.";

                        dr["Client_ID"] = 0;
                        dr["PartyName"] = "";
                        dr["BillingLocationId"] = 0;
                        dr["PartyfromBranchLocation"] = "";
                        dr["MinimumBalance"] = 0;
                        dr["Ledger_Closing_Balance"] = 0;
                        dr["CreditLimit"] = 0;
                    }
                    else if (Convert.ToDecimal(hdn_Billing_Party_CreditLimit.Value) > 0 && (Convert.ToDecimal(hdn_Billing_Party_Ledger_Closing_Balance.Value) + Convert.ToDecimal(hdn_Billing_Party_CreditLimit.Value)) <=0 )
                    {
                        errorMessage = "Aapka Credit Limmit Khatam Huva. Om Turanth Me Payment Bhejo.";

                        dr["Client_ID"] = 0;
                        dr["PartyName"] = "";
                        dr["BillingLocationId"] = 0;
                        dr["PartyfromBranchLocation"] = "";
                        dr["MinimumBalance"] = 0;
                        dr["Ledger_Closing_Balance"] = 0;
                        dr["CreditLimit"] = 0;
                    }
                    else
                    {
                        dr["Client_ID"] = hdn_BillingPartyId.Value;
                        dr["PartyName"] = txt_BillingParty.Text;
                        dr["BillingLocationId"] = hdn_BillingLocationId.Value;
                        dr["PartyfromBranchLocation"] = hdn_BillingLocation.Value;
                        dr["MinimumBalance"] = hdn_Billing_Party_MinimumBalance.Value;
                        dr["Ledger_Closing_Balance"] = hdn_Billing_Party_Ledger_Closing_Balance.Value;
                        dr["CreditLimit"] = hdn_Billing_Party_CreditLimit.Value;

                    }

                    ////dr["Client_ID"] = hdn_BillingPartyId.Value;
                    ////dr["PartyName"] = txt_BillingParty.Text;
                    ////dr["BillingLocationId"] = hdn_BillingLocationId.Value;
                    ////dr["PartyfromBranchLocation"] = hdn_BillingLocation.Value;
                    ////dr["MinimumBalance"] = hdn_Billing_Party_MinimumBalance.Value;
                    ////dr["Ledger_Closing_Balance"] = hdn_Billing_Party_Ledger_Closing_Balance.Value;
                    dr["ReasonForNonDly"] = 0;

                    dr["CouponNo"] = "";
                    dr["CouponAmount"] = 0;
                    dr["BalanceToBePaid"] = 0;

                    if (_menuItemId == 82)
                    { dr["Remark"] = ""; } 
                }
                else if (DeliveryStatusID == "4" || DeliveryStatusID == "7")
                {
                    dr["ChequeNo"] = "";
                    dr["ChequeDate"] = ChequeDate.ToString("dd MMM yyyy");
                    dr["BankName"] = "";
                    dr["Client_ID"] = 0;
                    dr["PartyName"] = ""; 
                    dr["BillingLocationId"] = 0;
                    dr["PartyfromBranchLocation"] = "";
                    dr["MinimumBalance"] = 0;
                    dr["Ledger_Closing_Balance"] = 0;
                    dr["CreditLimit"] = 0;

                    dr["CouponNo"] = "";
                    dr["CouponAmount"] = 0;
                    dr["BalanceToBePaid"] = 0;

                    dr["ReasonForNonDly"] = ddl_ReasonForNonDelivery.SelectedValue;
                    if (_menuItemId == 82)
                    { dr["Remark"] = ""; }                      
                }
                else if (DeliveryStatusID == "8")
                {
                    dr["ChequeNo"] = "";
                    dr["ChequeDate"] = ChequeDate.ToString("dd MMM yyyy");
                    dr["BankName"] = "";
                    dr["Client_ID"] = 0;
                    dr["PartyName"] = "";
                    dr["BillingLocationId"] = 0;
                    dr["PartyfromBranchLocation"] = "";
                    dr["MinimumBalance"] = 0;
                    dr["Ledger_Closing_Balance"] = 0;
                    dr["CreditLimit"] = 0;
                    dr["ReasonForNonDly"] = 0;

                    dr["CouponNo"] = txt_CouponNo.Text;
                    dr["CouponAmount"] = Convert.ToInt32(lbl_CouponValue.Text);
                    dr["BalanceToBePaid"] = Convert.ToInt32(lbl_ToBePaid.Text);

                    if (_menuItemId == 82)
                    { dr["Remark"] = ""; }
                }

                if (_menuItemId == 82) //Door Delivery
                {
                    StateManager.SaveState("BindDDSGrid", SessionDT);
                }
                else if(_menuItemId == 80) // Godown Delivery
                {
                    StateManager.SaveState("BindGDCGrid", SessionDT);
                }
            }

        }
        
 
    }

    private bool AlowToSave()
    {
        bool ATS = true;

         
        if (DeliveryStatusID == "2")
        {
            if (txt_ChequeNo.Text == "")
            {
                errorMessage = "Please Enter Cheque No";
                txt_ChequeNo.Focus();
                ATS = false;
                
            }
            else if(txt_BankName.Text == "") 
            {
                errorMessage = "Please Enter Bank Name";
                txt_BankName.Focus();
                ATS = false;
                
            }
            else if (ChequeDate < Convert.ToDateTime(hdn_DDS_Date))
            {
                errorMessage = "Cheque Date Should be Greater or Equal to DDS Date";
                txt_BankName.Focus();
                ATS = false;
            }
            else if (Convert.ToInt32(lbl_GCAmount.Text) < 500)
            {
                errorMessage = "Cheque Can Not Be Accepted For Freight Less Than Rs. 500.";
                txt_ChequeNo.Focus();
                ATS = false;
            }
            else
            {
                ATS = true;
            }
        }
        else if (DeliveryStatusID == "3")
        {
            if (txt_BillingParty.Text == "")
            {
                errorMessage = "Please Enter Party Name";
                txt_BillingParty.Focus();  
                ATS = false;                
            }
            else if (hdn_BillingLocation.Value == "")
            {
                errorMessage = "Please Enter Billing Branch";
                txt_BillingParty.Focus();
                ATS = false;
            }
            else if (BillingParty_MinimumBalance>0 && (BillingParty_Ledger_Closing_Balance <= BillingParty_MinimumBalance))
            {
                errorMessage = "Aapka Balance Khatam Huva. Om Turanth Me Payment Bhejo.";
                btn_Exit.Focus(); 
                ATS = false;
            }
            else if (Billing_Party_CreditLimit > 0 && ((Billing_Party_CreditLimit + BillingParty_Ledger_Closing_Balance) <=0))
            {
                errorMessage = "Aapka Credit Limit Khatam Huva. Om Turanth Me Payment Bhejo.";
                btn_Exit.Focus();
                ATS = false;
            }
            else
            {
                ATS = true;
            }
        }
        else if (DeliveryStatusID == "4")
        {
           if (ddl_ReasonForNonDelivery.SelectedIndex == 0 )
           {
               errorMessage = "Please Select Valid Reason for Non Delivery";
               ddl_ReasonForNonDelivery.Focus();
               ATS = false;
           }
           else
           {
               ATS = true;
           }
        }
        else if (DeliveryStatusID == "7")
        {
            if (ddl_ReasonForNonDelivery.SelectedIndex == 0)
            {
                errorMessage = "Please Select Valid Reason for Pending Freight";
                ddl_ReasonForNonDelivery.Focus();
                ATS = false;
            }
            else
            {
                ATS = true;
            }
        }
        else if (DeliveryStatusID == "8")
        {
            if (txt_CouponNo.Text.Trim() == "" && Convert.ToInt32(lbl_CouponValue.Text) <= 0)
            {
                errorMessage = "Please Enter Valid Coupon No.";
                txt_CouponNo.Focus();
                ATS = false;
            }
            else
            {
                ATS = true;
                if (SessionDT.Rows.Count > 0)
                {
                    for (int i = 0; i <= SessionDT.Rows.Count - 1; i++)
                    {
                        if (i != ds_Index)
                        {
                            if (txt_CouponNo.Text == SessionDT.Rows[i]["CouponNo"].ToString())
                            {
                                errorMessage = "Coupon No. Already Used";
                                ATS = false;
                                break;
                            }
                        }
                    }
                }
            }
        } 
        
        return ATS;
    }
   
    
    protected void btn_Exit_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}
