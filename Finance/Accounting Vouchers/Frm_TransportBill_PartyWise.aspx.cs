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

public partial class Finance_Accounting_Vouchers_Frm_TransportBill_PartyWise : System.Web.UI.Page
{

    #region Declaration

    private DataSet ds;
    private DAL objDAL = new DAL();
    Raj.EC.Common objComm = new Raj.EC.Common();

    int Client_Id;

    #endregion

    public string errorMessage
    {
        set { lblErrors.Text = value; }
    }

    public DataTable SessionBindGridDetails
    {
        get { return StateManager.GetState<DataTable>("GetDetails"); }
        set { StateManager.SaveState("GetDetails", value); }
    }

    public DataTable SessionBindOtherCharges1
    {
        get { return StateManager.GetState<DataTable>("BindOtherCharges1"); }
        set { StateManager.SaveState("BindOtherCharges1", value); }
    }

    public DataTable SessionBindOtherCharges2
    {
        get { return StateManager.GetState<DataTable>("BindOtherCharges2"); }
        set { StateManager.SaveState("BindOtherCharges2", value); }
    }

    public string BillNo
    {
        set { lbl_TransBillNo.Text = value; }
        get { return lbl_TransBillNo.Text; }
    }

    public string TotalGC
    {
        set
        {
            hdn_TotalGC.Value = value;
            lbl_TotalGC.Text = value;
        }
    }

    public string TotalFreight
    {
        set
        {
            hdn_TotalFreight.Value = value;
            lbl_TotalFreight.Text = value;
        }
    }

    public string TotalOtherCharge1
    {
        set
        {
            hdn_TotalOtherCharge1.Value = value;
            lbl_TotalOtherCharge1.Text = value;
        }
    }

    public string TotalOtherCharge2
    {
        set
        {
            hdn_TotalOtherCharge2.Value = value;
            lbl_TotalOtherCharge2.Text = value;
        }
    }


    public string Total
    {
        set
        {
            hdn_Total.Value = value;
            lbl_Total.Text = value;
        }
    }

    public string TotalGST
    {
        set
        {
            hdn_TotalGST.Value = value;
            lbl_TotalGST.Text = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        btnSave.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btnSave));

