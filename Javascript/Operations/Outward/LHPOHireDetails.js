// JScript File

function EnabledDisabledControlOnFreightType(IsExecute)
{
//if (IsExecute == 1) return;

//var hdn_is_page_loaded=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_is_page_loaded');
//if (hdn_is_page_loaded.value == '0')
//  return;

var ddl_FreightType=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_FreightType');
var lbl_WtGuarantee=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_WtGuarantee');
var lbl_RateKg=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_RateKg');
var lbl_ActualWtPayable=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_ActualWtPayable');
var txt_WtGuarantee=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_WtGuarantee');
var txt_RateKg=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_RateKg');
var lbl_ActualWtPayableValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_ActualWtPayableValue');
var lbl_TruckHireChargeValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_TruckHireChargeValue');
var txt_TruckHireCharge=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TruckHireCharge');

tr_WtGuarantee=document.getElementById('WucLHPO1_WucLHPOHireDetails1_tr_WtGuarantee');
tr_RateKg=document.getElementById('WucLHPO1_WucLHPOHireDetails1_tr_RateKg');
tr_ActualWtPayable=document.getElementById('WucLHPO1_WucLHPOHireDetails1_tr_ActualWtPayable');
tr_TruckHireCharge=document.getElementById('WucLHPO1_WucLHPOHireDetails1_tr_TruckHireCharge');
tr_txt_TruckHireCharge=document.getElementById('WucLHPO1_WucLHPOHireDetails1_tr_txt_TruckHireCharge');
tr_ActualKms=document.getElementById('WucLHPO1_WucLHPOHireDetails1_tr_ActualKms');

hdn_TotalArticle=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalArticle');
hdn_TotalArticleWT=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalArticleWT');
txt_TotalArticle=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TotalArticle');
txt_TotalArticleWT=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TotalArticleWT');

hdn_ActualKms=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_ActualKms');
hdn_TruckHireCharge=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TruckHireCharge');
hdn_ActualWtPayable=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_ActualWtPayable');

hdn_TDSPer=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TDSPer');
hdn_TDSAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TDSAmount');
hdn_TotalTruckHire=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalTruckHire');
hdn_LHPOId=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_LHPOId');

hdn_FreightType = document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_FreightType');
hdn_WtGuarantee = document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_WtGuarantee');
hdn_RateKg = document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_RateKg');      

tr_WtGuarantee.style.display = 'none';
tr_RateKg.style.display = 'none';
tr_ActualWtPayable.style.display = 'none';
tr_TruckHireCharge.style.display = 'none';
tr_txt_TruckHireCharge.style.display = 'none';
tr_ActualKms.style.display = 'none';

if(val(ddl_FreightType.value)==1)
  {
  tr_WtGuarantee.style.display = '';
  tr_RateKg.style.display = '';
  tr_ActualWtPayable.style.display = '';
  tr_TruckHireCharge.style.display = '';
  lbl_WtGuarantee.innerText='Wt. Guarantee:';
  lbl_RateKg.innerText='Rate/Kg:';
  lbl_ActualWtPayable.innerText='Actual Wt. Payable:';
  }
else if(val(ddl_FreightType.value)==2)
  {
  tr_txt_TruckHireCharge.style.display = '';
  }
else if(val(ddl_FreightType.value)==3)
  {
  tr_WtGuarantee.style.display = '';
  tr_RateKg.style.display = '';
  tr_ActualWtPayable.style.display = '';     
  lbl_WtGuarantee.innerText='Km Guarantee:';
  lbl_RateKg.innerText='Rate/Km:';
  lbl_ActualWtPayable.innerText='Kms Payable:';     
  tr_TruckHireCharge.style.display = '';
  tr_ActualKms.style.display = '';
  }
else if(val(ddl_FreightType.value)==4)
  {
  tr_WtGuarantee.style.display = '';
  tr_RateKg.style.display = '';
  tr_ActualWtPayable.style.display = '';
  lbl_WtGuarantee.innerText='Articles Guarantee:';
  lbl_RateKg.innerText='Rate/Articles:';
  lbl_ActualWtPayable.innerText='Articles Payable:';
  tr_TruckHireCharge.style.display = '';
  }
}
//====================================================================================================

