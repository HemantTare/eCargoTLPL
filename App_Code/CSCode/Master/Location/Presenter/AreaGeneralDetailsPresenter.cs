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
/// Summary description for AreaGeneralDetailsPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class AreaGeneralDetailsPresenter : Presenter
    {
        private IAreaGeneralDetailsView objIAreaGeneralDetailsView;
        private AreaGeneralDetailsModel objAreaGeneralDetailsModel;
        private DataSet objDS;

        public AreaGeneralDetailsPresenter(IAreaGeneralDetailsView areaGeneralDetailsView, bool IsPostBack)
        {
            objIAreaGeneralDetailsView = areaGeneralDetailsView;
            objAreaGeneralDetailsModel = new AreaGeneralDetailsModel(objIAreaGeneralDetailsView);

            base.Init(objIAreaGeneralDetailsView, objAreaGeneralDetailsModel);

            if (!IsPostBack)
            {
                FillDivision();
                initValues();
            }
        }

        public void FillDivision()
        {
            objIAreaGeneralDetailsView.BindChkListDivision = objAreaGeneralDetailsModel.ReadValues();
        }
        public void Save()
        {
            //base.DBSave();
            objAreaGeneralDetailsModel.Save();
        }

       
        private void initValues()
        {


            if (objIAreaGeneralDetailsView.keyID >0)
            {
                objDS = objAreaGeneralDetailsModel.ReadGeneralValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];

                    objIAreaGeneralDetailsView.AreaCode = DR["Area_Code"].ToString();
                    objIAreaGeneralDetailsView.AreaName = DR["Area_Name"].ToString();
                    objIAreaGeneralDetailsView.ContactPerson = DR["Contact_Person"].ToString();
                    objIAreaGeneralDetailsView.AddressView.AddressLine1 = DR["Adress_1"].ToString();
                    objIAreaGeneralDetailsView.AddressView.AddressLine2=DR["Adress_2"].ToString();
                    objIAreaGeneralDetailsView.AddressView.PinCode=DR["Pin_Code"].ToString();
                    objIAreaGeneralDetailsView.AddressView.CityId= Util.String2Int(DR["City_Id"].ToString());
                    objIAreaGeneralDetailsView.AddressView.Phone1=DR["Phone_1"].ToString();
                    objIAreaGeneralDetailsView.AddressView.Phone2=DR["Phone_2"].ToString();
                    objIAreaGeneralDetailsView.AddressView.StdCode=DR["Std_Code"].ToString();
                    objIAreaGeneralDetailsView.AddressView.FaxNo=DR["Fax"].ToString();
                    objIAreaGeneralDetailsView.AddressView.EmailId=DR["Email_Id"].ToString();
                    objIAreaGeneralDetailsView.StartedOn = Convert.ToDateTime(DR["Started_On"].ToString());



                }
                //objDS = objAreaGeneralDetailsModel.ReadValues();
                //if (objDS.Tables[0].Rows.Count > 0)
                //{
                //    objIAreaGeneralDetailsView.BindChkListDivision = objDS;
                //}

            }



        }

    }
}
