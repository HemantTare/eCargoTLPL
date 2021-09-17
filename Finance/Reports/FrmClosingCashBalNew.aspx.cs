using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Raj.EC;
using System.Text;

public partial class Finance_Reports_FrmClosingCashBalNew : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    string Crypt, BranchNameFromUserDesk;

    int BranchIDFromUserDesk, IsFromUserDesk;

    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {

        Crypt = Request.QueryString["Branch_id"];

        IsFromUserDesk = 0;

        if (Crypt != null)
        {
            BranchIDFromUserDesk = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["IsFromUserDesk"];
            IsFromUserDesk = Util.String2Int(Crypt);

            Crypt = Request.QueryString["Branch_Name"];
            BranchNameFromUserDesk = ClassLibraryMVP.Util.DecryptToString(Crypt);

        }
        if (IsPostBack == false)
        {
            Dtp_AsOnDate.SelectedDate = Convert.ToDateTime(System.DateTime.Now.Date);
            Common objcommon = new Common();
        }

        if (IsFromUserDesk == 1)
        {
            Wuc_Region_Area_Branch1.Visible = false;
            lblBranch.Visible = true;
            lblBranch.Text = BranchNameFromUserDesk;
            BindGrid("form", e);
        }
        else
        {
            lblBranch.Visible = false;

        }


    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        if (Branch_id == 0 && IsFromUserDesk != 1)
        {

            msg = "Please Select One Branch";
            lbl_Error.Text = msg;
            dg_Grid.Visible = false;
        }
        else
        {
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);
        }
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;


        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        DateTime AsOnDate = Dtp_AsOnDate.SelectedDate;

        if (IsFromUserDesk == 1)
        {
            Branch_id = BranchIDFromUserDesk;
        }
        else
        {
            Branch_id = Wuc_Region_Area_Branch1.BranchID;
        }

        SqlParameter[] objSqlParam ={
            objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,Branch_id), 
            objDAL.MakeInParams("@Date", SqlDbType.DateTime,0,AsOnDate)  
        };

        objDAL.RunProc("FA_Opr_BranchWiseDailyClosingCashReport_New2", objSqlParam, ref ds);

        if (CallFrom == "form_and_pageload") return;


        lbl_TotalCredit.Text = ds.Tables[3].Rows[0][0].ToString();
        lbl_TotalDebit.Text = ds.Tables[3].Rows[0][1].ToString();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[1], CallFrom, lbl_Error);

        Common objcommon2 = new Common();
        objcommon2.ValidateReportForm(dg_Grid2, ds.Tables[2], CallFrom, lbl_Error);
    }

    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {


        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            string TransactionType, Header;
            LinkButton lnk_TransactionNoCr, lnk_AmountCr; ;

            int TransactionID;

            decimal Amount;

            TransactionType = DataBinder.Eval(e.Item.DataItem, "TransactionType").ToString();
            Header = DataBinder.Eval(e.Item.DataItem, "Header").ToString();

            TransactionID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TransactionID").ToString());

            Amount = Util.String2Decimal(DataBinder.Eval(e.Item.DataItem, "Amount").ToString());

            if (Header == "Other Cash Receipt" || Header == "Cash Receipt From Other Branch" || Header == "Cash Transfer To Other")
            {
                e.Item.BackColor = System.Drawing.Color.BlanchedAlmond;

            }
            else
            {
                e.Item.BackColor = System.Drawing.Color.LightGray;

            }


            if (TransactionType == "Opening")
            {
                e.Item.BackColor = System.Drawing.Color.GreenYellow;
            }
            //else if (TransactionType == "Booking" || TransactionType == "Voucher" || TransactionType == "Summary")
            //{
            //    e.Item.BackColor = System.Drawing.Color.BlanchedAlmond;
            //}

            lnk_TransactionNoCr = (LinkButton)e.Item.FindControl("lnk_TransactionNoCr");

            if (TransactionType == "Booking")
            {
                lnk_TransactionNoCr.Attributes.Add("onclick", "return viewwindow_GC('" + ClassLibraryMVP.Util.EncryptInteger(TransactionID) + "')");
            }
            else if (TransactionType == "Voucher")
            {
                lnk_TransactionNoCr.Attributes.Add("onclick", "return openVoucherWindow('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TransactionID").ToString())) + "')");
            }
            else if (TransactionType == "Summary")
            {
                if (IsFromUserDesk == 0)
                {
                    lnk_TransactionNoCr.Attributes.Add("onclick", "return openSummaryWindow('" + Header + "','" + Wuc_Region_Area_Branch1.BranchID
                        + "','" + Dtp_AsOnDate.SelectedDate + "')");
                }
                else
                {
                    lnk_TransactionNoCr.Attributes.Add("onclick", "return openSummaryWindow('" + Header + "','" + BranchIDFromUserDesk
                        + "','" + Dtp_AsOnDate.SelectedDate + "')");

                }
            }


            lnk_AmountCr = (LinkButton)e.Item.FindControl("lnk_AmountCr");

            lnk_AmountCr.Font.Underline = false;
            lnk_AmountCr.ForeColor = System.Drawing.Color.Black;

            if (Amount < 0)
            {
                lnk_AmountCr.ForeColor = System.Drawing.Color.Red;
            }


        }
    }


    protected void dg_Grid2_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());


            string TransactionType, Header;
            LinkButton lnk_TransactionNoDr, lnk_AmountDr;

            int TransactionID;

            decimal Amount;

            TransactionType = DataBinder.Eval(e.Item.DataItem, "TransactionType").ToString();
            Header = DataBinder.Eval(e.Item.DataItem, "Header").ToString();

            TransactionID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TransactionID").ToString());

            Amount = Util.String2Decimal(DataBinder.Eval(e.Item.DataItem, "Amount").ToString());

            if (Header == "Cash Deposited In Bank" || Header == "Other Expense" || Header == "Cash Transfer To Other")
            {
                e.Item.BackColor = System.Drawing.Color.BlanchedAlmond;

            }
            else if (Header == "Diesel Expense" || Header == "Conveyance Expense" || Header == "Petrol Expense")
            {
                e.Item.BackColor = System.Drawing.Color.LightSkyBlue;

            }
            else
            {
                e.Item.BackColor = System.Drawing.Color.LightGray;

            }


            if (TransactionType == "Closing")
            {
                e.Item.BackColor = System.Drawing.Color.GreenYellow;

                if (Amount < 0)
                {
                    e.Item.ForeColor = System.Drawing.Color.Red;
                }
            }
            //else if (TransactionType == "Booking" || TransactionType == "Voucher" || TransactionType == "Summary")
            //{
            //    e.Item.BackColor = System.Drawing.Color.BlanchedAlmond;
            //}
            else if (TransactionType == "Total Debit")
            {
                e.Item.BackColor = System.Drawing.Color.LightPink;
            }


            lnk_TransactionNoDr = (LinkButton)e.Item.FindControl("lnk_TransactionNoDr");

            if (TransactionType == "Voucher" && Header == "Freight Discount")
            {
                lnk_TransactionNoDr.Attributes.Add("onclick", "return openFrtDiscountWindow('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TransactionID").ToString())) + "')");
            }
            else if (TransactionType == "Voucher" && Header == "Diesel Expense")
            {
                lnk_TransactionNoDr.Attributes.Add("onclick", "return openDieselExpenseWindow('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TransactionID").ToString())) + "')");
            }
            else if (TransactionType == "Voucher" && Header == "Conveyance Expense")
            {
                lnk_TransactionNoDr.Attributes.Add("onclick", "return openConveyanceExpenseWindow('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TransactionID").ToString())) + "')");
            }
            else if (TransactionType == "Voucher" && Header == "Petrol Expense")
            {
                lnk_TransactionNoDr.Attributes.Add("onclick", "return openPetrolExpenseWindow('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TransactionID").ToString())) + "')");
            }
            else if (TransactionType == "Voucher")
            {
                lnk_TransactionNoDr.Attributes.Add("onclick", "return openVoucherWindow('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TransactionID").ToString())) + "')");
            }
            else if (TransactionType == "Summary")
            {
                if (IsFromUserDesk == 0)
                {
                    lnk_TransactionNoDr.Attributes.Add("onclick", "return openSummaryWindow('" + Header + "','" + Wuc_Region_Area_Branch1.BranchID
                        + "','" + Dtp_AsOnDate.SelectedDate + "')");
                }
                else
                {
                    lnk_TransactionNoDr.Attributes.Add("onclick", "return openSummaryWindow('" + Header + "','" + BranchIDFromUserDesk
                        + "','" + Dtp_AsOnDate.SelectedDate + "')");

                }
            }


            lnk_AmountDr = (LinkButton)e.Item.FindControl("lnk_AmountDr");

            if (TransactionType != "Closing")
            {
                lnk_AmountDr.Font.Underline = false;
                lnk_AmountDr.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                lnk_AmountDr.Font.Underline = true;

                if (Amount < 0)
                {
                    lnk_AmountDr.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lnk_AmountDr.ForeColor = System.Drawing.Color.Blue;
                }

                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmUserDeskDlyBranchToPayRecovery.aspx?BranchID=" + Wuc_Region_Area_Branch1.BranchID);

                lnk_AmountDr.Attributes.Add("onclick", "return DlyBranchToPayRecovery('" + PathF4 + "')");

            }

        }

    }


    #endregion


    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();


        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        string Branch_Name = Wuc_Region_Area_Branch1.SelectedBranchText;

        if (IsFromUserDesk == 1)
        {
            Branch_id = BranchIDFromUserDesk;
            Branch_Name = BranchNameFromUserDesk;
        }
        else
        {
            Branch_id = Wuc_Region_Area_Branch1.BranchID;
        }


        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/");

        Path.Append("Finance/Reports/FrmClosingCashBalNewViewer.aspx?Branch_id=" + Util.EncryptInteger(Branch_id) + "&Branch_Name=" + Util.EncryptString(Branch_Name)
                 + "&date=" + Dtp_AsOnDate.SelectedDate);


        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("Open_Details_Window('");
        sb.Append(Path);
        sb.Append("');");
        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());

    }

}
