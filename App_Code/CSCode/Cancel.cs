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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP;


namespace Raj.EC
{
    public class Cancel
    {
        private DAL _objDal = new DAL();
        private DataSet _objDS = null;



        public Cancel()
        {
            
        }

        public void EC_Opr_Cancel(int Document_ID, int Menu_Item_Id,string Reason, int Cancelled_By, ref int Error_Id, ref string Error_Name)
        {
            SqlParameter[] sqlpara =
            {
                _objDal.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
                _objDal.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                _objDal.MakeInParams("@Cancelled_Document_Id",SqlDbType.Int,0,Document_ID),
                _objDal.MakeInParams("@Menu_Item_Id",SqlDbType.Int,0,Menu_Item_Id),
                _objDal.MakeInParams("@Hierarchy_code",SqlDbType.VarChar,5,UserManager.getUserParam().HierarchyCode),
                _objDal.MakeInParams("@Main_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId),
                _objDal.MakeInParams("@Reason",SqlDbType.VarChar,250,Reason),
                _objDal.MakeInParams("@Cancelled_By",SqlDbType.Int,0,Cancelled_By),
                _objDal.MakeInParams("@Cancellation_Date",SqlDbType.DateTime,0,DateTime.Now)
                
            };
            _objDal.RunProc("EC_Opr_Cancellation", sqlpara);

            Error_Id = Convert.ToInt32(sqlpara[0].Value);
            Error_Name = Convert.ToString(sqlpara[1].Value);

            if (Error_Id == 1012)
            {
                Common.DisplayErrors(Error_Id);
            }

            if (Error_Id == 1010)
            {
                Common.DisplayErrors(Error_Id);
            }


        }
        public void EC_Opr_Cancel_Finance(int Voucher_ID, int Menu_Item_Id, string Reason, int Cancelled_By, ref int Error_Id, ref string Error_Name)
        {
            SqlParameter[] sqlpara =
            {
                _objDal.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
                _objDal.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                _objDal.MakeInParams("@Cancelled_Document_Id",SqlDbType.Int,0,Voucher_ID),
                _objDal.MakeInParams("@Menu_Item_Id",SqlDbType.Int,0,Menu_Item_Id),
                _objDal.MakeInParams("@Hierarchy_code",SqlDbType.VarChar,5,UserManager.getUserParam().HierarchyCode),
                _objDal.MakeInParams("@Main_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId),
                _objDal.MakeInParams("@Reason",SqlDbType.VarChar,250,Reason),
                _objDal.MakeInParams("@Cancelled_By",SqlDbType.Int,0,Cancelled_By),
                _objDal.MakeInParams("@Cancellation_Date",SqlDbType.DateTime,0,DateTime.Now)
                         
            };
            _objDal.RunProc("EC_Opr_Cancellation_Finance", sqlpara);

            Error_Id = Convert.ToInt32(sqlpara[0].Value);
            Error_Name = Convert.ToString(sqlpara[1].Value);

            if (Error_Id == 1012)
            {
                Common.DisplayErrors(Error_Id);
            }
            

        }
    }
}