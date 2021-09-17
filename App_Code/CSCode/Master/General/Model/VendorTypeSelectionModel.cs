using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EF.MasterView;


/// <summary>
/// Summary description for VendorTypeSelectionModel
/// </summary>
namespace Raj.EF.MasterModel
{
    class VendorTypeSelectionModel : IModel
    {
        private IVendorTypeSelectionView objIVendorTypeSelectionView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;        
        private int _userID = UserManager.getUserParam().UserId;
        private int _yearCode = UserManager.getUserParam().YearCode;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _mainId = UserManager.getUserParam().MainId;
        private int _systemId = UserManager.getUserParam().SystemId;


        public VendorTypeSelectionModel(IVendorTypeSelectionView vendorTypeSelectionView)
        {
            objIVendorTypeSelectionView = vendorTypeSelectionView;
        }
        public DataSet GetKeyName()
        {

            objDAL.RunProc("rstil43.EF_Mst_General_VendorTypeSelection_FillKeyName", ref objDS);
            return objDS;
        }

        public DataSet GetVendorTypeOnKeyNameChanged()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@keyID", SqlDbType.Int, 0, objIVendorTypeSelectionView.KeyNameId)
                                          
                                        };
            objDAL.RunProc("rstil43.EF_Mst_General_VendorTypeselection_FillOnKeyNameChanged", objSqlParam, ref objDS);
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),                                               
                                               objDAL.MakeInParams("@keyID",SqlDbType.Int,0,objIVendorTypeSelectionView.KeyNameId),                                              
                                               objDAL.MakeInParams("@ChkListVendorTypeDetails", SqlDbType.Xml,0,objIVendorTypeSelectionView.ChkListVendorTypeDetails)                                             
                                              
                                         };


             objDAL.RunProc("rstil43.EF_Mst_General_VendorTypeSelection_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
        public DataSet ReadValues()
        {
            return objDS;
        }
	}
}
