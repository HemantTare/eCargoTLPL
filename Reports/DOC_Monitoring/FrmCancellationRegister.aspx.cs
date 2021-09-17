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
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using ClassLibraryMVP;

public partial class Reports_DOC_Monitoring_FrmCancellationRegister : System.Web.UI.Page
{
    DAL objDal = new DAL();
    DataSet ds = new DataSet();
    DataSet ds_Export = new DataSet();
    int main_id;
    string Hierarchy_Code;

    protected void Page_Load(object sender, EventArgs e)
    {
        Hierarchy_Code = (string)UserManager.getUserParam().HierarchyCode;
        main_id = (int)UserManager.getUserParam().MainId;
        if (!IsPostBack)
        {
            Wuc_Region_Area_Branch1.SetRegionCaption = "Region:";
            Wuc_Region_Area_Branch1.SetAreaCaption = "Area:";
            Wuc_Region_Area_Branch1.SetBranchCaption = "Branch:";
            //Division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;



            fillMenuItems();
        }
        Wuc_Export_To_Excel1.FileName = "CancellationRegister";
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        //ddl_Menu_Item.Items[1].Text = CompanyManager.getCompanyParam().GcCaption;
        //ddl_Menu_Item.Items[5].Text = CompanyManager.getCompanyParam().LHPOCaption;
    }

    private void fillMenuItems()
    {
        DataSet ds_MenuItem = new DataSet();
        if (Convert.ToInt32(ddl_Module_Type.SelectedValue) == 1)
        {
            ds_MenuItem = objDal.RunQuery(" Select Menuitem_id, Menuitem_name from com_Adm_menu_item " +
                                          " where Menuitem_ID in (30,154,51,73,158,72,115,77,80,82,83,90,91,101,213) AND Is_Active=1");
        }
        else if (Convert.ToInt32(ddl_Module_Type.SelectedValue) == 2)
        {
            ds_MenuItem = objDal.RunQuery(" Select b.Voucher_type_id 'Menuitem_id', b.Voucher_Name 'Menuitem_name' from dbo.com_Adm_menu_item a " +
                                          " INNER JOIN dbo.FA_Master_Voucher_Type b on a.Menuitem_Name = b.Voucher_Name " +
                                          " WHERE b.Voucher_Type_Id not in (5,7,9,10,11,12,13,14,15,16,17,18,19,20,21,32)" +
                                          " AND a.Menu_System_ID = 1 AND NOT a.linkurl like '%IBT%'" +
                                          " AND a.Is_Active=1 ORDER BY a.Menuitem_name OPTION (HASH JOIN)");
        }
        //ds_MenuItem = objDal.RunQuery("Select Menuitem_id, Menuitem_name from com_Adm_menu_item where Menuitem_ID in (30,154,51,73,158,72,115,77,80,82,83,90,91,101) AND Is_Active=1");
        ddl_Menu_Item.DataTextField = "Menuitem_name";
        ddl_Menu_Item.DataValueField = "Menuitem_id";
        ddl_Menu_Item.DataSource = ds_MenuItem;
        ddl_Menu_Item.DataBind();
        ddl_Menu_Item.Items.Insert(0, new ListItem("Select", "0"));
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }
        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        int Division_ID = WucDivisions1.Division_ID;
        int MenuItem_Id = Convert.ToInt32(ddl_Menu_Item.SelectedValue);
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_Date = Wuc_From_To_Datepicker1.SelectedToDate;

