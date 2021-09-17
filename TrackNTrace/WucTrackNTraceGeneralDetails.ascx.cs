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
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;

public partial class TrackNTrace_WucTrackNTraceGeneralDetails : System.Web.UI.UserControl
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    string Document_Type;
    int Document_No;
    public Label lbl_cancelled;

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

    public DataTable BindGeneralGrid
    {
        set
        {
            Rep_GC_Details.DataSource = value;
            Rep_GC_Details.DataBind();
        }
    }

    public DataTable BindBookingMRGrid
    {
        set
        {
            DG_GC_MR_Details.DataSource = value;
            DG_GC_MR_Details.DataBind();
        }
    }

    public DataTable BindCommodityGrid
    {
        set
        {
            DG_GC_Commodity_details.DataSource = value;
            DG_GC_Commodity_details.DataBind();
        }
    }

    public DataTable BindBillingPertyGrid
    {
        set
        {
            DG_Billing_Details.DataSource = value;
            DG_Billing_Details.DataBind();
        }
    }
    public bool setVisibleOnPageLoad
    {
        set
        {
            tr_booking_MR.Visible = value;
            tbl_GC_View.Visible = value;
            tr_commodity.Visible = value;
            tr_Billing_Party.Visible = value;
        }
    }
    #endregion    
    public void FillGeneralDetails()
    {
        tr_Error.Visible = false;
        if (Doc_Type == "GC")
        {
            fill_General_Grid();
        }
    }

    #region getValues

    private void fill_General_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@GC_Id", SqlDbType.Int, 0, Doc_No) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_General_Details", objSqlParam, ref objDS);

        BindGeneralGrid = objDS.Tables[0];
               
        if (objDS.Tables[0].Rows.Count > 0)
        {
            Cancelled = objDS.Tables[0].Rows[0]["CancelledText"].ToString();
            tbl_GC_View.Visible = true;
            lbl_GC_Text.Text = CompanyManager.getCompanyParam().GcCaption +" Tracing for " + CompanyManager.getCompanyParam().GcCaption +" No:";
            lbl_GC_No_For_Print.Text = objDS.Tables[0].Rows[0]["GC_No_For_View"].ToString();

            SetLinks_For_GCView();
        }
        else
        {
            tbl_GC_View.Visible = false;
            Cancelled = "";

            if(IsPostBack)
            tr_Error.Visible = true;
        }

        if (objDS.Tables[1].Rows.Count > 0)
        {
            tr_booking_MR.Visible = true;
            BindBookingMRGrid = objDS.Tables[1];
        }
        else
        {
            tr_booking_MR.Visible = false;
        }
        if (objDS.Tables[2].Rows.Count > 0)
        {
            tr_commodity.Visible = true;
            BindCommodityGrid = objDS.Tables[2];
        }
        else
        {
            tr_commodity.Visible = false;
        }

        if (objDS.Tables[3].Rows.Count > 0)
        {
            tr_Billing_Party.Visible = true;
            BindBillingPertyGrid = objDS.Tables[3];
        }
        else
        {
            tr_Billing_Party.Visible = false;
        }
    }

    private void SetLinks_For_GCView()
    {
        UserRights uObj;
        uObj = StateManager.GetState<UserRights>("UserRights");
        FormRights fRights;
        string path = "";

        if (Util.String2Bool(objDS.Tables[0].Rows[0]["Is_Opening_GC"].ToString()) == false)
        {
            //if(CompanyManager.getCompanyParam().ClientCode.ToLower() == "nandwana")
            //    fRights = uObj.getForm_Rights(213);
            //else
                fRights = uObj.getForm_Rights(30);
        }
        else
        {
            fRights = uObj.getForm_Rights(200);
        }
        bool can_view = fRights.canRead();

        if (can_view == true)
        {
            int gc_id = Util.String2Int(objDS.Tables[0].Rows[0]["GC_ID"].ToString());

            if (Util.String2Bool(objDS.Tables[0].Rows[0]["Is_Opening_GC"].ToString()) == false)
            {
                //if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "nandwana")
                //    path = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(213).ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(gc_id);
                //else
                path = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(30).ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(gc_id);
            }
            else
            {
                path = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(200).ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(gc_id);
            }
            btn_View_GC.Attributes.Add("onclick", "return viewwindow_ForGC('" + path + "')"); 
            String PrintPath = Util.GetBaseURL() + "/Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(30) + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(gc_id);
            btn_Print_GC.Attributes.Add("onclick", "return Open_Print_Window('" + PrintPath + "');");

            String PDFPath = Util.GetBaseURL() + "/TrackNTrace/FrmGCPDFViewer.aspx?GC_ID=" + ClassLibraryMVP.Util.EncryptInteger(gc_id) + "&GC_No=" + ClassLibraryMVP.Util.EncryptString(lbl_GC_No_For_Print.Text);
            btn_PDF.Attributes.Add("onclick", "return Open_PDF_Window('" + PDFPath + "');");

        }
        else
        {
            tbl_GC_View.Visible = false;
        }
    }

    protected void DG_GC_MR_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_MR_No, btn_MR_UpdatedBy;
        HiddenField hdn_MR_Id;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_MR_No = (LinkButton)(e.Item.FindControl("btn_MR_No"));
                btn_MR_UpdatedBy = (LinkButton)(e.Item.FindControl("btn_MR_UpdatedBy"));
                hdn_MR_Id = (HiddenField)(e.Item.FindControl("hdn_MR_Id"));

                btn_MR_No.Attributes.Add("onclick", "return viewwindow('BKMR','" + hdn_MR_Id.Value + "')");
                btn_MR_UpdatedBy.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "MR_Updated_By_ID").ToString())) + "')");
            }
        }
    }
    protected void DG_Billing_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_Octroi_Bill_No, btn_Freight_Bill_No;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_Octroi_Bill_No = (LinkButton)(e.Item.FindControl("btn_Octroi_Bill_No"));
                btn_Freight_Bill_No = (LinkButton)(e.Item.FindControl("btn_Freight_Bill_No"));

                btn_Octroi_Bill_No.Attributes.Add("onclick", "return viewwindow('BILL','" + DataBinder.Eval(e.Item.DataItem, "Bill_Octroi_ID").ToString() + "')");
                btn_Freight_Bill_No.Attributes.Add("onclick", "return viewwindow('BILL','" + DataBinder.Eval(e.Item.DataItem, "Bill_Freight_ID").ToString() + "')");
            }
        }
    }
    protected void Rep_GC_Details_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lbtn_Updatedby;
        Label lbl_GC_No, lbl_GC_Date, lbl_GC_Tot_Amt, lbl_GC_UpdatedBy, lbl_GC_UpdatedOn, lbl_GC_Amount;
        HtmlTableRow TD_Octroi_Details, TD_Cheque_Details;

        if (e.Item.ItemIndex != -1)
        {
            if (e.Item.ItemIndex == 0)
            {
                lbtn_Updatedby = (LinkButton)(e.Item.FindControl("btn_GC_updated_by"));
                lbl_GC_No = (Label)(e.Item.FindControl("lbl_GC_No"));
                lbl_GC_Date = (Label)(e.Item.FindControl("lbl_GC_Date"));
                lbl_GC_Tot_Amt = (Label)(e.Item.FindControl("lbl_GC_Tot_Amt"));
                lbl_GC_UpdatedBy = (Label)(e.Item.FindControl("lbl_GC_UpdatedBy"));
                lbl_GC_UpdatedOn = (Label)(e.Item.FindControl("lbl_GC_UpdatedOn"));
                //lbl_GC_Amount = (Label)(e.Item.FindControl("lbl_GC_Amount"));
                TD_Octroi_Details = (HtmlTableRow)(e.Item.FindControl("TD_Octroi_Details"));
                TD_Cheque_Details = (HtmlTableRow)(e.Item.FindControl("TD_Cheque_Details"));

                lbl_GC_No.Text = CompanyManager.getCompanyParam().GcCaption + " No";
                lbl_GC_Date.Text = CompanyManager.getCompanyParam().GcCaption + " Date";
                lbl_GC_Tot_Amt.Text = CompanyManager.getCompanyParam().GcCaption + " Total Amount";
                lbl_GC_UpdatedBy.Text = CompanyManager.getCompanyParam().GcCaption + " Updated By";
                lbl_GC_UpdatedOn.Text = CompanyManager.getCompanyParam().GcCaption + " Updated On";
                //lbl_GC_Amount.Text = CompanyManager.getCompanyParam().GcCaption + " Amount";
                if (Util.String2Bool(DataBinder.Eval(e.Item.DataItem, "Is_Octroi_Updated").ToString()) == false)
                {
                    TD_Octroi_Details.Visible = false;
                }
                if (Util.String2Bool(DataBinder.Eval(e.Item.DataItem, "Is_Cheque").ToString()) == false)
                {
                    TD_Cheque_Details.Visible = false;
                }
                lbtn_Updatedby.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_Emp_id").ToString())) + "')");
            }
        }
    }
    #endregion   
   
}
