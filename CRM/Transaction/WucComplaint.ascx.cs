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
using Raj.EC;

/// <summary>
/// Author : Sunil
/// Date Created : 4/16/2008
/// Description : This page is For Add And Edit Complaint Transactions, 
/// Change Hostory :
/// Changed By    Date(DD/MM/YYYY)      Description
/// ================================================
/// </summary>

public partial class CRM_WucComplaint : System.Web.UI.UserControl, IComplaintView
{
    #region ClassVariables
     ComplaintPresenter objComplaintPresenter;
     Common objCommon = new Common();
    #endregion

    #region ControlsValues

    public string TicketNo
    { 
      set {txt_TicketNo.Text=value;} 
      get {return txt_TicketNo.Text;} 
    }
    public DateTime TicketDate
    {
        set { lbl_TicketDate.Text = value.ToString("dd/MM/yyyy"); }
    }
    public string Name
    {
        set { txt_Name.Text = value; }
        get { return txt_Name.Text; }
    }
    public string TelephoneNo
    {
        set { txt_TelephoneNo.Text = value; }
        get { return txt_TelephoneNo.Text; }
    }
    public string MobileNo
    {
        set { txt_MobileNo.Text = value; }
        get { return txt_MobileNo.Text; }
    } 
    public string Designation
    {
        set { txt_Designation.Text = value; }
        get { return txt_Designation.Text; }
    }
    public string EMailID
    {
        set { txt_EMailID.Text = value; }
        get { return txt_EMailID.Text; }
    }
    public int PriorityId
    {
        set { rdbl_Priority.SelectedValue = value.ToString(); }
        get { return Util.String2Int(rdbl_Priority.SelectedValue); }
    }  
    public string UndeliveredReason
    {
        set { txt_UndeliveredReason.Text = value; }
        get { return txt_UndeliveredReason.Text; }
    }
    public string ComplaintDetails
    {
        set { txt_ComplaintDetails.Text = value; }
        get { return txt_ComplaintDetails.Text; }
    }
    public int CNeeNorID
    {
        set { ddl_CNeeNor.SelectedValue = value.ToString(); }
        get { return Util.String2Int(ddl_CNeeNor.SelectedValue); }
    }
    public int ComplaintNatureId
    {
        set { ddl_NatureComplaint.SelectedValue = value.ToString(); }
        get { return Util.String2Int(ddl_NatureComplaint.SelectedValue); }
    }
    public void SetDocGcId(string value,string text)
    {
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_DocGcNo);
    }

    public bool IsQuery
    {
        set
        {
            rdbl_ComplaintType.Items[0].Selected = (!value);
            rdbl_ComplaintType.Items[1].Selected = value;
        }
        get
        {
            if (rdbl_ComplaintType.SelectedValue == "0")
                return false;
            else
                return true;
        }
    }
    public int SetAttachmentCount 
    {
        set 
        {
            //btn_Attachments.Text ="Attachments(" + value.ToString() + ")";
            btn_Attachments.Text = "Attachments";
        }
    }
    public int DocGcId
    {
        get{return Util.String2Int(ddl_DocGcNo.SelectedValue);}
    }
    public int DocGcNo 
    {        
        get {return Util.String2Int(ddl_DocGcNo.SelectedText.Trim());}
    }

    public DataTable SetLables 
    {
         set 
         {
             if (value.Rows.Count > 0)
             {

                 DataRow Dr = value.Rows[0];

                 lbl_BookingStation.Text = Dr["From_Branch_Name"].ToString();
                 lbl_CurrentBranch.Text = Dr["Current_Branch_Name"].ToString();
                 lbl_DocGcDate.Text = Convert.ToDateTime(Dr["Date"]).ToString("dd/MM/yyy");
                 lbl_CommtdDlyDate.Text = Convert.ToDateTime(Dr["Committed_Del_Date"]).ToString("dd/MM/yyy");
                 lbl_DeliveryStation.Text = Dr["Delivery_Branch_Name"].ToString();
                 lbl_CurrentStatus.Text = Dr["Status"].ToString();

                 lbl_Pkgs.Text = Dr["Articles"].ToString();
                 lbl_Weight.Text = Dr["Charged_Weight"].ToString();

                 lbl_BookingType.Text = Dr["Booking_Type"].ToString();
                 lbl_DeliveryType.Text = Dr["Delivery_Type"].ToString();
                 lbl_PaymentType.Text = Dr["Payment_Type"].ToString();

                 lbl_DeliveryDate.Text = Util.String2Int(Dr["Status_Id"].ToString()) == 200 ? Convert.ToDateTime(Dr["Document_Date"]).ToString("dd/MM/yyy") : "Not Deliverd";
                 lbl_IsDACC.Text = Convert.ToBoolean(Dr["Is_DACC"]) == true ? "Yes" : "No";
                 lbl_IsDOD.Text =Util.String2Decimal(Dr["DOD_Amount"].ToString())>0 ? "Yes" : "No";


                 txt_NeeName.Text = Dr["ConsigneeAdd"].ToString();
                 txt_NorName.Text = Dr["ConsignorAdd"].ToString();

                 if(Util.String2Int(Dr["TicketCount"].ToString())>0)
                 {
                     btn_TicketInfo.Visible = true;
                 }
                 else
                 {
                     btn_TicketInfo.Visible = false;
                 }

                 lbtn_ViewTrackTrace.Text = DocGcNo.ToString();
             }
             else 
             {
                 lbl_BookingStation.Text ="";
                 lbl_CurrentBranch.Text = "";
                 lbl_DocGcDate.Text = "";
                 lbl_CommtdDlyDate.Text = "";
                 lbl_DeliveryStation.Text = "";
                 lbl_CurrentStatus.Text = "";

                 lbl_Pkgs.Text = "";
                 lbl_Weight.Text = "";

                 lbl_BookingType.Text = "";
                 lbl_DeliveryType.Text = "";
                 lbl_PaymentType.Text = "";

                 lbl_DeliveryDate.Text = "";
                 lbl_IsDACC.Text = "";
                 lbl_IsDOD.Text = "";

                 txt_NeeName.Text = "";
                 txt_NorName.Text = "";
                 lbtn_ViewTrackTrace.Text = "";
             }
         }
     }
    public DataTable bind_ddl_DocGcNo
    {
        set
        {
            ddl_DocGcNo.DataTextField = "No";
            ddl_DocGcNo.DataValueField = "Id";
            ddl_DocGcNo.DataBind();
        }
    }
    public DataTable bind_ddl_CNeeNor
    {
        set
        {
            ddl_CNeeNor.DataTextField = "";
            ddl_CNeeNor.DataValueField = "";
            ddl_CNeeNor.DataSource = value;
            ddl_CNeeNor.DataBind();
        }
    }
    public DataTable bind_ddl_NatureOfComplaint
    {
        set
        {
            ddl_NatureComplaint.DataTextField = "Complaint_Nature";
            ddl_NatureComplaint.DataValueField = "Complaint_Nature_ID";
            ddl_NatureComplaint.DataSource = value;
            ddl_NatureComplaint.DataBind();
            ddl_NatureComplaint.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable bind_rdbl_Priority
    {
        set
        {
            rdbl_Priority.DataTextField = "Priority";
            rdbl_Priority.DataValueField = "Priority_ID";
            rdbl_Priority.DataSource = value;
            rdbl_Priority.DataBind();
        }
    }
    #endregion       

    #region IView
    public bool validateUI()
    {
        TextBox txt_gc = (TextBox)ddl_DocGcNo.FindControl("txtBoxddl_DocGcNo");
      bool _isValid = false;

      if(ComplaintNatureId <= 0)
      {
          errorMessage = "Please Select Complaint Nature";
          ddl_NatureComplaint.Focus();
      }
      else if (Name.Trim() == string.Empty)
      {
          errorMessage = "Please Enter Name";
          txt_Name.Focus();
      }
      //else if (!(EMailID.Trim() != "" || EMailID.Trim().IndexOf("/^'\'w+((-'\'w+)|('\'.'\'w+))*'\'@[A-Za-z0-9]+(('\'.|-)[A-Za-z0-9]+)*'\'.[A-Za-z0-9]+$/") == -1))
      //{
      //    errorMessage = "Please Enter Valid Email ID ";
      //}
      else if(ComplaintDetails.Trim() == string.Empty)
      {
          errorMessage = "Please Enter Complaint Details";
          txt_ComplaintDetails.Focus();
      }
      else if (Util.String2Int(ddl_DocGcNo.SelectedValue) <= 0 && rdbl_ComplaintType.SelectedValue == "0")
      {
          errorMessage = "Please Select " + CompanyManager.getCompanyParam().GcCaption + " No.";
          txt_gc.Focus();
      }
      else
      { 
          _isValid = true;
      }

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

    public string btnAttachmentsCaption
    {
        set
        {
            btn_Attachments.Text = value;
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
       
    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Save.Style.Add("value", "Save");
        btn_Save.Style.Add("disabled", "false");

        if (!IsPostBack)
        {
            ddl_DocGcNo.DataTextField = "No";
            ddl_DocGcNo.DataValueField = "Id";

            lbl_PkgsCaption.Text = "Packages :";
            lbl_BookingTypeCaption.Text = "Booking Type :";
            lbl_DeliveryTypeCaption.Text = "Delivery Type :";
            lbl_PaymentTypeCaption.Text = "Payment Type :";
            
            if (keyID <= 0)
            {
                TicketDate = DateTime.Now;
                rdbl_Priority.SelectedIndex = 0;
                btn_TicketInfo.Visible = false;
            }

            TicketNo = "AUTO GENERATION";
            StateManager.SaveState("Attachments", null);
            btn_Attachments.Attributes.Add("onclick", " return OpenWindow('" + objCommon.getBaseURL() + "/CRM/Transaction/FrmAttachments.aspx?Id=" + Util.EncryptInteger(keyID) + "&Type=" + Util.EncryptString("Complaint") + "')");
        }

        objComplaintPresenter = new ComplaintPresenter(this, IsPostBack);    

        lbtn_ViewTrackTrace.Attributes.Add("onclick", "return Show_Ticket_History('" + objCommon.getBaseURL() + "/Reports/Operations_VT/Tracking_and_Tracing/frm_Tracking_Tracing_New.aspx?value=" + DocGcNo.ToString() + "&index=0')");
        
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.CRM.CallBack));
        btn_TicketInfo.Attributes.Add("onclick", " return Show_Ticket_History('" + objCommon.getBaseURL() + "/CRM/Transaction/FrmDisplayInfo.aspx?Id=" + Util.EncryptString(DocGcNo.ToString()) + "&Type="+Util.EncryptString("TicketInfo")+"')");

        SetStandardCaption();
    }

    private void SetStandardCaption()
    {
        lbl_GcDocNoCaption.Text = CompanyManager.getCompanyParam().GcCaption + " No. :";
        lbl_DocGcNo.Text = CompanyManager.getCompanyParam().GcCaption + " No. :";
        lbl_DocGcDateLable.Text = CompanyManager.getCompanyParam().GcCaption + " Date :";
    }

    protected void ddl_DocGcNo_SelectedIndexChanged(object sender, EventArgs e)
    {
         objComplaintPresenter.SetLablesOnGcDocChanged();
         UpdatePanel1.Update();
         UpdatePanel3.Update();
    }
        
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        //if(validateUI())
         objComplaintPresenter.Save();
    }
    #endregion
   
}
 














 


   
 