function Check_All(chk,gridname)
{
var grid = document.getElementById(gridname);
var checkbox,TotalGC,TotalArticles,TotalActualWT;
var CrossingCost,DeliveryCommision,ToPayCollection;
var sum_CrossingCost=0,sum_DeliveryCommision=0,sum_ToPayCollection=0;
var i,j=0;
var sum_TotalGC=0,sum_TotalArticles=0,sum_TotalActualWT=0;

var txt_TotalArticle = document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TotalArticle');
var txt_TotalArticleWT = document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TotalArticleWT');
var ddl_LHPOType=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_LHPOType');  
var hdn_TotalArticle= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalArticle');
var hdn_TotalArticleWT = document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalArticleWT');
var hdn_Total_No_of_GC = document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_Total_No_of_GC');

var hdn_NetAmount= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_NetAmount');
var hdn_ToPayCollection= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_ToPayCollection');
var hdn_TotalPayable= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalPayable');
var hdn_DeliveryCommission= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_DeliveryCommission');
var hdn_CrossingCost= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_CrossingCost');


var lbl_NetAmountValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_NetAmountValue');
var lbl_ToPayCollectionValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_ToPayCollectionValue');
var lbl_TotalPayableValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_TotalPayableValue');
var lbl_DeliveryCommissionValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_DeliveryCommissionValue');
var lbl_CrossingCostValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_CrossingCostValue');
var txt_Others= document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_Others');

var hdn_TotalCrossingCost= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalCrossingCost');
var hdn_TotalDeliveryCommision= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalDeliveryCommision');
var hdn_TotalToPayCollection= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalToPayCollection');
        
var max = (grid.rows.length - 1);
for(i=1;i<grid.rows.length;i++)
  {
  checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
  TotalArticles = grid.rows[i].cells[6].getElementsByTagName('input');
  TotalActualWT = grid.rows[i].cells[7].getElementsByTagName('input');          

  CrossingCost = grid.rows[i].cells[8].getElementsByTagName('input');          
  DeliveryCommision = grid.rows[i].cells[9].getElementsByTagName('input');          
  ToPayCollection = grid.rows[i].cells[10].getElementsByTagName('input');          
 
  if(checkbox[0].type = 'checkbox')
    {
    checkbox[0].checked = chk.checked;
    }
  if(chk.checked == true)
    {
    if(TotalArticles[0].type =='text')
      {
      sum_TotalArticles = sum_TotalArticles + val(TotalArticles[0].value);
      }
    if(TotalActualWT[0].type =='text')
      {
      sum_TotalActualWT = sum_TotalActualWT + val(TotalActualWT[0].value);
      }
    if(CrossingCost[0].type =='hidden')
      {
      sum_CrossingCost = sum_CrossingCost + val(CrossingCost[0].value);
      }
     if(DeliveryCommision[0].type =='hidden')
      {
      sum_DeliveryCommision = sum_DeliveryCommision + val(DeliveryCommision[0].value);
      }
     if(ToPayCollection[0].type =='hidden')
      {
      sum_ToPayCollection = sum_ToPayCollection + val(ToPayCollection[0].value);
      }
    }//if(chk.checked == true)
  }//for(i=1;i<grid.rows.length;i++)
        
  if(chk.checked == true)
    {
    txt_TotalArticle.value = sum_TotalArticles;
    txt_TotalArticleWT.value = sum_TotalActualWT;

    hdn_Total_No_of_GC.value = max;
    hdn_TotalArticle.value = sum_TotalArticles;
    hdn_TotalArticleWT.value = sum_TotalActualWT;

    hdn_TotalCrossingCost.value=sum_CrossingCost;
    lbl_CrossingCostValue.innerText=val(hdn_TotalCrossingCost.value);
    hdn_TotalDeliveryCommision.value=sum_DeliveryCommision;
    lbl_DeliveryCommissionValue.innerText=val(hdn_TotalDeliveryCommision.value);
    hdn_TotalToPayCollection.value=sum_ToPayCollection;
    lbl_ToPayCollectionValue.innerText=val(hdn_TotalToPayCollection.value);

    hdn_TotalPayable.value= val(hdn_TotalCrossingCost.value) + val(hdn_TotalDeliveryCommision.value) + val(txt_Others.value);
    lbl_TotalPayableValue.innerText=val(hdn_TotalPayable.value);

    hdn_NetAmount.value=val(hdn_TotalPayable.value)-val(hdn_TotalToPayCollection.value);
    lbl_NetAmountValue.innerText=val(hdn_NetAmount.value);
    }
  else
    {
    txt_TotalArticle.value = 0;
    txt_TotalArticleWT.value = 0;

    hdn_Total_No_of_GC.value = 0;
    hdn_TotalArticle.value = 0;
    hdn_TotalArticleWT.value = 0;

    hdn_TotalCrossingCost.value=0;
    lbl_CrossingCostValue.innerText=0;

    hdn_TotalDeliveryCommision.value=0;
    lbl_DeliveryCommissionValue.innerText=0;

    hdn_TotalToPayCollection.value=0;
    lbl_ToPayCollectionValue.innerText=0;

    txt_Others.value=0; 

    hdn_TotalPayable.value= 0; 
    lbl_TotalPayableValue.innerText=0;

    hdn_NetAmount.value=0;
    lbl_NetAmountValue.innerText=0;
    }

  if(val(ddl_LHPOType.value)==1)
    {
    CalculateTruckHireCharge();        
    }
}
//====================================================================================================
function Check_Single(chk,gridname)
{
var grid = document.getElementById(gridname);
var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
var row = chk.parentElement.parentElement;
var TotalGC,TotalArticles,TotalActualWT;
var CrossingCost,DeliveryCommision,ToPayCollection;
var sum_CrossingCost,sum_DeliveryCommision,sum_ToPayCollection;
var Total_CrossingCost=0,Total_DeliveryCommision=0,Total_ToPayCollection=0;

var ddl_LHPOType=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_LHPOType');  
var txt_TotalArticle = document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TotalArticle');
var txt_TotalArticleWT = document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TotalArticleWT');

var hdn_TotalArticle= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalArticle');
var hdn_TotalArticleWT = document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalArticleWT');
var hdn_Total_No_of_GC = document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_Total_No_of_GC');

var hdn_NetAmount= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_NetAmount');
var hdn_ToPayCollection= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_ToPayCollection');
var hdn_TotalPayable= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalPayable');
var hdn_DeliveryCommission= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_DeliveryCommission');
var hdn_CrossingCost= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_CrossingCost');

var hdn_TotalCrossingCost= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalCrossingCost');
var hdn_TotalDeliveryCommision= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalDeliveryCommision');
var hdn_TotalToPayCollection= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalToPayCollection');

var lbl_NetAmountValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_NetAmountValue');
var lbl_ToPayCollectionValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_ToPayCollectionValue');
var lbl_TotalPayableValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_TotalPayableValue');
var lbl_DeliveryCommissionValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_DeliveryCommissionValue');
var lbl_CrossingCostValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_CrossingCostValue');
var txt_Others= document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_Others');


TotalArticles  = row.cells[6].getElementsByTagName('input');
TotalActualWT = row.cells[7].getElementsByTagName('input');


CrossingCost = row.cells[8].getElementsByTagName('input');          
DeliveryCommision = row.cells[9].getElementsByTagName('input');          
ToPayCollection = row.cells[10].getElementsByTagName('input');          
  

if (chk.checked == true)
  {
  txt_TotalArticle.value = val(txt_TotalArticle.value) + val(TotalArticles[0].value);
  txt_TotalArticleWT.value  = val(txt_TotalArticleWT.value)  + val(TotalActualWT[0].value);

  hdn_Total_No_of_GC.value = val(hdn_Total_No_of_GC.value) + 1;
  hdn_TotalArticle.value = val(hdn_TotalArticle.value) + val(TotalArticles[0].value);
  hdn_TotalArticleWT.value  = val(hdn_TotalArticleWT.value)  + val(TotalActualWT[0].value);

  hdn_TotalCrossingCost.value = val(hdn_TotalCrossingCost.value) + val(CrossingCost[0].value);
  hdn_TotalDeliveryCommision.value = val(hdn_TotalDeliveryCommision.value) + val(DeliveryCommision[0].value);
  hdn_TotalToPayCollection.value = val(hdn_TotalToPayCollection.value) + val(ToPayCollection[0].value);
  }
else
  {
  txt_TotalArticle.value = val(txt_TotalArticle.value) - val(TotalArticles[0].value);
  txt_TotalArticleWT.value  = val(txt_TotalArticleWT.value)  - val(TotalActualWT[0].value);

  hdn_Total_No_of_GC.value = val(hdn_Total_No_of_GC.value) - 1; 
  hdn_TotalArticle.value = val(hdn_TotalArticle.value) - val(TotalArticles[0].value);
  hdn_TotalArticleWT.value  = val(hdn_TotalArticleWT.value)  - val(TotalActualWT[0].value);

  hdn_TotalCrossingCost.value = val(hdn_TotalCrossingCost.value) - val(CrossingCost[0].value);
  hdn_TotalDeliveryCommision.value = val(hdn_TotalDeliveryCommision.value) - val(DeliveryCommision[0].value);
  hdn_TotalToPayCollection.value = val(hdn_TotalToPayCollection.value) - val(ToPayCollection[0].value);
  }
lbl_CrossingCostValue.innerText=val(hdn_TotalCrossingCost.value); 
lbl_DeliveryCommissionValue.innerText=val(hdn_TotalDeliveryCommision.value);
lbl_ToPayCollectionValue.innerText=val(hdn_TotalToPayCollection.value);
 
hdn_TotalPayable.value= val(hdn_TotalCrossingCost.value) + val(hdn_TotalDeliveryCommision.value) + val(txt_Others.value);
lbl_TotalPayableValue.innerText=val(hdn_TotalPayable.value);
   
hdn_NetAmount.value=val(hdn_TotalPayable.value)-val(hdn_TotalToPayCollection.value);
lbl_NetAmountValue.innerText=val(hdn_NetAmount.value); 

if((grid.rows.length-1) == val(hdn_Total_No_of_GC.value))
  checkall[0].checked = true;
else
  checkall[0].checked = false;

if(val(ddl_LHPOType.value)==1)
  {
  CalculateTruckHireCharge(0); 
  }
}

