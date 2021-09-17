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

public partial class TrackNTrace_WucTrackNTraceAttachedLHPODetails : System.Web.UI.UserControl
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

    public string Doc_Type
    {
        get { return Document_Type; }
        set { Document_Type = value; }
    }
  
    public DataTable BindLHPOGrid
    {
        set
        {
            DG_GC_LHPO_Details.DataSource = value;
            DG_GC_LHPO_Details.DataBind();
        }
    }  

    #endregion

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    tr_Error.Visible = false;
    //    if (Doc_Type == "LHPO")
    //    {
    //        fill_ATT_LHPO_Grid();
    //        lbl_Att_LHPO_Heading.Text = "ATTACHED "+ CompanyManager.getCompanyParam().LHPOCaption + " DETAILS";
    //    }
    //}
    public void FillAttachedLHPODetails()
    {
        tr_Error.Visible = false;
        if (Doc_Type == "LHPO")
        {
            fill_ATT_LHPO_Grid();
            lbl_Att_LHPO_Heading.Text = "ATTACHED " + CompanyManager.getCompanyParam().LHPOCaption + " DETAILS";
        }
    }
    #region getValues

    private void fill_ATT_LHPO_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@LHPO_Id", SqlDbType.Int, 0, Doc_No) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_Attached_LHPO_Details", objSqlParam, ref objDS);
          
        if (objDS.Tables[0].Rows.Count > 0)
        {
            tr_att_LHPO_Details.Visible = true;
            DG_GC_LHPO_Details.Columns[4].HeaderText = objDS.Tables[0].Rows[0]["AUSCaption"].ToString() + "/DDC No";
            DG_GC_LHPO_Details.Columns[6].HeaderText = objDS.Tables[0].Rows[0]["AUSCaption"].ToString() + "/DDC Date";

            BindLHPOGrid = objDS.Tables[0];
        }
        else
        {
            tr_att_LHPO_Details.Visible = false;
            tr_Error.Visible = true;
        }

        upnl_dg_lhpo.Update();
    }

    protected void DG_GC_LHPO_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton btn_Memo_No, btn_AUS_DDC_No, btn_Memo_UpdatedBy;
        HiddenField hdn_Memo_Id, hdn_AUSDDC_Id;
        int Memo_Type_Id;
        string Delivery_Type_Id;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                btn_Memo_No = (LinkButton)(e.Item.FindControl("btn_Memo_No"));
                btn_AUS_DDC_No = (LinkButton)(e.Item.FindControl("btn_AUS_DDC_No"));
                btn_Memo_UpdatedBy = (LinkButton)(e.Item.FindControl("btn_Memo_UpdatedBy"));
                hdn_Memo_Id = (HiddenField)(e.Item.FindControl("hdn_Memo_Id"));
                hdn_AUSDDC_Id = (HiddenField)(e.Item.FindControl("hdn_AUSDDC_Id"));

                Memo_Type_Id = Util.String2Int(((HiddenField)(e.Item.FindControl("hdn_Memo_Type_Id"))).Value);
                Delivery_Type_Id = ((HiddenField)(e.Item.FindControl("hdn_DelType_Id"))).Value;

                btn_Memo_No.Attributes.Add("onclick", "return viewwindow('MEMO','" + hdn_Memo_Id.Value + "')");
                btn_Memo_UpdatedBy.Attributes.Add("onclick", "return view_userdetails('" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "memo_UpdatedBy_id").ToString())) + "')");

                if (Memo_Type_Id == 1)
                {
                    btn_AUS_DDC_No.Attributes.Add("onclick", "return viewwindow('AUS','" + hdn_AUSDDC_Id.Value + "')");
                }
                else if (Memo_Type_Id == 2)
                {
                    btn_AUS_DDC_No.Attributes.Add("onclick", "return viewwindow_ddc('" + Delivery_Type_Id + "','" + hdn_AUSDDC_Id.Value + "')");
                }
            }
        }
    }

    #endregion

}
