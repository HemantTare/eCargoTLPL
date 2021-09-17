// JScript File


function ddlDesti1Change()
{
  var ddl_Desti1 = document.getElementById('ddl_Desti1');
  var ddl_Desti2 = document.getElementById('ddl_Desti2');
  var ddl_Desti3 = document.getElementById('ddl_Desti3');
  
  var ddl_Desti1As = document.getElementById('ddl_Desti1As');
  var ddl_Desti2As = document.getElementById('ddl_Desti2As');
  
  var txt_LR2 = document.getElementById('txtLR2');
  var txt_LR3 = document.getElementById('txtLR3');

  var txt_Bonus2 = document.getElementById('txtBonus2');
  var txt_Bonus3 = document.getElementById('txtBonus3');
  
  if (ddl_Desti1.value == "0")
  {
    ddl_Desti2.value="0";
    ddl_Desti2.disabled=true;
    ddl_Desti3.value="0";
    ddl_Desti3.disabled=true;
    
    txt_LR2.value="";
    txt_LR3.value="";
    
    txt_LR2.disabled=true;
    txt_LR3.disabled=true;
    
    txt_Bonus2.value="";
    txt_Bonus3.value="";

    txt_Bonus2.disabled=true;
    txt_Bonus3.disabled=true;
    
    ddl_Desti1As.disabled=true;
    ddl_Desti2As.disabled=true;
    
  }
  else
  {
    ddl_Desti2.disabled=false;
    txt_LR2.disabled=false;
    txt_Bonus2.disabled=false;
    
    ddl_Desti1As.disabled=false;

  }
}  
  


function ddlDesti2Change()
{
  var ddl_Desti2 = document.getElementById('ddl_Desti2');
  var ddl_Desti3 = document.getElementById('ddl_Desti3');

  var ddl_Desti2As = document.getElementById('ddl_Desti2As');

  var txt_LR3 = document.getElementById('txtLR3');
  var txt_Bonus3 = document.getElementById('txtBonus3');
  
  if (ddl_Desti2.value == "0")
  {
    ddl_Desti3.value="0";
    ddl_Desti3.disabled=true;
    
    txt_LR3.value="";
    txt_LR3.disabled=true;
    
    txt_Bonus3.value="";
    txt_Bonus3.disabled=true;
    
    ddl_Desti2As.disabled=true;
    
  }
  else
  {
    ddl_Desti3.disabled=false;
    txt_LR3.disabled=false;
    txt_Bonus3.disabled=false;
    
    ddl_Desti2As.disabled=false;
  }
} 