//====================================================================================================

function CalculateTruckHireCharge(callfrom)
{
ddl_FreightType=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_FreightType');
lbl_WtGuarantee=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_WtGuarantee');
lbl_RateKg=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_RateKg');
lbl_ActualWtPayable=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_ActualWtPayable');
txt_WtGuarantee=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_WtGuarantee');
txt_RateKg=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_RateKg');
lbl_ActualWtPayableValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_ActualWtPayableValue');
lbl_TruckHireChargeValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_TruckHireChargeValue');
txt_TruckHireCharge=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TruckHireCharge');
lbl_ActualKmsValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_ActualKmsValue');
txt_CharityAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_CharityAmount');


tr_WtGuarantee=document.getElementById('WucLHPO1_WucLHPOHireDetails1_tr_WtGuarantee');
tr_RateKg=document.getElementById('WucLHPO1_WucLHPOHireDetails1_tr_RateKg');
tr_ActualWtPayable=document.getElementById('WucLHPO1_WucLHPOHireDetails1_tr_ActualWtPayable');
tr_TruckHireCharge=document.getElementById('WucLHPO1_WucLHPOHireDetails1_tr_TruckHireCharge');
tr_txt_TruckHireCharge=document.getElementById('WucLHPO1_WucLHPOHireDetails1_tr_txt_TruckHireCharge');
tr_ActualKms=document.getElementById('WucLHPO1_WucLHPOHireDetails1_tr_ActualKms');

hdn_TotalArticle=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalArticle');
hdn_TotalArticleWT=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalArticleWT');
txt_TotalArticle=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TotalArticle');
txt_TotalArticleWT=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TotalArticleWT');

hdn_ActualKms=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_ActualKms');
hdn_TruckHireCharge=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TruckHireCharge');
hdn_ActualWtPayable=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_ActualWtPayable');

hdn_SurchargeAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_SurchargeAmount');
hdn_AddlSurchargeCessAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_AddlSurchargeCessAmount');
hdn_AddlEducationCessAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_AddlEducationCessAmount');
hdn_TDSAmountValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TDSAmountValue');
var ddl_LHPOType=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_LHPOType');  


//hdn_TruckHireCharge.value=0;
//hdn_ActualWtPayable.value=0;
//txt_WtGuarantee.value=0;
//txt_RateKg.value=0;
//lbl_ActualWtPayableValue.innerText=0;
//lbl_TruckHireChargeValue.innerText=0;
//hdn_TotalTruckHire.value=0;                                     
//hdn_SurchargeAmount.value=0;
//hdn_AddlSurchargeCessAmount.value=0;
//hdn_AddlEducationCessAmount.value=0;
//hdn_TDSAmount.value=0;
//hdn_TDSAmountValue.value=0;

if (callfrom ==1)
  {
  txt_WtGuarantee.value=0;
  txt_RateKg.value=0;
  txt_CharityAmount.value=0;
  }
  
if(val(ddl_FreightType.value)==1)//1	Per Kg
  {
  
  lbl_ActualWtPayableValue.innerText=val(txt_WtGuarantee.value); 
  hdn_ActualWtPayable.value=val(txt_WtGuarantee.value);
  
  if ((val(hdn_TotalArticleWT.value) > val(txt_WtGuarantee.value)) && (val(ddl_LHPOType.value)==1))
    {    
    hdn_ActualWtPayable.value=val(hdn_TotalArticleWT.value);   
    lbl_ActualWtPayableValue.innerText=val(hdn_TotalArticleWT.value); 
    }
  hdn_TruckHireCharge.value=val(txt_RateKg.value)*val(hdn_ActualWtPayable.value);
  lbl_TruckHireChargeValue.innerText=val(hdn_TruckHireCharge.value);
  tr_ActualKms.style.display = 'none';
  tr_txt_TruckHireCharge.style.display='none';
  //hdn_ActualKms.value=0;
  lbl_ActualKmsValue.innerText=0;

  }
else if(val(ddl_FreightType.value)==2) //2	Fixed
  {
  tr_WtGuarantee.style.display = 'none';
  tr_RateKg.style.display = 'none';
  tr_ActualWtPayable.style.display = 'none';
  tr_ActualKms.style.display = 'none';
  tr_TruckHireCharge.style.display = 'none';
  txt_WtGuarantee.value=0;
  txt_RateKg.value=0;
  lbl_WtGuarantee.innerText=0;
  lbl_RateKg.innerText=0;
  hdn_ActualWtPayable.value=0;
  lbl_ActualWtPayableValue.innerText=0; 
  //hdn_ActualKms.value=0;
  lbl_ActualKmsValue.innerText=0;
  hdn_TruckHireCharge.value=val(txt_TruckHireCharge.value);
  }
else if(val(ddl_FreightType.value)==3)//3	Per Km
  {
  
  lbl_ActualWtPayableValue.innerText=val(txt_WtGuarantee.value); 
  hdn_ActualWtPayable.value=val(txt_WtGuarantee.value);
  hdn_TruckHireCharge.value=val(txt_RateKg.value)*val(hdn_ActualWtPayable.value);
  
   if ((val(hdn_ActualKms.value)>val(txt_WtGuarantee.value)) && (val(ddl_LHPOType.value)==1))
    {    
     hdn_ActualWtPayable.value=val(hdn_ActualKms.value);   
     lbl_ActualWtPayableValue.innerText=val(hdn_ActualKms.value); 
     hdn_TruckHireCharge.value=val(txt_RateKg.value)*val(hdn_ActualKms.value);
    }
 
    lbl_TruckHireChargeValue.innerText=val(hdn_TruckHireCharge.value);    
    txt_TruckHireCharge.value=0;
    tr_txt_TruckHireCharge.style.display='none';   
    lbl_ActualKmsValue.innerText=val(hdn_ActualKms.value);  

  }
else if(val(ddl_FreightType.value)==4)////4	Per Article
  {
  
  lbl_ActualWtPayableValue.innerText=val(txt_WtGuarantee.value); 
  hdn_ActualWtPayable.value=val(txt_WtGuarantee.value); 
       
      if ((val(hdn_TotalArticle.value)>val(txt_WtGuarantee.value)) && (val(ddl_LHPOType.value)==1))
        {
        hdn_ActualWtPayable.value=val(hdn_TotalArticle.value);   
        lbl_ActualWtPayableValue.innerText=val(hdn_TotalArticle.value); 
        }  
          
  hdn_TruckHireCharge.value=val(txt_RateKg.value)*val(hdn_ActualWtPayable.value);
  lbl_TruckHireChargeValue.innerText=val(hdn_TruckHireCharge.value); 
   
  tr_ActualKms.style.display = 'none';
  tr_txt_TruckHireCharge.style.display='none';
  //hdn_ActualKms.value=0;
  lbl_ActualKmsValue.innerText=0;  
  txt_TruckHireCharge.value=0; 

  }

CalculateTDS();
}

