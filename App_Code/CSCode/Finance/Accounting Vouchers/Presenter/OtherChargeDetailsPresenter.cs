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
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;

/// <summary>
/// Summary description for OtherChargeDetailsPresenter
/// </summary>
/// 
namespace Raj.EC.FinancePresenter
{
    public class OtherChargeDetailsPresenter:Presenter
    {
        private IOtherChargeDetailsView _OtherChargeDetailsView;
        private OtherChargeDetailsModel _OtherChargeDetailsModel;

        public OtherChargeDetailsPresenter(IOtherChargeDetailsView OtherChargeDetailsView,bool isPostBack)
        {
            _OtherChargeDetailsView = OtherChargeDetailsView;
            _OtherChargeDetailsModel = new OtherChargeDetailsModel(_OtherChargeDetailsView);

            base.Init(_OtherChargeDetailsView, _OtherChargeDetailsModel);

            if (!isPostBack)
            {
                initvalues();
            }
        }

        public void initvalues()
        {
            DataSet ds = new DataSet();

            ds = _OtherChargeDetailsModel.ReadValues();
            //_OtherChargeDetailsView.Session_ddl_Ledger = ds.Tables[1];
            _OtherChargeDetailsView.Bind_OtherDetailsGrid = ds.Tables[0];
            _OtherChargeDetailsView.Session_OtherDetailsGrid = ds.Tables[0];


            if (_OtherChargeDetailsView.keyID > 0)
            {
                decimal Total_Amount = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(dr["Is_Add"]) == true)
                        {
                            Total_Amount = Total_Amount + Convert.ToDecimal(dr["Amount"]);
                        }
                        else
                        {
                            Total_Amount = Total_Amount - Convert.ToDecimal(dr["Amount"]);
                        }

                    }

                }
                             
                _OtherChargeDetailsView.TotalAmount = Total_Amount;
            }


        }
    }
}