using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

/// <summary>
/// Author        : Ankit Champaneriya
/// Created On    : 06/10/2008
/// Modified      : 06/12/08
/// Description   : This is the Form  For Master Profile Rights  without MVP
/// </summary>
/// 

public partial class Admin_Rights_WucProfileRights : System.Web.UI.UserControl  
{
    #region ClassVariables
    string ProfileRights = "";

    #endregion

    #region ControlsValues

    public string HierarchyID
    {
        get { return ddl_Hierarchy.SelectedValue; }
        set { ddl_Hierarchy.SelectedValue = value; }
    }

    public int ProfileID
    {
        get { return Util.String2Int(ddl_Profile.SelectedValue); }
        set { ddl_Profile.SelectedValue = Util.Int2String(value); }
    }

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

    public bool IsApplicableToAll
    {
        get { return chk_IsApplicableToAll.Checked; }
        set { chk_IsApplicableToAll.Checked = value; }
    }


       #endregion

    #region ControlsBind

    public DataTable Bind_ddl_Hierarchy
    {
        set
        {
            ddl_Hierarchy.DataTextField = "Hierarchy_Name";
            ddl_Hierarchy.DataValueField = "Hierarchy_Code";
            ddl_Hierarchy.DataSource = value;
            ddl_Hierarchy.DataBind();
            ddl_Hierarchy.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_ddl_Profile
    {
        set
        {
            ddl_Profile.DataTextField = "Profile_Name";
            ddl_Profile.DataValueField = "Profile_Id";
            ddl_Profile.DataSource = value;
            ddl_Profile.DataBind();
            ddl_Profile.Items.Insert(0, new ListItem("Select One", "0"));

        }
    }
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
    public DataTable Bind_dg_ProfileRights
    {
        set
        {
            if (value.Rows.Count == 0)
            { dg_ProfileRights.ShowFooter = false; }
            else { dg_ProfileRights.ShowFooter = true; }

            SessionProfileRights = value;

            dg_ProfileRights.DataSource = value;
            dg_ProfileRights.DataBind();
            errorMessage = "";
        }
    }

    private DataTable SessionProfileRights
    {
        get { return StateManager.GetState<DataTable>("ProfileRights"); }
        set { StateManager.SaveState("ProfileRights", value); }
    }

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = true;
        if (ddl_Hierarchy.SelectedValue == "0")
        {
            errorMessage = GetLocalResourceObject("Msg_HierarchyName").ToString();// "Please Select Hierarchy Name";
            ddl_Hierarchy.Focus();
            _isValid = false;
        }
        if (ddl_Profile.SelectedValue == "0")
        {
            errorMessage = GetLocalResourceObject("Msg_ProfileName").ToString(); //"Please Select Profile Name";
            ddl_Profile.Focus();
            _isValid = false;
        }
        if (ddl_MenuSystem.SelectedValue == "0")
        {
            errorMessage = GetLocalResourceObject("Msg_SystemName").ToString(); //"Please Select Menu System Name";
            ddl_MenuSystem.Focus();
            _isValid = false;
        }
        if (ddl_MenuHead.SelectedValue == "0")
        {
            errorMessage = GetLocalResourceObject("Msg_HeadName").ToString();// "Please Select Menu Head Name";
            ddl_MenuHead.Focus();
            _isValid = false;
        }
        if (ddl_MenuGroup.SelectedValue == "0")
        {
            errorMessage = GetLocalResourceObject("Msg_GroupName").ToString(); // "Please Select Menu Group Name";
            ddl_MenuGroup.Focus();
            _isValid = false;
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
        }
    }
    #endregion

    #region OtherProperties

    #endregion


    #region OtherMethods

    private void Get_All_DropDown()
    {
        Bind_ddl_Hierarchy = GetAndSetValues(1);
        Bind_ddl_Profile = GetAndSetValues(2);
        Bind_ddl_MenuSystem = GetAndSetValues(3);
        Bind_ddl_MenuHead = GetAndSetValues(4);
        Bind_ddl_MenuGroup = GetAndSetValues(5);
        Bind_dg_ProfileRights = GetAndSetValues(6);
    }

    public DataTable GetAndSetValues(int Flag)
    {
        DataSet objDS = new DataSet();
        DAL objDAL = new DAL();

        SqlParameter[] param ={ 
                                            objDAL.MakeOutParams("@Error_Code",SqlDbType.Int,5), 
                                objDAL.MakeOutParams("@Error_Desc",SqlDbType.VarChar,4000), 
            objDAL.MakeInParams("@Login_hierarchy_Code",SqlDbType.VarChar, 5,UserManager.getUserParam().HierarchyCode), 
                                   objDAL.MakeInParams("@hierarchy_Code",SqlDbType.VarChar, 5,HierarchyID ), 
                                   objDAL.MakeInParams("@Profile_Id",SqlDbType.Int ,0,ProfileID ), 
                                   objDAL.MakeInParams("@Menusytem_id",SqlDbType.Int , 0,MenuSystemID), 
                                   objDAL.MakeInParams("@MenuHead_ID",SqlDbType.Int,0,MenuHeadID ),
                                   objDAL.MakeInParams("@Menugroup_ID",SqlDbType.Int ,0,MenuGroupID ),
                                   objDAL.MakeInParams("@Applicable_To_All_Users",SqlDbType.Bit  ,0,IsApplicableToAll),
                                   objDAL.MakeInParams("@Flag",SqlDbType.Int,0,Flag), 
                                   objDAL.MakeInParams("@rightsxml",SqlDbType.Xml,0,ProfileRights  )
                                  };

        objDAL.RunProc("[dbo].[Com_ADM_ProfileRights]", param, ref objDS);

        if (Convert.ToInt32(param[0].Value) == 0)
            errorMessage = "Saved Successfully";
        else
            errorMessage = param[1].Value.ToString();

        if (Flag != 7)
            return objDS.Tables[0];
        else
            return null;
    }

    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Get_All_DropDown();
            Common objCommon = new Common();

            hdf_ResourceString.Value = objCommon.GetResourceString("Administration/Rights/App_LocalResources/WucProfileRights.ascx.resx");
            objCommon.CheckFormRights(btn_Save);

            if (UserManager.getUserParam().HierarchyCode == "AD")
                dg_ProfileRights.Columns[0].Visible = true;
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        CheckBox chk_CanAdd;
        CheckBox chk_CanEdit;
        CheckBox chk_CanDelete;
        CheckBox chk_CanRead;

        for (int i = 0; i <= dg_ProfileRights.Items.Count - 1; i++)
        {
            chk_CanAdd = (CheckBox)dg_ProfileRights.Items[i].FindControl("chk_CanAdd");
            chk_CanEdit = (CheckBox)dg_ProfileRights.Items[i].FindControl("chk_CanEdit");
            chk_CanDelete = (CheckBox)dg_ProfileRights.Items[i].FindControl("chk_CanDelete");
            chk_CanRead = (CheckBox)dg_ProfileRights.Items[i].FindControl("chk_CanRead");

            SessionProfileRights.Rows[i]["Can_Add"] = chk_CanAdd.Checked;
            SessionProfileRights.Rows[i]["Can_Edit"] = chk_CanEdit.Checked;
            SessionProfileRights.Rows[i]["Can_Delete"] = chk_CanDelete.Checked;
            SessionProfileRights.Rows[i]["Can_Read"] = chk_CanRead.Checked;
        }

        DataSet objprofilerights = new DataSet();
        DataTable objdtprofilerights = SessionProfileRights.Copy();
        objprofilerights.Tables.Add(objdtprofilerights);

        ProfileRights = objprofilerights.GetXml().ToLower().ToString();

        GetAndSetValues(7);
    }

    protected void ddl_Hierarchy_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_ddl_Profile = GetAndSetValues(2);
        Bind_dg_ProfileRights = GetAndSetValues(6);
    }

    protected void ddl_Profile_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_dg_ProfileRights = GetAndSetValues(6);
    }

    protected void ddl_MenuSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_ddl_MenuHead = GetAndSetValues(4);
        Bind_ddl_MenuGroup = GetAndSetValues(5);
        Bind_dg_ProfileRights = GetAndSetValues(6);
    }

    protected void ddl_MenuHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_ddl_MenuGroup = GetAndSetValues(5);
        Bind_dg_ProfileRights = GetAndSetValues(6);
    }

    protected void ddl_MenuGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_dg_ProfileRights = GetAndSetValues(6);
    }

    protected void dg_ProfileRights_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER';");
            e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUT';");
        }

        if (e.Item.ItemType == ListItemType.AlternatingItem)
        {
            e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER';");
            e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTALT';");
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER';");
            e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUT';");
        }
    }

    #endregion

}
