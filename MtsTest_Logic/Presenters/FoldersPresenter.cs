using MtsTest_Logic.Interfaces;
using MtsTest_Logic.Models;

namespace MtsTest_Logic.Presenters
{
    public class FoldersPresenter
    {
        IDataModel model;
        IDataView view;

        public FoldersPresenter(IDataView View)
        {
            this.view = View;

            model = new FoldersDataModel();
        }

        public void SetFoldersData()
        {
            List<ViewElement> foldersData = model.GetAllData(@"C:\").ToList();

            view.ViewData = foldersData;
        }

    }
}
