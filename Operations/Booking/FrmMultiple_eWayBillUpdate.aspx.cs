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


public partial class Operations_Booking_FrmMultiple_eWayBillUpdate : System.Web.UI.Page
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    TextBox txt_eWayBillNo;
    
    DataRow dr;
   
    bool Allow_To_Save;
    DataTable objDT;
    

    public bool validateUI()
    {
        bool ATS;
        ATS = false;
 

        if (Session_BharaiGrid.Rows.Count <= 0)
        {
            lblErrors.Text = "Please Enter eWay Bill Nos";
        }
        else
        {
            ATS = true;
        }

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

         
    } 
    private string ErrorMsg
    {
        set { lblErrors.Text = value; }
    }
    private string ClientCode
    {
        get { return CompanyManager.getCompanyParam().ClientCode.ToLower(); }
    }
    private void Set_Common_DDL(DropDownList DDl, string TextField, string ValueField, DataTable DT, bool Is_ZeroInex)
    {
        DDl.DataSource = DT;
        DDl.DataTextField = TextField;
        DDl.DataValueField = ValueField;
        DDl.DataBind();
        if (Is_ZeroInex)
            DDl.Items.Insert(0, new ListItem("Select One", "0"));
    }
 

    public DataTable Session_BharaiGrid
    {
        get { return StateManager.GetState<DataTable>("BharaiGrid"); }
        set { StateManager.SaveState("BharaiGrid", value); }
    } 

    protected void Page_Load(object sender, EventArgs e)
    {

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
       
        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["GC_ID"]));
            lbl_GCNo.Text =  Util.DecryptToString(Request.QueryString["GC_No"]);
            lbl_LReWayBillNo.Text = Util.DecryptToString(Request.QueryString["eWayBillNo"]);

            filleWayBillDetails();
        }
    }

    private void filleWayBillDetails()
    {
        SqlParameter[] objSqlParam ={objDAL.MakeInParams("@GC_ID", SqlDbType.Int, 0,Convert.ToInt32(hdnKeyID.Value))};

        objDAL.RunProc("EC_Opr_Fill_Multiple_eWayBillDetails", objSqlParam, ref objDS);
        
        Session_BharaiGrid = objDS.Tables[0];

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];
        }

        Bind_dg_Commodity();
        
    }   

    
    protected void dg_Commodity_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                txt_eWayBillNo = (TextBox)(e.Item.FindControl("txt_eWayBillNo"));
                
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                DataRow DR = null;
                DataTable dt = Session_BharaiGrid;//for grid topic
                DR = dt.Rows[e.Item.ItemIndex];

                txt_eWayBillNo.Text = DR["eWayBillNo"].ToString();
            }
        }
    }
    protected void dg_Commodity_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = e.Item.ItemIndex;
        dg_Commodity.ShowFooter = false;
        Bind_dg_Commodity();
        ErrorMsg = ""; 

    }
    protected void dg_Commodity_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = -1;
        dg_Commodity.ShowFooter = true;
         

        Bind_dg_Commodity();
        ErrorMsg = ""; 

    }
    protected void dg_Commodity_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            dr = Session_BharaiGrid.Rows[e.Item.ItemIndex];
            dr.Delete();
            Session_BharaiGrid.AcceptChanges();
            dg_Commodity.EditItemIndex = -1;
            dg_Commodity.ShowFooter = true;
            Bind_dg_Commodity();
        } 

    }
    protected void dg_Commodity_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Insert_Update_Commodity_Dataset(source, e);
            if (Allow_To_Save == true)
            {
                dg_Commodity.EditItemIndex = -1;
                dg_Commodity.ShowFooter = true;
                 
                Bind_dg_Commodity(); 
            }
        }
        catch (ConstraintException)
        {
            ErrorMsg = "Duplicate eWay Bill No.";
            lblErrors.Text = "Duplicate eWay Bill No.";

        }
    }
    protected void dg_Commodity_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        txt_eWayBillNo = (TextBox)(e.Item.FindControl("txt_eWayBillNo"));

        if (txt_eWayBillNo.Text == lbl_LReWayBillNo.Text)
        {
            ErrorMsg = "Duplicate eWay Bill No. Same eWay Bill No. Entered In LR.";
            lblErrors.Text = "Duplicate eWay Bill No. Same eWay Bill No. Entered In LR.";
        }
        else if (IsDuplicate_eWayBillNo(txt_eWayBillNo.Text) == true)
        {
             ErrorMsg = "Duplicate eWay Bill No. Same No. Entered In Another LR.";
            lblErrors.Text = "Duplicate eWay Bill No. Same No. Entered In Another LR.";
        }
        else
        {
            if (e.CommandName == "Add" || e.CommandName == "Update")
            {
                try
                {
                    objDT = Session_BharaiGrid;

                    DataColumn[] _dtColumnPrimaryKey;
                    _dtColumnPrimaryKey = new DataColumn[2];
                    _dtColumnPrimaryKey[0] = objDT.Columns["eWayBillNo"];
                    objDT.PrimaryKey = _dtColumnPrimaryKey;

                    Insert_Update_Commodity_Dataset(source, e);
                    if (Allow_To_Save == true)
                    {
                        Bind_dg_Commodity();
                        dg_Commodity.EditItemIndex = -1;
                        dg_Commodity.ShowFooter = true;

                    }
                    //TotalRate = TotalRate;
                }
                catch (ConstraintException)
                {
                    ErrorMsg = "Duplicate eWay Bill No.";
                    lblErrors.Text = "Duplicate eWay Bill No.";
                }
            }
        }
    }
    private void Insert_Update_Commodity_Dataset(object source, DataGridCommandEventArgs e)
    { 
        txt_eWayBillNo = (TextBox)(e.Item.FindControl("txt_eWayBillNo"));
      
        if (Allow_To_Add_Update_Commodity())
        {
            if (e.CommandName == "Add")
            {
                dr = Session_BharaiGrid.NewRow();
            }
            else if (e.CommandName == "Update")
            {
                dr = Session_BharaiGrid.Rows[e.Item.ItemIndex];
            }

            dr["eWayBillNo"] = txt_eWayBillNo.Text.Trim() == string.Empty ? "0" : txt_eWayBillNo.Text.Trim();

            if (e.CommandName == "Add")
            {
                Session_BharaiGrid.Rows.Add(dr);
            }
        }
    }
 

    public bool Allow_To_Add_Update_Commodity()
    {
        Allow_To_Save = false;
        ErrorMsg = "";
        
        decimal RatePerArticle = txt_eWayBillNo.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_eWayBillNo.Text.Trim());
        
        if (txt_eWayBillNo.Text.Trim().Length != 12 && txt_eWayBillNo.Text.Trim().Length != 10)
        {
            ErrorMsg = "Please Enter Proper eWay Bill No.";
            lblErrors.Text = "Please Enter Proper eWay Bill No.";
            lblErrors.Visible = true;
            scm_Comm.SetFocus(txt_eWayBillNo);
        }
        else
        {
            Allow_To_Save = true;
        }

        return Allow_To_Save;
    }
     

    protected void btn_Item_Click(object sender, EventArgs e)
    {
        dg_Commodity.ShowFooter = true;
        dg_Commodity.DataSource = Session_BharaiGrid;
        dg_Commodity.DataBind();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();
        }
    }

    private Message Save()
    {

        DataTable DT1 = Session_BharaiGrid.Copy();
        DT1.TableName = "bharaigrid";
        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);

        string eWayBillDetailsXML = ds.GetXml().ToLower();
        
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {
            objDAL.MakeInParams("@GC_ID", SqlDbType.Int,0, Util.String2Int(hdnKeyID.Value)), 
            objDAL.MakeInParams("@eWayBillDetailsXML",SqlDbType.Xml,0,eWayBillDetailsXML),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId),
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000)};

        objDAL.RunProc("dbo.EC_Opr_Multiple_eWayBills_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[3].Value);
        objMessage.message = Convert.ToString(objSqlParam[4].Value);


        if (objMessage.messageID == 0)
        {
            String popupScript = "";
            string _Msg = "Saved SuccessFully";

            ClearVariables();
            Response.Redirect("~/Display/CloseForm.aspx");

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

    public void ClearVariables()
    {
        objDS = null;
    }

    public bool IsDuplicate_eWayBillNo(string eWayBillNo)
    {
        DataSet ds = new DataSet();

        SqlParameter[] objSqlParam = {
                                    objDAL.MakeInParams("@GC_ID", SqlDbType.Int,0,Util.String2Int(hdnKeyID.Value)),
                                    objDAL.MakeInParams("@eWayBillNo",SqlDbType.VarChar,15,eWayBillNo),
                                    objDAL.MakeOutParams("@Is_Duplicate", SqlDbType.Bit, 1)};

        objDAL.RunProc("EC_Opr_Check_Duplicate_eWayBillNo", objSqlParam, ref ds);

        return Util.String2Bool(ds.Tables[0].Rows[0]["Is_Duplicate"].ToString());
    }

}


