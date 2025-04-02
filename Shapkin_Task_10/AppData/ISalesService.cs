using System.ComponentModel;

namespace Shapkin_Task_10.Classes
{
    public interface ISalesService
    {
        BindingList<SalesData> GetSalesData();
        string GetSaleDetails(SalesData sale);
        void ExportToExcel(SalesData sale);
    }
}