//====================================================================================================


function CalculateTDS()
{
lbl_TDSPerValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_TDSPerValue');
lbl_TDSPerValue1=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_TDSPerValue1');
lbl_SurchargePer=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_SurchargePer');
lbl_Addl_Surcharges_CessPer=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_Addl_Surcharges_CessPer');
lbl_Addl_Education_CessPer=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_Addl_Education_CessPer');

// Amount
lbl_TDSAmountValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_TDSAmountValue');
lbl_SurchargeAmountValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_SurchargeAmountValue');
lbl_AddlSurchargeCessAmountValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_AddlSurchargeCessAmountValue');
lbl_AddlEducationCessAmountValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_AddlEducationCessAmountValue');
lbl_ExemptionLimitAmountValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_ExemptionLimitAmountValue');
lbl_TotalTruckHireValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_TotalTruckHireValue');
lbl_TotalAfterTDSDedValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_TotalAfterTDSDedValue');
//Percentage
hdn_TDSPer=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TDSPer');
hdn_Surcharge=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_Surcharge');
hdn_Addl_Surcharges_CessPer=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_Addl_Surcharges_CessPer');
hdn_Addl_Education_Cess=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_Addl_Education_Cess');
hdn_ExemptionLimit=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_ExemptionLimit');

//Hidden Amount
hdn_TDSAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TDSAmount');
hdn_SurchargeAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_SurchargeAmount');
hdn_AddlSurchargeCessAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_AddlSurchargeCessAmount');
hdn_AddlEducationCessAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_AddlEducationCessAmount');
hdn_ExemptionLimitAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_ExemptionLimitAmount');
hdn_TDSAmountValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TDSAmountValue');

hdn_TotalTruckHire=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalTruckHire');
hdn_TruckHireCharge=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TruckHireCharge');
var txt_OtherCharges=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_OtherCharges');
hdn_OtherCharges=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_OtherCharges');
var ddl_VehicleCategory=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_VehicleCategory');
hdn_TotalAfterTDSDedValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalAfterTDSDedValue');
var txt_CharityAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_CharityAmount');
hdn_CharityAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_CharityAmount');

hdn_OtherCharges.value=val(txt_OtherCharges.value);
if(val(ddl_VehicleCategory.value)== 1)
  {
  hdn_TDSAmount.value=0;
  lbl_TDSAmountValue.innerText=0;
  hdn_SurchargeAmount.value=0;
  hdn_TDSPer.value=0;
  hdn_AddlSurchargeCessAmount.value=0;
  hdn_AddlEducationCessAmount.value=0;
  hdn_TotalAfterTDSDedValue.value=0;
  lbl_SurchargeAmountValue.innerText=0;
  lbl_Addl_Surcharges_CessPer.innerText=0;
  lbl_Addl_Education_CessPer.innerText=0;
  lbl_TDSPerValue1.innerText=0;
  lbl_SurchargePer.innerText=0;
  lbl_Addl_Surcharges_CessPer.innerText=0;
  lbl_Addl_Education_CessPer.innerText=0;
  lbl_TotalAfterTDSDedValue.innerText=0;
  
  }
else//Calculate TDS Amount on the Basis of TDS Percentage
  {
  hdn_TDSAmount.value=(val(hdn_TruckHireCharge.value) + val(hdn_OtherCharges.value)) * val(hdn_TDSPer.value)/100;
  hdn_SurchargeAmount.value=val(hdn_TDSAmount.value) * val(hdn_Surcharge.value)/100;
  hdn_AddlSurchargeCessAmount.value=val(val(hdn_TDSAmount.value) + val( hdn_SurchargeAmount.value))* val(hdn_Addl_Surcharges_CessPer.value)/100;
  hdn_AddlEducationCessAmount.value=val(val(hdn_TDSAmount.value) + val( hdn_SurchargeAmount.value))* val(hdn_Addl_Education_Cess.value)/100;
  lbl_TDSPerValue1.innerText=val(hdn_TDSAmount.value); //Added :Ankit
  lbl_SurchargeAmountValue.innerText=val(hdn_SurchargeAmount.value);
  lbl_AddlSurchargeCessAmountValue.innerText=val(hdn_AddlSurchargeCessAmount.value);
  lbl_AddlEducationCessAmountValue.innerText=val(hdn_AddlEducationCessAmount.value);
  hdn_TDSAmountValue.value = val(val(hdn_TDSAmount.value)+ val(hdn_SurchargeAmount.value)+ val(hdn_AddlSurchargeCessAmount.value)+ val(hdn_AddlEducationCessAmount.value));
  hdn_TDSAmountValue.value = Math.round(hdn_TDSAmountValue.value);
  lbl_TDSAmountValue.innerText=Math.round(hdn_TDSAmountValue.value);
  lbl_SurchargePer.innerText=Math.round(hdn_Surcharge.value);
  lbl_Addl_Surcharges_CessPer.innerText=val(hdn_Addl_Surcharges_CessPer.value);
  lbl_Addl_Education_CessPer.innerText=val(hdn_Addl_Education_Cess.value);
  }
  hdn_CharityAmount.value=val(txt_CharityAmount.value);
 hdn_TotalAfterTDSDedValue.value=val(val(hdn_TruckHireCharge.value)+ val(hdn_OtherCharges.value)-val(lbl_TDSAmountValue.innerText)); 
 lbl_TotalAfterTDSDedValue.innerText=Math.round(hdn_TotalAfterTDSDedValue.value);
hdn_TotalTruckHire.value= val(val(hdn_TruckHireCharge.value)+ val(hdn_OtherCharges.value)-val(lbl_TDSAmountValue.innerText)-val(hdn_CharityAmount.value)); 
lbl_TotalTruckHireValue.innerText=Math.round(hdn_TotalTruckHire.value);
CalculateBalanceAmount();
}
//========================================================================================================
function CheckCharityAmount()
{
  hdn_TotalAfterTDSDedValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalAfterTDSDedValue');
  var txt_CharityAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_CharityAmount');
  hdn_CharityAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_CharityAmount');
  lbl_TotalTruckHireValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_TotalTruckHireValue');
  hdn_TotalTruckHire=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalTruckHire');

  var lbl_Errors = document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_Errors');
  
  if (val(txt_CharityAmount.value) > val(hdn_TotalAfterTDSDedValue.value))
  {
      lbl_Errors.innerText = "Charity Amount Cannot Be Greater than  TotalafterTDSDeduction"; 
      txt_CharityAmount.value=0; 
      hdn_CharityAmount.value=0;      
      CalculateTDS();
      txt_CharityAmount.focus();       
      return false;
    }    

else 
    {     
         CalculateTDS();   
        return true;
    }
 
}

