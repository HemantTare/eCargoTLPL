using System;
using System.Data;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;

/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 16/10/2008
/// Description   : This Page is For  branch bank selection 
/// </summary>

public partial class FA_Common_Accounting_Masters_WucBranchBankSelection : System.Web.UI.UserControl,IBranchBankSelectionView 
{
    #region ClassVariables
    BranchBankSelectionPresenter objBranchBankSelectionPresenter;
    DataRow DR = null;


    #endregion

    #region ControlsValues

    public DataSet SessionChkLedgers
    {
        get { return StateManager.GetState<DataSet>("ChkLedgers"); }
        set { StateManager.SaveState("ChkLedgers", value); }
    }

    public int BranchID
    {
        set { ddl_BranchName.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_BranchName.SelectedValue); }

    }
    public int ChkLedgers
    {
        set
        {
            ChkList_Ledger.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ChkList_Ledger.SelectedValue);
        }
    }
    
    public string SelectedHerch 
    {
        get 
        {
            if (rdl_HO_Branch.SelectedValue == "1")
            { 
                return "BO";
            }
            else
            {
                return "HO";
            }
        }
    }
    //public int Is_HO
    //{
    //    set
    //    {
    //        rdl_HO_Branch.SelectedValue = Util.Int2String(value);
    //    }
    //    get
    //    {
    //        return Util.String2Int(rdl_HO_Branch.SelectedValue);
    //    }
    //}

    #endregion

    # region OtherMethod
    private DataSet MakeDS()
    {
        int cnt;
        DataSet objDS;
        objDS = SessionChkLedgers;
        objDS.Tables[0].TableName = "ChkLedgerDetails";
        objDS.Clear();


        for (cnt = 0; cnt < ChkList_Ledger.Items.Count; cnt++)
        {
            if (ChkList_Ledger.Items[cnt].Selected == true)
            {
                DR = objDS.Tables[0].NewRow();
                DR["Ledger_ID"] = ChkList_Ledger.Items[cnt].Value;
                DR["Ledger_Name"] = ChkList_Ledger.Items[cnt].Text;
                objDS.Tables["ChkLedgerDetails"].Rows.Add(DR);
            }
        }

        //Ds_VendorTypeDetail.GetXml();        
        SessionChkLedgers = objDS;
        return objDS;

    }
    public void ClearVariables()
    {
        SessionChkLedgers = null;
    }
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (ddl_BranchName.Visible == true && Util.String2Int(ddl_BranchName.SelectedValue) == 0)
        {
            errorMessage = "Please Select Branch Name";
            _isValid = false;
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


    public int keyID
    {
        get
        {
            return 0;

        }
    }

    #endregion

    #region ControlsBind

    public DataSet Bind_ddl_Branch_Name
    {
        set
        {
            ddl_BranchName.DataTextField = "Branch_Name";
            ddl_BranchName.DataValueField = "Branch_Id";
            ddl_BranchName.DataSource = value;
            ddl_BranchName.DataBind();
            ddl_BranchName.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataSet Bind_Chk_Ledgers
    {
        set
        {
            ChkList_Ledger.DataTextField = "Ledger_Name";
            ChkList_Ledger.DataValueField = "Ledger_ID";
            ChkList_Ledger.DataSource = value;
            SessionChkLedgers = value;
            ChkList_Ledger.DataBind();

            int i;
            if (BranchID > 0 || Util.String2Int(rdl_HO_Branch.SelectedValue)==2)
            {
                for (i = 0; i < SessionChkLedgers.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToBoolean(SessionChkLedgers.Tables[0].Rows[i]["Checked"]))
                    {
                        ChkList_Ledger.Items[i].Selected = true;
                    }
                }
            }

        }
    }

    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        Raj.EC.Common objCommon = new Raj.EC.Common();
        btn_Save.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));
        objBranchBankSelectionPresenter = new BranchBankSelectionPresenter(this, IsPostBack);
        string _Hiearachy_Code = UserManager.getUserParam().HierarchyCode;// Param.getUserParam().HierarchyCode.ToString();
        if (_Hiearachy_Code == "AO" || _Hiearachy_Code == "RO" || _Hiearachy_Code == "BO")
        {
            rdl_HO_Branch.Style.Add("display", "none");
            lbl_Select.Style.Add("display", "none");
        }
        else
        {
            rdl_HO_Branch.Visible = true;
            lbl_Select.Visible = true;
        }

        if (BranchID <= 0 && rdl_HO_Branch.SelectedValue == "1")
        {
            ChkList_Ledger.Visible = false;
        }
        else
        {
            ChkList_Ledger.Visible = true;
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        MakeDS();
        objBranchBankSelectionPresenter.save();
    }
    
    protected void ddl_BranchName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (BranchID > 0)
        {
            ChkList_Ledger.Visible = true;
            objBranchBankSelectionPresenter.FillOnBranchNameChanged();
        }
        else
        {
            ChkList_Ledger.Visible = false;
        }
    }
    
    protected void rdl_HO_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(rdl_HO_Branch.SelectedValue) == 2)
        {     
            ddl_BranchName.Visible = false;
            lbl_Branch_Name.Visible = false;
            lbl_Mandotary.Visible = false;
            //Is_HO = 2;
        }
        else
        {     
            ddl_BranchName.Visible = true;
            lbl_Branch_Name.Visible = true;
            lbl_Mandotary.Visible = true;
            //Is_HO = 1;    

        }
        ddl_BranchName.SelectedValue = "0";

        
        objBranchBankSelectionPresenter.FillOnBranchNameChanged();

        if (rdl_HO_Branch.SelectedValue == "1" && BranchID <= 0)
        {
            ChkList_Ledger.Visible = false;
        }
        else
        {
            ChkList_Ledger.Visible = true;
        }
       
    }
    
#endregion
}
