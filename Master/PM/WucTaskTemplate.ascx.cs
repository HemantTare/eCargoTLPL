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
using System.Data.SqlClient;
using System.Text;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.Security;

using Raj.EF.MasterView;
using Raj.EF.MasterPresenter;
using Raj.EC.ControlsView;
using Raj.EF;


public partial class Master_PM_WucTaskTemplate : System.Web.UI.UserControl, ITaskTemplateView
{
    #region page variables
    TaskTemplatePresenter objTaskTemplatePresenter;
    #endregion

    #region ClassVariables

    TextBox txt_Custom_Grid_Odometer;
    ComponentArt.Web.UI.Calendar dtp_Custom_Grid_Date;
    bool isValid = false;
    DataSet objDS;

    #endregion

    #region ControlsValues

    public Boolean Is_Custom
    {
        set { Chk_Is_Custom.Checked = (value); }
        get { return (Chk_Is_Custom.Checked); }
    }

    public Boolean Is_Days_Selected
    {
        set
        {

            hdn_Is_Days_Selected.Value = Convert.ToString(value);

            rbl_Months.Checked = false;
            rbl_Is_Custome_Months.Checked = false;
            rbl_Days.Checked = true;
            rbl_Is_Custome_Days.Checked = true;

            if (!value)
            {
                if (rblst_Schedule_By.Items[2].Selected == true)
                {
                    rbl_Is_Custome_Months.Checked = true;
                    rbl_Is_Custome_Days.Checked = false;
                }
                else if (rblst_Schedule_By.Items[1].Selected == true)
                {
                    rbl_Months.Checked = true;
                    rbl_Days.Checked = false;
                }
            }
            else
            {

            }
        }
        get { return Util.String2Bool(hdn_Is_Days_Selected.Value); }
    }

