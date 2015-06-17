<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecycleContentList.ascx.cs" Inherits="WebApplication1.CustomControls.RecycleContentList" %>
<table id="content_con_tab" cellspacing="0">
                    <tr style="background:#f5f5f5;">
                        <td class="con_td_left" align="center">新闻标题</td>
                        <td class="con_td_center"></td>
                        <td class="con_td_right"></td>
                    </tr>
<asp:Repeater ID="repDataList" runat="server">
    <ItemTemplate>
        <tr id='<%# Eval("NewsId", "news-{0}") %>'>
            <td class="con_td_left"><%# Eval("NewsTitle") %> </td>
            <td class="con_td_center"><img onclick='<%# Eval("NewsId", "RemoveFromRecycleBin({0})") %>' src="../images/cms_restore.jpg" style="cursor:pointer" /></td>
            <td class="con_td_right"><img onclick='<%# Eval("NewsId", "DeleteNews({0})") %>' src="../images/cms_delcontent.jpg" style="cursor:pointer" /></td>
        </tr>
    </ItemTemplate>
</asp:Repeater>
<tr><td><asp:Label ID="lblTip" runat="server" Visible="false" Text=""></asp:Label></td></tr>
<tr><td colspan="3" id="page"> <asp:Label ID="lblIndex" runat="server" Text="1"/>/<asp:Label ID="lblCount" runat="server" Text="1"/> 页  <asp:LinkButton ID="btnUp" Enabled="false" OnClick="btnUp_Click" Text="&lt;&lt;上一页" runat="server" /> <asp:LinkButton ID="btnNext" Enabled="false" OnClick="btnNext_Click" Text="下一页&gt;&gt;" runat="server" /></td></tr>
</table>
<script type="text/javascript">
        function RemoveFromRecycleBin(index) {
            $.post("RestoreContentHandler.ashx", { "id": index }, function (data) {
                if (data == "true") {
                    var box = document.getElementById("news-" + index);
                    box.style.display = 'none';
                    alert('内容已恢复成功');
                }
                else {
                    alert("内容恢复失败，请稍后重试" + data);
                }
            });
        }
        function DeleteNews(index) {
            if (confirm("从回收站删除后将不可恢复，是否继续？")) {
                $.post("DeleteNewsHandler.ashx", { "id": index }, function (data) {
                    if (data == "true") {
                        var box = document.getElementById("news-" + index);
                        box.style.display = 'none';
                    }
                    else {
                        alert("删除失败，请稍后重试" + data);
                    }
                });
            }
        }
    </script>