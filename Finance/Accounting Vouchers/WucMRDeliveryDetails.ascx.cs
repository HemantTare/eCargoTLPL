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
using Raj.EC;

public partial class Finance_Accounting_Vouchers_WucMRDeliveryDetails : System.Web.UI.UserControl,IMRDeliveryDetailsView
{
    MRDeliveryDetailsPresenter objMRDeliveryDetailsPresenter;
    private ScriptManager _scmmrdlydetials;
    Common objcommon = new Common();

    #region Control Values
    public string ThroughMr
    {
        get { return txt_ThroughMr.Text; }
        set { txt_ThroughMr.Text = value; }
    }

    public int DeliveredToID
    {
        get { return Util.String2Int(ddl_DeliveredTo.SelectedValue); }
        set { ddl_DeliveredTo.SelectedValue = Util.Int2String(value); }
    }

    public int DeliveryAgainstID
    {
        get { return Util.String2Int(ddl_DeliveryAgainst.SelectedValue); }
        set { ddl_DeliveryAgainst.SelectedValue = Util.Int2String(value); }
    }

    public ScriptManager SetScriptManager
    {
        set { _scmmrdlydetials = value; }

    }
    #endregion

    #region ControlsBind

    private void BindDeliveredTo()
    {

            ddl_DeliveredTo.DataSource = SessionDeliveredTo;
            ddl_DeliveredTo.DataTextField = "Delivery_To";
            ddl_DeliveredTo.DataValueField = "Delivery_To_Id";
            ddl_DeliveredTo.DataBind();
            if (keyID < 0)
            {
                ddl_DeliveredTo.Items.Insert(0, new ListItem("Select One", "0"));
            }

    }

    public DataTable SessionDeliveredTo
    {
        get { return StateManager.GetState<DataTable>("DeliveredTo"); }
    }   

    public void BindDeliveredAgainst()
    {
       
            DataView dv = new DataView();           
            DataTable dt = SessionDeliveredAgainst;
            dv = objcommon.Get_View_Table(dt, "Delivery_To_Id= " + ddl_DeliveredTo.SelectedValue);
            ddl_DeliveryAgainst.DataSource = dv;
            ddl_DeliveryAgainst.DataTextField = "Delivery_Against";
            ddl_DeliveryAgainst.DataValueField = "Delivery_Against_Id";           
            ddl_DeliveryAgainst.DataBind();            

    }

    public DataTable SessionDeliveredAgainst
    {
        get { return StateManager.GetState<DataTable>("DeliveredAgainst"); }
    }
    #endregion
   
     #region IView

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set
        {
           lbl_Errors.Text = value;
        }
    }

    public bool validateUI()
    {
        bool _isValid = false;
        errorMessage = "";

        if (txt_ThroughMr.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Through MR. Name";
            txt_ThroughMr.Focus();
        }
        else if (DeliveredToID <= 0)
        {
            errorMessage = "please Select Delivered To";
            ddl_DeliveredTo.Focus();
        }
        else if (DeliveryAgainstID <= 0)
        {
            errorMessage = "please Select Delivery Against";
            ddl_DeliveryAgainst.Focus();
        }
        else
        {
            _isValid = true;
        }
        return _isValid;

    }
     #endregion

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindDeliveredTo();
        }
        objMRDeliveryDetailsPresenter = new MRDeliveryDetailsPresenter(this, IsPostBack);
       
}
    protected void ddl_DeliveredTo_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindDeliveredAgainst();
    }
    #endregion

   
}
