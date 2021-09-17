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
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC;

public partial class Finance_VoucherView_WucVoucher : System.Web.UI.UserControl
{
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    public string RefNo
    {
        set { lbl_RefNo.Text = value; }
    }

    public string VoucherNo
    {
        set { lbl_VoucherNo.Text = value; }
    }

    public string Narration
    {
        set { txt_Narration.Text = value; }
    }

    public bool IsIBT
    {
        get 
            {
                if (Request.QueryString["IsIBT"] == null)
                {
                    return false;
                }
                else
                {
                    return Convert.ToBoolean(Request.QueryString["IsIBT"]);
                }
                
            }
    }

    public bool Tr_Heading
    {
        set
        {
            tr_Heading.Visible = value;
        }
    }


    public string FBTPaymentType
    {
        set {
                if (value == "0")
                {
                    lbl_FBTPaymentType.Text = "";
                }
                else
                {
                    lbl_FBTPaymentType.Text = value;
                }
            }
    }

    public string VoucherDate
    {
        set { lbl_VoucherDate.Text = value;}
    }

    public string TotalDebit
    {
        set { lbl_TotalDebit.Text = value; }
    }

    public string TotalCredit
    {
        set {lbl_TotalCredit.Text = value;}
    }

    public string Voucher_Type
    {
        set { lbl_VoucherType.Text = value;}
        get {
                if (lbl_VoucherType.Text.Trim() == "")
                {
                    return "";
                }
                else
                {
                    return lbl_VoucherType.Text;
                }
            }
    }

    public DataTable BindGrid
    {
        set
        {
            dg_Voucher.DataSource = value;
            dg_Voucher.DataBind();
        }
    }

    public int Voucher_ID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]);}
    }

    public string convertToDrCr(object value)
    {
        if (convertToDecimal(value) > 0)
        { return "Dr"; }
        else { return "Cr"; }
    }

    public decimal convertToDecimal(object value)
    {
        if (Convert.IsDBNull(value) || value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Convert.ToDecimal(value); }
    }

    public int convertToInt(object value)
    {
        if (value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Convert.ToInt32(value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        VoucherReadValues();
        SetValues();
    }

    public void VoucherReadValues()
    {
        SqlParameter[] objSqlParam = { 
                                        objDAL.MakeInParams("@flag",SqlDbType.Int,0,1),
                                        objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int,0,Voucher_ID)//UserManager.getUserParam().MainId)
                                     };

        if (IsIBT == false)
        {
            objDAL.RunProc("EC_FA_Mst_VoucherView_ReadValues", objSqlParam, ref ds);
        }
        else
        {
            objDAL.RunProc("FA_Opr_IBT_VoucherView_ReadValues", objSqlParam, ref ds);
        }

        ds.Tables[0].TableName = "Voucher";
        ds.Tables[1].TableName = "VoucherDetails";
       
    }

    public void SetValues()
    {
        DataRow DR = ds.Tables["Voucher"].Rows[0];

        RefNo = DR["Ref_No"].ToString();
        VoucherNo = DR["Voucher_No"].ToString();
        VoucherDate = DR["Voucher_Date"].ToString();
        Narration = DR["Narration"].ToString();
        TotalCredit = DR["Total_Credit"].ToString();
        TotalDebit = DR["Total_Debit"].ToString();
        Voucher_Type = DR["Voucher_Name"].ToString(); 
        BindGrid = ds.Tables["VoucherDetails"];

        if (DR["FBT_Payment_Type"].ToString().Trim() == "" || DR["FBT_Payment_Type"].ToString() == "0")
        {
            tr_FBT.Visible = false;
        }
        else
        {
            tr_FBT.Visible = true;
            FBTPaymentType = DR["FBT_Payment_Type"].ToString();
        }
        
    }
    
    
    protected void dg_Voucher_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnk_CostCentre = (LinkButton)e.Item.FindControl("lnk_CostCentre");
            LinkButton lnk_BillbyBill = (LinkButton)e.Item.FindControl("lnk_BillbyBill");
            LinkButton lnk_Bank = (LinkButton)e.Item.FindControl("lnk_Bank");
            LinkButton lnk_FBTCategory = (LinkButton)e.Item.FindControl("lnk_FBTCategory");
            Label lbl_CrDr = (Label)e.Item.FindControl("lbl_CrDr");

            string LedgerName = ds.Tables["VoucherDetails"].Rows[e.Item.ItemIndex]["Ledger_Name"].ToString();
            decimal Dr = convertToDecimal(ds.Tables["VoucherDetails"].Rows[e.Item.ItemIndex]["Debit"]);
            decimal Cr = convertToDecimal(ds.Tables["VoucherDetails"].Rows[e.Item.ItemIndex]["Credit"]);
            int LedgerID = convertToInt(ds.Tables["VoucherDetails"].Rows[e.Item.ItemIndex]["Ledger_Id"]);
            int VoucherDetailsID = convertToInt(ds.Tables["VoucherDetails"].Rows[e.Item.ItemIndex]["Details_ID"]);
            string CrDr = lbl_CrDr.Text.Trim();

            string qryString = "Ledger_Id=" + LedgerID + "&Voucher_Id=" + Util.EncryptInteger(Voucher_ID) + "&Ledger_Name=" + Util.EncryptString(LedgerName) + "&Voucher_Type=" + Voucher_Type + "&Credit=" + Util.EncryptDecimal(Cr) + "&Debit=" + Util.EncryptDecimal(Dr) + "&IBT=" + IsIBT.ToString() + "&VoucherDetailsID=" + VoucherDetailsID;

            lnk_CostCentre.OnClientClick = " return OpenPopup('" + Util.GetBaseURL() + "/Finance/VoucherView/FrmVoucherCostCentre.aspx?" + qryString + "')";
            lnk_BillbyBill.OnClientClick = "return OpenPopup('" + Util.GetBaseURL() + "/Finance/VoucherView/FrmVoucherBillByBill.aspx?" + qryString + "')";
            lnk_Bank.OnClientClick = "return OpenSmallPopup('" + Util.GetBaseURL() + "/Finance/VoucherView/FrmVoucherBank.aspx?" + qryString + "')";
            lnk_FBTCategory.OnClientClick = "return OpenSmallPopup('" + Util.GetBaseURL() + "/Finance/VoucherView/FrmVoucherFBT.aspx?" + qryString + "')";
            
        }

    }
}
