using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ClassLibraryMVP;
using ClassLibraryMVP.General;

using Raj.EF.MasterView;
using Raj.EF.MasterModel;
using Raj.EC.ControlsView;

/// <summary>
/// Summary description for DriverDetailsPresenter
/// </summary>
/// 

namespace Raj.EF.MasterPresenter
{
    public class DriverDetailsPresenter:Presenter 
    {
        private IDriverDetailsView objIDriverDetailsView;
        private DriverDetailsModel objDriverDetailsModel;
        private DataSet _objDS;

        public DriverDetailsPresenter(IDriverDetailsView DriverDetailsView, bool isPostback)
        {
            objIDriverDetailsView = DriverDetailsView;
            objDriverDetailsModel = new DriverDetailsModel(objIDriverDetailsView);

            base.Init(objIDriverDetailsView, objDriverDetailsModel);

            if (!isPostback)
            {
                FillAllDropdowns();
                if (objIDriverDetailsView.keyID > 0)
                {
                    initValues();
                }
                else
                {
                    objIDriverDetailsView.BirthDate = DateTime.Now.Date;
                    objIDriverDetailsView.LicenseExpiryDate = DateTime.Now.Date;
                }
            }
        }

        public String  Duplicate_Driver_Check()
        {
            //bool _isDuplicate;
            //_isDuplicate = objDriverDetailsModel.ISDuplicateDriverCheck();
            //return _isDuplicate;
            String _Duplicate_Field;
            _Duplicate_Field = objDriverDetailsModel.ISDuplicateDriverCheck();
            return _Duplicate_Field;
        }

        //public bool Duplicate_Driver_Code()
        //{
        //    bool _isDuplicate;
        //    _isDuplicate = objDriverDetailsModel.ISDuplicateDriverCode();
        //    return _isDuplicate;
        //}

        private void initValues()
        {
            _objDS = objDriverDetailsModel.ReadValues();

            if (_objDS.Tables[0].Rows.Count > 0)
            {
                DataRow DR = _objDS.Tables[0].Rows[0];
                objIDriverDetailsView.DriverCode = DR["Driver_Code"].ToString();
                objIDriverDetailsView.DriverName = DR["Driver_Name"].ToString();
                objIDriverDetailsView.DriverNickName  = DR["Nick_Name"].ToString();

                objIDriverDetailsView.DriverImage = DR["Driver_Image"].ToString();
                objIDriverDetailsView.Qualification = DR["Qualification"].ToString();
                objIDriverDetailsView.DriverLicenseNo = DR["Driver_License_No"].ToString();
                objIDriverDetailsView.AddressView.AddressLine1 = DR["Address1"].ToString();
                objIDriverDetailsView.AddressView.AddressLine2 = DR["Address2"].ToString();
                objIDriverDetailsView.AddressView.PinCode = DR["Pin_Code"].ToString();
                
                objIDriverDetailsView.AddressView.MobileNo = DR["Mobile_No"].ToString();
                objIDriverDetailsView.AddressView.Phone1 = DR["Phone_No"].ToString();

                objIDriverDetailsView.DriverMobile1  = DR["Mobile_No"].ToString();
                objIDriverDetailsView.DriverMobile2  = DR["Phone_No"].ToString();


                objIDriverDetailsView.NativeAddress1 = DR["Native_Address1"].ToString();
                objIDriverDetailsView.NativeAddress2 = DR["Native_Address2"].ToString();
                objIDriverDetailsView.NativeContactNo = DR["Native_Contact_No"].ToString();
                objIDriverDetailsView.ReferenceName = DR["Reference_Name"].ToString();
                objIDriverDetailsView.ReferencePhone = DR["Reference_Phone"].ToString();
                objIDriverDetailsView.ReferenceMobile = DR["Reference_Mobile"].ToString();

                objIDriverDetailsView.DriverCategoryID = Util.String2Int(DR["Driver_Category_ID"].ToString());
                objIDriverDetailsView.ReligionID = Util.String2Int(DR["Religion_ID"].ToString());
                objIDriverDetailsView.LicenseIssueCityID = Util.String2Int(DR["License_Issue_City_ID"].ToString());
                objIDriverDetailsView.LicenseIssueStateID = Util.String2Int(DR["License_Issue_State_ID"].ToString());

                objIDriverDetailsView.LicenseIssueStateCode  = DR["License_Issue_State_Code"].ToString();


                objIDriverDetailsView.LicenseCategoryID = Util.String2Int(DR["License_Category_ID"].ToString());
                objIDriverDetailsView.AddressView.CityId = Util.String2Int(DR["City_ID"].ToString());

                objIDriverDetailsView.IsReliable = Util.String2Bool(DR["Is_Reliable"].ToString());
                objIDriverDetailsView.IsMarried = Util.String2Bool(DR["Is_Married"].ToString());

                objIDriverDetailsView.BirthDate = Convert.ToDateTime(DR["Birth_Date"].ToString());
                objIDriverDetailsView.LicenseExpiryDate = Convert.ToDateTime(DR["License_Expiry_Date"].ToString());
                objIDriverDetailsView.BloodGroup = DR["Blood_Group"].ToString();

                objIDriverDetailsView.OpeningBalance = Util.String2Decimal(DR["Opening_Balance"].ToString());
                objIDriverDetailsView.IsLicenseAuthenticated = Util.String2Bool(DR["Is_License_Authenticated"].ToString());

                if (objIDriverDetailsView.IsLicenseAuthenticated == true)
                {
                    objIDriverDetailsView.LicenseAuthenticatedBy = DR["License_Authenticated_By"].ToString();
                }

                objIDriverDetailsView.IseCargoUser = Util.String2Bool(DR["IseCargoUser"].ToString());

                objIDriverDetailsView.AadharNo  = DR["AadharNo"].ToString();
                objIDriverDetailsView.HistoryRemarks  = DR["HistoryRemarks"].ToString();
                objIDriverDetailsView.ReferenceDate  = Convert.ToDateTime(DR["ReferenceDate"].ToString());

                objIDriverDetailsView.ReferenceName2 = DR["Reference_Name2"].ToString();
                objIDriverDetailsView.ReferencePhone2 = DR["Reference_Phone2"].ToString();
                objIDriverDetailsView.ReferenceMobile2 = DR["Reference_Mobile2"].ToString();
                objIDriverDetailsView.ReferenceDate2 = Convert.ToDateTime(DR["ReferenceDate2"].ToString());




            }
        }

        private void FillAllDropdowns()
        {
            _objDS = objDriverDetailsModel.FillValues();
            objIDriverDetailsView.BindDDLDriverCategory = _objDS.Tables[0];
            objIDriverDetailsView.BindDDLReligion = _objDS.Tables[1];
            objIDriverDetailsView.BindDDLLicenseIssueCity = _objDS.Tables[2];
            objIDriverDetailsView.BindDDLLicenseCategory = _objDS.Tables[3];
            objIDriverDetailsView.BindDDLLAccountEffectType = _objDS.Tables[4];
            objIDriverDetailsView.BindDDLLicenseIssueState  = _objDS.Tables[5];

        }
    }
}