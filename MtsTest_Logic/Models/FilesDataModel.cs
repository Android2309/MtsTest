using MtsTest_Logic.Interfaces;

namespace MtsTest_Logic.Models
{
    internal class FilesDataModel : IDataModel
    {
        public static IEnumerable<ViewElement> DataList { get; set; }
        public IEnumerable<ViewElement> GetAllData(string path)
        {
            var result = GetFiles(path);

            CleanFromNulls(result);

            return result;
        }

        private List<ViewElement> GetFiles(string path)
        {
            var result = new List<ViewElement>();

            try
            {
                var folders = Directory.GetDirectories(path);

                Parallel.ForEach(folders, folder =>
                {
                    try 
                    { 
                        var files = Directory.GetFiles(folder);

                        foreach (var file in files)
                        {
                            result.Add(new ViewElement()
                            {
                                Name = file,
                                Size = new FileInfo(file).Length
                            });
                        }
                        result.AddRange(GetFiles(folder));
                    }
                    catch { }
                });
            }
            //TODO обработать ошибки
            catch (UnauthorizedAccessException ex) { }
            catch (DirectoryNotFoundException ex) { }
            catch (Exception ex) {  }

            return result;
        }

        //TODO убрать этот костыль, понять откуда беруться null-ы в списке
        private void CleanFromNulls(List<ViewElement> dataList)
        {
            dataList.RemoveAll(x => x == null);
        }

        public IEnumerable<ViewElement> GetOrderByAscData(IEnumerable<ViewElement> folders)
        {
            return folders.OrderBy(x => x.Size);
        }

        public IEnumerable<ViewElement> GetOrderByDescData(IEnumerable<ViewElement> folders)
        {
            return folders.OrderByDescending(x => x.Size);
        }
    }
}
