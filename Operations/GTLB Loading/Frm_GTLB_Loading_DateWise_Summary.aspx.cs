using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Operations_GTLB_Loading_Frm_GTLB_Loading_DateWise_Summary : ClassLibraryMVP.UI.Page
{

    #region Declaration

    private DataSet ds, dsMonth;
    private DAL objDAL = new DAL();

    DropDownList ddl_Remark;


    #endregion

    public DataTable Session_HolidayRemarkDdl
    {
        get { return StateManager.GetState<DataTable>("HolidayRemarkDdl"); }
        set { StateManager.SaveState("HolidayRemarkDdl", value); }
    }

    public DataTable SessionBindGrid
    {
        get { return StateManager.GetState<DataTable>("GetDetails"); }
        set { StateManager.SaveState("GetDetails", value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            fillHolidayRemark();

            Fill_Month();

        }


        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/Operations/GTLB Loading/Frm_Rpt_GTLB_Loading_DateWise_Summary.aspx?");

        btnReport.Attributes.Add("onclick", "return OpenReport('" + Path + "');");


    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        GetDetails("form_and_pageload", e);
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {

        //StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());
        //PathF4 = new StringBuilder(Util.GetBaseURL());
        //PathF4.Append("/Operations/GTLB Loading/Frm_Rpt_GTLB_Loading_DateWise_Summary.aspx?MonthId=" + ddl_Month.SelectedValue + "&Period=" + ddl_Period.SelectedValue);
        //btnReport.Attributes.Add("onclick", "return OpenReport('" + PathF4 + "')");
    }

    private void fillHolidayRemark()
    {
        objDAL.RunProc("[dbo].[EC_Opr_GTLB_LoadingDetails_Fill_HoliDayRemark]", ref ds);
        Session_HolidayRemarkDdl = ds.Tables[0];

    }

    public DataTable BindHolidayRemark
    {
        set { Set_Common_DDL(ddl_Remark, "Remark", "RemarkID", value, true); }
    }

    private void Set_Common_DDL(DropDownList DDl, string TextField, string ValueField, DataTable DT, bool Is_ZeroInex)
    {
        DDl.DataSource = DT;
        DDl.DataTextField = TextField;
        DDl.DataValueField = ValueField;
        DDl.DataBind();
        //if (Is_ZeroInex)
        //    DDl.Items.Insert(0, new ListItem("Select One", "0"));
    }


    #region grid operation

    public void GetDetails(object sender, EventArgs e)
    {

        string CallFrom = (string)(sender);

        int ddlMonth = 4, ddlPeriod = 1;

        ddlMonth = Util.String2Int(ddl_Month.SelectedValue);
        ddlPeriod = Util.String2Int(ddl_Period.SelectedValue);



        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeInParams("@MonthID",SqlDbType.Int,0,ddlMonth),
            objDAL.MakeInParams("@Period",SqlDbType.Int,0,ddlPeriod)};

        objDAL.RunProc("dbo.EC_Opr_GTLB_LoadingDetails_DateWise_Summary", objSqlParam, ref ds);

        SessionBindGrid = ds.Tables[0];

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lblErrors);
    }

    #endregion

    private void Fill_Month()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@MonthID", SqlDbType.Int,0,0) 
        };

        objDAL.RunProc("EC_Master_Get_Finacial_Month", objSqlParam, ref dsMonth);

        ddl_Month.DataSource = dsMonth;
        ddl_Month.DataTextField = "MonthName";
        ddl_Month.DataValueField = "MonthID";
        ddl_Month.DataBind();
    }

    protected void dg_Details_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {


        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            ComponentArt.Web.UI.Calendar dtp_DateToBeIncluded;

            dtp_DateToBeIncluded = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_DateToBeIncluded")); ;

            dtp_DateToBeIncluded.SelectedDate = Convert.ToDateTime(SessionBindGrid.Rows[e.Item.ItemIndex]["ToBeIncludeDate"].ToString());


            ddl_Remark = (DropDownList)(e.Item.FindControl("ddl_Remark"));
            BindHolidayRemark = Session_HolidayRemarkDdl;

            TextBox txt_Remark;

            txt_Remark = (TextBox)(e.Item.FindControl("txt_Remark"));

            CheckBox chk_IsHoliday = (CheckBox)e.Item.FindControl("chk_IsHoliday");


            DataRow DR = null;
            DataTable dt = SessionBindGrid;//for grid topic
            DR = dt.Rows[e.Item.ItemIndex];
            ddl_Remark.SelectedValue = DR["RemarkID"].ToString();


            if (Util.String2Int(DR["RemarkID"].ToString()) == 1)
            {
                chk_IsHoliday.Enabled = false;
                ddl_Remark.Visible = false;
                txt_Remark.Visible = true;
            }


            if (chk_IsHoliday.Checked == true && (ddl_Remark.SelectedValue == "0" || ddl_Remark.SelectedValue == "2" || ddl_Remark.SelectedValue == "3"))
            {
                ddl_Remark.Enabled = true;
            }

            if (dtp_DateToBeIncluded.SelectedDate != Convert.ToDateTime("2020-01-01 00:00:00.000"))
            {
                dtp_DateToBeIncluded.Visible = true;
            }
            else
            {
                dtp_DateToBeIncluded.Visible = false;
            }

            chk_IsHoliday.Attributes.Add("onclick", "checkuncheckIsHoliday('" + chk_IsHoliday.ClientID + "','" + ddl_Remark.ClientID  + "');");


            LinkButton lbtnDate;
            lbtnDate = (LinkButton)(e.Item.FindControl("lbtnDate"));

            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Operations/GTLB Loading/Frm_GTLB_Loading_DateWise_Details.aspx?Date=" + lbtnDate.Text);

            if (chk_IsHoliday.Checked == false)
            {
                lbtnDate.Attributes.Add("onclick", "return GTLB_LoadingDetails('" + PathF4 + "')");
            }

            if (SessionBindGrid.Rows[e.Item.ItemIndex]["Status"].ToString() == "COMPLETE")
            {
                e.Item.BackColor = System.Drawing.Color.GreenYellow;
            }

            if (Util.String2Bool(SessionBindGrid.Rows[e.Item.ItemIndex]["CanEdit"].ToString()) == false)
            {
                chk_IsHoliday.Enabled = false;
                ddl_Remark.Enabled = false;
                dtp_DateToBeIncluded.Enabled = false;
            }

            LinkButton lbtnFORM1;
            lbtnFORM1 = (LinkButton)(e.Item.FindControl("lbtnFORM1"));

            

            if (chk_IsHoliday.Checked == false && SessionBindGrid.Rows[e.Item.ItemIndex]["Status"].ToString() == "COMPLETE")
            {
                StringBuilder PathFORM1 = new StringBuilder(Util.GetBaseURL());
                PathFORM1 = new StringBuilder(Util.GetBaseURL());
                PathFORM1.Append("/Operations/GTLB Loading/Frm_Rpt_GTLB_Loading_FORM_1_Viewer.aspx?LoadingDate=" + lbtnDate.Text);

                lbtnFORM1.Attributes.Add("onclick", "return Open_FORM1_Window('" + PathFORM1 + "')");
            }


        }
    }

    protected void ddl_Remark_SelectedIndexChanged(object sender, EventArgs e)
    {
       

        ddl_Remark = (DropDownList)sender;
        DataGridItem _dg_Details = (DataGridItem)ddl_Remark.Parent.Parent;

        LinkButton lbtnDate;

        lbtnDate = (LinkButton)(_dg_Details.FindControl("lbtnDate"));

        ComponentArt.Web.UI.Calendar dtp_DateToBeIncluded;

        dtp_DateToBeIncluded = (ComponentArt.Web.UI.Calendar)(_dg_Details.FindControl("dtp_DateToBeIncluded"));

        if (Convert.ToInt32(ddl_Remark.SelectedValue) == 3)
        {

            if (PublicHolidayLoadingDate(lbtnDate.Text) != "")
            {
                dtp_DateToBeIncluded.SelectedDate = Convert.ToDateTime(PublicHolidayLoadingDate(lbtnDate.Text));
            }
            else
            {
                dtp_DateToBeIncluded.Visible = false;
            }
        }
        else
        {
            dtp_DateToBeIncluded.Visible = false;
        }


        if (Convert.ToInt32(ddl_Remark.SelectedValue) == 3 && dtp_DateToBeIncluded.SelectedDate != Convert.ToDateTime("2020-01-01 00:00:00.000"))
        {
            dtp_DateToBeIncluded.Visible = true;
        }
        else
        {
            dtp_DateToBeIncluded.Visible = false;
        }

        CheckBox chk_IsHoliday;
        chk_IsHoliday = (CheckBox)(_dg_Details.FindControl("chk_IsHoliday"));

        if (chk_IsHoliday.Checked == true && Convert.ToInt32(ddl_Remark.SelectedValue) != 1)
        {
            ddl_Remark.Enabled = true;
        }
        else
        {
            ddl_Remark.Enabled = false;
        }
    }


    public string PublicHolidayLoadingDate(string lDate)
    {
        String ToBeIncludeDate;


        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Date", SqlDbType.VarChar, 20, lDate) };

        objDAL.RunProc("dbo.EC_Opr_GTLB_LoadingDetails_Get_HolidateLoadingDate", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ToBeIncludeDate = ds.Tables[0].Rows[0]["HolidateLoadingDate"].ToString();
        }
        else
        {
            ToBeIncludeDate = "";
        }
        return ToBeIncludeDate;
    }



    private void SetSessionDetailsFromGrid()
    {

        int i;

        LinkButton lbtnDate, lbtn_ToBeIncludeDate;
        Label lblStatus;
        CheckBox chk_IsHoliday;
        TextBox txt_Remark;

        ComponentArt.Web.UI.Calendar dtp_DateToBeIncluded;

        dtp_DateToBeIncluded = (ComponentArt.Web.UI.Calendar)(dg_Details.FindControl("dtp_DateToBeIncluded"));

        if (dg_Details.Items.Count > 0)
        {
            for (i = 0; i <= dg_Details.Items.Count - 1; i++)
            {

                lbtnDate = (LinkButton)dg_Details.Items[i].FindControl("lbtnDate");
                lblStatus = (Label)dg_Details.Items[i].FindControl("lblStatus");
                chk_IsHoliday = (CheckBox)dg_Details.Items[i].FindControl("chk_IsHoliday");
                ddl_Remark = (DropDownList)dg_Details.Items[i].FindControl("ddl_Remark");
                txt_Remark = (TextBox)dg_Details.Items[i].FindControl("txt_Remark");

                dtp_DateToBeIncluded = (ComponentArt.Web.UI.Calendar)(dg_Details.Items[i].FindControl("dtp_DateToBeIncluded"));

                SessionBindGrid.Rows[i]["IsHoliday"] = chk_IsHoliday.Checked;


                if (txt_Remark.Visible == true)
                {
                    SessionBindGrid.Rows[i]["RemarkID"] = "1";
                    SessionBindGrid.Rows[i]["Remark"] = txt_Remark.Text;
                }
                else
                {
                    SessionBindGrid.Rows[i]["RemarkID"] = ddl_Remark.SelectedValue;
                    SessionBindGrid.Rows[i]["Remark"] = ddl_Remark.SelectedItem;
                }

                if (chk_IsHoliday.Checked == true)
                {
                    if ((ddl_Remark.SelectedValue == "0" || ddl_Remark.SelectedValue == "2") && txt_Remark.Visible == false )
                    {
                        SessionBindGrid.Rows[i]["ToBeIncludeDate"] = "01 Jan 2020";
                    }
                    else
                    {
                        SessionBindGrid.Rows[i]["ToBeIncludeDate"] = dtp_DateToBeIncluded.SelectedDate.ToString("dd MMM yyyy");
                    }
                }
                else
                {
                    SessionBindGrid.Rows[i]["ToBeIncludeDate"] = "01 Jan 2020";
                }
            }
        }
    }

    public String DetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBindGrid.Copy());
            _objDs.Tables[0].TableName = "SessionBindGrid";
            return _objDs.GetXml().ToLower();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        SetSessionDetailsFromGrid();

        if (AllowToSave())
        {
            Save();
        }

    }

    private bool grid_validation()
    {
        int i;
        bool ATS = true;

        TextBox txt_Remark;
        CheckBox chk_IsHoliday;

        if (dg_Details.Items.Count > 0)
        {
            for (i = 0; i <= dg_Details.Items.Count - 1; i++)
            {
                ddl_Remark = (DropDownList)dg_Details.Items[i].FindControl("ddl_Remark");
                txt_Remark = (TextBox)dg_Details.Items[i].FindControl("txt_Remark");
                chk_IsHoliday = (CheckBox)dg_Details.Items[i].FindControl("chk_IsHoliday");


                if (chk_IsHoliday.Checked == true && txt_Remark.Visible == false && Util.String2Int(ddl_Remark.SelectedValue) == 0)
                {
                    lblErrors.Text = "Please Select Holiday Remark";
                    ScriptManager.SetFocus(ddl_Remark);
                    ATS = false;
                    break;
                }
                else
                {
                    ATS = true;
                }
            }
        }
        return ATS;
    }

    private bool AllowToSave()
    {
        bool ATS = false;

        if (grid_validation() == false)
        {
            ATS = false;
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }

    private Message Save()
    {

        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),  
            objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,8000,DetailsXML), 
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.EC_Opr_GTLB_LoadingDetails_DateWise_Summary_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {


            String popupScript = "";
            string _Msg = "Saved SuccessFully";

            string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&DecryptUrl='No'");

            //System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(String), "PopupScript1", popupScript.ToString(), false);

        }
        else
        {
            lblErrors.Text = objMessage.message;
        }

        return objMessage;
    }
}
