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
/// Summary description for StandardBookingRatePresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class StandardBookingRatePresenter : Presenter
    {
        private IStandardBookingRateView objIStandardBookingRateView;
        private StandardBookingRateModel objStandardBookingRateModel;
        private DataSet objDS;

        public StandardBookingRatePresenter(IStandardBookingRateView StandardBookingRateView, bool isPostback)
        {
            objIStandardBookingRateView = StandardBookingRateView;
            objStandardBookingRateModel = new StandardBookingRateModel(objIStandardBookingRateView);
            base.Init(objIStandardBookingRateView, objStandardBookingRateModel);

            if (!isPostback)
            {
                initValues();
            }
        }

        public DataSet Fill_Crossing_Rate(int FromId,int ToId)
        {
            objDS = objStandardBookingRateModel.Fill_Crossing_Rate(FromId, ToId);
            return objDS;
        }
        private void initValues()
        {
            objDS = objStandardBookingRateModel.ReadValues();

            if (objIStandardBookingRateView.keyID > 0)
            {
                if (objDS.Tables[1].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];
                    objIStandardBookingRateView.ApplicableFromDate = Convert.ToDateTime(objDR["Applicable_From"].ToString());
                    objIStandardBookingRateView.ProfitRatio = Util.String2Decimal(objDR["Profit_Ratio_Percent"].ToString());
                    objIStandardBookingRateView.BookingRate = Util.String2Decimal(objDR["Standard_Booking_Rate"].ToString());

                    objIStandardBookingRateView.SetBookingBranchId(objDR["Book_Branch_Name"].ToString(), objDR["Book_Branch_ID"].ToString());
                    objIStandardBookingRateView.SetDeliveryBranchId(objDR["Del_Branch_Name"].ToString(), objDR["Del_Branch_ID"].ToString());
                }
            }

            objIStandardBookingRateView.SessionStandardRateGrid = objDS.Tables[0];
        }

        public void Save()
        {
            base.DBSave();
        }
    }
}

