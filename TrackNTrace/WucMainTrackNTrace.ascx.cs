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

public partial class TrackNTrace_WucMainTrackNTrace : System.Web.UI.UserControl
{
    #region declearaion
    private DAL objDAL = new DAL();
    string Document_No, Document_Type, Document_SubType;
    Common objComm = new Common();
    DataSet objDS = new DataSet();
    string CallFrom = "";

    #endregion

    #region documet setvalues

    public string Doc_No
    {
        get { return Document_No; }
        set { Document_No = value; }
    }
    public string Doc_Type
    {
        get { return Document_Type; }
        set { Document_Type = value; }
    }
    public string Doc_SubType
    {
        get { return Document_SubType; }
        set { Document_SubType = value; }
    }
    private string Doc_No_For_GC       // For Integer value {GC,Booking MR,Delivery MR}
    {
        get { return Util.String2Int(txt_GC_No.Text.Trim()).ToString() == "-1" ? "0" : txt_GC_No.Text.Trim(); }
    }
    public int DocNoForFill
    {
        get { return Util.String2Int(ddl_GC_No.SelectedValue); }
        set { ddl_GC_No.SelectedValue = value.ToString(); }
    }  
    private int Doc_Id
    {
        get { return Util.String2Int(DDL_Select_No.SelectedValue); }
    }

    private int Delivery_Type_Id
    {
        get { return Util.String2Int(ddl_Delivery_type.SelectedValue); }
        set { ddl_Delivery_type.SelectedValue = Util.Int2String(value); }
    } 

    public string Set_Cancelled_Messase
    {
        set { lbl_Cancelled_Msg.Text = value; }
    }

    public string CheckNumeric(string TxtValue)
    {
        if (Util.String2Int(TxtValue) < 0)
        {
            return "0";
        }
        else
        {
            return TxtValue;
        }
    }

    #endregion

    #region control event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           fill_delivery_type();
           ddl_GC_No.Visible = false;
           Call_On_PageLoad_For_Disable();
           txt_GC_No.Focus();

