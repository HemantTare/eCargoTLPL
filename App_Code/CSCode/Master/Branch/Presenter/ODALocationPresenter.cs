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
/// Summary description for ODALocationPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class ODALocationPresenter : Presenter
    {
        private IODALocationView objIODALocationView;
        private ODALocationModel objODALocationModel;
        private DataSet objDS;

        public ODALocationPresenter(IODALocationView odaLocationView, bool IsPostBack)
        {
            objIODALocationView = odaLocationView;
            objODALocationModel = new ODALocationModel(objIODALocationView);

            base.Init(objIODALocationView, objODALocationModel);

            if (!IsPostBack)
            {

                initValues();
            }
        }
        public void FillControllingBranch()
        {
            DataSet objds = objODALocationModel.GetBranchValues();

            objIODALocationView.BindControllingBranch = objds.Tables[0];
            objIODALocationView.BindDeliveryType = objds.Tables[1];
        }
        private void initValues()
        {
            FillControllingBranch();

            if (objIODALocationView.keyID > 0)
            {
                objDS = objODALocationModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIODALocationView.LocationName = DR["Service_Location_Name"].ToString();
                    objIODALocationView.DeliveryTypeID = Util.String2Int(DR["Delivery_Type_ID"].ToString());
                    objIODALocationView.BranchId=Util.String2Int(DR["Branch_ID"].ToString());
                    objIODALocationView.DistanceFromBranch = Util.String2Int(DR["Distance_From_Branch"].ToString());
                    objIODALocationView.PrimaryPinCode=DR["Pin_Code_Primary"].ToString();
                    objIODALocationView.SecondryPinCode=DR["Pin_Code_Secondary"].ToString();
                    objIODALocationView.IsBooking=Util.String2Bool(DR["Is_To_Pay_Booking"].ToString());
                    objIODALocationView.IsODALocation = Util.String2Bool(DR["Is_ODA"].ToString());
                    objIODALocationView.IsOctroiApplicable = Util.String2Bool(DR["Is_Octroi"].ToString());
                    objIODALocationView.ODAChargeUpto = Util.String2Decimal(DR["ODA_Charges_UpTo_500_kg"].ToString());
                    objIODALocationView.ODAChargeAbove = Util.String2Decimal(DR["ODA_Charges_Above_500_kg"].ToString());
                    objIODALocationView.SetCityId(DR["City_Name"].ToString(), DR["City_Id"].ToString());

                }
            }
        }

        public void Save()
        {
           // base.DBSave();
            objODALocationModel.Save();
        }
	}
}
