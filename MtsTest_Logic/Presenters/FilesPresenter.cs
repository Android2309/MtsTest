using MtsTest_Logic.Interfaces;
using MtsTest_Logic.Models;

namespace MtsTest_Logic.Presenters
{
    public class FilesPresenter
    {
        IDataModel model;
        IFileView view;
        public FilesPresenter(IFileView View)
        {
            this.view = View;

            model = new FilesDataModel();
        }

        public async void SetFilesData()
        {
            //TODO вынести директорию для сканирования в конфиги или UI
            //для простоты отладки взята директория C:\Program Files, 
            List<ViewElement> filesData = await Task.Run(() => model.GetAllData(@"C:\Program Files").ToList());

            view.ViewFilesData = FilesDataModel.DataList = filesData;
        }

        public async void OrderFilesByASc()
        {
            List<ViewElement> filesData = await Task.Run(() => model.GetOrderByAscData(FilesDataModel.DataList).ToList());

            view.ViewFilesData = filesData;
        }

        public async void OrderFilesByDesc()
        {
            List<ViewElement> filesData = await Task.Run(() => model.GetOrderByDescData(FilesDataModel.DataList).ToList());

            view.ViewFilesData = filesData;
        }
    }
}
