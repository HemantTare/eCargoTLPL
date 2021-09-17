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
/// Date   : 08-01-09
/// Summary description for GCConsigneeListPresenter
/// </summary>
/// 

namespace Raj.EC.OperationPresenter
{
    public class GCConsigneeListPresenter : Presenter
    {
        private IGCConsigneeListView objIGCConsigneeListView;
        private GCConsigneeListModel objGCConsigneeListModel;
        private DataSet objDS;

        public GCConsigneeListPresenter(IGCConsigneeListView GCConsigneeListView, Boolean isPostBack)
        {
            objIGCConsigneeListView = GCConsigneeListView;
            objGCConsigneeListModel = new GCConsigneeListModel(objIGCConsigneeListView);
            base.Init(objIGCConsigneeListView, objGCConsigneeListModel);
            
            if (!isPostBack)
            {
            }
        }

        public void fillgrid()
        {
            objDS = objGCConsigneeListModel.ReadValues();
            objIGCConsigneeListView.SessionGConsigneeList = objDS.Tables[0];
        }

        //////public void fillClientDetails()
        //////{
        //////    objDS = objGCConsigneeListModel.FillClientDetails();
        //////    DataRow dr;
        //////    dr = objDS.Tables[0].Rows[0];
        //////    objIGCConsigneeListView.ContactPerson = dr["Contact_Person"].ToString();
        //////    objIGCConsigneeListView.Add1 = dr["Address1"].ToString();
        //////    objIGCConsigneeListView.Add2 = dr["Address2"].ToString();
        //////    objIGCConsigneeListView.pincode = dr["Pin_Code"].ToString();
        //////    objIGCConsigneeListView.stdcode = dr["Std_Code"].ToString();
        //////    objIGCConsigneeListView.phone = dr["Phone1"].ToString();
        //////    objIGCConsigneeListView.mobile = dr["Mobile_No"].ToString();
        //////    objIGCConsigneeListView.csttinno = dr["CST_TIN_No"].ToString();
        //////    objIGCConsigneeListView.servicetaxNo = dr["Service_Tax_No"].ToString();
        //////}

        public void Save()
        {
            objGCConsigneeListModel.Save();
        }

    }
}