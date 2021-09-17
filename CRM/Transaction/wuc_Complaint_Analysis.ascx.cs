
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
using ClassLibraryMVP.General;
using Raj.CRM.TransactionView;
using Raj.CRM.TransactionPresenter;
using ClassLibraryMVP;
using Raj.EC;

public partial class Complaint_Analysis_wuc_Complaint_Analysis : System.Web.UI.UserControl, IComplaint_AnalysisView
{
    private Complaint_AnalysisPresenter _Complaint_AnalysisPresenter;

    DropDownList ddl_Resion_For_Fault;

    CheckBox chk_VA_Booking ;
    CheckBox chk_Booking_Branch ;
    CheckBox chk_Booking_Hub ;
    CheckBox chk_Delivery_Hub;
    CheckBox chk_Delivery_Branch;
    CheckBox chk_VA_Delivery;
    CheckBox chk_Customer;
    DataSet objDS;
    DataSet ds_Complaint_Analysis;

    bool isValid = false;

    #region IView Members

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
               return Util.DecryptToInt(Request.QueryString["Id"]);
          //  return 34;
        }
    }

    #endregion

    #region InitInterface

    public int Ticket_ID
    {
        get{return Util.DecryptToInt(Request.QueryString["Ticket_ID"]);}        
    }

    public string Complaint_Analysis_Xml
    {
        get { return Session_Complaint_Analysis_Details.GetXml(); }
    }

    public string GC_Docket_No
    {
        set {lbl_GC_Docket_No.Text = value;}
        get {return lbl_GC_Docket_No.Text ;}
    }

    public string Ticket_No
    {
        set{lbl_Ticket_No.Text = value;}
        get{return lbl_Ticket_No.Text; }
    }

    public int GC_Docket_Id
    {
        set{hdn_GC_Docket_Id.Value  = Convert.ToString( value); }
        get {return Convert.ToInt32( hdn_GC_Docket_Id.Value); }
    }

    public string Action_Taken
    {
        set
        {
            txt_Action_Taken.Text = value;
        }
        get
        {
            return txt_Action_Taken.Text;
        }
    }

    public string Person_Responsible 
    {
        set
        {
            txt_Person_Responsible.Text = value;
        }
        get
        {
            return txt_Person_Responsible.Text;
        }
    }


    //public DateTime Complaint_Analysis_Date
    //{
    //    set { Picker_Voucher_Date.SelectedDate = value; }
    //    get { return Picker_Voucher_Date.SelectedDate; }
    //}

    
    public DataSet Session_Ds_Complaint_Analysis
    {
        get { return StateManager.GetState<DataSet>("ds_Complaint_Analysis"); }
        set { StateManager.SaveState("ds_Complaint_Analysis", value); }
    }
    
    public DataSet Bind_Complaint_Analysis_Details
    {
        set
        {
            Session_Complaint_Analysis_Details = value;
            Set_Sr_No();
            
            dg_Complaint_Analysis_Details.DataSource = Session_Complaint_Analysis_Details;
            dg_Complaint_Analysis_Details.DataBind();
        }
    }

    public DataSet Session_Complaint_Analysis_Details
    {
        get { return StateManager.GetState<DataSet>("Complaint_Analysis"); }
        set { StateManager.SaveState("Complaint_Analysis", value); }
    }

    private void BindComplaintAnalysisDetailsGrid()
    {
        Set_Sr_No();        
        dg_Complaint_Analysis_Details.DataSource = Session_Complaint_Analysis_Details;
        dg_Complaint_Analysis_Details.DataBind();
    }

    //public void SetVendorId(string text, string value)
    //{
    //    ddl_Vendor.DataValueField = "Id";
    //    ddl_Vendor.DataTextField = "Name";

    //    hdn_Vendor_Id.Value = value;
    //    //ddl_Truck_No.OtherColumns = Convert.ToString(Param.Get_Branch_Param().BranchID) + "Ö" + Convert.ToString(ddl_Vendor.SelectedValue);

    //    //ddl_Vendor.OtherColumns = Convert.ToString(1) + "Ö" + Convert.ToString(ddl_Vendor.SelectedValue);

    //    Raj.FA.CommonCs.SetValueToDDLSearch(text, value, ddl_Vendor);
    //}

    #endregion
      
    #region Function

    public bool validateUI()
    {
         return true ;
    }

    private void Set_Sr_No()
    {
        int Sr_No;
        DataSet DS = Session_Complaint_Analysis_Details;
        DataRow DR = null;
        for (Sr_No = 0; Sr_No <= DS.Tables[0].Rows.Count - 1; Sr_No++)
        {
            DR = DS.Tables[0].Rows[Sr_No];
            DR["Sr_No"] = Sr_No + 1;
        }
        Session_Complaint_Analysis_Details = DS;
    }
    
    private void Load_Dataset()
    {
        DataSet ds_temp = new DataSet();        

        try
        {
            if (ds_Complaint_Analysis.Tables["Complaint_Analysis"].Rows.Count > 0)
            {
                ds_temp = Session_Complaint_Analysis_Details;
                if (ds_temp.Tables[0].Rows.Count > ds_Complaint_Analysis.Tables[0].Rows.Count)
                {                   
                }
                else
                {
                    ds_Complaint_Analysis = ds_temp;
                }

                Set_Sr_No();
        
                //dg_Complaint_Analysis_Details = Session_Complaint_Analysis_Details;
                dg_Complaint_Analysis_Details.DataSource = Session_Complaint_Analysis_Details;
            }
        }
        catch
        {
            //ds_Complaint_Analysis = Create_Columns_In_Dataset();
            //dg_Complaint_Analysis_Details.DataSource = ds_Complaint_Analysis;
        }
        dg_Complaint_Analysis_Details.DataBind();        
        Session_Complaint_Analysis_Details = ds_Complaint_Analysis;
    }

    private bool Allow_To_Add_Update()
    {
        errorMessage = "";

        if (ddl_Resion_For_Fault.SelectedValue == "0" || ddl_Resion_For_Fault.SelectedValue == "")
        {
            errorMessage = "Please Select Reason For Fault";
            ddl_Resion_For_Fault.Focus();
        }
        else if (!( chk_Booking_Branch.Checked || chk_Booking_Hub.Checked ||
                 chk_Delivery_Branch.Checked || chk_Delivery_Hub.Checked ||
                 chk_VA_Booking.Checked || chk_VA_Delivery.Checked || chk_Customer.Checked))
        {
            errorMessage = "Please Select Atleast One Option";
        }
        else
            isValid = true;

        return isValid;
    }

    private bool Allow_To_Save()
    {

        errorMessage = "";

        if (Session_Complaint_Analysis_Details.Tables[0].Rows.Count == 0)
        {
            errorMessage = " Please Enter Complaint Analysis Details";
        }

        else if (txt_Person_Responsible.Text.Trim() == String.Empty)
        {
            errorMessage = "Please Enter Person Responsible";
            txt_Person_Responsible.Focus();
        }
        else if (txt_Action_Taken.Text.Trim()  == String.Empty)
        {
            errorMessage = "Please Enter Action Taken Details";
            txt_Action_Taken.Focus();
        }
        else
        {
            isValid = true;
        } 
        return isValid;        
    }

    private void Fill_Region_For_Falult()
    {

        DataSet ds_Region_For_Fault = new DataSet();
        ds_Region_For_Fault = _Complaint_AnalysisPresenter.Get_Region_For_Fault();

        ddl_Resion_For_Fault.DataValueField = "Reason_Fault_ID";
        ddl_Resion_For_Fault.DataTextField = "Reason_For_Fault";
        ddl_Resion_For_Fault.DataBind();

        ddl_Resion_For_Fault.Items.Insert(0, new ListItem("Select One", "0"));
    }
    #endregion

    #region GridControlsEvents

    protected void dg_Complaint_Analysis_Details_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Complaint_Analysis_Details.EditItemIndex = -1;
        dg_Complaint_Analysis_Details.ShowFooter = true;
        Set_Sr_No();
        
        BindComplaintAnalysisDetailsGrid();
    }

    protected void dg_Complaint_Analysis_Details_EditCommand(object source, DataGridCommandEventArgs e)
    {
        //LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));
        //lbtn_Delete.Enabled = false;
        dg_Complaint_Analysis_Details.EditItemIndex = e.Item.ItemIndex;
        dg_Complaint_Analysis_Details.ShowFooter = false;
        BindComplaintAnalysisDetailsGrid();
    }

    protected void dg_Complaint_Analysis_Details_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
            try
            {
                objDS = Session_Complaint_Analysis_Details;
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDS.Tables[0].Columns["Reason_Fault_ID"];
                objDS.Tables[0].PrimaryKey = _dtColumnPrimaryKey;


                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindComplaintAnalysisDetailsGrid();
                    dg_Complaint_Analysis_Details.EditItemIndex = -1;
                    dg_Complaint_Analysis_Details.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                //lbl_Errors.Text = "Duplicate Ledger Id.";

                errorMessage = "Duplicate Reason For Fault";
            }
    }

    protected void dg_Complaint_Analysis_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        ddl_Resion_For_Fault = (DropDownList)(e.Item.FindControl("ddl_Resion_For_Fault"));

        chk_VA_Booking = (CheckBox)(e.Item.FindControl("chk_VA_Booking"));
        chk_Booking_Branch = (CheckBox)(e.Item.FindControl("chk_Booking_Branch"));
        chk_Booking_Hub = (CheckBox)(e.Item.FindControl("chk_Booking_Hub"));
        chk_Delivery_Hub = (CheckBox)(e.Item.FindControl("chk_Delivery_Hub"));
        chk_Delivery_Branch = (CheckBox)(e.Item.FindControl("chk_Delivery_Branch"));
        chk_VA_Delivery = (CheckBox)(e.Item.FindControl("chk_VA_Delivery"));

        chk_Customer = (CheckBox)(e.Item.FindControl("chk_Customer"));
            
        DataSet ds_Region_For_Fault = new DataSet();
         
            if (e.Item.ItemType != ListItemType.Header)
            {
                if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
                {
                    ddl_Resion_For_Fault = (DropDownList)(e.Item.FindControl("ddl_Resion_For_Fault"));


                    //Fill_Region_For_Falult();

                    ds_Region_For_Fault = _Complaint_AnalysisPresenter.Get_Region_For_Fault();
                    ddl_Resion_For_Fault.DataSource = ds_Region_For_Fault;
                    ddl_Resion_For_Fault.DataValueField = "Reason_Fault_ID";
                    ddl_Resion_For_Fault.DataTextField = "Reason_For_Fault";
                    ddl_Resion_For_Fault.DataBind();

                    ddl_Resion_For_Fault.Items.Insert(0, new ListItem("Select One", "0"));
                }

                if (e.Item.ItemType == ListItemType.EditItem)
                {
                    DataSet DS = Session_Complaint_Analysis_Details;
                    DataRow DR = DS.Tables[0].Rows[e.Item.ItemIndex];

                    ddl_Resion_For_Fault.SelectedValue = Convert.ToString  ( DR["Reason_Fault_ID"]);
                    ddl_Resion_For_Fault.SelectedItem.Text = Convert.ToString( DR["Reason_For_Fault"]);


                    chk_VA_Booking.Checked = Convert.ToBoolean( DR["VA_Booking"]);
                    chk_Booking_Branch.Checked = Convert.ToBoolean( DR["Booking_Branch"]);
                    chk_Booking_Hub.Checked = Convert.ToBoolean(DR["Booking_Hub"]);
                    chk_Delivery_Hub.Checked = Convert.ToBoolean( DR["Delivery_Hub"]);
                    chk_Delivery_Branch.Checked = Convert.ToBoolean( DR["Delivery_Branch"]);
                    chk_VA_Delivery.Checked=Convert.ToBoolean( DR["VA_Delivery"] );

                    chk_Customer.Checked   =Convert.ToBoolean( DR["Customer"] ); 
                }
            }
        
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        DataSet DS = Session_Complaint_Analysis_Details;
        DataRow DR = null;
        if (e.CommandName == "Add")
        {
            ddl_Resion_For_Fault = (DropDownList)(e.Item.FindControl("ddl_Resion_For_Fault"));

            chk_VA_Booking = (CheckBox)(e.Item.FindControl("chk_VA_Booking"));
            chk_Booking_Branch = (CheckBox)(e.Item.FindControl("chk_Booking_Branch"));
            chk_Booking_Hub = (CheckBox)(e.Item.FindControl("chk_Booking_Hub"));
            chk_Delivery_Hub = (CheckBox)(e.Item.FindControl("chk_Delivery_Hub"));
            chk_Delivery_Branch = (CheckBox)(e.Item.FindControl("chk_Delivery_Branch"));
            chk_VA_Delivery = (CheckBox)(e.Item.FindControl("chk_VA_Delivery"));
            chk_Customer = (CheckBox)(e.Item.FindControl("chk_Customer"));

            DR = DS.Tables[0].NewRow();
        }
        else if (e.CommandName == "Update")
        {
            ddl_Resion_For_Fault = (DropDownList)(e.Item.FindControl("ddl_Resion_For_Fault"));

            chk_VA_Booking = (CheckBox)(e.Item.FindControl("chk_VA_Booking"));
            chk_Booking_Branch = (CheckBox)(e.Item.FindControl("chk_Booking_Branch"));
            chk_Booking_Hub = (CheckBox)(e.Item.FindControl("chk_Booking_Hub"));
            chk_Delivery_Hub= ( CheckBox ) (e.Item.FindControl("chk_Delivery_Hub"));
            chk_Delivery_Branch= ( CheckBox ) (e.Item.FindControl("chk_Delivery_Branch"));
            chk_VA_Delivery = (CheckBox)(e.Item.FindControl("chk_VA_Delivery"));
            chk_Customer = (CheckBox)(e.Item.FindControl("chk_Customer"));

            DR = DS.Tables[0].Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            
            DR["Reason_Fault_ID"] = ddl_Resion_For_Fault.SelectedValue;
            DR["Reason_For_Fault"] = ddl_Resion_For_Fault.SelectedItem.Text  ;
            
            DR["VA_Booking"]= chk_VA_Booking.Checked ;
            DR["Booking_Branch"]=chk_Booking_Branch.Checked;
            DR["Booking_Hub"]=chk_Booking_Hub.Checked;
	        DR["Delivery_Hub"]=chk_Delivery_Hub.Checked;
            DR["Delivery_Branch"]=chk_Delivery_Branch.Checked;
            DR["VA_Delivery"] = chk_VA_Delivery.Checked;
            DR["Customer"] = chk_Customer.Checked;
             

            if (e.CommandName == "Add") { DS.Tables[0].Rows.Add(DR); }
            Session_Complaint_Analysis_Details = DS;
        }
    }

  

    protected void dg_Complaint_Analysis_Details_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Insert_Update_Dataset(source, e);

        if (isValid == true)
        {
            dg_Complaint_Analysis_Details.EditItemIndex = -1;
            dg_Complaint_Analysis_Details.ShowFooter = true;

            BindComplaintAnalysisDetailsGrid();
        }
    }

    protected void dg_Complaint_Analysis_Details_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        DataSet DS = Session_Complaint_Analysis_Details;
        DataRow DR = DS.Tables[0].Rows[e.Item.ItemIndex];
        DR.Delete();
      //  DR.AcceptChanges();
        Session_Complaint_Analysis_Details = DS;
        BindComplaintAnalysisDetailsGrid();
        dg_Complaint_Analysis_Details.ShowFooter = true;
    }
    #endregion

    #region GridControlsEvents

    //protected void dg_LHPO_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    //{
    //    if (e.Item.ItemIndex >= 0)
    //    {
    //        DataSet ds_LHPO_Details = new DataSet();
    //        CheckBox chk = new CheckBox();
    //        Boolean Checked;

    //        ds_LHPO_Details = Session_LHPO_Details;

    //        Checked = Convert.ToBoolean(ds_LHPO_Details.Tables[0].Rows[e.Item.ItemIndex]["selected"]);

    //        chk = (CheckBox)e.Item.FindControl("chk_Attach");
    //        if (Checked == true)
    //        {
    //            chk.Checked = true;
    //        }
    //        else
    //        {
    //            chk.Checked = false;
    //        }
    //    }
    //}



    #endregion

    public void Clear_Session()
    {
        Session_Complaint_Analysis_Details = null;
        Session_Ds_Complaint_Analysis = null;        
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Clear_Session();           
        }

        _Complaint_AnalysisPresenter = new Complaint_AnalysisPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            _Complaint_AnalysisPresenter.Get_Details();

            if (Session_Complaint_Analysis_Details.Tables[0].Rows.Count > 0)
            {
                dg_Complaint_Analysis_Details.Columns[10].Visible = false;
                dg_Complaint_Analysis_Details.Columns[11].Visible = false;
                dg_Complaint_Analysis_Details.ShowFooter = false;

                btn_Save.Visible = false;
                txt_Action_Taken.Enabled = false;
                txt_Person_Responsible.Enabled = false;
            }
        }
        SetStandardCaption();
    }

    private void SetStandardCaption()
    {
        lbl_GC_Docket.Text = CompanyManager.getCompanyParam().GcCaption + " No. :";
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {        
        if (Allow_To_Save())
        {           
            _Complaint_AnalysisPresenter.Save();
        }         
    }
}
