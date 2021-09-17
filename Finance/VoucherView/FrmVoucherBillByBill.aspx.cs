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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using System.Data.SqlClient;

public partial class Finance_VoucherView_FrmVoucherBillByBill : System.Web.UI.Page
{

    DAL objDAL = new DAL();
    DataSet ds = new DataSet();


    public string LedgerName
    {
        set {lbl_LedgerName.Text = value;}
        get { return Util.DecryptToString(Request.QueryString["Ledger_Name"]); }
    }

    public int Ledger_ID
    {
        get 
        {
            return Convert.ToInt32(Request.QueryString["Ledger_Id"]);
        }
    }

    public bool IsIBT
    {
        get
        {
            return Convert.ToBoolean(Request.QueryString["IBT"]);
        }
    }

    public int Voucher_ID
    {
        get { return Util.DecryptToInt(Request.QueryString["Voucher_Id"]) ;}
    }

    public string Upto
    {
        set {lbl_Upto.Text = value;}
    }

    public DataTable BindBillByBill
    {
        set
            {
                dg_BillByBill.DataSource = value;
                dg_BillByBill.DataBind();
            }
    }

    public decimal Debit
    {
        get { return Math.Abs(convertToDecimal(Util.DecryptToDecimal(Request.QueryString["Debit"]))); }
    }

    public decimal Credit
    {
        get { return Math.Abs(convertToDecimal(Util.DecryptToDecimal(Request.QueryString["Credit"]))); }
    }

    public decimal convertToDecimal(object value)
    {
        if (Convert.IsDBNull(value) || value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Convert.ToDecimal(value); }
    }
    
    public string UptoAmount
    {
        get
        {
            if (Debit > 0)
            { return Debit.ToString() + " Dr"; }
            else { return Credit.ToString() + " Cr"; }
        }
    }

    public string convertToDrCr(object value)
    {
        if (convertToDecimal(value) > 0)
        { return "Cr"; }
        else { return "Dr"; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetValues();
    }

    public void SetValues()
    {
        LedgerName = LedgerName;
        Upto = UptoAmount;
        
        SqlParameter[] objSqlParam = { 
                                        objDAL.MakeInParams("@flag",SqlDbType.Int,0,3),
                                        objDAL.MakeInParams("@Ledger_Id",SqlDbType.Int,0,Ledger_ID),
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


        if (ds.Tables[0].Rows.Count > 0)
        {
            if (Util.String2Int(ds.Tables[0].Rows[0]["TDS_Ledger_Id"].ToString()) > 0)
            {
                dg_BillByBill.Columns[4].Visible = true;
            }
            else
            {
                dg_BillByBill.Columns[4].Visible = false;
            }
        }
        else
        {
            dg_BillByBill.Columns[4].Visible = false;
        }
        BindBillByBill = ds.Tables[0];
    }
}
