using System;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Text;

public partial class Reports_CL_Nandwana_UserDesk_Frm_Dly_BranchWise_Pending_Stock_Door_Godown_Summary : System.Web.UI.Page
{
    int Main_Id, Year_Code;
    string Hirearchy_Code;

    decimal ArticlesDoor, ArticlesGodown, Total_GcDoor, Total_GcGodown;
    decimal Total_FreightDoor, Total_FreightGodown;

    DataSet ds = new DataSet();


    protected void Page_Load(object sender, EventArgs e)
    {
        Hirearchy_Code = (string)UserManager.getUserParam().HierarchyCode;
        Main_Id = (int)UserManager.getUserParam().MainId;
        Year_Code = UserManager.getUserParam().YearCode;

        tbl_Godown.Visible = false;
        tbl_Door.Visible = false;

        if (IsPostBack == false)
        {
            Bind_DDL();

        }
    }

    protected void Bind_DDL()
    {
        
        string query;

        if (Main_Id == 0)
        {
            query = "Select Branch_ID,Branch_Name from EC_Master_Branch where Branch_Id in (12,13,72,23) order by IndexNo";
        }
        else
        {
            query = "Select Branch_ID,Branch_Name from EC_Master_Branch where Branch_Id = " + Convert.ToString(Main_Id) ;
        }

        Common ObjCommon = new Common();

        ds = ObjCommon.EC_Common_Pass_Query(query);

        ddl_Branch.DataSource = ds;
        ddl_Branch.DataTextField = "Branch_Name";
        ddl_Branch.DataValueField = "Branch_ID";
        ddl_Branch.DataBind();

    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_Branch.SelectedValue) > 0)
        {
            BindGridDoor();

            tbl_Godown.Visible = true;
            tbl_Door.Visible = true;
        }
        else
        {
            lbl_Error.Text = "Select Branch";
            ddl_Branch.Focus();
            tbl_Godown.Visible = false;
            tbl_Door.Visible = false;

        }
    }

    protected void dg_DetailsDoor_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_Door_SumArticles, lbl_Door_SumTotal_Gc, lbl_Door_SumTotalFreight;

            lbl_Door_SumArticles = (Label)e.Item.FindControl("lbl_Door_SumArticles");
            lbl_Door_SumTotal_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Door_SumTotal_Gc");
            lbl_Door_SumTotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Door_SumTotalFreight");

            lbl_Door_SumArticles.Text = ArticlesDoor.ToString();
            lbl_Door_SumTotal_Gc.Text = Total_GcDoor.ToString();
            lbl_Door_SumTotalFreight.Text = Total_FreightDoor.ToString();

        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int SrNoDoor,IsOldDoor;
            LinkButton lbtn_ParticularsDoor;

            int GC_Count;

            GC_Count = 0;


            IsOldDoor = 0;

            SrNoDoor = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "SrNo").ToString());

            lbtn_ParticularsDoor = (LinkButton)e.Item.FindControl("lbtn_ParticularsDoor");

            int Delivery_branch_Id;

            Delivery_branch_Id = Util.String2Int(ddl_Branch.SelectedValue);

            DateTime As_On_Date = DateTime.Now;

            if (SrNoDoor == 1)
            {
                IsOldDoor = 4;
            }
            else if (SrNoDoor == 2)
            {
                IsOldDoor = 5;
            }
            else if (SrNoDoor == 3)
            {
                IsOldDoor = 6;
            }
            else if (SrNoDoor == 4)
            {
                IsOldDoor = 7;
            }

            GC_Count = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "LrCount").ToString());

            StringBuilder PathDlyStk = new StringBuilder(Util.GetBaseURL());
            PathDlyStk.Append("/");
            PathDlyStk.Append("Reports/CL_Nandwana/User Desk/Frm_Dly_BranchWise_Pending_Stock_Summary.aspx?Delivery_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Delivery_branch_Id)
                + "&DlyBranch=" + ClassLibraryMVP.Util.EncryptString(ddl_Branch.SelectedItem.Text + " (Door)") + "&IsOld=" + IsOldDoor );

            if (GC_Count > 0)
            {
                lbtn_ParticularsDoor.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + PathDlyStk + "')");
            }

        }
    }

    protected void dg_DetailsGodown_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_Godown_SumArticles, lbl_Godown_SumTotal_Gc, lbl_Godown_SumTotalFreight;

            lbl_Godown_SumArticles = (Label)e.Item.FindControl("lbl_Godown_SumArticles");
            lbl_Godown_SumTotal_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Godown_SumTotal_Gc");
            lbl_Godown_SumTotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Godown_SumTotalFreight");

            lbl_Godown_SumArticles.Text = ArticlesGodown.ToString();
            lbl_Godown_SumTotal_Gc.Text = Total_GcGodown.ToString();
            lbl_Godown_SumTotalFreight.Text = Total_FreightGodown.ToString();

        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int SrNoGodown, IsOldGodown;
            LinkButton lbtn_ParticularsGodown;

            int GC_Count;

            GC_Count = 0;

            IsOldGodown = 0; 

            SrNoGodown = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "SrNo").ToString());

            lbtn_ParticularsGodown = (LinkButton)e.Item.FindControl("lbtn_ParticularsGodown");



            int Delivery_branch_Id;

            Delivery_branch_Id = Util.String2Int(ddl_Branch.SelectedValue);

            DateTime As_On_Date = DateTime.Now;

            if (SrNoGodown == 1)
            {
                IsOldGodown = 8;
            }
            else if (SrNoGodown == 2)
            {
                IsOldGodown = 9;
            }
            else if (SrNoGodown == 3)
            {
                IsOldGodown = 10;
            }
            else if (SrNoGodown == 4)
            {
                IsOldGodown = 11;
            }


            StringBuilder PathDlyStk = new StringBuilder(Util.GetBaseURL());
            PathDlyStk.Append("/");
            PathDlyStk.Append("Reports/CL_Nandwana/User Desk/Frm_Dly_BranchWise_Pending_Stock_Summary.aspx?Delivery_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Delivery_branch_Id)
                + "&DlyBranch=" + ClassLibraryMVP.Util.EncryptString(ddl_Branch.SelectedItem.Text + " (Godown)")  + "&IsOld=" + IsOldGodown);

            GC_Count = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "LrCount").ToString());

            if (GC_Count > 0)
            {
                lbtn_ParticularsGodown.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + PathDlyStk + "')");
            }

        }
    }

    private void BindGridDoor()
    {
        DAL objDAL = new DAL();

        int Branch_id = Util.String2Int(ddl_Branch.SelectedValue);

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Branch_id)
        };

        objDAL.RunProc("EC_Opr_Dly_BranchWise_Pending_Stock_DoorGodown_Summary", objSqlParam, ref ds);

        calculate_totals();


        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_DoorDetails, ds.Tables[0], "", lbl_Error);
        objcommon.ValidateReportForm(dg_GodownDetails, ds.Tables[2], "", lbl_Error);


    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Total_GcDoor = Util.String2Decimal(dr["LrCount"].ToString());
        ArticlesDoor = Util.String2Decimal(dr["Qty"].ToString());
        Total_FreightDoor = Util.String2Decimal(dr["Freight"].ToString());

        DataRow dr2 = ds.Tables[3].Rows[0];
        Total_GcGodown = Util.String2Decimal(dr2["LrCount"].ToString());
        ArticlesGodown = Util.String2Decimal(dr2["Qty"].ToString());
        Total_FreightGodown = Util.String2Decimal(dr2["Freight"].ToString());

    }

    public void ClearVariables()
    {
        ds = null;
    }


}
