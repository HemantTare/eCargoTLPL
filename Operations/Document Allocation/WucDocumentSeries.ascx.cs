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
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;

/// <summary>
/// Author        : Aashish Lad
/// Created On    : 21th October 2008
/// Description   : This is the Page For Operation Document Series
/// </summary>
/// 

public partial class Operations_Document_Allocation_WucDocumentSeries : System.Web.UI.UserControl, IDocumentSeriesView
{
    #region ClassVariables
    DocumentSeriesPresenter objDocumentSeriesPresenter;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    Label lbl_PrintedSeriesID;

    #endregion

    #region ControlsValue

    public int BranchID
    {
        get
        {
            if (Is_Multiple_hierarchy())
                return WucHierarchyWithID1.BranchID;
            else
                return Util.String2Int(ddl_Branch.SelectedValue);
        }

        set
        {
            if (Is_Multiple_hierarchy())
            {
                if (value > 0)
                    WucHierarchyWithID1.BranchID = value;
            }
        }
    }
    public int AreaID
    {
        get 
        {
            if (Is_Multiple_hierarchy())
                return WucHierarchyWithID1.AreaID;
            else
                return 0;
        }
        set
        {
            if (value > 0)
                WucHierarchyWithID1.AreaID = value;
        }
    }
    public int RegionID
    {
        get 
        {
            if (Is_Multiple_hierarchy())
                return WucHierarchyWithID1.RegionID;
            else
                return 0;
        }
        set 
        {
            if (value > 0)
            WucHierarchyWithID1.RegionID= value; 
        }
    }
    public bool Is_HO
    {
        get { return WucHierarchyWithID1.Is_Ho;}
        set
        {
            if (value == true)
                WucHierarchyWithID1.Is_Ho = value;
        }
    }

    public int DocumentTypeID
    {
        set{ ddl_DocumentType.SelectedValue = Util.Int2String(value);}
        get{ return Util.String2Int(ddl_DocumentType.SelectedValue);}
    }
    public int VAID
    {
        set {ddl_VA.SelectedValue = Util.Int2String(value);}
        get{ return Util.String2Int(ddl_VA.SelectedValue);}
    }

    public int PrintedSeriesID
    {
        set{hdn_PrintedSeriesID.Value = Util.Int2String(value); }
        get{return Util.String2Int(hdn_PrintedSeriesID.Value);}
    }
    public int MinStartNo
    {
        set{hdn_MinStartNo.Value = Util.Int2String(value); }
        get{return Util.String2Int(hdn_MinStartNo.Value); }
    }
    public int MaxEndNo
    {
        set{ hdn_MaxEndNo.Value = Util.Int2String(value); }
        get{ return Util.String2Int(hdn_MaxEndNo.Value); }
    }
    public int ParentStartNo
    {
        set{hdn_ParentStartNo.Value = Util.Int2String(value); }
        get {return Util.String2Int(hdn_ParentStartNo.Value); }
    }
    public int ParentEndNo
    {
        set{ hdn_ParentEndNo.Value = Util.Int2String(value); }
        get{return Util.String2Int(hdn_ParentEndNo.Value);}
    }
    public int StartNo
    {
        set {txt_StartNo.Text = Util.Int2String(value);}
        get{ return Util.String2Int(txt_StartNo.Text); }
    }
    public int EndNo
    {
        set { txt_EndNo.Text = Util.Int2String(value); }
        get {return Util.String2Int(txt_EndNo.Text); }
    }
    public int Balance
    {
        get { return (EndNo - StartNo) + 1; }
    }
    public DateTime DateofAllocation
    {
        set { WucDateofAllocation.SelectedDate = value;}
        get { return WucDateofAllocation.SelectedDate;}
    }

    #endregion

