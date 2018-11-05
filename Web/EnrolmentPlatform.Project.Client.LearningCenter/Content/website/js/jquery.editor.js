// 文本编辑器
(function ($) {
    /// <summary>
    /// 创建Web编辑器
    /// </summary>
    $.fn.createEditor = function (options, callback) {
        var itemTypes = [
			{
			    name: 'basic', items: ['undo', 'redo', '|', 'fontsize', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                    'justifyleft', 'justifycenter', 'justifyright', 'justifyfull', '|', 'insertorderedlist', 'insertunorderedlist',
                    'table', 'link', 'unlink', 'hr', 'image', 'flash', 'media', '|', 'preview', 'clearhtml', 'removeformat', 'plainpaste']
			},
			{
			    name: 'default', items: ['undo', 'redo', '|', 'selectall', 'cut', 'copy', 'paste', 'plainpaste', '|',
                    'justifyleft', 'justifycenter', 'justifyright', 'justifyfull', 'insertorderedlist', 'insertunorderedlist',
                    'indent', 'outdent', 'subscript',
                    'superscript', '|', 'clearhtml', 'quickformat', 'source', 'preview', 'fullscreen', '/',
                    'fontname', 'fontsize', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', 'strikethrough', 'removeformat', '|',
                    'image', 'flash', 'media', 'table', 'hr', 'anchor', 'link', 'unlink']
			},
			{
			    name: 'full', items: ['source', '|', 'undo', 'redo', '|', 'preview', 'print', 'template', 'code', 'cut', 'copy', 'paste',
                    'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
                    'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
                    'superscript', 'clearhtml', 'quickformat', 'selectall', '|', 'fullscreen', '/',
                    'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold',
                    'italic', 'underline', 'strikethrough', 'lineheight', 'removeformat', '|', 'image', 'multiimage',
                    'flash', 'media', 'insertfile', 'table', 'hr', 'emoticons', 'baidumap', 'pagebreak',
                    'anchor', 'link', 'unlink'/*, '|', 'about'*/]
			}];

        // KindEditor支持的参数列表，see：http://www.kindsoft.net/docs/option.html
        var defaults = {
            themeType: 'default',
            itemType: 'default',
            langType: langType,
            allowFileManager: true,
            uploadJson: editorAppRoot + 'Content/website/js/kindeditor-4.1.10/asp.net/upload_json.ashx',
            fileManagerJson: editorAppRoot + 'Content/website/js/kindeditor-4.1.10/asp.net/file_manager_json.ashx',
            formatUploadUrl: false,
            imageSizeLimit: '2MB',
            imageUploadLimit: 10
        };

        settings = $.extend(defaults, options);
        var items = itemTypes[0].items;
        for (i = 0; i < itemTypes.length; i++) {
            if (itemTypes[i].name == settings.itemType)
                items = itemTypes[i].items;
        }
        settings.items = items;
        var KE = window.KindEditor || parent.window.KindEditor;
        return this.each(function () {
            var $this = $(this);
            KE.ready(function (K) {
                if (callback) {
                    callback(K.create($this, settings));
                }
                else {
                    K.create($this, settings);
                }
            });
        });
    };

})(jQuery);
