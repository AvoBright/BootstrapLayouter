using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AvoBright.BootstrapLayouter
{
    static class HtmlGenerator
    {
        public static string GeneratePreviewHtml(Page page)
        {
            string path = System.IO.Path.GetDirectoryName(page.GetType().Assembly.Location);

            string bootstrapCssPath = System.IO.Path.Combine(path, "bootstrap.css");
            string bootstrapCss = "";
            using (var reader = new StreamReader(new FileStream(bootstrapCssPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                bootstrapCss = reader.ReadToEnd();
            }

            string previewHtmlPath = System.IO.Path.Combine(path, "preview.html");
            var sourceBuilder = new StringBuilder();
            using (var reader = new StreamReader(new FileStream(previewHtmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                sourceBuilder.Append(reader.ReadToEnd());
            }

            StringBuilder builder = new StringBuilder();
            foreach (var container in page.Containers)
            {
                if (container.IsFluid)
                {
                    builder.AppendLine("<div class=\"container-fluid\">");
                }
                else
                {
                    builder.AppendLine("<div class=\"container\">");
                }

                foreach (var row in container.Rows)
                {
                    Row(row, builder, 1, true);
                }

                builder.AppendLine("</div>");
            }

            sourceBuilder.Replace("{BOOTSTRAPCSS}", bootstrapCss);
            sourceBuilder.Replace("{LAYOUTHTML}", builder.ToString());

            return sourceBuilder.ToString();
        }

        public static string GenerateHtml(Page page)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var container in page.Containers)
            {
                if (container.IsFluid)
                {
                    builder.AppendLine("<div class=\"container-fluid\">");
                }
                else
                {
                    builder.AppendLine("<div class=\"container\">");
                }

                foreach (var row in container.Rows)
                {
                    Row(row, builder, 1, false);
                }

                builder.AppendLine("</div>");
            }

            return builder.ToString();
        }

        private static void Row(Row row, StringBuilder builder, int indentLevel, bool isPreview)
        {
            builder.Append('\t', indentLevel);
            if (isPreview)
            {
                builder.AppendLine("<div class=\"row show-grid\">");
            }
            else
            {
                builder.AppendLine("<div class=\"row\">");
            }

            foreach (var column in row.Columns)
            {
                Column(column, builder, indentLevel + 1, isPreview);
            }

            builder.Append('\t', indentLevel);
            builder.AppendLine("</div>");
        }

        private static void Column(Column column, StringBuilder builder, int indentLevel, bool isPreview)
        {
            string classes = string.Join(" ", (
                    from n in column.Sizes
                    select n.ClassName));

            if (column.Offsets.Count > 0)
            {
                classes += " " + string.Join(" ", (
                from n in column.Offsets
                select n.ClassName));
            }

            builder.Append('\t', indentLevel);
            builder.AppendLine("<div class=\"" + classes + "\">");

            string text = string.Join(" ", (
                    from n in column.Sizes
                    select n.HtmlText));

            text += " " + string.Join(" ", (
                from n in column.Offsets
                select n.HtmlText));

            builder.Append('\t', indentLevel + 1);
            builder.AppendLine(text);

            foreach (var row in column.Rows)
            {
                Row(row, builder, indentLevel + 1, isPreview);
            }

            builder.Append('\t', indentLevel);
            builder.AppendLine("</div>");
        }
    }
}