        if (!IsPostBack)
        {

            string Crypt, Client_Name;

            Crypt = System.Web.HttpContext.Current.Request.QueryString["Client_Id"];

            if (Crypt != null)
            {
                Client_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

                hdn_Client_Id.Value = Client_Id.ToString();

                Crypt = System.Web.HttpContext.Current.Request.QueryString["Client_Name"];
                Client_Name = ClassLibraryMVP.Util.DecryptToString(Crypt);

                lbl_ClientName.Text = Client_Name;

                FillOtherCharges();

                GetDetails("form_and_pageload", e);

            }

            Next_Bill_Number();
        }

    }

    private void Next_Bill_Number()
    {
        BillNo = objComm.Get_Next_Number();
    }

    public void GetDetails(object sender, EventArgs e)
    {

        string CallFrom = (string)(sender);


        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0,Client_Id),
            objDAL.MakeInParams("@Branch_id",SqlDbType.Int ,0,UserManager.getUserParam().MainId),
            objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar  ,3,UserManager.getUserParam().HierarchyCode)
        };

        objDAL.RunProc("dbo.EC_FA_Transport_Bill_Fill_GCDetails_PartyWiseBill", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {

            hdn_OtherCharge1_Id.Value = ds.Tables[2].Rows[0]["Other_Charge1_Id"].ToString();
            hdn_OtherCharge2_Id.Value = ds.Tables[2].Rows[0]["Other_Charge2_Id"].ToString();

            SessionBindGridDetails = ds.Tables[0];

            Common objcommon = new Common();

            objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lblErrors);

            lbl_TotalGC.Text = ds.Tables[1].Rows[0]["TotalGC"].ToString();
            lbl_TotalFreight.Text = ds.Tables[1].Rows[0]["TotalFreight"].ToString();
            lbl_TotalOtherCharge1.Text = ds.Tables[1].Rows[0]["TotalOtherCharges1"].ToString();
            lbl_TotalOtherCharge2.Text = ds.Tables[1].Rows[0]["TotalOtherCharges2"].ToString();
            lbl_Total.Text = ds.Tables[1].Rows[0]["TotalBillAmount"].ToString();
            lbl_TotalGST.Text = ds.Tables[1].Rows[0]["TotalGST"].ToString();

        }
        else
        {
            btnSave.Visible = false;
            lblErrors.Text = "No Records Found";
        }

    }

    public void FillOtherCharges()
    {

        DAL objDAL = new DAL();

        objDAL.RunProc("EC_FA_Transport_Bill_Fill_Other_Charges", ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            SessionBindOtherCharges1 = ds.Tables[0];
            SessionBindOtherCharges2 = ds.Tables[0];

        }

    }



    protected void dg_Details_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {

            DropDownList ddl_OtherCharges1 = (DropDownList)e.Item.FindControl("ddl_OtherCharges1");
            DropDownList ddl_OtherCharges2 = (DropDownList)e.Item.FindControl("ddl_OtherCharges2");

            ddl_OtherCharges1.DataTextField = "Other_Charge";
            ddl_OtherCharges1.DataValueField = "Other_Charge_Id";

            ddl_OtherCharges2.DataTextField = "Other_Charge";
            ddl_OtherCharges2.DataValueField = "Other_Charge_Id";

            ddl_OtherCharges1.DataSource = SessionBindOtherCharges1;
            ddl_OtherCharges1.DataBind();

            ddl_OtherCharges2.DataSource = SessionBindOtherCharges2;
            ddl_OtherCharges2.DataBind();

            ddl_OtherCharges1.Items.Insert(0, new ListItem("Select One", "0"));
            ddl_OtherCharges2.Items.Insert(0, new ListItem("Select One", "0"));


            ddl_OtherCharges1.SelectedValue = hdn_OtherCharge1_Id.Value;
            ddl_OtherCharges2.SelectedValue = hdn_OtherCharge2_Id.Value;


        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            TextBox txtOtherCharge1 = (TextBox)e.Item.FindControl("txtOtherCharge1");
            TextBox txtOtherCharge2 = (TextBox)e.Item.FindControl("txtOtherCharge2");

            if (Util.String2Int(hdn_OtherCharge1_Id.Value) > 0)
            {
                txtOtherCharge1.Enabled = true;
            }

            if (Util.String2Int(hdn_OtherCharge2_Id.Value) > 0)
            {
                txtOtherCharge2.Enabled = true;
            }
        }

    }


    protected void ddl_OtherCharges1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl;

        TextBox txtOtherCharge1;
        ddl = (DropDownList)sender;

        int i;

        for (i = 0; i <= dg_Details.Items.Count - 1; i++)
        {

            txtOtherCharge1 = (TextBox)dg_Details.Items[i].FindControl("txtOtherCharge1");

            if (ddl.SelectedValue == "0")
            {
                hdn_OtherCharge1_Id.Value = "0";
                hdn_OtherCharge1.Value = "";

                txtOtherCharge1.Text = "0";
                txtOtherCharge1.Enabled = false;
            }
            else
            {
                hdn_OtherCharge1_Id.Value = ddl.SelectedValue;
                hdn_OtherCharge1.Value = ddl.SelectedItem.Text;

                txtOtherCharge1.Enabled = true;
            }
        }

        CalculateTotal();
        
    }

    protected void ddl_OtherCharges2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl;

        TextBox txtOtherCharge2;
        ddl = (DropDownList)sender;

        int i;

        for (i = 0; i <= dg_Details.Items.Count - 1; i++)
        {

            txtOtherCharge2 = (TextBox)dg_Details.Items[i].FindControl("txtOtherCharge2");


            if (ddl.SelectedValue == "0")
            {
                hdn_OtherCharge2_Id.Value = "0";
                hdn_OtherCharge2.Value = "";

                txtOtherCharge2.Text = "0";
                txtOtherCharge2.Enabled = false;
            }
            else
            {
                hdn_OtherCharge2_Id.Value = ddl.SelectedValue;
                hdn_OtherCharge2.Value = ddl.SelectedItem.Text;

                txtOtherCharge2.Enabled = true;
            }
        }

        

        CalculateTotal();
    }

    private void SetSessionDetailsFromGrid()
    {

        int i;

        CheckBox Chk_Attach;
        TextBox txt_Freight, txtOtherCharge1, txtOtherCharge2, txt_Total, txt_GST, txt_GSTPercent;
        decimal Total, GST;

        if (dg_Details.Items.Count > 0)
        {
            for (i = 0; i <= dg_Details.Items.Count - 1; i++)
            {

                Chk_Attach = (CheckBox)dg_Details.Items[i].FindControl("Chk_Attach");

                txt_Freight = (TextBox)dg_Details.Items[i].FindControl("txt_Freight");
                txtOtherCharge1 = (TextBox)dg_Details.Items[i].FindControl("txtOtherCharge1");
                txtOtherCharge2 = (TextBox)dg_Details.Items[i].FindControl("txtOtherCharge2");
                txt_Total = (TextBox)dg_Details.Items[i].FindControl("txt_Total");
                txt_GST = (TextBox)dg_Details.Items[i].FindControl("txt_GST");
                txt_GSTPercent = (TextBox)dg_Details.Items[i].FindControl("txt_GSTPercent");

                txtOtherCharge1.Text = (txtOtherCharge1.Text == string.Empty ? "0" : txtOtherCharge1.Text);
                txtOtherCharge2.Text = (txtOtherCharge2.Text == string.Empty ? "0" : txtOtherCharge2.Text);

                SessionBindGridDetails.Rows[i]["Att"] = Chk_Attach.Checked;


                SessionBindGridDetails.Rows[i]["OtherCharges1"] = txtOtherCharge1.Text;
                SessionBindGridDetails.Rows[i]["OtherCharges2"] = txtOtherCharge2.Text;


                Total = Util.String2Decimal(txt_Freight.Text) + Util.String2Decimal(txtOtherCharge1.Text) + Util.String2Decimal(txtOtherCharge2.Text);
                txt_Total.Text = Total.ToString();


                GST = Math.Round((Total * (Util.String2Decimal(txt_GSTPercent.Text) / 100)), 0);
                txt_GST.Text = GST.ToString();

                SessionBindGridDetails.Rows[i]["Total"] = txt_Total.Text;
                SessionBindGridDetails.Rows[i]["GST"] = txt_GST.Text;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        SetSessionDetailsFromGrid();
        CalculateTotal();

        if (validateUI())
        {
            Save();
        }
    }

    private void CalculateTotal()
    {


        int i, TotalGC;
        decimal TotalFreight, TotalOtherCharge1, TotalOtherCharge2, Total, TotalGST, RowTotal, RowGST;

        CheckBox Chk_Attach;
        TextBox txt_Freight, txtOtherCharge1, txtOtherCharge2, txt_Total, txt_GST, txt_GSTPercent;

        TotalGC = 0;
        TotalFreight = 0;
        TotalOtherCharge1 = 0;
        TotalOtherCharge2 = 0;
        Total = 0;
        TotalGST = 0;

        if (dg_Details.Items.Count > 0)
        {
            for (i = 0; i <= dg_Details.Items.Count - 1; i++)
            {
                Chk_Attach = (CheckBox)dg_Details.Items[i].FindControl("Chk_Attach");
                if (Chk_Attach.Checked == true)
                {
                    RowTotal = 0;
                    RowGST = 0;

                    TotalGC = TotalGC + 1;
                    txt_Freight = (TextBox)dg_Details.Items[i].FindControl("txt_Freight");
                    txtOtherCharge1 = (TextBox)dg_Details.Items[i].FindControl("txtOtherCharge1");
                    txtOtherCharge2 = (TextBox)dg_Details.Items[i].FindControl("txtOtherCharge2");
                    txt_Total = (TextBox)dg_Details.Items[i].FindControl("txt_Total");
                    txt_GST = (TextBox)dg_Details.Items[i].FindControl("txt_GST");

                    txt_GSTPercent = (TextBox)dg_Details.Items[i].FindControl("txt_GSTPercent");


                    if (txtOtherCharge1.Text == "")
                        txtOtherCharge1.Text = "0";

                    if (txtOtherCharge2.Text == "")
                        txtOtherCharge2.Text = "0";

                    RowTotal = Util.String2Decimal(txt_Freight.Text) + Util.String2Decimal(txtOtherCharge1.Text) + Util.String2Decimal(txtOtherCharge2.Text);
                    txt_Total.Text = RowTotal.ToString();


                    RowGST = Math.Round((RowTotal * (Util.String2Decimal(txt_GSTPercent.Text) / 100)), 0);
                    txt_GST.Text = RowGST.ToString();


                    TotalFreight = TotalFreight + Convert.ToDecimal(txt_Freight.Text);
                    TotalOtherCharge1 = TotalOtherCharge1 + Convert.ToDecimal(txtOtherCharge1.Text);
                    TotalOtherCharge2 = TotalOtherCharge2 + Convert.ToDecimal(txtOtherCharge2.Text);
                    Total = Total + Convert.ToDecimal(txt_Total.Text);
                    TotalGST = TotalGST + Convert.ToDecimal(txt_GST.Text);

                }
            }

            

            lbl_TotalGC.Text = TotalGC.ToString();
            hdn_TotalGC.Value = TotalGC.ToString();

            lbl_TotalFreight.Text = TotalFreight.ToString();
            hdn_TotalFreight.Value = TotalFreight.ToString();

            lbl_TotalOtherCharge1.Text = TotalOtherCharge1.ToString();
            hdn_TotalOtherCharge1.Value = TotalOtherCharge1.ToString();

            lbl_TotalOtherCharge2.Text = TotalOtherCharge2.ToString();
            hdn_TotalOtherCharge2.Value = TotalOtherCharge2.ToString();

            lbl_Total.Text = Total.ToString();
            hdn_Total.Value = Total.ToString();

            lbl_TotalGST.Text = TotalGST.ToString();
            hdn_TotalGST.Value = TotalGST.ToString();

        }
    }

    public String DetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBindGridDetails.Copy());
            _objDs.Tables[0].TableName = "SessionBindGridDetails";
            return _objDs.GetXml().ToLower();
        }
    }


    private bool GridValidation()
    {
        bool ATS = true;
        Label lbl_GCNo, lbl_GCDate;
        int i = 0;

        CheckBox chk;

        DateTime BillDate;

        BillDate = dtp_BillDate.SelectedDate;

        if (dg_Details.Items.Count > 0)
        {
            for (i = 0; i <= dg_Details.Items.Count - 1; i++)
            {
                lbl_GCNo = (Label)dg_Details.Items[i].FindControl("lbl_GCNo");
                lbl_GCDate = (Label)dg_Details.Items[i].FindControl("lbl_GCDate");
                chk = (CheckBox)dg_Details.Items[i].FindControl("Chk_Attach");

                if (chk.Checked == true && BillDate < Convert.ToDateTime(lbl_GCDate.Text))
                {
                    errorMessage = "Bill Date can't be less than " + CompanyManager.getCompanyParam().GcCaption + " Date For " + CompanyManager.getCompanyParam().GcCaption + " : " + lbl_GCNo.Text;
                    ATS = false;
                    break;
                }
            }
        }

        return ATS;
    }

    public bool validateUI()
    {
        bool _isValid = false;

        errorMessage = "";


        if ( Util.String2Int(lbl_TotalGC.Text)   <= 0)
        {
            errorMessage = "Please Select Atleast One " + CompanyManager.getCompanyParam().GcCaption;
        }
        else if (Util.String2Decimal(lbl_Total.Text) <= 0)
        {
            errorMessage = "Total " + CompanyManager.getCompanyParam().GcCaption + " Amount should be greater than Zero";
        }
        else if (GridValidation() == false)
        {
            _isValid = false;
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }

    private Message Save()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeOutParams("@Print_Document_ID", SqlDbType.Int,0), 
            objDAL.MakeInParams("@Bill_Date",SqlDbType.DateTime,0,dtp_BillDate.SelectedDate),
            objDAL.MakeInParams("@Bill_ID",SqlDbType.Int ,0,0),
            objDAL.MakeInParams("@Year_Code",SqlDbType.Int ,0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Division_ID",SqlDbType.Int ,0,UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,3,UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Main_ID",SqlDbType.Int ,0,UserManager.getUserParam().MainId),
            objDAL.MakeInParams("@Client_ID",SqlDbType.Int ,0,Util.String2Int(hdn_Client_Id.Value)),
            objDAL.MakeInParams("@TotalGC",SqlDbType.Int ,0,Util.String2Int(hdn_TotalGC.Value)),
            objDAL.MakeInParams("@TotalFreight",SqlDbType.Decimal,0,Util.String2Int(hdn_TotalFreight.Value)),
            objDAL.MakeInParams("@TotalOtherCharges1",SqlDbType.Decimal,0,Util.String2Int(hdn_TotalOtherCharge1.Value)),
            objDAL.MakeInParams("@TotalOtherCharges2",SqlDbType.Decimal,0,Util.String2Int(hdn_TotalOtherCharge1.Value)),
            objDAL.MakeInParams("@OtherCharge1_Id",SqlDbType.Int ,0,Util.String2Int(hdn_OtherCharge1_Id.Value)),
            objDAL.MakeInParams("@OtherCharge2_Id",SqlDbType.Int ,0,Util.String2Int(hdn_OtherCharge2_Id.Value)),
            objDAL.MakeInParams("@Total",SqlDbType.Decimal,0,Util.String2Int(hdn_Total.Value)),
            objDAL.MakeInParams("@TotalGST",SqlDbType.Decimal,0,Util.String2Int(hdn_TotalGST.Value)),
            objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,50000,DetailsXML), 
            objDAL.MakeInParams("@Updated_By",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.EC_FA_Transport_Bill_New_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {

            string _Msg = "Saved SuccessFully";

            int MenuItemId = 143;
            string Mode = "MgA=";
            int Document_ID = Convert.ToInt32(objSqlParam[2].Value);

            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" +
                    ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" +
                    ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" +
                    ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" +
                    ClassLibraryMVP.Util.EncryptInteger(Document_ID)  
                    + "&SpecialBillFormat=1"));

        }
        else
        {
            errorMessage = objMessage.message;
        }

        return objMessage;
    }

}
