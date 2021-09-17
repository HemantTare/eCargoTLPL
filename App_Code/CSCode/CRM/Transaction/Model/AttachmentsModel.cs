using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.CRM.TransactionsView;
//using Raj.eCargo.Init;
using ClassLibraryMVP;
using System.IO;
namespace Raj.CRM.TransactionsModel
{
    public class AttachmentsModel
    {

        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = (int)UserManager.getUserParam().UserId;
        private int _yearCode = (int)UserManager.getUserParam().YearCode;
        private string _hierarchyCode = (string)UserManager.getUserParam().HierarchyCode;
        private int _mainId = (int)UserManager.getUserParam().MainId;
        //private bool _isVT = Convert.ToBoolean(UserManager.getUserParam().Is_VT);

        public AttachmentsModel()
        {

        }

        public DataSet ReadValues(int Ticket_Id,int User_Id)
        {
            SqlParameter[] objSqlParam = {
                                           objDAL.MakeInParams("@Ticket_Id", SqlDbType.Int, 0,Ticket_Id),
                                           objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0,User_Id) 
                                         };
            objDAL.RunProc("[EC_CRM_Trn_AttachmentReadValues]", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save(int Ticket_Id,int User_Id,Boolean IsQuery)   //parameter isquery added by Ankit
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam =   { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),

                                               objDAL.MakeInParams("@Is_Query", SqlDbType.Bit , 0, IsQuery),
                                               objDAL.MakeInParams("@User_ID", SqlDbType.Int, 0, User_Id),
                                               objDAL.MakeInParams("@Ticket_Id", SqlDbType.Int, 0, Ticket_Id),  
                                               objDAL.MakeInParams("@Attachment_XML", SqlDbType.Xml,0,StateManager.IsValidSession("Attachments")==true?StateManager.GetState<DataSet>("Attachments").GetXml():"<NewDataSet> </NewDataSet>"),
                                           };


            objDAL.RunProc("[EC_CRM_Trn_AttachmentSave]", objSqlParam,ref objDS);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {

                //string AttachmentPath;
                ////foreach (DataRow Dr in objDS.Tables[0].Rows)
                ////{
                //AttachmentPath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath + "/CRM/Attachments/" + "633578830613750000^file.txt");
                //    DeleteAttachement(AttachmentPath);
                ////}
            }
            return objMessage;
        }


        //private void DeleteAttachement(string filePath)
        //{
        //        try
        //        {
        //            FileInfo serverFile = new FileInfo(filePath);

        //            if (serverFile.IsReadOnly)
        //            { serverFile.IsReadOnly = false;}
                 
        //            if (serverFile.Exists)
        //            {
        //               File.Delete(filePath);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message);
        //        }
        //}
    }
}
