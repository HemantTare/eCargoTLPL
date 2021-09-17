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
using Raj.EC.MasterView;
using Raj.EC.MasterModel;


/// <summary>
/// Summary description for AreaDepartmentPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class AreaDepartmentPresenter : Presenter
    {
        private IAreaDepartmentView objIAreaDepartmentView;
        private AreaDepartmentModel objAreaDepartmentModel;
        private DataSet objDS;

        public AreaDepartmentPresenter(IAreaDepartmentView areaGeneralDetailsView, bool IsPostBack)
        {
            objIAreaDepartmentView = areaGeneralDetailsView;
            objAreaDepartmentModel = new AreaDepartmentModel(objIAreaDepartmentView);

            base.Init(objIAreaDepartmentView, objAreaDepartmentModel);

            if (!IsPostBack)
            {
                FillDepartment();
                initValues();
            }
        }

        public void FillDepartment()
        {
            objIAreaDepartmentView.BindChkListDepartment = objAreaDepartmentModel.GetDepartmentValues();
        }
        public void Save()
        {
            //base.DBSave();
            objAreaDepartmentModel.Save();
        }
        private void fillValues()
        {

            objDS = objAreaDepartmentModel.FillValues();
            objIAreaDepartmentView.BindCashLedger = objDS.Tables[0];
            objIAreaDepartmentView.BindBankLedger = objDS.Tables[1];
           

        }
        private void initValues()
        {
            fillValues();

            if (objIAreaDepartmentView.keyID > 0)
            {
                objDS = objAreaDepartmentModel.ReadParameterValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    
                    objIAreaDepartmentView.CashLimit = Util.String2Decimal(DR["Cash_Limit"].ToString());
                    objIAreaDepartmentView.BankLimit = Util.String2Decimal(DR["Bank_Limit"].ToString());
                    objIAreaDepartmentView.CashLedgerId = Util.String2Int(DR["Cash_Ledger_Id"].ToString());
                    objIAreaDepartmentView.BankLedgerId = Util.String2Int(DR["Bank_Ledger_Id"].ToString());


                }
                objDS = objAreaDepartmentModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIAreaDepartmentView.BindChkListDepartment = objDS;
                }

            }



        }

    }
}
