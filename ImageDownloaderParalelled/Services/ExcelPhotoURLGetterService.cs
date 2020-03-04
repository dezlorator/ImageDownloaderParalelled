using ClosedXML.Excel;
using ImageDownloaderParalelled.Interfaces;
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
        private List<string> urlList;
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
            urlList = new List<string>(20);
        }
        #endregion

        public IEnumerable<string> GetUrlFromFile(string pathToFile)
        {
            urlList.Clear();

            if(!fileValidator.IsFileExist(pathToFile))
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
                        var a = worksheet.RowsUsed();

                        urlList.Add(worksheet.RowsUsed().ElementAt(currentPosition).Cell(1).Value.ToString());
                    });
                }

                currentPosition += step;
            }

            return urlList;
        }
    }
}
