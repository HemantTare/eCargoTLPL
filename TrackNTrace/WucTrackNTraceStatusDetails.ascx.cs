using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using Raj.EC;
using ClassLibraryMVP.DataAccess;

public partial class TrackNTrace_WucTrackNTraceStatusDetails : System.Web.UI.UserControl
{
    private DAL objDAL = new DAL();
    private DataSet objDS;

    string Document_Type;
    int Document_No;
    #region ControlsValues

    public int Doc_No
    {
        get { return Document_No; }
        set { Document_No = value; }
    }

    public string Doc_Type
    {
        get { return Document_Type; }
        set { Document_Type = value; }
    }

    public DataSet BindStatusGrid
    {
        set
        {
            DG_GC_Status_Details.DataSource = value;
            DG_GC_Status_Details.DataBind();
        }
    }
    public DataSet BindDeliveryAttemptsGrid
    {
        set
        {
            DG_GC_DeliveryAttempts_Details.DataSource = value;
            DG_GC_DeliveryAttempts_Details.DataBind();
        }
    }
    public bool setVisibleOnPageLoad
    {
        set
        {
            tbl_Status.Visible = value;
        }
    }
    #endregion

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    tr_Error.Visible = false;
    //    DG_GC_Status_Details.Columns[0].HeaderText = CompanyManager.getCompanyParam().GcCaption + "  No";

    //    if (Doc_Type == "GC")
    //    {
    //        fill_Status_Grid();
    //    }
    //}
    public void FillStatusDetails()
    {
        tr_Error.Visible = false;
        DG_GC_Status_Details.Columns[0].HeaderText = CompanyManager.getCompanyParam().GcCaption + "  No";

        if (Doc_Type == "GC")
        {
            fill_Status_Grid();
            fill_DeliveryAttempts_Grid();
        }
    }
    #region getValues   

    private void fill_Status_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@GC_Id", SqlDbType.Int,0, Doc_No) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_Status_Details", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            tbl_Status.Visible = true;
            BindStatusGrid = objDS;
        }
        else
        {
            tbl_Status.Visible = false;

            if (IsPostBack)
            tr_Error.Visible = true;
        }
    }
    private void fill_DeliveryAttempts_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@GC_Id", SqlDbType.Int,0, Doc_No) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_DeliveryAttempts_Details", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        { 
            BindDeliveryAttemptsGrid = objDS;
            UpdatePanel1.Visible = true;
            td_deliveryattempts.Visible = true;
        }
        else
        { 
            if (IsPostBack)
                tr_Error.Visible = true;
            UpdatePanel1.Visible = false;
            td_deliveryattempts.Visible = false;
        }
    }

    protected void DG_GC_Status_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        int Status_Id;
        string Delivery_Type_Id;
        HiddenField hdn_Document_Id;

        if (e.Item.ItemIndex != -1)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Status_Id = Util.String2Int(((HiddenField)(e.Item.FindControl("hdn_Status_Id"))).Value);
                Delivery_Type_Id = ((HiddenField)(e.Item.FindControl("hdn_DelType_Id"))).Value;
                hdn_Document_Id = (HiddenField)(e.Item.FindControl("hdn_Document_Id"));

                if (Status_Id == 0)
                {
                    e.Item.Cells[5].CssClass = "SHOWSELECTEDLINK";
                    e.Item.Cells[5].Attributes.Add("onmouseover", "this.style.cursor='hand'");
                    e.Item.Cells[5].Attributes.Add("onclick", "return viewwindow('GC','" + hdn_Document_Id.Value + "');");
                }
                else if (Status_Id == 30)
                {
                    e.Item.Cells[5].CssClass = "SHOWSELECTEDLINK";
                    e.Item.Cells[5].Attributes.Add("onmouseover", "this.style.cursor='hand'");
                    e.Item.Cells[5].Attributes.Add("onclick", "return viewwindow('MEMO','" + hdn_Document_Id.Value + "');");
                }
                else if (Status_Id == 40)
                {
                    e.Item.Cells[5].CssClass = "SHOWSELECTEDLINK";
                    e.Item.Cells[5].Attributes.Add("onmouseover", "this.style.cursor='hand'");
                    e.Item.Cells[5].Attributes.Add("onclick", "return viewwindow('LHPO','" + hdn_Document_Id.Value + "');");
                }
                else if (Status_Id == 60)
                {
                    e.Item.Cells[5].CssClass = "SHOWSELECTEDLINK";
                    e.Item.Cells[5].Attributes.Add("onmouseover", "this.style.cursor='hand'");
                    e.Item.Cells[5].Attributes.Add("onclick", "return viewwindow('AUS','" + hdn_Document_Id.Value + "');");
                }
                else if (Status_Id == 150)
                {
                    e.Item.Cells[5].CssClass = "SHOWSELECTEDLINK";
                    e.Item.Cells[5].Attributes.Add("onmouseover", "this.style.cursor='hand'");
                    e.Item.Cells[5].Attributes.Add("onclick", "return viewwindow_pds('" + Delivery_Type_Id + "','" + hdn_Document_Id.Value + "');");
                }
                else if (Status_Id == 200)
                {
                    e.Item.Cells[5].CssClass = "SHOWSELECTEDLINK";
                    e.Item.Cells[5].Attributes.Add("onmouseover", "this.style.cursor='hand'");
                    e.Item.Cells[5].Attributes.Add("onclick", "return viewwindow_ddc('" + Delivery_Type_Id + "','" + hdn_Document_Id.Value + "');");
                }
            }
        }
    }
    #endregion   
}
