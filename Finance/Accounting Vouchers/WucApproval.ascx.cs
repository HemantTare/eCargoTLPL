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
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC;
using System.IO;

public partial class Finance_Accounting_Vouchers_WucApproval : System.Web.UI.UserControl
{
    #region ClassVariables
    DataSet objDs = new DataSet();
    private DAL objDAL = new DAL();    
    #endregion

    #region ControlsValue

    private int DocumentTypeId
    {
        get { return Util.String2Int(ddl_DocumentType.SelectedValue); }
        set { ddl_DocumentType.SelectedValue = Util.Int2String(value); }
    }   
    private DataSet SessionApprovalGrid
    {
        get { return StateManager.GetState<DataSet>("SessionApprovalBind"); }
        set { StateManager.SaveState("SessionApprovalBind", value); }
    }
    public bool IsApproved
    {
        //set
        //{
        //    rbl_Approved.Items[0].Selected = value;
        //    rbl_Approved.Items[1].Selected = !value;
        //}
        get
        {
            if (rbl_Approved.Items[0].Selected == true)
                return true;
            else
                return false;
        }
        
    }
    #endregion

    #region OtherMethod   
    #endregion

    #region ControlBind
    private DataTable BindDocumentType
    {
        set
        {
            ddl_DocumentType.DataTextField = "Document_Name";
            ddl_DocumentType.DataValueField = "MenuItem_ID";
            ddl_DocumentType.DataSource = value;
            ddl_DocumentType.DataBind();

        }
    }
    private void BindGrid()
    {
        dg_Grid.DataSource = null;
        dg_Grid.DataBind();
        dg_Grid.DataSource = SessionApprovalGrid;
        dg_Grid.DataBind();

        CalculateTotal();
        //dg_Grid.Columns[0].Visible = false;
    }
    public String ApprovalGridDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs = SessionApprovalGrid;        
            return _objDs.GetXml().ToLower();
        }
    }
   

    #endregion

    #region OtherMethods
    private void FillValues()
    {
        objDAL.RunProc("[dbo].[EC_Opr_AccountigVouchers_Approval]", ref objDs);
        BindDocumentType = objDs.Tables[0];
    }

    private DataSet FillComboAndGrid(Boolean IsPageLoad)
    {
        SqlParameter[] param ={ objDAL.MakeInParams("@ispageload", SqlDbType.Bit, 1,IsPageLoad),
                                objDAL.MakeInParams("@menuitem_id",SqlDbType.Int,0, DocumentTypeId),
                                objDAL.MakeInParams("@Hierarchy_code",SqlDbType.VarChar, 5,UserManager.getUserParam().HierarchyCode), 
                                objDAL.MakeInParams("@main_id",SqlDbType.Int,0,UserManager.getUserParam().MainId),
                                objDAL.MakeInParams("@Searched_col",SqlDbType.VarChar,50,ddl_Search.SelectedValue),
                                objDAL.MakeInParams("@Search_Text",SqlDbType.VarChar,50,txt_Search.Text),
                                objDAL.MakeInParams("@From_Date",SqlDbType.DateTime,0,PickerFrom.SelectedDate),
                                objDAL.MakeInParams("@To_Date",SqlDbType.DateTime,0,PickerTo.SelectedDate),
                                objDAL.MakeInParams("@IsApproved",SqlDbType.Bit,1,IsApproved)
        };

        objDAL.RunProc("[dbo].[EC_FA_Approval_Fill_Grid]", param, ref objDs);
        return objDs;


    }   
    private void FillCombo()
    {
        ddl_Search.DataSource = FillComboAndGrid(true);
        ddl_Search.DataValueField = "Datavalue_Field";
        ddl_Search.DataTextField = "Datatext_Field";
        ddl_Search.DataBind();
    }
    private void FillGrid()
    {
        SessionApprovalGrid = FillComboAndGrid(false);
    }
     private void UpdateDsApprovalDetails()
     {
         int DsRow,i;
         CheckBox chkbox;

         objDs=SessionApprovalGrid;

         for (i = 0; i < dg_Grid.Items.Count ; i++)
         {
             DsRow = (dg_Grid.PageSize * dg_Grid.CurrentPageIndex) + i;
             chkbox = (CheckBox)dg_Grid.Items[i].Cells[0].FindControl("Chk_IsApproved");
           
             objDs.Tables[0].Rows[DsRow]["Is Approved"] = Util.String2Bool((chkbox.Checked).ToString());

             if (dg_Grid.Items[i].Enabled==true)
             {
                 objDs.Tables[0].Rows[DsRow]["Is_Approved_Now"] = Util.String2Bool((chkbox.Checked).ToString());

             }
             
         }
         SessionApprovalGrid = objDs;
       
     }

    private void CalculateTotal()
    {
        decimal TotalCashAmt = 0;
        decimal TotalChqAmt = 0;
        decimal TotalMRAmt = 0;

        lblTotalCashAmt.Text = TotalCashAmt.ToString();
        lblTotalChqAmt.Text = TotalChqAmt.ToString();
        lblTotalMRAmt.Text = TotalMRAmt.ToString();

        foreach (DataRow dr in SessionApprovalGrid.Tables[0].Rows)
        {
            if (Util.String2Bool(dr["Is Approved"].ToString()) == true)
            {
                if (ddl_DocumentType.SelectedValue != "195")
                {
                    TotalCashAmt = Util.String2Decimal(lblTotalCashAmt.Text) + Util.String2Decimal(dr["Cash Amount"].ToString());
                    TotalChqAmt = Util.String2Decimal(lblTotalChqAmt.Text) + Util.String2Decimal(dr["Cheque Amount"].ToString());
                }

                TotalMRAmt = Util.String2Decimal(lblTotalMRAmt.Text) + Util.String2Decimal(dr["Total MR Amount"].ToString());

                lblTotalCashAmt.Text = TotalCashAmt.ToString();
                lblTotalChqAmt.Text = TotalChqAmt.ToString();
                lblTotalMRAmt.Text = TotalMRAmt.ToString();
            }
        }

        if (ddl_DocumentType.SelectedValue == "195")
        {
            tdTotalCashAmt.Visible = false;
            tdTotalCashAmtCaption.Visible = false;
            tdTotalChqAmt.Visible = false;
            tdTotalChqAmtCaption.Visible = false;
            tdTotalMRAmt.Visible = true;
            tdTotalMRAmtCaption.Visible = true;
        }
        else
        {
            tdTotalCashAmt.Visible = true;
            tdTotalCashAmtCaption.Visible = true;
            tdTotalChqAmt.Visible = true;
            tdTotalChqAmtCaption.Visible = true;
            tdTotalMRAmt.Visible = true;
            tdTotalMRAmtCaption.Visible = true;
        }
    }
    #endregion    

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            FillValues();            
            FillCombo();
            FillGrid();
            BindGrid();
            if (rbl_Approved.Items[1].Selected == true)
            {
                btn_Save.Enabled = true;
            }
            else
            {
                btn_Save.Enabled = false;
            }

        }        
       
    }    

    private void EventBindGrid()
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
            dg_Grid.CurrentPageIndex = 0;
            FillGrid();
            BindGrid();

           
        }
    }
   
    protected void btn_Search_Click(object sender, ImageClickEventArgs e)
    {       
       EventBindGrid();
       if (rbl_Approved.Items[1].Selected == true)
       {
           btn_Save.Enabled = true;
       }
       else
       {
           btn_Save.Enabled = false;
       }
       
    }
    
    protected void ddl_DocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_Search.Text = "";
        FillCombo();
        dg_Grid.CurrentPageIndex = 0;        
        FillGrid();
        BindGrid();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        UpdateDsApprovalDetails();
        SqlParameter[] param ={                                
                                objDAL.MakeInParams("@UserId",SqlDbType.Int,0,UserManager.getUserParam().UserId),
                                objDAL.MakeInParams("@ApprovalGridDetailsXML",SqlDbType.Xml,0,ApprovalGridDetailsXML),
                                objDAL.MakeInParams("@MenuItemId",SqlDbType.Int,0,DocumentTypeId)
                                
                               };

        objDAL.RunProc("[dbo].[EC_FA_AccountigVouchers_Approval_Save]", param);
        string CloseScript = "<script language='javascript'>self.close();</script>";
        ScriptManager.RegisterStartupScript(btn_Save, typeof(String), "CloseScript", CloseScript, false);
        //Response.Write("<script language='javascript'>self.close();</script>");
       //Response.Write("



    }
    protected void rbl_Approved_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillCombo();
        if (rbl_Approved.Items[0].Selected == true)
        {
            btn_Save.Enabled = false;
        }
        else
        {
            btn_Save.Enabled = true;
        }
        FillGrid();
        BindGrid();
       
    }

    #region GridEvents

    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        UpdateDsApprovalDetails();
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
        if (rbl_Approved.Items[0].Selected == true)
        {
            btn_Save.Enabled = false;
        }
        else
        {
            btn_Save.Enabled = true;
        }
    }  
    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
      

        if (e.Item.ItemIndex != -1)
        {
            //if (Util.String2Bool(DataBinder.Eval(e.Item.DataItem, "Is_Approved_Now").ToString()) == false && Util.String2Bool(DataBinder.Eval(e.Item.DataItem, "Is Approved").ToString()) == true)
            //{
            //    e.Item.Enabled = false;
            //}                   
      
            if (ddl_DocumentType.SelectedValue == "195")
            {
                e.Item.Cells[2].Visible = true;
                //int MenuItemId = Common.GetMenuItemId();
                //int Mode = 4;           
                int Voucher_Id = Convert.ToInt32(((Label)e.Item.Cells[1].FindControl("lbl_VoucherId")).Text);

                LinkButton lnk_Btn = ((LinkButton)e.Item.Cells[2].FindControl("lnk_View"));

                //String Path = "VoucherView/FrmVoucher.aspx?Id=" + ClassLibraryMVP.Util.EncryptInteger(Voucher_Id);
                //String ViewPath = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(MenuItemId).ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Voucher_Id);
                lnk_Btn.Attributes.Add("onclick", "return Open_View_Window('" + ClassLibraryMVP.Util.EncryptInteger(Voucher_Id) + "')");
            }
            else
            {
                e.Item.Cells[2].Visible = false;
            }
            e.Item.Cells[3].Visible = false;
            e.Item.Cells[4].Visible = false;
            e.Item.Cells[5].Visible = false;

            CheckBox Chk_IsApproved = (CheckBox)(e.Item.FindControl("Chk_IsApproved"));
            //HiddenField hdnCashAmt = (HiddenField)(e.Item.FindControl("hdnCashAmt"));
            //HiddenField hdnChqAmt = (HiddenField)(e.Item.FindControl("hdnChqAmt"));
            //HiddenField hdnMRAmt = (HiddenField)(e.Item.FindControl("hdnMRAmt"));

            string CashAmt = "0";
            string ChqAmt = "0";
            string MRAmt = DataBinder.Eval(e.Item.DataItem, "Total MR Amount").ToString();

            if (ddl_DocumentType.SelectedValue != "195")
            {
                CashAmt = DataBinder.Eval(e.Item.DataItem, "Cash Amount").ToString();
                ChqAmt = DataBinder.Eval(e.Item.DataItem, "Cheque Amount").ToString();
            }

            Chk_IsApproved.Attributes.Add("onclick", "return CalculateTotal('" + Chk_IsApproved.ClientID + "','" +
                CashAmt + "','" + ChqAmt + "','" + MRAmt + "')");

           
        }
    }
    protected void dg_Grid_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            if (ddl_DocumentType.SelectedValue == "195")
            {
                e.Item.Cells[2].Visible = true;
            }
            else
            {
                e.Item.Cells[2].Visible = false;
            }
            e.Item.Cells[3].Visible = false;
            e.Item.Cells[4].Visible = false;
            e.Item.Cells[5].Visible = false;  
        }
    }
    #endregion


    protected void btn_Export_To_Excel_Click(object sender, EventArgs e)
    {

        dg_Grid.AllowPaging = false;
        //dg_Ledger_Outstandings.DataSource = Session_OutStandings;
        //dg_Ledger_Outstandings.DataBind();


        string filename = "Approval";
        StringWriter strWtr = new System.IO.StringWriter();
        Html32TextWriter htmlWtr = new Html32TextWriter(strWtr);


        //ClearControls(dg_Delivery_Service_Tax_Payable)
        //this.dg_Ageing_Outstanding_Grid.RenderControl(htmlWtr);
        //this.dg_Grid.Columns[0].Visible = false;
        //this.dg_Grid.Columns[1].Visible = false;
        //this.dg_Grid.Columns[2].Visible = false;


        if (this.dg_Grid.Items.Count > 0)
        {
            if (SessionApprovalGrid.Tables[0].Rows.Count >= 0)
            {
                GridView dg1 = new GridView();
                dg1.AllowPaging = false;
                if (SessionApprovalGrid.Tables[0].Columns.Count > 7 )
                {
                    
                    SessionApprovalGrid.Tables[0].Columns.RemoveAt(0);
                    SessionApprovalGrid.Tables[0].Columns.RemoveAt(0);
                    SessionApprovalGrid.Tables[0].Columns.RemoveAt(0);
                    SessionApprovalGrid.Tables[0].AcceptChanges();
                    //decimal dr = Convert.ToDecimal(SessionApprovalGrid.Tables[0].Compute("sum([Total MR Amount])", ""));
                    DataRow dr = SessionApprovalGrid.Tables[0].NewRow();
                    dr["Total MR Amount"] = Convert.ToDecimal(SessionApprovalGrid.Tables[0].Compute("sum([Total MR Amount])", ""));
                    if (ddl_DocumentType.SelectedValue == "195")
                    {
                        dr["Amount"] = Convert.ToDecimal(SessionApprovalGrid.Tables[0].Compute("sum([Amount])", ""));
                    }
                    SessionApprovalGrid.Tables[0].Rows.Add(dr);


                }
                
                //if (ddl_DocumentType.SelectedValue == "195")
                //{
                //    SessionApprovalGrid.Tables[0].Columns.RemoveAt(0);
                //    SessionApprovalGrid.Tables[0].Columns.RemoveAt(1);
                //    SessionApprovalGrid.Tables[0].Columns.RemoveAt(2);
                //}


                dg1.DataSource = SessionApprovalGrid;
                dg1.DataBind();
                StringWriter SW = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(SW);
                dg1.RenderControl(htmlWrite);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".xls");
                Response.ContentEncoding = System.Text.Encoding.UTF7;
                Response.ContentType = "application/vnd.ms-excel";
                Response.Write(SW.ToString());
                Response.End();
            }
        }
        else
        {
            //Response.Write("<script type='text/javascript'> {alert('No Record(s) Found');}</script>");
            String popupScript = "<script language='javascript'>alert('No Record(s) Found');</script>";

            Page.ClientScript.RegisterStartupScript(typeof(String), "PopupScript1", popupScript.ToString(), false);

        }

    }
}

    


