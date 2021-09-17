
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.MasterView;

/// <summary>
/// Summary description for CommodityModel
/// </summary>


namespace Raj.EC.MasterModel
{
    public class EmployeeModel : IModel
    {

        private IEmployeeView objIEmployeeView;
        private DataSet _ds;
        private DAL _objDAL = new DAL();
        private int _userId = Convert.ToInt32(UserManager.getUserParam().UserId);
        private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;


        public EmployeeModel(IEmployeeView EmployeeView)
        {
            objIEmployeeView = EmployeeView;
        }

        public DataSet ReadValues()
        {

            SqlParameter[] objSqlParam ={ _objDAL.MakeInParams("@Emp_ID", SqlDbType.Int, 0, objIEmployeeView.keyID) };


            _objDAL.RunProc("EC_Mst_Employee_ReadValues", objSqlParam, ref _ds);

            return _ds;


        }


        public DataSet ReadDivisionsValues()
        {

            SqlParameter[] objSqlParam ={ _objDAL.MakeInParams("@Emp_ID", SqlDbType.Int, 0, objIEmployeeView.keyID) };


            _objDAL.RunProc("dbo.EC_Mst_Employee_ReadDivisionsValues", objSqlParam, ref _ds);

            return _ds;


        }


        public Message Save()
        {
            Message objMessage = new Message();


            SqlParameter[] objSqlParam = {_objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            _objDAL.MakeOutParams("@User_Name", SqlDbType.VarChar, 10),
            _objDAL.MakeOutParams("@Prompt_For_User_Creation", SqlDbType.Bit, 0),
            _objDAL.MakeInParams("@Emp_ID", SqlDbType.Int, 0,objIEmployeeView.keyID ),
            _objDAL.MakeInParams("@Emp_Code", SqlDbType.VarChar  , 10,objIEmployeeView.EmployeeCode),
            _objDAL.MakeInParams("@First_Name", SqlDbType.VarChar, 50,objIEmployeeView.FirstName),            
            _objDAL.MakeInParams("@Middle_Name", SqlDbType.VarChar, 50,objIEmployeeView.MiddleName),            
            _objDAL.MakeInParams("@Last_Name", SqlDbType.VarChar, 50,objIEmployeeView.LastName),            
            _objDAL.MakeInParams("@Department_Id", SqlDbType.Int , 100,objIEmployeeView.Department_Id),
            _objDAL.MakeInParams("@UserJobProfile", SqlDbType.Int  , 100,objIEmployeeView.UserProfile_Id),
            _objDAL.MakeInParams("@Email", SqlDbType.VarChar, 50,objIEmployeeView.Email),            
            _objDAL.MakeInParams("@Qualification", SqlDbType.VarChar, 50,objIEmployeeView.Qualification),            
            _objDAL.MakeInParams("@PreviousJobProfile", SqlDbType.VarChar, 250,objIEmployeeView.PreviousJobProfile),            
            _objDAL.MakeInParams("@Is_eCargoUser", SqlDbType.Bit, 1,objIEmployeeView.Is_eCargoUser), 
            _objDAL.MakeInParams("@Is_Active", SqlDbType.Bit , 0,objIEmployeeView.Is_Active ), 
            _objDAL.MakeInParams("@Is_Account_Locked",SqlDbType.Bit,0,objIEmployeeView.Is_Account_Locked),
            _objDAL.MakeInParams("@Is_HO", SqlDbType.Bit  , 100,objIEmployeeView.Is_HO  ),
            _objDAL.MakeInParams("@Region_Id", SqlDbType.Int  , 100,objIEmployeeView.Region_Id  ),            
            _objDAL.MakeInParams("@Area_Id", SqlDbType.Int  , 100,objIEmployeeView.Area_Id    ),
            _objDAL.MakeInParams("@Branch_Id", SqlDbType.Int  , 100,objIEmployeeView.Branch_Id ),
            _objDAL.MakeInParams("@Profile_ID", SqlDbType.Int  , 100,objIEmployeeView.UserProfile_Id  ),
            _objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userId),
            _objDAL.MakeInParams("@Applicable_Divisions_Details_Xml",SqlDbType.Xml,0,objIEmployeeView.Applicable_Divisions_Details_Xml ),
             //==================================================ADDED FOR PAYROLL==================================================
               _objDAL.MakeInParams("@Consider_For_Payroll", SqlDbType.Bit  , 0,objIEmployeeView.Consider_Payroll_Yes),            
               _objDAL.MakeInParams("@Birth_Date",SqlDbType.SmallDateTime,0,objIEmployeeView.Birth_Date),
               _objDAL.MakeInParams("@DOJ",SqlDbType.SmallDateTime,0,objIEmployeeView.DOJ),
               _objDAL.MakeInParams("@Basic_Rate",SqlDbType.Decimal,0,objIEmployeeView.Basic_Rate),
               _objDAL.MakeInParams("@Probation_Period",SqlDbType.Int,0,objIEmployeeView.Probation_Period),                       
               _objDAL.MakeInParams("@Confirmation_Date",SqlDbType.SmallDateTime,0,objIEmployeeView.Confirmation_Date),
               _objDAL.MakeInParams("@Weekly_Off",SqlDbType.Int,0,objIEmployeeView.Weekly_Off)
            //===================================================ADDED FOR PAYROLL===================================================
            };

