using MtsTest_Logic.Interfaces;
using MtsTest_Logic.Models;

namespace MtsTest_Logic.Presenters
{
    public class FoldersPresenter
    {
        IDataModel model;
        IFolderView view;

        public FoldersPresenter(IFolderView View)
        {
            this.view = View;

            model = new FoldersDataModel();
        }

        public async void SetFoldersData()
        {
            List<ViewElement> foldersData = await Task.Run(() => model.GetAllData(@"C:\Program Files").ToList());

            view.ViewFoldersData = FoldersDataModel.DataList = foldersData;
        }

        public async void OrderFoldersByASc()
        {
            List<ViewElement> foldersData = await Task.Run(() => model.GetOrderByAscData(FoldersDataModel.DataList).ToList());

            view.ViewFoldersData = foldersData;
        }

        public async void OrderFoldersByDesc()
        {
            List<ViewElement> foldersData = await Task.Run(() => model.GetOrderByDescData(FoldersDataModel.DataList).ToList());

            view.ViewFoldersData = foldersData;
        }
    }
}
