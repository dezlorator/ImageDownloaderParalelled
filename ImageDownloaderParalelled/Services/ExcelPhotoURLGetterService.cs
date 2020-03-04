using ClosedXML.Excel;
using ImageDownloaderParalelled.Interfaces;
using ImageDownloaderParalelled.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloaderParalelled.Services
{
    class ExcelPhotoURLGetterService : IExcelPhotoURLGetterService
    {
        #region fields
        private List<CustomUrl> urlList;
        private int step;
        private int currentPosition = 0;
        private readonly IStepCounterService stepCounter;
        private readonly IFileValidator fileValidator;
        #endregion

        #region ctor
        public ExcelPhotoURLGetterService(IStepCounterService stepCounter, IFileValidator fileValidator)
        {
            this.stepCounter = stepCounter;
            this.fileValidator = fileValidator;
            urlList = new List<CustomUrl>(20);
        }
        #endregion

        public IEnumerable<string> GetUrlFromFile(string pathToFile)
        {
            urlList.Clear();

            if (!fileValidator.IsFileExist(pathToFile))
            {
                return null;
            }

            using (XLWorkbook workBook = new XLWorkbook(pathToFile))
            {
                foreach (IXLWorksheet worksheet in workBook.Worksheets)
                {
                    step = stepCounter.GetStep(worksheet.RowsUsed().Count() - currentPosition);
                    int stopPosition = currentPosition + step;
                    Parallel.For(currentPosition, stopPosition, currentPosition =>
                    {
                        urlList.Add( new CustomUrl
                        { 
                            UrlString = worksheet.RowsUsed().ElementAt(currentPosition).Cell(1).Value.ToString(),
                            UrlIndex = currentPosition
                        });
                    });
                    if (urlList.Count() != step)
                    {
                        var indexesOfUrlsNotProsessedByParallelFor = Enumerable.Range(0, step).
                                                                     Except(urlList.Select(p => p.UrlIndex));
                        foreach(int urlInder in indexesOfUrlsNotProsessedByParallelFor)
                        {
                            urlList.Add(new CustomUrl
                            {
                                UrlString = worksheet.RowsUsed().ElementAt(urlInder).Cell(1).Value.ToString(),
                                UrlIndex = currentPosition
                            });
                        }
                    }
                }

                currentPosition += step;
            }

            return urlList.Select(p => p.UrlString);
        }
    }
}
