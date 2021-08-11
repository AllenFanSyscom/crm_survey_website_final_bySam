<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestWebApi.aspx.cs" Inherits="Survey.TestWebApi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="js/jquery-New.js"></script>
    <script type="text/javascript">
        function ClearInput() {
            $("input[name*=txtUserId]").val('');
            $("input[name*=txtSurveyID]").val('');
            $("input[name*=txtTile]").val('');
            $("input[name*=txtData]").val('');
        }

        function ClearOutput() {
            $("input[name*=txtToken]").val('');
            $("textarea[name*=txtErrorMsg]").val('');
            $("inout[name*=txtBaseData]").val('');
            
        }
        

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="button" id="btnClearInput" onclick="ClearInput();" value="清除input" />&nbsp&nbsp;&nbsp;
            <input type="button" id="btnClearOutput" onclick="ClearOutput();" value="清除輸出結果" />
            <br /><br />
            UserID: <asp:TextBox ID="txtUserId" runat="server" Text="C1C45678-C8D6-EA11-9731-00155D13601B"></asp:TextBox>
            <br /><br />
            Token: <asp:TextBox ID="txtToken" runat="server"></asp:TextBox>
            <br/>
            <asp:Button ID="btnGetToken" runat="server" Text="GetToken over HTTP" OnClick="btnGetToken_Click"/>
            &nbsp;&nbsp;
            <asp:Button ID="btnGetTokenWithHTTPS" runat="server" Text="GetToken over HTTPS" OnClick="btnGetTokenWithHTTPS_Click" />
            &nbsp;&nbsp;
            <asp:Button id="btnTokenAuth" runat="server" Text="TokenAuth over HTTP" OnClick="btnTokenAuth_Click"/>
            <asp:Button id="btnTokenAuthWithHttps" runat="server" Text="TokenAuth over HTTPS" OnClick="btnTokenAuthWithHttps_Click"/>
        </div>
        <p>
            <asp:TextBox ID="TextBox1" runat="server" Columns="3" Width="300px" TextMode="MultiLine"  Visible="false"></asp:TextBox>
        </p>
        <br/ ><br/ >
        SurveyID: <asp:TextBox ID="txtSurveyID" runat="server"  Width="300px" Text="88888888-0000-0000-0000-000000000005"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        window top:<asp:TextBox ID="txtTop" runat="server" Text="200"></asp:TextBox>
        window left:<asp:TextBox ID="txtLeft" runat="server" Text="200"></asp:TextBox>
        <br/>
        <asp:Button id="btnGetURL" runat="server" OnClick="btnGetURL_Click" Text="取得網址 over HTTP"/>
        <asp:Button id="btnGetURLWithHTTPS" runat="server" Text="取得網址 over HTTPS" OnClick="btnGetURLWithHTTPS_Click" />
        <asp:Button id="btnClearUrl" runat="server" Text="清除網址" OnClick="btnClearUrl_Click"/>
        <asp:Button id="btnDownloadFile" runat="server" Text="下載檔案 over HTTP" OnClick="btnDownloadFile_Click" />
        <asp:Button id="btnDownloadFileWithHTTPS" runat="server" Text="下載檔案 over HTTPS" OnClick="btnDownloadFileWithHTTPS_Click" />
        <asp:Button id="btnProposalSurvey" runat="server" Text="行銷活動方式陳核" OnClick="btnProposalSurvey_Click"/>
        <br />
        <label style="width: 60px; height: 20px; font-family: SourceHanSansTC; font-size: 12px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.67; letter-spacing: normal; color: var(--black-black-1);">
        正式網址-</label>
        <asp:HyperLink ID="lnkFinal" runat="server"></asp:HyperLink>
        <br />
        <label style="width: 60px; height: 20px; font-family: SourceHanSansTC; font-size: 12px; font-weight: normal; font-stretch: normal; font-style: normal; line-height: 1.67; letter-spacing: normal; color: var(--black-black-1);">
         測試網址-</label>
         <asp:HyperLink ID="lnkTest" runat="server"></asp:HyperLink>
        <br /><br />
        <asp:Button id="btnCreateSurvey" runat="server" Text="產生問卷 over HTTP" OnClick="btnCreateSurvey_Click"/>
        &nbsp;&nbsp;&nbsp;
        <asp:Button id="btnCreateSurveyWithHTTPS" runat="server" Text="產生問卷 over HTTPS" OnClick="btnCreateSurveyWithHTTPS_Click"/>
        <br /><br />
        <asp:TextBox ID="txtErrorMsg" TextMode="MultiLine" Rows="3" Width="300px" runat="server"></asp:TextBox>
        <br /><br />
        Data: <asp:TextBox ID="txtData" runat="server" with="100px"></asp:TextBox> &nbsp;&nbsp;
        Base Data: <asp:TextBox ID="txtBaseData" runat="server" with="100px"></asp:TextBox>
        <br />
        <asp:Button id="btnBase64" runat="server" Text="Base64 Encode" OnClick="btnBase64_Click"/>
        <br /><br />
        <asp:Button id="btnOpenChrome" runat="server" Text="開啟Chrome" OnClick="btnOpenChrome_Click"/>
        
    </form>
</body>
</html>