//====================================================================================================
function CheckTotalAdvance()
{
    
    var txt_TotalAdvanceHireDetails = document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TotalAdvance');
    var hdn_TotalAdvance = document.getElementById('WucLHPO1_WucLHPOAlertsBranches1_hdn_TotalAdvance');
    var hdn_ToalAdvanceGrid= document.getElementById('WucLHPO1_WucLHPOAlertsBranches1_hdn_ToalAdvanceGrid');
    
    var lbl_Errors = document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_Errors');
    var lbl_ErrorsAlert= document.getElementById('WucLHPO1_WucLHPOAlertsBranches1_lbl_Errors');
    //var objResource=new Resource('WucLHPO1_WucLHPOHireDetails1_hdf_ResourceString');
    
    if (val(txt_TotalAdvanceHireDetails.value)!= val(hdn_TotalAdvance.value))
    {
        
         lbl_Errors.innerText = "Total Advance Should be Same"; //objResource.GetMsg("Msg_TotalAdvance");
          txt_TotalAdvanceHireDetails.disabled=false;
         txt_TotalAdvanceHireDetails.focus();
         return false;
    }    

else 
    {      
        return true;
    }
CalculateBalanceAmount()    
  
}
function CalculateBalanceAmount()
{
hdn_TotalTruckHire=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalTruckHire');
txt_TotalAdvance=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TotalAdvance');
lbl_BalanceAmountValue=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_BalanceAmountValue');
hdn_BalanceAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_BalanceAmount');
var ddl_VehicleCategory=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_VehicleCategory'); 
var ddl_hierarchy= document.getElementById('WucLHPO1_WucLHPOHireDetails1_WucHierarchyWithID1_ddl_hierarchy'); 

if(val(ddl_VehicleCategory.value)== 1)
   {
      ddl_hierarchy.disabled=true;
      hdn_BalanceAmount.value=0;
    }  
else 
    {
    hdn_BalanceAmount.value=val(hdn_TotalTruckHire.value)-val(txt_TotalAdvance.value);   
    if (val(hdn_BalanceAmount.value) <= 0)
     {
        hdn_BalanceAmount.value=0;
      }
    else 
    {
       if(val(txt_TotalAdvance.value) > val(hdn_TotalTruckHire.value))
        {
        txt_TotalAdvance.value=val(hdn_TotalTruckHire.value);   
        } 
        hdn_BalanceAmount.value=val(hdn_TotalTruckHire.value)-val(txt_TotalAdvance.value);  
      }
   } 

lbl_BalanceAmountValue.innerText=val(hdn_BalanceAmount.value); 
}
//====================================================================================================

