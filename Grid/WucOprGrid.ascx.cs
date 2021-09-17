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
using System.Text.RegularExpressions;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC;
using System.IO;


public partial class Grid_WucOprGrid : System.Web.UI.UserControl
{
    DataSet objDs = null;
    Common objCommon = new Common();
    int MenuItemId;
    private int _count;
    DateTime CurrentDateTime = DateTime.Now;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        MenuItemId = Common.GetMenuItemId();
        
        hdn_MenuItemId.Value = MenuItemId.ToString();

        String scripts;
        scripts = "<script type='text/javascript' language='javascript'>" +
                    " On_Load(); " +
                    "</script>";


        ScriptManager.RegisterStartupScript(Page, typeof(System.String), "ss", scripts, false);


        Search.SetDateInSession += new EventHandler(SetDateInSession);
        Search.SearchClicked += new EventHandler(EventBindGrid);
       
        if (!IsPostBack)
        {
            SetDateInSession(sender, e);    
            hdn_Sort_Dir.Value = "DESC";
            hdn_Sort_Expression.Value = "Col1";

            Session["objDs"] = null;
            
            Search.FillCombo(MenuItemId);
            Search.FillOprGridHeaders(dg_Grid);

                        
            RedirectOnAddClick();
            StateManager.SaveState("QueryString", Rights.GetObject().GetLinkDetails(MenuItemId).QueryString);



            Search.FillGrid(MenuItemId);
            EventBindGrid(sender, e);
        }
        Link.text = Rights.GetObject().GetLinkDetails(MenuItemId).Link.ToUpper();
        Set_User_Rights();
        ForceRights();
    }
    protected void btn_ExportToExcel_Click(object sender, EventArgs e)
    {
        //if (UserManager.getUserParam().HierarchyCode == "HO")
        //{
            if (dg_Grid.Items.Count > 0)
            {
                objDs = (DataSet)Session["objDs"];

                DataTable Dt = new DataTable();

                DataView dv = new DataView(objDs.Tables[0]);

                dv.Sort = hdn_Sort_Expression.Value + " " + hdn_Sort_Dir.Value;
                Dt = dv.Table.Copy();
                int i = Dt.Columns.Count - 1;
                foreach (DataColumn dc in Dt.Columns)
                {
                    if (i == 0) break;
                    if (i <= dg_Grid.Columns.Count )
                    {
                        if (dg_Grid.Columns[dc.Ordinal].Visible)
                        {
                            dc.Caption = dg_Grid.Columns[dc.Ordinal].HeaderText;
                        }
                        
                    }
                    i--;
                    
                }


                for (int j = 10; j < Dt.Columns.Count; j++)
                {

                    //if (Dt.Columns[j].Ordinal > 9)
                    //{
                    //Dt.Columns.RemoveAt(j);
                    //}
                    if (j == (Dt.Columns.Count-1) ) break;
                    Dt.Columns.Remove(Dt.Columns[j].ColumnName);
                }
                Dt.Columns.Remove("Col1");
                if (Dt.Columns.Contains("Can_Edit_Cancel"))
                {
                    Dt.Columns.Remove("Can_Edit_Cancel");
                }
                if (Dt.Columns.Contains("View"))
                {
                    Dt.Columns.Remove("View");
                }
                else if (Dt.Columns.Contains("Edit"))
                {
                    Dt.Columns.Remove("Edit");
                }
                else if (Dt.Columns.Contains("Is_Attached"))
                {
                    Dt.Columns.Remove("Is_Attached");
                }

                Session["ExportToExcel"] = Dt;
                //Response.Redirect("~/Finance/Utilities/FrmBankStatementExportToExcel.aspx");

                Response.Redirect("~/Finance/Utilities/frm_Infra_Grid_Common_ExportToExcel.aspx");
            }
        //}
    }
    

    private void ForceRights()
    {
        //pankaj 17 nov as some forms required rights externally
        bool can_add = true;
        bool can_edit = true;
        bool can_cancel = true;
        bool can_read = true;

        objCommon.ForceRights(ref can_read, ref can_add, ref can_edit, ref can_cancel);

        if (can_read == false)
            dg_Grid.Columns[10].Visible = false;

        if (can_add == false)
            btn_Add.Visible = false;

        if (can_edit == false)
            dg_Grid.Columns[11].Visible = false;

        if (can_cancel == false)
            dg_Grid.Columns[12].Visible = false;
        //pankaj 17 nov as some forms required rights externally
    }

    private void SetDateInSession(object source, EventArgs e)
    {
        StateManager.SaveState("FromDate", PickerFrom.SelectedDate);
        StateManager.SaveState("ToDate", PickerTo.SelectedDate);
        TimeSpan TS = PickerTo.SelectedDate - PickerFrom.SelectedDate;
        double A=TS.TotalDays;
        
    }
 
    private void RedirectOnAddClick()
    {
        //StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        //Path.Append("/");
        //Path.Append(Rights.GetObject().GetLinkDetails(MenuItemId).AddUrl);
 
        //Boolean AllowBooking = UserManager.getUserParam().AllowBooking;

        //if (MenuItemId == 30 && AllowBooking == false)
        //{
        //    //btn_Add.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Add));

        //    btn_Add.Enabled = false;
            

        //    String popupScript = "<script language='javascript'>BookingClosed_Window();</script>";

        //    Page.ClientScript.RegisterStartupScript(typeof(String), "PopupScript1", popupScript.ToString(), false);

        //}
        //else
        //{
        //    btn_Add.Attributes.Add("onclick", "return Open_Add_Window('" + Path + "','" + MenuItemId.ToString() + "')");
        //}

        ////btn_Add.Attributes.Add("onclick", "return Open_Add_Window('" + Path + "','" + MenuItemId.ToString() + "')");


        ///Start Added On 26 Jul 2021 For php Pages By Comenting Above
        string Path = Util.GetBaseURL();
        Path += "/";
        Path += Rights.GetObject().GetLinkDetails(MenuItemId).AddUrl;
        //Path.Append("/");
        //Path.Append(Rights.GetObject().GetLinkDetails(MenuItemId).AddUrl);

        if (Path.ToString().Contains(".php"))
        {
            string[] pathArray = Path.Split(new char[] { '/' });
            Path = pathArray[0] + "//" + pathArray[2] + "/";
            Path += Rights.GetObject().GetLinkDetails(MenuItemId).AddUrl + "&function=1&user=" + UserManager.getUserParam().UserId + "&HierarchyCode=" + UserManager.getUserParam().HierarchyCode + "&MainId=" + UserManager.getUserParam().MainId + "&YearCode=" + UserManager.getUserParam().YearCode;
        }

        Boolean AllowBooking = UserManager.getUserParam().AllowBooking;

        if (MenuItemId == 30 && AllowBooking == false)
        {
            //btn_Add.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Add));

            btn_Add.Enabled = false;


            String popupScript = "<script language='javascript'>BookingClosed_Window();</script>";

            Page.ClientScript.RegisterStartupScript(typeof(String), "PopupScript1", popupScript.ToString(), false);

        }
        else
        {
            btn_Add.Attributes.Add("onclick", "return Open_Add_Window('" + Path + "','" + MenuItemId.ToString() + "')");
        }

        //btn_Add.Attributes.Add("onclick", "return Open_Add_Window('" + Path + "','" + MenuItemId.ToString() + "')");

        //End Added On 26 Jul 2021 For php Pages
    
    }

    private void Set_User_Rights()
    {
            btn_Add.Visible = Convert.ToBoolean(Rights.GetObject().getForm_Rights(MenuItemId).canAdd());
            dg_Grid.Columns[10].Visible = Convert.ToBoolean(Rights.GetObject().getForm_Rights(MenuItemId).canRead());
            dg_Grid.Columns[11].Visible = Convert.ToBoolean(Rights.GetObject().getForm_Rights(MenuItemId).canEdit());
            dg_Grid.Columns[12].Visible = Convert.ToBoolean(Rights.GetObject().getForm_Rights(MenuItemId).canDelete());

            if (MenuItemId == 136)//for Pickup status
            {
                dg_Grid.Columns[11].Visible = false;
                dg_Grid.Columns[12].Visible = false;
                btn_Add.Visible = false;
            }
            else if (MenuItemId == 269)//for Pickup status
            {
                dg_Grid.Columns[12].Visible = false;
                btn_Add.Visible = false;
            }

    }

    private void BindGrid()
    {
        objDs = (DataSet)Session["objDs"];

        
        if (objDs != null)
        {

            if (objDs.Tables[0].Columns.Count > 1)
            {
                lbl_Errors.Text = "";        
                DataView dv = new DataView(objDs.Tables[0]);

                dv.Sort = hdn_Sort_Expression.Value + " " + hdn_Sort_Dir.Value;

                dg_Grid.DataSource = dv;
                dg_Grid.DataBind();
                SetColumnsDatatype();

                if (MenuItemId == 82)
                {
                    pnl_Total.Visible = true;
                    Session["objdv"] = dv;
                    SetTotal(dv);
                }
                else
                {
                    pnl_Total.Visible = false; 
                }
            }
            else
            {
                lbl_Errors.Text = objDs.Tables[0].Rows[0][0].ToString();
            }
        }
    }

    public void SetTotal(object sender)
    {
        
        DataView View = (DataView)Session["objdv"];
        Decimal Total_Local_Tempo_Freight = 0; 
 
        if (sender == View)
        {
            foreach (DataRow DR in View.ToTable().Rows)
            {
                Total_Local_Tempo_Freight = Total_Local_Tempo_Freight + Convert.ToDecimal(DR["Col8"]);
                
            }
        }
        if (Total_Local_Tempo_Freight > 0)
        {
            lbl_Total.Visible = true;
            lbl_Total.Text = String.Format(Math.Abs(Total_Local_Tempo_Freight).ToString(), "0.00");// +" " + "Cr";
        }
        else
        {
            lbl_Total.Text = String.Format(Math.Abs(Total_Local_Tempo_Freight).ToString(), "0.00");// +" " + "Dr";
            lbl_Total.Visible = false;
        }
         

    }

    private void EventBindGrid(object source, EventArgs e)
    {
        //  Common.GetMenuItemId() = 200 for opening gc 

        if (Common.GetMenuItemId() != 200 &&( PickerFrom.SelectedDate < UserManager.getUserParam().StartDate
            || PickerTo.SelectedDate > UserManager.getUserParam().EndDate))
        {
            lbl_Errors.Text = "Please Select Date Range in Current financial Year";
        }
        else if (PickerFrom.SelectedDate > PickerTo.SelectedDate)
        {
            lbl_Errors.Text = "From Date Should Not Be Greater Than To Date";
        }
        else
        {
            dg_Grid.CurrentPageIndex = 0;
            BindGrid();
        }
    }

    private void SetColumnsDatatype()
    {
        int i;

        objDs = (DataSet)Session["objDs"];

        for (i = 0; i <= objDs.Tables[0].Columns.Count - 1; i++)
        {
            if ((objDs.Tables[0].Columns[i].DataType == typeof(int) || objDs.Tables[0].Columns[i].DataType == typeof(decimal)))
            {
                dg_Grid.Columns[i].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                dg_Grid.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            }

            //dg_Grid.Columns[i + 1].HeaderText = ddl_Search.Items[i].Text;
        }
    }

    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void dg_Grid_SortCommand(object source, DataGridSortCommandEventArgs e)
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

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        int Id;
        LinkButton lnk_Btn;
        Boolean AllowBooking = UserManager.getUserParam().AllowBooking;

        if (Session["objDs"] == null)
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

            MenuItemId = Common.GetMenuItemId();

            if (MenuItemId == 30 || MenuItemId == 213 || MenuItemId == 188)// for 30 GC Only and 188 IBA GC
            {
                if (Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Status_id").ToString()) == 80) // 'Reserved GCs
                {
                    e.Item.CssClass = "NOTUPDATEDLBL";
                }
                else if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Is_ReBooked")) == true) // 'ReBook GCs
                {                    
                    e.Item.CssClass = "REBOOK";
                }
                else if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Is_Attached")) == true) // 'ReBook GCs
                {
                    e.Item.CssClass = "ATTACHED";
                }
            }
            else if (MenuItemId == 278)// for Truck Bharai
            {
                if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Is_Updated")) == true) // 'Reserved GCs
                {
                    e.Item.CssClass = "NOTUPDATEDLBL";
                }
            }

            if (e.Item.ItemType == ListItemType.Item)
            {
                if (MenuItemId == 30 || MenuItemId == 213 || MenuItemId == 188) // for 30 GC Only and 188 IBA GC
                {
                    if (Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Status_id").ToString()) == 80) // 'Reserved GCs
                    {
                        e.Item.Attributes.Add("onmouseout", "this.className='NOTUPDATEDLBL';");
                    }
                    else if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Is_ReBooked")) == true) // 'ReBook GCs
                    {
                        e.Item.Attributes.Add("onmouseout", "this.className='REBOOK';");
                    }
                    else if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Is_Attached")) == true) // 'ReBook GCs
                    {
                        e.Item.Attributes.Add("onmouseout", "this.className='ATTACHED';");
                    }
                    else
                    {
                        e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUT';");
                    }
                }
                else if (MenuItemId == 278)// for Truck Bharai
                {
                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Is_Updated")) == true)
                    {
                        e.Item.Attributes.Add("onmouseout", "this.className='NOTUPDATEDLBL';");
                    }
                }
                else
                {
                    e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUT';");
                }
            }
            else
            {
                if (MenuItemId == 30 || MenuItemId == 213 || MenuItemId == 188) // for 30 GC Only and 188 IBA GC
                {
                    if (Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Status_id").ToString()) == 80) // 'Reserved GCs
                    {
                        e.Item.Attributes.Add("onmouseout", "this.className='NOTUPDATEDLBL';");
                    }
                    else if (Convert.ToBoolean  (DataBinder.Eval(e.Item.DataItem, "Is_ReBooked")) == true) // 'ReBook GCs
                    {
                        e.Item.Attributes.Add("onmouseout", "this.className='REBOOK';");
                    }
                    else if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Is_Attached")) == true) // 'ReBook GCs
                    {
                        e.Item.Attributes.Add("onmouseout", "this.className='ATTACHED';");
                    }
                    else
                    {
                        e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTALT';");
                    }
                }
                else if (MenuItemId == 278)// for Truck Bharai
                {
                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Is_Updated")) == true)
                    {
                        e.Item.Attributes.Add("onmouseout", "this.className='NOTUPDATEDLBL';");
                    }
                }                
                else
                {
                    e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUT';");
                }
                             
            }


            Id = Convert.ToInt32(((Label)e.Item.FindControl("lbl_Id")).Text);
            lnk_Btn = ((LinkButton)e.Item.FindControl("lnk_Edit"));

            StringBuilder EditPath = new StringBuilder(Util.GetBaseURL());
            EditPath.Append("/");
            EditPath.Append(Rights.GetObject().GetLinkDetails(MenuItemId).EditUrl);
            EditPath.Append("&Id=");
            EditPath.Append(ClassLibraryMVP.Util.EncryptInteger(Id));

//            lnk_Btn.Attributes.Add("onclick", "return Open_Edit_Window('" + EditPath + "','" + MenuItemId.ToString() + "')");

            string EditPathPHP;
            if (EditPath.ToString().Contains(".php"))
            {
                string[] pathArray = EditPath.ToString().Split(new char[] { '/' });
                EditPathPHP = pathArray[0] + "//" + pathArray[2] + "/";
                //EditPathPHP += Rights.GetObject().GetLinkDetails(MenuItemId).EditUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id) + "&function=3&user=" + UserManager.getUserParam().UserId + "&HierarchyCode=" + UserManager.getUserParam().HierarchyCode + "&MainId=" + UserManager.getUserParam().MainId + "&YearCode=" + UserManager.getUserParam().YearCode;
                EditPathPHP += Rights.GetObject().GetLinkDetails(MenuItemId).EditUrl + "&Id=" + Id + "&function=3&user=" + UserManager.getUserParam().UserId + "&HierarchyCode=" + UserManager.getUserParam().HierarchyCode + "&MainId=" + UserManager.getUserParam().MainId + "&YearCode=" + UserManager.getUserParam().YearCode;
                lnk_Btn.Attributes.Add("onclick", "return Open_Edit_Window('" + EditPathPHP + "','" + MenuItemId.ToString() + "')");
            }
            else
            {
                lnk_Btn.Attributes.Add("onclick", "return Open_Edit_Window('" + EditPath + "','" + MenuItemId.ToString() + "')");
            }


            lnk_Btn = ((LinkButton)e.Item.FindControl("lnk_View"));
            StringBuilder ViewPath = new StringBuilder(Util.GetBaseURL());
            ViewPath.Append("/");
            ViewPath.Append(Rights.GetObject().GetLinkDetails(MenuItemId).ViewUrl);
            ViewPath.Append("&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id));

//            lnk_Btn.Attributes.Add("onclick", "return Open_View_Window('" + ViewPath + "','" + MenuItemId.ToString() + "')");

            string ViewPathPHP;
            if (ViewPath.ToString().Contains(".php"))
            {
                string[] pathArray = ViewPath.ToString().Split(new char[] { '/' });
                ViewPathPHP = pathArray[0] + "//" + pathArray[2] + "/";
                //ViewPathPHP += Rights.GetObject().GetLinkDetails(MenuItemId).ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id) + "&function=2&user=" + UserManager.getUserParam().UserId + "&HierarchyCode=" + UserManager.getUserParam().HierarchyCode + "&MainId=" + UserManager.getUserParam().MainId + "&YearCode=" + UserManager.getUserParam().YearCode;
                ViewPathPHP += Rights.GetObject().GetLinkDetails(MenuItemId).ViewUrl + "&Id=" + Id + "&function=2&user=" + UserManager.getUserParam().UserId + "&HierarchyCode=" + UserManager.getUserParam().HierarchyCode + "&MainId=" + UserManager.getUserParam().MainId + "&YearCode=" + UserManager.getUserParam().YearCode;
                lnk_Btn.Attributes.Add("onclick", "return Open_View_Window('" + ViewPathPHP + "','" + MenuItemId.ToString() + "')");
            }
            else
            {
                lnk_Btn.Attributes.Add("onclick", "return Open_View_Window('" + ViewPath + "','" + MenuItemId.ToString() + "')");
            }

            lnk_Btn = ((LinkButton)e.Item.FindControl("lnk_Cancel"));
            StringBuilder CancelPath = new StringBuilder(Util.GetBaseURL());
            CancelPath.Append("/");
            CancelPath.Append(Rights.GetObject().GetLinkDetails(MenuItemId).DeleteUrl);
            CancelPath.Append("&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id));
            CancelPath.Append("&LinkBtnText=" + ClassLibraryMVP.Util.EncryptString(lnk_Btn.Text.ToUpper()));
            CancelPath.Append("&LinkName=" + ClassLibraryMVP.Util.EncryptString(Link.text.ToUpper()));
            CancelPath.Append("&No=" + ClassLibraryMVP.Util.EncryptString(DataBinder.Eval(e.Item.DataItem, "col2").ToString()));
            ViewPath.Append("&Is_Cancel=" + ClassLibraryMVP.Util.EncryptBool(true));

            lnk_Btn.Attributes.Add("onclick", "return Open_Cancel_Window('" + CancelPath + "','" + MenuItemId.ToString() + "')");

            if (MenuItemId == 262 || MenuItemId == 269)//for Pickup status
            {
                e.Item.Cells[11].Enabled = true;
            }
            else
            {
                e.Item.Cells[11].Enabled = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Can_Edit_Cancel"));
            }
             try
            {
                 e.Item.Cells[12].Enabled = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Can_Cancel"));
            }
            catch(Exception ew)
            {
                e.Item.Cells[12].Enabled = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Can_Edit_Cancel"));
            }

            if (MenuItemId == 30 && AllowBooking == false)
            {
                e.Item.Cells[11].Enabled = false;
            }

        }
    }

        
}
