using MtsTest_Logic.Models;

namespace MtsTest_Logic.Interfaces
{
    public interface IFolderView
    {
        IEnumerable<ViewElement> ViewFoldersData { set; }
    }
}
