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
using System.Data.SqlClient;
using ClassLibrary;
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Master_Sales_FrmDlyAreaDiscount : ClassLibraryMVP.UI.Page
{
    
    bool ATS = false;

    public decimal DiscountPercent
    {
        set
        {
            txtDiscountPercent.Text = Util.Decimal2String(Math.Round(value, 2));
            hdn_DiscountPercent.Value = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return hdn_DiscountPercent.Value == string.Empty ? 0 : Util.String2Decimal(hdn_DiscountPercent.Value); }
        
    }
    public int ToBe_ID
    {
        get { return Util.String2Int(ddl_ToBe.SelectedValue); }
        set { ddl_ToBe.SelectedValue = Util.Int2String(value); }
    }
    public string ToBe
    {
        get { return ddl_ToBe.SelectedItem.Text; }
        set { ddl_ToBe.SelectedItem.Text = value; }
    } 
    #region members
    public bool validateUI()
    {
        bool ATS;
        ATS = false;

        if (Util.String2Int(ddlDiscountBranch.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please select discount branch";
            ddlDiscountBranch.Focus();
        }
        else if (Util.String2Int(ddl_dlyArea.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select Delivery Area";
        }
        else if (dtpValidFrom.SelectedDate > dtpValidUpto.SelectedDate)
        {
            lblErrors.Text = "Valid Upto must be greater than Valid from date";
        }
        else if (DiscountPercent <= 0)
        {
            lblErrors.Text = "Please enter discount percent greater > 0";
        }
        else if (ToBe == "-" && DiscountPercent > 99)
        {
            lblErrors.Text = "Discount can not greater than 99%";
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }
    private void Assign_Hidden_Value()
    {
        hdn_DiscountPercent.Value = txtDiscountPercent.Text;

    }
    private void Set_Hidden_Value()
    {
        DiscountPercent = hdn_DiscountPercent.Value == string.Empty ? 0 : Util.String2Decimal(hdn_DiscountPercent.Value);
       
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"])); 

            ddlDiscountBranch.DataTextField = "Branch_Name";
            ddlDiscountBranch.DataValueField = "Branch_Id";
            ReadValues(); 
        }
        Assign_Hidden_Value();
        Set_Hidden_Value();

    }
    private void ReadValues()
    {

        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@DiscountID", SqlDbType.Int, 0, Util.String2Int(hdnKeyID.Value)) };
        objDAL.RunProc("EC_Mst_DeliveryAreaRateDiscount_ReadValues", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            string text, value;
            DataRow objDR = ds.Tables[0].Rows[0]; 

            text = objDR["Branch_Name"].ToString();
            value = objDR["DeliveryBranchID"].ToString();
            Raj.EC.Common.SetValueToDDLSearch(text, value, ddlDiscountBranch);
             
            BindDlyArea();
            ddl_dlyArea.SelectedValue = objDR["DeliveryAreaID"].ToString();  
            dtpValidFrom.SelectedDate = Convert.ToDateTime(objDR["ValidFrom"].ToString());
            dtpValidUpto.SelectedDate = Convert.ToDateTime(objDR["ValidUpTo"].ToString());
            DiscountPercent = Util.String2Decimal(objDR["DiscountPercent"].ToString());
            ddl_ToBe.SelectedValue = objDR["ToBe"].ToString();  
        } 
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
        //DataTable DT = (DataTable)(Session["DiscountDetails"]);
        //DT.TableName = "discountdetails";
        //DataTable DT1 = DT.Copy();

        //DataSet ds = new DataSet();
        //ds.Tables.Add(DT1);

        //string discountDetailsXML = ds.GetXml().ToLower();

        int dlyAreaId = 0;
         
            dlyAreaId = Util.String2Int(ddl_dlyArea.SelectedValue);
         
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeOutParams("@DARDIDOutput", SqlDbType.Int, 0), 
            objDAL.MakeInParams("@DiscountID",SqlDbType.Int,0,Util.String2Int(hdnKeyID.Value)), 
            objDAL.MakeInParams("@DeliveryBranchID",SqlDbType.Int,0,Util.String2Int(ddlDiscountBranch.SelectedValue)), 
            objDAL.MakeInParams("@DeliveryAreaID",SqlDbType.Int,0,dlyAreaId),
            objDAL.MakeInParams("@ValidFrom",SqlDbType.DateTime,0,dtpValidFrom.SelectedDate),
            objDAL.MakeInParams("@ValidUpTo",SqlDbType.DateTime,0,dtpValidUpto.SelectedDate),
            objDAL.MakeInParams("@DiscountPercent",SqlDbType.Decimal,0, DiscountPercent),
            objDAL.MakeInParams("@ToBe",SqlDbType.VarChar,1, ddl_ToBe.SelectedValue),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.EC_Mst_DeliveryAreaRateDiscount_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            lblErrors.Text = "Saved SuccessFully";
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);
        }
        else
        {
            lblErrors.Text = objMessage.message;
        }
        return objMessage;
    }

    #region grid operation

    
    #endregion

    protected void ddlDiscountBranch_TxtChange(object sender, EventArgs e)
    {
        BindDlyArea();
    }
    public void BindDlyArea()
    {
        DataTable dt = new DataTable();
        int DiscountBranch_Id;
        DiscountBranch_Id = Convert.ToInt32(ddlDiscountBranch.SelectedValue);
        Common objCommon = new Common();


        dt = objCommon.GetDeliveryArea(DiscountBranch_Id, false, true, false, 0);

        ddl_dlyArea.DataTextField = "DeliveryAreaName";
        ddl_dlyArea.DataValueField = "DeliveryAreaID";
        ddl_dlyArea.DataSource = dt;
        ddl_dlyArea.DataBind();

        ddl_dlyArea.Items.Insert(0, new ListItem("Select One", "0"));
        ScriptManager.SetFocus(ddl_dlyArea);  
    
    }
}