function SetATHPaybleAlertsBranchesTotalAdvance()
{
var txt_TotalAdvance = document.getElementById('WucLHPO1_WucLHPOAlertsBranches1_txt_TotalAdvance');
var txt_TotalAdvanceHireDetails = document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TotalAdvance');
var hdn_TotalAdvance = document.getElementById('WucLHPO1_WucLHPOAlertsBranches1_hdn_TotalAdvance');
var hdn_ToalAdvanceGrid = document.getElementById('WucLHPO1_WucLHPOAlertsBranches1_hdn_ToalAdvanceGrid');
var hdn_LHPOId=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_LHPOId');

txt_TotalAdvance.value=val(txt_TotalAdvanceHireDetails.value);
hdn_TotalAdvance.value=val(txt_TotalAdvanceHireDetails.value);

if(val(hdn_LHPOId.value)>0)
  {
  hdn_ToalAdvanceGrid.Value=val(txt_TotalAdvanceHireDetails.value);
  }
}

//====================================================================================================

function EnabledDisabledBalancePayableOnBalanceAmountChange()
{    
var lbl_Errors = document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_Errors');   
var hdn_BalanceAmount= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_BalanceAmount'); 
var hdn_ToLocation_Parameter= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_ToLocation_Parameter'); 

var ddl_hierarchy= document.getElementById('WucLHPO1_WucLHPOHireDetails1_WucHierarchyWithID1_ddl_hierarchy'); 
var ddl_location= document.getElementById('WucLHPO1_WucLHPOHireDetails1_WucHierarchyWithID1_ddl_location'); 
var lbl_location_caption=document.getElementById('WucLHPO1_WucLHPOHireDetails1_WucHierarchyWithID1_lbl_location_caption'); 
var ddl_VehicleCategory=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_VehicleCategory');

  
    if(val(hdn_ToLocation_Parameter.value)!= 1)
    {
//      ddl_hierarchy.disabled=false;  
        
        if(val(hdn_BalanceAmount.value)<=0)
        {
          if(ddl_hierarchy.value=='HO' )
            {
                ddl_hierarchy.value='0';
                ddl_hierarchy.selectedIndex=0;
                ddl_hierarchy.disabled=true;  
            }
          else if (ddl_hierarchy.value=='AO' || ddl_hierarchy.value=='BO' || ddl_hierarchy.value=='RO')
            {
                ddl_hierarchy.value='0';     
                ddl_hierarchy.selectedIndex=0;
                ddl_hierarchy.disabled=true;      
                ddl_location.style.display = 'none';
                lbl_location_caption.style.display = 'none';
            }   
          else if(ddl_hierarchy.value=='0' )
            {
                ddl_hierarchy.value='0';
                ddl_hierarchy.selectedIndex=0;
                ddl_hierarchy.disabled=true;
                if(ddl_location!=null)
                  {
                      ddl_location.style.display = 'none';
                      lbl_location_caption.style.display = 'none';
                  }
            }
        }
        else
        {
          ddl_hierarchy.disabled=false;
        }
    }
    if (ddl_VehicleCategory.value == 1)
  {
    ddl_hierarchy.disabled=true;
  }
}

