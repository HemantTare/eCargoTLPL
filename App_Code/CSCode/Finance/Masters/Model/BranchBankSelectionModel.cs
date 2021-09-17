using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess ;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;




namespace Raj.EC.FinanceModel
{
    /// <summary>
    /// Author        : Ankit Champaneriya 
    /// Created On    : 16/10/2008
    /// Description   : This Page is For  branch bank selection 
    /// Summary description for BranchBankSelectionModel
    /// </summary>
    /// 

    public class BranchBankSelectionModel : ClassLibraryMVP.General.IModel    
    {
        private IBranchBankSelectionView objIBranchBankSelectionView;
        private DataSet _ds;
        private DAL _objDAL = new DAL();
        private int _userId = UserManager.getUserParam().UserId;  
               
        public BranchBankSelectionModel(IBranchBankSelectionView branchBankSelectionView)
        {
            objIBranchBankSelectionView = branchBankSelectionView;
        }

        public DataSet ReadValues()
        {
            return _ds;
        }

        public Message Save()
        {
            Message objMsg = new Message();
            bool _is_HO;

            //if (objIBranchBankSelectionView.Is_HO == 2)
            //{ _is_HO = true; }
            //else
            //{ _is_HO = false; }
         
         
            SqlParameter[] sqlParam = { _objDAL.MakeOutParams("@Error_Code",SqlDbType.Int,0),
                                        _objDAL.MakeOutParams("@Error_Desc",SqlDbType.VarChar,4000),                                                                                
                                        _objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,objIBranchBankSelectionView.BranchID),
                                        //_objDAL.MakeInParams("@Is_HO",SqlDbType.Bit,0,_is_HO),
                                        _objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,2,objIBranchBankSelectionView.SelectedHerch),
                                        _objDAL.MakeInParams("@User_ID",SqlDbType.Int,0,_userId),  
                                        _objDAL.MakeInParams("@ChkLedgersDetails", SqlDbType.Xml,0,objIBranchBankSelectionView.SessionChkLedgers.GetXml().ToUpper())                                             
                                              
                                       };

            _objDAL.RunProc("dbo.EC_FA_Branch_Bank_Selection_Save", sqlParam);

            objMsg.messageID = Util.String2Int(sqlParam[0].Value.ToString());
            objMsg.message = sqlParam[1].Value.ToString();


            if (objMsg.messageID == 0)
            {
                objIBranchBankSelectionView.ClearVariables();
            }
            return objMsg;

        }
        public DataSet FillValues()
        {
            SqlParameter[] sqlParam = { _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, UserManager.getUserParam().HierarchyCode), _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, UserManager.getUserParam().MainId) };
            _objDAL.RunProc("dbo.EC_FA_Branch_Bank_Selection_FillValues",sqlParam, ref _ds);
            return _ds;
        }
        public DataSet GetLedgersOnBranchNameChanged()
        { 
            SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, objIBranchBankSelectionView.BranchID),
                                           _objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, UserManager.getUserParam().DivisionId),
                                           _objDAL.MakeInParams("@Selected_Hierarchy_Code",SqlDbType.VarChar,2,objIBranchBankSelectionView.SelectedHerch),
                                         };
            _objDAL.RunProc("dbo.EC_FA_Branch_Bank_Selection_FillLedgerOnBranchChanged", objSqlParam, ref _ds);
            return _ds;
        }
    }
}