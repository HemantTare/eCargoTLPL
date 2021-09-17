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
using Raj.EC.ControlsModel;
using Raj.EC.ControlsView;

/// <summary>
/// Summary description for TDSAppPresenter
/// </summary>
/// 

namespace Raj.EC.ControlsPresenter
{
    public class TDSAppPresenter:Presenter
    {
        private ITDSAppView objITDSAppView;
        private TDSAppModel objTDSAppModel;
        private DataSet objDS;


        public TDSAppPresenter(ITDSAppView ITDSAppView,bool isPostBack)
        {
            objITDSAppView = ITDSAppView;
            objTDSAppModel = new TDSAppModel(objITDSAppView);

            base.Init(objITDSAppView, objTDSAppModel);
            if (!isPostBack)
            {
                initvalues();
            }
        }

        public void initvalues()
        {
            objITDSAppView.BindDeducteeType = objTDSAppModel.FillDeducteeType();

            if (objITDSAppView.keyID > 0)
            {
                FillDetails();
            
            }
        
        }

        public void FillDetails()
        {
            String Section_Number;
            DataSet ds = new DataSet();
            ds = objTDSAppModel.ReadValues();

            Section_Number = "0";

            if (ds.Tables[0].Rows.Count > 0)
            {
                objITDSAppView.IsTDSApp = Util.String2Bool(ds.Tables[0].Rows[0]["Is_TDS_Applicable"].ToString());

                if (Util.String2Int(ds.Tables[0].Rows[0]["TDS_Deductee_Type_ID"].ToString()) > 0)
                {
                    objITDSAppView.DeducteeTypeID = Util.String2Int(ds.Tables[0].Rows[0]["TDS_Deductee_Type_ID"].ToString());
                }
                objITDSAppView.IsLower = Util.String2Bool(ds.Tables[0].Rows[0]["Is_Lower_Deduction"].ToString());
                objITDSAppView.IsIgnore = Util.String2Bool(ds.Tables[0].Rows[0]["Ignore_Surcharge_Exemption_Limit"].ToString());

                Section_Number = ds.Tables[0].Rows[0]["Section_Number"].ToString();
                if (Section_Number != "0" && Section_Number !="")
                {
                    objITDSAppView.sectionNo = Section_Number;// ds.Tables[0].Rows[0]["Section_Number"].ToString();
                }
                objITDSAppView.LowerRate = Util.String2Decimal(ds.Tables[0].Rows[0]["TDS_Lower_Rate"].ToString());

            }
            
        
        }

    }
}
