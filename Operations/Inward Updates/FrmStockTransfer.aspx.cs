using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
using Raj.EC;

/// <summary>
/// Author : Ankit champaneriya
/// Date   : 07-01-09
/// </summary>
/// 

public partial class Operations_Inward_Updates_FrmStockTransfer : ClassLibraryMVP.UI.Page, IStockTransferView
{
    #region ClassVariables
    StockTransferPresenter objStockTransferPresenter;
    Common objCommon = new Common();
    string _GC_No_XML;
    #endregion

    #region Properties
    public int BranchId
    {
        get { return Wuc_Region_Area_Branch1.BranchID; }
    }
    public int NewCurrentBranchId
    {
        get { return Util.String2Int(ddl_NewCurrentBranch.SelectedValue); }
    }
    public String Reason
    {
        get { return txt_Reason.Text; }
    }
    public DateTime TransactionDate
    {
        get { return WucDatePicker1.SelectedDate; }
    }
    #endregion

    #region ControlsBind

    public DataTable BindDDLStockTransfer
    {
        set
        {
            ddl_NewCurrentBranch.DataTextField = "Branch_Name";
            ddl_NewCurrentBranch.DataValueField = "Branch_Id";
            ddl_NewCurrentBranch.DataSource = value;
            ddl_NewCurrentBranch.DataBind();
            if (keyID <= 0)
            {
                ddl_NewCurrentBranch.Items.Insert(0, new ListItem("Select One", "0"));
            }
        }
    }

    public void BindDGStockTransfer()
    {
        dg_StockTransfer.DataSource = SessionDGStockTransfer;
        dg_StockTransfer.DataBind();
    }

    public DataTable SessionDGStockTransfer
    {
        get { return StateManager.GetState<DataTable>("BindStockTransfer"); }
        set
        {
            StateManager.SaveState("BindStockTransfer", value);
            if (StateManager.Exist("BindStockTransfer"))
            {
                BindDGStockTransfer();
            }
        }
    }

    public String GCXML
    {
        get
        {
            if (_GC_No_XML != null)
            {
                return _GC_No_XML.ToString().ToLower();
            }
            else
            {
                return "<NewDataSet/>";
            }
        }
        set { _GC_No_XML = value; }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (NewCurrentBranchId <= 0)
        {
            errorMessage = "Please Select New Current Branch";
            ddl_NewCurrentBranch.Focus();
        }
        else if (TransactionDate > DateTime.Now)
        {
            errorMessage = "Please Select Valid Transaction Date";
        }
        else if (BranchId <= 0)
        {
            errorMessage = "Please Select Branch";
        }
        else if (dg_StockTransfer.Items.Count == 0)
        {
            errorMessage = "Please Insert Atleast One " + CompanyManager.getCompanyParam().GcCaption;
        }
        else if (Reason == string.Empty)
        {
            errorMessage = "Please Enter Reason";
            txt_Reason.Focus();
        }
        else if (GridValidation() == false)
        {
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
        set { lbl_Error.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    #endregion

    #region other Method
    private bool GridValidation()  //added Ankit : 23-02-09 
    {
        bool ATS = true;
        Label lbl_AUSDate;
        string GCNo;
        int i = 0;

        if (dg_StockTransfer.Items.Count > 0)
        {
            for (i = 0; i <= dg_StockTransfer.Items.Count - 1; i++)
            {
                GCNo = dg_StockTransfer.Items[i].Cells[0].Text;
                lbl_AUSDate = (Label)dg_StockTransfer.Items[i].FindControl("lbl_AUSDate");

                if (TransactionDate < Convert.ToDateTime(lbl_AUSDate.Text))
                {
                    errorMessage = "Transaction Date can't be less than Dcument Date(" + lbl_AUSDate.Text + ") For " + CompanyManager.getCompanyParam().GcCaption + " : " + GCNo;
                    ATS = false;
                    break;
                }
            }
        }

        return ATS;
    }

    private void SetStandardCaption()
    {
        WucSelectedItems1.SetFoundCaption = "Enter  " + CompanyManager.getCompanyParam().GcCaption + "  Nos.:";
        WucSelectedItems1.SetNotFoundCaption = CompanyManager.getCompanyParam().GcCaption + "  Nos.Not Found :";
        WucSelectedItems1.Set_GCCaption = CompanyManager.getCompanyParam().GcCaption;
    }

    public void ClearVariables() // added Ankit
    {
        SessionDGStockTransfer = null;
    }

    private void OnGetGCXML(object sender, EventArgs e)
    {
        _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
        objStockTransferPresenter.fillgrid();
        if (SessionDGStockTransfer.Rows.Count > 0)
        {
            WucSelectedItems1.dtdetails = SessionDGStockTransfer;
            pnl_StockTransfer.Visible = true;
        }
        else
            pnl_StockTransfer.Visible = false;
        WucSelectedItems1.Get_Not_Selected_Items();
    }
    #endregion

    #region Event Click
    protected void Page_Load(object sender, EventArgs e)
    {
        objStockTransferPresenter = new StockTransferPresenter(this, IsPostBack);

        WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);
        btn_Save.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));

        if (!IsPostBack)
        {
            Wuc_Region_Area_Branch1.SetDDLBranchAutoPostback = true;
            objCommon.SetStandardCaptionForGrid(dg_StockTransfer);
        }
        Wuc_Region_Area_Branch1.BranchIndexChange += new EventHandler(OnGetGCXML);
        SetStandardCaption();
        WucSelectedItems1.SetsmallTextboxWidth();
    }
    
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objStockTransferPresenter.Save();
    }

    protected void ddl_NewCurrentBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        OnGetGCXML(sender, e);
    }

    #endregion

    
}