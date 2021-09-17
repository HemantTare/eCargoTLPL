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
/// Summary description for BranchGeneralPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class BranchGeneralPresenter : Presenter
    {
        private IBranchGeneralView objIBranchGeneralView;
        private BranchGeneralModel objBranchGeneralModel;
        private DataSet objDS;

        public BranchGeneralPresenter(IBranchGeneralView BranchGeneralView, bool isPostback)
        {
            objIBranchGeneralView = BranchGeneralView;
            objBranchGeneralModel = new BranchGeneralModel(objIBranchGeneralView);
            base.Init(objIBranchGeneralView, objBranchGeneralModel);

            if (!isPostback)
            {
                
                initValues();
            }
        }

        private void fillValues()
        {
            objDS = objBranchGeneralModel.FillValues();
            objIBranchGeneralView.BindBranchType = objDS.Tables[0];
            objIBranchGeneralView.BindDeliveryType = objDS.Tables[1];
        }

        public void fillDivisionOnAreaSelection()
        {
            objDS = objBranchGeneralModel.FillDivisionOnAreaSelection();
            objIBranchGeneralView.BindDivision = objDS.Tables[0];
        }

        private void initValues()
        {
            fillValues();
            
            
            
            if (objIBranchGeneralView.keyID > 0)
            {
                objDS = objBranchGeneralModel.ReadValues();

                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objIBranchGeneralView.BranchCode = objDR["Branch_Code"].ToString();
                    objIBranchGeneralView.BranchName = objDR["Branch_Name"].ToString();
                    objIBranchGeneralView.BranchTypeID = Util.String2Int(objDR["Branch_Type_ID"].ToString());
                    objIBranchGeneralView.ContactPersonName = objDR["Contact_Person"].ToString();
                    objIBranchGeneralView.STRegistrationNo = objDR["Service_Tax_Reg_No"].ToString();
                    objIBranchGeneralView.AddressView.AddressLine1 = objDR["Address_1"].ToString();
                    objIBranchGeneralView.AddressView.AddressLine2 = objDR["Address_2"].ToString();
                    objIBranchGeneralView.AddressView.CityId = Util.String2Int(objDR["City_ID"].ToString());
                    //objIBranchGeneralView.SetCityID = Util.String2Int(objDR["City_ID"].ToString());
                    //objIBranchGeneralView.RegionId = Util.String2Int(objDR["Region_Id"].ToString());
                    objIBranchGeneralView.AddressView.PinCode = objDR["Pin_Code"].ToString();
                    objIBranchGeneralView.AddressView.StdCode = objDR["Std_Code"].ToString();
                    objIBranchGeneralView.AddressView.Phone1 = objDR["Phone_1"].ToString();
                    objIBranchGeneralView.AddressView.Phone2 = objDR["Phone_2"].ToString();
                    objIBranchGeneralView.AddressView.FaxNo = objDR["Fax"].ToString();
                    objIBranchGeneralView.AddressView.EmailId = objDR["EMail_Id"].ToString();
                    objIBranchGeneralView.SetMemoDestinationID(objDR["Memo_Destination_Branch"].ToString(), objDR["Memo_Destination"].ToString());
                    objIBranchGeneralView.SetAgencyAcountID(objDR["Agency_Ledger_Name"].ToString(), objDR["Agency_Ledger_ID"].ToString());
                    objIBranchGeneralView.SetAreaID(objDR["Area_Name"].ToString(), objDR["Area_Id"].ToString());
                    objIBranchGeneralView.SetDefaultHubID(objDR["Default_Hub_Name"].ToString(), objDR["Default_Hub"].ToString());
                    objIBranchGeneralView.SetDeliveryAtID(objDR["Delivery_At_Name"].ToString(), objDR["Delivery_At"].ToString());
                    objIBranchGeneralView.DeliveryTypeID = Util.String2Int(objDR["Delivery_Type_ID"].ToString());
                    objIBranchGeneralView.SetRegionId(objDR["Region_Name"].ToString(), objDR["Region_Id"].ToString());

                    objIBranchGeneralView.StartedOn = Convert.ToDateTime(objDR["Started_On"].ToString());
                    objIBranchGeneralView.Branch_ID = objIBranchGeneralView.keyID;
                    fillDivisionOnAreaSelection();
                }
            }
            else
            {
                fillDivisionOnAreaSelection();
            }
        }
        
    }
}