    public int Template_ID
    {
        set { ddl_Task_Template.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Task_Template.SelectedValue); }
    }


    public int Vehicle_Id
    {
        set { hdn_Vehicle_Id.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Vehicle_Id.Value); }
    }

    public int Schedule_By
    {
        set
        {
            rblst_Schedule_By.Items[value - 1].Enabled = true;
            rblst_Schedule_By.Items[value - 1].Selected = true;

            if (value - 1 == 0)
            {
                Set_Day_Month_Visibility(false);
            }
            else
            {
                Set_Day_Month_Visibility(true);
            }
            hdn_Schedule_By.Value = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_Schedule_By.Value); }
    }

    public int Vehicle_Manufacturer_ID
    {
        set { ddl_VehicleManufacturer.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_VehicleManufacturer.SelectedValue); }
    }

    public int Vehicle_Model_ID
    {
        set { ddl_VehicleModel.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_VehicleModel.SelectedValue); }
    }

    public int Repair_Service_Category_ID
    {
        set { ddl_Repair_Service_Category.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Repair_Service_Category.SelectedValue); }
    }

    public int Repair_Service_ID
    {
        set { ddl_Repair_Service.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Repair_Service.SelectedValue); }
    }

    public int To_Be_Worked_At_ID
    {
        set { ddl_To_Be_Worked_At.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_To_Be_Worked_At.SelectedValue); }
    }


    public int CompanyWorkShop
    {
        set { rblWorkShop.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(rblWorkShop.SelectedValue); }
    }

    public int Task_Completion_ID
    {
        set { ddl_Completion.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Completion.SelectedValue); }
    }

    public int Task_Defination_ID
    {
        set { ddl_Completion.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Completion.SelectedValue); }
    }

    public int DueOn_Value
    {
        set
        {
            hdn_DueOn_Value.Value = Util.Int2String(value);
            //txt_Days_Months.Text = Util.Int2String(value);

            if (rblst_Schedule_By.Items[0].Selected == true && Chk_Is_Custom.Checked == false)
            {
                txt_Due_Every.Text = Util.Int2String(value);
            }
            txt_Kms.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_DueOn_Value.Value); }

    }

    public int DueOn_Days
    {
        set
        {
            hdn_DueOn_Days.Value = Util.Int2String(value);
            if (rblst_Schedule_By.Items[1].Selected == true && Chk_Is_Custom.Checked == false)
            {
                txt_Due_Every.Text = Util.Int2String(value);
            }

            txt_Days_Months.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_DueOn_Days.Value); }
    }

    public int Month_Value
    {
        set { hdn_Month_Value.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Month_Value.Value); }
    }

    public int Alert_Before_Value
    {
        set
        {
            hdn_Alert_Before_Value.Value = Util.Int2String(value);
            txt_Odometer_Time_Kms.Text = Util.Int2String(value);
            // txt_Alert_Before.Text = Util.Int2String(value);           


            if (rblst_Schedule_By.Items[0].Selected == true && Chk_Is_Custom.Checked == false)
            {
                txt_Alert_Before.Text = Util.Int2String(value);
            }
        }
        get { return Util.String2Int(hdn_Alert_Before_Value.Value); }
    }

    public int Alert_Before_Days
    {
        set
        {
            hdn_Alert_Before_Days.Value = Util.Int2String(value);
            txt_Odometer_Time_Days_Months.Text = Util.Int2String(value);

            if (rblst_Schedule_By.Items[1].Selected == true && Chk_Is_Custom.Checked == false)
            {
                txt_Alert_Before.Text = Util.Int2String(value);
            }


        }
        get { return Util.String2Int(hdn_Alert_Before_Days.Value); }
    }

    public int Vendor_Id
    {
        set
        {
            hdn_Vendor_Id.Value = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_Vendor_Id.Value); }
    }

    public int Branch_Id
    {
        set
        {
            hdn_Branch_Id.Value = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_Branch_Id.Value); }
    }

    public int CustomAlert_On_Value
    {
        set
        {
            txt_Custom_Alert_Before.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(txt_Custom_Alert_Before.Text); }
    }

    public int Last_Permormed_On
    {
        set { txt_Last_Permormed_On.Text = Util.Int2String(value); }
        get { return Util.String2Int(txt_Last_Permormed_On.Text); }
    }

    public void SetVendorId(string text, string value)
    {
        ddl_To_Be_Worked_At.DataValueField = "Id";
        ddl_To_Be_Worked_At.DataTextField = "Name";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_To_Be_Worked_At);
    }

    public void SetLocationId(string text, string value)
    {
        ddl_To_Be_Worked_At.DataValueField = "Id";
        ddl_To_Be_Worked_At.DataTextField = "Name";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_To_Be_Worked_At);
    }

    public Decimal Cost
    {
        set { txt_Cost.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_Cost.Text); }
    }

    public DateTime Last_Permormed_Date
    {
        set { dtp_Last_Permormed_Date.SelectedDate = value; }
        get { return dtp_Last_Permormed_Date.SelectedDate; }
    }

    #endregion

    #region ControlsBind

    public DataTable Bind_ddl_Task_Template
    {
        set
        {
            ddl_Task_Template.DataTextField = "Template_Name";
            ddl_Task_Template.DataValueField = "Template_ID";
            ddl_Task_Template.DataSource = value;
            ddl_Task_Template.DataBind();
            ddl_Task_Template.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable Bind_ddl_VehicleManufacturer
    {
        set
        {
            ddl_VehicleManufacturer.DataTextField = "Manufacturer";
            ddl_VehicleManufacturer.DataValueField = "Manufacturer_ID";
            ddl_VehicleManufacturer.DataSource = value;
            ddl_VehicleManufacturer.DataBind();
            ddl_VehicleManufacturer.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable Bind_ddl_VehicleModel
    {
        set
        {
            ddl_VehicleModel.DataTextField = "Name";
            ddl_VehicleModel.DataValueField = "ID";
            ddl_VehicleModel.DataSource = value;
            ddl_VehicleModel.DataBind();
            ddl_VehicleModel.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }


    public DataTable Bind_ddl_Repair_Service_Category
    {
        set
        {
            ddl_Repair_Service_Category.DataTextField = "Service_Category";
            ddl_Repair_Service_Category.DataValueField = "Service_Category_ID";
            ddl_Repair_Service_Category.DataSource = value;
            ddl_Repair_Service_Category.DataBind();
            ddl_Repair_Service_Category.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable Bind_ddl_Repair_Service
    {
        set
        {
            ddl_Repair_Service.DataTextField = "Name";
            ddl_Repair_Service.DataValueField = "ID";
            ddl_Repair_Service.DataSource = value;
            ddl_Repair_Service.DataBind();
            ddl_Repair_Service.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }


    public DataTable Bind_rblst_Schedule_By
    {
        set
        {
            rblst_Schedule_By.DataTextField = "Task_Schedule_By";
            rblst_Schedule_By.DataValueField = "Task_Schedule_By_ID";
            rblst_Schedule_By.DataSource = value;
            rblst_Schedule_By.DataBind();
        }
    }


    public DataTable Bind_ddl_Completion
    {
        set
        {
            ddl_Completion.DataTextField = "Task_Completion_Method";
            ddl_Completion.DataValueField = "Task_Completion_ID";
            ddl_Completion.DataSource = value;
            ddl_Completion.DataBind();
            ddl_Completion.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }


    #endregion

    #region Properties

    public string TaskTemplateName
    {
        set { txt_TaskTemplateName.Text = value; }
        get { return txt_TaskTemplateName.Text; }
    }

    public string Form_Type
    {
        set { hdn_Form_Type.Value = value; }
        get { return hdn_Form_Type.Value; }
    }

    public string To_Be_Worked_At_Type
    {
        set
        {
            lbl_To_Be_Worked_At.Text = value;
            rbl_Internal.Checked = false;
            rbl_External.Checked = false;
            if (lbl_To_Be_Worked_At.Text == "Location:")
            {
                rbl_Internal.Checked = true;
                rbl_External.Checked = false;
            }
            else
            {
                rbl_External.Checked = true;
                rbl_Internal.Checked = false;
            }
        }
        get { return lbl_To_Be_Worked_At.Text; }
    }

    public string Description
    {
        set { txt_Description.Text = value; }
        get { return txt_Description.Text; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //  return 48;
        }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }


    public string CustomAlertOnDetailsXML
    {
        get { return SessiondgCustomAlertOnDetailsGrid.GetXml(); }
    }

    public DataSet SessiondgCustomAlertOnDetailsGrid
    {
        get { return StateManager.GetState<DataSet>("CustomAlertOnDetailsGrid"); }
        set { StateManager.SaveState("CustomAlertOnDetailsGrid", value); }
    }

    private void BindCustomAlertOnDetailsGrid()
    {
        Set_Sr_No();
        dg_Custom_Alert_On_Details.DataSource = SessiondgCustomAlertOnDetailsGrid;
        dg_Custom_Alert_On_Details.DataBind();
    }

    #endregion



    #region OtherProperties

    public int Mode
    {
        get { return Util.DecryptToInt(Request.QueryString["Mode"]); }
    }

    #endregion


    #region Validation

    public bool validateUI()
    {
        bool _isValid = true;

        if (txt_TaskTemplateName.Text == string.Empty)
        {
            errorMessage = "Please Enter TaskTemplate Name";
            _isValid = false;
        }
        else if (Vehicle_Manufacturer_ID  == 0)
        {
            errorMessage = "Please Select Vehicle Manufacturer";
            ddl_VehicleManufacturer.Focus();
            _isValid = false;
        }
        else if (Vehicle_Model_ID == 0)
        {
            errorMessage = "Please Select Vehicle Model";
            ddl_VehicleModel.Focus();
            _isValid = false;
        }

        else if (Convert.ToInt32(ddl_Repair_Service_Category.SelectedValue) == 0)
        {
            errorMessage = "Please Select Repair Service Category ";
            _isValid = false;
        }
        else if (Convert.ToInt32(ddl_Repair_Service.SelectedValue) == 0)
        {
            errorMessage = "Please Select Repair Service ";
            _isValid = false;
        }
        else if (Convert.ToInt32(ddl_Completion.SelectedValue) == 0)
        {
            errorMessage = "Please Select Completion ";
            _isValid = false;
        }

        else if (CompanyWorkShop < 0)
        {
            errorMessage = "Please Select To Be Worked AT";
            _isValid = false;
            rblWorkShop.Focus();
        }
        else if (txt_Cost.Text == string.Empty || Convert.ToDecimal(txt_Cost.Text) == 0)
        {
            errorMessage = "Please Enter Cost";
            _isValid = false;
        }
        else if (lbl_To_Be_Worked_At.Text == "Preffered Vendor:")
        {
            if (hdn_Vendor_Id.Value == string.Empty || Convert.ToInt32(hdn_Vendor_Id.Value) == 0)
            {
                errorMessage = "Please select Vendor";
                _isValid = false;
            }
        }

        else if (lbl_To_Be_Worked_At.Text == "Location:")
        {
            if (hdn_Branch_Id.Value == string.Empty || Convert.ToInt32(hdn_Branch_Id.Value) == 0)
            {
                errorMessage = "Please select Location";
                _isValid = false;
            }
        }

        else if ((rblst_Schedule_By.Items[0].Selected == true || rblst_Schedule_By.Items[1].Selected == true) && Chk_Is_Custom.Checked == false)
        {
            if (txt_Due_Every.Text == string.Empty)
            {
                errorMessage = "Please Enter Due Every ";
                _isValid = false;
            }
            else if (txt_Alert_Before.Text == string.Empty)
            {
                errorMessage = "Please Enter Alert Before Due Every ";
                _isValid = false;
            }

            else if (Convert.ToInt32(txt_Due_Every.Text) < Convert.ToInt32(txt_Alert_Before.Text))
            {
                errorMessage = "Alert Before Should Not Exceeds Due Every ";
                _isValid = false;
            }
        }
        else
        {

            if (rblst_Schedule_By.Items[2].Selected == true && Chk_Is_Custom.Checked == false)
            {
                if (txt_Kms.Text == string.Empty)
                {
                    errorMessage = "Please Enter Due Every ";
                    _isValid = false;
                }
                else if (txt_Days_Months.Text == string.Empty)
                {
                    errorMessage = "Please Enter Days/Months ";
                    _isValid = false;
                }
                else if (txt_Odometer_Time_Kms.Text == string.Empty)
                {
                    errorMessage = "Please Enter Alert Before Odometer ";
                    _isValid = false;
                }
                else if (txt_Odometer_Time_Days_Months.Text == string.Empty)
                {
                    errorMessage = "Please Enter Alert Before Odometer Days/Months ";
                    _isValid = false;
                }
                else if (Convert.ToInt32(txt_Kms.Text) < Convert.ToInt32(txt_Odometer_Time_Kms.Text))
                {
                    errorMessage = "Alert Before Should Not Exceeds Due Every ";
                    _isValid = false;
                }
                else if (rbl_Is_Custome_Days.Checked == true && Convert.ToInt32(txt_Days_Months.Text) < Convert.ToInt32(txt_Odometer_Time_Days_Months.Text))
                {
                    errorMessage = "Alert Before Days Should Not Exceeds Due Every ";
                    _isValid = false;
                }

                else if (rbl_Is_Custome_Months.Checked == true && (Convert.ToInt32(txt_Odometer_Time_Days_Months.Text) > (Convert.ToInt32(txt_Days_Months.Text) * 30)))
                {
                    errorMessage = "Alert Before Days Exceeds The No Of Days of Month Specified";
                    _isValid = false;
                }
            }
        }

        return _isValid;
    }

    #endregion



    #region function


    private void Set_Sr_No()
    {
        int Sr_No;
        DataSet DS = SessiondgCustomAlertOnDetailsGrid;
        DataRow DR = null;
        for (Sr_No = 0; Sr_No <= DS.Tables[0].Rows.Count - 1; Sr_No++)
        {
            DR = DS.Tables[0].Rows[Sr_No];
            DR["Sr_No"] = Sr_No + 1;
        }
        SessiondgCustomAlertOnDetailsGrid = DS;
    }

    private void set_Hidden_Value()
    {

        if (rblst_Schedule_By.Items[0].Selected == true)
        {
            hdn_Schedule_By.Value = rblst_Schedule_By.SelectedValue;
        }
        else if (rblst_Schedule_By.Items[1].Selected == true)
        {
            hdn_Schedule_By.Value = rblst_Schedule_By.SelectedValue;
        }
        else if (rblst_Schedule_By.Items[2].Selected == true)
        {
            hdn_Schedule_By.Value = rblst_Schedule_By.SelectedValue;
        }

        if (lbl_To_Be_Worked_At.Text == "Location:")
        {
            hdn_Branch_Id.Value = ddl_To_Be_Worked_At.SelectedValue;
            hdn_Vendor_Id.Value = "0";
        }
        else
        {
            hdn_Branch_Id.Value = "0";
            hdn_Vendor_Id.Value = ddl_To_Be_Worked_At.SelectedValue;
        }

        hdn_Is_Days_Selected.Value = "false";
        hdn_Alert_Before_Days.Value = txt_Alert_Before.Text;

        Int32 Day_Value;

        Int32 Alert_Before_Days_Value;

        Day_Value = 0;
        Alert_Before_Days_Value = 0;

        if (rbl_Days.Checked == true)
        {

            hdn_DueOn_Days.Value = txt_Due_Every.Text == string.Empty || Convert.ToInt32(txt_Due_Every.Text) < 0 ? "0" : txt_Due_Every.Text;
            hdn_Month_Value.Value = Day_Value.ToString();

            hdn_DueOn_Value.Value = txt_Due_Every.Text == string.Empty || Convert.ToInt32(txt_Due_Every.Text) < 0 ? "0" : txt_Due_Every.Text;


            hdn_Alert_Before_Days.Value = txt_Alert_Before.Text == string.Empty || Convert.ToInt32(txt_Alert_Before.Text) < 0 ? "0" : txt_Alert_Before.Text;

            hdn_Alert_Before_Value.Value = Alert_Before_Days_Value.ToString();
        }
        else
        {
            hdn_Month_Value.Value = txt_Due_Every.Text == string.Empty || Convert.ToInt32(txt_Due_Every.Text) < 0 ? "0" : txt_Due_Every.Text;
            hdn_DueOn_Days.Value = Day_Value.ToString();
            hdn_DueOn_Value.Value = txt_Due_Every.Text == string.Empty || Convert.ToInt32(txt_Due_Every.Text) < 0 ? "0" : txt_Due_Every.Text;

            hdn_Alert_Before_Days.Value = txt_Alert_Before.Text == string.Empty || Convert.ToInt32(txt_Alert_Before.Text) < 0 ? "0" : txt_Alert_Before.Text;
            hdn_Alert_Before_Value.Value = Alert_Before_Days_Value.ToString();
        }

        //--------------------------------------------------------------------------

        if (rblst_Schedule_By.Items[0].Selected == true)
        {
            hdn_DueOn_Days.Value = "0";// txt_Due_Every.Text;
            hdn_DueOn_Value.Value = txt_Due_Every.Text == string.Empty || Convert.ToInt32(txt_Due_Every.Text) < 0 ? "0" : txt_Due_Every.Text; //Day_Value.ToString();

            hdn_Alert_Before_Days.Value = "0";// (Util.String2Int(hdn_DueOn_Days.Value) - Util.String2Int(hdn_Alert_Before_Days.Value)).ToString();
            hdn_Alert_Before_Value.Value = txt_Alert_Before.Text == string.Empty || Convert.ToInt32(txt_Alert_Before.Text) < 0 ? "0" : txt_Alert_Before.Text;

            hdn_Month_Value.Value = "0";// Day_Value.ToString();
        }

        //--------------------------------------------------------------------------

        if (rblst_Schedule_By.Items[1].Selected == true)
        {
            Alert_Before_Days_Value = txt_Alert_Before.Text == string.Empty ? 0 : Convert.ToInt32(txt_Alert_Before.Text);
            if (rbl_Days.Checked == true)
            {
                hdn_DueOn_Days.Value = txt_Due_Every.Text == string.Empty || Convert.ToInt32(txt_Due_Every.Text) < 0 ? "0" : txt_Due_Every.Text;
                hdn_Month_Value.Value = "0";// Day_Value.ToString();

                hdn_DueOn_Value.Value = "0"; //  txt_Due_Every.Text; km

                hdn_Alert_Before_Days.Value = txt_Alert_Before.Text == string.Empty || Convert.ToInt32(txt_Alert_Before.Text) < 0 ? "0" : txt_Alert_Before.Text;
                hdn_Alert_Before_Value.Value = "0";//  Alert_Before_Days_Value.ToString();
            }
            else
            {
                hdn_Month_Value.Value = txt_Due_Every.Text == string.Empty || Convert.ToInt32(txt_Due_Every.Text) < 0 ? "0" : txt_Due_Every.Text;

                hdn_DueOn_Days.Value = (Convert.ToInt32(hdn_Month_Value.Value) * 30).ToString();
                hdn_DueOn_Value.Value = "0"; //  txt_Due_Every.Text;  km

                //hdn_Alert_Before_Days.Value = (Alert_Before_Days_Value * 30 ).ToString(); //txt_Alert_Before.Text;
                hdn_Alert_Before_Days.Value = Alert_Before_Days_Value.ToString(); //txt_Alert_Before.Text;
                hdn_Alert_Before_Value.Value = "0"; // Alert_Before_Days_Value.ToString(); km
            }

            if (rbl_Days.Checked == true)
            {
                hdn_Is_Days_Selected.Value = "true";
            }
            else
            {
                hdn_Is_Days_Selected.Value = "false";
            }
        }

        //--------------------------------------------------------------------------

        if (rblst_Schedule_By.Items[2].Selected == true)
        {

            hdn_Month_Value.Value = txt_Days_Months.Text == string.Empty || Convert.ToInt32(txt_Days_Months.Text) < 0 ? "0" : txt_Days_Months.Text;



            hdn_DueOn_Value.Value = txt_Kms.Text == string.Empty || Convert.ToInt32(txt_Kms.Text) < 0 ? "0" : txt_Kms.Text;

            hdn_Alert_Before_Value.Value = txt_Odometer_Time_Kms.Text;

            if (rbl_Is_Custome_Days.Checked == true)
            {
                hdn_Is_Days_Selected.Value = "true";
                hdn_Alert_Before_Days.Value = txt_Odometer_Time_Days_Months.Text == string.Empty || Convert.ToInt32(txt_Odometer_Time_Days_Months.Text) < 0 ? "0" : txt_Odometer_Time_Days_Months.Text;



                hdn_DueOn_Days.Value = txt_Days_Months.Text == string.Empty || Convert.ToInt32(txt_Days_Months.Text) < 0 ? "0" : txt_Days_Months.Text;
                hdn_Month_Value.Value = "0";
            }
            else
            {
                hdn_Is_Days_Selected.Value = "false";
                hdn_Alert_Before_Days.Value = txt_Odometer_Time_Days_Months.Text == string.Empty || Convert.ToInt32(txt_Odometer_Time_Days_Months.Text) < 0 ? "0" : txt_Odometer_Time_Days_Months.Text;
                hdn_DueOn_Days.Value = (Convert.ToInt32(txt_Days_Months.Text) * 30).ToString();
                hdn_Month_Value.Value = txt_Days_Months.Text == string.Empty || Convert.ToInt32(txt_Days_Months.Text) < 0 ? "0" : txt_Days_Months.Text;
            }
        }

        //--------------------------------------------------------------------------

    }
    public void Set_Day_Month_Visibility(Boolean value)
    {
        rbl_Days.Visible = value;
        rbl_Months.Visible = value;

        lbl_Due_Every.Visible = !value;

        if (rbl_Days.Checked == true && rblst_Schedule_By.Items[1].Selected == true)
        {
            //lbl_Alert_Before.Visible = !value;
            lbl_Alert_Before.Text = "Days";
        }
        else if (rbl_Months.Checked == true && rblst_Schedule_By.Items[1].Selected == true)
        {
            //lbl_Alert_Before.Visible = !value;
            lbl_Alert_Before.Text = "Days";
        }
        else
        {
            //lbl_Alert_Before.Visible = !value;
            lbl_Alert_Before.Text = "Kms";
        }

    }

    private void rblst_selected()
    {
        if (rblst_Schedule_By.Items[0].Selected == true)
        {
            if (hdn_Form_Type.Value == "Template" || Chk_Is_Custom.Checked == false)
            {
                Set_Day_Month_Visibility(false);
            }
            else
            {
                dg_Custom_Alert_On_Details.Columns[2].Visible = true;
                dg_Custom_Alert_On_Details.Columns[1].Visible = false;

                lbl_Custome_Alert_Before.Text = "Kms";

                if (keyID <= 0)
                {
                    Clear_Grid();
                }
            }
        }
        else if (rblst_Schedule_By.Items[1].Selected == true)
        {
            if (hdn_Form_Type.Value == "Template" || Chk_Is_Custom.Checked == false)
            {
                Set_Day_Month_Visibility(true);
            }
            else
            {
                dg_Custom_Alert_On_Details.Columns[2].Visible = false;
                dg_Custom_Alert_On_Details.Columns[1].Visible = true;

                lbl_Custome_Alert_Before.Text = "Days";

                if (keyID <= 0)
                {
                    Clear_Grid();
                }
            }
        }

        if (hdn_Form_Type.Value == "Template")
        {

        }
        else
        {
            if (rblst_Schedule_By.Items[0].Selected == true)
            {
                txt_Last_Permormed_On.Visible = true;
                tdtxt_Last_Permormed_On.Visible = true;
                lbl_Last_Permormed_On.Visible = true;
                //dtp_Last_Permormed_Date.Visible = false;   
                //tddtp_Last_Permormed_Date.Visible = false;
                Label1.Visible = false;
            }
            else if (rblst_Schedule_By.Items[1].Selected == true)
            {
                //dtp_Last_Permormed_Date.Visible = true;
                //tddtp_Last_Permormed_Date.Visible = true;
                txt_Last_Permormed_On.Visible = false;
                tdtxt_Last_Permormed_On.Visible = false;
                Label1.Visible = false;
                tdkms.Visible = false;
                tdon.Visible = false;
                lbl_Last_Permormed_On.Visible = false;
            }
            else if (rblst_Schedule_By.Items[2].Selected == true)
            {
                //dtp_Last_Permormed_Date.Visible = true;
                //tddtp_Last_Permormed_Date.Visible = true;
                txt_Last_Permormed_On.Visible = true;
                tdtxt_Last_Permormed_On.Visible = true;
                Label1.Visible = true;
                tdon.Visible = true;
                lbl_Last_Permormed_On.Visible = true;
            }
        }
    }

    private void Clear_Grid()
    {
        DataSet DS = SessiondgCustomAlertOnDetailsGrid;
        DS = SessiondgCustomAlertOnDetailsGrid;

        DS.Tables[0].Rows.Clear();

        SessiondgCustomAlertOnDetailsGrid = DS;

        Set_Sr_No();
        BindCustomAlertOnDetailsGrid();
    }

    private void chk_checked()
    {
        if (Chk_Is_Custom.Checked == true)
        {
            rblst_Schedule_By.Items[2].Enabled = false;
            if (keyID <= 0)
            {
                rblst_Schedule_By.SelectedIndex = 0;
            }
        }
        else
        {
            rblst_Schedule_By.Items[2].Enabled = true;
        }

        if (rblst_Schedule_By.Items[0].Selected == true)
        {
            Set_Day_Month_Visibility(false);
        }
        else if (rblst_Schedule_By.Items[1].Selected == true)
        {
            Set_Day_Month_Visibility(true);
        }

        if (Chk_Is_Custom.Checked == false && keyID <= 0)
        {
            rblst_Schedule_By.SelectedIndex = 0;
        }

        if (hdn_Form_Type.Value == "Template")
        {

        }
        else
        {
            if (rblst_Schedule_By.Items[0].Selected == true)
            {
                txt_Last_Permormed_On.Visible = true;
                //tddtp_Last_Permormed_Date.Visible=false;
            }
            else if (rblst_Schedule_By.Items[1].Selected == true)
            {
                //dtp_Last_Permormed_Date.Visible=true;
                txt_Last_Permormed_On.Visible = false;
            }
        }
    }

    #endregion

    #region GridControlsEvents

    protected void dg_Custom_Alert_On_Details_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Custom_Alert_On_Details.EditItemIndex = -1;
        dg_Custom_Alert_On_Details.ShowFooter = true;
        Set_Sr_No();
        BindCustomAlertOnDetailsGrid();
    }

    protected void dg_Custom_Alert_On_Details_EditCommand(object source, DataGridCommandEventArgs e)
    {
        //LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));
        //lbtn_Delete.Enabled = false;
        dg_Custom_Alert_On_Details.EditItemIndex = e.Item.ItemIndex;
        dg_Custom_Alert_On_Details.ShowFooter = false;
        BindCustomAlertOnDetailsGrid();
    }

    protected void dg_Custom_Alert_On_Details_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            objDS = SessiondgCustomAlertOnDetailsGrid;

            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                BindCustomAlertOnDetailsGrid();
                dg_Custom_Alert_On_Details.EditItemIndex = -1;
                dg_Custom_Alert_On_Details.ShowFooter = true;
            }
        }
    }

    protected void dg_Custom_Alert_On_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        txt_Custom_Grid_Odometer = (TextBox)(e.Item.FindControl("txt_Custom_Grid_Odometer"));
        dtp_Custom_Grid_Date = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_Custom_Grid_Date"));

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                dtp_Custom_Grid_Date.SelectedDate = DateTime.Now.AddDays(1);
                txt_Custom_Grid_Odometer.Text = "0";

                upd_pnl_dg_Custom_Alert_On_Details.Update();
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                DataSet DS = SessiondgCustomAlertOnDetailsGrid;
                DataRow DR = DS.Tables[0].Rows[e.Item.ItemIndex];

                dtp_Custom_Grid_Date.SelectedDate = Convert.ToDateTime(DR["Grid_Date"]);
                txt_Custom_Grid_Odometer.Text = DR["Grid_Odometer"].ToString();
                rblst_Schedule_By.Enabled = false;
                Chk_Is_Custom.Enabled = false;
            }
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        DataSet DS = SessiondgCustomAlertOnDetailsGrid;
        DataRow DR = null;
        if (e.CommandName == "Add")
        {
            txt_Custom_Grid_Odometer = (TextBox)(e.Item.FindControl("txt_Custom_Grid_Odometer"));

            dtp_Custom_Grid_Date = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_Custom_Grid_Date"));

            DR = DS.Tables[0].NewRow();
        }
        else if (e.CommandName == "Update")
        {
            txt_Custom_Grid_Odometer = (TextBox)(e.Item.FindControl("txt_Custom_Grid_Odometer"));

            dtp_Custom_Grid_Date = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_Custom_Grid_Date"));

            DR = DS.Tables[0].Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update(txt_Custom_Grid_Odometer.Text == string.Empty ? 0 : Convert.ToInt32(txt_Custom_Grid_Odometer.Text), dtp_Custom_Grid_Date.SelectedDate, e.Item.ItemIndex) == true)
        {
            DR["Grid_Date"] = dtp_Custom_Grid_Date.SelectedDate.ToString("dd MMM yyyy");

            DR["Grid_Odometer"] = txt_Custom_Grid_Odometer.Text == string.Empty ? "0" : txt_Custom_Grid_Odometer.Text;

            if (e.CommandName == "Add") { DS.Tables[0].Rows.Add(DR); }
            SessiondgCustomAlertOnDetailsGrid = DS;
        }
    }

    private bool Allow_To_Add_Update(Int32 Custom_Grid_Odometer, DateTime Custom_Grid_Date, Int32 RowIndex)
    {

        if (txt_Custom_Grid_Odometer.Text == string.Empty && rblst_Schedule_By.Items[0].Selected == true)
        {
            errorMessage = "Please Enter Odometer";
            txt_Custom_Grid_Odometer.Focus();
        }
        else if (dtp_Custom_Grid_Date.SelectedDate < DateTime.Now && rblst_Schedule_By.Items[1].Selected == true)
        {
            errorMessage = "Schedule Date Should Not Less Than System Date";
            dtp_Custom_Grid_Date.Focus();
        }
        else
            isValid = true;


        DataSet ds_CustomAlertOnDetailsGrid;

        ds_CustomAlertOnDetailsGrid = SessiondgCustomAlertOnDetailsGrid;

        Int32 Sr_No;

        string CustomGridOdometer;
        DateTime CustomGridDate;

        DataSet DS = SessiondgCustomAlertOnDetailsGrid;
        DataRow DR = null;

        for (Sr_No = 0; Sr_No <= DS.Tables[0].Rows.Count - 1; Sr_No++)
        {
            DR = DS.Tables[0].Rows[Sr_No];
            CustomGridOdometer = DR["Grid_Odometer"].ToString();
            CustomGridDate = Convert.ToDateTime(DR["Grid_Date"]);

            if (rblst_Schedule_By.Items[0].Selected == true)
            {
                if (RowIndex >= 0)
                {
                    if (Convert.ToInt32(CustomGridOdometer) >= Custom_Grid_Odometer && Sr_No < RowIndex)
                    {
                        errorMessage = "Please Enter Valid Odometer";
                        isValid = false;
                        break;
                    }
                    else if (Convert.ToInt32(CustomGridOdometer) < Custom_Grid_Odometer && Sr_No > RowIndex)
                    {
                        errorMessage = "Please Enter Valid Odometer";
                        isValid = false;
                        break;
                    }
                }
                else
                {
                    if (Convert.ToInt32(CustomGridOdometer) >= Custom_Grid_Odometer)
                    {
                        errorMessage = "Please Enter Valid Odometer";
                        isValid = false;
                        break;
                    }
                }
            }
            else if (rblst_Schedule_By.Items[1].Selected == true)
            {
                if (RowIndex >= 0)
                {
                    if (CustomGridDate >= Custom_Grid_Date && Sr_No < RowIndex)
                    {
                        errorMessage = "Please Enter Valid Date";
                        isValid = false;
                        break;
                    }
                    else if (CustomGridDate <= Custom_Grid_Date && Sr_No > RowIndex)
                    {
                        errorMessage = "Please Enter Valid Date";
                        isValid = false;
                        break;
                    }
                }
                else
                {
                    if (CustomGridDate >= Custom_Grid_Date)
                    {
                        errorMessage = "Please Enter Valid Date";
                        isValid = false;
                        break;
                    }
                }
            }
        }

        return isValid;
    }

    protected void dg_Custom_Alert_On_Details_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Insert_Update_Dataset(source, e);

        if (isValid == true)
        {
            dg_Custom_Alert_On_Details.EditItemIndex = -1;
            dg_Custom_Alert_On_Details.ShowFooter = true;

            BindCustomAlertOnDetailsGrid();

            rblst_Schedule_By.Enabled = true;
            Chk_Is_Custom.Enabled = true;
        }
    }

    protected void dg_Custom_Alert_On_Details_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        DataSet DS = SessiondgCustomAlertOnDetailsGrid;
        DataRow DR = DS.Tables[0].Rows[e.Item.ItemIndex];
        DR.Delete();
        DR.AcceptChanges();
        SessiondgCustomAlertOnDetailsGrid = DS;
        BindCustomAlertOnDetailsGrid();
    }
    #endregion


    #region ControlEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        hdn_Form_Type.Value = StateManager.GetState<string>("QueryString").ToString();

        //  hdn_Form_Type.Value = "Template";
        //hdn_Form_Type.Value = "VehiclePM";  


        objTaskTemplatePresenter = new TaskTemplatePresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

            if (hdn_Form_Type.Value == "Template")
            {
                Chk_Is_Custom.Enabled = false;
                //Chk_Is_Custom.Visible = false;
            }
            else
            {
                Chk_Is_Custom.Enabled = true;
                // Chk_Is_Custom.Visible = true ;
                hdn_Vehicle_Id.Value = Util.DecryptToInt(Request.QueryString["Vehicle_Id"]).ToString();


                lbl_Task_Template.Text = "Task:";
                lbl_Task_Template_Name.Text = "Task Name:";

                lbl_Heading.Text = "Task";

                ddl_VehicleManufacturer.Enabled = false;
                ddl_VehicleModel.Enabled = false;
            }

            if (keyID <= 0)
            {
                //objTaskTemplatePresenter.FillDDL_For("EC_Master_Branch", "Branch_ID as ID,Branch_Name as Name","", "Branch_Name");
                objTaskTemplatePresenter.FillVehicleDDL_For(" Manufacturer_ID=" + ddl_VehicleManufacturer.SelectedValue);
                objTaskTemplatePresenter.FillDDL_For(" Service_Category_ID=" + ddl_Repair_Service_Category.SelectedValue);

                if (keyID <= 0)
                {
                    lbl_To_Be_Worked_At.Text = "Preffered Vendor:";
                    rblst_Schedule_By.Items[0].Selected = true;
                    dtp_Last_Permormed_Date.SelectedDate = DateTime.Now;
                }

                if (rblst_Schedule_By.Items[0].Selected == true)
                {
                    Set_Day_Month_Visibility(false);
                }

                if (rblst_Schedule_By.Items[1].Selected == true)
                {
                    Set_Day_Month_Visibility(true);
                }
            }

            ddl_To_Be_Worked_At.DataValueField = "ID";
            ddl_To_Be_Worked_At.DataTextField = "Name";
            //ddl_Tyre_No.OtherColumns = keyID.ToString();
            ddl_To_Be_Worked_At.OtherColumns = lbl_To_Be_Worked_At.Text;

            BindCustomAlertOnDetailsGrid();

            if (keyID > 0)
            {
                rblst_selected();
                chk_checked();
            }

            if (keyID <= 0)
            {
                rbl_External_CheckedChanged(rbl_External, e);
                chk_checked();
            }

            if (hdn_Form_Type.Value == "VehiclePM" && keyID > 0)
            {
                rblst_Schedule_By.Enabled = false;
                Chk_Is_Custom.Enabled = false;
            }
        }
    }

    protected void ddl_To_Be_Worked_At_TxtChange(object sender, EventArgs e)
    {
        ddl_To_Be_Worked_At.OtherColumns = lbl_To_Be_Worked_At.Text;

        if (lbl_To_Be_Worked_At.Text == "Location:")
        {
            Vendor_Id = 0;
            Branch_Id = Util.String2Int(ddl_To_Be_Worked_At.SelectedValue);
        }
        else
        {
            Vendor_Id = Util.String2Int(ddl_To_Be_Worked_At.SelectedValue);
            Branch_Id = 0;
        }
        Upd_Pnl_To_Be_Worked_At.Update();
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        set_Hidden_Value();
        objTaskTemplatePresenter.Save();
        //string _popopScript;
        //_popopScript = "<script language='javascript'> window.opener.location.reload();window.close();</script>";
        //Page.ClientScript.RegisterStartupScript(typeof(string), "PopupScript", _popopScript, true);
    }

    protected void rbl_Internal_CheckedChanged(object sender, EventArgs e)
    {
        lbl_To_Be_Worked_At.Text = "Location:";
        // objTaskTemplatePresenter.FillDDL_For("EC_Master_Branch", "Branch_ID as ID,Branch_Name as Name","", "Branch_Name");

        ddl_To_Be_Worked_At.OtherColumns = lbl_To_Be_Worked_At.Text;

        Raj.EC.Common.SetValueToDDLSearch("", "0", ddl_To_Be_Worked_At);
    }

    protected void rbl_External_CheckedChanged(object sender, EventArgs e)
    {
        lbl_To_Be_Worked_At.Text = "Preffered Vendor:";
        //objTaskTemplatePresenter.FillDDL_For("EF_Master_Vendor", "Vendor_ID as ID,Vendor_Name as Name","", "Vendor_Name");

        ddl_To_Be_Worked_At.OtherColumns = lbl_To_Be_Worked_At.Text;
        Raj.EC.Common.SetValueToDDLSearch("", "0", ddl_To_Be_Worked_At);
    }

    protected void ddl_VehicleManufacturer_SelectedIndexChanged(object sender, EventArgs e)
    {
        objTaskTemplatePresenter.FillVehicleDDL_For(" Manufacturer_ID=" + ddl_VehicleManufacturer.SelectedValue);
        scm_TaskTemplate.SetFocus(ddl_VehicleManufacturer);
    }

    protected void ddl_Repair_Service_Category_SelectedIndexChanged(object sender, EventArgs e)
    {
        objTaskTemplatePresenter.FillDDL_For(" Service_Category_ID=" + ddl_Repair_Service_Category.SelectedValue);
        scm_TaskTemplate.SetFocus(ddl_Repair_Service_Category);
    }

    protected void rbl_Odometer_CheckedChanged(object sender, EventArgs e)
    {
        //if (rbl_Odometer.Checked == true)
        if (rblst_Schedule_By.Items[0].Selected == true)
        {
            Set_Day_Month_Visibility(false);
        }
        //  scm_TaskTemplate.SetFocus(rbl_Odometer);
    }

    protected void rbl_Time_CheckedChanged(object sender, EventArgs e)
    {
        //if (rbl_Time.Checked == true)
        if (rblst_Schedule_By.Items[1].Selected == true)
        {
            Set_Day_Month_Visibility(true);
        }
        // scm_TaskTemplate.SetFocus(rblst_Schedule_By);
    }

    protected void rbl_Is_Custome_Odometer_CheckedChanged(object sender, EventArgs e)
    {
        //if (rbl_Is_Custome_Odometer.Checked == true)
        if (rblst_Schedule_By.Items[2].Selected == true)
        {
            Set_Day_Month_Visibility(false);
        }
        // scm_TaskTemplate.SetFocus(rblst_Schedule_By);
    }

    protected void rbl_Is_Custome_Time_CheckedChanged(object sender, EventArgs e)
    {
        //if (rbl_Is_Custome_Time.Checked == true)
        //{
        //    Set_Day_Month_Visibility(true);
        //}
        //  scm_TaskTemplate.SetFocus(rblst_Schedule_By);
    }

    protected void rbl_Is_Custome_Days_CheckedChanged(object sender, EventArgs e)
    {
        lbl_Is_Custome_Days_Months.Text = "Days";
    }

    protected void rbl_Is_Custome_Months_CheckedChanged(object sender, EventArgs e)
    {
        lbl_Is_Custome_Days_Months.Text = "Days";
    }

    protected void rbl_Odometer_Time_CheckedChanged(object sender, EventArgs e)
    {
        scm_TaskTemplate.SetFocus(rblst_Schedule_By);
    }

    protected void rblst_Schedule_By_SelectedIndexChanged(object sender, EventArgs e)
    {
        rblst_selected();
        //chk_checked();
        string popupScript = "<script language='javascript'>Show_Last_Permormed_On();</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(string), "PopupScript", popupScript.ToString(), false);
    }

    protected void Chk_Is_Custom_CheckedChanged(object sender, EventArgs e)
    {
        chk_checked();
        rblst_selected();

        if (hdn_Form_Type.Value == "VehiclePM")
        {
            Clear_Grid();
        }
    }


    #endregion

}