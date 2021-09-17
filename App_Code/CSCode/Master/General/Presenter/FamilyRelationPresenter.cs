using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EF.MasterView;
using Raj.EF.MasterModel;


/// <summary>
/// Summary description for FamilyRelationPresenter
/// </summary>
namespace Raj.EF.MasterPresenter
{
    public class FamilyRelationPresenter : Presenter
    {
        private IFamilyRelationView objIFamilyRelationView;
        private FamilyRelationModel objFamilyRelationModel;
        private DataSet objDS;

        public FamilyRelationPresenter(IFamilyRelationView familyRelationView, bool isPostBack)
        {
            objIFamilyRelationView = familyRelationView;
            objFamilyRelationModel = new FamilyRelationModel(objIFamilyRelationView);
            base.Init(objIFamilyRelationView, objFamilyRelationModel);

            if (!isPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            if (objIFamilyRelationView.keyID > 0)
            {
                objDS = objFamilyRelationModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIFamilyRelationView.FamilyRelationName = objDS.Tables[0].Rows[0]["Family_Relation"].ToString();
                    objIFamilyRelationView.Gender_ID = Util.String2Int(objDS.Tables[0].Rows[0]["Gender"].ToString());
                  
                }

            }

        }
        public void Save()
        {
            base.DBSave();
            //objFamilyRelationModel.Save();
        }

    }

}

