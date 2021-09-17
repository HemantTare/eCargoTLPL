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
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;
public partial class Master_Vehicle_FrmLocalDeliveryVehicleContract : ClassLibraryMVP.UI.Page
{
    Common objCommon = new Common();

    private string LocalDeliveryVehicleContractID
    {
        set
        {

            if (value == null)
                hdnKeyID.Value = "0";
            else
                hdnKeyID.Value = value.ToString();
        }
        get
        {
            if (Util.String2Int(hdnKeyID.Value) <= 0)
            {
                return "0";
            }
            return hdnKeyID.Value;
        }
    }
    private string BranchID
    {
        get
        {
            return DDLBranch.SelectedValue;
        }
    }
    private int VehicleID
    {
        set
        {
            DDLVehicle.VehicleID = value;
        }

        get
        {
            return DDLVehicle.VehicleID;
        }
    }

    private string VendorID
    {
        get
        {
            return DDLVendor.SelectedValue;
        }
    }

    private DateTime ValidFrom
    {
        set
        {
            dtpValidFrom.SelectedDate = value;
        }
        get
        {
            return dtpValidFrom.SelectedDate;
        }
    }

    private DateTime ValidUpto
    {
        set
        {
            dtpValidUpto.SelectedDate = value;
        }
        get
        {
            return dtpValidUpto.SelectedDate;
        }
    }

    public bool IsFreightCalculationFixed
    {
        set
        {
            Boolean IsChk1 = value;
            if (IsChk1 == false)
                rdl_FreightCalculation.SelectedValue = "1";
            else
                rdl_FreightCalculation.SelectedValue = "0";
        }
        get { return rdl_FreightCalculation.SelectedValue == "0" ? true : false; }
    }

    private string FreightSettlementID
    {
        set
        {
            ddlFreightSettlement.SelectedValue = value;
        }

        get
        {
            return ddlFreightSettlement.SelectedValue;
        }
    }

    private string FixedTypeId
    {
        set
        {
            ddlFixedType.SelectedValue = value;
        }

        get
        {
            return ddlFixedType.SelectedValue;
        }
    }

    private string FixedRs
    {
        set
        {
            txt_FixedRs.Text = value;
        }

        get
        {
            return txt_FixedRs.Text;
        }
    }



    private string Remarks
    {
        set
        {
            txtRemarks.Text = value;
        }

        get
        {
            return txtRemarks.Text;
        }
    }

