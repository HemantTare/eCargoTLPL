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
using Raj.CRM.TransactionsModel;
using System.IO;

public partial class CommonControls_WucAttachments : System.Web.UI.UserControl
{
    #region ClassVariables
    AttachmentsModel objAttachmentsModel;
    TextBox txt_Description;
    LinkButton lbtn_Description;
    FileUpload fileUpload_Drawing;
    bool isValid = false;

    public int SrNo = 1;
    #endregion

    #region iview implementation
    public DataSet SessionAttachmentsGrid
    {
        get { return StateManager.GetState<DataSet>("Attachments"); }
        set { StateManager.SaveState("Attachments", value); }
    }


    public int KeyId
    {
        get { 
               return Util.DecryptToInt(Request.QueryString["Id"]); 
            
            }
    }

    public string Type
    {
        get {
              return Util.DecryptToString(Request.QueryString["Type"]);
            }
    }

    public string ErrorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    #endregion

    #region Otherproperties
    private bool EnableGrid
    {
        set 
        {
            dg_AttachmentsGrid.ShowFooter = value;
            dg_AttachmentsGrid.Columns[3].Visible = value;
            dg_AttachmentsGrid.Columns[4].Visible = value;
        }
    }
    
    #endregion


    #region Validation
    public bool ValidateUI()
    {
        return true;
    }
    #endregion
    private void DefaultSettings()
    {
        if (!IsPostBack)
        {
            if (Type == "Other" || Type == "Closed Tickets" || Type == "Archived")
            {
                EnableGrid = false;
            }
        }
    }

    private void BindAttachmentsDatagrid()
    {
       // Set_Sr_No();
        dg_AttachmentsGrid.DataSource = SessionAttachmentsGrid;
        dg_AttachmentsGrid.DataBind();
    }

    #region events

    protected void Page_Load(object sender, EventArgs e)
    {
        //Param.ValidateSession();
        if (IsPostBack == false)
        {
            objAttachmentsModel = new AttachmentsModel();
            if (StateManager.IsValidSession("Attachments") == false)
            {
                SessionAttachmentsGrid = objAttachmentsModel.ReadValues(KeyId, (int)UserManager.getUserParam().UserId);
            }

            BindAttachmentsDatagrid();
            hdn_baseURL.Value = Util.GetBaseURL() + "/CRM/Attachments/";

            DefaultSettings();
        }
    }

    #region GridControlsEvents
    protected void dg_AttachmentsGrid_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_AttachmentsGrid.EditItemIndex = -1;
        dg_AttachmentsGrid.ShowFooter = true;
        BindAttachmentsDatagrid();
    }

    protected void dg_AttachmentsGrid_EditCommand(object source, DataGridCommandEventArgs e)
    {
        //LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));
        //lbtn_Delete.Enabled = false;
        dg_AttachmentsGrid.EditItemIndex = e.Item.ItemIndex;
        dg_AttachmentsGrid.ShowFooter = false;
        BindAttachmentsDatagrid();
    }

    protected void dg_AttachmentsGrid_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                BindAttachmentsDatagrid();
                dg_AttachmentsGrid.EditItemIndex = -1;
                dg_AttachmentsGrid.ShowFooter = true;
            }
        }
    }

    protected void dg_AttachmentsGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            txt_Description = (TextBox)(e.Item.FindControl("txt_Description"));
            fileUpload_Drawing = (FileUpload)(e.Item.FindControl("fileUpload_Drawing"));

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataSet DS = SessionAttachmentsGrid;
                DataRow DR = null;
                DR = DS.Tables[0].Rows[e.Item.ItemIndex];
                lbtn_Description = (LinkButton)(e.Item.FindControl("lbtn_Description"));
                lbtn_Description.Attributes.Add("onclick", "return LoadAttachmentForm('" + DR["Attachment_Path"] + "')");
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                DataSet DS = SessionAttachmentsGrid;
                DataRow DR = DS.Tables[0].Rows[e.Item.ItemIndex];

                txt_Description.Text = DR["Title"].ToString();
            }
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        DataSet DS = SessionAttachmentsGrid;
        DataRow DR = null;

        txt_Description = (TextBox)(e.Item.FindControl("txt_Description"));
        fileUpload_Drawing = (FileUpload)(e.Item.FindControl("fileUpload_Drawing"));

        if (e.CommandName == "Add")
        {
            DR = DS.Tables[0].NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = DS.Tables[0].Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {

            DR["Title"] = txt_Description.Text;
            DR["Attachment_Path"] = DateTime.Now.Ticks.ToString() + '^' + fileUpload_Drawing.FileName;

            string AttachmentPath;
            AttachmentPath = Server.MapPath(Request.ApplicationPath + "/CRM/Attachments/" + DR["Attachment_Path"]);

            fileUpload_Drawing.SaveAs(AttachmentPath);

            if (e.CommandName == "Add") { DS.Tables[0].Rows.Add(DR); }
            SessionAttachmentsGrid = DS;
        }
    }

    private bool Allow_To_Add_Update()
    {

        if (txt_Description.Text == string.Empty)
        {
            ErrorMessage = "Please Enter Title";
            txt_Description.Focus();
        }
        else if (fileUpload_Drawing.HasFile == false)
        {
            ErrorMessage = "Please Select File";
        }
        else if (fileUpload_Drawing.FileBytes.Length > 1 * Math.Pow(10,6))
        {
            ErrorMessage = "Size Of Attachment Should Not More Than 1MB";
        }
        else if (validateFiles() == false)
        {
            ErrorMessage = "Invalid Attachment File";
        }
        else
            isValid = true;

        return isValid;
    }

    private bool validateFiles()
    {
        string fileName = fileUpload_Drawing.FileName;

        string[] splited = fileName.Split(new char[] { '.' });

        if (splited.Length == 0)
        { return false; }

        string standardExention = "txt,doc,xls,jpg,jif,docx,xlsx,bmp,jpeg,png,rar,zip,pps,ppt";

        string ext = splited[splited.Length - 1].Trim();

        string[] splittedStandardExention = standardExention.Split(new char[] { ',' });

        for (int i = 0; i < splittedStandardExention.Length; i++)
        {
            if (ext == splittedStandardExention[i].Trim())
            {
                return true;
            }
        }

        return false;
    }

    protected void dg_AttachmentsGrid_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Insert_Update_Dataset(source, e);

        if (isValid == true)
        {
            dg_AttachmentsGrid.EditItemIndex = -1;
            dg_AttachmentsGrid.ShowFooter = true;

            BindAttachmentsDatagrid();
        }
    }

    protected void dg_AttachmentsGrid_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        DataSet DS = SessionAttachmentsGrid;
        DataRow DR = DS.Tables[0].Rows[e.Item.ItemIndex];

        DR.Delete();
        DS.Tables[0].AcceptChanges();
        SessionAttachmentsGrid = DS;
        BindAttachmentsDatagrid();
    }

    
    #endregion
    #endregion
}


