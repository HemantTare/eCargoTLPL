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
using Raj.EC.MasterView;
using Raj.EC.MasterModel;
using ClassLibraryMVP;
/// <summary>
/// Summary description for SizePresenter
/// </summary>
/// 

namespace Raj.EC.MasterPresenter
{
    public class SizePresenter : Presenter
    {
        private ISizeView objISizeView;
        private SizeModel objSizeModel;
        private DataSet objDS;

        public SizePresenter(ISizeView SizeView, bool IsPostBack)
        {
            objISizeView = SizeView;
            objSizeModel = new SizeModel(objISizeView);

            base.Init(objISizeView, objSizeModel);

            if (!IsPostBack)
            {
                Fill_Values();
                initValues();
            }
        }

        public void Fill_Values()
        {
            objDS = objSizeModel.Fill_Values();

            objISizeView.BindFunction = objDS.Tables[0];
        }

        private void initValues()
        {
            if (objISizeView.keyID > 0)
            {
                Read_Values();
            }
        }

        public void Read_Values()
        {
            objDS = objSizeModel.ReadValues();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow dr = objDS.Tables[0].Rows[0];

                objISizeView.SizeName = dr["SizeName"].ToString();
                objISizeView.ApproxChargeWieght = Util.String2Decimal(dr["ApproxChargeWeight"].ToString());
                objISizeView.Function = Util.String2Int(dr["FunctionId"].ToString());
                objISizeView.FactorAmount = Util.String2Decimal(dr["FactorAmount"].ToString());
                objISizeView.MinChrgQty = Util.String2Decimal(dr["MinChrgQty"].ToString());  
                objISizeView.Description = dr["Description"].ToString();
                objISizeView.IsDefault = false;
                if (dr["IsDefault"].ToString().ToLower() == "true")
                {
                    objISizeView.IsDefault = true;
                }
            }
        }

        public void save()
        {
            base.DBSave();
        }

    }
}