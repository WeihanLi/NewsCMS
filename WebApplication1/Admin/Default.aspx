<%@ Page Title="内容管理" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Admin.Default" %>
<%@ Register Src="~/CustomControls/MyContentList.ascx" TagPrefix="uc1" TagName="MyContentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content_hea">
            <div id="content_hea_o">
                信息管理
            </div>
            </div>
            <div id="content_column">
                <table cellspacing="0">
                    <tr style="background:#f5f5f5;"><td></td></tr>
                    <asp:Repeater ID="repColumns" runat="server">
                        <ItemTemplate>                    
                            <tr><td><a href='<%# Eval("CategoryId", "Default-{0}.aspx") %>'><%#  Eval("CategoryName")%> </a></td></tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div id="content_con_top">
                
                <div class="button_content">
                    <asp:ImageButton ImageUrl="~/images/cms_static.jpg" ID="btnStatic" OnClick="btnStatic_Click1" runat="server" />
                    <a href="ContentManage.aspx"><img src="../images/cms_addcontent.jpg" /></a> </div>
            </div>
            <div id="content_content">
                <uc1:MyContentList runat="server" id="MyContentList" />
            </div> 
</asp:Content>
