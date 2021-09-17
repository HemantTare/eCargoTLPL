using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;
using Raj.EC;

/// <summary>
/// Author : Ankit champaneriya
/// Date   : 07-1-09
/// Summary description for Stock transferPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{
    public class StockTransferPresenter : Presenter
    {
        private IStockTransferView objIStockTransferView;
        private StockTransferModel objStockTransferModel;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        public StockTransferPresenter(IStockTransferView StockTransferView, Boolean isPostBack)
        {
            objIStockTransferView = StockTransferView;
            objStockTransferModel = new StockTransferModel(objIStockTransferView);

            base.Init(objIStockTransferView, objStockTransferModel);
            if (!isPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            Fill_Values();

            if (objIStockTransferView.keyID > 0)
            {
                ReadValues();
            }
        }

        public void Fill_Values()
        {
            objDS = objStockTransferModel.Fill_Values();
            objIStockTransferView.BindDDLStockTransfer = objDS.Tables[0];
        }

        public void fillgrid()
        {
            objDS = objStockTransferModel.ReadValues();
            objIStockTransferView.SessionDGStockTransfer = objDS.Tables[0];
        }

        public void Save()
        {
            base.DBSave();
        }
    }
}