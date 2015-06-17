//通过ajax动态加载newsType列表，默认选中第一个
function InitNewsType() {
    $.post("SelectNewsTypeHandler.ashx", function (data) {
        if (data!="Fail") 
        {
            //alert(data);
            //alert(Array.isArray(eval(data)));
            //必须要eval()或者Json.Parse()，不然无法直接解析返回的json数据，不知道为什么。。。。。
            var types = eval(data);
            var select = document.getElementById("selectNewsType").innerHTML;
            //动态添加 下拉列表 数据        
            for (var i = 0; i < types.length; i++) {
                var html = '';
                if (i == 0) {
                    html = "<option value='" + types[i].CategoryId + "' selected='selected'>" + types[i].CategoryName + "</option>";
                }
                else{
                    html = "<option value='" + types[i].CategoryId + "'>" + types[i].CategoryName + "</option>";
                }
                select += html;
            }
            //设置下拉列表的 innerHTML
            $("#selectNewsType").html(select);
        }
    });
}
//通过ajax动态加载newsType列表，选中 type 项
function InitNewsTypeByType(type) {
    $.post("SelectNewsTypeHandler.ashx", function (data) {
        if (data != "Fail") {
            //alert(data);
            //alert(Array.isArray(eval(data)));
            //必须要eval，不然无法直接解析返回的json数据，不知道为什么。。。。。
            var types = eval(data);
            var select = document.getElementById("selectNewsType").innerHTML;
            //动态添加 下拉列表 数据        
            for (var i = 0; i < types.length; i++) {
                var html = '';
                if (types[i].CategoryId == type) {
                    html = "<option value='" + types[i].CategoryId + "' selected='selected'>" + types[i].CategoryName + "</option>";
                }
                else {
                    html = "<option value='" + types[i].CategoryId + "'>" + types[i].CategoryName + "</option>";
                }
                select += html;
            }
            //设置下拉列表的 innerHTML
            $("#selectNewsType").html(select);
        }
    });
}
//添加新闻 js
function AddNews(title, content, typeId, imgPath, attachPath, externalLink, publishDate) {
    //修改
    $.post("AddNewsHandler.ashx", { "newsTitle": title, "newsContent": content, "newsType": typeId, "imagePath": imgPath, "attachPath": attachPath, "externalLink": externalLink, "publishDate": publishDate }, function (data) {
        if (data == "true") {
            window.location.href = 'Default.aspx';
        }
        else {
            alert("发生异常，添加失败！");
        }
    });
}
//更新新闻 js
function UpdateNews(id, title, content, typeId, imgPath, attachPath, externalLink, publishDate) {
    //
    $.post("UpdateNewsHandler.ashx", { "newsId": id, "newsTitle": title, "newsContent": content, "newsType": typeId, "imagePath": imgPath, "attachPath": attachPath, "externalLink": externalLink, "publishDate": publishDate }, function (data) {
        if (data == "true") {
            window.location.href = 'Default.aspx';
        }
        else {
            alert("发生异常，更新失败！");
        }
    });
}