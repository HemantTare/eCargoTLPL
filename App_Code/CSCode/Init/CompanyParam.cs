using System;
using System.Data;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC;
/// <summary> /// Author : Ankit Champaneriya
/// Created On : 26-12-2008
/// Summary description for CompanyParam
/// </summary>

public class CompanyParam
{
    #region Class variable
    //company 
    private int _City_ID, _Pin_Code, _State_ID, _Country_ID, _Employee_ID, _Assessee_Type_Id, _Assessee_Category_Id ;
    private string _Company_Name, _Mailing_Name, _Address_Line_1, _Address_Line_2, _Std_Code, _Phone_1, _Phone_2, _Fax, _email, _TAN_No, _Income_tax_Circle, _Deductor_Type, _Designation, _PAN_No,_client_code;
    private Boolean _Allow_FBT_Category_Selection, _Is_Surcharge_Applicable;

    //company parameter
    private int _Company_Parameters_ID, _Standard_Basic_Freight_Unit_ID,_LHPO_Nature_Of_Payemnt_ID_For_TDS_Deduction;
    private string _GC_Caption, _LHPO_Caption, _division_caption;
    private decimal _Standard_Freight_Rate_Per, _Max_Advance_Percent_Of_Vehicle_hire_Charge;
    private Boolean _Is_Activate_Divisions, _Is_Account_Transfer_Required, _Is_Co_Loader_Business, _Is_Memo_Series_Required, _Is_LHPO_Series_Required, _Is_ALS_Required,_Is_Book_Own_Truck_Hire, _Is_Market_Truck_Ledger_Account_Truck_Wise, _Is_Attached_Truck_Ledger_Account_Truck_Wise, _Is_Managed_Truck_Ledger_Account_Truck_Wise;
    private Boolean _Is_TAS_Required, _Is_Part_Loading_Required,_Is_GcNumber_Editable;

    //company division service tax
    private int _Company_Division_Service_Tax_ID, _Division_ID, _Type_Of_Organisation_ID;
    private string _Service_Tax_Registration_No, _Assessee_Code, _Premises_Code_No, _Large_Tax_Payer_Unit, _Division_Code, _Division_Name, _Range_Code, _Range_Name, _Commissionerate_Code, _Commissionerate_Name, _Focal_Bank_Code, _Focal_Bank_Name, _Focal_Bank_Address;
    private DateTime _Date_Of_Registration;
    private Boolean _Is_Large_Tax_Payer;

    #endregion

    #region property

