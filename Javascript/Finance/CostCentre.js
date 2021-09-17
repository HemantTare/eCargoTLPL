// JScript File

/// Author        : Ankit Champaneriya 
/// Created On    : 15/10/2008
/// Description   : This Page is For  Cost centre java script 

function Allow_To_Save()
{
    var ATS = false;

    var Lbl_Error = document.getElementById('WucCostCentre1_lbl_Errors');
    var Txt_Cost_Centre_Name = document.getElementById('WucCostCentre1_Txt_Cost_Centre_Name');
    
    var objResource = new Resource('WucCostCentre1_hdf_ResourceString');
 
    Lbl_Error.innerText = " ";
    
     if(Trim(Txt_Cost_Centre_Name.value) == '')
     {
         Lbl_Error.innerText = objResource.GetMsg("Msg_Txt_Cost_Centre_Name");  // 
         Txt_Cost_Centre_Name.focus();
     }
     else if (ChkLstBox_Select_Atleast_One_Ledger()==false)
     {
        Lbl_Error.innerText = "Select Atleast One Ledger" ; // objResource.GetMsg("MSG_Select_Ledger"); 
     }
     else
     ATS = true;
            return ATS;
}


function ChkLstBox_Select_Atleast_One_Ledger() 
{

    var found = false
    for(i = 0; i < document.forms[0].elements.length; i++) 
    {
        elm = document.forms[0].elements[i];
        if (elm.name != undefined)
        {
            if (elm.type == 'checkbox')
                 {   
                    if(elm.checked)
                        { 
			                found =  true; 
			                break;
            			}
            	}		
        }
    }

   // if (found == false)
    
       //{ alert("Select Atleast One Ledger"); }

return found
}


function CheckAllCheckBoxes(chk) 
{
    for(i = 0; i < document.forms[0].elements.length; i++) 
    {
        elm = document.forms[0].elements[i];
        if (elm.name != undefined)
        {
            if (elm.type == 'checkbox')
             {
                var elm_name = elm.name;
                var arr = elm_name.split("$");
                elm.checked = chk.checked;
            }
        }
    }
}
