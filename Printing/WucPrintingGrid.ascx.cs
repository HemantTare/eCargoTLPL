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
using System.Text;
using ClassLibraryMVP.Security;
using Raj.EC.Security;
using ClassLibraryMVP;
using Raj.EC;

public partial class Printing_WucPrintingGrid : System.Web.UI.UserControl
{
    DataSet objDS = null;
    Common objCommon = new Common();
    private int _count;

    private int GetMenuHeadId
    {
        get { return StateManager.GetState<int>("MenuHeadId"); }
    }
    private int MenuItemId
    {
        get { return Util.String2Int(ddl_DocumentType.SelectedValue); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Search.SetDateInSession += new EventHandler(SetDateInSession);
        Search.SearchClicked += new EventHandler(EventBindGrid);

        if (!Page.IsPostBack)
        {
            Session["objDS"] = null;
            SetDateInSession(sender, e);
            Search.Visible = false;
            Fill_DocumentType();

            hdn_Sort_Dir.Value = "ASC";
            hdn_Sort_Expression.Value = "Col1";
            Link.text = Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).Link.ToUpper();
        }
    }

    private void SetDateInSession(object source, EventArgs e)
    {
        StateManager.SaveState("FromDate", PickerFrom.SelectedDate);
        StateManager.SaveState("ToDate", PickerTo.SelectedDate);
        TimeSpan TS = PickerTo.SelectedDate - PickerFrom.SelectedDate;
        double A = TS.TotalDays;
    }

    private void BindGrid()
    {
        objDS = StateManager.GetState<DataSet>("objDS");

        if (objDS != null)
        {
            if (objDS.Tables[0].Columns.Count > 1)
            {
                lbl_Errors.Text = "";
                DataView objDV = new DataView(objDS.Tables[0]);
                objDV.Sort = hdn_Sort_Expression.Value + " " + hdn_Sort_Dir.Value;

                dg_PrintingGrid.DataSource = objDV;
                dg_PrintingGrid.DataBind();
            }
            else
            {
                lbl_Errors.Text = objDS.Tables[0].Rows[0][0].ToString();
            }
        }
    }

    private void Fill_DocumentType()
    {
        objDS = objCommon.Get_Values_Where("EC_Printing", "MenuItem_ID,Document_Name", "Is_Active = 1 and Module_Type = " + GetMenuHeadId.ToString(), "Serial_No", true);

        ddl_DocumentType.DataSource = objDS;
        ddl_DocumentType.DataTextField = "Document_Name";
        ddl_DocumentType.DataValueField = "MenuItem_ID";
        ddl_DocumentType.DataBind();
        ddl_DocumentType.Items.Insert(0, new ListItem("--- Select Document ---", "0"));
    }

    protected void ddl_DocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int menuitem = Util.String2Int(ddl_DocumentType.SelectedValue);
        Session["objDS"] = null;
        SetDateInSession(sender, e);

        if (menuitem > 0)
        {
            Search.FillCombo(menuitem);
            Search.FillOprGridHeaders(dg_PrintingGrid);
            Search.Visible = true;

            Search.FillGrid(menuitem);
            Search.GetMenuItemForPrint = menuitem.ToString();
            EventBindGrid(sender, e);
        }
        ddl_DocumentType.Items.Remove(new ListItem("--- Select Document ---", "0"));
    }

    private void EventBindGrid(object source, EventArgs e)
    {
        if (PickerFrom.SelectedDate < UserManager.getUserParam().StartDate || PickerTo.SelectedDate > UserManager.getUserParam().EndDate)
        {
            lbl_Errors.Text = "Please Select Date Range in Current financial Year";
        }
        else if (PickerFrom.SelectedDate > PickerTo.SelectedDate)
        {
            lbl_Errors.Text = "From Date Should Not Be Greater Than To Date";
        }
        else
        {
            dg_PrintingGrid.CurrentPageIndex = 0;
            BindGrid();
        }
    }
    protected void Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_PrintingGrid.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void Grid_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        if (Session["objDS"] != null)
        {
            hdn_Sort_Expression.Value = e.SortExpression;

            if (hdn_Sort_Dir.Value == "DESC")
            {
                hdn_Sort_Dir.Value = "ASC";
            }
            else
            {
                hdn_Sort_Dir.Value = "DESC";
            }
            BindGrid();
        }
    }

    protected void Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        UserRights uObj;
        uObj = StateManager.GetState<UserRights>("UserRights");
        FormRights fRights;
        fRights = uObj.getForm_Rights(MenuItemId);
        bool can_view = fRights.canRead();

        int Id;
        LinkButton lnk_Btn, lnk_Print;
        HiddenField hdn_Path;

        if (Session["objDS"] == null)
        {
            _count = 0;
        }
        else
        {
            _count = StateManager.GetState<DataSet>("objDS").Tables[0].Rows.Count;
        }
        if (_count == 0)
        {
            Lbl_Total_Records.Text = "";
        }
        else
        {
            Lbl_Total_Records.Text = _count.ToString() + " Record(s) Found";
        }
        if (e.Item.ItemIndex != -1)
        {
            e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER';");

            if (e.Item.ItemType == ListItemType.Item)
            {
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUT';");
            }
            else
            {
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTALT';");
            }

            Id = Convert.ToInt32(((Label)e.Item.FindControl("lbl_Id")).Text);
            lnk_Btn = (LinkButton)(e.Item.FindControl("lbl_Name"));
            lnk_Print = (LinkButton)(e.Item.FindControl("lnk_Print"));
            hdn_Path = (HiddenField)(e.Item.FindControl("hdn_Path"));

            if (can_view == true)
            {
                StateManager.SaveState("QueryString", Rights.GetObject().GetLinkDetails(MenuItemId).QueryString);
                hdn_Path.Value = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(MenuItemId).ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id);
            }
            else
            {
                hdn_Path.Value = "";
            }
            lnk_Btn.Attributes.Add("onclick", "return Open_View_Window('" + hdn_Path.Value + "');");

            string SpecialBillFormat;

            SpecialBillFormat = "0";

            if (MenuItemId == 143)
            {
                SpecialBillFormat = StateManager.GetState<DataSet>("objDS").Tables[0].Rows[e.Item.ItemIndex]["Col10"].ToString();
            }
            else
            {
                SpecialBillFormat = "0";
            }


            String PrintPath = Util.GetBaseURL() + "/Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Id) + "&AUSTYPE=" + "" + "&SpecialBillFormat=" + SpecialBillFormat;
            lnk_Print.Attributes.Add("onclick", "return Open_Print_Window('" + PrintPath + "');");
        }
    }
}
