// JScript File

function ValidateUI()
{
var ATS = false;

 var Lbl_Error = document.getElementById('WucProfile1_lbl_Errors');
 var txt_ProfileName = document.getElementById('WucProfile1_txt_ProfileName');
 var txt_Description = document.getElementById('WucProfile1_txt_Description');
 var ddl_Hierarchy = document.getElementById('WucProfile1_ddl_Hierarchy');
 
 var objResource = new Resource('WucProfile1_hdf_ResourceString');
 
 Lbl_Error.innerText =" ";
 if(Trim(txt_ProfileName.value) == '')
 {
     Lbl_Error.innerText = objResource.GetMsg("Msg_txt_ProfileName");  // "Please Enter Profile Name";
     txt_ProfileName.focus();
 }else if(Trim(txt_Description.value) == '')
 {
     Lbl_Error.innerText =objResource.GetMsg("Msg_txt_Description");  // "Please Enter Description";
     txt_Description.focus();
 }else if(ddl_Hierarchy.options.length <= 0)
 {
     Lbl_Error.innerText = objResource.GetMsg("Msg_txt_HierarchyName"); // "Please Select Hierarchy Name";
     ddl_Hierarchy.focus();
 }
 
  else 
 ATS = true;

        return ATS;

}

function Trim(sString) 
{
    //Begining space Removes
    while (sString.substring(0,1) == ' ')
        {
             sString = sString.substring(1, sString.length);
        }
    //Ending space Removes
    while (sString.substring(sString.length-1, sString.length) == ' ')
        {
             sString = sString.substring(0,sString.length-1);
        }
     return sString;
}
