<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ColumnManage.aspx.cs" Inherits="WebApplication1.Admin.ColumnManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
        function Del(id) {
            if (confirm('确定删除？')) {
                $.post("DeleteColumnHandler.ashx", { "id": id }, function (data) {
                    if (data == "true") {
                        //alert("删除成功！");
                        $("#column-" + id).hide();
                    }
                    else {
                        alert('删除失败，请稍后重试');
                    }
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content_hea">        
        <div id="content_hea_o">
                信息管理 / 栏目管理
            </div>
            </div>
            <div id="button_div">
                <div class="button_column"><a href="AddColumn.aspx"><img src="/images/cms_addcolumn.jpg" /></a></div>
            </div>
            <div id="content_con">
                <table id="content_tab" cellspacing="0">
                    <tr style="background:#f5f5f5;">
                        <td class="td_left" align="center">栏目名称</td>
                        <td class="td_right"></td>
                    </tr>
                    <asp:Repeater ID="repColumns" runat="server">
                        <ItemTemplate>
                            <tr id='<%# Eval("CategoryId", "column-{0}") %>'>
                                <td class="td_left"> <%# Eval("CategoryName") %> </td>
                                <td class="td_right"><a href="#" onclick='<%# Eval("CategoryId", "Del({0})") %>'><img src="/images/cms_delcolumn.jpg" /></a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        <div id="page"> <asp:Label ID="lblIndex" runat="server" Text="1"/>/<asp:Label ID="lblCount" runat="server" Text="1"/> 页  <asp:LinkButton ID="btnUp" OnClick="btnUp_Click" Text="上一页" runat="server" /> <asp:LinkButton ID="btnNext" OnClick="btnNext_Click" Text="下一页" runat="server" /></div>
</asp:Content>
