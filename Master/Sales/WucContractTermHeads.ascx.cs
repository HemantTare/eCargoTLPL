using System;
using System.Data;

using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;
using ClassLibraryMVP;
using Raj.EC;

/// Author:		Ankit champaneriya
/// Create date: 13-10-2008
/// Desc : TO INSERT / UPDATE THE Contract Term  heads details
/// 

public partial class Master_Sales_WucContractTermHeads : System.Web.UI.UserControl, IContractTermHeadsView
{
    #region ClassVariables
    ContractTermHeadsPresenter objContractTermHeadsPresenter;
    #endregion

    #region Control Events

    protected void Page_Load(object sender, EventArgs e)
    {
        objContractTermHeadsPresenter = new ContractTermHeadsPresenter(this, IsPostBack);      
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objContractTermHeadsPresenter.Save();
    }
    #endregion

    #region ControlsValues

    public string TermHead
    {
        get { return txt_TermHead.Text.Trim(); }
        set { txt_TermHead.Text = value; }
    }

    public string Description
    {
        get { return txt_Description.Text.Trim(); }
        set { txt_Description.Text = value; }
    }

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_TermHead.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Term Head";
            txt_TermHead.Focus();
        }
        else if (txt_Description.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Description";
            txt_Description.Focus();
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
}