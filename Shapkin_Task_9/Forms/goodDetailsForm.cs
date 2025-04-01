using Shapkin_Task_9.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Shapkin_Task_9.Forms
{
    public partial class GoodDetailsForm : Form
    {
        private Good _good;
        private List<Category> _categories;

        private BindingSource _bindingSource;
        private int _rowIndex;

        /// <summary>
        /// PKGH Конструктор для добавления нового товара.
        /// </summary>
        /// <param name="categories">Список категорий товаров.</param>
        /// <param name="bindingSource">BindingSource, связанный с DataGridView.</param>
        public GoodDetailsForm(List<Category> categories, BindingSource bindingSource)
        {
            InitializeComponent();
            _good = new Good();
            _categories = categories;
            _bindingSource = bindingSource;
            this.Text = "Добавление товара";

            InitializeControls();
        }

        /// <summary>
        /// PKGH Конструктор для редактирования существующего товара.
        /// </summary>
        /// <param name="good">Редактируемый товар.</param>
        /// <param name="categories">Список категорий товаров.</param>
        /// <param name="bindingSource">BindingSource, связанный с DataGridView.</param>
        /// <param name="rowIndex">Индекс строки в DataGridView, соответствующей редактируемому товару.</param>
        public GoodDetailsForm(Good good, List<Category> categories, BindingSource bindingSource, int rowIndex)
        {
            InitializeComponent();
            _good = good;
            _categories = categories;
            _bindingSource = bindingSource;
            _rowIndex = rowIndex;
            this.Text = "Редактирование товара";

            InitializeControls();
        }

        /// <summary>
        /// PKGH Инициализирует элементы управления формы.
        /// </summary>
        private void InitializeControls()
        {
            comboCategory.DataSource = _categories;
            comboCategory.DisplayMember = "CategoryName";
            comboCategory.ValueMember = "CategoryId";

            if (_good != null)
            {
                txtName.Text = "";
                txtPrice.Text = "";
                txtPhoto.Text = "";
                txtDesc.Text = "";
                UpdateGoodPhoto();
            }
        }

        /// <summary>
        /// PKGH Обновляет отображаемое изображение товара.  Если файл не найден, отображается картинка по умолчанию.
        /// </summary>
        private void UpdateGoodPhoto()
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", txtPhoto.Text);

            if (string.IsNullOrWhiteSpace(txtPhoto.Text) || !File.Exists(imagePath))
            {
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "picture.png");
            }

            try
            {
                picturePhoto.Image = Image.FromFile(imagePath);
            }
            catch (Exception)
            {
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "picture.png");
                picturePhoto.Image = Image.FromFile(imagePath);
            }
        }

        /// <summary>
        /// PKGH Обработчик нажатия кнопки "Сохранить".  Сохраняет или обновляет информацию о товаре.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPrice.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Валидация цены
                if (!double.TryParse(txtPrice.Text, out double price))
                {
                    MessageBox.Show("Пожалуйста, введите корректное значение для цены.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Обновляем свойства товара
                _good.GoodName = txtName.Text;
                _good.Price = double.Parse(txtPrice.Text);
                _good.Picture = txtPhoto.Text;
                _good.Description = txtDesc.Text;
                _good.CategoryId = (int)comboCategory.SelectedValue;

                // Если редактируем существующий товар
                if (_rowIndex >= 0)
                {
                    // Получаем объект из BindingSource для обновления
                    dynamic item = _bindingSource[_rowIndex];
                    item.GoodName = _good.GoodName;
                    item.Price = (double)_good.Price;
                    item.Picture = _good.Picture;
                    item.Description = _good.Description;
                    item.CategoryName = ((Category)comboCategory.SelectedItem).CategoryName; // Обновляем отображаемое имя категории
                    item.CategoryId = (int)_good.CategoryId;
                    _bindingSource.ResetBindings(false); // Обновляем DataGridView
                }
                else
                {
                    Category selectedCategory = (Category)comboCategory.SelectedItem;

                    var newGood = new
                    {
                        GoodId = 0, // Временный ID, будет обновлен при сохранении в БД
                        GoodName = _good.GoodName,
                        Price = _good.Price,
                        Picture = _good.Picture,
                        Description = _good.Description,
                        CategoryName = selectedCategory.CategoryName,
                        CategoryId = selectedCategory.CategoryId
                    };
                    // Добавляем новый товар в BindingSource
                    _bindingSource.Add(newGood);
                }

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// PKGH Обработчик нажатия кнопки "Отмена".  Закрывает форму.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// PKGH Обработчик изменения текста в поле "Фото".  Обновляет отображаемое изображение.
        /// </summary>
        private void txtPhoto_TextChanged(object sender, EventArgs e)
        {
            UpdateGoodPhoto();
        }
    }
}