    private string ErrorMessage
    {
        set { lblErrors.Text = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        DDLVendor.OtherColumns = "1,2";

        if (!IsPostBack)
        {
            LocalDeliveryVehicleContractID = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));
            FillDropdowns();
            ReadValues();

            TextBox txtBranch = (TextBox)DDLBranch.FindControl("txtBoxDDLBranch");
            ScriptManager.SetFocus(txtBranch);
        }
    }

    private void FillDropdowns()
    {
        ddlFreightSettlement.DataTextField = "FreightSettlementName";
        ddlFreightSettlement.DataValueField = "FreightSettlementId";
        ddlFreightSettlement.DataSource = objCommon.EC_Common_Pass_Query("SELECT FreightSettlementId,FreightSettlementName FROM dbo.EC_Master_FreightSettlement");
        ddlFreightSettlement.DataBind();

        ddlFixedType.DataTextField = "FixedType";
        ddlFixedType.DataValueField = "FixedTypeId";
        ddlFixedType.DataSource = objCommon.EC_Common_Pass_Query("Select FixedTypeId,FixedType From EC_Master_VehicleContractFixedType Where Is_Active=1 Order By FixedTypeId");
        ddlFixedType.DataBind();
    }


    private void ReadValues()
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@LocalVehicleDeliveryContractID", SqlDbType.Int, 0, LocalDeliveryVehicleContractID) };
        objDAL.RunProc("dbo.EC_Master_LocalDeliveryVehicleContractReadValues", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];

            DDLBranch.DataTextField = "Branch_Name";
            DDLBranch.DataValueField = "Branch_ID";
            Raj.EC.Common.SetValueToDDLSearch(objDR["Branch_Name"].ToString(), objDR["Branch_ID"].ToString(), DDLBranch);

            VehicleID = Util.String2Int(objDR["VehicleID"].ToString());

            DDLVendor.DataTextField = "Vendor_Name";
            DDLVendor.DataValueField = "Vendor_ID";
            Raj.EC.Common.SetValueToDDLSearch(objDR["Vendor_Name"].ToString(), objDR["Vendor_ID"].ToString(), DDLVendor);

            ValidFrom = Convert.ToDateTime(objDR["ValidFrom"].ToString());
            ValidUpto = Convert.ToDateTime(objDR["ValidUpTo"].ToString());

            FreightSettlementID = objDR["FreightSettlementID"].ToString();

            IsFreightCalculationFixed = Util.String2Bool(objDR["IsFreightCalculationFixed"].ToString());

            if (IsFreightCalculationFixed == false)
            {
                tr_FreightCalculation.Style.Add("display", "none");
                FixedRs = "0";
            }
            else
            {
                tr_FreightCalculation.Style.Add("display", "block");
                FixedRs = objDR["RatePerFix"].ToString();
            }

            FixedTypeId = objDR["FixedTypeId"].ToString();

            if (objDR["FixedTypeId"].ToString() == "1")
            {
                lbl_FixedRsPer.Text = " / Trip";
            }
            else if (objDR["FixedTypeId"].ToString() == "2")
            {
                lbl_FixedRsPer.Text = " / Day";
            }
            else
            {
                lbl_FixedRsPer.Text = " / Month";
            }

            Remarks = objDR["Remarks"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (AllowToSave())
        {
            Save();
        }
    }


    private bool AllowToSave()
    {
        bool ATS = false;

        if (Util.String2Int(BranchID) <= 0)
        {
            ErrorMessage = "Please Select Branch";
            TextBox txtBranch = (TextBox)DDLBranch.FindControl("txtBoxDDLBranch");
            ScriptManager.SetFocus(txtBranch);
        }
        else if (VehicleID <= 0)
        {
            ErrorMessage = "Please Select Vehicle";
            TextBox txtVehicleNumber = (TextBox)DDLVehicle.FindControl("txt_Vehicle_Last_4_Digits");
            txtVehicleNumber.Focus();
        }
        else if (Util.String2Int(VendorID) <= 0)
        {
            ErrorMessage = "Please Select Vendor";
            TextBox txtVendor = (TextBox)DDLVendor.FindControl("txtBoxDDLVendor");
            ScriptManager.SetFocus(txtVendor);
        }
        else if (ValidFrom > ValidUpto)
        {
            ErrorMessage = "Valid From date must be less than equal to Valid Upto Date";
        }
        else
            ATS = true;

        return ATS;
    }

    public Message Save()
    {

        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
        objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeOutParams("@KeyID", SqlDbType.Int, 0), 
        objDAL.MakeInParams("@LocalVehicleDeliveryContractID",SqlDbType.Int,0,LocalDeliveryVehicleContractID),
        objDAL.MakeInParams("@BranchID",SqlDbType.Int,0,BranchID),
        objDAL.MakeInParams("@VehicleID",SqlDbType.Int,0,VehicleID),
        objDAL.MakeInParams("@VendorID",SqlDbType.Int,0,VendorID),
        objDAL.MakeInParams("@ValidFrom",SqlDbType.DateTime,0,ValidFrom),
        objDAL.MakeInParams("@ValidUpTo",SqlDbType.DateTime,0,ValidUpto),
        objDAL.MakeInParams("@FreightSettlementID",SqlDbType.Int,0,FreightSettlementID),

        objDAL.MakeInParams("@IsFreightCalculationFixed",SqlDbType.Bit,0,IsFreightCalculationFixed),
        objDAL.MakeInParams("@FixedTypeId",SqlDbType.Int,0,FixedTypeId),
        objDAL.MakeInParams("@RatePerFix",SqlDbType.Decimal,0,Util.String2Decimal(FixedRs) ),
        objDAL.MakeInParams("@Remarks",SqlDbType.VarChar,250,Remarks),
        objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)};


        objDAL.RunProc("EC_Master_LocalDeliveryVehicleContractSave", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            ErrorMessage = "Saved SuccessFully";
            LocalDeliveryVehicleContractID = Convert.ToString(objSqlParam[2].Value);
        }
        else if (objMessage.messageID == 2627)
        {
            ErrorMessage = "Duplicate record found for the combination of selected Branch,vehicle,Vendor,ValidFrom and ValidUpto";
        }
        else
        {
            ErrorMessage = objMessage.message;
        }
        return objMessage;
    }

    protected void ddlFixedType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Util.String2Int(ddlFixedType.SelectedValue) == 1)
        {
            lbl_FixedRsPer.Text = "/ Trip";
        }
        else if (Util.String2Int(ddlFixedType.SelectedValue) == 2)
        {
            lbl_FixedRsPer.Text = "/ Day";
        }
        else if (Util.String2Int(ddlFixedType.SelectedValue) == 3)
        {
            lbl_FixedRsPer.Text = "/ Month";
        }

    }

}
