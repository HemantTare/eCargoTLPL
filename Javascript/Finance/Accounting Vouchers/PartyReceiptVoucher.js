// JScript File

function Calculate_AdjustedAmount(TextBox)
{
    var grid = document.getElementById('WucPartyRecieptVoucher1_dg_BillGrid');
    var row = TextBox.parentElement.parentElement;
    
    txt_TDSAmount= row.cells[6].getElementsByTagName('input');
    txt_FrieghtAmount= row.cells[7].getElementsByTagName('input');
    txt_ReceivedAmount= row.cells[5].getElementsByTagName('input');
    lbl_AdjustedAmount = row.cells[8].getElementsByTagName('span');
    var amount = 0;
    amount = val(txt_TDSAmount[0].value) + val(txt_FrieghtAmount[0].value) + val(txt_ReceivedAmount[0].value)
    lbl_AdjustedAmount[0].innerText = roundNumber(amount,2);

}

  
function SetBillValues(text,value)
{

//    if(text == '' || value = 'undefined') return ;

//alert(text)
//alert(value)


    var hdf_GridControlID = document.getElementById('WucPartyRecieptVoucher1_hdf_GridControlID');
    var arr = hdf_GridControlID.value.split('Ö');
    
    var splitted = value.split('Ö');
    
    var lbl_BillDate = document.getElementById(arr[0]);
    var txt_BillAmount = document.getElementById(arr[1]);   
    var lbl_PendingAmount = document.getElementById(arr[2]);
    var lbl_AdjustmentAmount = document.getElementById(arr[3]);
    
//    alert(splitted.length);
    if(splitted.length >1)  
    {
    
        lbl_PendingAmount.innerText = splitted[1];
        txt_BillAmount.value = splitted[2];
        lbl_BillDate.innerText = splitted[3];
    }
    else
    {
        lbl_PendingAmount.innerText = '0.00';
        txt_BillAmount.value = '0.00';
        lbl_BillDate.innerText = '';
    }
    
//    alert(splitted[1]);
//    alert(splitted[2]);
//    alert(splitted[3]);

}

