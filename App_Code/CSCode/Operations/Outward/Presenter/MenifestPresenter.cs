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
/// Summary description for MenifestPresenter
/// </summary>
namespace Raj.EC.OperationPresenter
{
    public class MenifestPresenter : Presenter
    {
        private IMenifestView objIMenifestView;
        private MenifestModel objMenifestModel;
        private DataSet objDS;
        private DataSet DS = new DataSet();

        public MenifestPresenter(IMenifestView MenifestView, bool isPostback)
        {
            objIMenifestView = MenifestView;
            objMenifestModel = new MenifestModel(objIMenifestView);
            base.Init(objIMenifestView, objMenifestModel);

            if (!isPostback)
            {
                objIMenifestView.MenifestDate = DateTime.Now.Date;
                objIMenifestView.ArrivalDeliveryDate = DateTime.Now.Date;
                fillValues();
                initValues();
            }
        }

        private void fillValues()
        {
            objDS = objMenifestModel.FillValues();
            objIMenifestView.BindMenifestType = objDS.Tables[0];
            objIMenifestView.BindVehicleCotegory = objDS.Tables[1];
        }

        public void fillALSNO()
        {
            objDS = objMenifestModel.Fill_ALS_On_Vehicle_Selection();
            objIMenifestView.BindALSNo = objDS.Tables[0];
        }

        public void fillMemoDetailsOnALSSelection()
        {
            objDS = objMenifestModel.fillMemoDetailsOnALSSelection();
            objIMenifestView.SessionBindMenifestGrid = objDS.Tables[0];
            if (objDS.Tables[1].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[1].Rows[0];
                objIMenifestView.SetLoadedById(objDR["Emp_Name"].ToString(), objDR["Emp_Id"].ToString());
            }
            else
            {
                objIMenifestView.SetLoadedById("", "0");
            }
        }

        public void fillgrid()
        {
            objDS = objMenifestModel.ReadValues();
            objIMenifestView.SessionBindMenifestGrid = objDS.Tables[0];
        }

        public DataSet gc_grid_validation()
        {
            objDS = objMenifestModel.gc_grid_validation();
            return objDS;
        }

        public DateTime Set_SheduledArrival_Date()
        {           
            objDS = objMenifestModel.Set_SheduledArrival_Date();
            return Convert.ToDateTime(objDS.Tables[0].Rows[0]["sheduled_arrival_date"].ToString());
        }

        private void initValues()
        {
           
            objDS = objMenifestModel.ReadValues();            
            objIMenifestView.SessionBindMenifestGrid = objDS.Tables[0];

            if (objIMenifestView.keyID > 0)
            {
                if (objDS.Tables[1].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];
                    objIMenifestView.MenifestTypeID = Util.String2Int(objDR["Memo_Type_Id"].ToString());
                    if (objIMenifestView.MenifestTypeID == 1)
                    {
                        objIMenifestView.MenifestToID= Util.String2Int(objDR["To_Branch_Id"].ToString());
                        //objIMenifestView.SetMenifestToId(objDR["To_Name"].ToString(), objDR["To_Branch_Id"].ToString());
                    }
                    objIMenifestView.MenifestTo = objDR["To_Name"].ToString();
                    objIMenifestView.SetLoadedById(objDR["Emp_Name"].ToString(), objDR["Emp_Id"].ToString());
                                       
                    objIMenifestView.VehicleCotegoryID = Util.String2Int(objDR["Vehicle_Category_ID"].ToString());
                    objIMenifestView.VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());
                    
                    objIMenifestView.MenifestDate = Convert.ToDateTime(objDR["Memo_Date"].ToString());
                    objIMenifestView.ArrivalDeliveryDate = Convert.ToDateTime(objDR["Schedule_Arrival_Delivery_Date"].ToString());
                    objIMenifestView.Total_No_Of_GC = Util.String2Int(objDR["Total_No_Of_GC"].ToString());

                    objIMenifestView.Remarks = objDR["Remarks"].ToString();
                    objIMenifestView.MenifestNo = objDR["Memo_No_For_Print"].ToString();
                    objIMenifestView.ArrivalDeliveryTime = objDR["Schedule_Arrival_Delivery_Time"].ToString();

                    objIMenifestView.Book_ActualWt = Util.String2Decimal(objDR["Booking_Actual_Wt"].ToString());
                    objIMenifestView.Book_ToPayCollection = Util.String2Decimal(objDR["Booking_To_Pay_Collection"].ToString());
                    objIMenifestView.Cros_ActualWt = Util.String2Decimal(objDR["Crossing_Actual_Wt"].ToString());
                    objIMenifestView.Cros_ToPayCollection = Util.String2Decimal(objDR["Crossing_To_Pay_Collection"].ToString());
                    objIMenifestView.Total_ActualWt = Util.String2Decimal(objDR["Total_Actual_Weight"].ToString());
                    objIMenifestView.Total_ToPayCollection = Util.String2Decimal(objDR["Total_To_Pay_Collection"].ToString());
                    objIMenifestView.Total_Loded_Articles = Util.String2Int(objDR["Total_Loaded_Articles"].ToString());
                    objIMenifestView.Total_Loded_Weight = Util.String2Decimal(objDR["Total_Loaded_Weight"].ToString());

                    objIMenifestView.Number_Part4 = objDR["Number_Part4"].ToString();
                    objIMenifestView.ShortUrl = objDR["ShortUrl"].ToString();

                }
                if (CompanyManager.getCompanyParam().IsALSRequired == true)
                {
                    fillALSNO();

                    objDS = objMenifestModel.Fill_ALS_Date_For_Validation();
                    objIMenifestView.ALSDate = Convert.ToDateTime(objDS.Tables[0].Rows[0]["ALS_Date"].ToString());
                }
            }
        }

        public void Save()
        {
            base.DBSave();
        }
    }
}
