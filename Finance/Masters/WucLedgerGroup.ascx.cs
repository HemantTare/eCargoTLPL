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
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
/// <summary>
/// Author : Sunil
/// Date Created : 4/10/2008
/// Description : This page is For Add And Edit LedgerGroup, 
/// Change Hostory :
/// Changed By    Date(DD/MM/YYYY)      Description
/// ================================================
/// </summary>

public partial class Finance_Masters_WucLedgerGroup : System.Web.UI.UserControl, ILedgerGroupView
{


    #region ClassVariables
     LedgerGroupPresenter objLedgerGroupPresenter;
    #endregion


    #region ControlsValues



    public string LedgerGroupName
    {
        set { txt_Ledger_Gr_Name.Text = value; }
        get { return txt_Ledger_Gr_Name.Text.Trim(); } 
    }

    public string Alias
    {
        set { txt_Alias.Text = value; }
        get { return txt_Alias.Text; }
    }
    public int UnderId
    {
        set { ddl_Parent_Ldg_Grp.SelectedValue = value.ToString(); }
        get { return Util.String2Int(ddl_Parent_Ldg_Grp.SelectedValue); }
    }

    public string NatureName
    {
        set {
                if (value.Trim() != string.Empty)
                {
                    ddl_Nature.SelectedValue = value.Trim();
                }
            }
        get { return ddl_Nature.SelectedItem.Text; }
    }

    public int IndexNo
    {
        set { txt_Index_no.Text = value.ToString(); }
        get {return Util.String2Int(txt_Index_no.Text); }
    }

    public bool IsAffectGrossProfit
    {
        set { Chk_Gross_Profit.Checked = value; }
        get { return Chk_Gross_Profit.Checked; }
    }



    public void bind_ddl_Nature()
    {
        ddl_Nature.Items.Add(new ListItem("Assets","Assets"));
        ddl_Nature.Items.Add(new ListItem("Expenses","Expenses"));
        ddl_Nature.Items.Add(new ListItem("Income","Income"));
        ddl_Nature.Items.Add(new ListItem("Liabilities","Liabilities"));
    }
   
    public DataTable bind_ddl_Under
    {
        set
        {
            ddl_Parent_Ldg_Grp.DataTextField = "Ledger_Group_Name";
            ddl_Parent_Ldg_Grp.DataValueField = "Ledger_Group_ID";
            ddl_Parent_Ldg_Grp.DataSource = value;
            ddl_Parent_Ldg_Grp.DataBind();
        }
    }

    public bool EnableControls
    {
        set 
        {
            ddl_Parent_Ldg_Grp.Enabled = value;
            ddl_Nature.Enabled = value;
            Chk_Gross_Profit.Enabled = value;
        }
    }

    

    #endregion

    

    #region IView
    public bool validateUI()
    {
      bool _isValid = false;

      if (LedgerGroupName == string.Empty)
      {
          errorMessage = "Please Enter Ledger Group Name";
      }
      else { _isValid = true; }
         
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
            //return Util.DecryptToInt(Request.QueryString["Id"]); 
            //return Util.String2Int(Request.QueryString["Id"]); 
           // return Raj.CRM.Common.Null2Int(Request.QueryString["Id"]);
          
        }
    }

    #endregion


    #region OtherProperties
     
    #endregion


    #region OtherMethods

    #endregion
 

    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        //Param.ValidateSession();
        Raj.EC.Common objCommon = new Raj.EC.Common();
        btn_save.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_save));
        if (!IsPostBack)
        {
            bind_ddl_Nature();
        }
         objLedgerGroupPresenter = new LedgerGroupPresenter(this, IsPostBack);

        
    }

    #endregion
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        objLedgerGroupPresenter.Save();
    }
}
 














 


   
 
