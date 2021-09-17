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
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;

/// <summary>
/// Author        : Aashish Lad
/// Created On    : 25th October 2008
/// Description   : This is the Page For LHPO Forms First Tab takes LHPO Penalties Alert
/// </summary>

public partial class Operations_Outward_WucLHPOPenalties : System.Web.UI.UserControl,ILHPOPenaltiesView 
{
    #region ClassVariables
    LHPOPenaltiesPresenter objLHPOPenaltiesPresenter;    
    LinkButton lbtn_Delete;

    DataSet objDS;
    private ScriptManager scm_LHPOPenalties;
    bool isValid = false;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    #endregion

    #region ControlsValue

    
    #endregion

    #region ControlsBind

    public DataSet Bind_dg_IncentiveDetails
    {
        set
        {
            // SessionContractTermsGrid = value;
            dg_IncentiveDetails.DataSource = value;
            dg_IncentiveDetails.DataBind();
        }
    }
    public DataSet Bind_dg_PenaltyDetails
    {
        set
        {
            // SessionContractTermsGrid = value;
            dg_PenaltyDetails.DataSource = value;
            dg_PenaltyDetails.DataBind();
        }
    }
    public DataSet SessionIncentiveDetailsGrid
    {
        get { return StateManager.GetState<DataSet>("IncentiveDetails"); }
        set { StateManager.SaveState("IncentiveDetails", value); }
    }
    public DataSet SessionPenaltyDetailsGrid
    {
        get { return StateManager.GetState<DataSet>("PenaltyDetails"); }
        set { StateManager.SaveState("PenaltyDetails", value); }
    }
    public string IncentiveDetailsXML
    {
        get
        {
            return SessionIncentiveDetailsGrid.GetXml().ToLower();
        }
    }
    public string PenaltyDetailsXML
    {
        get
        {
            return SessionPenaltyDetailsGrid.GetXml().ToLower();
        }
    }
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = true;

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
            //return -1;
        }
    }

    #endregion


    #region OtherProperties
    public ScriptManager SetScriptManager
    {
        set { scm_LHPOPenalties = value; }
    }
    #endregion


    #region OtherMethods
    //private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    //{
    //    objDS = SessionContractTermsGrid;
    //    DataRow DR = null;
    //    ddl_TermsHead = (DropDownList)e.Item.FindControl("ddl_TermsHead");
    //    txt_Description = (TextBox)e.Item.FindControl("txt_Description");

    //    lbl_Terms_ID = (Label)e.Item.FindControl("lbl_Terms_ID");

    //    if (e.CommandName == "ADD")
    //    {
    //        DR = objDS.Tables[0].NewRow();

    //    }
    //    if (e.CommandName == "Update")
    //    {
    //        DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
    //    }

    //    if (Allow_To_Add_Update() == true)
    //    {

    //        DR["Term_ID"] = ddl_TermsHead.SelectedValue;
    //        DR["Term_Head"] = ddl_TermsHead.SelectedItem.Text;
    //        DR["Description"] = txt_Description.Text;
    //        if (e.CommandName == "ADD")
    //        {
    //            objDS.Tables[0].Rows.Add(DR);
    //        }
    //        SessionContractTermsGrid = objDS;
    //    }
    //}
    //private bool Allow_To_Add_Update()
    //{
    //    if (Util.String2Int(ddl_TermsHead.SelectedValue) == 0)
    //    {

    //        errorMessage = "Please Select Terms Head";
    //    }
    //    else
    //        isValid = true;

    //    return isValid;
    //}
    #endregion
    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion
}
