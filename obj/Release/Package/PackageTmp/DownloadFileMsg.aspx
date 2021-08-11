<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownloadFileMsg.aspx.cs" Inherits="Survey.DownloadFileMsg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        //接收父視窗傳值

        //返回父視窗，並回傳值
        function GoDownload() {
            //alert(document.getElementById("txtChild").value);
            window.returnValue = 1;
            window.close();
        }

        function NoDownload() {
            //alert(document.getElementById("txtChild").value);
            window.returnValue = 0;
            window.close();
        }
		function Resize_dialog(w,h)
		{
			window.center="Yes";
			window.dialogHeight=h+"px";
			window.dialogWidth=w+"px";
			
		}
    </script>
</head>
<body onload=Resize_dialog(370,200)> 
    <form id="form1" runat="server">
		<div id="divHasData" style="cursor: default" runat="server" visible="false">
        <table style="width: 100%;height: 150px;">
            <tr style="height:30px;width:100%">
                <td colspan="2" style="padding-left:10px;padding-top:10px;font-family:SourceHanSansTC;font-size:14px;color:#15428B;text-align:left;">問卷結果原始資料下載</td>
            </tr>
            <tr style="height:40px;padding-top:0px;width:100%">
                <td rowspan="2" style="text-align:center;padding-left:25px;padding-top:0px;"><img src="img/ico/icon-waring.png" style="width:36px;height:36px;" /></td>
                <td style="font-family:SourceHanSansTC;font-size:12px;text-align:left;">
                    在有限期限內可以進行多次匯出，每次成功的匯出<br />
                    皆會產生【資料銷毀申請表】，而每個銷毀申請表<br />
                    都必須進行銷毀回報。
                </td>

            </tr>
            <tr style="height:30px;width:100%;">
                <td style="font-family:SourceHanSansTC;font-size:12px;text-align:left;">
                    請從【行銷活動】中左側工具列的【名單匯出申請<br />
                    表】填寫【資料銷毀申請表】，以符合個資規範。
                </td>
            </tr>
            <tr style="height:30px;width:100%;">
                    <td colspan="2" style="100%;vertical-align:bottom;text-align:right;padding-top:10px;padding-right:35px;">
                    <input type="button" id="btnYes" value="取消" style="width: 84px; height: 24px;font-family:SourceHanSansTC;font-size:12px;" onclick="NoDownload()" />
                    <input type="button" id="btnYes" value="下載" style="width: 84px; height: 24px;font-family:SourceHanSansTC;font-size:12px;" onclick="GoDownload()" />
					</td>

            </tr>
        </table>
     </div>
     <div id="divNodata" style="cursor: default" runat="server" visible="false">
        <table style="width: 100%;height: 150px;">
            <tr style="height:30px;width:100%">
                <td colspan="2" style="padding-left:10px;padding-top:10px;font-family:SourceHanSansTC;font-size:14px;color:#15428B;text-align:left;">問卷結果原始資料下載</td>
            </tr>
             <tr style="height:70px;padding-top:20px;width:100%">
                <td rowspan="2" style="text-align:center;text-valign:center;padding-left:25px;padding-top:0px;"><img src="img/ico/icon-waring.png" style="width:36px;height:36px;" /></td>
                <td style="font-family:SourceHanSansTC;font-size:12px;text-align:left;">
                    <br />目前沒有收集到資料
                </td>
            </tr>
            <tr style="height:30px;width:100%;">
                <td colspan="2" style="100%;vertical-align:bottom;text-align:right;padding-top:10px;padding-right:35px;">
                    <input type="button" id="btnYes" value="確定" style="width: 84px; height: 24px;font-family:SourceHanSansTC;font-size:12px;" onclick="NoDownload()" />
				</td>

            </tr>
        </table>
     </div>
    </form>
</body>
</html>
