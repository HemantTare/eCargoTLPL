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
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_CL_Nandwana_User_Desk_FrmUserDeskClientSearch : System.Web.UI.Page
{
    #region ClassVariables
    private DataSet ds, dsCity;
    private DAL objDAL = new DAL();
    #endregion

    #region ControlsValue

    public int City_id
    {

        get { return Util.String2Int(ddl_City.SelectedValue); }
        set
        {
            ddl_City.SelectedValue = Util.Int2String(value);
        }
    }


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (IsPostBack == false)
        {
            txt_Search.Focus();
            Fill_City();

            //txt_Search.Text  = Request.QueryString["ClientName"];
            //City_id = Convert.ToInt32(Request.QueryString["CityID"]);

            hdn_MobileNo.Value = Request.QueryString["MobileNo"];
            hdn_ClientName.Value = Request.QueryString["ClientName"];
            hdn_GSTNo.Value  = Request.QueryString["GSTNo"];

            string FromClientForm;

            FromClientForm = Request.QueryString["FromClientForm"];

            if (FromClientForm == "Yes")
            {
                tr_Copy.Visible = true;
            }
            else
            {
                tr_Copy.Visible = false;
            }

            //City_id = Convert.ToInt32(Request.QueryString["CityID"]);


            txt_Search.Text = Request.QueryString["MobileNo"];

        }

        lnk_btnAddWalkInClient.Attributes.Add("onclick", "return NewWalkInClient()");

    }

    private void Fill_City()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@CityID", SqlDbType.Int,0,0) 
        };

        objDAL.RunProc("EC_Fill_City", objSqlParam, ref dsCity);

        ddl_City.DataSource = dsCity;
        ddl_City.DataTextField = "City_Name";
        ddl_City.DataValueField = "City_ID";
        ddl_City.DataBind();

        ddl_City.Items.Insert(0, new ListItem("All", "0"));
    }


    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        City_id = Convert.ToInt32(ddl_City.SelectedItem.Value);

        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Search", SqlDbType.VarChar, 150, txt_Search.Text),
        objDAL.MakeInParams("@City_Id", SqlDbType.Int , 0, City_id)
        };

        objDAL.RunProc("dbo.EC_Opr_User_Desk_Client_Search", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            dg_Grid.DataSource = ds.Tables[0];
            dg_Grid.DataBind();
        }
        else
        {
            dg_Grid.DataSource = null;
            dg_Grid.DataBind();
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            dg_Grid2.DataSource = ds.Tables[1];
            dg_Grid2.DataBind();
        }
        else
        {
            dg_Grid2.DataSource = null;
            dg_Grid2.DataBind();
        }

        if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
        {
            lbl_Error.Text = "No Record Found";
        }
        else
        {
            lbl_Error.Text = "";
        }
    }


    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            int WalkInClient_Id;
            LinkButton lbtn_WalkInClientName;

            WalkInClient_Id= Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Client_ID").ToString());
            lbtn_WalkInClientName = (LinkButton)e.Item.FindControl("lbtn_WalkInClientName");

            lbtn_WalkInClientName.Attributes.Add("onclick", "return viewwindow_ClientWalkIn('" + ClassLibraryMVP.Util.EncryptInteger(WalkInClient_Id) + "')");

        }
    }

    protected void dg_Grid2_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            int RegularClient_Id;
            LinkButton lbtn_RegularClientName;

            RegularClient_Id  = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Client_ID").ToString());
            lbtn_RegularClientName = (LinkButton)e.Item.FindControl("lbtn_RegularClientName");

            lbtn_RegularClientName.Attributes.Add("onclick", "return viewwindow_ClientRegular('" + ClassLibraryMVP.Util.EncryptInteger(RegularClient_Id) + "')");

        }
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

    protected void btn_Search_Click(object sender, EventArgs e)
    {

        if (txt_Search.Text.Trim().Length >= 4)
        {
            lbl_Error.Text = "";
            BindGrid("form", e);
            ds = null;
        }
        else
        {
            dg_Grid.DataSource = null;
            dg_Grid.DataBind();

            dg_Grid2.DataSource = null;
            dg_Grid2.DataBind();

            lbl_Error.Text = "Enter Minimum 4 Characters Or Numbers";
            txt_Search.Focus();
        }
    }


    protected void btn_CopyName_Click(object sender, EventArgs e)
    {
        txt_Search.Text = hdn_ClientName.Value;
        btn_Search_Click(sender,e);
    }

    protected void btn_CopyMobileNo_Click(object sender, EventArgs e)
    {
        txt_Search.Text = hdn_MobileNo.Value;
        btn_Search_Click(sender, e);

    }

    protected void btn_CopyGSTNo_Click(object sender, EventArgs e)
    {
        txt_Search.Text = hdn_GSTNo.Value;
        btn_Search_Click(sender, e);
    }

}



