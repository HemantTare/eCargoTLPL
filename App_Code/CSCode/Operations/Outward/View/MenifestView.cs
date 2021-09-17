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

/// <summary>
/// Summary description for MenifestView
/// </summary>
namespace Raj.EC.OperationView
{
    public interface IMenifestView : IView
    {
        int MenifestTypeID { get;set;}
        int MenifestToID { get;set;}
        int LoadedById { get;}
        int VehicleCotegoryID { get;set;}
        int VehicleID { get;set;}
        int ALSID { get;set;}
        int Total_Loded_Articles { get;set;}
        int Total_No_Of_GC { get;set;}
        bool IsFrom_Edit { get;}
        string Flag { get;}
        string MenifestTo { get;set;}
        string Remarks { get;set;}
        string MenifestNo { set;}
        string ArrivalDeliveryTime { get;set;}

        DateTime MenifestDate { get;set;}
        DateTime ALSDate { get;set;}
        DateTime ArrivalDeliveryDate { get;set;}

        decimal Book_ActualWt { get;set;}
        decimal Book_ToPayCollection { get;set;}
        decimal Cros_ActualWt { get;set;}
        decimal Cros_ToPayCollection { get;set;}
        decimal Total_ActualWt { get;set;}
        decimal Total_ToPayCollection { get;set;}
        decimal Total_Loded_Weight { get;set;}

        DataTable BindMenifestType { set;}
        DataTable BindVehicleCotegory { set;}
        DataTable BindALSNo { set;}
        DataTable SessionBindMenifestGrid { set;}

        //void SetMenifestToId(string text, string value);
        void SetLoadedById(string text, string value);

        string MenifestDetailsXML { get;}
        string GetGCNoXML { get;set;}

        int Next_No{get;set;}
        string MEMO_No{get;set;}
        int Document_Series_Allocation_ID { get;set;}

        string Number_Part4 { get;set;}
        string ShortUrl { get;set;}

        void ClearVariables();
    }
}
