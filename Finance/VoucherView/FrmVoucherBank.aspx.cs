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

public partial class Finance_VoucherView_FrmVoucherBank : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DAL objDAL = new DAL();

    public string LedgerName
    {
        get
        {
            return Util.DecryptToString(Request.QueryString["Ledger_Name"]);
        }
        set
        {
            lbl_LedgerName.Text = value;
        }
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

    public int VoucherDetailsID
    {
        get { return Convert.ToInt32(Request.QueryString["VoucherDetailsID"]); }
    }

    public string BankName
    {
        set
        {
            lbl_BankName.Text = value;
        }
    }

    public string ChequeNo
    {
        set
        {
            lbl_ChequeNo.Text = value;
        }
    }

    public string ChequeDate
    {
        set
        {
            lbl_ChequeDate.Text = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlParameter[] objSqlParam = { 
                                        objDAL.MakeInParams("@flag",SqlDbType.Int,0,5),
                                        objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int,0,VoucherDetailsID)//UserManager.getUserParam().MainId)
                                     };

        if (IsIBT == false)
        {
            objDAL.RunProc("EC_FA_Mst_VoucherView_ReadValues", objSqlParam, ref ds);
        }
        else
        {
            objDAL.RunProc("FA_Opr_IBT_VoucherView_ReadValues", objSqlParam, ref ds);
        }

        DataRow Dr = ds.Tables[0].Rows[0];

        LedgerName = LedgerName;

        if (Dr != null)
        {
            BankName = Dr["Bank_Name"].ToString();
            ChequeNo = Dr["Cheque_No"].ToString();
            ChequeDate = Dr["Cheque_Date"].ToString();
        }
       
          

    }
}
