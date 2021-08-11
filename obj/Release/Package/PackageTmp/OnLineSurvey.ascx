<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnLineSurvey.ascx.cs"
    Inherits="Survey.OnLineSurvey" EnableViewState="true" %>

<%--<script type="text/JavaScript" src="https://cdn.bootcdn.net/ajax/libs/jquery.fileDownload/1.4.2/jquery.fileDownload.js"></script>--%>
<script type="text/JavaScript" src="js/jquery-ui.js"></script>
<script type="text/JavaScript" src="js/json2.js"></script>
<script type="text/JavaScript" src="js/jquery.blockUI.js"></script>
<script type="text/JavaScript" src="js/jquery.fileDownload.js"></script>
<script type="text/javascript">
    
    function chkChange() {
        var controlValue = $("#chkAgree").prop('checked');
        if (controlValue) {
            $("input[name*=btnAgree]").removeAttr("disabled");
			$("input[name*=btnAgree]").attr("style","width: 126px; height: 32px; background-color: #0082C9; font-size: 14px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.57; letter-spacing: normal; text-align: center; color: #FFFFFF;");
        }
        else {
            $("input[name*=btnAgree]").attr('disabled', "true");
			$("input[name*=btnAgree]").attr("style","width: 126px; height: 32px; background-color: var(--color-grey-grey-30); font-size: 14px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.57; letter-spacing: normal; text-align: center; color: var(--color-grey-black-25);");
        }
    }

    function setButtonState(hasData, SurveyId, UserId) {
        if (hasData) {
            $("input[name*=btnDownLoadFile]").removeAttr("disabled");
			$("input[name*=btnDownLoadFile]").attr("style","width: 126px; height: 32px; background-color: #0082C9; font-size: 14px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.57; letter-spacing: normal; text-align: center; color: #FFFFFF;");
       
        }
        else {
            $("input[name*=btnDownLoadFile]").attr('disabled', "true");
			$("input[name*=btnDownLoadFile]").attr("style","width: 126px; height: 32px; background-color: var(--color-grey-grey-30); font-family: SourceHanSansTC; font-size: 14px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.57; letter-spacing: normal; text-align: center; color: var(--color-grey-black-25);");
       
        }

        //$("input[name*=btnDownLoadFile]").removeAttr("disabled");
        $('#txtSurveyId').val(SurveyId.toString()); 
        $('#txtUserId').val(UserId.toString()); 
    }  


    function closeblock(){
        $.unblockUI();
    }

    function yesBlock() {
        $.blockUI({ message: "" });
        exportxls();
    }

    function showMsg() {
        
        var retValue = window.showModalDialog("DownloadFileMsg.aspx?surveyID=" + $('#txtSurveyId').val() + "&userID=" + $('#txtUserId').val(), "", "dialogHeight:200px");
        if (1 == retValue) {
            return true;
        }
        else {
            return false;
        }
    }
    jQuery.support.cors = true;

    function callCS() {
        var retValue = window.showModalDialog("DownloadFileMsg.aspx", "", '');
        if (1 == retValue) {
            alert(retValue);
            return true;
        }
        else {
            alert(retValue);
            return false;
        }
    }

    function openChrome() {
        try {
            var objShell = new ActiveXObject("WScript.Shell");
            objShell.Run($("input[name*=txtBatPath]").val() + "shellQ.bat", 0, true);
        }
        catch (e) {
            var retValue = window.showModalDialog("DownloadShellQMsg.aspx", "dialogHeight:200px");
        }
        
    }

</script>

