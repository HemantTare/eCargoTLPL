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

namespace Raj.EC.OperationPresenter
{
    public class GDCPresenter : Presenter
    {
        private IGDCView objIGDCView;
        private GDCModel objGDCModel;
        private DataSet objDS;

        public GDCPresenter(IGDCView GDCView, bool isPostBack)
        {
            objIGDCView = GDCView;
            objGDCModel = new GDCModel(objIGDCView);
            base.Init(objIGDCView, objGDCModel);

            if (!isPostBack)
            {
                objIGDCView.GDCDate = DateTime.Now.Date;
                FillValues();
                initValues();
            }
        }

        public void FillValues()
        {
            objDS = objGDCModel.FillValues();
            objIGDCView.SessionBindDDLDeliveryMode = objDS.Tables[0];
            objIGDCView.SessionBindDlyStatus = objDS.Tables[1];
            objIGDCView.SessionBindDDLUndelReason = objDS.Tables[2];

            objIGDCView.BindPhotoType = objDS.Tables[3];
            objIGDCView.BindVehicleType = objDS.Tables[4];

        }

        public bool ValidateConsigneeClient()
        {
            bool Is_ValidateConsigneeList = objGDCModel.ValidateConsigneeClient();
            return Is_ValidateConsigneeList;
        }

        public void fillgrid()
        {
            objDS = objGDCModel.ReadValues();
            objIGDCView.SessionBindGDCGrid = objDS.Tables[0];
            if (objIGDCView.keyID > 0)
            {
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];

                    objIGDCView.SetSuperviserId(objDR["Supervisor_Name"].ToString(), objDR["Supervisor_ID"].ToString());
                    objIGDCView.Total_No_Of_GC = Util.String2Int(objDR["Total_No_Of_GC"].ToString());
                    objIGDCView.Total_Delivered_Articles = Util.String2Int(objDR["Total_DDC_Articles"].ToString());
                    objIGDCView.Total_Delivered_Weight = Util.String2Decimal(objDR["Total_DDC_Actual_Wt"].ToString());

                    objIGDCView.Remarks = objDR["Remarks"].ToString();
                }
            }
        }

        public void initValues()
        {
            objDS = objGDCModel.ReadValues();
            objIGDCView.SessionBindGDCGrid = objDS.Tables[0];

            if (objIGDCView.keyID > 0)
            {
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];

                    objIGDCView.GDCDate = Convert.ToDateTime(objDR["DDC_Date"].ToString());
                    objIGDCView.GDCNo = objDR["DDC_No_For_Print"].ToString();

                    objIGDCView.SetSuperviserId(objDR["Supervisor_Name"].ToString(), objDR["Supervisor_ID"].ToString());
                    objIGDCView.Total_No_Of_GC = Util.String2Int(objDR["Total_No_Of_GC"].ToString());
                    objIGDCView.Total_Delivered_Articles = Util.String2Int(objDR["Total_DDC_Articles"].ToString());
                    objIGDCView.Total_Delivered_Weight = Util.String2Decimal(objDR["Total_DDC_Actual_Wt"].ToString());

                    objIGDCView.Remarks = objDR["Remarks"].ToString();

                    objIGDCView.DeliveredTo = objDR["Delivered_To"].ToString();
                    objIGDCView.DeliveredToMobile = objDR["Delivered_to_Mobile"].ToString();
                    objIGDCView.PhotoIDType = Util.String2Int(objDR["PhotoID_Type_ID"].ToString());
                    objIGDCView.PhotoIDNo = objDR["PhotoID_No"].ToString();
                    objIGDCView.VehicleType = Util.String2Int(objDR["Vehicle_Type_ID"].ToString());
                    objIGDCView.VehicleNoPart1 = objDR["Vehicle_No_Part1"].ToString();
                    objIGDCView.VehicleNoPart2 = objDR["Vehicle_No_Part2"].ToString();
                    objIGDCView.VehicleNoPart3 = objDR["Vehicle_No_Part3"].ToString();
                    objIGDCView.VehicleNoPart4 = objDR["Vehicle_No_Part4"].ToString();
                }
            }
        }

        public void save()
        {
            base.DBSave();
            //objGDCModel.Save();
        }
    }
}