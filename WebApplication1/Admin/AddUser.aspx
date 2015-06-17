<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="WebApplication1.Admin.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function ExistUserName(name) {
            $.post("ExistNameHandler.ashx", { "uid": name }, function (data) {
                if (data == "true") {
                    $("#tip").text("该用户名已存在");
                    return true;
                } else {
                    $("#tip").text('');
                }
            });
            return false;
        }

        function IsEmpty(val) {
            if (val == null || val == "") {
                return true;
            } else {
                return false;
            }
        }

        function ValidSubmit() {
            if (IsEmpty($("txtUid").val())||IsEmpty($("txtPwd").val())||IsEmpty($("txtPwd2").val())) {
                return false;
            }
            if (ExistUserName($("txtUid").val())) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content_hea">
            <div id="content_hea_o">
                管理员管理
            </div>
            </div>
            <div id="addcolumn">
                <div id="addcolumn_hea">添加管理员</div>
                <table id="addcolumn_tab">
                    <tr>
                        <td class="addcolumn_left">请设置登录名</td>
                        <td class="addcolumn_right">
                            <asp:TextBox runat="server" ID="txtUid" onchange="ExistUserName(this.value)" MaxLength="10" /> </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvUid" runat="server" ControlToValidate="txtUid" ErrorMessage="用户名不能为空"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="addcolumn_left">请设置密码</td>
                        <td class="addcolumn_right">
                            <asp:TextBox runat="server" TextMode="Password" ID="txtPwd" MaxLength="20" /> </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvPwd" runat="server" ControlToValidate="txtPwd" ErrorMessage="密码不能为空"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="addcolumn_left">请确认密码</td>
                        <td class="addcolumn_right"><asp:TextBox runat="server" TextMode="Password" ID="txtPwd2" MaxLength="20" /></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvPwd2" ControlToValidate="txtPwd2" runat="server" ErrorMessage="确认密码不能为空"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CompareValidator ID="cvPwd" runat="server" ForeColor="Red" ControlToValidate="txtPwd2" ControlToCompare="txtPwd" ErrorMessage="密码 与 确认密码 不一致"></asp:CompareValidator></td>
                        <td id="tip" style="color:red"> </td>
                    </tr>
                </table>
                <div class="addcolumn_button">
                    <div class="addcolumn_but">
                        <asp:ImageButton ImageUrl="../images/cms_add.jpg" ID="btnAdd" OnClientClick="ValidSubmit()" OnClick="btnAdd_Click" runat="server" /> </div>
                    <div class="addcolumn_but"><a href="default.aspx"><img src="/images/cms_back.jpg" /></a></div>
                </div>
            </div>
</asp:Content>
