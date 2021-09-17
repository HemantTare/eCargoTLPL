using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Raj.EC.MasterView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for TransitScheduleModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{

    public class TransitScheduleModel:IModel 
    {
        private ITransitScheduleView objITransitScheduleView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        private int _divisionID = UserManager.getUserParam().DivisionId;


        public TransitScheduleModel(ITransitScheduleView transitScheduleView)
        {
            objITransitScheduleView = transitScheduleView;
        }
        public DataSet ReadValues()
        {
            //SqlParameter[] objSqlParam = { objDAL.MakeInParams("@TemplateId", SqlDbType.Int, 0, objITransitScheduleView.keyID)
            //                               };
            //objDAL.RunProc("EF_Master_PM_Template_Read", objSqlParam, ref objDS);
            return objDS;

        }
        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Mst_TransitSchedule_FillValues", ref objDS);
            SetTableName(new string[] { "StateMaster",                                                        
                                        "VehicleTypeMaster"                                        
                                         }
                                       );
            return objDS;

        }
        
        private void SetTableName(string[] nameList)
        {
            for (int i = 0; i < nameList.Length; i++)
            {
                objDS.Tables[i].TableName = nameList[i];
            }

        }
        private DataSet Get_Name(int State_Id)
        {
            DataSet ds = new DataSet();
            SqlParameter[] SqlPara = { objDAL.MakeInParams("@State_Id", SqlDbType.Int, 0, State_Id) };

            objDAL.RunProc("EC_Mst_TransitSchedule_GetCityStateWise", SqlPara,ref ds);
            return ds;
        }
        private DataSet Transit_Schedule(int From_State_Id, int To_State_Id, int VehicleType_Id)
        {
            DataSet ds = new DataSet();
            SqlParameter[] SqlPara = { objDAL.MakeInParams("@From_State_Id", SqlDbType.Int, 0, From_State_Id), 
                                       objDAL.MakeInParams("@To_State_Id", SqlDbType.Int, 0, To_State_Id),
                                       objDAL.MakeInParams("@Vehicle_Type_Id", SqlDbType.Int, 0, VehicleType_Id) 
                                     };

            objDAL.RunProc("EC_Mst_TransitSchedule_FillGrid", SqlPara, ref ds);
             
            return ds;
        } 
        public DataSet Get_Transit_Schedule(int FromState_Id, int ToState_Id,int VehicleType_Id)
        {

            DataSet Final_Ds = new DataSet();
            int i = 0;
            int j = 0;

            DataSet ds = default(DataSet);
            ds = Get_Name(ToState_Id);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Transit Schedule"));
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                dt.Columns.Add(new DataColumn(ds.Tables[0].Rows[i][0].ToString()));
            }

            ds = Get_Name(FromState_Id);
            DataRow dr = default(DataRow);
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                dr = dt.NewRow();
                dr[0] = ds.Tables[0].Rows[i][0];
                dt.Rows.Add(dr);
            }

            ds = Transit_Schedule(FromState_Id, ToState_Id, VehicleType_Id);

            Hashtable Ht = new Hashtable();
            Ht = Hash_Table(ref ds);

            string row = null;
            string col = null;

            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {
                row = dt.Rows[i][0].ToString();

                for (j = 1; j <= dt.Columns.Count - 1; j++)
                {
                    col = dt.Columns[j].ColumnName;
                    if (Ht[row + "~" + col] == null)
                    {
                        dt.Rows[i][j] = "NA";
                    }
                    else
                    {
                        dt.Rows[i][j] = Ht[row + "~" + col];
                    }
                }
            }

            Final_Ds.Tables.Add(dt);
            return Final_Ds;
        }
        private Hashtable Hash_Table(ref DataSet ds)
        {

            string from_City_Name = null;
            string to_City_Name = null;
            string HashTable_KeyValue = null;

            int Transit_Days = 0;

            int i = 0;

            Hashtable HT = new Hashtable();

            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                from_City_Name = ds.Tables[0].Rows[i]["From_City"].ToString();
                to_City_Name = ds.Tables[0].Rows[i]["To_City"].ToString();

                Transit_Days = Convert.ToInt32(ds.Tables[0].Rows[i]["Transit_Days"]);

                HashTable_KeyValue = from_City_Name + "~" + to_City_Name;

                HT.Add(HashTable_KeyValue, Transit_Days);
            }

            return HT;
        } 

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                   objDAL.MakeOutParams("@ERROR_DESC", SqlDbType.VarChar, 4000),
                                   objDAL.MakeInParams("@Transit_Days_ID", SqlDbType.Int, 0, objITransitScheduleView.FromStateID),
                                   objDAL.MakeInParams("@From_City_ID", SqlDbType.Int, 0,objITransitScheduleView.ToStateID),                                   
                                   objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID)};


            objDAL.RunProc("EC_Mst_TransitDays_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
    }
}