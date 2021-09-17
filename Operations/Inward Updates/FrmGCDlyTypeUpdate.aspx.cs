using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
using Raj.EC;

/// <summary>
/// Author : Ankit champaneriya
/// Date   : 07-01-09
/// </summary>
/// 

public partial class Operations_Inward_Updates_FrmGCDlyTypeUpdate : ClassLibraryMVP.UI.Page, IGCDlyTypeUpdateView
{
    #region Declaration
    GCDlyTypeUpdatePresenter objGCDlyTypeUpdatePresenter;
    Common objCommon = new Common();
    string _GC_No_XML;
    DropDownList ddl_DeliveryType;
    Label lbl_GC_ID;
    TextBox txtReason;
    DataTable objDT = new DataTable();
    int ds_index, i;

    string Crypt;

    #endregion

    #region Properties

    public int GC_ID
    {
        get { return 0; }
    }
    public int BranchId
    {
        get { return Wuc_Region_Area_Branch1.BranchID; }
    }

    public void BindDGGCDlyTypeUpdate()
    {
        dg_GCDlyTypeUpdate.DataSource = SessionGCDlyTypeUpdate;
        dg_GCDlyTypeUpdate.DataBind();
    }

    public DataTable SessionGCDlyTypeUpdate
    {
        get { return StateManager.GetState<DataTable>("BindGCDlyTypeUpdate"); }
        set
        {
            StateManager.SaveState("BindGCDlyTypeUpdate", value);
            if (StateManager.Exist("BindGCDlyTypeUpdate"))
                BindDGGCDlyTypeUpdate();
        }
    }

    public String GetBranchXML
    {
        get
        {
            if (_GC_No_XML != null)
            {
                return _GC_No_XML.ToString().ToLower();
            }
            else
            {
                return "<NewDataSet/>";
            }
        }
        set { string _GC_No_XML = value; }
    }
    public String GCDlyTypeUpdateXML
    {
        get
        {
            DataSet _objDs = new DataSet();
 
            _objDs.Tables.Add(SessionGCDlyTypeUpdate.Copy());

            _objDs.Tables[0].TableName = "GCDlyTypeUpdate";
            return _objDs.GetXml().ToLower();
        }
    }

    public string ClientId
    {
        get { return ""; }
    }

    public string ClientName
    {
        get { return ""; }
    }
    public string ContactPerson
    {
        set { }
    }

    public string Add1
    {
        get { return ""; }
        set { }
    }
    public string Add2
    {
        get { return ""; }
        set { }
    }

    public string pincode
    {
        get { return ""; }
        set { }
    }
    public string stdcode
    {
        get { return ""; }
        set { }
    }
    public string phone
    {
        get { return ""; }
        set { }
    }
    public string mobile
    {
        get { return ""; }
        set { }
    }
    public string csttinno
    {
        get { return ""; }
        set { }
    }
    public string servicetaxNo
    {
        get { return ""; }
        set { }
    }

    public string Reason
    {
        get { return ""; }
        set { }
    }
    public DataTable SessionDeliveryType
    {
        get { return StateManager.GetState<DataTable>("DeliveryType"); }
        set { StateManager.SaveState("DeliveryType", value); }
    }

    public void BindDeliveryType()
    {
        ddl_DeliveryType.DataSource = SessionDeliveryType;
        ddl_DeliveryType.DataTextField = "Delivery_Type";
        ddl_DeliveryType.DataValueField = "Delivery_Type_ID";
        ddl_DeliveryType.DataBind();
    }
    #endregion

    #region IView
  
