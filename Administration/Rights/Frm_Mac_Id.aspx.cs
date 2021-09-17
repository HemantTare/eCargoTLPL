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
using Raj.EC.AdminPresenter;
using Raj.EC.AdminView;
using Raj.EC;

public partial class Administration_Rights_Frm_Mac_Id : ClassLibraryMVP.UI.Page, IMacIDView
{
    DataTable objDT;
    bool isValid = false;
    TextBox txt_Computer_Name;
    TextBox txt_Mac_Id;
    MacIDPresenter objMacIDPresenter;

    public int keyID
    {
        get
        {
            return 0;
        }
    }

    public string Hierarchy_Code
    {
        get { return WucHierarchyWithID1.HierarchyCode.ToString(); }
        set { WucHierarchyWithID1.HierarchyCode = value.ToString(); }
    }

    public int Main_ID
    {
        get { return WucHierarchyWithID1.MainId; }
        set { WucHierarchyWithID1.MainId = value; }
    }

    public DataTable Bind_Mac_Id
    {
        set
        {
            SessionMacIdGrid = value;
            dg_Mac_Id.DataSource = value;
            dg_Mac_Id.DataBind();
            dg_Mac_Id.Enabled=true;
            if (Hierarchy_Code == string.Empty || Hierarchy_Code =="0")
            {
                dg_Mac_Id.Enabled = false;
            }
            else if(Hierarchy_Code != "HO" && Main_ID == 0)
            {
                dg_Mac_Id.Enabled = false;
            }
        }
        get { return (DataTable)SessionMacIdGrid; }
    }

    public DataTable SessionMacIdGrid
    {
        get { return StateManager.GetState<DataTable>("dtMac_Id"); }
        set { StateManager.SaveState("dtMac_Id", value); }
    }

    public String Mac_ID_XML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionMacIdGrid.Copy());
            // Please do not convert this to lower as it might change Mac Id
            return _objDs.GetXml();
        }
    }
    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        WucHierarchyWithID1.DDLHierarchyChange += new EventHandler(HierarchyChangIndexChange);
        WucHierarchyWithID1.setDDLLocationAutoPostBack = true;
        WucHierarchyWithID1.DDLLocationChange += new EventHandler(LocationChangIndexChange);

        objMacIDPresenter = new MacIDPresenter(this, IsPostBack);

    }

    private void HierarchyChangIndexChange(object sender, EventArgs e)
    { 
        objMacIDPresenter.initValues(); 
    }
    private void LocationChangIndexChange(object sender, EventArgs e)
    {
        objMacIDPresenter.initValues();
    }

    protected void dg_Mac_Id_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Mac_Id.EditItemIndex = -1;
        dg_Mac_Id.ShowFooter = true;
        Bind_Mac_Id = SessionMacIdGrid;
    }

    protected void dg_Mac_Id_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionMacIdGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionMacIdGrid = objDT;
            dg_Mac_Id.EditItemIndex = -1;
            dg_Mac_Id.ShowFooter = true;
            Bind_Mac_Id = SessionMacIdGrid;
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        objDT = SessionMacIdGrid;
        DataRow DR = null;
        txt_Computer_Name = (TextBox)(e.Item.FindControl("txt_Computer_Name"));
        txt_Mac_Id = (TextBox)(e.Item.FindControl("txt_Mac_Id"));

        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = objDT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["Computer_Name"] = txt_Computer_Name.Text;
            DR["Mac_Id"] = txt_Mac_Id.Text;

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            SessionMacIdGrid = objDT;
        }
    }

    private bool Allow_To_Add_Update()
    {
        if (txt_Computer_Name.Text == string.Empty)
        {
            errorMessage = "Please Enter Computer Name";
            scm_Profile.SetFocus(txt_Computer_Name);
        }
        else if (txt_Mac_Id.Text == string.Empty)
        {
            errorMessage = "Please Enter Mac Id";
            scm_Profile.SetFocus(txt_Mac_Id);
        }
        else
            isValid = true;

        return isValid;
    }

    protected void dg_Mac_Id_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            objDT = SessionMacIdGrid;
            try
            {
                //DataColumn[] _dtColumnPrimaryKey;
                //_dtColumnPrimaryKey = new DataColumn[1];
                //_dtColumnPrimaryKey[0] = objDT.Columns["MAc_Id"];
                //objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    Bind_Mac_Id = SessionMacIdGrid;
                    dg_Mac_Id.EditItemIndex = -1;
                    dg_Mac_Id.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                lbl_Errors.Text = "Duplicate Mac ID";
                scm_Profile.SetFocus(txt_Mac_Id);
                return;
            }
        }
    }

    protected void dg_Mac_Id_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDT = SessionMacIdGrid;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[1];
            _dtColumnPrimaryKey[0] = objDT.Columns["MAc_Id"];
            objDT.PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                dg_Mac_Id.EditItemIndex = -1;
                dg_Mac_Id.ShowFooter = true;
                Bind_Mac_Id = SessionMacIdGrid;
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Visible = true;
            lbl_Errors.Text = "Duplicate Mac Id";
            scm_Profile.SetFocus(txt_Mac_Id);
            return;
        }
    }

    protected void dg_Mac_Id_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                txt_Computer_Name = (TextBox)(e.Item.FindControl("txt_Computer_Name"));
                txt_Mac_Id = (TextBox)(e.Item.FindControl("txt_Mac_Id"));
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionMacIdGrid;
                DataRow DR = objDT.Rows[e.Item.ItemIndex];

                txt_Computer_Name.Text = DR["Computer_Name"].ToString();
                txt_Mac_Id.Text = DR["Mac_Id"].ToString();
            }
        }
    }

    protected void dg_Mac_Id_EditCommand(object source, DataGridCommandEventArgs e)
    {
        LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));
        lbtn_Delete.Enabled = false;
        dg_Mac_Id.EditItemIndex = e.Item.ItemIndex;
        dg_Mac_Id.ShowFooter = false;
        Bind_Mac_Id = SessionMacIdGrid;
    }

    public bool validateUI()
    {
        isValid = true;
        return isValid;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objMacIDPresenter.Save();
    }
}
