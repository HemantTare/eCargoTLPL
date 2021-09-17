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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using System.Data.SqlClient;

public partial class Finance_IBT_WucVoucherForApprovalGrid : System.Web.UI.UserControl
{

    DataSet objDs = null;
    Common objCommon = new Common();
    DAL objDal = new DAL();
    int MenuItemId;
    private int _count;
    StringBuilder _path;

    bool _isVisibleAdd;
    bool _isVisibleEdit;
    bool _isVisibleCancel;
    bool _isVisibleView;
    int _colCount;
    private string IBTVoucherFlag
    {
        get
        {
            return StateManager.GetState<string>("QueryString").Trim();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        MenuItemId = Common.GetMenuItemId();
        StateManager.SaveState("QueryString", Rights.GetObject().GetLinkDetails(MenuItemId).QueryString);

        if (IBTVoucherFlag == "2222" || IBTVoucherFlag == "3333")
        {
            _isVisibleAdd = true;
            _isVisibleEdit = true;
            _isVisibleView = true;
            _isVisibleCancel = true;
        }
        else if (IBTVoucherFlag == "VoucherForApproval")
        {
            _isVisibleEdit = true;
            _isVisibleView = true;
            _isVisibleCancel = true;
        }
        else if (IBTVoucherFlag == "RejectedVouchers")
        {
            _isVisibleView = true;
            _isVisibleCancel = true;
        }
        else
        {
            _isVisibleView = true;
        }



        _path = new StringBuilder(Util.GetBaseURL());

        if (!IsPostBack)
        {
            SetDateInSession(sender, e);
            hdn_Sort_Dir.Value = "DESC";
            hdn_Sort_Expression.Value = "Voucher_Id";

            Session["objDs"] = null;

            //FillCombo();
            //FillGridHeaders();

            FillGrid();

            RedirectOnAddClick();
        }


        Link.text = Rights.GetObject().GetLinkDetails(MenuItemId).Link.ToUpper();
        Set_User_Rights();
        //ForceRights();
    }

    private void ForceRights()
    {
        bool can_add = true;
        bool can_edit = true;
        bool can_cancel = true;
        bool can_read = true;

        objCommon.ForceRights(ref can_read, ref can_add, ref can_edit, ref can_cancel);

        if (can_read == false)
            dg_Grid.Columns[11].Visible = false;

        if (can_add == false)
            btn_Add.Visible = false;

        if (can_edit == false)
            dg_Grid.Columns[12].Visible = false;

        if (can_cancel == false)
            dg_Grid.Columns[13].Visible = false;

    }

    private void SetDateInSession(object source, EventArgs e)
    {
        StateManager.SaveState("FromDate", PickerFrom.SelectedDate);
        StateManager.SaveState("ToDate", PickerTo.SelectedDate);
        TimeSpan TS = PickerTo.SelectedDate - PickerFrom.SelectedDate;
        double A = TS.TotalDays;

    }

    private void RedirectOnAddClick()
    {
        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/");
        Path.Append(Rights.GetObject().GetLinkDetails(MenuItemId).AddUrl);
        Path.Append("&IBTVoucherFlag=" + Util.EncryptString("Add"));
        Path.Append("&Voucher_Type_Id=" + Util.EncryptInteger(IBTVoucherFlag=="2222"?2:3));
        btn_Add.Attributes.Add("onclick", "return Open_Add_Window('" + Path + "')");
    }

    private void Set_User_Rights()
    {
        btn_Add.Visible = _isVisibleAdd;
        dg_Grid.Columns[dg_Grid.Columns.Count - 3].Visible = _isVisibleView;
        dg_Grid.Columns[dg_Grid.Columns.Count - 2].Visible = _isVisibleEdit;
        dg_Grid.Columns[dg_Grid.Columns.Count - 1].Visible = _isVisibleCancel;
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
            }
            else
            {
                lbl_Errors.Text = objDs.Tables[0].Rows[0][0].ToString();
            }
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
            e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUT';");


            Label lbl_VoucherTypeId = (Label)e.Item.FindControl("lbl_VoucherTypeId");
            int _voucherTypeId = Convert.ToInt32(lbl_VoucherTypeId.Text);

            Id = Convert.ToInt32(((Label)e.Item.FindControl("lbl_Id")).Text);
            LinkButton lnk_Edit = ((LinkButton)e.Item.FindControl("lnk_Edit"));

            ///----Edit-------
     
            StringBuilder EditPath = new StringBuilder(Util.GetBaseURL());
            EditPath.Append("/");
            EditPath.Append(Rights.GetObject().GetLinkDetails(MenuItemId).EditUrl);
            EditPath.Append("&Id=");
            EditPath.Append(ClassLibraryMVP.Util.EncryptInteger(Id));



            if (IBTVoucherFlag == "VoucherForApproval")
            {
                EditPath.Append("&IBTVoucherFlag=" + Util.EncryptString("Accept"));
                EditPath.Append("&Voucher_Type_Id=" + Util.EncryptInteger(_voucherTypeId == 2 ? 3 : 2));//To Make Opposit Not Voucher 
                lnk_Edit.Text = "Accept";
            }
            else 
            {
                //EditPath.Append("&IBTVoucherFlag=" + Util.EncryptString(IBTVoucherFlag));
                EditPath.Append("&Voucher_Type_Id=" + Util.EncryptInteger(_voucherTypeId));
                EditPath.Append("&IBTVoucherFlag=" + Util.EncryptString("Edit"));
            }

            lnk_Edit.Attributes.Add("onclick", "return Open_Edit_Window('" + EditPath + "')");

            string bolean;

            if (MenuItemId == 145)
                bolean = "False";
            else
                bolean = "True";

           ///----View-------
            LinkButton lnk_View = ((LinkButton)e.Item.FindControl("lnk_View"));
            StringBuilder ViewPath = new StringBuilder(Util.GetBaseURL());
            ViewPath.Append("/");
            //ViewPath.Append(Rights.GetObject().GetLinkDetails(MenuItemId).ViewUrl);
            ViewPath.Append("Finance/VoucherView/FrmVoucher.aspx?");
            ViewPath.Append("&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id));
            ViewPath.Append("&Voucher_Type_Id=" + Util.EncryptInteger(_voucherTypeId));
            ViewPath.Append("&IBTVoucherFlag=" + Util.EncryptString("View"));
            ViewPath.Append("&IsIBT=" + bolean); 
            lnk_View.Attributes.Add("onclick", "return Open_View_Window('" + ViewPath + "')");

         
            ///----Cancel-------
            LinkButton lnk_Cancel = ((LinkButton)e.Item.FindControl("lnk_Cancel"));
            StringBuilder CancelPath = new StringBuilder(Util.GetBaseURL());
            CancelPath.Append("/");
            CancelPath.Append(Rights.GetObject().GetLinkDetails(MenuItemId).DeleteUrl);
            CancelPath.Append("&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id));
            CancelPath.Append("&LinkBtnText=" + ClassLibraryMVP.Util.EncryptString(lnk_Cancel.Text.ToUpper()));
            CancelPath.Append("&LinkName=" + ClassLibraryMVP.Util.EncryptString(lnk_Cancel.Text.ToUpper()));
            CancelPath.Append("&No=" + ClassLibraryMVP.Util.EncryptString(DataBinder.Eval(e.Item.DataItem, "Voucher_No").ToString()));
            CancelPath.Append("&Is_Cancel=" + ClassLibraryMVP.Util.EncryptBool(true));
            CancelPath.Append("&IsIBT=" + "True"); 
           

            if (IBTVoucherFlag == "VoucherForApproval")
            {
                lnk_Cancel.Text = "Reject";
            }
            else if (IBTVoucherFlag == "RejectedVouchers")
            {
                Label lbl_Debit = ((Label)e.Item.FindControl("lbl_Debit"));
                Label lbl_Credit = ((Label)e.Item.FindControl("lbl_Credit"));
                          

                CancelPath.Append("&Debit=" + ClassLibraryMVP.Util.EncryptDecimal(Convert.ToDecimal(lbl_Debit.Text)));
                CancelPath.Append("&Credit=" + ClassLibraryMVP.Util.EncryptDecimal(Convert.ToDecimal(lbl_Credit.Text)));
                CancelPath.Append("&Voucher_Type_ID=" + ClassLibraryMVP.Util.EncryptInteger(_voucherTypeId));
                CancelPath.Append("&Branch_Ledger_ID=" + ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(objDs.Tables[0].Rows[e.Item.ItemIndex]["Ledger_Id"])));
                lnk_Cancel.Text = "Reverse";
            }

            lnk_Cancel.Attributes.Add("onclick", "return Open_View_Window('" + CancelPath + "')");

            //e.Item.Cells[11].Enabled = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Can_Edit_Cancel"));
            //e.Item.Cells[12].Enabled = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Can_Edit_Cancel"));

        }
    }
       
