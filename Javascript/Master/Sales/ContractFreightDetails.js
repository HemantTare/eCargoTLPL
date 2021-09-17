// JScript File

function OpenPopUp(Url)
{
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-400);
    var popH = (h-200);
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;

    window.open(Url, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')

}