
// JScript File
function validateUI()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucFamilyRelation1_lbl_Errors');
   var txt_Family_Relation_Name = document.getElementById('WucFamilyRelation1_txt_Family_Relation_Name');
//   var objResource=new Resource('WucFamilyRelation1_hdf_ResourecString');
  lbl_Errors.innerText ="";
  
  if (txt_Family_Relation_Name.value == '')
  {
      lbl_Errors.innerText = "Please Enter Family Relation  Name";// objResource.GetMsg("Msg_txt_FamilyRelationName");
     txt_Family_Relation_Name.focus();
  }
  else
      ATS = true;

return ATS;
}



