namespace SewingProduction.Documentation;

internal static class Program
{
    private static int Main(string[] args)
    {
        try
        {
            var options = DocumentationOptions.Parse(args, Directory.GetCurrentDirectory());
            return DocumentationApplication.Execute(options, Console.Out, Console.Error);
        }
        catch (DocumentationUsageException exception)
        {
            Console.Error.WriteLine(exception.Message);
            Console.Error.WriteLine();
            Console.Error.WriteLine(DocumentationOptions.Usage);
            return 2;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine($"Ошибка генератора документации: {exception.Message}");
            return 1;
        }
    }
}