           btn_SMS.Attributes.Add("onclick", "return SendStatusSMS('" + ddl_GC_No.SelectedValue  + "')");
        }

        Set_Cancelled_Label_Referance();
        set_Caption_type();
        On_Page_Load();
    }

    private void On_Page_Load()
    {
        if (hdn_check_for_show.Value == "1")
        {
            CallFrom = "Self";
            if (Doc_Id == 5 || Doc_Id == 6 || Doc_Id == 7)  // 0- GC No ,5- Book MR,6- Del MR,7- Credit Memo //Doc_Id == 0 || 
            {
                Doc_No = CheckNumeric(txt_GC_No.Text.Trim());
            }
            else
            {
                Doc_No = txt_GC_No.Text.Trim();
            }
            Doc_Type = Set_Doc_Type_For_Show();
            Set_Doc_Type_And_Doc_No();
        }
        else if (Request.QueryString["Doc_Type"] != null && Request.QueryString["Doc_No"] != null)
        {
            Doc_Type = Request.QueryString["Doc_Type"];
            Doc_No = Request.QueryString["Doc_No"];
            Doc_SubType = Request.QueryString["Doc_SubType"];
            CallFrom = "Out";

            if (Request.QueryString["Doc_Type"] == "DDC" && Request.QueryString["DeliveryType_Id"] != null)
            {
                Delivery_Type_Id = Util.String2Int(Request.QueryString["DeliveryType_Id"].ToString());
            }
            Set_ddl_and_Textbox(Doc_Type);

            Fill_Document_No();
            ddl_GC_No.SelectedValue = Doc_No;
            Set_Textbox();
            Set_Doc_Type_And_Doc_No();
            Call_All_User_Control();
            CurrentStatus();
            CheckShort();

        }
        else if (Request.QueryString["Doc_Type"] == null && Request.QueryString["Doc_No"] == null)
        {
            CallFrom = "Self";
            if (Doc_Id == 5 || Doc_Id == 6 || Doc_Id == 7) // 0- GC No ,5- Book MR,6- Del MR,7- Credit Memo //Doc_Id == 0 || 
            {
                Doc_No = CheckNumeric(txt_GC_No.Text.Trim());
            }
            else
            {
                Doc_No = txt_GC_No.Text.Trim();
            }
            Doc_Type = Set_Doc_Type_For_Show();
            Set_Doc_Type_And_Doc_No();
        }

        Set_Tab_Enable_Disable(Doc_Type);
    }

    protected void btn_Show_Click(object sender, EventArgs e)
    {
        CallFrom = "Self";

        if (Doc_Id == 5 || Doc_Id == 6 || Doc_Id == 7) //  0- GC No ,5- Book MR,6- Del MR ,7- Credit Memo //Doc_Id == 0 ||
        {
            Doc_No = CheckNumeric(txt_GC_No.Text.Trim());
        }
        else
        {
            Doc_No = txt_GC_No.Text.Trim();
        }

        Fill_Document_No();
        Doc_Type = Set_Doc_Type_For_Show();
        Set_Doc_Type_And_Doc_No();
        Call_All_User_Control();
        CurrentStatus();
        CheckShort();
    }

    private void CheckShort()
    {
        int GC_ID = 0;
        if (ddl_GC_No.SelectedValue != "")
        {
            GC_ID = Convert.ToInt32(ddl_GC_No.SelectedValue);
        }
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@GC_Id", SqlDbType.Int,0, GC_ID ) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_Short_Details", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            lbl_Short.Visible = true;
            lbl_Short.Text = objDS.Tables[0].Rows[0]["Short"].ToString();
        }
        else
        {
            lbl_Short.Visible = false;
            lbl_Short.Visible = false;
        }
    }

    private void CurrentStatus()
    {
        int GC_ID = 0;
        if (ddl_GC_No.SelectedValue != "")
        {
            GC_ID = Convert.ToInt32(ddl_GC_No.SelectedValue);
        }
        SqlParameter[] objSqlParam ={objDAL.MakeInParams("@GC_Id", SqlDbType.Int,0, GC_ID ) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_Current_Status", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            lbl_Status.Visible = true;
            lbl_Status.Text = objDS.Tables[0].Rows[0]["Status"].ToString();
            if (lbl_Status.Text != "")
            {
                btn_SMS.Visible = true;
            }
            else
            {
                btn_SMS.Visible = false;
            }
        }
        else
        {
            lbl_Status.Visible = false;
            lbl_Status.Visible = false;
            btn_SMS.Visible = false;
            btn_SMS.Visible = false;
        }
    }

    protected void ddl_GC_No_SelectedIndexChanged(object sender, EventArgs e)
    {
        DocNoForFill = Util.String2Int(ddl_GC_No.SelectedValue);
        Doc_Type = Set_Doc_Type_For_Show();
        Set_Doc_Type_And_Doc_No();

        Call_All_User_Control();
    }

    private void Fill_Document_No()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Doc_No_for", SqlDbType.VarChar, 20, Doc_No),
        objDAL.MakeInParams("@Call_From", SqlDbType.VarChar, 20, CallFrom),
        objDAL.MakeInParams("@Doc_Id", SqlDbType.Int, 0, Doc_Id),
         objDAL.MakeInParams("@YearCode", SqlDbType.Int, 0, UserManager.getUserParam().YearCode)};

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_Fill_GC_No", objSqlParam, ref objDS);

        ddl_GC_No.DataSource = objDS;
        ddl_GC_No.DataTextField = "Doc_No";
        ddl_GC_No.DataValueField = "Doc_ID";
        ddl_GC_No.DataBind();

        if (objDS.Tables[0].Rows.Count <= 1)
            ddl_GC_No.Visible = false;
        else
            ddl_GC_No.Visible = true;
    }    
    #endregion

    #region Fill DeliveryType

    private void Call_On_PageLoad_For_Disable()
    {
        WucTrackNTraceGeneralDetails1.setVisibleOnPageLoad = false;
        WucTrackNTraceCrossingDetails1.setVisibleOnPageLoad = false;
        WucTrackNTraceDeliveryDetails1.setVisibleOnPageLoad = false;
        WucTrackNTraceStatusDetails1.setVisibleOnPageLoad = false;
        WucPODTrackNTraceMovementDetails.setVisibleOnPageLoad = false;
        WucPODTrackNTraceCoverDeliveryDetails.setVisibleOnPageLoad = false;
        WucTrackNTraceFinanceDetails1.setVisibleOnPageLoad = false;
        WucTrackNTracePDSDeliveryDetails1.setVisibleOnPageLoad = false; 

        if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "excel")
        {
            DDL_Select_No.Items.Insert(1, new ListItem("Private Mark", "15"));
        }
    }

    private void Call_All_User_Control()
    {
        WucTrackNTraceGeneralDetails1.FillGeneralDetails();
        WucTrackNTraceCrossingDetails1.FillCrossingDetails();
        WucTrackNTraceMemoDetails1.FillMemoDetails();
        WucTrackNTraceLHPODetails1.FillLHPODetails();
        WucTrackNTraceAUSDetails1.FillAUSDetails();
        WucTrackNTraceAttachedLHPODetails1.FillAttachedLHPODetails();
        WucTrackNTraceDeliveryDetails1.FillDeliveryDetails();
        WucTrackNTraceBookingMR1.FillBookMRDetails();
        WucTrackNTraceDeliveryMR1.FillDeliveryMRDetails();
        WucTrackNTraceCreditMemo1.FillCreditMemoDetails();
        WucTrackNTraceBillingDetails1.FillBillingDetails();
        WucTrackNTraceFinanceDetails1.FillFinanceDetails();
        WucTrackNTraceStatusDetails1.FillStatusDetails();
        WucPODTrackNTraceMovementDetails.FillPODMovementDetails();
        WucPODTrackNTraceCoverDeliveryDetails.FillPODCoverDeliveryDetails();
        WucPODTrackNTraceCoverGenerationDetails.FillPODCoverGenerationDetails();
        WucPODTrackNTraceCoverReceivedDetails.FillPODCoverReceivedDetails();
        WucTrackNTracePDSDeliveryDetails1.FillPDSDlyDetails();
    }

    private void Set_Cancelled_Label_Referance()
    {
        WucTrackNTraceGeneralDetails1.lbl_cancelled = lbl_Cancelled_Msg;
        WucTrackNTraceMemoDetails1.lbl_cancelled = lbl_Cancelled_Msg;
        WucTrackNTraceLHPODetails1.lbl_cancelled = lbl_Cancelled_Msg;
        WucTrackNTraceAUSDetails1.lbl_cancelled = lbl_Cancelled_Msg;
        WucTrackNTraceDeliveryDetails1.lbl_cancelled = lbl_Cancelled_Msg;
        WucTrackNTraceBookingMR1.lbl_cancelled = lbl_Cancelled_Msg;
        WucTrackNTraceDeliveryMR1.lbl_cancelled = lbl_Cancelled_Msg;
        WucTrackNTraceCreditMemo1.lbl_cancelled = lbl_Cancelled_Msg;
        WucTrackNTraceBillingDetails1.lbl_cancelled = lbl_Cancelled_Msg;
        WucPODTrackNTraceCoverGenerationDetails.lbl_cancelled = lbl_Cancelled_Msg;
        WucPODTrackNTraceCoverReceivedDetails.lbl_cancelled = lbl_Cancelled_Msg;
        WucPODTrackNTraceCoverDeliveryDetails.lbl_cancelled = lbl_Cancelled_Msg;
        WucTrackNTracePDSDeliveryDetails1.lbl_cancelled = lbl_Cancelled_Msg;
    }

    private void fill_delivery_type()
    {
        ddl_Delivery_type.DataSource = objComm.Get_Values_Where("ec_master_ddc_Type", "GDC_Type_ID,GDC_Type", "", "GDC_Type", false);
        ddl_Delivery_type.DataTextField="GDC_Type";
        ddl_Delivery_type.DataValueField="GDC_Type_ID";
        ddl_Delivery_type.DataBind();
    }

    private void set_Caption_type()
    {
        DataSet ds = new DataSet();
        ds = objComm.Get_Values_Where("EC_Master_Company_Parameters", "GC_Caption,LHPO_Caption,AUS_Caption", "", "Company_Parameters_ID", false);

        DDL_Select_No.Items[0].Text = ds.Tables[0].Rows[0]["GC_Caption"].ToString() + " No";

        if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "excel")
        {
            DDL_Select_No.Items[3].Text = ds.Tables[0].Rows[0]["LHPO_Caption"].ToString() + " No";
            DDL_Select_No.Items[4].Text = ds.Tables[0].Rows[0]["AUS_Caption"].ToString() + " No";
        }
        else
        {
            DDL_Select_No.Items[2].Text = ds.Tables[0].Rows[0]["LHPO_Caption"].ToString() + " No";
            DDL_Select_No.Items[3].Text = ds.Tables[0].Rows[0]["AUS_Caption"].ToString() + " No";
        }

        TB_TrackNTrace.Tabs[3].Text = ds.Tables[0].Rows[0]["LHPO_Caption"].ToString() + " Details";
        TB_TrackNTrace.Tabs[9].Text = "Attached " + ds.Tables[0].Rows[0]["LHPO_Caption"].ToString() + " Details";
        TB_TrackNTrace.Tabs[4].Text = ds.Tables[0].Rows[0]["AUS_Caption"].ToString() + " Details";
    }  

    #endregion

    #region set document type
    private void Set_Doc_Type_And_Doc_No()
    {
        if (Doc_Type == "GC" || Doc_Id == 0)
        {
            WucTrackNTraceGeneralDetails1.Doc_No = DocNoForFill;
            WucTrackNTraceGeneralDetails1.Doc_Type = Doc_Type;
            WucTrackNTraceCrossingDetails1.Doc_No = DocNoForFill;
            WucTrackNTraceCrossingDetails1.Doc_Type = Doc_Type;
            WucTrackNTraceStatusDetails1.Doc_No = DocNoForFill;
            WucTrackNTraceStatusDetails1.Doc_Type = Doc_Type;
            WucTrackNTraceDeliveryDetails1.Doc_No = DocNoForFill;
            WucTrackNTraceDeliveryDetails1.Doc_Type = Doc_Type;
            WucTrackNTraceFinanceDetails1.Doc_No = DocNoForFill;
            WucTrackNTraceFinanceDetails1.Doc_Type = Doc_Type;
            WucPODTrackNTraceMovementDetails.Doc_No = DocNoForFill;
            WucPODTrackNTraceMovementDetails.Doc_Type = Doc_Type;
            WucPODTrackNTraceCoverDeliveryDetails.Doc_No = DocNoForFill;
            WucPODTrackNTraceCoverDeliveryDetails.Doc_Type = Doc_Type;
            WucTrackNTracePDSDeliveryDetails1.Doc_No = DocNoForFill;
            WucTrackNTracePDSDeliveryDetails1.Doc_Type = Doc_Type;
        }
        else if (Doc_Type == "MEMO" || Doc_Id == 1)
        {
            WucTrackNTraceMemoDetails1.Doc_No = DocNoForFill;
            WucTrackNTraceMemoDetails1.Doc_Type = Doc_Type;
        }
        else if (Doc_Type == "LHPO" || Doc_Id == 2)
        {
            WucTrackNTraceLHPODetails1.Doc_No = DocNoForFill;
            WucTrackNTraceLHPODetails1.Doc_Type = Doc_Type;
            WucTrackNTraceAttachedLHPODetails1.Doc_No = DocNoForFill;
            WucTrackNTraceAttachedLHPODetails1.Doc_Type = Doc_Type;
            WucTrackNTraceFinanceDetails1.Doc_No = DocNoForFill;
            WucTrackNTraceFinanceDetails1.Doc_Type = Doc_Type;
        }
        else if (Doc_Type == "AUS" || Doc_Id == 3)
        {
            WucTrackNTraceAUSDetails1.Doc_No = DocNoForFill;
            WucTrackNTraceAUSDetails1.Doc_Type = Doc_Type;
        }
        else if (Doc_Type == "DDC" || Doc_Id == 4)
        {
            WucTrackNTraceDeliveryDetails1.Doc_No = DocNoForFill;
            WucTrackNTraceDeliveryDetails1.Doc_Type = Doc_Type;
            WucTrackNTraceDeliveryDetails1.DeliveryType_ID = Delivery_Type_Id;
        }
        else if (Doc_Type == "BKMR" || Doc_Id == 5)
        {
            WucTrackNTraceBookingMR1.Doc_No = DocNoForFill;
            WucTrackNTraceBookingMR1.Doc_Type = Doc_Type;
            WucTrackNTraceFinanceDetails1.Doc_No = DocNoForFill;
            WucTrackNTraceFinanceDetails1.Doc_Type = Doc_Type;
        }
        else if (Doc_Type == "DLMR" || Doc_Id == 6)
        {
            WucTrackNTraceDeliveryMR1.Doc_No = DocNoForFill;
            WucTrackNTraceDeliveryMR1.Doc_Type = Doc_Type;
            WucTrackNTraceFinanceDetails1.Doc_No = DocNoForFill;
            WucTrackNTraceFinanceDetails1.Doc_Type = Doc_Type;
        }
        else if (Doc_Type == "CMEMO" || Doc_Id == 7)
        {
            WucTrackNTraceCreditMemo1.Doc_No = DocNoForFill;
            WucTrackNTraceCreditMemo1.Doc_Type = Doc_Type;
            WucTrackNTraceFinanceDetails1.Doc_No = DocNoForFill;
            WucTrackNTraceFinanceDetails1.Doc_Type = Doc_Type;
        }
        else if (Doc_Type == "BILL" || Doc_Id == 8)
        {
            WucTrackNTraceBillingDetails1.Doc_No = DocNoForFill;
            WucTrackNTraceBillingDetails1.Doc_Type = Doc_Type;
            WucTrackNTraceFinanceDetails1.Doc_No = DocNoForFill;
            WucTrackNTraceFinanceDetails1.Doc_Type = Doc_Type;
        }
        else if (Doc_Type == "PODCG" || Doc_Id == 16)
        {
            WucPODTrackNTraceCoverGenerationDetails.Doc_No = DocNoForFill;
            WucPODTrackNTraceCoverGenerationDetails.Doc_Type = Doc_Type;
        }
        else if (Doc_Type == "PODCR" || Doc_Id == 17)
        {
            WucPODTrackNTraceCoverReceivedDetails.Doc_No = DocNoForFill;
            WucPODTrackNTraceCoverReceivedDetails.Doc_Type = Doc_Type;
        }
        else if (Doc_Type == "PODCD" || Doc_Id == 18)
        {
            WucPODTrackNTraceCoverDeliveryDetails.Doc_No = DocNoForFill;
            WucPODTrackNTraceCoverDeliveryDetails.Doc_Type = Doc_Type;
        }
        else if (Doc_Type == "PDS" || Doc_Id == 19)
        {
            WucTrackNTracePDSDeliveryDetails1.Doc_No = DocNoForFill;
            WucTrackNTracePDSDeliveryDetails1.Doc_Type = Doc_Type; 
        }
    }
  #endregion

    #region Set_Tab_Enable_Disable
    private void Set_Tab_Enable_Disable(bool v1, bool v2, bool v3, bool v4, bool v5, bool v6, bool v7, bool v8, bool v9, bool v10, bool v11, bool v12, bool v13, bool v14, bool v15, bool v16, bool v17, bool v18)
    {
        TB_TrackNTrace.Tabs[0].Visible = v1;
        TB_TrackNTrace.Tabs[1].Visible = v2;
        TB_TrackNTrace.Tabs[2].Visible = v3;
        TB_TrackNTrace.Tabs[3].Visible = v4;
        TB_TrackNTrace.Tabs[4].Visible = v5;
        TB_TrackNTrace.Tabs[5].Visible = v6;
        TB_TrackNTrace.Tabs[6].Visible = v7;
        TB_TrackNTrace.Tabs[7].Visible = v8;
        TB_TrackNTrace.Tabs[8].Visible = v9;
        TB_TrackNTrace.Tabs[9].Visible = v10;
        TB_TrackNTrace.Tabs[10].Visible = v11;
        TB_TrackNTrace.Tabs[11].Visible = v12;
        TB_TrackNTrace.Tabs[12].Visible = v13;
        TB_TrackNTrace.Tabs[13].Visible = v14;
        TB_TrackNTrace.Tabs[14].Visible = v15;
        TB_TrackNTrace.Tabs[15].Visible = v16;
        TB_TrackNTrace.Tabs[16].Visible = v17;
        TB_TrackNTrace.Tabs[17].Visible = v18;
    }   

    private void Set_Tab_Enable_Disable(string Doc_Type)
    {
        if (Doc_Type == "GC")
        {
            Set_Tab_Enable_Disable(true, true, false, false, false, true, true, false, false, false, true, false, false, true, false, false, true, false);
            if (Doc_SubType == "STATUS")
            {
                MP_TrackNTrace.SelectedIndex = 6;
                TB_TrackNTrace.SelectedTab = TB_TrackNTrace.Tabs[6];
            }
            else
            {
                MP_TrackNTrace.SelectedIndex = 0;
                TB_TrackNTrace.SelectedTab = TB_TrackNTrace.Tabs[0];
            }
        }
        else if (Doc_Type == "MEMO")
        {
            Set_Tab_Enable_Disable(false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
            MP_TrackNTrace.SelectedIndex = 2;
            TB_TrackNTrace.SelectedTab = TB_TrackNTrace.Tabs[2];
        }
        else if (Doc_Type == "LHPO")
        {
            Set_Tab_Enable_Disable(false, false, false, true, false, false, false, false, false, true, false, false, false, false, false, false, true, false);
            MP_TrackNTrace.SelectedIndex = 3;
            TB_TrackNTrace.SelectedTab = TB_TrackNTrace.Tabs[3];
        }
        else if (Doc_Type == "AUS")
        {
            Set_Tab_Enable_Disable(false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false);
            MP_TrackNTrace.SelectedIndex = 4;
            TB_TrackNTrace.SelectedTab = TB_TrackNTrace.Tabs[4];
        }
        else if (Doc_Type == "DDC")
        {
            Set_Tab_Enable_Disable(false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false);
            MP_TrackNTrace.SelectedIndex = 5;
            TB_TrackNTrace.SelectedTab = TB_TrackNTrace.Tabs[5];
        }
        else if (Doc_Type == "BKMR")
        {
            Set_Tab_Enable_Disable(false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, true, false);
            MP_TrackNTrace.SelectedIndex = 7;
            TB_TrackNTrace.SelectedTab = TB_TrackNTrace.Tabs[7];
        }
        else if (Doc_Type == "DLMR")
        {
            Set_Tab_Enable_Disable(false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, true, false);
            MP_TrackNTrace.SelectedIndex = 8;
            TB_TrackNTrace.SelectedTab = TB_TrackNTrace.Tabs[8];
        }
        else if (Doc_Type == "CMEMO")
        {
            Set_Tab_Enable_Disable(false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, true, false);
            MP_TrackNTrace.SelectedIndex = 14;
            TB_TrackNTrace.SelectedTab = TB_TrackNTrace.Tabs[14];
        }
        else if (Doc_Type == "BILL")
        {
            Set_Tab_Enable_Disable(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, true, false);
            MP_TrackNTrace.SelectedIndex = 15;
            TB_TrackNTrace.SelectedTab = TB_TrackNTrace.Tabs[15];
        }
        else if (Doc_Type == "PODCG")
        {
            Set_Tab_Enable_Disable(false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false);
            MP_TrackNTrace.SelectedIndex = 11;
            TB_TrackNTrace.SelectedTab = TB_TrackNTrace.Tabs[11];
        }
        else if (Doc_Type == "PODCR")
        {
            Set_Tab_Enable_Disable(false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false);
            MP_TrackNTrace.SelectedIndex = 12;
            TB_TrackNTrace.SelectedTab = TB_TrackNTrace.Tabs[12];
        }
        else if (Doc_Type == "PODCD")
        {
            Set_Tab_Enable_Disable(false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false);
            MP_TrackNTrace.SelectedIndex = 13;
            TB_TrackNTrace.SelectedTab = TB_TrackNTrace.Tabs[13];
        }
        else if (Doc_Type == "PDS")
        {
            Set_Tab_Enable_Disable(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true);
            MP_TrackNTrace.SelectedIndex = 17;
            TB_TrackNTrace.SelectedTab = TB_TrackNTrace.Tabs[17];
        }
    }
    #endregion

    #region set initial document type
    private string Set_Doc_Type_For_Show()
    {
        string DocType ="";

        if (Doc_Id == 0 || Doc_Id == 15)
        {
            DocType = "GC";
        }
        else if (Doc_Id == 1)
        {
            DocType = "MEMO";
        }
        else if (Doc_Id == 2)
        {
            DocType = "LHPO";
        }
        else if (Doc_Id == 3)
        {
            DocType = "AUS";
        }
        else if (Doc_Id == 4)
        {
            DocType = "DDC";
        }
        else if (Doc_Id == 5)
        {
            DocType = "BKMR";
        }
        else if (Doc_Id == 6)
        {
            DocType = "DLMR";
        }
        else if (Doc_Id == 7)
        {
            DocType = "CMEMO";
        }
        else if (Doc_Id == 8)
        {
            DocType = "BILL";
        }
        else if (Doc_Id == 16)
        {
            DocType = "PODCG";
        }
        else if (Doc_Id == 17)
        {
            DocType = "PODCR";
        }
        else if (Doc_Id == 18)
        {
            DocType = "PODCD";
        }
        else if (Doc_Id == 19)
        {
            DocType = "PDS";
        }
        return DocType;
    }

    #endregion

    #region set ddl and textbox
    private void Set_ddl_and_Textbox(string DocType)
    {
        if (DocType == "GC" || DocType == "STATUS")
        {
            DDL_Select_No.SelectedValue = "0";
        }
        else if (DocType == "MEMO")
        {
            DDL_Select_No.SelectedValue = "1";
        }
        else if (DocType == "LHPO")
        {
            DDL_Select_No.SelectedValue = "2";
        }
        else if (DocType == "AUS")
        {
            DDL_Select_No.SelectedValue = "3";
        }
        else if (DocType == "DDC")
        {
            DDL_Select_No.SelectedValue = "4";
        }
        else if (DocType == "BKMR")
        {
            DDL_Select_No.SelectedValue = "5";
        }
        else if (DocType == "DLMR")
        {
            DDL_Select_No.SelectedValue = "6";
        }
        else if (DocType == "CMEMO")
        {
            DDL_Select_No.SelectedValue = "7";
        }
        else if (DocType == "BILL")
        {
            DDL_Select_No.SelectedValue = "8";
        }
        else if (DocType == "PODCG")
        {
            DDL_Select_No.SelectedValue = "16";
        }
        else if (DocType == "PODCR")
        {
            DDL_Select_No.SelectedValue = "17";
        }
        else if (DocType == "PODCD")
        {
            DDL_Select_No.SelectedValue = "18";
        }
        else if (DocType == "PDS")
        {
            DDL_Select_No.SelectedValue = "19";
        }
    }

    private void Set_Textbox()
    {
        txt_GC_No.Text = ddl_GC_No.SelectedItem.Text;        
    }

    #endregion  
  
}
