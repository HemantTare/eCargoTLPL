using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using Raj.EC.MasterView;

/// <summary>
/// Summary description for ContractFreightDetailsView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface IContractFreightDetailsView : IView
    {
        // Integer Property Declaration

        int UnitOfFreightID { get;set;}
        int SubUnitID { get;set;}
        int FreightBasisID { get;set;}
        int UnitFreightID { get;set;}
        int SubUnitFreightID { get;set;}
        int FromLocationID { get;}
        int ToLocationID { get;}
        int SrNo { get;set;}

        string OtherChargesFreightRateDetailsXML { get;}
        string FreightDetailsGridXML { get;}
        //Decimal Property Declaration       
        decimal CFTFactor { get;set;}

        //void Property For DDLSearch Control(string text, string value);
        void SetFromLocationID(string FromLocation_Name, string FromLocationID);
        void SetToLocationID(string ToLocation_Name, string ToLocationID);
        
        //DataTable Property Declaration
        DataTable Bind_ddl_UnitOfFreight { set;}
        DataTable Bind_ddl_SubUnit { set;}
        DataTable Bind_ddl_FreightBasis { set;}
        DataTable Bind_ddl_UnitFreight{ set;}
        DataTable Bind_ddl_SubUnitFreight { set;}
        DataSet Bind_dg_FreightDetails { set;}
        DataSet SessionFreightDetailsGrid { get;set;}
      //  DataTable Bind_ddl_FromLocation { set;}
     //   DataTable Bind_ddl_ToLocation { set;}
    //    DataTable SessionFromToLocation{ get;set;}
        DataSet SessionOtherChargesFreightRateDetails { get;set;}
    }   
}