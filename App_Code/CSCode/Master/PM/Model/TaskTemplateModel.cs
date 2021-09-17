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
/// Summary description for TaskTemplateModel
/// </summary>
namespace Raj.EF.MasterModel
{
    public class TaskTemplateModel : IModel
    {
        private ITaskTemplateView objITaskTemplateView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;

      //  private int _userID = 1;

        public TaskTemplateModel(ITaskTemplateView TaskTemplateView)
        {
            objITaskTemplateView = TaskTemplateView;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("[rstil37].[EF_Mst_PM_Task_Template_Fill_Ddl]", ref objDS);
            return objDS;
        }


        public DataSet FillVehicleDDL_For(string Where_Condition)
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Where_Condition", SqlDbType.VarChar, 0, Where_Condition) };

            objDAL.RunProc("rstil37.EF_Mst_PM_Task_Template_Fill_VehicleManufacturerModel", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet FillDDL_For(string Where_Condition)
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Where_Condition", SqlDbType.VarChar , 0, Where_Condition)};

            objDAL.RunProc("rstil37.EF_Mst_PM_Task_Template_Fill_Service", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet BindGrid()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Task_ID", SqlDbType.Int, 0, objITaskTemplateView.keyID) };
            objDAL.RunProc("rstil37.EF_Mst_PM_Task_Template_Custom_FillGrid", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Template_Task_Id", SqlDbType.Int, 0, objITaskTemplateView.keyID),
                                           objDAL.MakeInParams("@Task_Id", SqlDbType.Int, 0, objITaskTemplateView.keyID), 
                                           objDAL.MakeInParams("@Form_Type", SqlDbType.VarChar, 0, objITaskTemplateView.Form_Type  ),
                                           };
            objDAL.RunProc("rstil37.EF_Mst_PM_Task_Template_Read", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                   objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                   objDAL.MakeInParams("@Template_Task_Id", SqlDbType.Int, 0, objITaskTemplateView.keyID),
                                   objDAL.MakeInParams("@Template_Name", SqlDbType.VarChar, 0,objITaskTemplateView.TaskTemplateName),                
                                   objDAL.MakeInParams("@Template_Id", SqlDbType.Int, 0,objITaskTemplateView.Template_ID ),
                                   objDAL.MakeInParams("@Schedule_By", SqlDbType.Int, 0,objITaskTemplateView.Schedule_By  ),
                                   objDAL.MakeInParams("@Is_Custom", SqlDbType.Bit , 0,objITaskTemplateView.Is_Custom),
                                   objDAL.MakeInParams("@No_Of_Occurances", SqlDbType.Int, 0,0),
                                   objDAL.MakeInParams("@Reference_Value", SqlDbType.Int, 0,0),                                    
                                   objDAL.MakeInParams("@Reference_Date", SqlDbType.DateTime , 0,DateTime.Now ),
                                   objDAL.MakeInParams("@Due_On_Value", SqlDbType.Int , 0,objITaskTemplateView.DueOn_Value ),
                                   objDAL.MakeInParams("@Due_On_Days", SqlDbType.Int , 0,objITaskTemplateView.DueOn_Days  ),
                                   objDAL.MakeInParams("@Is_Days_Selected", SqlDbType.Bit , 0,objITaskTemplateView.Is_Days_Selected  ),
                                   objDAL.MakeInParams("@Month_Value", SqlDbType.Int, 0,objITaskTemplateView.Month_Value),
                                   objDAL.MakeInParams("@Alert_Before_Value", SqlDbType.Int, 0,objITaskTemplateView.Alert_Before_Value ),
                                   objDAL.MakeInParams("@Alert_Before_Days", SqlDbType.Int, 0,objITaskTemplateView.Alert_Before_Days ),
                                   objDAL.MakeInParams("@Service_Category_ID", SqlDbType.Int, 0,objITaskTemplateView.Repair_Service_Category_ID  ),
                                   objDAL.MakeInParams("@Service_ID", SqlDbType.Int, 0,objITaskTemplateView.Repair_Service_ID  ),
                                   objDAL.MakeInParams("@Task_Defination_ID", SqlDbType.Int, 0,1),
                                   objDAL.MakeInParams("@Task_Completion_ID", SqlDbType.Int, 0,objITaskTemplateView.Task_Completion_ID ),
                                   objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0,objITaskTemplateView.Branch_Id ),
                                   objDAL.MakeInParams("@Vendor_ID", SqlDbType.Int, 0,objITaskTemplateView.Vendor_Id ),
                                   objDAL.MakeInParams("@Cost", SqlDbType.Decimal, 0,objITaskTemplateView.Cost ),
                                   objDAL.MakeInParams("@Task_Description", SqlDbType.VarChar, 0,objITaskTemplateView.Description), 
                                   objDAL.MakeInParams("@CustomAlertOnDetailsXML", SqlDbType.Xml, 0, objITaskTemplateView.CustomAlertOnDetailsXML),
                                   objDAL.MakeInParams("@CustomAlert_On_Value", SqlDbType.Int, 0, objITaskTemplateView.CustomAlert_On_Value),                
                                    objDAL.MakeInParams("@Last_Permormed_On", SqlDbType.Int, 0, objITaskTemplateView.Last_Permormed_On ),
                                    objDAL.MakeInParams("@Last_Permormed_Date", SqlDbType.DateTime, 0, objITaskTemplateView.Last_Permormed_Date ),
                                    objDAL.MakeInParams("@Form_Type", SqlDbType.VarChar, 0, objITaskTemplateView.Form_Type),
                                    objDAL.MakeInParams("@Task_Id", SqlDbType.Int, 0, objITaskTemplateView.keyID),
                                    objDAL.MakeInParams("@Task_On_Id", SqlDbType.Int, 0, objITaskTemplateView.Vehicle_Id),
                                    objDAL.MakeInParams("@Manufacturer_ID", SqlDbType.Int, 0, objITaskTemplateView.Vehicle_Manufacturer_ID),
                                    objDAL.MakeInParams("@Vehicle_Model_ID", SqlDbType.Int, 0, objITaskTemplateView.Vehicle_Model_ID),
                                    objDAL.MakeInParams("@CompanyWorkShop", SqlDbType.Int, 0, objITaskTemplateView.CompanyWorkShop),
                                   objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID)};
            
            // 

            objDAL.RunProc("rstil37.EF_Mst_PM_Task_Template_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {

                string _Msg;
                _Msg = "Saved SuccessFully";

                if (objITaskTemplateView.Form_Type == "VehiclePM")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&RefreshParentPage=1");
                }
                else if (objITaskTemplateView.Form_Type == "Template")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
            }

            return objMessage;
        }
    }
}
