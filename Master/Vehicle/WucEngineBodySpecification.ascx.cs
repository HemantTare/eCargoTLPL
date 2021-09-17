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

using Raj.EF.MasterPresenter;
using Raj.EF.MasterView;
using Raj.EF;
using Raj.EC;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 24/04/2008
/// Description   : This Page is For Master Vehicle Engine Body Specification
/// </summary>
/// 
public partial class Master_General_WucEngineBodySpecification : System.Web.UI.UserControl,IEngineBodySpecificationView 
{
    #region ClassVariables
    EngineBodySpecificationPresenter objEngineBodySpecificationPresenter;
    private ScriptManager scm_EngionBody;
    TextBox txt_Description;
    TextBox txt_ValueQuantity;
    DataTable objDT;
    DataRow dr = null;
    bool isValid = false;
    PageControls pc = new PageControls();

    #endregion

    #region Iview Implementation
    

    public string ChasisNo
    {
        set {txt_Chasis_No.Text = value;}
        get {return txt_Chasis_No.Text; }
    }
    public string TrollyChasisNo
    {
        set { txt_TrollyChasisNo.Text = value; }
        get { return txt_TrollyChasisNo.Text; }
    }
    public string EngineNo
    {
        set { txt_Engine_No.Text = value; }
        get { return txt_Engine_No.Text; }
    }
    public int FuelTypeID
    {
        set { ddl_Fuel_Type.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Fuel_Type.SelectedValue); }
    }
    public string Power
    {
        set { txt_Power.Text = value; }
        get { return txt_Power.Text; }
    }
    public int GrossVehicleWt
    {
        set { txt_Gross_Veh_Wt.Text = Util.Int2String(value); }
        get { return Util.String2Int(txt_Gross_Veh_Wt.Text); }
    }
    public int UnladenWt
    {
        set { txt_Unladen_Wt.Text = Util.Int2String(value); }
        get { return Util.String2Int(txt_Unladen_Wt.Text); }
    }
    public int VehicleCapacity
    {
        set 
        { 
            hdn_Vehicle_Capacity.Value  = Util.Int2String(value);
            lbl_Vehicle_Capacity.Text = Util.Int2String(value); 
        }
        get { return Util.String2Int(hdn_Vehicle_Capacity.Value); }
    }
    
    public decimal  WheelBase
    {
        set { txt_Wheel_Base.Text =Util.Decimal2String( value); }
        get { return Util.String2Decimal( txt_Wheel_Base.Text); }
    }
    public decimal Length
    {
        set { txt_Length.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_Length.Text); }
    }
    public decimal Height
    {
        set { txt_Height.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_Height.Text); }
    }
    public decimal Width
    {
        set { txt_Width.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_Width.Text); }
    }
    public decimal FuelTankCapacity 
    {
        set { txt_FuelTankCapacity.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_FuelTankCapacity.Text); }
    }
   
    public string PaintCode
    {
        set { txt_Paint_Code.Text = value; }
        get { return txt_Paint_Code.Text; }
    }
    public string PaintColor
    {
        set { txt_Paint_color.Text = value; }
        get { return txt_Paint_color.Text; }
    }
    public string IgnitionKeyCode
    {
        set { txt_Ignition_Code.Text = value; }
        get { return txt_Ignition_Code.Text; }
    }
    public string DoorKeyCode
    {
        set { txt_Door_Code.Text = value; }
        get { return txt_Door_Code.Text; }
    }
    public int VehicleCategoryId
    {
        get { return Util.String2Int(StateManager.GetState<string>("QueryString")); }
    }

    public DataSet BindFuelType
    {
        set
        {
            ddl_Fuel_Type.DataTextField = "Fuel_Type";
            ddl_Fuel_Type.DataValueField = "Fuel_Type_ID";
            ddl_Fuel_Type.DataSource = value;
            ddl_Fuel_Type.DataBind();
            ddl_Fuel_Type.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable BindEngineBodyGrid
    {
        set
        {
            SessionEngineBodyGrid = value;
            dg_Specification.DataSource = value;
            dg_Specification.DataBind();
        }
    }
    public DataTable SessionEngineBodyGrid
    {
        get { return StateManager.GetState<DataTable>("EngineBodyGrid"); }
        set { StateManager.SaveState("EngineBodyGrid", value); }
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
           // return 128;
        }
    }
    #endregion

    #region Other Properties
    public ScriptManager SetScriptManager
    {
        set { scm_EngionBody = value; }
    }
    public String VehicleSpecificationDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionEngineBodyGrid.Copy());
            _objDs.Tables[0].TableName = "Vehicle_Specification_Details";
            return _objDs.GetXml().ToLower();
        }
    }
    #endregion

    #region Validation
    public bool validateUI()
    {
        bool _isValid = false;
      
        if (txt_Chasis_No.Text.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_Chasis_No) == true)
        {
            errorMessage = "Please Enter Chasis No";
            scm_EngionBody.SetFocus(txt_Chasis_No);
        }      
        else if (txt_Engine_No.Text.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_Engine_No) == true)
        {
            errorMessage = "Please Enter Engine No";
            scm_EngionBody.SetFocus(txt_Engine_No);
        }
        else if (FuelTypeID <= 0 && pc.Control_Is_Mandatory(ddl_Fuel_Type) == true)
        {
            errorMessage = "Please Select Fuel Type";
            scm_EngionBody.SetFocus(ddl_Fuel_Type);
        }
        else if (Util.String2Int(txt_Gross_Veh_Wt.Text) < 0 && pc.Control_Is_Mandatory(txt_Gross_Veh_Wt) == true)
        {
            errorMessage = "Please Enter Gross Vehicle Weight";
            scm_EngionBody.SetFocus(txt_Gross_Veh_Wt);
        }
        else if (Util.String2Int(txt_Gross_Veh_Wt.Text) <= 0 && pc.Control_Is_Mandatory(txt_Gross_Veh_Wt) == true)
        {
            errorMessage = "Please Enter Vehicle Weight Greater Than Zero";
            scm_EngionBody.SetFocus(txt_Gross_Veh_Wt);
        }
        else if (Util.String2Int(txt_Unladen_Wt.Text) < 0 && pc.Control_Is_Mandatory(txt_Unladen_Wt) == true)
        {
            errorMessage = "Please Enter Unladen Weight";
            scm_EngionBody.SetFocus(txt_Unladen_Wt);
        }
        else if (Util.String2Int(txt_Unladen_Wt.Text) <= 0 && pc.Control_Is_Mandatory(txt_Unladen_Wt) == true)
        {
            errorMessage = "Please Enter Unladen Weight Greater Than Zero";
            scm_EngionBody.SetFocus(txt_Unladen_Wt);
        }
        else
        {
            _isValid = true ;
        }
        return _isValid;
    }
    #endregion

    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        objEngineBodySpecificationPresenter = new EngineBodySpecificationPresenter(this, IsPostBack);
    }

    #region Grid Events
    protected void dg_Specification_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        txt_Description = (TextBox)(e.Item.FindControl("txt_Description"));
        txt_ValueQuantity = (TextBox)(e.Item.FindControl("txt_Value"));

        if (e.Item.ItemType == ListItemType.EditItem)
        {
            objDT = SessionEngineBodyGrid;
            
            LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));
            lbtn_Delete.Enabled = false;

            txt_Description.Text = objDT.Rows[e.Item.ItemIndex]["Description"].ToString();
            txt_ValueQuantity.Text = objDT.Rows[e.Item.ItemIndex]["ValueQuantity"].ToString();
        }
    }

    protected void dg_Specification_ItemCommand(object source, DataGridCommandEventArgs e)
    {

        if (e.CommandName == "Add")
        {
            objDT = SessionEngineBodyGrid;
            try
            {
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[2];

                _dtColumnPrimaryKey[0] = objDT.Columns["Description"];
                _dtColumnPrimaryKey[1] = objDT.Columns["ValueQuantity"];

                objDT.PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                BindEngineBodyGrid = SessionEngineBodyGrid;
                dg_Specification.EditItemIndex = -1;
                dg_Specification.ShowFooter = true;
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Text = "Dulpicate Description and Quantity";
            scm_EngionBody.SetFocus(txt_Description);
            return;
        }
        }
      
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        txt_Description = (TextBox)(e.Item.FindControl("txt_Description"));
        txt_ValueQuantity = (TextBox)(e.Item.FindControl("txt_Value"));

        objDT = SessionEngineBodyGrid;

        if (e.CommandName == "Add")
        {
            dr = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            dr = objDT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            dr["Description"] = txt_Description.Text;
            dr["ValueQuantity"] = txt_ValueQuantity.Text;

            if (e.CommandName == "Add") { objDT.Rows.Add(dr); }
            SessionEngineBodyGrid = objDT;
        }
    }

    private bool Allow_To_Add_Update()
    {
        lbl_Errors.Text = "";
        if (txt_Description.Text == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Description";
            scm_EngionBody.SetFocus(txt_Description);
        }
        else if (txt_ValueQuantity.Text == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Value";
            scm_EngionBody.SetFocus(txt_ValueQuantity);
        }
        else
            isValid = true;

        return isValid;
    }

    protected void dg_Specification_UpdateCommand(object source, DataGridCommandEventArgs e)
    {

        try
        {
            objDT = SessionEngineBodyGrid;
            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[2];

            _dtColumnPrimaryKey[0] = objDT.Columns["Description"];
            _dtColumnPrimaryKey[1] = objDT.Columns["ValueQuantity"];

            objDT.PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset(source, e);

            if (isValid == true)
            {
                dg_Specification.EditItemIndex = -1;
                dg_Specification.ShowFooter = true;
                BindEngineBodyGrid = SessionEngineBodyGrid;
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Text = "Dulpicate Description and Value/Quantity";
            scm_EngionBody.SetFocus(txt_Description);
            return;
        }
    }

    protected void dg_Specification_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Specification.EditItemIndex = e.Item.ItemIndex;
        dg_Specification.ShowFooter = false;
        BindEngineBodyGrid = SessionEngineBodyGrid;
    }

    protected void dg_Specification_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Specification.EditItemIndex = -1;
        dg_Specification.ShowFooter = true;
        BindEngineBodyGrid = SessionEngineBodyGrid;
    }

    protected void dg_Specification_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionEngineBodyGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionEngineBodyGrid = objDT;
            dg_Specification.EditItemIndex = -1;
            dg_Specification.ShowFooter = true;
            BindEngineBodyGrid = SessionEngineBodyGrid;
        }
    }
    #endregion
    #endregion
    protected void dg_Specification_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
