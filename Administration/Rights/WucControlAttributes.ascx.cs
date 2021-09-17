using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;

/// <summary>
/// Create: Ankit champaneriya
/// Date  : 03/12/08
/// </summary>
/// 

public partial class Administration_Rights_WucControlAttributes : System.Web.UI.UserControl
{
    #region Declaration

    private DataSet objDS = new DataSet();
    private DAL objDAL = new DAL();

    TextBox txt_ControlId;
    CheckBox chk_IsVisible;
    CheckBox chk_IsMandatory;
    Label lbl_ControlId;

    bool isValid = false;

    #endregion

    #region Property

    public int MenuSystemID
    {
        get { return Util.String2Int(ddl_MenuSystem.SelectedValue); }
        set { ddl_MenuSystem.SelectedValue = Util.Int2String(value); }
    }

    public int MenuHeadID
    {
        get { return Util.String2Int(ddl_MenuHead.SelectedValue); }
        set { ddl_MenuHead.SelectedValue = Util.Int2String(value); }
    }

    public int MenuGroupID
    {
        get { return Util.String2Int(ddl_MenuGroup.SelectedValue); }
        set { ddl_MenuGroup.SelectedValue = Util.Int2String(value); }
    }

    public int MenuItemID
    {
        get { return Util.String2Int(ddl_MenuItem.SelectedValue); }
        set { ddl_MenuItem.SelectedValue = Util.Int2String(value); }
    }

    #endregion