<table style="width: 100%; height: 100%;">
    <tbody>
        <tr id="tr1" runat="server">
            <td>
                <table style="width: 100%; height: 100%; background-color: #f0f8ff; border: solid 1px #7da8d4;">
                    <tr>
                        <td style="height: 40px; padding-left: 24px; padding-top: 16px;">
                            <label id="lblTitle1" style="width: 131px; height: 24px; font-family: SourceHanSansTC; font-size: 16px; font-weight: 500; font-stretch: normal; font-style: normal; line-height: normal; letter-spacing: 0.32px; color: var(--black-black-1);">
                                個資宣導注意事項</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 60px; padding-left: 24px; padding-top: 8px;">
                            <label id="lblContent1" style="width: 400px; height: 60px; font-family: SourceHanSansTC; font-size: 12px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.67; letter-spacing: normal; color: var(--black-black-1);">
                                問卷的製作將涉及個人資料的處理及利用，使用人以確實了解個人資料法保<br />
                                護法，中華電信股份有限公司個人資料保護管理規範及本公司相關規定，如<br />
                                有違反或外洩，相依法負起相關法律責任。 
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 40px; padding-left: 24px;">
                            <input type="checkbox" id="chkAgree" style="width: 16px; border-radius: 50%;" onclick="chkChange();" />
                            <label id="lblAgree" style="width: 84px; height: 20px; font-family: PingFangTC; font-size: 12px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.67; letter-spacing: normal; color: var(--color-grey-black-65);">
                                我同意上述事項
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 40px; padding-left: 24px;">
                            <asp:Button id="btnAgree" Text="確認" runat="server"  Disabled="true" 
                                style="width: 126px; height: 32px; background-color: var(--color-grey-grey-30); font-size: 14px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.57; letter-spacing: normal; text-align: center; color: var(--color-grey-black-25);" OnClick="btnAgree_Click"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: auto;"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="tr2" runat="server" visible="false" >
            <td>
                <table style="width: 100%; height: 100%; background-color: #f0f8ff; border: solid 1px #7da8d4;">
                    <tr>
                        <td style="height: 40px; padding-left: 24px; padding-top: 26px;">
                            <label id="lblTitle2" style="width: 107px; height: 24px; font-family: SourceHanSansTC; font-size: 16px; font-weight: 500; font-stretch: normal; font-style: normal; line-height: normal; letter-spacing: 0.32px; color: var(--black-black-1);">
                                CRM問卷平台</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 80px; padding-left: 24px; padding-top: 8px;">
                            <label id="lblContent2" style="width: 406px; height: 80px; font-family: SourceHanSansTC; font-size: 12px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.67; letter-spacing: normal; color: var(--black-black-1);">
                                此產品提供給CRM行銷人員使用，提供問卷範本、問卷設計、收集方式、即<br />
                                時成效分析等功能，行銷人員透過線上問卷調查，取得客戶廣泛又即時的回<br />
                                饋意見。<br />
                                為提供穩定品質與最佳操作體驗。建議使用Google Chrome瀏覽器瀏覽。</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 40px; padding-left: 24px; padding-top: 25px;">
                            
                           <input type="button" id="btnToSurvey" value="立即前往CRM問卷平台" style="width: 195px; height: 32px; background-color: #0082C9; font-family: SourceHanSansTC; font-size: 14px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.57; letter-spacing: normal; text-align: center; color: #FFFFFF;"
                               onclick="openChrome()" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 60px; padding-left: 24px; padding-top: 8px;">
                            <label id="lblContent3" style="width: 406px; height: 60px; opacity: 0.85; font-family: SourceHanSansTC; font-size: 12px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.67; letter-spacing: normal; color: var(--black-black-1);">
                                電腦需先安裝ShellQ.bat檔，點選上方按鈕方可自動開啟Chrome瀏覽器 
                                <a href="https://cht365-my.sharepoint.com/:u:/g/personal/anitachen_cht_com_tw/EZhEMMQojtJFpzsSH7eWE1EBPeo5ARujWPwWYSZQr1W6TQ?e=Q7NvLp" target="_blank" >下載安裝檔</a> <br />
                                或手動登入：複製問卷平台網址 <font color="#0091FF">https://ecrmsurvey.cht.com.tw</font> 貼到Chrome瀏覽器登入
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sec bar" style="padding-top: 20px; padding-left: 24px;"></td>
                    </tr>
                    <tr>
                        <td style="padding-top: 10px; padding-left: 24px; height: 35px;">
                            <label id="lblTitle3" style="width: 335px; height: 24px; font-family: SourceHanSansTC; font-size: 16px; font-weight: 500; font-stretch: normal; font-style: normal; line-height: normal; letter-spacing: 0.32px; color: var(--black-black-1);">
                                問卷操作流程說明</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 24px; height: 20px;">
                            <label style="width: 335px; height: 20px; font-family: SourceHanSansTC; font-size: 12px; font-weight: 500; font-stretch: normal; font-style: normal; line-height: 1.67; letter-spacing: normal; color: rgb(0,130,201);font-weight:bold;">
                                區分CRM問卷平台與CRM行銷平台各自負責的功能</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 24px; height: 303px; padding-top: 25px;">
                            <img src="img/user_flow_CRM.png" style="width:90%"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="sec bar" style="padding-left: 24px; padding-top: 25px;"></td>
                    </tr>
                    <tr>
                        <td style="padding-left: 24px; padding-top: 25px; height: 40px;">
                            <label style="width: 335px; height: 24px; font-family: SourceHanSansTC; font-size: 16px; font-weight: 500; font-stretch: normal; font-style: normal; line-height: normal; letter-spacing: 0.32px; color: var(--black-black-1);">
                                問卷網址</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 24px; padding-top: 10px; " class="auto-style1">
                             <label style="width: 60px; height: 20px; font-family: SourceHanSansTC; font-size: 12px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.67; letter-spacing: normal; color: var(--black-black-1);">
                                正式網址-</label>
                             <asp:Label ID="lnkFinal" runat="server" Font-Size="12px" ForeColor="#0091FF"></asp:Label>
                            <br />
                            <label style="width: 60px; height: 20px; font-family: SourceHanSansTC; font-size: 12px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.67; letter-spacing: normal; color: var(--black-black-1);">
                                測試網址-</label>
                             <asp:Label ID="lnkTest" runat="server" Font-Size="12px" ForeColor="#0091FF"></asp:Label>
                        </td>

                    </tr>
                     <tr>
                        <td class="sec bar" style="padding-left: 24px; padding-top: 25px;"></td>
                    </tr>
                     <tr>
                        <td style="padding-left: 24px; padding-top: 25px; " class="auto-style1">
                            <label style="width: 335px; height: 24px; font-family: SourceHanSansTC; font-size: 16px; font-weight: 500; font-stretch: normal; font-style: normal; line-height: normal; letter-spacing: 0.32px; color: var(--black-black-1);">
                                QR Code網址</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 24px; padding-top: 10px; height: 40px;">
                             <label style="width: 60px; height: 20px; font-family: SourceHanSansTC; font-size: 12px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.67; letter-spacing: normal; color: var(--black-black-1);">
                                正式網址-</label>
                             <asp:Label ID="lnkFinalQRCode" runat="server" Font-Size="12px" ForeColor="#0091FF"></asp:Label>
                            <br />
                            <label style="width: 60px; height: 20px; font-family: SourceHanSansTC; font-size: 12px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.67; letter-spacing: normal; color: var(--black-black-1);">
                                測試網址-</label>
                             <asp:Label ID="lnkTestQRCode" runat="server" Font-Size="12px" ForeColor="#0091FF"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sec bar" style="padding-left: 24px; padding-top: 25px;"></td>
                    </tr>
                    <tr>
                        <td style="padding-left: 24px; padding-top: 25px; height: 40px;">
                            <label style="width: 335px; height: 24px; font-family: SourceHanSansTC; font-size: 16px; font-weight: 500; font-stretch: normal; font-style: normal; line-height: normal; letter-spacing: 0.32px; color: var(--black-black-1);">
                                下載問卷原始資料</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 24px; padding-top: 10px; height: 40px; padding-bottom: 100px;">
                            <asp:Button id="btnDownLoadFile" runat="server" Text="下載" OnClientClick="return showMsg();" OnClick="btnDownLoadFile_Click"
                                style="width: 126px; height: 32px; background-color: var(--color-grey-grey-30); font-family: SourceHanSansTC; font-size: 14px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.57; letter-spacing: normal; text-align: center; color: var(--color-grey-black-25);" />
                            
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </tbody>
</table>
<input id="txtSurveyId" type="hidden"/>
<input id="txtUserId" type="hidden"/>
<input id="txtTitle" type="hidden"/>
<input id="txtSurveyHost" type="hidden"/>
<input id="txtBatPath" runat="server" type="hidden"/>
