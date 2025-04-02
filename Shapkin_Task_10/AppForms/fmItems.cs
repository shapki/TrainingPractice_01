using Shapkin_Task_10.AppData;
using Shapkin_Task_10.Models;
using Shapkin_Task_10.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Shapkin_Task_10.Forms
{
    /// <summary>
    /// PKGH Форма для отображения и управления списком предметов.
    /// </summary>
    public partial class fmItems : Form
    {
        private readonly IItemsService _itemsService;
        private BindingSource _itemsBindingSource = new BindingSource();
        private bool _isAdmin;
        private List<Item> _originalItems; // Сохраняем исходный список товаров

        public fmItems(bool isAdmin, IItemsService itemsService)
        {
            InitializeComponent();
            Icon = Resources.shopCart1;
            _isAdmin = isAdmin;
            _itemsService = itemsService ?? throw new ArgumentNullException(nameof(itemsService));
            dataGridViewItems.AutoGenerateColumns = false;
            SetupDataGridViewColumns();
            LoadItemTypes();
            LoadItems();

            // Изначально делаем textBoxFilterValue недоступным
            textBoxFilterValue.Enabled = false;
        }

        /// <summary>
        /// PKGH Настраивает колонки DataGridView.
        /// </summary>
        private void SetupDataGridViewColumns()
        {
            // Создаем и настраиваем колонки
            AddColumn("ItemName", "Название");
            AddColumn("ProductionYear", "Год выпуска");
            AddColumn("Owner", "Владелец");
            AddColumn("EstimatedValue", "Оценочная стоимость", "N2"); // Формат для отображения валюты
            AddColumn("Description", "Описание");
        }

        /// <summary>
        /// PKGH Добавляет колонку в DataGridView.
        /// </summary>
        /// <param name="propertyName"> Имя свойства объекта данных.</param>
        /// <param name="headerText"> Текст заголовка колонки.</param>
        /// <param name="format"> Формат отображения данных.</param>
        /// <param name="visible"> Видимость колонки.</param>
        private void AddColumn(string propertyName, string headerText, string format = null, bool visible = true)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = propertyName,
                HeaderText = headerText,
                Name = propertyName,
                Visible = visible
            };
            if (!string.IsNullOrEmpty(format))
                column.DefaultCellStyle.Format = format;
            dataGridViewItems.Columns.Add(column);
        }

        /// <summary>
        /// PKGH Загружает типы предметов из сервиса.
        /// </summary>
        private void LoadItemTypes()
        {
            try
            {
                var itemTypes = _itemsService.GetItemTypes();

                // Привязываем данные к ComboBox
                comboBoxItemType.DataSource = itemTypes;
                comboBoxItemType.DisplayMember = "ItemTypeName";
                comboBoxItemType.ValueMember = "ItemTypeCode";

                // Добавляем пункт "Все"
                var allItemType = new ItemType { ItemTypeCode = 0, ItemTypeName = "Все" };
                itemTypes.Insert(0, allItemType);

                comboBoxItemType.SelectedIndex = 0; // Выбираем "Все" по умолчанию
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке видов предметов: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// PKGH Загружает предметы из сервиса.
        /// </summary>
        private void LoadItems()
        {
            try
            {
                // Получаем все товары из сервиса.
                _originalItems = _itemsService.GetItems().ToList();

                // Привязываем список товаров к BindingSource.
                _itemsBindingSource.DataSource = _originalItems;
                dataGridViewItems.DataSource = _itemsBindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке предметов: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _originalItems = new List<Item>(); // Инициализируем пустым списком в случае ошибки
                _itemsBindingSource.DataSource = _originalItems; // Чтобы grid не был null
                dataGridViewItems.DataSource = _itemsBindingSource;
            }
        }

        /// <summary>
        /// PKGH Применяет фильтры к данным.
        /// </summary>
        private void ApplyFilter()
        {
            if (_originalItems == null)
            {
                _itemsBindingSource.DataSource = new List<Item>();
                return;
            }

            // Получаем все товары.
            var filteredItems = _originalItems.ToList();

            // Фильтр по виду предмета
            if (comboBoxItemType.SelectedIndex > 0)
            {
                int itemTypeCode = (int)comboBoxItemType.SelectedValue;
                filteredItems = filteredItems.Where(item => item.ItemTypeCode == itemTypeCode).ToList();
            }

            // Поиск по названию
            if (!string.IsNullOrEmpty(textBoxSearch.Text))
            {
                string searchText = textBoxSearch.Text.ToLower();
                filteredItems = filteredItems.Where(item => item.ItemName.ToLower().Contains(searchText)).ToList();
            }

            //Добавляем фильтр оценочной стоимости
            if (textBoxFilterValue.Enabled && !string.IsNullOrEmpty(textBoxFilterValue.Text) && decimal.TryParse(textBoxFilterValue.Text, out decimal filterValue))
                filteredItems = filteredItems.Where(item => item.EstimatedValue < filterValue).ToList();

            _itemsBindingSource.DataSource = filteredItems;
        }

        private void textBoxFilterValue_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void comboBoxItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void comboBoxFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxFilterValue.Enabled = comboBoxFilterType.SelectedIndex == 2; //Включаем, если выбран фильтр по стоимости

            if (comboBoxFilterType.SelectedIndex == 0)
                _itemsBindingSource.Sort = "ProductionYear ASC";
            else if (comboBoxFilterType.SelectedIndex == 1)
                _itemsBindingSource.Sort = "ProductionYear DESC";
            else if (comboBoxFilterType.SelectedIndex == 2)
                ApplyFilter(); // Сразу применяем фильтр, если выбрали фильтр по стоимости.
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            comboBoxItemType.SelectedIndex = 0;
            textBoxSearch.Text = "";
            textBoxFilterValue.Text = "";
            textBoxFilterValue.Enabled = false;
            LoadItems();
            comboBoxFilterType.SelectedIndex = -1;
            _itemsBindingSource.Sort = "";
        }
    }
}
