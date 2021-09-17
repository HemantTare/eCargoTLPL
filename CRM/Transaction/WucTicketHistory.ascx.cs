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

public partial class CRM_Transaction_WucTicketHistory : System.Web.UI.UserControl, ITicketHistoryView
{
    #region ClassVariables
    TicketHistoryPresenter objTicketHistoryPresenter;
    #endregion
    
    #region ControlsValues
 
    public string Reply
    {
        set { txt_Reply.Text = value; }
        get { return txt_Reply.Text; }
    }

    public string HeaderLable
    {
        set { lbl_Heading.Text = value; }
    }

    public DataTable bind_rpt_History
    {
        set
        {
            rpt_History.DataSource = value;
            rpt_History.DataBind();
        }
    }

    public int SetAttachmentCount
    {
        set
        {
           //btn_Attachments.Text = "Attachments(" + value.ToString() + ")";
            btn_Attachments.Text = "Attachments";
        }
    }      

    public string CompliantNature
    {
        set
        {
            lbl_ComplaintNature.Text =value;
        }
    }
    #endregion



    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (Reply.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Reply";
            txt_Reply.Focus();
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
           //return 37;
        }
    }
    public string Type
    {
      get {
            //return "Assigned";
            return Util.DecryptToString(Request.QueryString["Type"]); 
          }
    }

    #endregion


    #region OtherProperties

    private bool VisibleReply
    { 
    set{
        tr_Reply.Visible = value;
        tr_ReplyButton.Visible = value;
       }
    }


    #endregion


    #region OtherMethods
    private void DefaultSettings()
    {
        if (!IsPostBack)
        {

            if (Type == "Assigned" || Type == "In Progress")
            {
                btn_ComplaintAnalysis.Visible = false;
            }
            else if (Type == "Closed Tickets")
            {
                btn_ComplaintAnalysis.Visible = false;
                btn_CloseTicket.Text = "View Resolution";
                btn_AssignUsers.Visible = false;
                VisibleReply = false;
            }
            else if (Type == "Archived")
            {
                btn_ComplaintAnalysis.Text = "View Analysis";
                btn_CloseTicket.Text = "View Resolution";
                btn_AssignUsers.Visible = false;
                VisibleReply = false;
            }
            else if (Type == "Pending For Action" || Type == "Action Taken By Me" || Type == "Action Taken By Others")
            {
                btn_ComplaintAnalysis.Visible = false;
                btn_CloseTicket.Visible = false;
                btn_AssignUsers.Visible = false;
            }
            else if (Type == "Other")
            {
                btn_ComplaintAnalysis.Visible = false;
                btn_CloseTicket.Visible = false;
                btn_AssignUsers.Visible = false;
                VisibleReply = false;
            }


            StateManager.SaveState("Attachments", null);
            btn_Attachments.Attributes.Add("onclick", " return OpenWindow('" + Util.GetBaseURL() + "/CRM/Transaction/FrmAttachments.aspx?Id=" + Util.EncryptInteger(keyID) + "&Type=" + Util.EncryptString(Type)+ "')");
        }
    }

    #endregion
    

    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
       DefaultSettings();
       objTicketHistoryPresenter = new TicketHistoryPresenter(this, IsPostBack);

       System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
       sbValid.Append("if (typeof(Page_ClientValidate) == 'function'){");
       sbValid.Append("if (ValidtateUI() == false) { return false; }}");
       sbValid.Append("this.value = 'Wait...';");
       sbValid.Append("this.disabled = true;");
       sbValid.Append(Page.ClientScript.GetPostBackEventReference(btn_Send, ""));
       sbValid.Append(";");
       btn_Send.Attributes.Add("OnClick", sbValid.ToString());
    }
    
    protected void btn_Send_Click(object sender, EventArgs e)
    {
        if (validateUI())
            objTicketHistoryPresenter.Save();
    }

    protected void btn_AssignUsers_Click(object sender, EventArgs e)
    {
        Response.Redirect("frm_Complaint_Assignment.aspx?Ticket_Id=" +Util.EncryptInteger(keyID));
    }
    protected void btn_CloseTicket_Click(object sender, EventArgs e)
    {
         Response.Redirect("FrmTicketResolution.aspx?Ticket_Id=" + Util.EncryptInteger(keyID));
    }
    protected void btn_ComplaintAnalysis_Click(object sender, EventArgs e)
    {
        Response.Redirect("frm_Complaint_Analysis.aspx?Ticket_Id=" + Util.EncryptInteger(keyID));
    }
    #endregion

    
}
