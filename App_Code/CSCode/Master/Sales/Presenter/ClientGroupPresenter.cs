using System;
using System.Data;
using System.Web.Security;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.SalesView;
using Raj.EC.SalesModel;

/// <summary>
/// Summary description for ClientGroupPresenter
/// </summary>
namespace Raj.EC.SalesPresenter
{
    public class ClientGroupPresenter : Presenter
    {
        private IClientGroupView objIClientGroupView;
        private ClientGroupModel objClientGroupModel;
        private DataSet objDS;

        public ClientGroupPresenter(IClientGroupView clientGroupView, bool IsPostBack)
        {
            objIClientGroupView = clientGroupView;
            objClientGroupModel = new ClientGroupModel(objIClientGroupView);

            base.Init(objIClientGroupView, objClientGroupModel);

            if (!IsPostBack)
            {
                FillValues();
                initValues();
            }
        }

        private void FillValues()
        {
            FillParentGroup();
            FillLedgerGroup();
        }

        public void FillParentGroup()
        {
            objIClientGroupView.BindParentGroup = objClientGroupModel.GetParentGroupValues();
        }

        public void FillLedgerGroup()
        {
            objIClientGroupView.BindLedgerGroup = objClientGroupModel.GetLedgerGroupValues();
        }
        public bool IsClientGroupChanged()
        {
            return objClientGroupModel.IsClientGroupChanged();
        }
        private void initValues()
        {
            if (objIClientGroupView.keyID > 0)
            {
                objDS = objClientGroupModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIClientGroupView.ClientGroupName = DR["Client_Group_Name"].ToString();
                    objIClientGroupView.ParentGroupId=Util.String2Int(DR["Parent_Group_Id"].ToString());
                    objIClientGroupView.LedgerGroupForRadio = Util.String2Bool(DR["Use_Existing_Ledger_Group"].ToString());
                    //if(Util.String2Bool(DR["Use_Existing_Ledger_Group"].ToString()) == true)
                    //{
                    //    objIClientGroupView.LedgerGroupForRadio=false;
                    //}
                    //else
                    //{
                    //    objIClientGroupView.LedgerGroupForRadio = true;
                    //}
                    FillLedgerGroup();
                    objIClientGroupView.LedgerGroupId = Util.String2Int(DR["Ledger_Group_ID"].ToString());
                }
            }
        }     

        public void Save()
        {
            base.DBSave();
        }
	}
}
