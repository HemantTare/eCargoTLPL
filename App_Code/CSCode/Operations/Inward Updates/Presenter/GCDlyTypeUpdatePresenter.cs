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
/// Summary description for GCDlyTypeUpdatePresenter
/// </summary>
/// 

namespace Raj.EC.OperationPresenter
{
    public class GCDlyTypeUpdatePresenter : Presenter
    {
        private IGCDlyTypeUpdateView objIGCDlyTypeUpdateView;
        private GCDlyTypeUpdateModel objGCDlyTypeUpdateModel;
        private DataSet objDS;

        public GCDlyTypeUpdatePresenter(IGCDlyTypeUpdateView GCDlyTypeUpdateView, Boolean isPostBack)
        {
            objIGCDlyTypeUpdateView = GCDlyTypeUpdateView;
            objGCDlyTypeUpdateModel = new GCDlyTypeUpdateModel(objIGCDlyTypeUpdateView);
            base.Init(objIGCDlyTypeUpdateView, objGCDlyTypeUpdateModel);
            
            if (!isPostBack)
            {
                initValues();
            }
        }
        private void initValues()
        {
            fillValues();
        }

        private void fillValues()
        {
            objDS = objGCDlyTypeUpdateModel.FillValues();
            objIGCDlyTypeUpdateView.SessionDeliveryType = objDS.Tables[0];  
        }

        public void fillgrid()
        {
            objDS = objGCDlyTypeUpdateModel.ReadValues();
            objIGCDlyTypeUpdateView.SessionGCDlyTypeUpdate = objDS.Tables[0];
        }

     
        public void Save()
        {
            objGCDlyTypeUpdateModel.Save();
        }

    }
}