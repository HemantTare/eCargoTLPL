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
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Text;
using System.Net;
using System.IO;


public partial class Operations_Booking_FrmPartyInvoiceWeightUpdate : System.Web.UI.Page
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    TextBox txt_Weight;
    TextBox txt_GC_No;
    DataRow dr;
    int BillingClientID;
   
    public bool validateUI()
    {
        bool ATS;
        ATS = false;
 

        if (Session_BharaiGrid.Rows.Count <= 0)
        {
            lblErrors.Text = "Please Enter LR, Weight";
        }
        else
        {
            ATS = true;
        }

        //for (int i = 0; i <= dg_Commodity.Items.Count - 1; i++)
        //{
        //    HiddenField hdnGCID = (HiddenField)(dg_Commodity.Items[i].FindControl("hdnGCID"));
        //    TextBox txt_Weight = (TextBox)(dg_Commodity.Items[i].FindControl("txt_Weight"));

        //    if (Convert.ToInt32(txt_Weight.Text) <= 0)
        //    {
        //        ATS = false;
        //        lblErrors.Text = "Please Enter LR, Weight";
        //        break;
        //    }
        //}

        return ATS;
    }

   
    public int Session_Mode
    {
        get { return StateManager.GetState<int>("SessionMode"); }
        set { StateManager.SaveState("SessionMode", value); }
    }
    public int Session_MenuItem
    {
        get { return StateManager.GetState<int>("SessionMenuItem"); }
        set { StateManager.SaveState("SessionMenuItem", value); }
    }
 
    private void Bind_dg_Commodity()
    { 
        dg_Commodity.DataSource = Session_BharaiGrid;
        dg_Commodity.DataBind();

        if (Session_BharaiGrid.Rows.Count > 0)
        {
            TextBox txt_Weight = (TextBox)(dg_Commodity.Items[0].FindControl("txt_Weight"));
            txt_Weight.Focus();
        }
    } 
    private string ErrorMsg
    {
        set { lblErrors.Text = value; }
    }
    private string ClientCode
    {
        get { return CompanyManager.getCompanyParam().ClientCode.ToLower(); }
    }
   

    private string GCWeightXML
    {
        get
        {
            string XML = "<newdataset>";

            for (int i = 0; i <= dg_Commodity.Items.Count - 1; i++)
            {
                HiddenField hdnGCID = (HiddenField)(dg_Commodity.Items[i].FindControl("hdnGCID"));
                TextBox txt_Weight = (TextBox)(dg_Commodity.Items[i].FindControl("txt_Weight"));

                XML = XML + "<gcdetails>";
                XML = XML + "<hdngcid>" + hdnGCID.Value + "</hdngcid>";
                XML = XML + "<gcweight>" + txt_Weight.Text + "</gcweight>";
                XML = XML + "</gcdetails>";
            }

            XML = XML + "</newdataset>";

            return XML;
        }
    }

    public DataTable Session_BharaiGrid
    {
        get { return StateManager.GetState<DataTable>("BharaiGrid"); }
        set { StateManager.SaveState("BharaiGrid", value); }
    } 

      
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        BillingClientID = 186; //186 Live  //99 Test;

        //if (!IsPostBack)
        //{
            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Operations/Booking/FrmPartyInvoiceWeightUpdateNewConsigneeDetails.aspx?HierarchyCode=" + UserManager.getUserParam().HierarchyCode 
                + "&FromDate=" + Wuc_From_To_Datepicker1.SelectedFromDate + "&ToDate=" + Wuc_From_To_Datepicker1.SelectedToDate
                + "&BranchID=" + UserManager.getUserParam().MainId + "&BillingClientID=" + BillingClientID);
            lnk_btnNewConsignee.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");

        //}
        if(UserManager.getUserParam().HierarchyCode == "BO")
        {
            lnk_btnNewConsignee.Visible = false;
        }   

    }


    private void fillLRDetails()
    {
        SqlParameter[] objSqlParam ={objDAL.MakeInParams("@HierarchyCode", SqlDbType.Char, 3, UserManager.getUserParam().HierarchyCode),
         objDAL.MakeInParams("@FromDate", SqlDbType.DateTime, 0, Wuc_From_To_Datepicker1.SelectedFromDate.Date),
         objDAL.MakeInParams("@ToDate", SqlDbType.DateTime, 0, Wuc_From_To_Datepicker1.SelectedToDate.Date),
         objDAL.MakeInParams("@BranchID", SqlDbType.Int, 0, UserManager.getUserParam().MainId),
         objDAL.MakeInParams("@ClientID", SqlDbType.Int, 0, BillingClientID)};

        objDAL.RunProc("EC_Opr_Fill_LR_For_Weight_Update_New", objSqlParam, ref objDS);
        
        Session_BharaiGrid = objDS.Tables[0];

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            lblTotalRecords.Text = objDS.Tables[1].Rows[0][0].ToString();

            if (UserManager.getUserParam().HierarchyCode != "BO")
            {
                lnk_btnNewConsignee.Visible = true;
            }
        }
        else
        {
            lblTotalRecords.Text = "No Record Found";
            lnk_btnNewConsignee.Visible = false;
        }


        Bind_dg_Commodity();

        

    }

       
    protected void dg_Commodity_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                txt_GC_No = (TextBox)(e.Item.FindControl("txt_GCNo"));
                txt_Weight = (TextBox)(e.Item.FindControl("txt_Weight"));
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                DataRow DR = null;
                DataTable dt = Session_BharaiGrid;//for grid topic
                DR = dt.Rows[e.Item.ItemIndex];

                txt_GC_No.Text = DR["GC_No"].ToString();
                txt_Weight.Text = DR["Weight"].ToString();
            }

            if (e.Item.ItemIndex != -1)
            {
                txt_Weight = (TextBox)(e.Item.FindControl("txt_Weight"));

                if (Util.String2Int(txt_Weight.Text) == 0)
                {
                    e.Item.BackColor = System.Drawing.Color.Yellow;
                }
            }
        }
    }
    //protected void dg_Commodity_EditCommand(object source, DataGridCommandEventArgs e)
    //{
    //    dg_Commodity.EditItemIndex = e.Item.ItemIndex;
    //    dg_Commodity.ShowFooter = false;
    //    Bind_dg_Commodity();
    //    ErrorMsg = ""; 

    //}
    //protected void dg_Commodity_CancelCommand(object source, DataGridCommandEventArgs e)
    //{
    //    dg_Commodity.EditItemIndex = -1;
    //    dg_Commodity.ShowFooter = true;
         

    //    Bind_dg_Commodity();
    //    ErrorMsg = ""; 

    //}
    //protected void dg_Commodity_DeleteCommand(object source, DataGridCommandEventArgs e)
    //{
    //    if (e.Item.ItemIndex != -1)
    //    {
    //        dr = Session_BharaiGrid.Rows[e.Item.ItemIndex];
    //        dr.Delete();
    //        Session_BharaiGrid.AcceptChanges();
    //        dg_Commodity.EditItemIndex = -1;
    //        dg_Commodity.ShowFooter = true;
    //        Bind_dg_Commodity();
    //    } 

    //}
   
   


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        fillLRDetails();
    }

    private Message Save()
    {
       
        DataTable DT1 = Session_BharaiGrid.Copy();
        DT1.TableName = "bharaigrid";
        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);

        string GCDetailsXML = ds.GetXml().ToLower();

 
        
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),  
            objDAL.MakeInParams("@GCDetailsXML",SqlDbType.Xml,0,GCWeightXML),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EC_Opr_LR_Weight_Update_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            String popupScript = "";
            string _Msg = "Saved SuccessFully";
            
            string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");

            System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(String), "PopupScript1", popupScript.ToString(), false);

        }
        else
        {
            if (objMessage.messageID == 2601)
            {
                lblErrors.Text = "Duplicate Entry Found";
            }
            else
            {
                lblErrors.Text = objMessage.message;
            }
        }

        return objMessage;
    }

}


