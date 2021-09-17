using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;
using ClassLibraryMVP;
//using Raj.eCargo.Init;

/// <summary> 
/// Summary description for PetrolPumpPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class PetrolPumpPresenter : ClassLibraryMVP.General.Presenter 
    {

   
        private IPetrolPumpView objPetrolPumpView ;
        private PetrolPumpModel objPetrolPumpModel ;

        public PetrolPumpPresenter(IPetrolPumpView PetrolPumpView, bool isPostback)
        {
            objPetrolPumpView = PetrolPumpView;
            objPetrolPumpModel = new PetrolPumpModel(objPetrolPumpView);

            base.Init(objPetrolPumpView, objPetrolPumpModel);

        }


        public void Save()
        {
             base.DBSave();
            //objPetrolPumpModel.Save();
        }
    }
}