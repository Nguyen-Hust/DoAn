using System;
using System.Globalization;
using System.Threading;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using Org.BouncyCastle.Asn1.Ocsp;

namespace WebAPI.Controllers
{
	public static class ExcelHelper
	{
        public static List<T> Import<T>(string filePath) where T : new()
        {
            try
            {
                XSSFWorkbook workbook;
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(stream);
                }

                var sheet = workbook.GetSheetAt(0);

                var rowHeader = sheet.GetRow(2);
                var row2 = rowHeader.Cells;
                var cellIndex = rowHeader.LastCellNum;
                var colIndexList = new Dictionary<string, int>();
                foreach (var cell in rowHeader.Cells)
                {
                    var colName = cell.StringCellValue;
                    colIndexList.Add(colName, cell.ColumnIndex);
                }

                var listResult = new List<T>();
                var currentRow = 3;
                while (currentRow <= sheet.LastRowNum)
                {
                    var row = sheet.GetRow(currentRow);
                    if (row == null) break;

                    var obj = new T();

                    foreach (var property in typeof(T).GetProperties())
                    {
                        if (!colIndexList.ContainsKey(property.Name))
                            throw new Exception($"Column {property.Name} not found.");

                        var colIndex = colIndexList[property.Name];
                        var cell = row.GetCell(colIndex);

                        if (cell == null)
                        {
                            property.SetValue(obj, null);
                        }
                        else if (property.PropertyType == typeof(string))
                        {
                            cell.SetCellType(CellType.String);
                            property.SetValue(obj, cell.StringCellValue);
                        }
                        else if (property.PropertyType == typeof(Nullable<Int32>))
                        {
                            cell.SetCellType(CellType.Numeric);
                            property.SetValue(obj, Convert.ToInt32(cell.NumericCellValue));
                        }
                        else if (property.PropertyType == typeof(int))
                        {
                            cell.SetCellType(CellType.Numeric);
                            property.SetValue(obj, Convert.ToInt32(cell.NumericCellValue));
                        }
                        else if (property.PropertyType == typeof(decimal))
                        {
                            cell.SetCellType(CellType.Numeric);
                            property.SetValue(obj, Convert.ToDecimal(cell.NumericCellValue));
                        }
                        else if (property.PropertyType == typeof(Nullable<Decimal>))
                        {
                            cell.SetCellType(CellType.Numeric);
                            property.SetValue(obj, Convert.ToDecimal(cell.NumericCellValue));
                        }
                        else if (property.PropertyType == typeof(DateTime))
                        {
                            var inputDate = ValidateDate(cell.StringCellValue);
                            property.SetValue(obj, inputDate);
                        }
                        else if (property.PropertyType == typeof(bool))
                        {
                            cell.SetCellType(CellType.Boolean);
                            property.SetValue(obj, cell.BooleanCellValue);
                        }
                        else
                        {
                            property.SetValue(obj, Convert.ChangeType(cell.StringCellValue, property.PropertyType));
                        }
                    }

                    listResult.Add(obj);
                    currentRow++;
                }

                return listResult;
            }catch(Exception ex)
            {
                return null;
            }
        }

        private static DateTime? ValidateDate(string dateTime)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(dateTime.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static async Task<List<List<string>>> CommonReadValueImportExcel(IFormFile File, int SheetIndex, int StartRowIndex, int MaxOfColumns)
        {
            var ret = new List<List<string>>();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using var stream = new MemoryStream();
            await File.CopyToAsync(stream);
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;
            for (var row = StartRowIndex; row <= rowCount; row++)
            {
                var values = GetValueRow(worksheet, row, MaxOfColumns);
                if (values?.Any(x => !string.IsNullOrEmpty(x)) == true)
                {
                    ret.Add(values);
                }
            }
            return ret;
        }

        private static List<string> GetValueRow(ExcelWorksheet worksheet, int row, int maxColumns)
        {
            var ret = new List<string>();
            for (var cell = 1; cell <= maxColumns; cell++)
            {
                ret.Add(GetValueCell(worksheet, row, cell));
            }
            return ret;
        }
        private static string GetValueCell(ExcelWorksheet worksheet, int row, int cell)
        {
            try
            {
                var value = worksheet.Cells[row, cell]?.Value?.ToString()?.Trim();
                if (string.IsNullOrWhiteSpace(value))
                {
                    return null;
                }
                return value;
            }
            catch
            {
                return null;
            }

        }

    }
}