            _objDAL.RunProc("EC_Mst_Employee_Save", objSqlParam);

            //_objDAL.MakeInParams("@TDS_Lower_Rate", SqlDbType.Bit, 0, objIPetrolPumpView.PetrolPumpFinanceDetailsView.TDS_Lower_Rate),

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);
            string UserName = Convert.ToString(objSqlParam[2].Value);
            Boolean Prompt_For_User_Creation = Convert.ToBoolean(objSqlParam[3].Value);

            if (objMessage.messageID == 0)
            {

                string _Msg;

                if (Prompt_For_User_Creation == true)
                    _Msg = UserName + " Created Sucessfully";
                else
                    _Msg = "Saved SuccessFully";


                string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;

                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");
            }
            return objMessage;
        }

        public DataSet Fill_Values()
        {

            _objDAL.RunProc("EC_Mst_Employee_FillValues", ref _ds);

            return _ds;
        }


        public DataSet Fill_Profile()
        {


            SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 50, objIEmployeeView.HierarchyCode) };

            _objDAL.RunProc("dbo.EC_Mst_Employee_Fill_Profile", objSqlParam, ref _ds);


            return _ds;
        }

        public DataSet Fill_Divisions()
        {
            Int32 Main_ID = 0;

            if (objIEmployeeView.HierarchiWithIdView.AreaID > 0)
            {
                Main_ID = objIEmployeeView.HierarchiWithIdView.AreaID;
            }

            else if (objIEmployeeView.HierarchiWithIdView.RegionID > 0)
            {
                Main_ID = objIEmployeeView.HierarchiWithIdView.RegionID;
            }
            else if (objIEmployeeView.HierarchiWithIdView.BranchID > 0)
            {
                Main_ID = objIEmployeeView.HierarchiWithIdView.BranchID;
            }
            else if (objIEmployeeView.HierarchiWithIdView.Is_Ho)
            {
                Main_ID = 0;
            }


            SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 50, objIEmployeeView.HierarchyCode),
                                            _objDAL.MakeInParams("@Main_ID", SqlDbType.Int , 50, Main_ID),
                                            _objDAL.MakeInParams("@Is_Ho", SqlDbType.Bit , 50, objIEmployeeView.HierarchiWithIdView.Is_Ho )};

            _objDAL.RunProc("dbo.EC_Mst_Employee_Fill_Divisions", objSqlParam, ref _ds);


            return _ds;
        }

    }
}




