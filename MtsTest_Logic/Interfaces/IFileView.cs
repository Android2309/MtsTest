using MtsTest_Logic.Models;

namespace MtsTest_Logic.Interfaces
{
    public interface IFileView
    {
        IEnumerable<ViewElement> ViewFilesData { set; }
    }
}
