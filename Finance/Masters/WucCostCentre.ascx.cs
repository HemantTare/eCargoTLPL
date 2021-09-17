using System;
using System.Data;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView ;
using Raj.EC.FinanceModel;
using Raj.EC;

/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 15/10/2008
/// Description   : This Page is For  Cost centre details
/// </summary>

public partial class Finance_Masters_WucCostCentre : System.Web.UI.UserControl, ICostCentreView
{
    #region ClassVariables
    CostCentrePresenter objCostCentrePresenter;

    #endregion

    #region ControlEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        objCostCentrePresenter = new CostCentrePresenter(this, IsPostBack);

        Common objCommon = new Common();
        hdf_ResourceString.Value = objCommon.GetResourceString("Finance/Masters/App_LocalResources/WucCostCentre.ascx.resx");
        btn_Save.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));
        if (!IsPostBack)
        {
            CostCentreModel _CostCentreModel = new CostCentreModel(this);
            DataSet objDS;
            objDS = _CostCentreModel.ReadValues();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                int i = objDS.Tables[0].Rows.Count;
                int j = ChkBoxLst_Ledgers.Items.Count;
                Txt_Cost_Centre_Name.Text = objDS.Tables[0].Rows[0]["Cost_Centre_Name"].ToString();
                DDL_Under_Cost_Centre.SelectedValue = objDS.Tables[0].Rows[0]["Parent_Cost_Centre_ID"].ToString();

                for (i = 0; i <= objDS.Tables[0].Rows.Count - 1; i++)
                {
                    for (j = 0; j <= ChkBoxLst_Ledgers.Items.Count - 1; j++)
                    {
                        if (ChkBoxLst_Ledgers.Items[j].Value == objDS.Tables[0].Rows[i]["Ledger_Id"].ToString())
                        {
                            ChkBoxLst_Ledgers.Items[j].Selected = true;
                        }
                    }
                }
            }
        }
    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objCostCentrePresenter.Save();
    }
    #endregion

    #region ControlsValues

    public string Cost_Centre_Name
    {
        get { return Txt_Cost_Centre_Name.Text.Trim(); }
        set { Txt_Cost_Centre_Name.Text = value; }
    }

    public string Cost_Centre_ID
    {
        get { return DDL_Under_Cost_Centre.SelectedValue; }
        set { DDL_Under_Cost_Centre.SelectedValue = value; }
    }


    #endregion

    #region ControlsBind

    public DataSet Bind_DDL_Under
    {
        set
        {
            DDL_Under_Cost_Centre.DataTextField = "Cost_Centre_Name";
            DDL_Under_Cost_Centre.DataValueField = "Cost_Centre_ID";
            DDL_Under_Cost_Centre.DataSource = value;
            DDL_Under_Cost_Centre.DataBind();
            DDL_Under_Cost_Centre.Items.Insert(0, new ListItem("Select Parent Cost Centre", "0"));
        }
    }

    public DataSet Bind_CheckBoxList_Ledger
    {
        set
        {
            ChkBoxLst_Ledgers.DataTextField = "Ledger_Name";
            ChkBoxLst_Ledgers.DataValueField = "Ledger_Id";
            ChkBoxLst_Ledgers.DataSource = value;
            ChkBoxLst_Ledgers.DataBind();
        }
    }


    public string xmlLedgerId
    {
        get
        {
            DataTable dt = new DataTable("tbl_Cost_Centre");
            dt.Columns.Add("Ledger_Id");
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            DataRow dr;
            int i;
            for (i = 0; i <= ChkBoxLst_Ledgers.Items.Count - 1; i++)
            {
                if (ChkBoxLst_Ledgers.Items[i].Selected == true)
                {
                    dr = ds.Tables["tbl_Cost_Centre"].NewRow();
                    dr["Ledger_Id"] = ChkBoxLst_Ledgers.Items[i].Value;
                    ds.Tables["tbl_Cost_Centre"].Rows.Add(dr);
                }
            }
            string XML;
            XML = ds.GetXml();
            return XML;
        }
    }
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (Txt_Cost_Centre_Name.Text.Trim() == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_Txt_Cost_Centre_Name").ToString();  // 
            Txt_Cost_Centre_Name.Focus();
            _isValid = false;
        }

        else if (ChkLstBox_Select_Atleast_One_Ledger() == false)
        {
            errorMessage = "Select Atleast One Ledger"; //GetLocalResourceObject("Msg_txt_HierarchyName").ToString();
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
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    #endregion

    #region OtherProperties
    #endregion

    #region OtherMethods

    public Boolean ChkLstBox_Select_Atleast_One_Ledger()
    {
        Boolean found = false;
        int i;
        for (i = 0; i <= ChkBoxLst_Ledgers.Items.Count - 1; i++)
        {
            if (ChkBoxLst_Ledgers.Items[i].Selected == true)
            {
                found = true;
                break;
            }
        }

        return found;
    }


    #endregion

}
