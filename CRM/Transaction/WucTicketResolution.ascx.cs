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
using Raj.CRM.TransactionView;
using Raj.CRM.TransactionPresenter;
using System.Data.SqlClient;
using Raj.EC;
//using Raj.eCargo.Init;

public partial class CRM_Transaction_WucTicketResolution : System.Web.UI.UserControl,ITicketResolutionView
{
    #region ClassVariables
    DataSet objDS;
    TicketResolutionPresenter objTicketResolutionPresenter;    
    #endregion

    //#region ControlBind

    //public DataSet BindWhetherCustomerSatisfied
    //{
    //    set
    //    {
    //        ddl_CustomerSatisfied.DataTextField = "Whether_Customer_Satisfied";
    //        ddl_CustomerSatisfied.DataValueField = "Whether_Customer_Satisfied";
    //        ddl_CustomerSatisfied.DataSource = value;
    //        ddl_CustomerSatisfied.DataBind();

    //    }

    //}
   

    ////#endregion

    #region IView
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public bool validateUI()
    {
        bool _isValid = false;        
        errorMessage = "";

        if (txt_HowResolved.Text==string.Empty)
        {
            lbl_Errors.Text = "Please Enter How Resolved";
            txt_HowResolved.Focus();
            

        }
        else if (ddl_CustomerSatisfied.SelectedValue=="0" && txt_Reason.Text==string.Empty)
        {

            errorMessage = "Please Enter Reason";
            txt_Reason.Focus();
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    #endregion

    #region ControlValues
    public string HowResolved
    {
        set { txt_HowResolved.Text = value; }
        get { return txt_HowResolved.Text; }
    }

    public string TicketNo
    {
     set {lbl_Ticket.Text  = value; } 
              
    }
    public int TicketId
    {
        get { return Util.DecryptToInt(Request.QueryString["Ticket_Id"]);}
       
    }
    public string GCDocketNo
    {
        set { lbl_GCDocket.Text = value; }

    }
    public int GCDocketNoId
    {
        set { lbl_GCDocket.Text = Util.Int2String(value); }
        get { return Util.String2Int(lbl_GCDocket.Text); }
    }

    public int WhetherCustomerSatisfied
    {
        set{ddl_CustomerSatisfied.SelectedValue= Util.Int2String(value);}
        get { return Util.String2Int(ddl_CustomerSatisfied.SelectedValue); }
    }

    public string Reason
    {
        set { txt_Reason.Text = value; }
        get { return txt_Reason.Text; }
    }
    public bool Save
    {
        set { btn_Save.Visible = value; }
       
    }
    public bool HowResolve_1
    {
        set { txt_HowResolved.ReadOnly = value; }

    }
    public bool Reason_1
    {
        set { txt_Reason.ReadOnly = value; }

    }
    public bool CustomerSatisfied
    {
        set { ddl_CustomerSatisfied.Enabled = value; }
    }   

    #endregion

    #region OtherMethod
    private void EnableReason()
    {
        if (ddl_CustomerSatisfied.SelectedValue=="1")
        {
            lbl_Reason.Visible = false;
            txt_Reason.Visible = false;
            td_Mandetory.Visible = false;

        }
        else
        {
            lbl_Reason.Visible = true;
            txt_Reason.Visible = true;
            td_Mandetory.Visible = true;
        }
    }

   
    public DataSet SaveVisibile()
    {
         objTicketResolutionPresenter.ReadValues();
         if (objDS.Tables[0].Rows.Count > 0)
         {
             btn_Save.Enabled = false;
         }
         else
         {
             btn_Save.Enabled = true;
         }
         return objDS;
    }

    //private void RadioButtonChange()
    //{
    //    if (rbl_SearchBy.Items[0].Selected==true)
    //    {
    //        lbl_Head.Text = "Ticket No";
    //    }
    //    else if (_isVT == true && rbl_SearchBy.Items[1].Selected == true)
    //    {
    //        lbl_Head.Text = "GC No";
    //        rbl_SearchBy.Items[1].Text = "GC No";
    //    }
    //    else if ( rbl_SearchBy.Items[1].Selected == true)
    //    {
    //        lbl_Head.Text = " Docket No";
    //        rbl_SearchBy.Items[1].Text = "Docket No";
    //    }
    //}

    //private void DefaultSetting()
    //{
    //   rbl_SearchBy.Items[0].Selected = true;
    //   lbl_Head.Text = "Ticket No";    
       
    //}
    #endregion

    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {

       // Util.DecryptToString(Request.QueryString["Id"]);
   
        
        //if (TicketNoId == 1)
        //{
        //    btn_Save.Visible = false;
        //    txt_HowResolved.ReadOnly = true;
        //    txt_Reason.ReadOnly = true;
        //}
        //else
        //{
        //    btn_Save.Visible =true;
        //    txt_HowResolved.ReadOnly = false;
        //    txt_Reason.ReadOnly = false;
        //}
        
        objTicketResolutionPresenter = new TicketResolutionPresenter(this, IsPostBack);
        EnableReason();
        lbl_GCDocketNo.Text = CompanyManager.getCompanyParam().GcCaption + " No :";
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            objTicketResolutionPresenter.save();
        }
    }

    protected void ddl_CustomerSatisfied_SelectedIndexChanged(object sender, EventArgs e)
    {
        EnableReason();
    }

    //protected void rbl_SearchBy_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    RadioButtonChange();
    //    objTicketResolutionPresenter.initValues();
    //}

   
    #endregion


   
}


