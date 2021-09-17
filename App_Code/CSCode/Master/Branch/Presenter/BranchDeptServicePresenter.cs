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
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for BranchDeptServicePresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class BranchDeptServicePresenter : Presenter
    {
        private IBranchDeptServiceView objIBranchDeptServiceView;
        private BranchDeptServiceModel objBranchDeptServiceModel;
        private DataSet objDS;

        public BranchDeptServicePresenter(IBranchDeptServiceView BranchDeptServiceView, bool isPostback)
        {
            objIBranchDeptServiceView = BranchDeptServiceView;
            objBranchDeptServiceModel = new BranchDeptServiceModel(objIBranchDeptServiceView);

            base.Init(objIBranchDeptServiceView, objBranchDeptServiceModel);

            if (!isPostback)
            {
                fillValues();
                initValues();
            }
        }

        private void fillValues()
        {
            objDS = objBranchDeptServiceModel.FillValues();
            objIBranchDeptServiceView.BindDepartment = objDS.Tables[0];
        }
            

        private void initValues()
        {
            if (objIBranchDeptServiceView.keyID > 0)
            {
                objDS = objBranchDeptServiceModel.ReadValues();

                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objIBranchDeptServiceView.IsBookingAllowed = Util.String2Bool(objDR["Is_VTrans_Booking"].ToString());
                    objIBranchDeptServiceView.IsDeliveryAllowed = Util.String2Bool(objDR["Is_VTrans_Delivery"].ToString());
                    objIBranchDeptServiceView.IsCrossingBranch = Util.String2Bool(objDR["Is_Crossing"].ToString());
                    objIBranchDeptServiceView.IsFranchiseeBranch = Util.String2Bool(objDR["Is_Frenchisee"].ToString());
                    objIBranchDeptServiceView.IsComputersiedBranch = Util.String2Bool(objDR["Is_Compuetrised"].ToString());
                    objIBranchDeptServiceView.IsOctroiApplicable = Util.String2Bool(objDR["Is_Octroi"].ToString());
                }
            }
        }

    }
}