    #region IView
    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); ; }
    }

    #endregion

    #region ControlBind

    public DataTable Bind_ddl_MenuSystem
    {
        set
        {
            ddl_MenuSystem.DataTextField = "System_Name";
            ddl_MenuSystem.DataValueField = "System_ID";
            ddl_MenuSystem.DataSource = value;
            ddl_MenuSystem.DataBind();
            ddl_MenuSystem.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable Bind_ddl_MenuHead
    {
        set
        {
            ddl_MenuHead.DataTextField = "Menu_Head_Name";
            ddl_MenuHead.DataValueField = "Menu_Head_Id";
            ddl_MenuHead.DataSource = value;
            ddl_MenuHead.DataBind();
            ddl_MenuHead.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable Bind_ddl_MenuGroup
    {
        set
        {
            ddl_MenuGroup.DataTextField = "MenuGroup_Name";
            ddl_MenuGroup.DataValueField = "MenuGroup_ID";
            ddl_MenuGroup.DataSource = value;
            ddl_MenuGroup.DataBind();
            ddl_MenuGroup.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable Bind_ddl_MenuItem
    {
        set
        {
            ddl_MenuItem.DataTextField = "MenuItem_Name";
            ddl_MenuItem.DataValueField = "MenuItem_ID";
            ddl_MenuItem.DataSource = value;
            ddl_MenuItem.DataBind();
            ddl_MenuItem.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable Bind_dg_Rights
    {
        set
        {
            if (value.Rows.Count == 0)
            { dg_Rights.ShowFooter = false; }
            else { dg_Rights.ShowFooter = true; }

            dg_Rights.DataSource = value;
            dg_Rights.DataBind();


            lbl_Errors.Text = "";
        }
    }

    #endregion

    #region otherMethods

    private void Get_All_DropDown()
    {
        Bind_ddl_MenuSystem = FillValues(0);
        Bind_ddl_MenuHead = FillValues(1);
        Bind_ddl_MenuGroup = FillValues(2);
        Bind_ddl_MenuItem = FillValues(3);
        Bind_dg_Rights = FillValues(4);
    }

    public DataSet SessionBind()
    {
        SqlParameter[] param ={    objDAL.MakeInParams("@Menusytem_id",SqlDbType.Int , 0, MenuSystemID), 
                                   objDAL.MakeInParams("@MenuHead_ID",SqlDbType.Int,0,MenuHeadID  ),
                                   objDAL.MakeInParams("@Menugroup_ID",SqlDbType.Int ,0,MenuGroupID  ),
                                   objDAL.MakeInParams("@Menuitem_ID",SqlDbType.Int,0,MenuItemID  ),
                                   objDAL.MakeInParams("@Flag",SqlDbType.Int,0,4), 
                                    objDAL.MakeInParams("@rightsxml",SqlDbType.Xml,0,"" )
                                  };

        objDAL.RunProc("[dbo].[Com_ADM_FillControlsFormenuItem]", param, ref objDS);

        return objDS;
    }

    public DataSet SessionRightsGrid
    {
        get { return StateManager.GetState<DataSet>("SessionBind"); }
        set { StateManager.SaveState("SessionBind", value); }
    }

    private void BindRightsDatagrid()
    {
        dg_Rights.DataSource = SessionRightsGrid;
        dg_Rights.DataBind();
    }


    public DataTable FillValues(int Flag)
    {
        SqlParameter[] param ={    objDAL.MakeInParams("@Menusytem_id",SqlDbType.Int , 0, MenuSystemID), 
                                   objDAL.MakeInParams("@MenuHead_ID",SqlDbType.Int,0,MenuHeadID  ),
                                   objDAL.MakeInParams("@Menugroup_ID",SqlDbType.Int ,0,MenuGroupID  ),
                                   objDAL.MakeInParams("@Menuitem_ID",SqlDbType.Int,0,MenuItemID  ),
                                   objDAL.MakeInParams("@Flag",SqlDbType.Int,0,Flag), 
                                    objDAL.MakeInParams("@rightsxml",SqlDbType.Xml,0,"" )
                                  };

        objDAL.RunProc("[dbo].[Com_ADM_FillControlsFormenuItem]", param, ref objDS);

        return objDS.Tables[0];
    }

    #endregion

    #region PageLoad

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Get_All_DropDown();
        }
    }

    #endregion

    #region EventClick

    protected void ddl_MenuSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_ddl_MenuHead = FillValues(1);
        Bind_ddl_MenuGroup = FillValues(2);
        Bind_ddl_MenuItem = FillValues(3);
        Bind_dg_Rights = FillValues(4);
    }

    protected void ddl_MenuHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_ddl_MenuGroup = FillValues(2);
        Bind_ddl_MenuItem = FillValues(3);
        Bind_dg_Rights = FillValues(4);
    }

    protected void ddl_MenuGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_ddl_MenuItem = FillValues(3);
        Bind_dg_Rights = FillValues(4);
    }

    protected void ddl_MenuItem_SelectedIndexChanged(object sender, EventArgs e)
    {

        SessionRightsGrid = SessionBind();
        Bind_dg_Rights = FillValues(4);
        dg_Rights.ShowFooter = true;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        save();
    }

    private void save()
    {

        for (int i = 0; i <= dg_Rights.Items.Count - 1; i++)
        {
            chk_IsVisible = (CheckBox)dg_Rights.Items[i].FindControl("chk_IsVisible");
            chk_IsMandatory = (CheckBox)dg_Rights.Items[i].FindControl("chk_IsMandatory");
            lbl_ControlId = (Label)dg_Rights.Items[i].FindControl("lbl_ControlId");

            SessionRightsGrid.Tables[0].Rows[i]["Control_Id"] = lbl_ControlId.Text;
            SessionRightsGrid.Tables[0].Rows[i]["Is_visible"] = chk_IsVisible.Checked;
            SessionRightsGrid.Tables[0].Rows[i]["Is_Mandatory"] = chk_IsMandatory.Checked;
        }

        DataSet objmenurights = new DataSet();
        DataTable objdtmenurights = SessionRightsGrid.Tables[0].Copy();
        objmenurights.Tables.Add(objdtmenurights);

        string menurights = objmenurights.GetXml().ToLower().ToString();

        SqlParameter[] param ={    objDAL.MakeInParams("@Menusytem_id",SqlDbType.Int , 0, MenuSystemID), 
                                   objDAL.MakeInParams("@MenuHead_ID",SqlDbType.Int,0,MenuHeadID  ),
                                   objDAL.MakeInParams("@Menugroup_ID",SqlDbType.Int ,0,MenuGroupID  ),
                                   objDAL.MakeInParams("@Menuitem_ID",SqlDbType.Int,0,MenuItemID  ),
                                   objDAL.MakeInParams("@Flag",SqlDbType.Int,0,5), 
                                objDAL.MakeInParams("@rightsxml",SqlDbType.Xml,0,menurights )};

        objDAL.RunProc("[dbo].[Com_ADM_FillControlsFormenuItem]", param);
        lbl_Errors.Text = "Saved Successfully";
    }  //ds for datagrid rights 

    #endregion

    #region GridControlEvents

    protected void dg_Rights_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            lbl_ControlId = (Label)(e.Item.FindControl("lbl_ControlId"));
            txt_ControlId = (TextBox)(e.Item.FindControl("txt_ControlId"));
            chk_IsVisible = (CheckBox)(e.Item.FindControl("chk_IsVisible"));
            chk_IsMandatory = (CheckBox)(e.Item.FindControl("chk_IsMandatory"));

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataSet DS = SessionRightsGrid;
                DataRow DR = null;
                DR = DS.Tables[0].Rows[e.Item.ItemIndex];

            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                DataSet DS = SessionRightsGrid;
                DataRow DR = DS.Tables[0].Rows[e.Item.ItemIndex];

                txt_ControlId.Text = DR["Control_ID"].ToString();
                chk_IsVisible.Checked = Convert.ToBoolean(DR["Is_visible"]);
                chk_IsMandatory.Checked = Convert.ToBoolean(DR["Is_Mandatory"]);
            }
        }
        //  dg_Rights.ShowFooter = true;
    }

    protected void dg_Rights_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                BindRightsDatagrid();
                dg_Rights.EditItemIndex = -1;
                dg_Rights.ShowFooter = true;
            }
        }
    }

    protected void dg_Rights_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Insert_Update_Dataset(source, e);

        if (isValid == true)
        {
            dg_Rights.EditItemIndex = -1;
            dg_Rights.ShowFooter = true;

            BindRightsDatagrid();
        }
    }

    protected void dg_Rights_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Rights.EditItemIndex = e.Item.ItemIndex;
        dg_Rights.ShowFooter = false;
        BindRightsDatagrid();
    }

    protected void dg_Rights_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        DataSet DS = SessionRightsGrid;
        DataRow DR = DS.Tables[0].Rows[e.Item.ItemIndex];
        DR.Delete();
        DS.Tables[0].AcceptChanges();
        SessionRightsGrid = DS;
        BindRightsDatagrid();
    }

    protected void dg_Rights_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Rights.EditItemIndex = -1;
        dg_Rights.ShowFooter = true;
        BindRightsDatagrid();
    }

    private bool Allow_To_Add_Update()
    {
        if (txt_ControlId.Text == string.Empty)
        {
            errorMessage = "Please Enter Control Id";
            scm_ControlAttributes.SetFocus(txt_ControlId);
        }
        else
            isValid = true;

        return isValid;
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        DataSet DS = SessionRightsGrid;
        DataRow DR = null;

        txt_ControlId = (TextBox)(e.Item.FindControl("txt_ControlId"));
        chk_IsVisible = (CheckBox)(e.Item.FindControl("chk_IsVisible"));
        chk_IsMandatory = (CheckBox)(e.Item.FindControl("chk_IsMandatory"));

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

            DR["Control_ID"] = txt_ControlId.Text;
            DR["Is_visible"] = chk_IsVisible.Checked;
            DR["Is_Mandatory"] = chk_IsMandatory.Checked;

            if (e.CommandName == "Add") { DS.Tables[0].Rows.Add(DR); }
            SessionRightsGrid = DS;
        }
    }

    #endregion

}
