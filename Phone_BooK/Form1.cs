using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data.SQLite;

namespace Phone_BooK
{
    public partial class Form1 : Form
    {
        public SQLiteConnection sqlConnection = new SQLiteConnection("Data Source = Phone_BooK.db");
        public Form1()
        {
            InitializeComponent();
          
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string FIO="", Nummber="";
           


            if (!string.IsNullOrEmpty(textBox1.Text)&&!string.IsNullOrWhiteSpace(textBox1.Text)
                && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                FIO = textBox1.Text;
                Nummber = textBox2.Text;

                SQLiteCommand command = new SQLiteCommand("INSERT INTO[PhoneBook](FIO, Nummber)VALUES(@FIO, @Nummber)", sqlConnection);
                command.Parameters.AddWithValue("FIO",FIO);
                command.Parameters.AddWithValue("Nummber",Nummber);
                command.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Ошибка, поля 'ФИО' и 'Номер'должны бить заполнеными", "Внимание",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            textBox1.Clear();
            textBox2.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SQLiteDataReader sqlDataReader = null;
            SQLiteCommand sqlCommand = new SQLiteCommand("SELECT * FROM[PhoneBook]", sqlConnection);
            try
            {
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    listBox1.Items.Add(Convert.ToString(sqlDataReader["ID"] + " " + sqlDataReader["FIO"] + " " + sqlDataReader["Nummber"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SQLiteDataReader sqlDataReader = null;
            SQLiteCommand sqlCommand = new SQLiteCommand("SELECT * FROM[PhoneBook]", sqlConnection);
            try
            {
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    listBox1.Items.Add(Convert.ToString(sqlDataReader["ID"] + " " + sqlDataReader["FIO"] + " " + sqlDataReader["Nummber"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }

        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            String link_XML = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange";
            XmlDocument doc = new XmlDocument();
            doc.Load(link_XML);
            Exchange ex = new Exchange();


            foreach (XmlNode node in doc.DocumentElement)
            {
                foreach (XmlNode childe in node.ChildNodes)
                {
                    if (childe.Name == "txt")
                    {
                        ex.txt = childe.InnerText;
                    }
                    if (childe.Name == "rate")
                    {
                        ex.rate = childe.InnerText;
                    }
                    if (childe.Name == "cc")
                    {
                        ex.cc = childe.InnerText;
                    }
                    if (childe.Name == "exchangedate")
                    {
                        ex.exchangedate = childe.InnerText;
                    }
                    //parthlist.Add(ex);
                }
                if (ex.txt == "Долар США")
                {
                    listBox2.Items.Add(ex.txt);
                    listBox2.Items.Add(ex.rate);
                    listBox2.Items.Add(ex.cc);
                    listBox2.Items.Add(ex.exchangedate);
                    listBox2.Items.Add("**************");
                }
                else if (ex.txt == "Євро")
                {
                    listBox2.Items.Add(ex.txt);
                    listBox2.Items.Add(ex.rate);
                    listBox2.Items.Add(ex.cc);
                    listBox2.Items.Add(ex.exchangedate);
                    listBox2.Items.Add("**************");
                }
                else if (ex.txt == "Російський рубль")
                {
                    listBox2.Items.Add(ex.txt);
                    listBox2.Items.Add(ex.rate);
                    listBox2.Items.Add(ex.cc);
                    listBox2.Items.Add(ex.exchangedate);
                    listBox2.Items.Add("**************");
                }
            }
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string FIO = "", Nummber = "", Id = "";


            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text)
                && !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text)
                && !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                FIO = textBox4.Text;
                Nummber = textBox3.Text;
                Id= textBox5.Text;

                SQLiteCommand command = new SQLiteCommand("UPDATE[PhoneBook] SET [FIO]=@FIO, [Nummber]=@Nummber WHERE [ID]=@ID", sqlConnection);
                command.Parameters.AddWithValue("ID",Id);
                command.Parameters.AddWithValue("FIO", FIO);
                command.Parameters.AddWithValue("Nummber", Nummber);
                command.ExecuteNonQueryAsync();
            }
            else if(!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Ошибка, поле Id должно быть заполнено", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                 MessageBox.Show("Ошибка, поля 'ФИО' и 'Номер'должны бить заполнеными", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void обновитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SQLiteDataReader sqlDataReader = null;
            SQLiteCommand sqlCommand = new SQLiteCommand("SELECT * FROM[PhoneBook]", sqlConnection);
            try
            {
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    listBox1.Items.Add(Convert.ToString(sqlDataReader["ID"] + " " + sqlDataReader["FIO"] + " " + sqlDataReader["Nummber"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }

        }

        private void очиститьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Id = "";
            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                Id = textBox6.Text;
                SQLiteCommand command = new SQLiteCommand("DELETE FROM [PhoneBook] WHERE [ID]=@ID", sqlConnection);
                command.Parameters.AddWithValue("ID",Id);
                command.ExecuteNonQuery();

            }
            else
                MessageBox.Show("Поле 'Id' должно быть заполнено", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox6.Clear();
        }
    }
}
