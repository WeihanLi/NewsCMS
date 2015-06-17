<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="RecycleContent.aspx.cs" Inherits="WebApplication1.Admin.RecycleContent" %>

<%@ Register Src="~/CustomControls/RecycleContentList.ascx" TagPrefix="uc" TagName="RecycleContentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CleanRecycleBin() {
            if (confirm('清空回收站将彻底删除，无法删除，是否继续？')){
                $.post("/Admin/EmptyRecycleBinHandler.ashx", function (data) {
                    if (data == "true") {
                        alert('操作成功');
                        $("#content_content").hide();
                    }
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content_hea">
            <div id="content_hea_o">
                回收站内容管理 
            </div>
     </div>
        <div id="content_con_top">
            <div class="button_content"><img src="/images/cms_cleanRecycleBin.jpg" style="cursor:pointer" alt="清空回收站" onclick="CleanRecycleBin()" /></div>
        </div>
        <div id="content_content" style="clear:left">
            <uc:RecycleContentList runat="server" id="RecycleContentList" />
        </div> 
</asp:Content>