    public int CityId
    {
        get { return _City_ID; }
        set { _City_ID = value; }
    }
    public int PinCode
    {
        get { return _Pin_Code; }
        set { _Pin_Code = value; }
    }
    public int StateId
    {
        get { return _State_ID; }
        set { _State_ID = value; }
    }
    public int CountryId
    {
        get { return _Country_ID; }
        set { _Country_ID = value; }
    }
    public int EmployeeId
    {
        get { return _Employee_ID; }
        set { _Employee_ID = value; }
    }
    public int AssesseeTypeId
    {
        get { return _Assessee_Type_Id; }
        set { _Assessee_Type_Id = value; }
    }
    public int AssesseeCategoryId
    {
        get { return _Assessee_Category_Id; }
        set { _Assessee_Category_Id = value; }
    }
    public string CompanyName
    {
        get { return _Company_Name; }
        set { _Company_Name = value; }
    }
    public string MailingName
    {
        get { return _Mailing_Name; }
        set { _Mailing_Name = value; }
    }
    public string AddressLine1
    {
        get { return _Address_Line_1; }
        set { _Address_Line_1 = value; }
    }
    public string AddressLine2
    {
        get { return _Address_Line_2; }
        set { _Address_Line_2 = value; }
    }
    public string StdCode
    {
        get { return _Std_Code; }
        set { _Std_Code = value; }
    }
    public string Phone1
    {
        get { return _Phone_1; }
        set { _Phone_1 = value; }
    }
    public string Phone2
    {
        get { return _Phone_2; }
        set { _Phone_2 = value; }
    }
    public string Fax
    {
        get { return _Fax; }
        set { _Fax = value; }
    }
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }
    public string TanNo
    {
        get { return _TAN_No; }
        set { _TAN_No = value; }
    }
    public string IncomeTaxCircle
    {
        get { return _Income_tax_Circle; }
        set { _Income_tax_Circle = value; }
    }
    public string DeductorType
    {
        get { return _Deductor_Type; }
        set { _Deductor_Type = value; }
    }
    public string Designation
    {
        get { return _Designation; }
        set { _Designation = value; }
    }
    public string PanNo
    {
        get { return _PAN_No; }
        set { _PAN_No = value; }
    }

    public string ClientCode
    {
        get { return _client_code; }
        set { _client_code = value; }
    }
    

    public Boolean AllowFBTCategorySelection
    {
        get { return _Allow_FBT_Category_Selection; }
        set { _Allow_FBT_Category_Selection = value; }
    }
    public Boolean IsSurchargeApplicable
    {
        get { return _Is_Surcharge_Applicable; }
        set { _Is_Surcharge_Applicable = value; }
    }

    public int CompanyParametersId
    {
        get { return _Company_Parameters_ID; }
        set { _Company_Parameters_ID = value; }
    }
    public int StandardBasicFreightUnitId
    {
        get { return _Standard_Basic_Freight_Unit_ID; }
        set { _Standard_Basic_Freight_Unit_ID = value; }
    }
    public string GcCaption
    {
        get { return _GC_Caption; }
        set { _GC_Caption = value; }
    }
    public string LHPOCaption
    {
        get { return _LHPO_Caption; }
        set { _LHPO_Caption = value; }
    }

    public string DivisionCaption
    {
        get { return _division_caption; }
        set { _division_caption = value; }
    }
    
    public decimal StandardFreightRatePer
    {
        get { return _Standard_Freight_Rate_Per; }
        set { _Standard_Freight_Rate_Per = value; }
    }
    public Boolean IsActivateDivision
    {
        get { return _Is_Activate_Divisions; }
        set { _Is_Activate_Divisions = value; }
    }
    public Boolean IsAccountTransferRequired
    {
        get { return _Is_Account_Transfer_Required; }
        set { _Is_Account_Transfer_Required = value; }
    }
    public Boolean IsCoLoaderBusiness
    {
        get { return _Is_Co_Loader_Business; }
        set { _Is_Co_Loader_Business = value; }
    }
    public Boolean IsMemoSeriesRequired
    {
        get { return _Is_Memo_Series_Required; }
        set { _Is_Memo_Series_Required = value; }
    }
    public Boolean IsLHPOSeriesRequired
    {
        get { return _Is_LHPO_Series_Required; }
        set { _Is_LHPO_Series_Required = value; }
    }
    public Boolean IsALSRequired
    {
        get { return _Is_ALS_Required; }
        set { _Is_ALS_Required = value; }
    }

    public Boolean IsTASRequired
    {
        get { return _Is_TAS_Required; }
        set { _Is_TAS_Required = value; }
    }

    public Boolean IsPartLoadingRequired
    {
        get { return _Is_Part_Loading_Required; }
        set { _Is_Part_Loading_Required = value; }
    }

    public Boolean IsGcNumberEditable
    {
        get { return _Is_GcNumber_Editable; }
        set { _Is_GcNumber_Editable = value; }
    }

    public Boolean IsBookOwnTruckHire
    {
        get { return _Is_Book_Own_Truck_Hire; }
        set { _Is_Book_Own_Truck_Hire = value; }
    }
    public Boolean IsMarketTruckLedgerAccountTruckWise
    {
        get { return _Is_Market_Truck_Ledger_Account_Truck_Wise; }
        set { _Is_Market_Truck_Ledger_Account_Truck_Wise = value; }
    }
    public Boolean IsAttachedTruckLedgerAccountTruckWise
    {
        get { return _Is_Attached_Truck_Ledger_Account_Truck_Wise; }
        set { _Is_Attached_Truck_Ledger_Account_Truck_Wise = value; }
    }
    public Boolean IsManagedTruckLedgerAccountTruckWise
    {
        get { return _Is_Managed_Truck_Ledger_Account_Truck_Wise; }
        set { _Is_Managed_Truck_Ledger_Account_Truck_Wise = value; }
    }

    public int CompanyDivisionServiceTaxId
    {
        get { return _Company_Division_Service_Tax_ID; }
        set { _Company_Division_Service_Tax_ID = value; }
    }
    public int DivisionId
    {
        get { return _Division_ID; }
        set { _Division_ID = value; }
    }
    public int TypeOfOrganisationId
    {
        get { return _Type_Of_Organisation_ID; }
        set { _Type_Of_Organisation_ID = value; }
    }

    
    public string ServiceTaxRegistrationNo
    {
        get { return _Service_Tax_Registration_No; }
        set { _Service_Tax_Registration_No = value; }
    }
    public string AssesseeCode
    {
        get { return _Assessee_Code; }
        set { _Assessee_Code = value; }
    }
    public string PremisesCodeNo
    {
        get { return _Premises_Code_No; }
        set { _Premises_Code_No = value; }
    }
    public string LargeTaxPayerUnit
    {
        get { return _Large_Tax_Payer_Unit; }
        set { _Large_Tax_Payer_Unit = value; }
    }
    public string DivisionCode
    {
        get { return _Division_Code; }
        set { _Division_Code = value; }
    }
    public string DivisionName
    {
        get { return _Division_Name; }
        set { _Division_Name = value; }
    }
    public string RangeCode
    {
        get { return _Range_Code; }
        set { _Range_Code = value; }
    }
    public string RangeName
    {
        get { return _Range_Name; }
        set { _Range_Name = value; }
    }

    public string CommissionRateCode
    {
        get { return _Commissionerate_Code; }
        set { _Commissionerate_Code = value; }
    }
    public string CommissionRateName
    {
        get { return _Commissionerate_Name; }
        set { _Commissionerate_Name = value; }
    }
    public string FocalBankCode
    {
        get { return _Focal_Bank_Code; }
        set { _Focal_Bank_Code = value; }
    }
    public string FocalBankName
    {
        get { return _Focal_Bank_Name; }
        set { _Focal_Bank_Name = value; }
    }
    public string FocalBankAddress
    {
        get { return _Focal_Bank_Address; }
        set { _Focal_Bank_Address = value; }
    }

    public DateTime DateOfRegistration
    {
        get { return _Date_Of_Registration; }
        set { _Date_Of_Registration = value; }
    }
    public Boolean IsLargeTaxPayer
    {
        get { return _Is_Large_Tax_Payer; }
        set { _Is_Large_Tax_Payer = value; }
    }
    public int LHPONatureOfPayemntIDForTDSDeduction
    {
        get { return _LHPO_Nature_Of_Payemnt_ID_For_TDS_Deduction; }
        set { _LHPO_Nature_Of_Payemnt_ID_For_TDS_Deduction = value; }
    }
    public decimal MaxAdvancePercentOfVehiclehireCharge
    {
        get { return _Max_Advance_Percent_Of_Vehicle_hire_Charge; }
        set { _Max_Advance_Percent_Of_Vehicle_hire_Charge = value; }
    }
   

	public CompanyParam() 
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #endregion
}

