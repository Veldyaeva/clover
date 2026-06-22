using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Markdig;

namespace SewingProduction.Documentation;

public static class DocumentationApplication
{
    private static readonly string[] RequiredUserMetadata =
    [
        "title",
        "audience",
        "module",
        "formType",
        "helpPath",
        "reviewedOn",
        "sourcePath"
    ];

    private static readonly Regex WikiLinkPattern = new(
        @"(?<!\!)\[\[([^\]|#]+)(?:#[^\]|]+)?(?:\|[^\]]+)?\]\]",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private static readonly Regex MarkdownLinkPattern = new(
        @"(?<!!)\[[^\]]*\]\((?<target>[^)]+)\)",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private static readonly Regex MarkdownImagePattern = new(
        @"!\[[^\]]*\]\((?<target>[^)]+)\)",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private static readonly MarkdownPipeline MarkdownPipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .UseAutoLinks()
        .Build();

    public static int Execute(DocumentationOptions options, TextWriter output, TextWriter error)
    {
        var result = ValidateAndRender(options.SourceDirectory, options.HelpOutputDirectory);
        if (result.Errors.Count > 0)
        {
            foreach (var validationError in result.Errors)
            {
                error.WriteLine($"- {validationError}");
            }

            return 1;
        }

        if (options.Command == DocumentationCommand.Build)
        {
            foreach (var page in result.Pages)
            {
                WriteUtf8(page.FullPath, page.Content);
            }

            output.WriteLine($"Сгенерировано файлов справки: {result.Pages.Count}.");
            return 0;
        }

        var stalePages = result.Pages
            .Where(page => !File.Exists(page.FullPath) ||
                           !string.Equals(File.ReadAllText(page.FullPath), page.Content, StringComparison.Ordinal))
            .Select(page => Path.GetRelativePath(options.HelpOutputDirectory, page.FullPath))
            .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        if (stalePages.Length > 0)
        {
            error.WriteLine("HTML-справка не синхронизирована с Markdown. Выполните команду build:");
            foreach (var stalePage in stalePages)
            {
                error.WriteLine($"- {stalePage}");
            }

            return 1;
        }

        output.WriteLine($"Проверка документации пройдена. Проверено файлов справки: {result.Pages.Count}.");
        return 0;
    }

    private static DocumentationRenderResult ValidateAndRender(string sourceDirectory, string helpOutputDirectory)
    {
        var errors = new List<string>();
        if (!Directory.Exists(sourceDirectory))
        {
            errors.Add($"Не найдена папка документации: {sourceDirectory}");
            return new DocumentationRenderResult(errors, []);
        }

        var projectDirectory = Directory.GetParent(sourceDirectory)?.FullName;
        if (string.IsNullOrWhiteSpace(projectDirectory))
        {
            errors.Add($"Не удалось определить папку проекта для: {sourceDirectory}");
            return new DocumentationRenderResult(errors, []);
        }

        var articles = Directory
            .EnumerateFiles(sourceDirectory, "*.md", SearchOption.AllDirectories)
            .Where(path => !path.Contains($"{Path.DirectorySeparatorChar}.obsidian{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
            .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
            .Select(path => DocumentationArticle.Parse(path, sourceDirectory))
            .ToArray();

        ValidateMarkdownLinks(articles, errors);

        var userArticles = articles
            .Where(article => string.Equals(article.MetadataValue("audience"), "user", StringComparison.OrdinalIgnoreCase))
            .OrderBy(article => article.MetadataValue("module"), StringComparer.OrdinalIgnoreCase)
            .ThenBy(article => article.MetadataValue("title"), StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var generatedPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var article in userArticles)
        {
            ValidateUserArticle(article, projectDirectory, helpOutputDirectory, generatedPaths, errors);
        }

        if (errors.Count > 0)
        {
            return new DocumentationRenderResult(errors, []);
        }

        var pages = new List<GeneratedPage>();
        foreach (var article in userArticles)
        {
            var helpPath = article.MetadataValue("helpPath")!;
            var outputPath = ResolveOutputPath(helpOutputDirectory, helpPath);
            pages.Add(new GeneratedPage(outputPath, RenderArticle(article)));
        }

        pages.Add(new GeneratedPage(
            Path.Combine(helpOutputDirectory, "Help.html"),
            RenderHelpIndex(userArticles, helpOutputDirectory, generatedPaths)));

        return new DocumentationRenderResult(errors, pages);
    }

    private static void ValidateMarkdownLinks(IEnumerable<DocumentationArticle> articles, ICollection<string> errors)
    {
        var articleNames = articles
            .GroupBy(article => Path.GetFileNameWithoutExtension(article.FullPath), StringComparer.OrdinalIgnoreCase)
            .ToDictionary(group => group.Key, group => group.Count(), StringComparer.OrdinalIgnoreCase);

        foreach (var article in articles)
        {
            foreach (Match match in WikiLinkPattern.Matches(article.Markdown))
            {
                var target = match.Groups[1].Value.Trim();
                if (!articleNames.TryGetValue(target, out var count))
                {
                    errors.Add($"{article.RelativePath}: не найдена Obsidian-ссылка [[{target}]].");
                }
                else if (count > 1)
                {
                    errors.Add($"{article.RelativePath}: Obsidian-ссылка [[{target}]] неоднозначна.");
                }
            }

            ValidateRelativeTargets(article, MarkdownLinkPattern, "ссылка", errors);
            ValidateRelativeTargets(article, MarkdownImagePattern, "изображение", errors);
        }
    }

    private static void ValidateRelativeTargets(
        DocumentationArticle article,
        Regex pattern,
        string targetType,
        ICollection<string> errors)
    {
        foreach (Match match in pattern.Matches(article.Markdown))
        {
            var target = match.Groups["target"].Value.Trim();
            if (string.IsNullOrWhiteSpace(target) ||
                target.StartsWith('#') ||
                Uri.TryCreate(target, UriKind.Absolute, out _))
            {
                continue;
            }

            var cleanTarget = target.Split('#', 2)[0].Trim();
            var targetPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(article.FullPath)!, cleanTarget));
            if (!File.Exists(targetPath))
            {
                errors.Add($"{article.RelativePath}: не найдено {targetType} {target}.");
            }
        }
    }

    private static void ValidateUserArticle(
        DocumentationArticle article,
        string projectDirectory,
        string helpOutputDirectory,
        ISet<string> generatedPaths,
        ICollection<string> errors)
    {
        if (!article.RelativePath.StartsWith($"05_UserGuide{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
        {
            errors.Add($"{article.RelativePath}: пользовательская статья должна находиться в 05_UserGuide.");
        }

        foreach (var metadataName in RequiredUserMetadata)
        {
            if (string.IsNullOrWhiteSpace(article.MetadataValue(metadataName)))
            {
                errors.Add($"{article.RelativePath}: не задано поле front matter {metadataName}.");
            }
        }

        var reviewedOn = article.MetadataValue("reviewedOn");
        if (!string.IsNullOrWhiteSpace(reviewedOn) && !DateOnly.TryParse(reviewedOn, out _))
        {
            errors.Add($"{article.RelativePath}: reviewedOn должен быть датой в формате YYYY-MM-DD.");
        }

        var helpPath = article.MetadataValue("helpPath");
        if (!string.IsNullOrWhiteSpace(helpPath))
        {
            if (!helpPath.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
            {
                errors.Add($"{article.RelativePath}: helpPath должен указывать на HTML-файл.");
            }
            else
            {
                var outputPath = ResolveOutputPath(helpOutputDirectory, helpPath);
                if (!generatedPaths.Add(outputPath))
                {
                    errors.Add($"{article.RelativePath}: helpPath дублирует другую пользовательскую статью.");
                }
            }
        }

        var sourcePath = article.MetadataValue("sourcePath");
        if (!string.IsNullOrWhiteSpace(sourcePath))
        {
            var absoluteSourcePath = Path.GetFullPath(Path.Combine(projectDirectory, sourcePath));
            if (!File.Exists(absoluteSourcePath))
            {
                errors.Add($"{article.RelativePath}: не найден исходный файл {sourcePath}.");
            }
            else if (!string.IsNullOrWhiteSpace(article.MetadataValue("formType")))
            {
                ValidateFormSource(article, absoluteSourcePath, errors);
            }
        }

        var formType = article.MetadataValue("formType");
        if (!string.IsNullOrWhiteSpace(formType) && !string.IsNullOrWhiteSpace(helpPath))
        {
            const string namespacePrefix = "SewingProduction.";
            if (!formType.StartsWith(namespacePrefix, StringComparison.Ordinal))
            {
                errors.Add($"{article.RelativePath}: formType должен начинаться с {namespacePrefix}.");
                return;
            }

            var expectedHelpPath = formType[namespacePrefix.Length..].Replace('.', Path.DirectorySeparatorChar) + ".html";
            if (!string.Equals(NormalizePath(helpPath), NormalizePath(expectedHelpPath), StringComparison.OrdinalIgnoreCase))
            {
                errors.Add($"{article.RelativePath}: helpPath не соответствует formType. Ожидается {expectedHelpPath}.");
            }
        }
    }

    private static void ValidateFormSource(
        DocumentationArticle article,
        string sourcePath,
        ICollection<string> errors)
    {
        var formType = article.MetadataValue("formType")!;
        var className = formType[(formType.LastIndexOf('.') + 1)..];
        var sourceCode = File.ReadAllText(sourcePath);

        if (!Regex.IsMatch(sourceCode, $@"\bnamespace\s+{Regex.Escape(formType[..formType.LastIndexOf('.')])}\b"))
        {
            errors.Add($"{article.RelativePath}: sourcePath не содержит namespace {formType[..formType.LastIndexOf('.')]}.");
        }

        if (!Regex.IsMatch(sourceCode, $@"\bclass\s+{Regex.Escape(className)}\b"))
        {
            errors.Add($"{article.RelativePath}: sourcePath не содержит класс {className}.");
        }
    }

    private static string RenderArticle(DocumentationArticle article)
    {
        var title = article.MetadataValue("title")!;
        var body = Markdown.ToHtml(article.Markdown, MarkdownPipeline);
        return """
<!DOCTYPE html>
<html lang="ru">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>__TITLE__</title>
  <style>
    body { font-family: "Segoe UI", Arial, sans-serif; line-height: 1.55; max-width: 960px; margin: 0 auto; padding: 24px; color: #1f2937; }
    h1, h2, h3 { color: #1d4f7a; }
    h1 { border-bottom: 1px solid #d1d5db; padding-bottom: 10px; }
    code, pre { font-family: Consolas, "Courier New", monospace; }
    pre { overflow-x: auto; background: #f3f4f6; padding: 12px; border-radius: 4px; }
    table { border-collapse: collapse; width: 100%; }
    th, td { border: 1px solid #d1d5db; padding: 8px; text-align: left; }
    th { background: #eef5fb; }
    img { max-width: 100%; height: auto; }
  </style>
</head>
<body>
__BODY__
</body>
</html>
""".Replace("__TITLE__", WebUtility.HtmlEncode(title), StringComparison.Ordinal)
   .Replace("__BODY__", body, StringComparison.Ordinal);
    }

    private static string RenderHelpIndex(
        IReadOnlyCollection<DocumentationArticle> userArticles,
        string helpOutputDirectory,
        ISet<string> generatedPaths)
    {
        var builder = new StringBuilder();
        builder.AppendLine("<!DOCTYPE html>");
        builder.AppendLine("<html lang=\"ru\">");
        builder.AppendLine("<head><meta charset=\"utf-8\"><title>Справка SewingProduction</title>");
        builder.AppendLine("<style>body{font-family:Segoe UI,Arial,sans-serif;max-width:960px;margin:0 auto;padding:24px;color:#1f2937}h1,h2{color:#1d4f7a}ul{padding-left:20px}li{margin:6px 0}a{color:#0b5cab}</style></head>");
        builder.AppendLine("<body><h1>Справка SewingProduction</h1>");
        builder.AppendLine("<p>Страницы этого раздела формируются из базы знаний <code>SewingProduction_doc</code>.</p>");

        foreach (var moduleGroup in userArticles.GroupBy(article => article.MetadataValue("module"), StringComparer.OrdinalIgnoreCase))
        {
            builder.AppendLine($"<h2>{WebUtility.HtmlEncode(moduleGroup.Key)}</h2><ul>");
            foreach (var article in moduleGroup.OrderBy(article => article.MetadataValue("title"), StringComparer.OrdinalIgnoreCase))
            {
                var title = WebUtility.HtmlEncode(article.MetadataValue("title"));
                var helpPath = NormalizeUrl(article.MetadataValue("helpPath")!);
                builder.AppendLine($"<li><a href=\"{helpPath}\">{title}</a></li>");
            }

            builder.AppendLine("</ul>");
        }

        var legacyPages = Directory.Exists(helpOutputDirectory)
            ? Directory.EnumerateFiles(helpOutputDirectory, "*.html", SearchOption.AllDirectories)
                .Where(path => !path.EndsWith("Help.html", StringComparison.OrdinalIgnoreCase))
                .Where(path => !path.EndsWith(".preview.html", StringComparison.OrdinalIgnoreCase))
                .Where(path => !generatedPaths.Contains(path))
                .Select(path => Path.GetRelativePath(helpOutputDirectory, path))
                .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
                .ToArray()
            : [];

        if (legacyPages.Length > 0)
        {
            builder.AppendLine("<h2>Наследуемые инструкции</h2><ul>");
            foreach (var legacyPage in legacyPages)
            {
                var title = WebUtility.HtmlEncode(Path.GetFileNameWithoutExtension(legacyPage));
                builder.AppendLine($"<li><a href=\"{NormalizeUrl(legacyPage)}\">{title}</a></li>");
            }

            builder.AppendLine("</ul>");
        }

        builder.AppendLine("</body></html>");
        return builder.ToString();
    }

    private static string ResolveOutputPath(string helpOutputDirectory, string relativeHelpPath)
    {
        var outputRoot = Path.GetFullPath(helpOutputDirectory)
            .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
        var outputPath = Path.GetFullPath(Path.Combine(helpOutputDirectory, relativeHelpPath));
        if (!outputPath.StartsWith(outputRoot, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"Недопустимый путь справки: {relativeHelpPath}");
        }

        return outputPath;
    }

    private static string NormalizePath(string path) =>
        path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

    private static string NormalizeUrl(string path) =>
        path.Replace(Path.DirectorySeparatorChar, '/').Replace(Path.AltDirectorySeparatorChar, '/');

    private static void WriteUtf8(string path, string content)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllText(path, content, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
    }
}

public sealed record DocumentationRenderResult(
    IReadOnlyCollection<string> Errors,
    IReadOnlyCollection<GeneratedPage> Pages);

public sealed record GeneratedPage(string FullPath, string Content);

public sealed class DocumentationArticle
{
    private DocumentationArticle(
        string fullPath,
        string relativePath,
        IReadOnlyDictionary<string, string> metadata,
        string markdown)
    {
        FullPath = fullPath;
        RelativePath = relativePath;
        Metadata = metadata;
        Markdown = markdown;
    }

    public string FullPath { get; }

    public string RelativePath { get; }

    public IReadOnlyDictionary<string, string> Metadata { get; }

    public string Markdown { get; }

    public string? MetadataValue(string name) =>
        Metadata.TryGetValue(name, out var value) ? value : null;

    public static DocumentationArticle Parse(string fullPath, string sourceDirectory)
    {
        var content = File.ReadAllText(fullPath);
        var metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var markdown = content;

        if (content.StartsWith("---", StringComparison.Ordinal))
        {
            var normalizedContent = content.Replace("\r\n", "\n", StringComparison.Ordinal);
            var closingMarker = normalizedContent.IndexOf("\n---\n", StringComparison.Ordinal);
            if (closingMarker < 0)
            {
                throw new InvalidOperationException($"{fullPath}: не закрыт блок front matter.");
            }

            var header = normalizedContent[4..closingMarker];
            foreach (var line in header.Split('\n'))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var delimiterIndex = line.IndexOf(':');
                if (delimiterIndex <= 0)
                {
                    throw new InvalidOperationException($"{fullPath}: некорректная строка front matter: {line}");
                }

                var key = line[..delimiterIndex].Trim();
                var value = line[(delimiterIndex + 1)..].Trim().Trim('"');
                metadata[key] = value;
            }

            markdown = normalizedContent[(closingMarker + "\n---\n".Length)..];
        }

        return new DocumentationArticle(
            fullPath,
            Path.GetRelativePath(sourceDirectory, fullPath),
            metadata,
            markdown);
    }
}
