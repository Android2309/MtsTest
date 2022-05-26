using MtsTest_Logic.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace MtsTest_Logic.Models
{
    public class FoldersDataModel : IDataModel
    {
        public IEnumerable<ViewElement> GetAllData(string path)
        {
            var result = GetFoldersWithSizeOfLevel(path);

            int index = 0;
            SetDataSize(result, ref index);

            return result;
        }

        private void SetDataSize(List<ViewElement> dataList, ref int startIndex)
        {
            var result = dataList;

            try
            {
                for (int i = startIndex; i < dataList.Count(); i++)
                {
                    if (dataList[i + 1].Name.Contains(dataList[startIndex].Name))
                        dataList[startIndex].Size += dataList[i + 1].Size;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            startIndex++;
            if (startIndex < dataList.Count())
                SetDataSize(dataList, ref startIndex);
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
                    result.AddRange(GetFoldersWithSizeOfLevel(folder));
                });
            }
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

        public IEnumerable<ViewElement> GetOrderByAscData(IEnumerable<ViewElement> folders)
        {
            List<ViewElement> result = new List<ViewElement>();

            return result;
        }

        public IEnumerable<ViewElement> GetOrderByDescData(IEnumerable<ViewElement> folders)
        {
            List<ViewElement> result = new List<ViewElement>();

            return result;
        }
    }
}
