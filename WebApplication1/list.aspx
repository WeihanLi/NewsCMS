<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="WebApplication1.list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="content_con_tab" cellspacing="0">
<asp:Repeater ID="repDataList" runat="server">
    <ItemTemplate>
        <tr>
            <td class="con_td_left"><a href='<%# Eval("NewsPath") %>'> <%# Eval("NewsTitle") %> </a></td>
            <td class="con_td_center">
            <td class="con_td_right"><%# ((DateTime)Eval("PublishTime")).ToString("yyyy-MM-dd") %> </td>
        </tr>
    </ItemTemplate>
</asp:Repeater>
<tr><td><asp:Label ID="lblTip" runat="server" Visible="false" Text=""></asp:Label></td></tr>
<tr><td colspan="3" id="page"> <asp:Label ID="lblIndex" runat="server" Text="1"/>/<asp:Label ID="lblCount" runat="server" Text="1"/> 页  <asp:LinkButton ID="btnUp" Enabled="false" OnClick="btnUp_Click" Text="&lt;&lt;上一页" runat="server" /> <asp:LinkButton ID="btnNext" Enabled="false" OnClick="btnNext_Click" Text="下一页&gt;&gt;" runat="server" /></td></tr>
</table>
    </div>
    </form>
</body>
</html>
