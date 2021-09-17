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

using Raj.EC;

using Raj.EC.ControlsPresenter;
using Raj.EC.ControlsView;

public partial class CommonControls_WucAttachments : System.Web.UI.UserControl,IAttachmentsView
{
    #region ClassVariables
    AttachmentsPresenter objAttachmentsPresenter;
    private int AttachmentFormId1;
    private ScriptManager scm_Attachments;
    TextBox txt_Description;
    LinkButton lbtn_Description;
    FileUpload fileUpload_Drawing;
    bool isValid = false;
    #endregion

    #region iview implementation
    public DataSet SessionAttachmentsGrid
    {
        get { return StateManager.GetState<DataSet>("Attachments"); }
        set { StateManager.SaveState("Attachments", value); }
    }

    public int AttachmentFormId
    {
        set { AttachmentFormId1 = value; }
        get { return Common.GetMenuItemId(); }
    }

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); ; }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    #endregion

    #region Otherproperties

    public ScriptManager SetScriptManager
    {
        set { scm_Attachments = value; }
    }

    public String AttachmentsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs = (SessionAttachmentsGrid.Copy());
            _objDs.Tables[0].TableName = "Attachments";
            return _objDs.GetXml().ToLower();
        }
    }
    #endregion


    #region Validation
    public bool validateUI()
    {
        return true;
    }
    #endregion

    private void BindAttachmentsDatagrid()
    {
        Set_Sr_No();
        dg_AttachmentsGrid.DataSource = SessionAttachmentsGrid;
        dg_AttachmentsGrid.DataBind();
    }

    private void Set_Sr_No()
    {
        int Sr_No;
        DataSet DS = SessionAttachmentsGrid;
        DataRow DR = null;
        for (Sr_No = 0; Sr_No <= DS.Tables[0].Rows.Count - 1; Sr_No++)
        {
            DR = DS.Tables[0].Rows[Sr_No];
            DR["Sr_No"] = Sr_No + 1;
        }
        SessionAttachmentsGrid = DS;
    }




    #region events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            objAttachmentsPresenter = new AttachmentsPresenter(this, IsPostBack);
            BindAttachmentsDatagrid();

            Common objCommon = new Common();
            hdn_baseURL.Value = Util.GetBaseURL() + "/" + System.Configuration.ConfigurationManager.AppSettings.Get("AttchmentFolder") + "/";
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

                txt_Description.Text = DR["Description"].ToString();
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
            
            DR["Description"] = txt_Description.Text;
            DR["Attachment_Path"] = DateTime.Now.Ticks.ToString() + '^' + fileUpload_Drawing.FileName;

            string AttachmentPath;
            AttachmentPath = Server.MapPath(Request.ApplicationPath + "/" + AppConfig.GetParameter("AttchmentFolder") + "/" + DR["Attachment_Path"]);

            fileUpload_Drawing.SaveAs(AttachmentPath);

            if (e.CommandName == "Add") { DS.Tables[0].Rows.Add(DR); }
            SessionAttachmentsGrid = DS;
        }
    }

    private bool Allow_To_Add_Update()
    {
        if (txt_Description.Text == string.Empty)
        {
            errorMessage = "Please Enter Title";
            txt_Description.Focus();
        }
        else if (fileUpload_Drawing.HasFile == false)
        {
            errorMessage = "Please Select File";
        }
        else
            isValid = true;

        return isValid;
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


