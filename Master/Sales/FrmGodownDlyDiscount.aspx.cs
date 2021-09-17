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

public partial class Master_Sales_FrmGodownDlyDiscount : ClassLibraryMVP.UI.Page
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
        else if (dtpValidFrom.SelectedDate > dtpValidUpto.SelectedDate)
        {
            lblErrors.Text = "Valid Upto must be greater than Valid from date";
        }
        else if (DiscountPercent <= 0)
        { 
            lblErrors.Text = "Please enter discount percent greater > 0";
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
        if (!IsPostBack)
        {
            ScriptManager.SetFocus(ddlDiscountBranch);
        }

    }
    private void ReadValues()
    {

        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@GodownDiscountID", SqlDbType.Int, 0, Util.String2Int(hdnKeyID.Value)) };
        objDAL.RunProc("EC_Mst_GodownDeliveryRateDiscount_ReadValues", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            string text, value;
            DataRow objDR = ds.Tables[0].Rows[0]; 

            text = objDR["Branch_Name"].ToString();
            value = objDR["DeliveryBranchID"].ToString();
            Raj.EC.Common.SetValueToDDLSearch(text, value, ddlDiscountBranch);
              
            dtpValidFrom.SelectedDate = Convert.ToDateTime(objDR["ValidFrom"].ToString());
            dtpValidUpto.SelectedDate = Convert.ToDateTime(objDR["ValidUpTo"].ToString());
            DiscountPercent = Util.String2Decimal(objDR["DiscountPercent"].ToString()); 
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
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeOutParams("@DARDIDOutput", SqlDbType.Int, 0), 
            objDAL.MakeInParams("@GodownDiscountID",SqlDbType.Int,0,Util.String2Int(hdnKeyID.Value)), 
            objDAL.MakeInParams("@DeliveryBranchID",SqlDbType.Int,0,Util.String2Int(ddlDiscountBranch.SelectedValue)), 
            objDAL.MakeInParams("@ValidFrom",SqlDbType.DateTime,0,dtpValidFrom.SelectedDate),
            objDAL.MakeInParams("@ValidUpTo",SqlDbType.DateTime,0,dtpValidUpto.SelectedDate),
            objDAL.MakeInParams("@DiscountPercent",SqlDbType.Decimal,0, DiscountPercent),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.EC_Mst_GodownDeliveryRateDiscount_Save", objSqlParam);

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
 
}
