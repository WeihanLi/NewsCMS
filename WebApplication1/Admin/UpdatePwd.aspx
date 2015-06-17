<%@ Page Title="修改密码" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UpdatePwd.aspx.cs" Inherits="WebApplication1.Admin.UpdatePwd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content_hea">
            <div id="content_hea_o">
                管理员管理
            </div>
            </div>
            <div id="addcolumn">
                <div id="addcolumn_hea">修改密码</div>
                <table id="addcolumn_tab">
                    <tr>
                        <td class="addcolumn_left">请输入原密码</td>
                        <td class="addcolumn_right">
                            <asp:TextBox runat="server" ID="txtOldPwd" TextMode="Password" onchange="ValidOldPwd(this.value)" MaxLength="10" /> </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvOldPwd" runat="server" ControlToValidate="txtOldPwd" ErrorMessage="用户名不能为空"></asp:RequiredFieldValidator>
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
                        <asp:ImageButton ImageUrl="../images/cms_ok.jpg" ID="btnOk" OnClientClick="ValidSubmit()" OnClick="btnOk_Click" runat="server" /> </div>
                    <div class="addcolumn_but"><a href="default.aspx"><img src="/images/cms_cancel.jpg" /></a></div>
                </div>
            </div>
   <script type="text/javascript">
        function ValidOldPwd(pwd) {
            $.post("LoginHandler.ashx", { "username": <%= ((Models.User)Session["User"]).UserName %>,"password":pwd,"action":"updatePwd" }, function (data) {
                if (data == "true") {
                    $("#tip").text("");
                    return true;
                } else {
                    $("#tip").text('原密码有误');
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

        function Compare() {
            if (IsEmpty($("txtOldPwd").val())||IsEmpty($("txtPwd").val())||IsEmpty($("txtPwd2").val())) {
                return false;
            }
            if (ValidOldPwd($("txtOldPwd").val())) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>