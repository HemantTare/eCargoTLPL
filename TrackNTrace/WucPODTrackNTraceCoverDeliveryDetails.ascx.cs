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
using ClassLibraryMVP;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using System.Data.SqlClient;

public partial class TrackNTrace_WucPODTrackNTraceCoverDeliveryDetails : System.Web.UI.UserControl
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    public Label lbl_cancelled;

    string Document_Type;
    int Document_No;

    #region ControlsValues

    public int Doc_No
    {
        get { return Document_No; }
        set { Document_No = value; }
    }

    public string Cancelled
    {
        set { lbl_cancelled.Text = value; }
    }

    public string Doc_Type
    {
        get { return Document_Type; }
        set { Document_Type = value; }
    }

    public DataTable BindGCDetailsGrid
    {
        set
        {
            dg_GC_Details.DataSource = value;
            dg_GC_Details.DataBind();
        }
    }

    public DataTable BindCDMasterDetails
    {
        set
        {
            Rep_PODCDMasterDetails.DataSource = value;
            Rep_PODCDMasterDetails.DataBind();
        }
    }
    public bool setVisibleOnPageLoad
    {
        set
        {
            tr_att_PODCD_Details.Visible = value;
            tr_GC_Details.Visible = value;
        }
    }
    #endregion
    
    public void FillPODCoverDeliveryDetails()
    {
        tr_Error.Visible = false;
        if (Doc_Type == "GC" || Doc_Type == "PODCD")
        {
            Fill_PODDelivery_Grid();
        }
    }
    #region getValues

    private void Fill_PODDelivery_Grid()
    {
        SqlParameter[] objSqlParam ={ 
                                    objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
                                    objDAL.MakeInParams("@Doc_Id", SqlDbType.Int, 0, Doc_No),
                                    objDAL.MakeInParams("@Doc_Type", SqlDbType.VarChar, 20, Doc_Type),
                                    };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_PODCoverDelivery_Details", objSqlParam, ref objDS);

        dg_GC_Details.Columns[0].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";
        dg_GC_Details.Columns[1].HeaderText = CompanyManager.getCompanyParam().GcCaption + " Date";

        if (objDS.Tables[0].Rows.Count > 0)
        {
            tr_att_PODCD_Details.Visible = true;
            BindCDMasterDetails = objDS.Tables[0];
        }
        else
        {
            tr_att_PODCD_Details.Visible = false;

            if (IsPostBack)
            tr_Error.Visible = true;
        }

        if (objDS.Tables[0].Rows.Count > 0 && Doc_Type == "PODCD")
        {
            Cancelled = objDS.Tables[0].Rows[0]["CancelledText"].ToString();
        }
        else if (Doc_Type == "PODCD")
        {
            Cancelled = "";
        }

        if (objDS.Tables[1].Rows.Count > 0)
        {
            tr_GC_Details.Visible = true;
            BindGCDetailsGrid = objDS.Tables[1];
        }
        else
        {
            tr_GC_Details.Visible = false;
        }
    }
    #endregion

    protected void Rep_PODCDMasterDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lnk_CD_Updated_By;

        if (e.Item.ItemIndex != -1)
        {
            if (e.Item.ItemIndex == 0)
            {
                lnk_CD_Updated_By = (LinkButton)(e.Item.FindControl("lnk_CD_Updated_By"));
                
                lnk_CD_Updated_By.Attributes.Add("onclick", "return view_userdetails('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "CD_Updated_By_ID").ToString())) + "')");                               
            }
        }
    }
    protected void dg_GC_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton lnk_GC_No;
        HiddenField hdn_GC_Id;

        if (e.Item.ItemIndex != -1)
        {
            if (e.Item.ItemIndex == 0)
            {
                lnk_GC_No = (LinkButton)(e.Item.FindControl("lnk_GC_No"));
                hdn_GC_Id = (HiddenField)(e.Item.FindControl("hdn_GC_ID"));

                lnk_GC_No.Attributes.Add("onclick", "return viewwindow('GC','" + hdn_GC_Id.Value + "')");
            }
        }
    }   
}
