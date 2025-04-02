using Shapkin_Task_10.Classes;
using Shapkin_Task_10.Properties;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Shapkin_Task_10.Forms
{
    /// <summary>
    /// PKGH Форма для отображения и экспорта данных о продажах.
    /// </summary>
    public partial class fmSales : Form
    {
        private readonly ISalesService _salesService;
        private BindingSource _salesBindingSource = new BindingSource();
        private BindingList<SalesData> _salesData;
        private int _currentIndex = 0;
        private bool _userSelection = false;

        public fmSales(ISalesService salesService)
        {
            InitializeComponent();
            Icon = Resources.shopCart1;
            _salesService = salesService ?? throw new ArgumentNullException(nameof(salesService));
            LoadSalesData();
            dataGridViewSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// PKGH Загружает данные о продажах в DataGridView.
        /// </summary>
        private void LoadSalesData()
        {
            try
            {
                _salesData = _salesService.GetSalesData();
                _salesBindingSource.DataSource = _salesData;
                dataGridViewSales.DataSource = _salesBindingSource;

                dataGridViewSales.Columns["SaleCode"].HeaderText = "Код продажи";
                dataGridViewSales.Columns["AuctionDate"].HeaderText = "Дата торгов";
                dataGridViewSales.Columns["StartingPrice"].HeaderText = "Начальная цена";
                dataGridViewSales.Columns["FinalPrice"].HeaderText = "Финальная цена";
                dataGridViewSales.Columns["Sold"].HeaderText = "Продано";
                dataGridViewSales.Columns["BuyerLastName"].HeaderText = "Фамилия покупателя";
                dataGridViewSales.Columns["ItemName"].HeaderText = "Название предмета";

                dataGridViewSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewSales.Columns["SaleCode"].Visible = false;

                UpdateDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных о продажах: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// PKGH Обновляет отображение данных в DataGridView и TextBox.
        /// </summary>
        private void UpdateDisplay()
        {
            if (_salesData != null && _salesData.Count > 0)
            {
                if (_currentIndex >= 0 && _currentIndex < _salesData.Count)
                {
                    dataGridViewSales.ClearSelection();

                    dataGridViewSales.Rows[_currentIndex].Selected = true;

                    dataGridViewSales.FirstDisplayedScrollingRowIndex = _currentIndex;
                    dataGridViewSales.Focus();

                    dataGridViewSales_SelectionChanged(dataGridViewSales, EventArgs.Empty);
                }
                else
                    textBoxDetails.Clear();
            }
            else
                textBoxDetails.Clear();

            _userSelection = false;
        }

        private void dataGridViewSales_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewSales.SelectedRows.Count > 0)
            {
                if (dataGridViewSales.SelectedRows[0].DataBoundItem is SalesData selectedRow)
                {
                    if (!_userSelection)
                        _currentIndex = _salesData.IndexOf(selectedRow);
                    textBoxDetails.Text = _salesService.GetSaleDetails(selectedRow);
                }
            }
            else
                textBoxDetails.Clear();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (dataGridViewSales.SelectedRows.Count > 0)
                if (dataGridViewSales.SelectedRows[0].DataBoundItem is SalesData selectedRow)
                {
                    try
                    {
                        _salesService.ExportToExcel(selectedRow);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при экспорте в Excel: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            else
                MessageBox.Show("Пожалуйста, выберите строку для экспорта.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            _userSelection = false;
            if (_currentIndex > 0)
            {
                _currentIndex--;
                UpdateDisplay();
            }
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            _userSelection = false;
            if (_currentIndex < _salesData.Count - 1)
            {
                _currentIndex++;
                UpdateDisplay();
            }
        }

        private void dataGridViewSales_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _userSelection = true;
        }
    }
}
