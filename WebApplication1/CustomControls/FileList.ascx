<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileList.ascx.cs" Inherits="WebApplication1.CustomControls.FileList" %>
<table id="content_con_tab" cellspacing="0">
    <tr style="background:#f5f5f5;">
        <td class="con_td_left" align="center">文件名</td>
<!--        <td style="border-right:1px #cdcdcd solid"  class="time">文件大小</td> -->
        <td class="con_td_center">创建时间</td>
        <td class="con_td_right">操作</td>
    </tr>
<asp:Repeater ID="repDataList" runat="server">
    <ItemTemplate>
        <tr>
            <td class="con_td_left"><%# Eval("FileName") %> &nbsp;&nbsp;&nbsp; <%# Eval("FileSize").ToString()+"KB" %>&nbsp;&nbsp;&nbsp;</td>
            <td class="con_td_center"><%# ((DateTime)Eval("CreateTime")).ToString("yyyy-MM-dd HH:mm") %></td>
            <td class="con_td_right"> <a href='<%# Eval("FileNameWithoutExtension", "FileEdit.aspx?fName={0}&t="+type.ToString()) %>'><img src="/images/cms_edit.jpg"  alt="编辑" /> </a><!-- <img onclick='<%# Eval("FilePath", "DeleteFile({0})") %>' src="../images/cms_delcontent.jpg" style="cursor:pointer" /> --></td>
        </tr>
    </ItemTemplate>
</asp:Repeater>
<tr><td><asp:Label ID="lblTip" runat="server" Visible="false" Text=""></asp:Label></td></tr>
<tr><td colspan="3" id="page"> <asp:Label ID="lblIndex" runat="server" Text="1"/>/<asp:Label ID="lblCount" runat="server" Text="1"/> 页  <asp:LinkButton ID="btnUp" Enabled="false" OnClick="btnUp_Click" Text="&lt;&lt;上一页" runat="server" /> <asp:LinkButton ID="btnNext" Enabled="false" OnClick="btnNext_Click" Text="下一页&gt;&gt;" runat="server" /></td></tr>
</table>
    <script type="text/javascript">
        function DeleteFile(path) {
            alert(path);
            if (confirm("删除后将不可恢复，是否继续？")) {
                $.post("DeleteFileHandler.ashx", { "id": index,"fPath":path }, function (data) {
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