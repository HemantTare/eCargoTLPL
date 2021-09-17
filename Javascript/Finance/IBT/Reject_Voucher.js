//Created : ANkit champaneriya
//date    : 15/12/08

function openCostCentre(ledger_id,ledger_name,amount)
{
    var Path='FrmVoucherCostCentreDetailsIBT.aspx?ledgerName=' + ledger_name + '&ledgerId=' + ledger_id + '&Amount=' + amount;
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-100);
    var popH = 400;
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
    window.open(Path, 'CostCentre', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
    return false;    
}

function openBillWiseDetails(voucher_id,voucher_Date,ledger_id,ledger_name,amount)
{
    var Path='FrmIBTBillWiseDetails.aspx?ledgerName=' + ledger_name + '&ledgerId=' + ledger_id + '&Amount=' + amount + '&voucherId=' + voucher_id + '&voucherDate=' + voucher_Date + '&isBranchLedger=' + '0' ;
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-100);
    var popH = 400;
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
    window.open(Path, 'BillByBill', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
    return false;    
}


function openVoucherView(IsApproveVoucher,VoucherId)
{
    var Path='FrmVoucherView.aspx?IsApproveVoucher=' + IsApproveVoucher+ '&VoucherId=' + VoucherId  ;
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-100);
    var popH = 400;
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
    window.open(Path, 'VoucherView', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
    return false;    
}

