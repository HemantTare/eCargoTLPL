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

using ClassLibraryMVP.DataAccess;

using Raj.EC.ControlsView;


/// <summary>
/// Summary description for AttachmentsModel
/// </summary>
/// 


namespace Raj.EC.ControlsModel
{
    public class AttachmentsModel
    {
        private IAttachmentsView objIAttachmentsView;
        private DAL objDAL = new DAL();
        DataSet objDS = new DataSet();

        public AttachmentsModel(IAttachmentsView _objIAttachmentsView)
        {
            objIAttachmentsView = _objIAttachmentsView;
        }

        public DataSet GetValuesForImageDatagrid()
        {
            SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Attachment_Form_ID", SqlDbType.Int, 0, objIAttachmentsView.AttachmentFormId),
            objDAL.MakeInParams("@ID", SqlDbType.Int, 0, objIAttachmentsView.keyID)};

            objDAL.RunProc("EC_Mst_Attachment_Read", objSqlParam, ref objDS);
            return objDS;
        }
    }
}