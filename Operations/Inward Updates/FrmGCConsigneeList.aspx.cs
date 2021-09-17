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

public partial class Operations_Inward_Updates_FrmGCConsigneeList : ClassLibraryMVP.UI.Page, IGCConsigneeListView
{
    #region Declaration
    GCConsigneeListPresenter objGCConsigneeListPresenter;
    Common objCommon = new Common();
    string _GC_No_XML;
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

    public void BindDGgcConsigneeList()
    {
        dg_ConsigneeList.DataSource = SessionGConsigneeList;
        dg_ConsigneeList.DataBind();
    }

    public DataTable SessionGConsigneeList
    {
        get { return StateManager.GetState<DataTable>("BindConsigneeList"); }
        set
        {
            StateManager.SaveState("BindConsigneeList", value);
            if (StateManager.Exist("BindConsigneeList"))
                BindDGgcConsigneeList();
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
        set { _GC_No_XML = value; }
    }

    public int NewClientId
    {
        set {  }
        get { return 0; }
    }
    public int isRegularClient
    {
        set { }
        get { return 0; }
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

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = true;
        return _isValid;
    }

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
        objGCConsigneeListPresenter.fillgrid();
        if (SessionGConsigneeList.Rows.Count > 0)
        {
            WucSelectedItems1.dtdetails = SessionGConsigneeList;
            //tr_DlyBranchGrid.Style.Add("display", "inline");
            dg_ConsigneeList.Visible = true;
        }
        else
        {
            //tr_DlyBranchGrid.Style.Add("display", "none");
            dg_ConsigneeList.Visible = false;
        }
        up_ConsigneeList.Update();
        WucSelectedItems1.Get_Not_Selected_Items();
    }

    #endregion

    #region Event Click
    protected void Page_Load(object sender, EventArgs e)
    {
        objGCConsigneeListPresenter = new GCConsigneeListPresenter(this, IsPostBack);

        WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);

        if (!IsPostBack)
        {
            Wuc_Region_Area_Branch1.SetDDLBranchAutoPostback = true;
            objCommon.SetStandardCaptionForGrid(dg_ConsigneeList);
        }
        Wuc_Region_Area_Branch1.BranchIndexChange += new EventHandler(OnGetGCXML);

        SetStandardCaption();
        WucSelectedItems1.SetsmallTextboxWidth();
    }
    protected void dg_GCConsigneeList_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            LinkButton lnk_ConsigneeUpdate = (LinkButton)e.Item.FindControl("lnk_ConsigneeUpdate");
            string Consignee_Name = e.Item.Cells[2].Text;
            string GC_No = e.Item.Cells[0].Text;
            Label lbl_GC_ID = (Label)e.Item.FindControl("lbl_GC_ID");
            HiddenField hdn_gc_To_Location_ID = (HiddenField)e.Item.FindControl("hdn_gc_To_Location_ID");

            //int BranchID = Wuc_Region_Area_Branch1.BranchID;
            int BranchID = Convert.ToInt32(hdn_gc_To_Location_ID.Value);

            if (lnk_ConsigneeUpdate != null)
            {
                lnk_ConsigneeUpdate.Attributes.Add("onclick", "return viewwindow_general('" + lbl_GC_ID.Text + "','" + Consignee_Name + "','" + GC_No + "','" + BranchID + "')");
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
        //SessionGConsigneeList = null;
    }
}