    public string errorMessage
    {
        set { lbl_Error.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    #endregion

    #region other Method
    private void SetStandardCaption()
    {
        WucSelectedItems1.SetFoundCaption = "Enter  " + CompanyManager.getCompanyParam().GcCaption + "  Nos.:";
        WucSelectedItems1.SetNotFoundCaption = CompanyManager.getCompanyParam().GcCaption + "  Nos.Not Found :";
        WucSelectedItems1.Set_GCCaption = CompanyManager.getCompanyParam().GcCaption;
    }

    private void OnGetGCXML(object sender, EventArgs e)
    {
        _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
        objGCDlyTypeUpdatePresenter.fillgrid();
        if (SessionGCDlyTypeUpdate.Rows.Count > 0)
        {
            WucSelectedItems1.dtdetails = SessionGCDlyTypeUpdate;
            //tr_DlyBranchGrid.Style.Add("display", "inline");
            dg_GCDlyTypeUpdate.Visible = true;
        }
        else
        {
            //tr_DlyBranchGrid.Style.Add("display", "none");
            dg_GCDlyTypeUpdate.Visible = false;
        }
        up_GCDlyTypeUpdate.Update();
        WucSelectedItems1.Get_Not_Selected_Items();
    }

    #endregion

    #region Event Click
    protected void Page_Load(object sender, EventArgs e)
    {
        objGCDlyTypeUpdatePresenter = new GCDlyTypeUpdatePresenter(this, IsPostBack);

        WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);

        if (!IsPostBack)
        {
            Wuc_Region_Area_Branch1.SetDDLBranchAutoPostback = true;
            objCommon.SetStandardCaptionForGrid(dg_GCDlyTypeUpdate);

            string GC_Nos;

            Crypt = Request.QueryString["Call_From"];

            if (Crypt == "GDC")
            {
                Crypt = Request.QueryString["GC_Nos"];
                GC_Nos = ClassLibraryMVP.Util.DecryptToString(Crypt);

                TextBox txtBox = (TextBox)WucSelectedItems1.FindControl("txt_get_item");
                txtBox.Text = GC_Nos;
            }

        }
        Wuc_Region_Area_Branch1.BranchIndexChange += new EventHandler(OnGetGCXML);

        SetStandardCaption();
        WucSelectedItems1.SetsmallTextboxWidth();
    }
    protected void dg_GCDlyTypeUpdate_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton lnk_ConsigneeUpdate = (LinkButton)e.Item.FindControl("lnk_ConsigneeUpdate");
        string Consignee_Name = e.Item.Cells[2].Text;
        string GC_No = e.Item.Cells[0].Text;
        Label lbl_GC_ID = (Label)e.Item.FindControl("lbl_GC_ID");
        ddl_DeliveryType = (DropDownList)(e.Item.FindControl("ddl_DeliveryType"));
 
        if (e.Item.ItemIndex != -1)
        {
            BindDeliveryType();

            Crypt = Request.QueryString["Call_From"];

            if (Crypt == "GDC")
            {
                ddl_DeliveryType.SelectedIndex = 1;
                ddl_DeliveryType.SelectedValue = "Godown";
            }
            else
            {
                ddl_DeliveryType.SelectedValue = SessionGCDlyTypeUpdate.Rows[e.Item.ItemIndex]["Delivery_Type_Id"].ToString();
            }
           
        }
    }

    #endregion

    protected void btn_update_grid_Click(object sender, EventArgs e)
    {
        OnGetGCXML(sender, e);
    }

    public void ClearVariables() // added Ankit
    {
        //SessionGCDlyTypeUpdate = null;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Update_griddetails();
            objGCDlyTypeUpdatePresenter.Save();
            ////////up_GCDlyTypeUpdate.Update();
        }
    }
    private void Update_griddetails()
    {

        if (dg_GCDlyTypeUpdate.Items.Count > 0)
        {
            objDT = SessionGCDlyTypeUpdate;

            for (i = 0; i <= dg_GCDlyTypeUpdate.Items.Count - 1; i++)
            {

                lbl_GC_ID = (Label)dg_GCDlyTypeUpdate.Items[i].FindControl("lbl_GC_ID");

                ddl_DeliveryType = (DropDownList)dg_GCDlyTypeUpdate.Items[i].FindControl("ddl_DeliveryType");
                txtReason = (TextBox)dg_GCDlyTypeUpdate.Items[i].FindControl("txtReason");

                objDT.Rows[i]["GC_ID"] = Util.String2Int(lbl_GC_ID.Text);

                objDT.Rows[i]["NewDelivery_Type_Id"] = Util.String2Int(ddl_DeliveryType.SelectedValue);
                
                objDT.Rows[i]["Reason"] = txtReason.Text; 
            }
        }
    }

    public bool validateUI()
    {
        bool _isValid = false;
        if (grid_validation() == false)
        {
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    private bool grid_validation()
    {
        bool ATS = true;
        string GC_No;

        objDT = SessionGCDlyTypeUpdate;

        if (objDT.Rows.Count > 0)
        {

            for (i = 0; i <= dg_GCDlyTypeUpdate.Items.Count - 1; i++)
            {
                ddl_DeliveryType = (DropDownList)dg_GCDlyTypeUpdate.Items[i].FindControl("ddl_DeliveryType");
                GC_No = dg_GCDlyTypeUpdate.Items[i].Cells[1].Text;

                if (dg_GCDlyTypeUpdate.Items.Count <= 0)
                {
                    errorMessage = "Please Enter Valid Data To Change Delivery Type ";
                    ATS = false;
                    break;
                }
                else if (Util.String2Int(ddl_DeliveryType.SelectedValue) <= 0)
                {
                    errorMessage = "Please Select New Delivery Type For " + objDT.Rows[i]["gc_caption no"].ToString();
                    scm_GCDlyTypeUpdate.SetFocus(ddl_DeliveryType);
                    ATS = false;
                    break;
                }
                else
                {
                    ATS = true;
                }
            }
        }
        else
        {
            ATS = false; 
        }

         
        return ATS;
    }
}