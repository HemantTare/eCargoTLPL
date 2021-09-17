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

public partial class TrackNTrace_WucTrackNTracePDSDeliveryDetails : System.Web.UI.UserControl
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    public Label lbl_cancelled;

    string Document_Type;
    int Delivery_Type_ID, Document_No;

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

    public string Cancelled
    {
        set { lbl_cancelled.Text = value; }
    }

    //public int DeliveryType_ID
    //{
    //    get { return Delivery_Type_ID; }
    //    set { Delivery_Type_ID = value; }
    //}

    public DataTable BindPDSDeliveryGrid
    {
        set
        {
            DG_GC_PDSDelivery_Details.DataSource = value;
            DG_GC_PDSDelivery_Details.DataBind();
        }
    }



    public DataTable BindPDSDeliveryMasterDetails
    {
        set
        {
            Rep_PDSDeliveryDetails.DataSource = value;
            Rep_PDSDeliveryDetails.DataBind();
        }
    }

    public bool setVisibleOnPageLoad
    {
        set
        {
            tr_PDSDELIVERY_Details.Visible = value; 
        }
    }
    #endregion

    public void FillPDSDlyDetails()
    {
        tr_Error.Visible = false;
        //DG_GC_Delivery_Details.Columns[3].HeaderText = CompanyManager.getCompanyParam().LHPOCaption + " No";

        if (Doc_Type == "GC")
        {
            DG_GC_PDSDelivery_Details.Columns[5].HeaderText = "Updated By";
            fill_GCPDSDelivery_Grid();
        }
        else if (Doc_Type == "PDS")
        {
            DG_GC_PDSDelivery_Details.Columns[0].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";
            DG_GC_PDSDelivery_Details.Columns[5].HeaderText = CompanyManager.getCompanyParam().GcCaption + " Updated By";

            fill_PDSDelivery_Grid();
        }
    }
    #region getValues

    private void fill_GCPDSDelivery_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@GC_Id", SqlDbType.Int, 0, Doc_No) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_GC_PreDelivery_Details", objSqlParam, ref objDS);

        ////////tr_PDSRepDelDetals.Visible = false;
        if (objDS.Tables[0].Rows.Count > 0)
        {
            tr_PDSDELIVERY_Details.Visible = true;
        
            BindPDSDeliveryGrid = objDS.Tables[0];
        }
        else
        {
            //////tr_PDSDELIVERY_Details.Visible = false;

            if (IsPostBack)
            tr_Error.Visible = true;
        }
         
    }

    private void fill_PDSDelivery_Grid()
    {
        SqlParameter[] objSqlParam ={objDAL.MakeInParams("@PDS_Id", SqlDbType.Int, 0, Doc_No)};

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_PreDelivery_Details", objSqlParam, ref objDS);

        //tr_PDSRepDelDetals.Visible = true;
        Cancelled = objDS.Tables[0].Rows.Count > 0 ? objDS.Tables[0].Rows[0]["CancelledText"].ToString() : "";

        BindPDSDeliveryMasterDetails = objDS.Tables[0];
        if (objDS.Tables[1].Rows.Count > 0)
        {
            tr_PDSDELIVERY_Details.Visible = true;

            //DG_GC_Delivery_Details.Columns[0].Visible = false;
            //DG_GC_Delivery_Details.Columns[1].Visible = true;
            //DG_GC_Delivery_Details.Columns[4].HeaderText = objDS.Tables[1].Rows[0]["AUSCaption"].ToString() + " No";

            BindPDSDeliveryGrid = objDS.Tables[1];
        }
        else
        {
            //////tr_PDSDELIVERY_Details.Visible = false;
        }
        if (objDS.Tables[0].Rows.Count == 0)
            tr_Error.Visible = true;
    }

    protected void DG_GC_PDSDelivery_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton  btn_GC_No, btn_UpdatedBy;
        HiddenField hdn_GC_Id,hdn_PDS_ID;
        
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                 
                btn_GC_No = (LinkButton)(e.Item.FindControl("btn_GC_No"));
                btn_UpdatedBy = (LinkButton)(e.Item.FindControl("btn_UpdatedBy"));

                hdn_GC_Id = (HiddenField)(e.Item.FindControl("hdn_GC_Id"));
                hdn_PDS_ID = (HiddenField)(e.Item.FindControl("hdn_PDS_ID"));
               
                btn_GC_No.Attributes.Add("onclick", "return viewwindow('GC','" + hdn_GC_Id.Value + "')");
                btn_UpdatedBy.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "UpdatedBy_id").ToString())) + "')");
            }
        }
    }

    protected void Rep_PDSDeliveryDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lbtn_Updatedby;
        Label lbl_Tot_GC;

        if (e.Item.ItemIndex != -1)
        {
            if (e.Item.ItemIndex == 0)
            {
                lbtn_Updatedby = (LinkButton)(e.Item.FindControl("btn_DDC_UpdatedBy"));
                lbl_Tot_GC = (Label)(e.Item.FindControl("lbl_Tot_GC"));

                lbl_Tot_GC.Text = "Total No Of " + CompanyManager.getCompanyParam().GcCaption;

                lbtn_Updatedby.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Del_UpdatedBy_id").ToString())) + "')");
            }
        }
    }

    #endregion 
}
