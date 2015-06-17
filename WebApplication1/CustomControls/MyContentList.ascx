<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyContentList.ascx.cs" Inherits="WebApplication1.CustomControl.MyContentList" %>
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
            <td class="con_td_center">
                <img style="cursor:pointer" id='<%# Eval("NewsId","transferTopState-{0}") %>' state='<%# Eval("IsTop") %>' onclick='<%# Eval("NewsId","TransferTopState({0})") %>' src='<%# Convert.ToBoolean(Eval("IsTop"))?"/images/cms_cancelTop.jpg":"/images/cms_setTop.jpg" %>'' style="cursor:pointer" alt="置顶" /></td>
            <td class="con_td_right"><a href='<%# Eval("NewsId", "ContentEdit-{0}.aspx") %>'><img src="/images/cms_edit.jpg" alt="编辑"/> </a> <img onclick='<%# Eval("NewsId", "MoveToRecycleBin({0})") %>' src="../images/cms_delcontent.jpg" style="cursor:pointer" /></td>
        </tr>
    </ItemTemplate>
</asp:Repeater>
<tr><td><asp:Label ID="lblTip" runat="server" Visible="false" Text=""></asp:Label></td></tr>
<tr><td colspan="3" id="page"> <asp:Label ID="lblIndex" runat="server" Text="1"/>/<asp:Label ID="lblCount" runat="server" Text="1"/> 页  <asp:LinkButton ID="btnUp" Enabled="false" OnClick="btnUp_Click" Text="&lt;&lt;上一页" runat="server" /> <asp:LinkButton ID="btnNext" Enabled="false" OnClick="btnNext_Click" Text="下一页&gt;&gt;" runat="server" /></td></tr>
</table>
    <script type="text/javascript">
        //永久删除新闻
        function DeleteNews(index) {
            if (confirm("永久删除后将不可恢复，是否继续？")) {
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
        //将新闻移动到回收站
        function MoveToRecycleBin(index) {
            $.post("MoveToRecycleBinHandler.ashx", { "id": index }, function (data) {
                if (data == "true") {
                    var box = document.getElementById("news-" + index);
                    box.style.display = 'none';
                    alert('已将内容移动内容回收站，可以从回收站恢复');
                }
                else {
                    alert("移动到回收站失败，请稍后重试" + data);
                }
            });
        }
        //转换新闻的置顶状态
        function TransferTopState(index) {
            var item =document.getElementById("transferTopState-" + index);
            var topState = item.getAttribute("state");
            //alert(topState);
            $.post("/Admin/TransferTopStateHandler.ashx", { "id": index, "topState": topState }, function (data) {
                if (data=='true') {
                    //alert('操作成功！');
                    if (topState == 'True') {
                        item.src = "/images/cms_setTop.jpg";
                        item.setAttribute("state","False");
                        //alert('cancel top success');
                    } else {
                        item.src = "/images/cms_cancelTop.jpg";
                        item.setAttribute("state", "True");
                        //alert('set top success');
                    }
                }
            });
        }
    </script>