public class CompanyManager
{

    private static CompanyParam objCompanyParam;
    private static void InitCompanyParameters()
    {
        Common commonobj = new Common();
        string Query = "";
        DataSet ds;
        DataRow dr;

        Query = "select * from EC_Master_Company";
        ds = commonobj.EC_Common_Pass_Query(Query);

        dr = ds.Tables[0].Rows[0];
        objCompanyParam = new CompanyParam();
        objCompanyParam.CityId = (int)dr["City_Id"];
        objCompanyParam.PinCode = (int)dr["Pin_Code"];
        objCompanyParam.StateId = (int)dr["State_ID"];
        objCompanyParam.CountryId = (int)dr["Country_ID"];
        objCompanyParam.EmployeeId = (int)dr["Employee_ID"];
        objCompanyParam.AssesseeTypeId = (int)dr["Assessee_Type_Id"];
        objCompanyParam.AssesseeCategoryId = (int)dr["Assessee_Category_Id"];
        objCompanyParam.CompanyName = dr["Company_Name"].ToString();
        objCompanyParam.MailingName = dr["Mailing_Name"].ToString();
        objCompanyParam.AddressLine1 = dr["Address_Line_1"].ToString();
        objCompanyParam.AddressLine2 = dr["Address_Line_2"].ToString();
        objCompanyParam.StdCode = dr["Std_Code"].ToString();
        objCompanyParam.Phone1 = dr["Phone_1"].ToString();
        objCompanyParam.Phone2 = dr["Phone_2"].ToString();
        objCompanyParam.Fax = dr["Fax"].ToString();
        objCompanyParam.Email = dr["email"].ToString();
        objCompanyParam.TanNo = dr["TAN_No"].ToString();
        objCompanyParam.IncomeTaxCircle = dr["Income_tax_Circle"].ToString();
        objCompanyParam.DeductorType = dr["Deductor_Type"].ToString();
        objCompanyParam.Designation = dr["Designation"].ToString();
        objCompanyParam.PanNo = dr["PAN_No"].ToString();
        objCompanyParam.ClientCode = dr["Client_Code"].ToString();

        objCompanyParam.AllowFBTCategorySelection = (Boolean)dr["Allow_FBT_Category_Selection"];
        objCompanyParam.IsSurchargeApplicable = (Boolean)dr["Is_Surcharge_Applicable"];

        Query = "";
        Query = "select * from EC_Master_Company_Parameters";
        ds = new DataSet();
        ds = commonobj.EC_Common_Pass_Query(Query);
        dr = ds.Tables[0].Rows[0];

        objCompanyParam.CompanyParametersId = (int)dr["Company_Parameters_ID"];
        objCompanyParam.StandardBasicFreightUnitId = (int)dr["Standard_Basic_Freight_Unit_ID"];
        objCompanyParam.GcCaption = dr["GC_Caption"].ToString();
        objCompanyParam.LHPOCaption = dr["LHPO_Caption"].ToString();
        objCompanyParam.DivisionCaption = dr["Division_Caption"].ToString();
        objCompanyParam.StandardFreightRatePer = (decimal)dr["Standard_Freight_Rate_Per"];
        objCompanyParam.IsActivateDivision = (Boolean)dr["Is_Activate_Divisions"];
        objCompanyParam.IsAccountTransferRequired = (Boolean)dr["Is_Account_Transfer_Required"];
        objCompanyParam.IsCoLoaderBusiness = (Boolean)dr["Is_Co_Loader_Business"];
        objCompanyParam.IsMemoSeriesRequired = (Boolean)dr["Is_Memo_Series_Required"];
        objCompanyParam.IsLHPOSeriesRequired = (Boolean)dr["Is_LHPO_Series_Required"];
        objCompanyParam.IsALSRequired = (Boolean)dr["Is_ALS_Required"];
        objCompanyParam.IsTASRequired = (Boolean)dr["Is_TAS_Required"];
        objCompanyParam.IsPartLoadingRequired = (Boolean)dr["Is_Part_Loading_Required"];
        objCompanyParam.IsGcNumberEditable = (Boolean)dr["Is_GC_Number_Editable"];
        objCompanyParam.IsBookOwnTruckHire = dr["Is_Book_Own_Truck_Hire"].ToString() == "" ? false : (Boolean)dr["Is_Book_Own_Truck_Hire"];
        objCompanyParam.IsMarketTruckLedgerAccountTruckWise = dr["Is_Market_Truck_Ledger_Account_Truck_Wise"].ToString() != "" ? (Boolean)dr["Is_Market_Truck_Ledger_Account_Truck_Wise"] : false;
        objCompanyParam.IsAttachedTruckLedgerAccountTruckWise = dr["Is_Attached_Truck_Ledger_Account_Truck_Wise"].ToString() != "" ? (Boolean)dr["Is_Attached_Truck_Ledger_Account_Truck_Wise"] : false;
        objCompanyParam.IsManagedTruckLedgerAccountTruckWise = dr["Is_Managed_Truck_Ledger_Account_Truck_Wise"].ToString() != "" ? (Boolean)dr["Is_Managed_Truck_Ledger_Account_Truck_Wise"] : false;
        objCompanyParam.LHPONatureOfPayemntIDForTDSDeduction = (int)dr["LHPO_Nature_Of_Payemnt_ID_For_TDS_Deduction"];
        objCompanyParam.MaxAdvancePercentOfVehiclehireCharge = (decimal)dr["Max_Advance_Percent_Of_Vehicle_hire_Charge"];
        Query = "";
        Query = "select * from EC_Master_Company_Service_Tax_Details";
        ds = new DataSet();
        ds = commonobj.EC_Common_Pass_Query(Query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            dr = ds.Tables[0].Rows[0];

            objCompanyParam.CompanyDivisionServiceTaxId = (int)dr["Company_Division_Service_Tax_ID"];
            objCompanyParam.DivisionId = (int)dr["Division_ID"];
            objCompanyParam.TypeOfOrganisationId = (int)dr["Type_Of_Organisation_ID"];
            objCompanyParam.ServiceTaxRegistrationNo = dr["Service_Tax_Registration_No"].ToString();
            objCompanyParam.AssesseeCode = dr["Assessee_Code"].ToString();
            objCompanyParam.PremisesCodeNo = dr["Premises_Code_No"].ToString();
            objCompanyParam.LargeTaxPayerUnit = dr["Large_Tax_Payer_Unit"].ToString();
            objCompanyParam.DivisionCode = dr["Division_Code"].ToString();
            objCompanyParam.DivisionName = dr["Division_Name"].ToString();
            objCompanyParam.RangeCode = dr["Range_Code"].ToString();
            objCompanyParam.RangeName = dr["Range_Name"].ToString();
            objCompanyParam.CommissionRateCode = dr["Commissionerate_Code"].ToString();
            objCompanyParam.CommissionRateName = dr["Commissionerate_Name"].ToString();
            objCompanyParam.FocalBankCode = dr["Focal_Bank_Code"].ToString();
            objCompanyParam.FocalBankName = dr["Focal_Bank_Name"].ToString();
            objCompanyParam.FocalBankAddress = dr["Focal_Bank_Address"].ToString();
            objCompanyParam.DateOfRegistration = (DateTime)dr["Date_Of_Registration"];
            objCompanyParam.IsLargeTaxPayer = (Boolean)dr["Is_Large_Tax_Payer"];
        }
    }

    public static CompanyParam getCompanyParam()
    {
        if (objCompanyParam == null)
        {
            InitCompanyParameters();
            return objCompanyParam;
        }
        else
        {
            return objCompanyParam;
        }
    }


    public static void SetCompanySessiontoNothing()
    {
        CompanyParam objCompanyParam;
        objCompanyParam = getCompanyParam();
        objCompanyParam = null;
    }
}