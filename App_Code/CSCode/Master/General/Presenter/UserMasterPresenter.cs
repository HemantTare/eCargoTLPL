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
using ClassLibraryMVP;
using Raj.EC.GeneralView;
using Raj.EC.GeneralModel;

/// <summary>
/// Summary description for UserMasterPresenter
/// </summary>
namespace Raj.EC.GeneralPresenter
{
    public class UserMasterPresenter : Presenter
    {
        private IUserMasterView objIUserMasterView;
        private UserMasterModel objUserMasterModel;
        private DataSet objDS;

        public UserMasterPresenter(IUserMasterView userMasterView, bool IsPostBack)
        {
            objIUserMasterView = userMasterView;
            objUserMasterModel = new UserMasterModel(objIUserMasterView);

            base.Init(objIUserMasterView, objUserMasterModel);

            if (!IsPostBack)
            {
                FillBranch();
                FillProfile();
                initValues();
            }
        }
        public void FillProfile()
        {

            objIUserMasterView.BindProfile=objUserMasterModel.FillValues();

        }

        public void FillBranch()
        {

            objIUserMasterView.BindBranch  = objUserMasterModel.FillBranch();

        }

        private void initValues()
        {


            if (objIUserMasterView.keyID > 0)
            {
                objDS = objUserMasterModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIUserMasterView.NonEmpUserName = DR["Non_Emp_User_Name"].ToString();
                    objIUserMasterView.AddressView.AddressLine1 = DR["Address1"].ToString();
                    objIUserMasterView.AddressView.AddressLine2 = DR["Address2"].ToString();
                    objIUserMasterView.AddressView.CityId = Util.String2Int(DR["City_ID"].ToString());
                    objIUserMasterView.AddressView.PinCode = DR["Pin_Code"].ToString();
                    objIUserMasterView.AddressView.StdCode = DR["std_Code"].ToString();
                    objIUserMasterView.AddressView.Phone1 = DR["Phone1"].ToString();
                    objIUserMasterView.AddressView.Phone2 = DR["Phone2"].ToString();
                    objIUserMasterView.AddressView.MobileNo = DR["Mobile_No"].ToString();
                    objIUserMasterView.AddressView.FaxNo = DR["Fax"].ToString();
                    objIUserMasterView.AddressView.EmailId = DR["Email_ID"].ToString();
                    objIUserMasterView.ProfileId =Util.String2Int(DR["Profile_ID"].ToString());
                    objIUserMasterView.BranchId = Util.String2Int(DR["Branch_ID"].ToString());
                    objIUserMasterView.IsActive = Util.String2Bool(DR["Is_Active"].ToString());
                    
                }
            }
        }
        public void Save()
        {
             base.DBSave();
           
        }
    }
}