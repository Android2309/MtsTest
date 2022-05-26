using MtsTest_Logic.Interfaces;

namespace MtsTest_Logic.Models
{
    public class FoldersDataModel : IDataModel
    {
        public static IEnumerable<ViewElement> DataList { get; set; }
        public IEnumerable<ViewElement> GetAllData(string path)
        {
            var result = GetFoldersWithSizeOfLevel(path);

            SetDataSize(result);

            return result;
        }

        /// <summary>
        /// Получить список папок с рамером всех файлов на подуровне иерархии
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private List<ViewElement> GetFoldersWithSizeOfLevel(string path)
        {
            var result = new List<ViewElement>();

            try
            {
                string[] folders = Directory.GetDirectories(path);

                Parallel.ForEach(folders, folder =>
                {
                    long levelSize = GetSizeOfLevel(folder);

                    result.Add(new ViewElement()
                    {
                        Name = folder,
                        Size = levelSize
                    });

                    var foldersToAdd = GetFoldersWithSizeOfLevel(folder);

                    result.AddRange(foldersToAdd);
                });
            }
            //TODO обработать ошибки
            catch (UnauthorizedAccessException ex) { }
            catch (DirectoryNotFoundException ex) { }
            catch (Exception ex) { }

            return result;
        }
        public long GetSizeOfLevel(string folder)
        {
            long levelSize = 0;
            try
            {
                levelSize = new DirectoryInfo(folder).GetFiles("", SearchOption.TopDirectoryOnly).Sum(p => p.Length);
            }
            catch (Exception ex) { }

            return levelSize;
        }
        private void SetDataSize(List<ViewElement> dataList)
        {
            CleanFromNulls(dataList);

            var tempList = new List<ViewElement>();

            tempList.AddRange(dataList);


            for (int i = 0; i < dataList.Count();i++)
            {
                for(int j = 0; j < tempList.Count(); j ++)
                {
                    if (tempList[j].Name.Contains(dataList[i].Name))
                        dataList[i].Size += tempList[j].Size;
                }
            }
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
