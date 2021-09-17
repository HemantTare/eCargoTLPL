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
/// Description   : This Is The Form For Leder VoucherFBT Details
/// </summary>

public partial class Finance_Accounting_Vouchers_VoucherFBT : System.Web.UI.UserControl
{
    #region ClassVariables

    Common objCommon = new Common();

    #endregion
    #region ControlsValues

    private int FBTCategoryId
    {
        get { return Convert.ToInt32(ddl_FBTCategory.SelectedValue); }
        set { ddl_FBTCategory.SelectedValue =value.ToString(); }
    }


    private bool IsRecoveryAmount
    {
        get { return chk_IsRecoveryAmount.Checked; }
        set { chk_IsRecoveryAmount.Checked = value; }
    }

    private string VoucherType
    {
        get 
        { 
            return Util.DecryptToString(Request.QueryString["Voucher_Type"]); 
        }
    }

    #endregion

    #region ControlsBind



    public DataTable SesssionVoucherDT
    {
        set { StateManager.SaveState("Voucher_DT", value); }
        get { return StateManager.GetState<DataTable>("Voucher_DT"); }
    }

   
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (FBTCategoryId<=0)
        {
            errorMessage = "Please Select FBT Category ";
        }
        else
        {
            _isValid = true;
        }
        return _isValid;  
    }


    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
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

    public string LedgerName
    {
        get
        {
           return Util.DecryptToString(Request.QueryString["Ledger_Name"]);
            //return "name";
        }
    }
    #endregion


    #region OtherProperties

    public DataTable bind_ddl_FBTCategory
    {
        set
        {
            ddl_FBTCategory.DataTextField = "FBT_Category_Name";
            ddl_FBTCategory.DataValueField = "FBT_Category_Id";
            ddl_FBTCategory.DataSource = value;
            ddl_FBTCategory.DataBind();
            ddl_FBTCategory.Items.Insert(0, new ListItem("---Select One---", "0"));
        }
    }
    

    #endregion


    #region OtherMethods


    


    #endregion


    #region  ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Save.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));

        if (!IsPostBack)
        {
            DAL objDAL = new DAL();
            DataSet objDS = new DataSet();
            SqlParameter[] objSqlParam = { 
                                            objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,LedgerId)
                                         };

            objDAL.RunProc("EC_FA_Mst_VoucherFBT_FillValues", objSqlParam, ref objDS);

            bind_ddl_FBTCategory = objDS.Tables[0];

            DataRow Dr = SesssionVoucherDT.Rows.Find(LedgerId);

            FBTCategoryId =Util.String2Int(Dr["FBT_Category_Id"].ToString());
            IsRecoveryAmount = Convert.IsDBNull(Dr["Is_FBT_Recovery"]) == true ? false : Convert.ToBoolean(Dr["Is_FBT_Recovery"]); 

            lbl_LedgerName.Text = LedgerName;
        }

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

    #endregion
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            save();
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx");
        }
    }

    private void save()
    {
        DataRow Dr = SesssionVoucherDT.Rows.Find(LedgerId);

        if (Dr == null)
        {
            Dr = SesssionVoucherDT.NewRow();
            Dr["Ledger_Id"] = LedgerId;
            Dr["FBT_Category_Id"] = FBTCategoryId;
            Dr["Is_FBT_Recovery"] = IsRecoveryAmount;
            SesssionVoucherDT.Rows.Add(Dr);
        }
        else 
        {
            Dr["Ledger_Id"] = LedgerId;
            Dr["FBT_Category_Id"] = FBTCategoryId;
            Dr["Is_FBT_Recovery"] = IsRecoveryAmount;
        }
        SesssionVoucherDT.AcceptChanges();
    }

    
}














 


   
 

