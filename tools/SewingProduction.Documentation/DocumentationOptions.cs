namespace SewingProduction.Documentation;

public enum DocumentationCommand
{
    Build,
    Check
}

public sealed record DocumentationOptions(
    DocumentationCommand Command,
    string SourceDirectory,
    string HelpOutputDirectory)
{
    public const string Usage = "Использование: dotnet run --project tools/SewingProduction.Documentation -- <build|check> [--source <путь>] [--output <путь>]";

    public static DocumentationOptions Parse(string[] args, string currentDirectory)
    {
        if (args.Length == 0 || args[0] is "--help" or "-h")
        {
            throw new DocumentationUsageException("Не указана команда.");
        }

        var command = args[0].ToLowerInvariant() switch
        {
            "build" => DocumentationCommand.Build,
            "check" => DocumentationCommand.Check,
            _ => throw new DocumentationUsageException($"Неизвестная команда: {args[0]}.")
        };

        string? sourceDirectory = null;
        string? helpOutputDirectory = null;

        for (var index = 1; index < args.Length; index++)
        {
            if (index + 1 >= args.Length)
            {
                throw new DocumentationUsageException($"Для параметра {args[index]} не задано значение.");
            }

            var parameter = args[index];
            var value = Path.GetFullPath(args[++index], currentDirectory);

            switch (parameter)
            {
                case "--source":
                    sourceDirectory = value;
                    break;
                case "--output":
                    helpOutputDirectory = value;
                    break;
                default:
                    throw new DocumentationUsageException($"Неизвестный параметр: {parameter}.");
            }
        }

        if (sourceDirectory is null || helpOutputDirectory is null)
        {
            var repositoryRoot = FindRepositoryRoot(currentDirectory);
            sourceDirectory ??= Path.Combine(repositoryRoot, "SewingProduction_doc");
            helpOutputDirectory ??= Path.Combine(repositoryRoot, "Help");
        }

        return new DocumentationOptions(command, sourceDirectory, helpOutputDirectory);
    }

    private static string FindRepositoryRoot(string currentDirectory)
    {
        var directory = new DirectoryInfo(currentDirectory);
        while (directory is not null)
        {
            if (Directory.Exists(Path.Combine(directory.FullName, "SewingProduction_doc")) &&
                File.Exists(Path.Combine(directory.FullName, "SewingProduction.csproj")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new DocumentationUsageException(
            "Не найден корень SewingProduction. Укажите --source и --output явно.");
    }
}

public sealed class DocumentationUsageException : Exception
{
    public DocumentationUsageException(string message)
        : base(message)
    {
    }
}
