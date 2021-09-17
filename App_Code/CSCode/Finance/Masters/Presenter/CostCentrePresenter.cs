using System;
using System.Data;

using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;

/// Author        : Ankit Champaneriya 
/// Created On    : 15/10/2008
/// Description   : This Page is For  Cost centre presenter details
/// <summary>
/// Summary description for CostCentrePresenter
/// </summary>
/// 
namespace Raj.EC.FinancePresenter
{
    public class CostCentrePresenter : Presenter
    {
        private ICostCentreView _iCostCentreView;
        private CostCentreModel _CostCentreModel;
       // private DataSet objDS;

        public CostCentrePresenter(ICostCentreView iCostCentreView, bool isPostBack)
        {
            _iCostCentreView = iCostCentreView;
            _CostCentreModel = new CostCentreModel(_iCostCentreView);
            base.Init(_iCostCentreView, _CostCentreModel);

            if (!isPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            _iCostCentreView.Bind_DDL_Under = _CostCentreModel.GetUnder();
            _iCostCentreView.Bind_CheckBoxList_Ledger = _CostCentreModel.GetLedgers();

            if (_iCostCentreView.keyID > 0)
            {
              //  readValues();
            }
        }

        private void readValues()
        {
            //objDS = _CostCentreModel.ReadValues();

            //int i = objDS.Tables[0].Rows.Count ;
            ////DataRow Dr = objDS.Tables[0].Rows[0];

            //_iCostCentreView.Cost_Centre_Name = Dr["Cost_Centre_Name"].ToString();
            //_iCostCentreView.Cost_Centre_ID = Dr["Parent_Cost_Centre_ID"].ToString();

            //for (i = 0; i <= objDS.Tables[0].Rows.Count - 1; i++)
            //{
                
            //}
            
            //DataTable dt = new DataTable("tbl_Cost_Centre");
            //dt.Columns.Add("Ledger_Id");
            //DataSet ds = new DataSet();
            //ds.Tables.Add(dt);
            //DataRow dr;
            //int i;
            //for (i = 0; i <= ChkBoxLst_Ledgers.Items.Count - 1; i++)
            //{
            //    if (ChkBoxLst_Ledgers.Items[i].Selected == true)
            //    {
            //        dr = ds.Tables["tbl_Cost_Centre"].NewRow();
            //        dr["Ledger_Id"] = ChkBoxLst_Ledgers.Items[i].Value;
            //        ds.Tables["tbl_Cost_Centre"].Rows.Add(dr);
            //    }
            //}
            //objILedgerGroupView.UnderId = Util.String2Int(Dr["Parent_Ledger_Group_Id"].ToString());
            //objILedgerGroupView.IsAffectGrossProfit = Util.String2Bool(Dr["Affect_Gross_Profit"].ToString());
        }

        public void Save()
        {
            base.DBSave();
            //objLedgerGroupModel.Save();
        }

    }
}