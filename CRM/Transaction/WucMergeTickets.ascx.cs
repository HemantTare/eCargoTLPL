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
using Raj.CRM.TransactionsPresenter;
using Raj.CRM.TransactionsView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.CRM;
using Raj.EC;

/// <summary>
/// Author : Sunil
/// Date Created : 10/Sep/2008
/// Description : This page is For Add And Edit MergeTickets Transactions, 
/// Change Hostory :
/// Changed By    Date(DD/MM/YYYY)      Description
/// ================================================
/// </summary>

public partial class CRM_WucMergeTickets : System.Web.UI.UserControl, IMergeTicketsView
{
    #region ClassVariables
     MergeTicketsPresenter objMergeTicketsPresenter;
     Common objCommon = new Common();
    #endregion

    #region ControlsValues

    public int GcDocId
    {
        set {ddl_GcDoc.SelectedValue = value.ToString(); }
        get { return Util.String2Int(ddl_GcDoc.SelectedValue); }
    }

    public int FromTicketId
    {
        set { ddl_FromTicket.SelectedValue = value.ToString(); }
        get { return Util.String2Int(ddl_FromTicket.SelectedValue); }
    }

    public int ToTicketId
    {
        set { ddl_ToTicket.SelectedValue = value.ToString(); }
        get { return Util.String2Int(ddl_ToTicket.SelectedValue); }
    }

    public DataTable bind_ddl_FromTicket
    {
        set
        {
            ddl_FromTicket.DataTextField = "Ticket_No";
            ddl_FromTicket.DataValueField = "Ticket_Id";
            ddl_FromTicket.DataSource = value;
            ddl_FromTicket.DataBind();
            ddl_FromTicket.Items.Insert(0, new ListItem("---Select One---","0"));
        }
    }

    public DataTable bind_ddl_ToTicket
    {
        set
        {
            ddl_ToTicket.DataTextField = "Ticket_No";
            ddl_ToTicket.DataValueField = "Ticket_Id";
            ddl_ToTicket.DataSource = value;
            ddl_ToTicket.DataBind();
            //ddl_ToTicket.Items.Insert(0, new ListItem( "---Select One---","0"));
        }
    }
    
    public DataTable bind_ddl_GcDoc
    {
        set
        {
            ddl_GcDoc.DataTextField = "GcDocNo";
            ddl_GcDoc.DataValueField = "GcDocNo";
            ddl_GcDoc.DataSource = value;
            ddl_GcDoc.DataBind();
            ddl_GcDoc.Items.Insert(0, new ListItem("---Select One---","0"));
        }
    }
    #endregion    

    #region IView
    public bool validateUI()
    {
      bool _isValid = false;
      if (GcDocId<=0)
      {
          errorMessage = "Please Select Complaint Nature";
      }
      else if (FromTicketId <= 0)
      {
          errorMessage = "Please Select From Ticket";
      }
      else if (ToTicketId <= 0)
      {
          errorMessage = "Please Select To Ticket";
      }
      else { _isValid = true; }

      //UpdatePanel2.Update();
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
           // return Raj.CRM.Common.Null2Int(Request.QueryString["Id"]);
            return -1;
        }
    }

    #endregion

 #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        objMergeTicketsPresenter = new MergeTicketsPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            lbl_GcDocCaption.Text = CompanyManager.getCompanyParam().GcCaption + " No. :";
        }
    }

    protected void btn_MargeTicket_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            objMergeTicketsPresenter.Save();
            ddl_GcDoc_SelectedIndexChanged(sender,e);
        }
    }
    #endregion
    protected void ddl_GcDoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        objMergeTicketsPresenter.FillFromTicket();
        ddl_ToTicket.Items.Clear();
        UpdatePanel3.Update();
    }
    protected void ddl_FromTicket_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (FromTicketId > 0)
        {
            objMergeTicketsPresenter.FillToTicket();
        }
        else { ddl_ToTicket.Items.Clear();}
    }
}
 














 


   
 
