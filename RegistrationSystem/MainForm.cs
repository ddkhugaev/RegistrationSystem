using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RegistrationSystem
{
    public partial class MainForm : Form
    {
        List<User> users = new List<User>();

        private void ReadDB()
        {
            StreamReader sr = new StreamReader("../../Data/db.txt");
            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
                if (s.Contains("~"))
                {
                    string[] data = s.Split('~');
                    users.Add(new User(data[0], data[1]));
                }
            }
            sr.Close();
        }

        public MainForm()
        {
            InitializeComponent();
            ReadDB();
        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            string[] data = GetLoginPassword();
            labelError.Text = "";
            if (User.CheckCorrectSymb(data[0], data[1]))
            {
                if (DBCheckLogin(data[0]))
                {
                    if (DBCheckPassword(data[0], data[1]))
                    {
                        MessageBox.Show($"Вы вошли в аккаунт {data[0]}");
                    }
                    else
                    {
                        labelError.Text = "Неправильный пароль";
                    }
                }
                else
                {
                    labelError.Text = "Такого пользователя не существует, создайте";
                }
            }
            else
            {
                labelError.Text = "Нельзя использовать пустые зачения и знак '~'";
            }
        }

        private void buttonSignUp_Click(object sender, EventArgs e)
        {
            string[] data = GetLoginPassword();
            labelError.Text = "";
            if (User.CheckCorrectSymb(data[0], data[1]))
            {
                if (DBCheckLogin(data[0]))
                {
                    labelError.Text = "Такой пользователь уже существует, войдите";
                }
                else
                {
                    MessageBox.Show($"Вы создали аккаунт {data[0]}");
                    StreamWriter sw = new StreamWriter("../../Data/db.txt", true);
                    sw.WriteLine($"{data[0]}~{data[1]}");
                    sw.Close();
                    users.Add(new User(data[0], data[1]));
                }
            }
            else
            {
                labelError.Text = "Нельзя использовать пустые зачения и знак '~'";
            }
        }

        private string[] GetLoginPassword()
        {
            string[] data = new string[2];
            data[0] = textBoxLogin.Text;
            data[1] = textBoxPassword.Text;
            return data;
        }

        private bool DBCheckLogin(string login)
        {
            for (int i = 0; i < users.Count; i++)
            {
                User user = users[i];
                if (user.Login == login)
                {
                    return true;
                }
            }
            return false;
        }

        private bool DBCheckPassword(string login, string password)
        {
            for (int i = 0; i < users.Count; i++)
            {
                User user = users[i];
                if (user.Login == login && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
