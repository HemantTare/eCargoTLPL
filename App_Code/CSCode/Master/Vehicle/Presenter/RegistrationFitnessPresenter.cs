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
    public class RegistrationFitnessPresenter:Presenter 
    {
        private IRegistrationFitnessView objIRegistrationFitnessView;
        private RegistrationFitnessModel objRegistrationFitnessModel;
        private DataSet objDS;

        public RegistrationFitnessPresenter(IRegistrationFitnessView RegistrationFitnessView,bool IsPostBack)
        {
            objIRegistrationFitnessView = RegistrationFitnessView;
            objRegistrationFitnessModel = new RegistrationFitnessModel(objIRegistrationFitnessView);

            base.Init(objIRegistrationFitnessView, objRegistrationFitnessModel);

            if (IsPostBack == false)
            {
                objIRegistrationFitnessView.Fill_DDL_RegistrationState = objRegistrationFitnessModel.FillValues();

                if (objIRegistrationFitnessView.keyID > 0)
                {
                    initValues();
                }
            }
        }
        public void initValues()
        {
            objDS = objRegistrationFitnessModel.ReadValues();

            if (objDS.Tables[0].Rows.Count > 0)
            { 
              DataRow DR=objDS.Tables[0].Rows[0];

              objIRegistrationFitnessView.RegistrationDate = Convert.ToDateTime(DR["Registration_Date"].ToString());
              objIRegistrationFitnessView.RegistraionCertificateNo = DR["Registration_Certificate_No"].ToString();
              objIRegistrationFitnessView.RegistrationStateID = Util.String2Int(DR["Registration_State_ID"].ToString());

              Fill_RTO();

              objIRegistrationFitnessView.RegistraionRtoId = Util.String2Int(DR["Fitness_RTO_City_ID"].ToString());
              objIRegistrationFitnessView.RegistrationFee = Util.String2Decimal(DR["Registration_Fee"].ToString());
              objIRegistrationFitnessView.FitnesCertificateNo = DR["Fitness_Certificate_No"].ToString();
              objIRegistrationFitnessView.FitnessRtoId =Util.String2Int(DR["Fitness_RTO_City_ID"].ToString());
              objIRegistrationFitnessView.IssueDate = Convert.ToDateTime(DR["Fitness_Issue_Date"].ToString());
              objIRegistrationFitnessView.ValidUpTO = Convert.ToDateTime(DR["Fitness_Valid_Upto"].ToString());
              objIRegistrationFitnessView.Amount = Util.String2Decimal(DR["Fitness_Amount"].ToString());
             }
        }

        public void Fill_RTO()
        {
            objIRegistrationFitnessView.Fill_RTO = objRegistrationFitnessModel.FillRTO();
        }

        public void Save()
        {
            base.DBSave();
        }
       
    }
}
