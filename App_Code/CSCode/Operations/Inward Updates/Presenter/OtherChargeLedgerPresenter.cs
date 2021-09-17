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
/// Summary description for OtherChargeLedgerPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{
    public class OtherChargeLedgerPresenter:Presenter
    {
        private IOtherChargeLedgerView _OtherChargeLedgerView;
        private OtherChargeLedgerModel _OtherChargeLedgerModel;

        public OtherChargeLedgerPresenter(IOtherChargeLedgerView OtherChargeLedgerView, bool isPostBack)
        {
            _OtherChargeLedgerView = OtherChargeLedgerView;
            _OtherChargeLedgerModel = new OtherChargeLedgerModel(_OtherChargeLedgerView);

            base.Init(_OtherChargeLedgerView, _OtherChargeLedgerModel);

            if (!isPostBack)
            {
                initvalues();
            }
        }
        public void initvalues()
        {
            DataSet ds = new DataSet();

            ds = _OtherChargeLedgerModel.ReadValues();
            _OtherChargeLedgerView.Bind_OtherDetailsGrid = ds.Tables[0];
            _OtherChargeLedgerView.Session_OtherDetailsGrid = ds.Tables[0];


            //if (_OtherChargeLedgerView.keyID > 0)
            //{
            //    decimal Total_Amount = 0;
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        foreach (DataRow dr in ds.Tables[0].Rows)
            //        {
            //            if (Convert.ToBoolean(dr["Is_Add"]) == true)
            //            {
            //                Total_Amount = Total_Amount + Convert.ToDecimal(dr["Amount"]);
            //            }
            //            else
            //            {
            //                Total_Amount = Total_Amount - Convert.ToDecimal(dr["Amount"]);
            //            }

            //        }

            //    }

            //    _OtherChargeLedgerView.TotalAmount = Total_Amount;
            //}


        }
    }
}