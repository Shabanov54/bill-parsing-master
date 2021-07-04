namespace ParserBill
{
    public interface IFileContext
    {
        string PatternFileName { get; }
        string WorkDir { get; }

        string[] GetBillRows(string fileName);
        string[] GetFileList(string workDir, string patternFileName);
    }
}