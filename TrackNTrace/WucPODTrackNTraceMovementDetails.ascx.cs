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

/// <summary>
/// Author       : Anita Gupta
/// Description  : POD Track and Trace Cover Movement Details
/// Date         : 07 Feb 09 
/// </summary>
/// 

public partial class TrackNTrace_WucPODTrackNTraceMovementDetails : System.Web.UI.UserControl
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
    public DataTable BindPODTrackNTraceDetails
    {
        set
        {
            dg_PODTrackNTraceMovementsDetails.DataSource=value;
            dg_PODTrackNTraceMovementsDetails.DataBind();                
        }
    }
    public bool setVisibleOnPageLoad
    {
        set
        {
            tr_att_PODMovement_Details.Visible = value;
        }
    }
    #endregion

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    tr_Error.Visible = false;
    //    if (Doc_Type == "GC")
    //    {
    //        Fill_PODTrackNTraceDetails();
    //    }
    //}

    public void FillPODMovementDetails()
    {
        tr_Error.Visible = false;
        if (Doc_Type == "GC")
        {
            Fill_PODTrackNTraceDetails();
        }
    }
    #region GetValues
    private void Fill_PODTrackNTraceDetails()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
                                      objDAL.MakeInParams("@GC_Id", SqlDbType.Int, 0, Doc_No) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_PODMovement_Details", objSqlParam, ref objDS);
        
        if (objDS.Tables[0].Rows.Count > 0)
        {
            tr_att_PODMovement_Details.Visible = true;
            BindPODTrackNTraceDetails = objDS.Tables[0];
        }
        else
        {
            tr_att_PODMovement_Details.Visible = false;

            if (IsPostBack)
            tr_Error.Visible = true;
        }
        if (objDS.Tables[1].Rows.Count > 0)
        {
            lbl_Current_Status.Text = objDS.Tables[1].Rows[0][0].ToString();
            lbl_Current_Location.Text = objDS.Tables[1].Rows[0][1].ToString();
        }
    }

    #endregion
    protected void dg_PODTrackNTraceMovementsDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton lnk_CG_Updated_By, lnk_CR_Updated_By, lnk_Cover_Gen_No, lnk_Cover_Recvd_No;
        HiddenField hdn_Cover_Gen_ID, hdn_Cover_Recvd_Id;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                lnk_CG_Updated_By = (LinkButton)(e.Item.FindControl("lnk_CG_Updated_By"));
                lnk_CR_Updated_By = (LinkButton)(e.Item.FindControl("lnk_CR_Updated_By"));
                lnk_Cover_Gen_No = (LinkButton)(e.Item.FindControl("lnk_Cover_Gen_No"));
                lnk_Cover_Recvd_No = (LinkButton)(e.Item.FindControl("lnk_Cover_Recvd_No"));
                hdn_Cover_Gen_ID = (HiddenField)(e.Item.FindControl("hdn_Cover_Id"));
                hdn_Cover_Recvd_Id = (HiddenField)(e.Item.FindControl("hdn_Recieved_Id"));

                lnk_Cover_Gen_No.Attributes.Add("onclick", "return viewwindow('PODCG','" + hdn_Cover_Gen_ID.Value + "')");
                lnk_Cover_Recvd_No.Attributes.Add("onclick", "return viewwindow('PODCR','" + hdn_Cover_Recvd_Id.Value + "')");

                lnk_CG_Updated_By.Attributes.Add("onclick", "return view_userdetails('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "CG_Updated_By_ID").ToString())) + "')");
                lnk_CR_Updated_By.Attributes.Add("onclick", "return view_userdetails('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "CR_Updated_By_ID").ToString())) + "')");
            }
        }
    }
}
