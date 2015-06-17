<%@ Page Title="内容管理" Language="C#" ValidateRequest="false" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ContentManage.aspx.cs" Inherits="WebApplication1.Admin.ContentManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/NewsManage.js"></script>
    <script src="/uploadify/jquery.uploadify.min.js"></script>

    <script src="/kindeditor/kindeditor-min.js"></script>
    <script src="/kindeditor/lang/zh_CN.js"></script>

    <link href="/uploadify/uploadify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content_hea">
        <div id="content_hea_o">
            信息管理  / 内容管理
        </div>
        </div>
    <div id="addcontent">
            <div id="addcontent_hea">添加内容</div>
            <table id="addcolumn_tab">
                <tr>
                    <td class="addcolumn_left">标题</td>
                    <td class="addcolumn_right">
                            <input type="text" id="txtNewsTitle" />
                    </td>
                </tr>
                <tr>
                    <td class="addcolumn_left">选择栏目</td>
                    <td class="addcolumn_right">
                            <ul id="tt" class="easyui-tree" data-options="url:'/Handlers/getTypesNodeHandler.ashx'"></ul>
                    </td>
                </tr>
                <tr>
                    <td class="addcolumn_left">
                        <input id="img_upload" name="img_upload" type="file"/>
                    </td>
                    <td class="addcolumn_right"><input id="imgPath" type="text" /></td>
                </tr>
                <tr>
                    <td class="addcolumn_left"><input id="file_upload" name="file_upload" type="file"/></td>
                    <td class="addcolumn_right">
                            <input type="text" id="filePath" />
                    </td>
                </tr>
                <tr>
                    <td class="addcolumn_left">外部链接</td>
                    <td class="addcolumn_right">
                            <input type="text" id="externalLink" placeholder="http:// 或https://" />
                    </td>
                </tr>
                <tr>
                        <td class="addcolumn_left">添加日期(保持为空则取当前时间)</td>
                        <td class="addcolumn_right"><input type="text" id="publishDate" placeholder="yyyy-MM-dd" /></td>
                </tr>
            </table>
        
            <div id="addcontent_con">
                <textarea id="editor" name="editor">
                </textarea>
            </div>
            <div class="addcolumn_button">
                <div class="addcolumn_but"><a href="javascript:void(0)" onclick="AddANews()"><img src="/images/cms_add.jpg" /></a></div>
                <div class="addcolumn_but"><a href="default.aspx"><img src="/images/cms_back.jpg" /></a></div>
            </div>
        </div>
    <script type="text/javascript">
        var tId = 0;
        //uploadify初始化
        $(function () {
            $('#file_upload').uploadify({
                //指定swf
                'swf': '/uploadify/uploadify.swf',
                //服务器端处理程序
                'uploader': '/Admin/UploadFileHandler.ashx?dir=file',
                fileObjName: "imgFile",
                multi: false,
                formData: { "customUpload": 'true' },
                //按钮文本
                buttonText: '上传附件',
                //文件类型
                fileTypeExts: "*.zip;*.rar;*.doc;*.docx;*.xls;*xlsx",
                fileSizeLimit: "5 MB",

                successTimeout: 60,
                onUploadSuccess: function (file, data, response) {
                    //服务器端响应
                    var res= JSON.parse(data);
                    if (data == 'noPermission') {
                        alert('没有上传权限');
                    }
                    if (res.error != 0) {
                        alert('上传失败' + res.message);
                    } else {
                        alert('上传成功~~~');
                        $("#filePath").val(res.url);
                    }
                }
            });
        });
        $(function () {
            $('#img_upload').uploadify({
                //指定swf
                'swf': '/uploadify/uploadify.swf',
                //服务器端处理程序
                'uploader': '/Admin/UploadFileHandler.ashx',
                fileObjName: "imgFile",
                multi: false,
                formData: { "customUpload": 'true' },
                //按钮文本
                buttonText: '上传图片',
                //文件类型
                fileTypeExts: "*.jpg;*.png;*.bmp",
                fileTypeDesc: "image files",
                fileSizeLimit: "4 MB",

                successTimeout:60,
                onUploadSuccess: function (file, data, response) {
                    //服务器端响应
                    var res = JSON.parse(data);
                    if (data == 'noPermission') {
                        alert('没有上传权限');
                    }
                    if (res.error != 0) {
                        alert('上传失败' + res.message);
                    } else {
                        alert('上传成功~~~');
                        $("#imgPath").val(res.url);
                    }
                }
            });
        });

        //kindeditor初始化
        var editor;
        KindEditor.ready(function (K) {
                //创建富文本编辑器
                editor = K.create('textarea[name="editor"]', {
                    //上传文件处理程序
                    uploadJson: '/Admin/uploadFileHandler.ashx',
                    //获取上传文件列表程序
                    fileManagerJson: '/Admin/fileManagerHandler.ashx',
                    //设置允许使用已上传文件中的文件
                    allowFileManager: true,
                    //设置取消标签过滤
                    filterMode: false,
                    //设置编辑器的宽度和高度，直接设置不行，
                    //生成富文本编辑器之后，原textarea的display被设置为none，设置宽度和高度无效
                    width: '858px',
                    height: '500px'
            });
        });
        
        //添加一个新闻
        function AddANews() {
            //alert(editor.html());
            var title = $("#txtNewsTitle").val();
            var content = editor.html();
            var node = $('#tt').tree('getSelected');
            if (node) {
                tId = node.id;
            }
            var imgPath = $("#imgPath").val();
            var attachPath = $("#filePath").val();
            var pubDate = $("#publishDate").val();
            //alert(type+top);
            AddNews(title, content, tId, imgPath, attachPath, externalLink.value, pubDate);
        }
    </script>
</asp:Content>