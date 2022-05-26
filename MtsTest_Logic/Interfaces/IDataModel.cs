using MtsTest_Logic.Models;

namespace MtsTest_Logic.Interfaces
{
    public interface IDataModel
    {
        IEnumerable<ViewElement> GetAllData(string startPath);
        IEnumerable<ViewElement> GetOrderByDescData(IEnumerable<ViewElement> elementsList);
        IEnumerable<ViewElement> GetOrderByAscData(IEnumerable<ViewElement> elementsList);
    }
}