    protected void btn_Search_Click(object sender, ImageClickEventArgs e)
    {
        MakeDataView();
    }

    public void FillGrid()
    {
        objDs = GetGridDs();
        StateManager.SaveState("objDs", objDs);
        dg_Grid.DataSource = objDs;
        dg_Grid.DataBind();

  
    }

    public void MakeDataView()
    {
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        DataTable dt = new DataTable();

        if (txt_Search.Text.Trim() == string.Empty)
        {
            FillGrid();
        }
        else
        {
            try
            {
                string filterStr;
                if (ddl_Search.SelectedValue.Trim() == "Credit" || ddl_Search.SelectedValue.Trim() == "Debit")
                {
                    filterStr = ddl_Search.SelectedValue.Trim() + "=" + txt_Search.Text.Trim() + "";
                }
                else
                {
                    filterStr = ddl_Search.SelectedValue.Trim() + " Like '" + txt_Search.Text.Trim() + "%'";
                }


                ds = StateManager.GetState<DataSet>("objDs");
                dt = ds.Tables[0];
                DataView objDv = new DataView(dt, filterStr, "", DataViewRowState.CurrentRows);

                ds1.Tables.Add(objDv.ToTable());
                StateManager.SaveState("objDs", ds1);
                dg_Grid.DataSource = ds1;
                dg_Grid.DataBind();
            }
            catch
            {
                ds1 = StateManager.GetState<DataSet>("objDs").Clone();
                dg_Grid.DataSource = ds1;
                dg_Grid.DataBind();
                lbl_Errors.Text = "No Data Found";
            }



        }
        
    }


