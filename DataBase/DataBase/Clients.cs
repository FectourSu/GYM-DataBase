using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase
{
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
            ConnectionDB();
            //Изменение цвета выделения ячейки датагрид
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.Black;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Black;
        }
        //заполнение таблиц из бд
        DatabaseEntities connectDB = new DatabaseEntities();
        public void ConnectionDB()
        {
            var collectionStudients = connectDB.Студенты
                .ToList();
            foreach (var item in collectionStudients)
            {
                dataGridView3.Rows.Add(item.Id, item.Имя, item.Фамилия, item.Отчество, item.Качалка.Секция, item.Спортзал.Секция, item.Время_записи.ToShortDateString());
            }
            var collectionSport = connectDB.Спортзал
                .ToList();
            foreach (var item in collectionSport)
            {
                dataGridView2.Rows.Add(item.Id, item.Секция, item.График);
            }
            var collectionGym = connectDB.Качалка
               .ToList();
            foreach (var item in collectionGym)
            {
                dataGridView1.Rows.Add(item.Id, item.Секция, item.График);
            }
        }
        //Функция обновления студентов
        private void Refresher()
        {
            foreach (var item in connectDB.Студенты)
            {
                dataGridView3.Rows.Add(item.Id, item.Имя, item.Фамилия, item.Отчество, item.Качалка.Секция, item.Спортзал.Секция, item.Время_записи.ToShortDateString());
            }
        }
        //Функция обновления спортзала
        private void SPORTref()
        {
            foreach (var item in connectDB.Спортзал)
            {
                dataGridView2.Rows.Add(item.Id, item.Секция, item.График);
            }
        }
        //Функция обновления качалки
        private void GYMref()
        {
            foreach (var item in connectDB.Качалка)
            {
                dataGridView1.Rows.Add(item.Id, item.Секция, item.График);
            }
        }
        //перетаскивание формы без верхней панели
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int WM_LBUTTONDBLCLK = 0x00A3;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDBLCLK)
            {
                return;
            }
            switch (m.Msg)
            {

                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    if ((int)m.Result == HTCLIENT)
                        m.Result = (IntPtr)HTCAPTION;
                    return;
            }
            base.WndProc(ref m);
        }
        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //скрыть форму
        private void PictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //кнопочка назад
        private void PictureBox4_Click(object sender, EventArgs e)
        {
            Clients close = new Clients();
            HelloForms client = new HelloForms();
            Hide();
            client.Show();
            close.Close();
        }
        //первоначальная очистка студентов

        private void PictureBox14_Click(object sender, EventArgs e)
        {
            Delete del = new Delete(dataGridView3, connectDB);
            del.DeleteEvent += Del_DeleteEvent;
            del.ShowDialog();
        }

        private void Del_DeleteEvent(DataGridView data, bool obj)
        {
            try
            {
                if (data.SelectedRows.Count != default)
                {
                    var delete = data.SelectedRows[data.SelectedRows.Count - 1];
                    data.Rows.RemoveAt(delete.Index);
                    var temp = Convert.ToInt32(delete.Cells[0].Value);
                    var list = connectDB
                        .Студенты
                        .Where(s => s.Id == temp)
                        .First();
                    connectDB.Студенты.Remove(list);
                    connectDB.SaveChanges();
                }
                else
                    MessageBox.Show("Не та таблица.\nБудьте внимательнее это Студенты!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {
                MessageBox.Show("Нельзя удалить пустую строку!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //очистка качалки
        private void PictureBox12_Click(object sender, EventArgs e)
        {
            Delete del = new Delete(dataGridView1, connectDB);
            del.DeleteEvent2 += Del_DeleteEvent2;
            del.ShowDialog();
        }

        private void Del_DeleteEvent2(DataGridView data, bool obj)
        {
            try
            {
                if (data.SelectedRows.Count != default)
                {
                    var delete = data.SelectedRows[data.SelectedRows.Count - 1];
                    var temp = Convert.ToInt32(delete.Cells[0].Value);
                    var list = connectDB
                        .Качалка
                        .Where(s => s.Id == temp)
                        .First();
                    connectDB.Качалка.Remove(list);
                    connectDB.SaveChanges();
                    data.Rows.RemoveAt(delete.Index);
                }
                else
                    MessageBox.Show("Не та таблица.\nБудьте внимательнее это Качалка!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {
                MessageBox.Show("Для удаления измените это поле в таблице \"Студенты\"", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connectDB = new DatabaseEntities();
            }

        }
        //очистка спортзала
        private void PictureBox10_Click(object sender, EventArgs e)
        {
            Delete del = new Delete(dataGridView2, connectDB);
            del.DeleteEvent3 += Del_DeleteEvent3;
            del.ShowDialog();
        }

        private void Del_DeleteEvent3(DataGridView data, bool obj)
        {
            try
            {
                if (data.SelectedRows.Count != default)
                {
                    var delete = data.SelectedRows[data.SelectedRows.Count - 1];
                    var temp = Convert.ToInt32(delete.Cells[0].Value);
                    var list = connectDB
                        .Спортзал
                        .Where(s => s.Id == temp)
                        .First();
                    connectDB.Спортзал.Remove(list);
                    connectDB.SaveChanges();
                    data.Rows.RemoveAt(delete.Index);
                }
                else
                    MessageBox.Show("Не та таблица.\nБудьте внимательнее это Спортзалл!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {
                MessageBox.Show("Для удаления измените это поле в таблице \"Студенты\"", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connectDB = new DatabaseEntities();
            }
        }

        //добавить  студентов
        private void PictureBox13_Click(object sender, EventArgs e)
        {
            AddStudient ad = new AddStudient(connectDB);
            ad.AddEvent += Ad_AddEvent;
            ad.ShowDialog();
        }

        private void Ad_AddEvent(List<Control> list, Boolean bol)
        {
            //Сохранить выделение
            int positionRow = dataGridView3.CurrentCell.RowIndex;
            int positionColumn = dataGridView3.CurrentCell.ColumnIndex;

            Студенты studient = new Студенты();
                studient.Имя = list[4].Text;
                studient.Фамилия = list[3].Text;
                studient.Отчество = list[2].Text;
                var temp = list[0].Text;
                var temp2 = list[1].Text;
                studient.КачалкаID = connectDB
                    .Качалка
                    .Where(c => c.Секция == temp)
                    .First().Id;
                studient.СпортзалID = connectDB
                    .Спортзал
                    .Where(c => c.Секция == temp2)
                    .First().Id;
                studient.Время_записи = ((DateTimePicker)list[5]).Value;
                connectDB.Студенты.Add(studient);
                connectDB.SaveChanges();
                dataGridView3.Rows.Clear();
                Refresher();
                dataGridView3.FirstDisplayedScrollingRowIndex = dataGridView3.RowCount - 1; //Автоматический скролл датагрид вниз
           
            //Сохранить выделение
            dataGridView3.CurrentCell = dataGridView3[positionColumn, positionRow];
        }
        //Добавить секцию в качалку
        private void PictureBox11_Click(object sender, EventArgs e)
        {
            Add add = new Add();
            add.AddE += Add_AddE2;
            add.ShowDialog();
        }
        private void Add_AddE2(List<Control> list, bool bol)
        {
            //Сохранить выделение
            int positionRow = dataGridView1.CurrentCell.RowIndex;
            int positionColumn = dataGridView1.CurrentCell.ColumnIndex;

            Качалка gym = new Качалка();
                gym.Секция = list[1].Text;
                gym.График = list[0].Text;
                connectDB.Качалка.Add(gym);
                connectDB.SaveChanges();
                dataGridView1.Rows.Clear();
                GYMref();
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;

            //Сохранить выделение
            dataGridView1.CurrentCell = dataGridView1[positionColumn, positionRow];
        }
        //Добавить секцию спортзала
        private void PictureBox9_Click(object sender, EventArgs e)
        {
            Add add = new Add();
            add.AddE += Add_AddE;
            add.ShowDialog();
        }
        private void Add_AddE(List<Control> list, bool arg2)
        {
            //Сохранить выделение
            int positionRow = dataGridView2.CurrentCell.RowIndex;
            int positionColumn = dataGridView2.CurrentCell.ColumnIndex;

            Спортзал sport = new Спортзал();
                sport.Секция = list[1].Text;
                sport.График = list[0].Text;
                connectDB.Спортзал.Add(sport);
                connectDB.SaveChanges();
                dataGridView2.Rows.Clear();
                SPORTref();
                dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.RowCount - 1;

            //Сохранить выделение
            dataGridView2.CurrentCell = dataGridView2[positionColumn, positionRow];
        }

        //Выбрать и изменить студентов
        private void DataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedStudients select = new SelectedStudients(dataGridView3, connectDB);
            select.SelectedStd += Select_SelectedStd;
            select.ShowDialog();
        }

        private void Select_SelectedStd(List<Control> data, bool arg2)
        {
            //Сохранить выделение
            int positionRow = dataGridView3.CurrentCell.RowIndex;
            int positionColumn = dataGridView3.CurrentCell.ColumnIndex;

            var select = (int)dataGridView3.SelectedRows[dataGridView3.SelectedRows.Count - 1].Cells[0].Value;
                var list = connectDB
                   .Студенты
                   .Where(s => s.Id == select)
                   .First();
                list.Имя = data[5].Text;
                list.Фамилия = data[4].Text;
                list.Отчество = data[3].Text;
                var id = data[1].Text;
                var id2 = data[2].Text;
                list.КачалкаID = connectDB
                        .Качалка
                        .Where(c => c.Секция == id)
                        .First().Id;
                list.СпортзалID = connectDB
                        .Спортзал
                        .Where(c => c.Секция == id2)
                        .First().Id;
                list.Время_записи = ((DateTimePicker)data[0]).Value;
                connectDB.SaveChanges();
                dataGridView3.Rows.Clear();
                Refresher();

            //Сохранить выделение
            dataGridView3.CurrentCell = dataGridView3[positionColumn, positionRow];
        }
        //Помощь по форме
        private void PictureBox18_Click(object sender, EventArgs e)
        {
            Setting set = new Setting();
            set.ShowDialog();
        }
        //Выбрать и изменить секцию Спортзала
        private void DataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Selected section = new Selected(dataGridView2);
            section.SelectedSection += Section_SelectedSection;
            section.ShowDialog();
        }

        private void Section_SelectedSection(List<Control> data, bool arg2)
        {
            //Сохранить выделение
            int positionRow = dataGridView2.CurrentCell.RowIndex;
            int positionColumn = dataGridView2.CurrentCell.ColumnIndex;

            var select = (int)dataGridView2.SelectedRows[dataGridView2.SelectedRows.Count - 1].Cells[0].Value;
            var list = connectDB
                  .Спортзал
                  .Where(s => s.Id == select)
                  .First();
            list.Секция = data[1].Text;
            list.График = data[0].Text;
            connectDB.SaveChanges();
            dataGridView2.Rows.Clear();
            SPORTref();

            //Сохранить выделение
            dataGridView2.CurrentCell = dataGridView2[positionColumn, positionRow];
        }
        //Выбрать и изменить секцию Качалки
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Selected section = new Selected(dataGridView1);
            section.SelectedSection += Section_SelectedSection1;
            section.ShowDialog();
        }

        private void Section_SelectedSection1(List<Control> data, bool arg2)
        {
            //Сохранить выделение
            int positionRow = dataGridView1.CurrentCell.RowIndex;
            int positionColumn = dataGridView1.CurrentCell.ColumnIndex;

            var select = (int)dataGridView1.SelectedRows[dataGridView1.SelectedRows.Count - 1].Cells[0].Value;
            var list = connectDB
                  .Качалка
                  .Where(s => s.Id == select)
                  .First();
            list.Секция = data[1].Text;
            list.График = data[0].Text;
            connectDB.SaveChanges();
            dataGridView1.Rows.Clear();
            GYMref();

            //Сохранить выделение
            dataGridView1.CurrentCell = dataGridView1[positionColumn, positionRow];
        }
        //Поиск по студентам
        private void PictureBox16_Click(object sender, EventArgs e)
        {
            Search search = new Search(dataGridView3);
            search.Searching += Search_Searching;
            search.Before += Search_Before;
            search.After += Search_After;
            search.ShowDialog();
        }
        //Интервал по дате до
        private void Search_After(DateTimePicker data)
        {
            var obj = connectDB.Студенты.Where(u => u.Время_записи > data.Value).ToList();
            dataGridView3.Rows.Clear();
            foreach (var item in obj)
                dataGridView3.Rows.Add(item.Id, item.Имя, item.Фамилия, item.Отчество, item.Качалка.Секция, item.Спортзал.Секция, item.Время_записи.ToShortDateString());
        }
        //Интервал по дате после
        private void Search_Before(DateTimePicker data)
        {
            var obj = connectDB.Студенты.Where(u => u.Время_записи < data.Value).ToList();
            dataGridView3.Rows.Clear();
            foreach (var item in obj)
                dataGridView3.Rows.Add(item.Id, item.Имя, item.Фамилия, item.Отчество, item.Качалка.Секция, item.Спортзал.Секция, item.Время_записи.ToShortDateString());
        }
        //ГОВНОКОД!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private void Search_Searching(List<Control> data, bool arg2)
        {
            dataGridView3.Rows.Clear();
            string[] razbienie = data[0].Text.Split(new char[] { ' ', '.' });
            //поиск по одному человеку
            for (int i = 0; i < razbienie.Length; i++)
            {
                if (razbienie.Length > 7)
                {
                    var a = razbienie[0];
                    var a2 = razbienie[1];
                    var a3 = razbienie[2];
                    var a4 = razbienie[3];
                    var a5 = razbienie[4];
                    var a6 = razbienie[5];
                    var a7 = razbienie[6];
                    var a8 = razbienie[7];
                    var Query = connectDB
                    .Студенты
                    .Where(u => u.Имя.Contains(a) && u
                    .Фамилия.Contains(a2) && u
                    .Отчество.Contains(a3) && u
                    .КачалкаID == connectDB
                    .Качалка.Where(c => c.Секция.Contains(a4)).FirstOrDefault().Id && u
                    .СпортзалID == connectDB
                    .Спортзал.Where(c => c.Секция.Contains(a5)).FirstOrDefault().Id && u
                    .Время_записи.ToString().Contains(a6) && u
                    .Время_записи.ToString().Contains(a7) && u
                    .Время_записи.ToString().Contains(a8)
                    ).ToList();
                    if (razbienie.Length > 7)
                    {
                        foreach (var item in Query)
                            dataGridView3.Rows.Add(item.Id, item.Имя, item.Фамилия, item.Отчество, item.Качалка.Секция, item.Спортзал.Секция, item.Время_записи.ToShortDateString());
                        break;
                    }
                    if (Query.Count() == 0)
                    {
                        MessageBox.Show("В таблице такого нет :(\nПопробуйте еще раз!\n(づ ◕‿◕ )づ ",
                            "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView3.Rows.Clear();
                        Refresher();
                        break;
                    }
                }
                //поиск по полю
                else if (razbienie.Length < 8)
                {
                    string text = razbienie[i];
                    var Query = connectDB
                    .Студенты
                    .Where(u => u.Имя.Contains(text) || u
                    .Фамилия.Contains(text) || u
                    .Отчество.Contains(text) || u
                    .КачалкаID == connectDB
                    .Качалка.Where(c => c.Секция.Contains(text)).FirstOrDefault().Id || u
                    .СпортзалID == connectDB
                    .Спортзал.Where(c => c.Секция.Contains(text)).FirstOrDefault().Id || u
                    .Время_записи.ToString().Contains(text)
                    ).ToList();
                    if (razbienie.Length < 8)
                    {
                        foreach (var item in Query)
                            dataGridView3.Rows.Add(item.Id, item.Имя, item.Фамилия, item.Отчество, item.Качалка.Секция, item.Спортзал.Секция, item.Время_записи.ToShortDateString());
                        break;
                    }
                    if (Query.Count() == 0)
                    {
                        MessageBox.Show("В таблице такого нет :(\nПопробуйте еще раз!\n(づ ◕‿◕ )づ ",
                            "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView3.Rows.Clear();
                        Refresher();
                        break;
                    }
                }
            }
        }
        //Поиск по качалке
        private void PictureBox15_Click(object sender, EventArgs e)
        {
            
            Search search = new Search(dataGridView1);
            search.InVisible();
            search.Searching += Search_Searching1;
            search.ShowDialog();
        }
        private void Search_Searching1(List<Control> list, bool arg2)
        {
            string[] razbienie = list[0].Text.Split(new char[] { ' ', ':', '-' });
            dataGridView1.Rows.Clear();
            for (int i = 0; i < razbienie.Length; i++)
            {
               string text = razbienie[i];
               var Query = connectDB
               .Качалка
               .Where(u => u.Секция.Contains(text) || u
               .График.Contains(text)).ToList();
                if (razbienie.Length >= 1)
                {
                    foreach (var item in Query)
                        dataGridView1.Rows.Add(item.Id, item.Секция, item.График);
                    break;
                }
                if (Query.Count == 0)
                {
                    MessageBox.Show("В таблице такого нет :(\nПопробуйте еще раз!\n(づ ◕‿◕ )づ ",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView1.Rows.Clear();
                    GYMref();
                    break;
                }
            }
        }
        //Поиск по спортзалу
        private void PictureBox8_Click(object sender, EventArgs e)
        {
            Search search = new Search(dataGridView2);
            search.InVisible();
            search.Searching += Search_Searching2;
            search.ShowDialog();
        }

        private void Search_Searching2(List<Control> list, bool arg2)
        {
            string[] razbienie = list[0].Text.Split(new char[] { ' ', ':','-' });
            dataGridView2.Rows.Clear();
            for (int i = 0; i < razbienie.Length; i++)
            {
                string text = razbienie[i];
                var Query = connectDB
                .Спортзал
                .Where(u => u.Секция.Contains(text) || u
                .График.Contains(text)).ToList();
                if (razbienie.Length >= 1)
                {
                    foreach (var item in Query)
                        dataGridView2.Rows.Add(item.Id, item.Секция, item.График);
                    break;
                }
                if (Query.Count == 0)
                {
                    MessageBox.Show("В таблице такого нет :(\nПопробуйте еще раз!\n(づ ◕‿◕ )づ ",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView2.Rows.Clear();
                    SPORTref();
                    break;
                }
            }
        }
        //Убрать выделение строк таблиц
        private void DataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            dataGridView2.CurrentCell = null;
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView3.CurrentCell = null;
            dataGridView2.CurrentCell = null;
        }

        private void DataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView3.CurrentCell = null;
            dataGridView1.CurrentCell = null;
        }
        //нажатие Del - удаление компонентов из датагрида
        private void DataGridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                PictureBox14_Click(sender, e);
            }
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                PictureBox12_Click(sender, e);
            }
        }

        private void DataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                PictureBox10_Click(sender, e);
            }
        }
        //обновление таблиц
        private void PictureBox19_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            SPORTref();
        }

        private void PictureBox20_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            GYMref();
        }

        private void PictureBox21_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            Refresher();
        }
        //Подключение CHM Файла
        private void PictureBox17_Click(object sender, EventArgs e)
        {
            string helpFileName = @"C:\Users\filin\Desktop\Help.chm";
            if (File.Exists(helpFileName))
            {
                Help.ShowHelp(this, helpFileName);
                this.CenterToScreen();
            }
        }
    }
}