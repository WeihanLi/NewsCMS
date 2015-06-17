<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="FileEdit.aspx.cs" Inherits="WebApplication1.Admin.FileEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <link href="../codeMirror/codemirror.css" rel="stylesheet" type="text/css" />
    <script src="../codeMirror/codemirror.js"></script>
    <script src="../codeMirror/mode/xml/xml.js"></script>
    <script src="../codeMirror/mode/css/css.js"></script>
    <script src="../codeMirror/mode/javascript/javascript.js"></script>
    <script src="../codeMirror/mode/htmlmixed/htmlmixed.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div id="context" style="padding:10px">
            <textarea id="code" runat="server"> 

            </textarea>
        </div>
        <div>
            <img src="/images/cms_save.jpg" style="cursor:pointer" onclick="SaveChanges()" alt="确定" />
            <img src="/images/cms_cancel.jpg" style="cursor:pointer" onclick="window.history.go(-1)" alt="取消" />
        </div>
    </div>
    
    <script type="text/javascript">
        //根据type 不同，动态设置 文件类型
        var index = <%= type %>;
        var t = '';
        if ( index <= 0) {
            t = 'css';
        } else if (index == 1) {
            t = 'javascript';
        } else {
            t = 'htmlmixed';
        }
        var mixedMode = {
            name: t
        };
        //CodeMirror 初始化
       var editor = CodeMirror.fromTextArea(document.getElementById("<%= code.ClientID %>"), {
           lineNumbers: true,
           extraKeys: { "Ctrl-Space": "autocomplete" },
           mode: mixedMode
       });
        //设置编辑器高度
        editor.setSize('1020px','540px');
        //保存变化
        function SaveChanges() {
            //将编辑器中的数据同步到textarea中
            editor.save();
            //获取数据
            var content =document.getElementById("<%= code.ClientID %>").value;
            var fileName = '<%= fName %>';
            $.post("/Admin/SaveFileHandler.ashx",{"type":index,"fName":fileName,"contents":content},function (data) {
                if(data=="true"){
                    alert('操作成功!');
                    window.location.href='/Admin/ResourceManage-'+index+".aspx";
                }else{
                    alert('操作失败！'+data);
                }
            });
        }
    </script>
</asp:Content>