    #region ControlsBind
    public DataTable Bind_ddl_DocumentType
    {
        set
        {
            ddl_DocumentType.DataSource = value;
            ddl_DocumentType.DataTextField = "Document_Name";
            ddl_DocumentType.DataValueField = "Document_Id";
            ddl_DocumentType.DataBind();
            ddl_DocumentType.Items.Insert(0, new ListItem("----Select One----", "0"));
        }
    }
    public DataTable Bind_ddl_VA
    {
        set
        {
            ddl_VA.DataSource = value;
            ddl_VA.DataTextField = "VA_Name";
            ddl_VA.DataValueField = "VA_ID";
            ddl_VA.DataBind();            
        }
    }
    public DataTable Bind_dg_PrintedSeries
    {
        set
        {
            dg_PrintedSeries.DataSource = value;
            dg_PrintedSeries.DataBind();
        }
    }
    
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (DocumentTypeID == 0)
        {
            errorMessage = "Please Select Document Type";
            ddl_DocumentType.Focus();
        }
        else if (!Is_Multiple_hierarchy() && BranchID <= 0)
        {
            errorMessage = "Please Select Branch";
        }
        else if (Is_Multiple_hierarchy() && WucHierarchyWithID1.validateHierarchyWithIdUI(lbl_Errors) == false)
        {
            //errorMessage = "Please Select Location";
        }
        else if (StartNo <= 0)
        {
            errorMessage = "Please Enter Start No";
            txt_StartNo.Focus();
        }
        else if (EndNo <= 0)
        {
            errorMessage = "Please Enter End No";
            txt_EndNo.Focus();
        }
        else if (check_Rbtn_Selected() == false && keyID<=0)
        {
            errorMessage = "Please Select any one Printed Series";
        }
        else if (CheckStartNoEndNo() == false)
        {
            txt_StartNo.Focus();
            _isValid = false;
        }
        else if (objDocumentSeriesPresenter.CheckDuplicate() == true)
        {
            errorMessage = "Duplicate Series";
            txt_StartNo.Focus();
            return _isValid;
        }
        else if (CheckMinMaxNo() == false)
        {
            _isValid = false;
            txt_StartNo.Focus();
            return _isValid;
        }
        else
        {
            _isValid = true;
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
        get{ return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    #endregion

    #region OtherMethods
    private bool check_Rbtn_Selected()
    {
        bool rbtnchecked = false;
        RadioButton rdo_Series;
        int i;

        for (i = 0; i < dg_PrintedSeries.Items.Count; i++)
        {
            rdo_Series = (RadioButton)dg_PrintedSeries.Items[i].FindControl("rdo_Series");
            if (rdo_Series.Checked == true)
            {
                rbtnchecked = true;
                break;
            }
        }
        return rbtnchecked;
    }

    private bool CheckStartNoEndNo()
    {
        if (StartNo > EndNo)
        {
            errorMessage = "Start No Should be Less then End No";
            return false;
        }
        else if (StartNo < ParentStartNo || EndNo > ParentEndNo)
        {
            errorMessage = "Series Should be in between '" + ParentStartNo + "' To  '" + ParentEndNo + " '";
            return false;
        }
        return true;
    }
    private bool CheckMinMaxNo()
    {
        if (keyID > 0)
        {
            if (MinStartNo > 0 && MaxEndNo > 0 && ParentStartNo > 0 && ParentEndNo > 0)
            {
                if (StartNo > MinStartNo || EndNo < MaxEndNo || StartNo < ParentStartNo || EndNo > ParentEndNo )
                {
                    errorMessage = "Start No Should not be Less then or Equal to ' " + MinStartNo + "' And End No Should be Greater then '" + MaxEndNo + "' ";
                    return false;
                }
            }
        }
        return true;
    }
    public void SetBranchID(string Branch_Name, string BranchID)
    {
        ddl_Branch.DataTextField = "Branch_Name";
        ddl_Branch.DataValueField = "Branch_Id";
        Raj.EC.Common.SetValueToDDLSearch(Branch_Name, BranchID, ddl_Branch);
    }
    #endregion
    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        ddl_Branch.DataTextField = "Branch_Name";
        ddl_Branch.DataValueField = "Branch_Id";

        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));

        objDocumentSeriesPresenter = new DocumentSeriesPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            if (keyID > 0)
            {
                ddl_DocumentType.Enabled = false;
                dg_PrintedSeries.Columns[1].Visible = false;
                ddl_Branch.Enabled = false;
                WucHierarchyWithID1.SetEnabled = false;
            }
            else
            {
                WucHierarchyWithID1.Visible = false;
                tbl_Branch.Visible = false;

                txt_StartNo.MaxLength = Util.String2Int(ObjCommon.EC_Common_Pass_Query("select GC_No_Length from EC_Master_Company_GC_Parameter").Tables[0].Rows[0][0].ToString());
                txt_EndNo.MaxLength = txt_StartNo.MaxLength;
            }

            ddl_DocumentType_SelectedIndexChanged(sender, e);
        }
        tr_VA.Visible = false;
    }
    protected void ddl_DocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (keyID <= 0)
        {
            objDocumentSeriesPresenter.FillGrid();
            txt_StartNo.Text = "";
            txt_EndNo.Text = "";
            WucHierarchyWithID1.Set_Default_Values(sender, e);
        }

        if (Is_Multiple_hierarchy())  // 6 means Petrol Slip
        {
            tbl_Branch.Visible = false;
            WucHierarchyWithID1.Visible = true;
        }
        else
        {
            WucHierarchyWithID1.Visible = false;
            tbl_Branch.Visible = true;
        }
    }
    protected void rdo_Series_CheckedChanged(object sender, EventArgs e)
    {
        Label lbl_StartNo, lbl_EndNo;
        RadioButton rdo_Series, rdo_SeriesUnChecked;
        int i;
        rdo_Series = (RadioButton)sender;
        DataGridItem _item = (DataGridItem)rdo_Series.Parent.Parent;
        lbl_StartNo = (Label)_item.FindControl("lbl_StartNo");
        lbl_EndNo = (Label)_item.FindControl("lbl_EndNo");
        lbl_PrintedSeriesID = (Label)_item.FindControl("lbl_PrintedSeriesID");

        PrintedSeriesID = Util.String2Int(lbl_PrintedSeriesID.Text);

        txt_StartNo.Text = lbl_StartNo.Text;
        ParentStartNo = Util.String2Int(lbl_StartNo.Text);

        txt_EndNo.Text = lbl_EndNo.Text;
        ParentEndNo = Util.String2Int(lbl_EndNo.Text);

        for (i = 0; i < dg_PrintedSeries.Items.Count; i++)
        {
            rdo_SeriesUnChecked = (RadioButton)dg_PrintedSeries.Items[i].FindControl("rdo_Series");
            rdo_SeriesUnChecked.Checked = false;
        }
        rdo_Series.Checked = true;

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {       
         objDocumentSeriesPresenter.Save();      
    }

    protected void ddl_Branch_TxtChange(object sender, EventArgs e)
    {
        objDocumentSeriesPresenter.FillVA();
    }

    private bool Is_Multiple_hierarchy()
    {
        bool val = false;
        if (DocumentTypeID == 6 || DocumentTypeID == 7)
            val = true;

        return val;
    }
}   
    #endregion
