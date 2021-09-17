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
using Raj.EC.GeneralView;
using Raj.EC.GeneralModel;

/// <summary>
/// Summary description for LengthChargeHeadPresenter
/// </summary>
namespace Raj.EC.GeneralPresenter
{
    public class LengthChargeHeadPresenter : Presenter
    {
        private ILengthChargeHeadView objILengthChargeHeadView;
        private LengthChargeHeadModel objLengthChargeHeadModel;
        private DataSet objDS;

        public LengthChargeHeadPresenter(ILengthChargeHeadView lengthChargeHeadView, bool IsPostBack)
        {
            objILengthChargeHeadView = lengthChargeHeadView;
            objLengthChargeHeadModel = new LengthChargeHeadModel(objILengthChargeHeadView);

            base.Init(objILengthChargeHeadView, objLengthChargeHeadModel);

            if (!IsPostBack)
            {

                initValues();
            }
        }
        private void initValues()
        {

            if (objILengthChargeHeadView.keyID > 0)
            {
                objDS = objLengthChargeHeadModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objILengthChargeHeadView.LengthCharge = Util.String2Decimal(objDS.Tables[0].Rows[0]["Charge"].ToString());

                    String CommaSepMenuItems = objDS.Tables[0].Rows[0]["Length_ChargeName"].ToString();
                    char[] Sep = { '-' };               
                    string[] Splitted = CommaSepMenuItems.Split(Sep);

                    objILengthChargeHeadView.FromLength = Splitted[0];
                    objILengthChargeHeadView.ToLength = Splitted[1];

                }
                
                  
               
            }
        }

        public void Save()
        {
            base.DBSave();
        }
    }
}