//====================================================================================================

function CalculateOtherPayable()
{
var hdn_TotalCrossingCost= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalCrossingCost');
var hdn_TotalDeliveryCommision= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalDeliveryCommision');
var hdn_TotalToPayCollection= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalToPayCollection');
var hdn_NetAmount= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_NetAmount');
var hdn_ToPayCollection= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_ToPayCollection');
var hdn_TotalPayable= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TotalPayable');

var lbl_NetAmountValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_NetAmountValue');
var lbl_ToPayCollectionValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_ToPayCollectionValue');
var lbl_TotalPayableValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_TotalPayableValue');
var lbl_DeliveryCommissionValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_DeliveryCommissionValue');
var lbl_CrossingCostValue= document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_CrossingCostValue');
var txt_Others= document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_Others');

lbl_CrossingCostValue.innerText=val(hdn_TotalCrossingCost.value); 
lbl_DeliveryCommissionValue.innerText=val(hdn_TotalDeliveryCommision.value);
lbl_ToPayCollectionValue.innerText=val(hdn_TotalToPayCollection.value);

hdn_TotalPayable.value= val(hdn_TotalCrossingCost.value) + val(hdn_TotalDeliveryCommision.value) + val(txt_Others.value);
lbl_TotalPayableValue.innerText=val(hdn_TotalPayable.value);

hdn_NetAmount.value=val(hdn_TotalPayable.value)-val(hdn_TotalToPayCollection.value);
lbl_NetAmountValue.innerText=val(hdn_NetAmount.value); 
}

//====================================================================================================


function UpdateLHPOdetails(Other_Charges)
{
var OtherCharges;
var txt_OtherCharges=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_OtherCharges');
var hdn_OtherCharges=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_OtherCharges'); 
if(Other_Charges != 0)
{
    txt_OtherCharges.value = val(Other_Charges);
    hdn_OtherCharges.value=val(Other_Charges);  
}
else
{
   hdn_OtherCharges.value = Other_Charges;
   txt_OtherCharges.value=val(Other_Charges);
}
CalculateTDS(); 
}

