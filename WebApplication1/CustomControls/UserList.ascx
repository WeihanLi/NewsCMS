<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserList.ascx.cs" Inherits="WebApplication1.CustomControls.UserList" %>
            <div id="content_user">
               <table id="user_table" cellspacing="0">
                   <tr style="background:#f5f5f5;">
                       <td style="border-right:1px #cdcdcd solid" class="name">账号</td>
                       <td style="border-right:1px #cdcdcd solid"  class="time">创建日期</td>
                       <td style="border-right:1px #cdcdcd solid"  class="user">角色</td>
                       <td style="border-right:1px #cdcdcd solid"  class="user">操作</td>
                       <td class="td_input"></td>
                   </tr>
                   <asp:Repeater ID="repUserList" runat="server">
                       <ItemTemplate>
                           <tr id='<%# Eval("UserId","user-{0}") %>'>
                               <td class="name"><%# Eval("UserName") %> </td>
                               <td class="time"> <%# ((DateTime)Eval("AddTime")).ToString("yyyy-MM-dd") %> </td>
                               <td class="user"> <%# Convert.ToBoolean(Eval("IsAdmin"))?"超级管理员":"管理员" %> </td>
                               <td class="user"></td>
                               <td class="td_input">
                                   <!--  Todo:启用、禁用用户   -->
<%--                                   <%# Convert.ToBoolean(Eval("IsDel"))?<image onclick='<%# Eval("UserId","EnableUser({0})") %>' src="/images/cms_enable.jpg" alt="启用" /> : image onclick='<%# Eval("UserId","DisableUser({0})") %>' src="/images/cms_disable.jpg" alt="禁用" /> %>--%>
                                   <image  id='<%# Eval("UserId","disableUser-{0}") %>' style="cursor:pointer;display:<%# Convert.ToBoolean(Eval("IsDel"))?"none":"inline" %>" onclick='<%# Eval("UserId","DisableUser({0})") %>' src="/images/cms_disable.jpg" alt="禁用" />

                                   <image id='<%# Eval("UserId","enableUser-{0}") %>' style="cursor:pointer;display:<%# Convert.ToBoolean(Eval("IsDel"))?"inline":"none" %>" onclick='<%# Eval("UserId","EnableUser({0})") %>' src="/images/cms_enable.jpg" alt="启用" /> 
                                   <image style="cursor:pointer;" onclick='<%# Eval("UserId","Delete({0})") %>' src="/images/cms_deluser.jpg" alt="删除" /> 
                               </td>
                           </tr>
                       </ItemTemplate>
                   </asp:Repeater>
               </table>
            </div>
            <div id="page"> <asp:Label ID="lblIndex" runat="server" Text="1"/>/<asp:Label ID="lblCount" runat="server" Text="1"/> 页  <asp:LinkButton ID="btnUp" OnClick="btnUp_Click" Text="上一页" runat="server" /> <asp:LinkButton ID="btnNext" OnClick="btnNext_Click" Text="下一页" runat="server" /></div>
    <script type="text/javascript">
        //删除用户，id：用户id
        function Delete(id){
            if (confirm('确定删除？如果只是禁用一段时间可以选择禁用，是否继续？')) {
                $.post("DeleteUserHandler.ashx", { "id": id }, function (data) {
                    if (data == "true") {
                        $("#user-" + id).hide();
                    }
                    else {
                        alert("发生异常 "+data);
                    }
                });
            }
        }
        //启用用户，id:用户id
        function EnableUser(id) {
            if (confirm('将启用此用户，启用后此用户将可以登录、可以进行内容的管理，是否继续？')) {
                $.post("EnableUserHandler.ashx", { "uid": id }, function (data) {
                    if (data == "true") {
                        //$("#user-" + id).hide();
                        alert('操作成功');
                        $("#enableUser-" + id).css("display", "none");
                        $("#disableUser-" + id).css("display", "inline");
                        //window.location.href = '/Admin/UserManage.aspx';
                    }
                    else {
                        alert("发生异常 " + data);
                    }
                });
            }
        }
        //禁用用户，id:用户id
        function DisableUser(id) {
            if (confirm('将禁用此用户，禁用后该用户将无法登录，是否继续？')) {
                $.post("DisableUserHandler.ashx", { "uid": id }, function (data) {
                    if (data == "true") {
                        //$("#user-" + id).hide();
                        alert('操作成功');
                        $("#enableUser-" + id).css("display", "inline");
                        $("#disableUser-" + id).css("display", "none");
                        //window.location.href = '/Admin/UserManage.aspx';
                    }
                    else {
                        alert("发生异常 " + data);
                    }
                });
            }
        }
    </script>