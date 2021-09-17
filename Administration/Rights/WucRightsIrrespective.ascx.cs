using System; 
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

public partial class Administration_Rights_WucRightsIrrespective : System.Web.UI.UserControl
{
    #region Declaration
    private DataSet objDS = new DataSet();

    private DAL objDAL = new DAL();

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

    private void save()
    {
        CheckBox chk_CanAdd;
        CheckBox chk_CanEdit;
        CheckBox chk_CanCancel;
        CheckBox chk_CanRead;

        for (int i = 0; i <= dg_Rights.Items.Count - 1; i++)
        {
            chk_CanAdd = (CheckBox)dg_Rights.Items[i].FindControl("chk_CanAdd");
            chk_CanEdit = (CheckBox)dg_Rights.Items[i].FindControl("chk_CanEdit");
            chk_CanCancel = (CheckBox)dg_Rights.Items[i].FindControl("chk_CanCancel");
            chk_CanRead = (CheckBox)dg_Rights.Items[i].FindControl("chk_CanRead");

            SessionMenuItemRights.Rows[i]["Can_Add"] = chk_CanAdd.Checked;
            SessionMenuItemRights.Rows[i]["Can_Edit"] = chk_CanEdit.Checked;
            SessionMenuItemRights.Rows[i]["Can_Cancel"] = chk_CanCancel.Checked;
            SessionMenuItemRights.Rows[i]["Can_Read"] = chk_CanRead.Checked;
        }

        DataSet objmenurights = new DataSet();
        DataTable objdtmenurights = SessionMenuItemRights.Copy();
        objmenurights.Tables.Add(objdtmenurights);

        string menurights = objmenurights.GetXml().ToLower().ToString();

        SqlParameter[] param ={    objDAL.MakeInParams("@Menusytem_id",SqlDbType.Int , 0, MenuSystemID), 
                                   objDAL.MakeInParams("@MenuHead_ID",SqlDbType.Int,0,MenuHeadID  ),
                                   objDAL.MakeInParams("@Menugroup_ID",SqlDbType.Int ,0,MenuGroupID  ),
                                   objDAL.MakeInParams("@Menuitem_ID",SqlDbType.Int,0,MenuItemID  ),
                                   objDAL.MakeInParams("@Flag",SqlDbType.Int,0,5), 
                                objDAL.MakeInParams("@rightsxml",SqlDbType.Xml,0,menurights )};

        objDAL.RunProc("[dbo].[Com_ADM_FillDataForRights_Irrespective]", param);
        lbl_Errors.Text = "Saved Successfully";
    }  //ds for datagrid rights 

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Get_All_DropDown();
        }

        if (UserManager.getUserParam().HierarchyCode != "AD")
            btn_Save.Enabled = false;
    }



    public DataTable  FillValues(int Flag)
    {
        SqlParameter[] param ={    objDAL.MakeInParams("@Menusytem_id",SqlDbType.Int , 0, MenuSystemID), 
                                   objDAL.MakeInParams("@MenuHead_ID",SqlDbType.Int,0,MenuHeadID  ),
                                   objDAL.MakeInParams("@Menugroup_ID",SqlDbType.Int ,0,MenuGroupID  ),
                                   objDAL.MakeInParams("@Menuitem_ID",SqlDbType.Int,0,MenuItemID  ),
                                   objDAL.MakeInParams("@Flag",SqlDbType.Int,0,Flag), 
                                    objDAL.MakeInParams("@rightsxml",SqlDbType.Xml,0,"" )
                                  };

        objDAL.RunProc("[dbo].[Com_ADM_FillDataForRights_Irrespective]", param, ref objDS);

        return objDS.Tables[0];
    }


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

            SessionMenuItemRights = value;

            lbl_Errors.Text = "";
        }
    }

    private DataTable SessionMenuItemRights
    {
        get { return StateManager.GetState<DataTable>("MenuItemRights"); }
        set{StateManager.SaveState("MenuItemRights", value);}
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

    #endregion

    #region EventChange

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
        Bind_dg_Rights = FillValues(4);
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        save();
    }

    #endregion

   
    protected void dg_Rights_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.Footer)
        {
            e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER';");
            e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUT';");
        }

        if (e.Item.ItemType == ListItemType.AlternatingItem)
        {
            e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER';");
            e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTALT';");
        }
    }
  
}
