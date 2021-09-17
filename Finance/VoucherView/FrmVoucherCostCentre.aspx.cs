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

public partial class Finance_VoucherView_FrmVoucherCostCentre : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DAL objDAL = new DAL();

    public string LedgerName
    {
        set { lbl_LedgerName.Text = value; }
        get { return Util.DecryptToString(Request.QueryString["Ledger_Name"]); }
    }

    public int Ledger_ID
    {
        get
        {
            return Convert.ToInt32(Request.QueryString["Ledger_Id"]);
        }
    }

    public string Upto
    {
        set { lbl_Upto.Text = value; }
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
        get { return Util.DecryptToInt(Request.QueryString["Voucher_Id"]); }
    }

    public DataTable BindCostCentre
    {
        set
        {
            dg_CostCentre.DataSource = value;
            dg_CostCentre.DataBind();
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

    protected void Page_Load(object sender, EventArgs e)
    {

        SqlParameter[] objSqlParam = { 
                                        objDAL.MakeInParams("@flag",SqlDbType.Int,0,2),
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


        SetValues();
    }

    public void SetValues()
    {
        LedgerName = LedgerName;
        Upto = UptoAmount;

        BindCostCentre = ds.Tables[0];
    }

}
