<%@ Page Title="资源管理" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ResourceManage.aspx.cs" Inherits="WebApplication1.Admin.ResourceManage" %>

<%@ Register Src="~/CustomControls/FileList.ascx" TagPrefix="uc" TagName="FileList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc:FileList runat="server" id="FileList" />
</asp:Content>
