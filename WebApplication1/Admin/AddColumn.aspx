<%@ Page Title="添加栏目" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddColumn.aspx.cs" Inherits="WebApplication1.Admin.AddColumn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content_hea">
        <div id="content_hea_o">
            信息管理 / 栏目管理
        </div>
    </div>
    <div id="addcolumn">
        <div id="addcolumn_hea">添加栏目</div>
        <table id="addcolumn_tab">
            <tr>
                <td class="addcolumn_left">栏目名称</td>
                <td class="addcolumn_right">
                    <input type="text" id="txtColumn"/> 
                </td>
            </tr>
            <tr>
                <td class="addcolumn_left">选择父栏目（若要添加一级目录，请忽略此项）</td>
                <td class="addcolumn_right">
                    <ul id="tt" class="easyui-tree" data-options="url:'/Handlers/getTypesNodeHandler.ashx'"></ul>
<%--                    <asp:TreeView runat="server" ID="tvCategory" SelectedNodeStyle-BackColor="SteelBlue" BorderColor="WindowFrame" OnSelectedNodeChanged="tvCategory_SelectedNodeChanged"></asp:TreeView>--%>
                </td>
            </tr>
            <tr>
                <td id="tipCell">

                </td>
                <td>
                    <input type="button" id="btnCancelParentSelect" onclick="cancelParent()" value="取消选择父节点" />
                </td>
            </tr>
        </table>
        <div class="addcolumn_button">
            <div class="addcolumn_but"> 
<%--                <asp:ImageButton ID="btnAdd" OnClick="btnAdd_Click" ImageUrl="~/images/cms_add.jpg" runat="server" />--%>
                <img src="/images/cms_add.jpg" style="cursor:pointer" alt="添加" onclick="AddColumn()" />
            </div>
                <div class="addcolumn_but"><a href="Default.aspx"><img src="/images/cms_back.jpg" /></a></div>
        </div>
    </div>
    <script type="text/javascript">
        $("#tt").tree({
            onSelect: function (node) {
                $("#tipCell").text('当前选择的父节点是:' + node.text);
            }
        });
        //添加栏目
        function AddColumn() {
            var text = $("#txtColumn").val();
            if (text==null||text=='') {
                alert('栏目名称不能为空');
                return;
            }
            var node = $('#tt').tree('getSelected');
            var pId = 0;
            if (node) {
                pId = node.id;
            }
            $.post("/Admin/AddColumnHandler.ashx", { "parentId": pId, "name": text }, function (data) {
                if (data == 'true') {
                    window.location.href = '/Admin/columnManage.aspx';
                } else {
                    alert('添加失败');
                }
            });
        }
        //取消选择父节点
        function cancelParent() {
            $(".tree-node-selected").removeClass("tree-node-selected");
            $("#tipCell").text('');
        }
    </script>
</asp:Content>
