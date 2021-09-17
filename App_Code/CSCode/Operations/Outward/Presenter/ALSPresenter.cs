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
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

/// <summary>
/// Summary description for ALSPresenter
/// </summary>
namespace Raj.EC.OperationPresenter
{
    public class ALSPresenter : Presenter
    {
        private IALSView objIALSView;
        private ALSModel objALSModel;
        private DataSet objDS;
        private DataSet DS = new DataSet();

        public ALSPresenter(IALSView ALSView, bool isPostback)
        {
            objIALSView = ALSView;
            objALSModel = new ALSModel(objIALSView);
            base.Init(objIALSView, objALSModel);

            if (!isPostback)
            {
                objIALSView.ALSDate = DateTime.Now.Date;
                fillValues();
                initValues();
            }
        }

        private void fillValues()
        {
            objDS = objALSModel.FillValues();
            objIALSView.BindVehicleCotegory = objDS.Tables[0];
        }

        public void fillgrid()
        {
            objDS = objALSModel.ReadValues();
            objIALSView.SessionBindALSGrid = objDS.Tables[0];

            if (objIALSView.keyID > 0)
            {
                if (objDS.Tables[1].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];

                    objIALSView.Total_No_Of_GC = Util.String2Int(objDR["Total_No_Of_GC"].ToString());
                    objIALSView.Total_Loded_Articles = Util.String2Int(objDR["Total_Loaded_Articles"].ToString());
                    objIALSView.Total_Loded_Weight = Util.String2Decimal(objDR["Total_Loaded_Weight"].ToString());
                }
            }
        }

        private void initValues()
        {
            objDS = objALSModel.ReadValues();
            objIALSView.SessionBindALSGrid = objDS.Tables[0];

            if (objIALSView.keyID > 0)
            {
                if (objDS.Tables[1].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];
                  
                    objIALSView.VehicleCotegoryID = Util.String2Int(objDR["Vehicle_Category_ID"].ToString());
                    objIALSView.VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());
                    objIALSView.ALSDate = Convert.ToDateTime(objDR["ALS_Date"].ToString());
                    objIALSView.Total_No_Of_GC = Util.String2Int(objDR["Total_No_Of_GC"].ToString());

                    objIALSView.Remarks = objDR["Remarks"].ToString();
                    objIALSView.ALSNo = objDR["ALS_No_For_Print"].ToString();
                    objIALSView.SetSupervisorID(objDR["Emp_Name"].ToString(), objDR["Emp_Id"].ToString());

                    objIALSView.Total_Loded_Articles = Util.String2Int(objDR["Total_Loaded_Articles"].ToString());
                    objIALSView.Total_Loded_Weight = Util.String2Decimal(objDR["Total_Loaded_Weight"].ToString());
                }
            }
        }

        public void Save()
        {
            base.DBSave();
        }
    }
}
