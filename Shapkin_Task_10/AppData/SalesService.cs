using Shapkin_Task_10.Classes;
using Shapkin_Task_10.Forms;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Shapkin_Task_10.AppData
{
    public class SalesService : ISalesService
    {
        public BindingList<SalesData> GetSalesData()
        {
            var salesData = (from sale in Program.context.Sales
                             join item in Program.context.Items on sale.ItemCode equals item.ItemCode
                             select new SalesData
                             {
                                 SaleCode = sale.SaleCode,
                                 AuctionDate = sale.AuctionDate,
                                 StartingPrice = sale.StartingPrice,
                                 FinalPrice = sale.FinalPrice,
                                 Sold = sale.Sold,
                                 BuyerLastName = sale.BuyerLastName,
                                 ItemName = item.ItemName
                             }).ToList();

            return new BindingList<SalesData>(salesData);
        }

        /// <summary>
        /// PKGH Получает подробную информацию о продаже.
        /// </summary>
        /// <param name="sale">Данные о продаже.</param>
        /// <returns>Строка с подробной информацией о продаже.</returns>
        public string GetSaleDetails(SalesData sale)
        {
            StringBuilder details = new StringBuilder();
            details.AppendLine($"Название предмета: {sale.ItemName}");
            details.AppendLine($"Дата торгов: {sale.AuctionDate.ToShortDateString()}");
            details.AppendLine($"Начальная цена: {sale.StartingPrice:N2}");
            if (sale.FinalPrice.HasValue)
                details.AppendLine($"Финальная цена: {sale.FinalPrice.Value:N2}");
            else
                details.AppendLine("Финальная цена: Не продано");

            details.AppendLine($"Продано: {(sale.Sold ? "Да" : "Нет")}");
            details.AppendLine($"Фамилия покупателя: {(string.IsNullOrEmpty(sale.BuyerLastName) ? "Неизвестно" : sale.BuyerLastName)}");

            return details.ToString();
        }

        /// <summary>
        /// PKGH Экспортирует данные о продаже в Excel.
        /// </summary>
        /// <param name="sale">Данные о продаже.</param>
        public void ExportToExcel(SalesData sale)
        {
            try
            {
                // Создаем Excel-приложение
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = workbook.Sheets[1]; // Получаем первый лист

                // Заполняем ячейки данными
                worksheet.Cells[1, 1] = "Название предмета:";
                worksheet.Cells[1, 2] = sale.ItemName;

                worksheet.Cells[2, 1] = "Дата торгов:";
                worksheet.Cells[2, 2] = sale.AuctionDate.ToShortDateString();

                worksheet.Cells[3, 1] = "Начальная цена:";
                worksheet.Cells[3, 2] = sale.StartingPrice.ToString("N2");

                worksheet.Cells[4, 1] = "Финальная цена:";
                worksheet.Cells[4, 2] = sale.FinalPrice.HasValue ? sale.FinalPrice.Value.ToString("N2") : "Не продано";

                worksheet.Cells[5, 1] = "Продано:";
                worksheet.Cells[5, 2] = sale.Sold ? "Да" : "Нет";

                worksheet.Cells[6, 1] = "Фамилия покупателя:";
                worksheet.Cells[6, 2] = string.IsNullOrEmpty(sale.BuyerLastName) ? "Неизвестно" : sale.BuyerLastName;

                // Сохраняем файл Excel
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = $"Карточка продажи - {sale.ItemName} - {sale.AuctionDate.ToShortDateString()}.xlsx";
                saveFileDialog.DefaultExt = "xlsx";
                saveFileDialog.Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    excelApp.Quit();
                    MessageBox.Show("Карточка успешно экспортирована в Excel!", "Экспорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    excelApp.Quit(); // Закрываем Excel, если пользователь отменил сохранение
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при экспорте в Excel: " + ex.Message);
            }
        }
    }
}
