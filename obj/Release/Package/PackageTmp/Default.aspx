<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MaintainScrollPositionOnPostback="true"
    EnableViewState="true" Inherits="Survey._Default" EnableSessionState="True" ValidateRequest="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<%@ Register Src="OnLineSurvey.ascx" TagName="OnLineSurvey" TagPrefix="uc12" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8"/>
    <link rel="stylesheet" type="text/css" href="css/OnLineSurvey.css" />
   <script src="js/jquery-New.js" type="text/javascript"></script>
</head>
<body>
    <form id="form" runat="server">
    <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>--%>
    <table style="width:100%;height:100%;border:0px;padding:0px;">
        <tbody>
            <!-- 置頂方塊 (optional) -->
            <%--<tr class="topArea" style="height:44px;">
                <td style="width: 100%; height:44px;">
                    <!-- Menu 區域 (optional) -->
                    <table class="menuBar">
                        <tbody>
                            <tr>
                                <td>
                                    <div id="tabs">
                                        <ul>
                                            <li>
                                                <asp:LinkButton class="tab" ID="lbtnTab8" runat="server"><span>線上問卷</span></asp:LinkButton></li>
                                        </ul>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>--%>
            <tr>
                <td style="width: 100%; height: 100%; padding-left:8px;padding-right:8px;padding-bottom:8px;">
                    <asp:MultiView ID="mv" ActiveViewIndex="0" runat="server">
                        <asp:View ID="mvNewSurvey" runat="server">
                            <uc12:OnLineSurvey ID="OnLineSurvey1" runat="server" />
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>