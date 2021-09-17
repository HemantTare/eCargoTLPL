using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.OperationView  ;
using Raj.EC.OperationModel  ;
using ClassLibraryMVP;

/// <summary>
/// Summary description for AUSPresenter
/// </summary>
/// 

namespace Raj.EC.OperationPresenter
{
    public class AUSPresenter : ClassLibraryMVP.General.Presenter 
    {

        private IAUSView   objAUSView;
        private  AUSModel  objAUSModel;

        public AUSPresenter(IAUSView AUSView, bool isPostback)
        {
            objAUSView = AUSView;
            objAUSModel = new AUSModel(objAUSView  );

            base.Init(objAUSView  , objAUSModel  );


        }

        public void AUSPrintLabel()
        {
            objAUSModel.PrintLabel();  
        }

        public void Save()
        {
            base.DBSave();
            //objAUSModel.Save ();
        }
    }
}