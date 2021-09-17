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
using Raj.EF.MasterView;
using Raj.EC.ControlsView;
using Raj.EF.TransactionsView;
/// <summary>
/// Summary description for VehicleView
/// </summary>
namespace Raj.EF.MasterView
{
	public interface IVehicleView : IView
	{
        string CallFrom { get;}
        IVehicleInformationView VehicleInformationView { get;}
        IEngineBodySpecificationView EngineBodySpecificationView { get;}
        IVehicleLoanDetailsView VehicleLoanDetailsView { get;}
        IVehicleChasisTyresView VehicleChasisTyresView { get;}
        IRegistrationFitnessView RegistrationFitnessView { get;}
        IRegistrationPermitView RegistrationPermitView { get;}
        IVehicleHireDetailsView VehicleHireDetailsView { get;}
        IAttachmentsView AttachmentsView { get;}
        IVehicleInsurancePremiumView VehicleInsurancePremiumView { get;}
        void ClearVariables();
	}
}
