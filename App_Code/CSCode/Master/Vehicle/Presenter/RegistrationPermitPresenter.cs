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
using Raj.EF.MasterView;
using Raj.EF.MasterModel;

namespace Raj.EF.MasterPresenter
{
    public class RegistrationPermitPresenter:Presenter 
    {
        private IRegistrationPermitView objIRegistrationPermitView;
        private RegistrationPermitModel objRegistrationPermitModel;
        private DataSet objDS;

        public RegistrationPermitPresenter(IRegistrationPermitView RegistrationPermitView,bool IsPostBack)
        {
            objIRegistrationPermitView = RegistrationPermitView;
            objRegistrationPermitModel = new RegistrationPermitModel(objIRegistrationPermitView);
            base.Init(objIRegistrationPermitView, objRegistrationPermitModel);

            if (IsPostBack == false)
            {
                FillAllDropdownAndGrid();

                if (objIRegistrationPermitView.keyID > 0)
                {
                    initValues();
                }
                else
                {
                    objIRegistrationPermitView.PermitValidFrom = System.DateTime.Now;
                    objIRegistrationPermitView.PermitValidUpTo = System.DateTime.Now;
                    objIRegistrationPermitView.MainSrNo = 1;
                }
            }
        }

        public void FillAllDropdownAndGrid()
        {

            objDS = objRegistrationPermitModel.FillValues();

             objIRegistrationPermitView.SessionStateDropDown = objDS.Tables[0];
             objIRegistrationPermitView.Bind_ddl_Permit_Type = objDS.Tables[1];

            objIRegistrationPermitView.SessionPermitTaxDetails = objRegistrationPermitModel.BindGridRegistrationPermit();
            objIRegistrationPermitView.SessionTemparayRegistrationPermitGrid = objRegistrationPermitModel.BindGridTempararyRegistrationPermit();
        }

        private void initValues()
        {
            objDS = objRegistrationPermitModel.ReadValues();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                objIRegistrationPermitView.PermitTypeId = Util.String2Int(objDR["Permit_Type_ID"].ToString());
                objIRegistrationPermitView.PermitNo = objDR["Permit_No"].ToString();
                objIRegistrationPermitView.PermitDocumentNo = objDR["Permit_Document_No"].ToString();
                objIRegistrationPermitView.PermitValidFrom = Convert.ToDateTime(objDR["Permit_Valid_From"]);
                objIRegistrationPermitView.PermitValidUpTo = Convert.ToDateTime(objDR["Permit_Valid_Upto"]);
                objIRegistrationPermitView.MainSrNo = Util.String2Int(objDR["Main_Sr_no"].ToString());
            }
        }

        public bool ValidateDuplicatePermitNo()
        {
            return objRegistrationPermitModel.ValidateDuplicatePermitNo();
        }
    }
}
