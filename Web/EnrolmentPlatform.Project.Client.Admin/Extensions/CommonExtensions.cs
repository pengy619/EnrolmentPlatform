using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.Admin.Extensions
{
    public static class CommonExtensions
    {
        private static string _defaultScriptTagFormat = "<script src=\"{0}\"></script>";
        //private static string _defaultStyleTagFormat = "<link href=\"{0}\" rel=\"stylesheet\"/>";

        #region kindeditor编辑器
        public static IHtmlString RenderTextAreaEditorScripts(this HtmlHelper htmlHelper)
        {
            var editor_lang = "zh_CN";
            var editor_i18n = "~/Content/website/js/kindeditor-4.1.10/lang/zh_CN.js";
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            StringBuilder html = new StringBuilder();
            html.AppendFormat(_defaultScriptTagFormat, url.Content("~/Content/website/js/kindeditor-4.1.10/kindeditor-min.js"));
            html.Append(Environment.NewLine);
            html.AppendFormat(_defaultScriptTagFormat, url.Content(editor_i18n));
            html.Append(Environment.NewLine);
            html.Append("<script type=\"text/javascript\">");
            html.Append(Environment.NewLine);
            html.Append("// editor根路径");
            html.Append(Environment.NewLine);
            html.Append("var editorAppRoot = '" + url.Content("~/") + "';");
            html.Append(Environment.NewLine);
            html.Append(Environment.NewLine);
            html.Append("// editor指定语言");
            html.Append(Environment.NewLine);
            html.Append("var langType = '" + editor_lang + "';");
            html.Append(Environment.NewLine);
            html.Append("</script>");
            html.Append(Environment.NewLine);
            html.AppendFormat(_defaultScriptTagFormat, url.Content("~/Content/website/js/jquery.editor.js"));
            html.Append(Environment.NewLine);
            return new HtmlString(html.ToString());
        }
        #endregion
    }
}