    private void linkNameChange(LinkButton lbtn)
    {
        if (IBTVoucherFlag == "RejectedVouchers" || IBTVoucherFlag == "VoucherForApproval")
        {
            lbtn.Text = "Reverse";
        }
        else if (IBTVoucherFlag == "RejectedVouchers" || IBTVoucherFlag == "VoucherForApproval")
        {
            lbtn.Text = "Reverse";
        }
    }

    private DataSet GetGridDs()
    {
        DataSet _objDS=new DataSet();

           DAL _objDal = new DAL();

           DateTime FromDate = PickerFrom.SelectedDate;
           DateTime ToDate = PickerTo.SelectedDate;

//         DateTime FromDate = DateTime.Now;
//         DateTime ToDate = DateTime.Now;

//        @Main_Id Int,
//@Hierarchy_Code Varchar(10),
//@Division_Id int,
//@Year_Code int,
//@Voucher_Type_Id int,
//@Fromdate Datetime,
//@Todate Datetime
//@IBTVoucherFlag Varchar(20)
                SqlParameter[] sqlPara = { 
                                         _objDal.MakeInParams("@Division_Id", SqlDbType.Int, 0, UserManager.getUserParam().DivisionId), 
                                         _objDal.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
                                         _objDal.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 50, UserManager.getUserParam().HierarchyCode), 
                                         _objDal.MakeInParams("@Main_Id", SqlDbType.Int, 0, UserManager.getUserParam().MainId),
                                         _objDal.MakeInParams("@FromDate", SqlDbType.DateTime, 0, FromDate),
                                         _objDal.MakeInParams("@ToDate", SqlDbType.DateTime, 0, ToDate),
                                         _objDal.MakeInParams("@IBTVoucherFlag", SqlDbType.VarChar,50,IBTVoucherFlag)
                                        };

                _objDal.RunProc("FA_Opr_IBT_VoucherGridFill", sqlPara, ref _objDS);

                //if (IBTVoucherFlag == "VoucherForApproval")   
                //{
                //    _objDal.RunProc("FA_Opr_IBT_UnapprovedVoucherFill", sqlPara, ref _objDS);
                //}
                //else if (IBTVoucherFlag == "ApprovedVouchers")  
                //{
                //    _objDal.RunProc("FA_Opr_IBT_ApprovedVoucherFill", sqlPara, ref _objDS);
                //}
                //else if (IBTVoucherFlag == "RejectedVouchers")   
                //{
                //    _objDal.RunProc("FA_Opr_IBT_RejectedVoucherFill", sqlPara, ref _objDS);
                //}
                //else if (IBTVoucherFlag == "PenUnapprovedVouchers")   
                //{
                //    _objDal.RunProc("FA_Opr_IBT_PenUnapprovedVouchersFill", sqlPara, ref _objDS);
                //}
                //else if (IBTVoucherFlag == "2222" || IBTVoucherFlag == "3333") 
                //{
                //    _objDal.RunProc("FA_Opr_IBT_DebitCreditNoteVouchersFill", sqlPara, ref _objDS); 
                //}

            return _objDS;
      }

}
