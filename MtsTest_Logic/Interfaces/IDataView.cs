using MtsTest_Logic.Models;

namespace MtsTest_Logic.Interfaces
{
    public interface IDataView
    {
        IEnumerable<ViewElement> ViewData { set; }
    }
}
