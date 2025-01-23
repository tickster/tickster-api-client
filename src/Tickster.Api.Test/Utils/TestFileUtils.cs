namespace Tickster.Api.Test.Utils;
internal static class TestFileUtils
{
    public static string GetTestFileContent(string fileName)
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResponses", fileName);
        return File.ReadAllText(filePath);
    }
}
