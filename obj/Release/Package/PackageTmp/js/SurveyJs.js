/* Project : OnlineSurvey Javascript.
Requirement: Jquery 1.3.2+ Version
*/

var nWin = null;
function dialgGetFocus() {
    if (window.nWin) nWin.focus();
}

function GenFckDialog(ShId, HfId) {
    try {

        nWin = window.open("FckEditor.aspx?ShowId=" + ShId + "&SourceId=" + HfId, 'newWin', 'modal=yes,width=500,height=400,resizable=yes,scrollbars=no,modal=yes,alwaysRaised', this.focus);
        //window.showModalDialog("FckEditor.aspx?ShowId=" + ShId + "&SourceId=" + HfId, window, "status:false;dialogWidth:600px;dialogHeight:700px;dialogLeft:50px;dialogTop:300px");
        nWin.focus();

        window.onfocus = dialgGetFocus;
        document.onmousedown = dialgGetFocus;
    }
    catch (e) {
        //for firefox

        alert(e);
        alert('不支援IE以外的瀏覽器喔!');
    }
}

//清除進階編輯文字區域
function ClearTxt(ShId, HfId) {

    var IsClear = confirm('是否真的要清除文字設定並切換為純文字編輯模式？ \n\n ※清空後仍須按下「儲存」才會將新的設定值存入。');

    if (IsClear) {//var ShId = '<%=this.txtContent.ClientID %>';
        //var id3 = '<%=this.txtHidden.ClientID %>';
        document.getElementById(ShId).value = '';
        document.getElementById(ShId).disabled = false;

        if (document.getElementById(HfId).value != null) {
            document.getElementById(HfId).value = '';
        }
    }
    else { }
}

function RemoveHTML(strText) {

    var regEx = /<[^>]*>/g;

    return strText.replace(regEx, "");

}

function SetTextBox(ShId, HfId, htmlContent) {
    //原本程式,請修正成下面程式
    //document.getElementById("TextBox1").value = str;    
    //如果遇到有套MasterPage,上面的程式會死掉,感謝Allen大大的提醒            

    var text = RemoveHTML(htmlContent);
    if (text.length != 0) {
        if (text.length >= 10) {
            document.getElementById(ShId).value = text.substring(0, 9) + "...\n請點擊右側「進階文字編輯」鈕進行修改";
        }
        else {
            document.getElementById(ShId).value = text.substring(0, text.length - 1) + "...\n請點擊右側「進階文字編輯」鈕進行修改";
        }
        document.getElementById(ShId).disabled = true;
        document.getElementById(HfId).value = htmlEnc(htmlContent);
    }
    else {
        document.getElementById(ShId).value = "";
        document.getElementById(HfId).value = "";
    }
   
    document.onmousedown = "";
    window.onfocus = "";
}

//html Encode
function htmlEnc(s) {
    var div = document.createElement('div');
    div.appendChild(document.createTextNode(s));
    return div.innerHTML;
}

//html Decode
function htmldecode(s) {
    var div = document.createElement('div');
    div.innerHTML = s;
    return div.innerText || div.textContent;
}

function ContentBlurHandler(ShId, HfId) {
    document.getElementById(HfId).value = document.getElementById(ShId).value;
}

function TogleDisplay(ObjId, IsVisble) {

    if (IsVisble == false) { document.getElementById(ObjId).style.display = "none"; }
    else
    { document.getElementById(ObjId).style.display = "block"; }
}
function TogleEnable(ObjId, IsEnable) {
    document.getElementById(ObjId).disabled = IsEnable;
}