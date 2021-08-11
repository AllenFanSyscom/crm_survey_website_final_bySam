<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownloadShellQMsg.aspx.cs" Inherits="Survey.DownloadShellQMsg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">

        function GoDownload() {
            window.location.replace("https://cht365-my.sharepoint.com/:u:/g/personal/anitachen_cht_com_tw/EZhEMMQojtJFpzsSH7eWE1EBPeo5ARujWPwWYSZQr1W6TQ?e=Q7NvLp");
            window.close();
        }

        function NoDownload() {
            window.close();
        }

        function Resize_dialog(w, h) {
            window.center = "Yes";
            window.dialogHeight = h + "px";
            window.dialogWidth = w + "px";

        }
    </script>
</head>
<body onload=Resize_dialog(370,200)>
    <form id="form1" runat="server">
		<div id="Waring" style="cursor: default">
        <table style="width: 100%;height: 150px;">
            <tr style="height:30px;width:100%">
                <td colspan="2" style="padding-left:10px;padding-top:10px;font-family:SourceHanSansTC;font-size:14px;color:#15428B;text-align:left;">跳轉 CRM 問卷平台不成功</td>
            </tr>
            <tr style="height:40px;padding-top:0px;width:100%">
                <td rowspan="2" style="text-align:center;padding-left:25px;padding-top:0px;"><img src="img/ico/icon-waring.png" style="width:36px;height:36px;" /></td>
                <td style="font-family:SourceHanSansTC;font-size:12px;text-align:left;">
                    您需要安裝 ShellQ.bat，才能成功跳轉 CRM 問卷平台，請下載安裝步驟簡報與執行檔。
                </td>
            </tr>
            <tr style="height:30px;width:100%;">
                <td colspan="2" style="100%;vertical-align:bottom;text-align:right;padding-top:10px;padding-right:35px;">
                <input type="button" id="btnYes" value="確定" style="width: 84px; height: 24px;font-family:SourceHanSansTC;font-size:12px;" onclick="NoDownload()" />
                <input type="button" id="btnDownload" value="下載" style="width: 84px; height: 24px;font-family:SourceHanSansTC;font-size:12px;" onclick="GoDownload()" />
				</td>
            </tr>
        </table>
     </div>
    </form>
</body>
</html>
