using Shapkin_Task_9.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Shapkin_Task_9.Forms
{
    public partial class MainWindow : Form
    {
        private int _itemcount = 0;
        private string _userRole;
        private bool _dataChanged = false;
        private BindingSource _bindingSource = new BindingSource();

        /// <summary>
        /// PKGH Конструктор главного окна приложения.
        /// </summary>
        /// <param name="userRole">Роль пользователя.</param>
        /// <param name="name">Имя пользователя.</param>
        public MainWindow(string userRole, string name)
        {
            InitializeComponent();
            stripRole.Text += " " + userRole;
            stripName.Text += " " + name;
            _userRole = userRole;

            dgvGoods.DataSource = _bindingSource;
            // Разрешаем редактирование только Admin
            btnSave.Visible = _userRole == "Admin";
            btnCancel.Visible = _userRole == "Admin";
            btnAdd.Visible = _userRole == "Admin";

            var CategoryType = Program.context.Categories.OrderBy(p => p.CategoryName).ToList();
            CategoryType.Insert(0, new Models.Category
            {
                CategoryName = "Все типы"
            }
            );
            comboCategory.DataSource = CategoryType;
            comboCategory.DisplayMember = "CategoryName";
            comboCategory.ValueMember = "CategoryId";

            LoadAndInitData();
        }

        /// <summary>
        /// PKGH Класс-обертка для передачи данных о товаре.  Используется для отображения данных в DataGridView.
        /// </summary>
        public class GoodViewModel
        {
            public int GoodId { get; set; }
            public string GoodName { get; set; }
            public double Price { get; set; }
            public string Picture { get; set; }
            public string Description { get; set; }
            public string CategoryName { get; set; }
            public int CategoryId { get; set; }
        }

        /// <summary>
        /// PKGH Получает текущий список товаров из базы данных.
        /// </summary>
        /// <returns>Список объектов GoodViewModel.</returns>
        private List<GoodViewModel> GetCurrentGoods()
        {
            return Program.context.Goods.Join(Program.context.Categories, p => p.CategoryId, t => t.CategoryId, (p, t) => new GoodViewModel
            {
                GoodId = p.GoodId,
                GoodName = p.GoodName,
                Price = (double)p.Price,
                Picture = p.Picture,
                Description = p.Description,
                CategoryName = t.CategoryName,
                CategoryId = (int)p.CategoryId
            }).ToList();
        }

        /// <summary>
        /// PKGH Загружает и инициализирует данные о товарах в DataGridView.
        /// </summary>
        private void LoadAndInitData()
        {
            _bindingSource.Clear();

            // получаем текущие данные из бд
            var currentGoods = GetCurrentGoods();

            // Преобразуем в BindingList с обработкой отсутствующих картинок
            BindingList<GoodViewModel> bindingList = new BindingList<GoodViewModel>();
            foreach (var item in currentGoods)
            {
                // Проверяем наличие картинки и заменяем на заглушку при необходимости
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", item.Picture);
                if (string.IsNullOrWhiteSpace(item.Picture) || !File.Exists(imagePath))
                {
                    item.Picture = "picture.png"; // Заменяем на имя файла заглушки
                }
                bindingList.Add(item);
            }

            // Устанавливаем BindingList в качестве источника данных для BindingSource
            _bindingSource.DataSource = bindingList;
            dgvGoods.DataSource = _bindingSource;

            dgvGoods.Columns[6].Visible = false;
            // заголовки столбцов
            dgvGoods.Columns[0].HeaderText = "Артикул товара";
            dgvGoods.Columns[1].HeaderText = "Название";
            dgvGoods.Columns[2].HeaderText = "Цена";
            dgvGoods.Columns[3].HeaderText = "Изображение";
            dgvGoods.Columns[4].HeaderText = "Описание";
            dgvGoods.Columns[5].HeaderText = "Категория";

            // количество товара
            _itemcount = Program.context.Goods.Count();
            labelCountGood.Text = $" Результат запроса: {currentGoods.Count} записей из {_itemcount}";

            if (dgvGoods.Rows.Count > 0)
                dgvGoods_CellClick(dgvGoods, new DataGridViewCellEventArgs(0, 0)); // Эмулируем клик на первую строку

            //Сбрасываем флаг изменений и блокируем кнопки
            _dataChanged = false;
            UpdateButtonsState();
        }

        /// <summary>
        /// PKGH Обработчик нажатия кнопки "Назад". Закрывает главное окно и открывает форму авторизации.
        /// </summary>
        private void btnBack_Click(object sender, System.EventArgs e)
        {
            CloseMainWindowAndOpenFmLogin();
        }

        /// <summary>
        /// PKGH Закрывает главное окно и открывает форму авторизации.
        /// </summary>
        private void CloseMainWindowAndOpenFmLogin()
        {
            fmLogin formLogin = new fmLogin();
            formLogin.Owner = this;
            this.Hide();
            formLogin.Show();
        }

        /// <summary>
        /// PKGH Метод для фильтрации и сортировки данных в DataGridView.
        /// </summary>
        private void UpdateData()
        {
            // получаем текущие данные из бд
            var currentGoods = GetCurrentGoods();

            // выбор только тех товаров, которые принадлежат данной категории
            if (comboCategory.SelectedIndex > 0)
            {
                var selectedCategory = comboCategory.SelectedItem as Category;
                if (selectedCategory != null)
                    currentGoods = currentGoods.Where(y => y.CategoryId == selectedCategory.CategoryId).ToList();
            }

            // выбор тех товаров, в названии которых есть поисковая строка
            currentGoods = currentGoods.Where(p => p.GoodName.ToLower().Contains(txtNameGood.Text.ToLower())).ToList();

            // сортировка
            if (comboSort.SelectedIndex >= 0)
            {
                // сортировка по возрастанию цены
                if (comboSort.SelectedIndex == 0)
                    currentGoods = currentGoods.OrderBy(p => p.Price).ToList();
                // сортировка по убыванию цены
                if (comboSort.SelectedIndex == 1)
                    currentGoods = currentGoods.OrderByDescending(p => p.Price).ToList();
                // сортировка по названию
                if (comboSort.SelectedIndex == 2)
                    currentGoods = currentGoods.OrderBy(p => p.GoodName).ToList();
            }
            else
                currentGoods = currentGoods.OrderBy(p => p.GoodName).ToList();

            // Преобразуем в BindingList с обработкой отсутствующих картинок
            BindingList<GoodViewModel> bindingList = new BindingList<GoodViewModel>();
            foreach (var item in currentGoods)
            {
                // Проверяем наличие картинки и заменяем на заглушку при необходимости
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", item.Picture);
                if (string.IsNullOrWhiteSpace(item.Picture) || !File.Exists(imagePath))
                {
                    item.Picture = "picture.png"; // Заменяем на имя файла заглушки
                }
                bindingList.Add(item);
            }

            _bindingSource.DataSource = bindingList;

            labelCountGood.Text = $"Результат запроса: {currentGoods.Count} Записей из {_itemcount}";
        }

        /// <summary>
        /// PKGH Обработчик изменения выбранного индекса в ComboBox сортировки. Обновляет данные.
        /// </summary>
        private void comboSort_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateData();
        }

        /// <summary>
        /// PKGH Обработчик изменения выбранного индекса в ComboBox категории. Обновляет данные.
        /// </summary>
        private void comboCategory_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateData();
        }

        /// <summary>
        /// PKGH Обработчик изменения текста в поле поиска по названию. Обновляет данные.
        /// </summary>
        private void txtNameGood_TextChanged(object sender, System.EventArgs e)
        {
            UpdateData();
        }

        /// <summary>
        /// PKGH Обработчик клика по ячейке DataGridView.  Отображает информацию о выбранном товаре.
        /// </summary>
        private void dgvGoods_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvGoods.Rows.Count)
            {
                // Получаем данные из BindingSource
                GoodViewModel selectedRow = (GoodViewModel)_bindingSource[e.RowIndex];

                // Заполняем информацию о товаре
                labelNameGood.Text = selectedRow.GoodName.ToString();
                labelPrice.Text = selectedRow.Price.ToString();

                // Обрабатываем картинку
                string imageName = selectedRow.Picture.ToString();
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", imageName);

                if (string.IsNullOrWhiteSpace(imageName) || !File.Exists(imagePath))
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "picture.png");
                }

                try
                {
                    pictureGood.Image = Image.FromFile(imagePath);
                }
                catch (Exception)
                {
                    // Обработка ошибки загрузки изображения
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "picture.png");
                    pictureGood.Image = Image.FromFile(imagePath);
                }
            }
        }

        /// <summary>
        /// PKGH Обработчик события закрытия главного окна. Открывает форму авторизации.
        /// </summary>
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            CloseMainWindowAndOpenFmLogin();
        }

        /// <summary>
        /// PKGH Обработчик события изменения значения ячейки DataGridView.  Устанавливает флаг изменения данных.
        /// </summary>
        private void dgvGoods_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_userRole == "Admin") // Проверка роли перед изменением флага
            {
                _dataChanged = true;
                UpdateButtonsState(); // Обновляем состояние кнопок
            }
        }

        /// <summary>
        /// PKGH Обработчик нажатия кнопки "Сохранить".  Сохраняет изменения в базе данных.
        /// </summary>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                dgvGoods.EndEdit(); // Завершаем редактирование ячейки

                // Получаем список GoodId из базы данных
                List<int> existingGoodIds = Program.context.Goods.Select(g => g.GoodId).ToList();

                // Получаем данные из BindingSource
                BindingList<GoodViewModel> changedData = (BindingList<GoodViewModel>)_bindingSource.DataSource;

                // Начинаем транзакцию
                using (var transaction = Program.context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (GoodViewModel row in changedData)
                        {
                            if (row.GoodId == 0)
                            {
                                // Новый товар

                                // Находим максимальный GoodId в базе данных
                                int maxGoodId = Program.context.Goods.Any() ? Program.context.Goods.Max(g => g.GoodId) : 0;
                                // Генерируем новый GoodId
                                int newGoodId = maxGoodId + 1;

                                Good newGood = new Good
                                {
                                    GoodId = newGoodId,
                                    GoodName = row.GoodName,
                                    Price = row.Price,
                                    Picture = row.Picture,
                                    Description = row.Description,
                                    CategoryId = row.CategoryId
                                };

                                // Добавляем товар в контекст
                                Program.context.Goods.Add(newGood);
                                row.GoodId = newGoodId;
                            }
                            else
                            {
                                // Существующий товар
                                int goodId = row.GoodId;
                                Good existingGood = Program.context.Goods.FirstOrDefault(g => g.GoodId == goodId);

                                if (existingGood != null)
                                {
                                    existingGood.GoodName = row.GoodName;
                                    existingGood.Price = row.Price;
                                    existingGood.Picture = row.Picture;
                                    existingGood.Description = row.Description;
                                    existingGood.CategoryId = row.CategoryId;
                                }
                            }
                        }

                        // Сохраняем изменения в базе данных
                        Program.context.SaveChanges();

                        // Фиксируем транзакцию
                        transaction.Commit();

                        MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _dataChanged = false;
                        UpdateButtonsState();
                        LoadAndInitData(); // Перезагружаем данные после сохранения
                    }
                    catch (Exception ex)
                    {
                        // Откатываем транзакцию в случае ошибки
                        transaction.Rollback();
                        MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// PKGH Обработчик нажатия кнопки "Отмена".  Отменяет изменения и перезагружает данные из базы данных.
        /// </summary>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            // Возвращаем данные в исходное состояние (перезагружаем из БД)
            LoadAndInitData();
            _dataChanged = false;
            UpdateButtonsState();
        }

        /// <summary>
        /// PKGH Обработчик клика по изображению товара. Открывает форму для просмотра изображения.
        /// </summary>
        private void pictureGood_Click(object sender, EventArgs e)
        {
            ImageForm imageForm = new ImageForm(pictureGood.Image);
            imageForm.Show();
        }

        /// <summary>
        /// PKGH Обновляет состояние кнопок "Сохранить" и "Отмена" в зависимости от флага изменения данных и роли пользователя.
        /// </summary>
        private void UpdateButtonsState()
        {
            btnSave.Enabled = _dataChanged && _userRole == "Admin";
            btnCancel.Enabled = _dataChanged && _userRole == "Admin";
        }

        /// <summary>
        /// PKGH Открывает форму деталей товара для редактирования или добавления нового товара.
        /// </summary>
        /// <param name="rowIndex">Индекс строки в DataGridView. -1 для добавления нового товара.</param>
        private void OpenGoodDetailsForm(int rowIndex)
        {
            GoodDetailsForm detailsForm;

            if (rowIndex == -1)
            {
                // Новая пустая форма
                detailsForm = new GoodDetailsForm(Program.context.Categories.ToList(), _bindingSource);
            }
            else
            {
                // Берем данные выбранной строки
                GoodViewModel selectedRow = (GoodViewModel)_bindingSource[rowIndex];

                // Находим соответствующий товар из БД
                int goodId = (int)dgvGoods.Rows[rowIndex].Cells[0].Value;
                Good good = Program.context.Goods.FirstOrDefault(g => g.GoodId == goodId);
                detailsForm = new GoodDetailsForm(good, Program.context.Categories.ToList(), _bindingSource, rowIndex);
            }

            if (detailsForm.ShowDialog() == DialogResult.OK)
            {
                _dataChanged = true;
                UpdateButtonsState();
            }
        }

        /// <summary>
        /// PKGH Обработчик двойного клика по ячейке DataGridView.  Открывает форму деталей товара для редактирования.
        /// </summary>
        private void dgvGoods_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && _userRole == "Admin")
            {
                OpenGoodDetailsForm(e.RowIndex);
            }
        }

        /// <summary>
        /// PKGH Обработчик нажатия кнопки "Добавить".  Открывает форму деталей товара для добавления нового товара.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenGoodDetailsForm(-1); // -1 означает добавление новой строки
        }
    }
}
