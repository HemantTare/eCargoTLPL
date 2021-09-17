
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;
using ClassLibraryMVP;

/// <summary>
/// Summary description for EmployeePresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{
    public class EmployeePresenter : Presenter
    {
       private IEmployeeView objIEmployeeView;
        private EmployeeModel objEmployeeModel;
        private DataSet objDS;

        public EmployeePresenter(IEmployeeView EmployeeView, bool IsPostBack)
        {
            objIEmployeeView = EmployeeView;
            objEmployeeModel = new EmployeeModel(objIEmployeeView);

            base.Init(objIEmployeeView, objEmployeeModel);

            if (!IsPostBack)
            {
                //=============================ADDED FOR PAYROLL=========================
                objIEmployeeView.Birth_Date = System.DateTime.Now;
                objIEmployeeView.DOJ = System.DateTime.Now;
                objIEmployeeView.Confirmation_Date = Convert.ToDateTime("1/1/1900");
                //=======================================================================                  
                initValues();                 
            }
        }

        public void Fill_Values()
        {
            objDS = objEmployeeModel.Fill_Values();

            objIEmployeeView.BindDepartment    = objDS.Tables[0];
            //objIEmployeeView.BindUserProfile  = objDS.Tables[1];
           // objIEmployeeView.BindDivision  = objDS.Tables[2];

        }

        public void Fill_Profile(object sender, EventArgs e)
        {
            objDS = objEmployeeModel.Fill_Profile();            
            objIEmployeeView.BindUserProfile = objDS.Tables[0];          
        }

        public void Fill_Divisions(object sender, EventArgs e)
        {
            objDS = objEmployeeModel.Fill_Divisions();
            objIEmployeeView.BindDivision   = objDS.Tables[0];
        }

        private void initValues()
        {
             Fill_Values();

            if (objIEmployeeView.keyID > 0)
            {
                ReadValues();
            }
        }

        public  void ReadValues()
        {
            objDS = objEmployeeModel.ReadValues();
            
            if (objDS.Tables[0].Rows.Count > 0)
            {
                objIEmployeeView.EmployeeCode = Convert.ToString(objDS.Tables[0].Rows[0]["Emp_Code"]);
                objIEmployeeView.FirstName= Convert.ToString(objDS.Tables[0].Rows[0]["First_Name"]);
                objIEmployeeView.MiddleName = Convert.ToString(objDS.Tables[0].Rows[0]["Middle_Name"]);
                objIEmployeeView.LastName = Convert.ToString(objDS.Tables[0].Rows[0]["Last_Name"]);


                objIEmployeeView.Email = Convert.ToString(objDS.Tables[0].Rows[0]["eMail_ID"]);
                objIEmployeeView.Qualification = Convert.ToString(objDS.Tables[0].Rows[0]["Qualification"]);

                objIEmployeeView.PreviousJobProfile = Convert.ToString(objDS.Tables[0].Rows[0]["Previous_Job_Details"]);
                objIEmployeeView.Department_Id = Convert.ToInt32(objDS.Tables[0].Rows[0]["Department_Id"]);
                objIEmployeeView.Is_eCargoUser = Convert.ToBoolean(objDS.Tables[0].Rows[0]["Is_USer"]);
                objIEmployeeView.Is_Active = Convert.ToBoolean(objDS.Tables[0].Rows[0]["Is_Active"]);
                objIEmployeeView.Is_Account_Locked = Convert.ToBoolean(objDS.Tables[0].Rows[0]["Is_Account_Locked"]);

                objIEmployeeView.HierarchiWithIdView.Is_Ho = Convert.ToBoolean(objDS.Tables[0].Rows[0]["HO"]);
                objIEmployeeView.HierarchiWithIdView.RegionID  = Convert.ToInt32(objDS.Tables[0].Rows[0]["Region_Id"]);
                objIEmployeeView.HierarchiWithIdView.AreaID  = Convert.ToInt32(objDS.Tables[0].Rows[0]["Area_Id"]);
                objIEmployeeView.HierarchiWithIdView.BranchID  = Convert.ToInt32(objDS.Tables[0].Rows[0]["Branch_Id"]);

                objEmployeeModel.Fill_Profile();
                objIEmployeeView.UserProfile_Id = Convert.ToInt32(objDS.Tables[0].Rows[0]["Profile_ID"]);
                //===============================================ADDED FOR PAYROLL========================================
                objIEmployeeView.Consider_Payroll_Yes = Util.String2Bool(objDS.Tables[0].Rows[0]["Consider_For_Payroll"].ToString());
                if (objIEmployeeView.Consider_Payroll_Yes)
                {
                    objIEmployeeView.Consider_Payroll_Not = false;
                }
                else
                {
                    objIEmployeeView.Consider_Payroll_Not = true;
                }
                objIEmployeeView.Birth_Date = Convert.ToDateTime(objDS.Tables[0].Rows[0]["Birth_Date"].ToString());
                objIEmployeeView.DOJ = Convert.ToDateTime(objDS.Tables[0].Rows[0]["DOJ"].ToString());
                objIEmployeeView.Basic_Rate = Util.String2Decimal(objDS.Tables[0].Rows[0]["Basic_Rate"].ToString());
                objIEmployeeView.Probation_Period = Util.String2Int(objDS.Tables[0].Rows[0]["Probation_Period"].ToString());
                objIEmployeeView.Confirmation_Date = Convert.ToDateTime(objDS.Tables[0].Rows[0]["Confirmation_Date"].ToString());
                objIEmployeeView.Weekly_Off = Util.String2Int(objDS.Tables[0].Rows[0]["Weekly_Off"].ToString());
                //========================================================================================================
            }
        }

        public DataSet ReadDivisionsValues()
        {
            objDS = objEmployeeModel.ReadDivisionsValues();
            return objDS;
        }

        public void save()
        {
            
            base.DBSave();
          // objEmployeeModel.Save();
        }
         
    }
}






