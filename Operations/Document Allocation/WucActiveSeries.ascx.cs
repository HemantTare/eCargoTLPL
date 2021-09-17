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
/// Created On    : 22th October 2008
/// Description   : This is the Page For Operation Active Series
/// </summary>
/// 
public partial class Operations_Document_Allocation_WucActiveSeries : System.Web.UI.UserControl, IActiveSeriesView
{
    #region ClassVariables
    ActiveSeriesPresenter objActiveSeriesPresenter;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    Label lbl_DocumentSeriesID;

    #endregion

    #region ControlsValue

    public int MainID
    {
        get{return WucHierarchyWithID1.MainId; }
        set { WucHierarchyWithID1.MainId = value; }
    }

    public string HierarchyCode
    {
        get { return WucHierarchyWithID1.HierarchyCode; }
        set { WucHierarchyWithID1.HierarchyCode = value; }
    }

    public int DocumentTypeID
    {
        set{ddl_DocumentType.SelectedValue = Util.Int2String(value);}
        get{return Util.String2Int(ddl_DocumentType.SelectedValue); }
    }

    public int VAID
    {
        set{ddl_VA.SelectedValue = Util.Int2String(value); }
        get{return Util.String2Int(ddl_VA.SelectedValue); }
    }

    public int DocumentSeriesID
    {
        set {hdn_DcumentSeriesID.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_DcumentSeriesID.Value);}
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
    public DataTable Bind_dg_DocumentSeries
    {
        set
        {
            dg_DocumentSeries.DataSource = value;
            dg_DocumentSeries.DataBind();
            SetActiveSeries(value);
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
        else if (WucHierarchyWithID1.validateHierarchyWithIdUI(lbl_Errors)== false)
        {
            //errorMessage = "Please Select Branch";
        }
        else if (dg_DocumentSeries.Items.Count <= 0)
        {
            errorMessage = "No document series allocated for selected Hierarchy,please allocate series";
        }
        else if (CheckActiveRadioButton() == false)
        {
            errorMessage = "please select series";
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    public string errorMessage
    {
        set{lbl_Errors.Text = value;}
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }

    #endregion
  
    #region OtherMethods
   
    private void SetActiveSeries(DataTable _dt)
    {
        int i;
        RadioButton rdo_SeriesUnChecked;
        for (i = 0; i < dg_DocumentSeries.Items.Count; i++)
        {
            rdo_SeriesUnChecked = (RadioButton)dg_DocumentSeries.Items[i].FindControl("rdo_Series");
            lbl_DocumentSeriesID = (Label)dg_DocumentSeries.Items[i].FindControl("lbl_DocumentSeriesID");
            if (Util.String2Bool(_dt.Rows[i]["Is_Active"].ToString()) == true)
            {
                rdo_SeriesUnChecked.Checked = true;
                DocumentSeriesID = Util.String2Int(_dt.Rows[i]["Document_Series_Allocation_ID"].ToString());
            }
        }
    }

    private bool CheckActiveRadioButton()
    {
        int i;
        bool check = false ;
        RadioButton rdo_SeriesUnChecked;
        for (i = 0; i < dg_DocumentSeries.Items.Count; i++)
        {
            rdo_SeriesUnChecked = (RadioButton)dg_DocumentSeries.Items[i].FindControl("rdo_Series");
            if (rdo_SeriesUnChecked.Checked == true)
            {
                check = true;
                break;
            }
        }
        return check;
    }

    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        WucHierarchyWithID1.DDLLocationChange = new EventHandler(ddl_Branch_TxtChange);
        WucHierarchyWithID1.DDLHierarchyChange = new EventHandler(ddl_Branch_TxtChange);

        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));
        WucHierarchyWithID1.setDDLLocationAutoPostBack = true;

        objActiveSeriesPresenter = new ActiveSeriesPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            if (keyID > 0)
            {
                ddl_DocumentType.Enabled = false;
            }
        }
        tr_VA.Visible = false;
    }
    protected void ddl_DocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objActiveSeriesPresenter.FillVA();
        objActiveSeriesPresenter.FillGrid();
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
        lbl_DocumentSeriesID = (Label)_item.FindControl("lbl_DocumentSeriesID");

        DocumentSeriesID = Util.String2Int(lbl_DocumentSeriesID.Text);

        for (i = 0; i < dg_DocumentSeries.Items.Count; i++)
        {
            rdo_SeriesUnChecked = (RadioButton)dg_DocumentSeries.Items[i].FindControl("rdo_Series");
            rdo_SeriesUnChecked.Checked = false;
        }
        rdo_Series.Checked = true;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objActiveSeriesPresenter.Save();
    }

    public void ddl_Branch_TxtChange(object sender, EventArgs e)
    {
        objActiveSeriesPresenter.FillVA();
        objActiveSeriesPresenter.FillGrid();
    }
    #endregion
}