        SqlParameter[] sqlParam ={
            objDAL.MakeInParams("@Region_Id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_Id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_Id", SqlDbType.Int,0,Branch_id),
            objDal.MakeInParams("@Division_ID",SqlDbType.Int,4,Division_ID),
            objDal.MakeInParams("@Menuitem_Id",SqlDbType.Int,4,MenuItem_Id),
            objDal.MakeInParams("@From_Date",SqlDbType.DateTime,0,From_Date),
            objDal.MakeInParams("@To_Date",SqlDbType.DateTime,0,To_Date),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@MainId",SqlDbType.Int,0,main_id),
            objDAL.MakeInParams("@HierarchyCode",SqlDbType.VarChar,2,Hierarchy_Code)
            };
        if (Convert.ToInt32(ddl_Module_Type.SelectedValue) == 1)
        {
            objDal.RunProc("[dbo].[FC_RPT_Cancellation_Details_GRD]", sqlParam, ref ds);
        }
        else if (Convert.ToInt32(ddl_Module_Type.SelectedValue) == 2)
        {
            objDal.RunProc("[dbo].[FC_RPT_Cancellation_Details_Voucher_GRD]", sqlParam, ref ds);
        }

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[1].Rows[0][0].ToString();
        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }

    }

    protected void btn_View_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            if (ddl_Menu_Item.SelectedValue == "0")
            {
                lbl_Error.Text = "Please Select Document";
                //pnl_Cancellation_Register.Visible = false;
            }
            else
            {
                lbl_Error.Text = "";
                dg_Grid.CurrentPageIndex = 0;
                BindGrid("form", e);
            }
        }
        else
        {
            lbl_Error.Text = msg;
            //dg_Grid.Visible = false;
        }

    }
    private void PrepareDTForExportToExcel()
    {
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }
    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton lnk_btn;
        int Id;
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                lnk_btn = (LinkButton)(e.Item.FindControl("lnk_btn_Doc_No"));

                if (Util.String2Int(ddl_Menu_Item.SelectedValue) == 30)
                {
                    lnk_btn.Attributes.Add("onclick", "return viewwindow('GC','" + DataBinder.Eval(e.Item.DataItem, "Document Id").ToString() + "')");
                }
                else if (ddl_Menu_Item.SelectedValue == "38")
                {
                    lnk_btn.Enabled = false;
                }
                else if (ddl_Menu_Item.SelectedValue == "51")
                {
                    lnk_btn.Attributes.Add("onclick", "return viewwindow('MEMO','" + DataBinder.Eval(e.Item.DataItem, "Document Id").ToString() + "')");

                }
                else if (ddl_Menu_Item.SelectedValue == "72")
                {
                    lnk_btn.Attributes.Add("onclick", "return viewwindow('AUS','" + DataBinder.Eval(e.Item.DataItem, "Document Id").ToString() + "')");

                }
                else if (ddl_Menu_Item.SelectedValue == "73")
                {
                    lnk_btn.Attributes.Add("onclick", "return viewwindow('LHPO','" + DataBinder.Eval(e.Item.DataItem, "Document Id").ToString() + "')");

                }
                else if (ddl_Menu_Item.SelectedValue == "77")
                {
                    lnk_btn.Enabled = false;

                }
                else if (ddl_Menu_Item.SelectedValue == "80")
                {
                    lnk_btn.Attributes.Add("onclick", "return viewwindow_ddc('1','" + DataBinder.Eval(e.Item.DataItem, "Document Id").ToString() + "')");

                }
                else if (ddl_Menu_Item.SelectedValue == "82")
                {
                    lnk_btn.Attributes.Add("onclick", "return viewwindow_ddc('2','" + DataBinder.Eval(e.Item.DataItem, "Document Id").ToString() + "')");

                }
                else if (ddl_Menu_Item.SelectedValue == "83")
                {
                    lnk_btn.Attributes.Add("onclick", "return viewwindow_ddc('3','" + DataBinder.Eval(e.Item.DataItem, "Document Id").ToString() + "')");

                }
                //else if (ddl_Menu_Item.SelectedValue == "37")
                else if (Convert.ToInt32(ddl_Module_Type.SelectedValue) == 2)
                {
                    Id = Convert.ToInt32(((Label)e.Item.FindControl("lblVoucher_Id")).Text);
                    string path = "../../Finance/VoucherView/FrmVoucher.aspx?Id=" + Util.EncryptInteger(Id).ToString() + "";
                    e.Item.Attributes.Add("onclick", "return Open_Popup_Window('" + path + "')");
                    //lnk_btn.Attributes.Add("onclick", "return viewwindow_ddc('ATH','" + DataBinder.Eval(e.Item.DataItem, "Document Id").ToString() + "')");

                }
                else if (ddl_Menu_Item.SelectedValue == "115")
                {
                    lnk_btn.Enabled = false;

                }
                else if (ddl_Menu_Item.SelectedValue == "154")
                {
                    lnk_btn.Enabled = false;

                }
                else if (ddl_Menu_Item.SelectedValue == "158")
                {
                    lnk_btn.Enabled = false;

                }
            }
        }

    }

    protected void ddl_Module_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillMenuItems();
    }
}