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
using ClassLibraryMVP.General;
using Raj.EF.MasterView;
using Raj.EF.MasterPresenter;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 30/04/2008
/// Description   : This Page is For Service Master
/// </summary>
/// 


public partial class Master_Work_Order_WucService : System.Web.UI.UserControl,IServiceView 
{
    #region ClassVariables
    ServicePresenter objIServicePresenter;

    DropDownList ddl_ServiceTask;
    DataTable objDT;
    bool isValid = false;

    #endregion

    #region ControlsValues
    public string ServiceName
    {
        set { txt_ServiceName.Text = value; }
        get { return txt_ServiceName.Text; }
    }
    public int ServiceCategoryID
    {
        set { ddl_ServiceCategory .SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_ServiceCategory.SelectedValue); }
    }
    public string ServiceDescription
    {
        set { txt_Description.Text = value; }
        get { return txt_Description.Text; }
    }
    public int ParentServiceID
    {
        set { ddl_ParentService.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_ParentService.SelectedValue); }
    }
    public decimal EstCheckingTime
    {
        set { txt_ChekingTime.Text = Util.Decimal2String (value); }
        get { return Util.String2Decimal (txt_ChekingTime.Text); }
    }
    public decimal EstRepairTime
    {
        set { txt_RepairTime.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_RepairTime.Text); }
    }




    public String ServiceTaskDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionServiceTaskDetailsGrid.Copy());

            return _objDs.GetXml().ToLower();
        }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false ;

        if (txt_ServiceName.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Service Name";
          scm_Service.SetFocus(txt_ServiceName);
        }
        else if (Convert.ToInt32 (ddl_ServiceCategory.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select Service Category";
            scm_Service.SetFocus(ddl_ServiceCategory);
        }
        //else if (txt_ChekingTime.Text == string.Empty)
        //{
        //    lbl_Errors.Text  = "please Enter Estimated Checking Time";
        //    scm_Service.SetFocus(txt_ChekingTime);
        //}
        //else if (Util.String2Decimal(txt_ChekingTime.Text) == 0)
        //{
        //    lbl_Errors.Text  = "Estimated Checking Time Must be Greater Than Zero";
        //    scm_Service.SetFocus(txt_ChekingTime);
        //}
        //else if (Validate_Time(txt_ChekingTime) == false)
        //{
        //    txt_ChekingTime.Text = Util.Int2String(0);
        //    scm_Service.SetFocus(txt_ChekingTime);
        //}
        //else if (txt_RepairTime.Text == string.Empty)
        //{
        //    lbl_Errors.Text  = "Please Enter Estimated Repair Time";
        //    scm_Service.SetFocus(txt_RepairTime);
        //}
        //else if (Util.String2Decimal(txt_RepairTime.Text) == 0)
        //{
        //    lbl_Errors.Text  = "Estimated Repair Time Must be Greater Than Zero";
        //    scm_Service.SetFocus(txt_RepairTime);
        //}

        //else if (Validate_Time(txt_RepairTime) == false)
        //{
        //    txt_RepairTime.Text = Util.Int2String(0);
        //    scm_Service.SetFocus(txt_RepairTime);
        //}
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]); 
           // return 0;
        }
    }

    #endregion

    #region OtherMethods

    private Boolean Validate_Time(TextBox txt)
    {
        Boolean Chk_Time = true ;
       if (txt.Text != "")
        {
            string[] arr= txt.Text.Split(new char[] {'.'});
            if (arr.Length > 1)
                {
                        if(arr[1].Length > 1)
                        {
                            if(Convert.ToInt32(arr[1]) > 59)
                            {
                                lbl_Errors.Text="Please enter minutes less than 60";
                                Chk_Time = false;
                            }
                            else
                            {
                                Chk_Time = true;
                            }
                        }
                        else       
                        {      
                            Chk_Time= true;
                        }
                }

        }
        return Chk_Time;
    }

    #endregion

    #region ControlsBind

    public DataTable BindServiceCategory
    {
        set
        {
            ddl_ServiceCategory.DataSource = value;
            ddl_ServiceCategory.DataTextField = "Service_Category";
            ddl_ServiceCategory.DataValueField = "Service_Category_ID";
            ddl_ServiceCategory.DataBind();
            ddl_ServiceCategory.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindParentService
    {
        set
        {
            ddl_ParentService.DataSource = value;
            ddl_ParentService.DataTextField = "Service_Name";
            ddl_ParentService.DataValueField = "Service_ID";
            ddl_ParentService.DataBind();
            ddl_ParentService.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    #endregion

    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        objIServicePresenter = new ServicePresenter(this, IsPostBack);

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objIServicePresenter.Save();
    }



    public DataTable BindServiceTask
    {
        set
        {
            ddl_ServiceTask.DataTextField = "ServiceTask_Name";
            ddl_ServiceTask.DataValueField = "ServiceTask_ID";
            ddl_ServiceTask.DataSource = value;
            ddl_ServiceTask.DataBind();
            ddl_ServiceTask.Items.Insert(0, new ListItem("--Select Task --", "0"));
        }
    }

    public DataTable SessionServiceTaskDropdown
    {
        get { return StateManager.GetState<DataTable>("ServiceTask"); }
        set { StateManager.SaveState("ServiceTask", value); }
    }

    public DataTable BindServiceTaskDetailsGrid
    {
        set
        {
            SessionServiceTaskDetailsGrid = value;
            Set_Sr_No();
            dg_ServiceTask.DataSource = value;
            dg_ServiceTask.DataBind();
        }
    }

    public DataTable SessionServiceTaskDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("ServiceTaskDetails"); }
        set { StateManager.SaveState("ServiceTaskDetails", value); }
    }

    private void Set_Sr_No()
    {
        int Sr_No;
        objDT = SessionServiceTaskDetailsGrid;
        DataRow DR = null;
        for (Sr_No = 0; Sr_No <= objDT.Rows.Count - 1; Sr_No++)
        {
            DR = objDT.Rows[Sr_No];
            DR["Sr_No"] = Sr_No + 1;
        }
        SessionServiceTaskDetailsGrid = objDT;
    }


    protected void dg_ServiceTask_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_ServiceTask.EditItemIndex = -1;
        dg_ServiceTask.ShowFooter = true;
        BindServiceTaskDetailsGrid  = SessionServiceTaskDetailsGrid;
    }

    protected void dg_ServiceTask_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionServiceTaskDetailsGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionServiceTaskDetailsGrid = objDT;
            dg_ServiceTask.EditItemIndex = -1;
            dg_ServiceTask.ShowFooter = true;
            BindServiceTaskDetailsGrid = SessionServiceTaskDetailsGrid;
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        objDT = SessionServiceTaskDetailsGrid;
        DataRow DR = null;
        ddl_ServiceTask = (DropDownList)(e.Item.FindControl("ddl_ServiceTask"));

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
            DR["ServiceTask_ID"] = ddl_ServiceTask.SelectedValue;
            DR["ServiceTask_Name"] = ddl_ServiceTask.SelectedItem.Text;

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            SessionServiceTaskDetailsGrid = objDT;
        }
    }

    private bool Allow_To_Add_Update()
    {
        if (Convert.ToInt32(ddl_ServiceTask.SelectedValue) == 0)
        {
            lbl_Errors.Text  = "Select Service Task";
            scm_Service.SetFocus(dg_ServiceTask);
        }
        else
            isValid = true;

        return isValid;
    }

    protected void dg_ServiceTask_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            objDT = SessionServiceTaskDetailsGrid;
            try
            {
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDT.Columns["ServiceTask_ID"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindServiceTaskDetailsGrid  = SessionServiceTaskDetailsGrid;
                    dg_ServiceTask.EditItemIndex = -1;
                    dg_ServiceTask.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                lbl_Errors.Text = "Duplicate Service Task Name";
                scm_Service.SetFocus(dg_ServiceTask);
                return;
            }
        }
    }

    protected void dg_ServiceTask_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDT = SessionServiceTaskDetailsGrid;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[1];
            _dtColumnPrimaryKey[0] = objDT.Columns["ServiceTask_ID"];
            objDT.PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                dg_ServiceTask.EditItemIndex = -1;
                dg_ServiceTask.ShowFooter = true;
                BindServiceTaskDetailsGrid = SessionServiceTaskDetailsGrid; 
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Visible = true;
            lbl_Errors.Text = "Duplicate Service Task";
            scm_Service.SetFocus(dg_ServiceTask);
            return;
        }
    }

    protected void dg_ServiceTask_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_ServiceTask = (DropDownList)(e.Item.FindControl("ddl_ServiceTask"));
            }

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                BindServiceTask  = SessionServiceTaskDropdown;
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionServiceTaskDetailsGrid;
                DataRow DR = objDT.Rows[e.Item.ItemIndex];

                ddl_ServiceTask.SelectedValue = DR["ServiceTask_ID"].ToString();
            }
        }
    }

    protected void dg_ServiceTask_EditCommand(object source, DataGridCommandEventArgs e)
    {
        LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));
        lbtn_Delete.Enabled = false;
        dg_ServiceTask.EditItemIndex = e.Item.ItemIndex;
        dg_ServiceTask.ShowFooter = false;
        BindServiceTaskDetailsGrid  = SessionServiceTaskDetailsGrid;
    }


    #endregion
}
