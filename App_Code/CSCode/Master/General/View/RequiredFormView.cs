using System;
using System.Data;
using ClassLibraryMVP.General;

/// <summary>
/// Name : Ankit champaneriya
/// Date : 10-11-08
/// Summary description for FormView
/// </summary>
/// 

namespace Raj.EC.MasterView
{
    public interface IRequiredFormView : IView
    {
        string  FormName { get;set;}
        string Description { get;set;}
    }
}