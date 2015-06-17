<%@ Page Title="用户管理" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="WebApplication1.Admin.UserManage" %>
<%@ Register Src="~/CustomControls/UserList.ascx" TagPrefix="uc" TagName="UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content_hea">
            <div id="content_hea_o">
                管理员管理
            </div>
            </div>
            <div id="button_div">
                <div class="button_column"><a href="AddUser.aspx"><img src="../images/cms_adduser.jpg" /></a></div>
            </div>
    <uc:UserList runat="server" id="UserList" />
</asp:Content>
