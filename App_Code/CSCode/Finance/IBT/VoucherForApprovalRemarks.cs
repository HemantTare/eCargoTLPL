using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
/// <summary>
/// Summary description for VoucherForApprovalRemarks
/// </summary>
/// 

public class VoucherForApprovalRemarks
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    public VoucherForApprovalRemarks()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void Get_Remarks_For_Voucher_UnApproval(string Hierarchy_Code, int Main_Id, int Voucher_Id, string Check, string Remarks, string Msg)
    {
        SqlParameter[] sqlparam = {objDAL .MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, Hierarchy_Code), 
                                            objDAL .MakeInParams("@Main_Id", SqlDbType.Int, 0, Main_Id), 
                                             objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int, 0, Voucher_Id), 
                                             objDAL .MakeInParams("@Check", SqlDbType.VarChar, 10, Check), 
                                             objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250, Remarks), 
                                             objDAL.MakeOutParams("@Remarks_For_UnApproval", SqlDbType.VarChar, 250)};

        objDAL.RunProc("FA_Get_UnApproved_Voucher_Remarks", sqlparam);

        Msg = Convert.ToString(sqlparam[5].Value);
    }
}
