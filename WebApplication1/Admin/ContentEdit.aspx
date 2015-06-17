<%@ Page Title="内容编辑" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ContentEdit.aspx.cs" Inherits="WebApplication1.Admin.ContentEdit" %>

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
            内容管理 / 作品管理 
        </div>
        </div>
    <div id="addcontent">
        <input type="hidden" name="NewsId" runat="server" id="NewsId" value=" " />
            <div id="addcontent_hea">添加内容</div>
            <table id="addcolumn_tab">
                <tr>
                    <td class="addcolumn_left">标题</td>
                    <td class="addcolumn_right">
                            <input type="text" id="txtNewsTitle" />
                    </td>
                </tr>
                <tr>
                    <td class="addcolumn_left">选择栏目（不选择为原类别）</td>
                    <td class="addcolumn_right">
                        <%--<select name="selectNewsType" id="selectNewsType">
                        </select>--%>
                          <ul id="tt" class="easyui-tree" data-options="url:'/Handlers/getTypesNodeHandler.ashx'"></ul>
                        <%--<label id="lblType"></label>--%>
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
                            <input type="text" id="externalLink" />
                    </td>
                </tr>
                <tr>
                        <td class="addcolumn_left">添加日期</td>
                        <td class="addcolumn_right"><input type="text" id="pubDate" /></td>
                 </tr>
            </table>
        
            <div id="addcontent_con">
                <textarea id="editor" name="editor">
                </textarea>
            </div>
            <div class="addcolumn_button">
                <div class="addcolumn_but"><a href="javascript:void(0)" onclick="AddANews()"><img src="/images/cms_add.jpg" /></a></div>
                <div class="addcolumn_but"><a href="default.aspx"><img src="/images/cms_cancel.jpg" /></a></div>
            </div>
        </div>

    <script type="text/javascript">
        var newsId = $("#<%= NewsId.ClientID %>").val();
        var typeId = 0;
        //kindeditor初始化
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('textarea[name="editor"]', {
                cssPath: '/kindeditor/plugins/code/prettify.css',
                uploadJson: '/Admin/uploadFileHandler.ashx',
                fileManagerJson: '/Admin/fileManagerHandler.ashx',
                allowFileManager: true,
                filterMode: false,
                width: '858px',
                height: '500px'
            });
        });
        //uploadify初始化
        //上传文件
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
                    var res = JSON.parse(data);
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
        //上传图片
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

                successTimeout: 60,
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
        //初始化新闻内容
        function InitNewsContent(newsId) {
            if (newsId == null) {
                return;
            }
            $.post("SelectNewsByIdHandler.ashx", { "id": newsId }, function (data) {
                if (data != "error") {
                    var news = JSON.parse(data);
                    //修改前台 id
                    $("#txtNewsTitle").val(news.NewsTitle);
                    //设置类别id
                    typeId = news.NewsTypeId;
                    $("#pubDate").val(news.PublishTime);
                    if (news.NewsImagePath) {
                        $("#imgPath").val(news.NewsImagePath);
                    }
                    if (news.NewsAttachPath) {
                        //alert(news.NewsAttachPath);
                        $("#filePath").val(news.NewsAttachPath);
                    }
                    if (news.NewsExternalLink) {
                        $("#externalLink").val(news.NewsExternalLink);
                    }
                    //设置富文本编辑器内容   news.NewsContent
                    editor.html(news.NewsContent);
                }
                else {
                    alert("发生异常，加载失败！");
                }
            });
        }
        //页面初始化
        InitNewsContent(newsId);
        //更新内容
        function Update() {
            var title = $("#txtNewsTitle").val();
            var content = editor.html();
            var node = $('#tt').tree('getSelected');
            if (node) {
                typeId = node.id;
            } else {
                return;
            }
            var imgPath = $("#imgPath").val();
            var attachPath = $("#filePath").val();
            var externalLink = $("#externalLink").val();
            var publishDate = $("pubDate").val();
            UpdateNews(newsId, title, content, typeId, imgPath, attachPath, externalLink, publishDate);
        }
    </script>
</asp:Content>
