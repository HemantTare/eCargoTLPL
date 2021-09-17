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

public partial class Finance_VoucherView_FrmVoucherFBT : System.Web.UI.Page
{
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

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

    public string FBTCategory
    {
        set
        {
            lbl_FBTCategory.Text = value;
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

    private bool IsRecoveryAmount
    {
        get { return chk_IsRecoveryAmount.Checked; }
        set { chk_IsRecoveryAmount.Checked = value; }
    }

    private string VoucherType
    {
        get
        { return Request.QueryString["Voucher_Type"];}
    }

    public DataSet SessionVoucher
    {
        set { StateManager.SaveState("VoucherView", value); }
        get { return StateManager.GetState<DataSet>("VoucherView"); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        LedgerName = LedgerName;



        SqlParameter[] objSqlParam = { 
                                        objDAL.MakeInParams("@flag",SqlDbType.Int,0,4),
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

        DataRow DR = ds.Tables[0].Rows[0];

        FBTCategory = DR["FBT_Category_Name"].ToString();
        IsRecoveryAmount = Convert.IsDBNull(DR["Is_FBT_Recovery"]) == true ? false : Convert.ToBoolean(DR["Is_FBT_Recovery"]);


        if (VoucherType == "Receipt")
        {
            chk_IsRecoveryAmount.Visible = true;
            lbl_IsRecoveryAmount.Visible = true;
        }
        else
        {
            chk_IsRecoveryAmount.Visible = false;
            lbl_IsRecoveryAmount.Visible = false;
        }
    }
}