//====================================================================================================
function onload()
{
var hdn_is_page_loaded=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_is_page_loaded');
hdn_is_page_loaded.value = '1';
}
//======================================================================================================
function validateadvanceamt()
{
var return_Value;
hdn_TruckHireCharge=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TruckHireCharge');
hdn_Max_Advance_Percent_Of_Vehicle_hire_Charge=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_Max_Advance_Percent_Of_Vehicle_hire_Charge');
 var txt_TotalAdvanceHireDetails = document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TotalAdvance');
 var lbl_Errors = document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_Errors'); 
 var MaxAdvanceAmt=0;
//var truck_hire_charge = val(lbl_truck_hire) or val(txt_truck_hire);
// max_percent_of_truck_hire_charge.value = val(hdn_TruckHireCharge) * val(hdn_Max_Advance_Percent_Of_Vehicle_hire_Charge/100);
//var max_percent_of_truck_hire_charge = max_percent_of_truck_hire_charge_percent / 100;

var MaxAdvanceAmt= val(hdn_TruckHireCharge.value) * val(hdn_Max_Advance_Percent_Of_Vehicle_hire_Charge.value);
MaxAdvanceAmt=MaxAdvanceAmt/100;
// MaxAdvanceAmt = val(hdn_TruckHireCharge.value) - Client_percentage_Charge_Of_Truck_HireCharge;
  // alert('b');
   if (val(txt_TotalAdvanceHireDetails.value) > MaxAdvanceAmt)
    {
      lbl_Errors.innerText="Advance Amount Should not be Greater than " + " " + val(hdn_Max_Advance_Percent_Of_Vehicle_hire_Charge.value ) + "%" + " " + "i.e" +"  " +  MaxAdvanceAmt +" "+  "Of Truck Hire Charge";
      return_Value=false;
    }
    else
    {
     return_Value=true;
    }
    return return_Value;
}

function OpenPopup(MenuItemId,keyID)
{
    var ddl_LHPOType = document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_LHPOType');       
    var hdn_Mode = document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_Mode');
    
    if (ddl_LHPOType.value == 1)
    {
       var btnCanEdit = 1;           
    }
    else       
    {
     var btnCanEdit = 0 ;
    } 
    
    if (hdn_Mode.value == 4)
    {
      var btnCanEdit= 0;
    }        
    
    var Path ='';
    Path='../Outward/FrmLHPOOtherCharges.aspx?Menu_Item_ID=' + MenuItemId + '&Id=' + keyID + '&btnCanEdit=' + btnCanEdit
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-200);
    var popH = (h-200);
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
            
    window.open(Path, 'MainPopUp1', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
    return false;
}
    
    
function Add_Driver_Window()
{
var hdn_Driver_path = document.getElementById('hdnDriverpath');

var w = screen.availWidth;
var h = screen.availHeight;
var popW = (w-100);
var popH = (h-100);
var leftPos = (w-popW)/2;
var topPos = (h-popH)/2;

if(hdn_Driver_path.value == '')
  {
  alert('You Don"t Have Rights to Add Driver');
  }
else
  {
  window.open(hdn_Driver_path.value, 'memo', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
  return false;
  }

return false;
}


function EnableDisableTerminatedLHCControl()
{
  var Chk_LHCTerminatedByReceivingCash=document.getElementById('WucLHPO1_WucLHPOHireDetails1_Chk_LHCTerminatedByReceivingCash');
  var lbl_ReceivedCashAmt=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_ReceivedCashAmt');
  var txt_ReceivedAmtTerminatedLHC=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_ReceivedAmtTerminatedLHC');
  
  
  if (Chk_LHCTerminatedByReceivingCash.checked == true)

  {
     lbl_ReceivedCashAmt.style.display='inline';
     txt_ReceivedAmtTerminatedLHC.style.display='inline';
  }
  else
  {
    lbl_ReceivedCashAmt.style.display='none';
     txt_ReceivedAmtTerminatedLHC.style.display='none';
   }
  
}
function EnableDisableLedegerTerminated()
{
  var Chk_LHCTerminatedByDebit=document.getElementById('WucLHPO1_WucLHPOHireDetails1_Chk_LHCTerminatedByDebit');
 var lbl_LedgerId=document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_LedgerId');
  var ddl_LHCTermiantedByDebitToLedger=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_LHCTermiantedByDebitToLedger_txtBoxddl_LHCTermiantedByDebitToLedger');
  var td_ddl_LHCTermiantedByDebitToLedger=document.getElementById('WucLHPO1_WucLHPOHireDetails1_td_ddl_LHCTermiantedByDebitToLedger');
  if (Chk_LHCTerminatedByDebit.checked == true)
  {
     lbl_LedgerId.style.display='inline';
     ddl_LHCTermiantedByDebitToLedger.style.display='inline';
     td_ddl_LHCTermiantedByDebitToLedger.style.display='inline';
  }
  else
  {
     lbl_LedgerId.style.display='none';
     ddl_LHCTermiantedByDebitToLedger.style.display='none';
     td_ddl_LHCTermiantedByDebitToLedger.style.display='none';
  }
}