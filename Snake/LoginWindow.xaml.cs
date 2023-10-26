using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Snake
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        AppContext db;
        
        public LoginWindow()
        {
            InitializeComponent();
            db = new AppContext();
        }

        private async void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ExBorder.Visibility = Visibility.Visible;
            await Task.Delay(50);
            ExBorder.Visibility = Visibility.Collapsed;
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Escape: Close(); break;
            }
        }

        private async void HeadColor_Click(object sender, RoutedEventArgs e)
        {
            HeadBorder.Visibility = Visibility.Visible;
            User authUser = null;
            using (var context = new AppContext())
            {
                authUser = context.Users.Where(b => b.Auth == 1).FirstOrDefault();
            }
            if (authUser != null)
            {
                authUser = db.Users.Where(b => b.Auth == 1).FirstOrDefault();
                if (IsHexColor(HeadColorText.Text))
                {
                    authUser.Headbrush = HeadColorText.Text;
                    bdrHeadExc.Visibility = Visibility.Collapsed;
                    bdrHeadReady.Visibility = Visibility.Visible;
                }
                else
                {
                    authUser.Headbrush = "#8B0000";
                    bdrHeadExc.Visibility = Visibility.Visible;
                    bdrHeadReady.Visibility = Visibility.Collapsed;
                }
                db.SaveChanges();
                await Task.Delay(50);
                HeadBorder.Visibility = Visibility.Collapsed;
            }
        }

        private bool IsHexColor(string input)
        {
            string hexColorPattern = @"^#([0-9A-Fa-f]{6})$";

            if (Regex.IsMatch(input, hexColorPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void TailColor_Click(object sender, RoutedEventArgs e)
        {
            TailBorder.Visibility = Visibility.Visible;
            User authUser = null;
            using (var context = new AppContext())
            {
                authUser = context.Users.Where(b => b.Auth == 1).FirstOrDefault();
            }
            if(authUser != null)
            {
                authUser = db.Users.Where(b => b.Auth == 1).FirstOrDefault();
                if (IsHexColor(TailColorText.Text))
                {
                    authUser.Tailbrush = TailColorText.Text;
                    bdrTailExc.Visibility = Visibility.Collapsed;
                    bdrTailReady.Visibility = Visibility.Visible;
                }
                else
                {
                    authUser.Tailbrush = "#FF0000";
                    bdrTailExc.Visibility = Visibility.Visible;
                    bdrTailReady.Visibility = Visibility.Collapsed;
                }
                db.SaveChanges();
                await Task.Delay(50);
                TailBorder.Visibility = Visibility.Collapsed;
            }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            bdrHeadExc.Visibility = Visibility.Collapsed;
            bdrHeadReady.Visibility = Visibility.Collapsed;
            bdrTailExc.Visibility = Visibility.Collapsed;
            bdrTailReady.Visibility = Visibility.Collapsed;
            LoginBorder.Visibility = Visibility.Visible;

            User authUser = null;
            authUser = db.Users.Where(b => b.Login.Substring(0, NameText.Text.Length) == NameText.Text && b.Login.Substring(NameText.Text.Length + 1, PassText.Text.Length) == PassText.Text).FirstOrDefault();
            
            if(authUser != null)
            {
                LoginReadyExc.Visibility = Visibility.Collapsed;
                LoginReadyNew.Visibility = Visibility.Collapsed;
                LoginReady.Visibility = Visibility.Visible;
                LoginReadyNewExc.Visibility = Visibility.Collapsed;

                TailColorText.Text = authUser.Tailbrush;
                HeadColorText.Text = authUser.Headbrush;
                User UnAuthUser = null;
                UnAuthUser = db.Users.Where(b => b.Login.Substring(0, NameText.Text.Length) != NameText.Text && b.Auth == 1).FirstOrDefault();
                if (UnAuthUser != null)
                    UnAuthUser.Auth = 0;
                authUser.Auth = 1;
                db.SaveChanges();
            }
            else
            {
                LoginReadyExc.Visibility = Visibility.Visible;
                LoginReadyNew.Visibility = Visibility.Collapsed;
                LoginReady.Visibility = Visibility.Collapsed;
                LoginReadyNewExc.Visibility = Visibility.Collapsed;
            }
            await Task.Delay(50);
            LoginBorder.Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            User authUser = null;
            authUser = db.Users.Where(b => b.Auth == 1).FirstOrDefault();
            if (!(authUser is null))
            {
                NameText.Text = authUser.Login.Substring(0, authUser.Login.IndexOf('+'));
                PassText.Text = authUser.Login.Substring(NameText.Text.Length + 1);
                TailColorText.Text = authUser.Tailbrush;
                HeadColorText.Text = authUser.Headbrush;
            }
        }

        private async void btnSetup_Click(object sender, RoutedEventArgs e)
        {
            bdrHeadExc.Visibility = Visibility.Collapsed;
            bdrHeadReady.Visibility = Visibility.Collapsed;
            bdrTailExc.Visibility = Visibility.Collapsed;
            bdrTailReady.Visibility = Visibility.Collapsed;
            SetupBorder.Visibility = Visibility.Visible;

            User authUser = null;
            authUser = db.Users.Where(b => b.Login.Substring(0, NameText.Text.Length) == NameText.Text).FirstOrDefault();
            if(authUser is null)
            {
                User user = new($"{NameText.Text}+{PassText.Text}", "#8B0000", "#FF0000", 0, 0, 0);
                db.Users.Add(user);
                db.SaveChanges();
                LoginReadyExc.Visibility = Visibility.Collapsed;
                LoginReadyNew.Visibility = Visibility.Visible;
                LoginReady.Visibility = Visibility.Collapsed;
                LoginReadyNewExc.Visibility = Visibility.Collapsed;
                HeadColorText.Text = user.Headbrush;
                TailColorText.Text = user.Tailbrush;
            }
            else
            {
                LoginReadyExc.Visibility = Visibility.Collapsed;
                LoginReadyNew.Visibility = Visibility.Collapsed;
                LoginReady.Visibility = Visibility.Collapsed;
                LoginReadyNewExc.Visibility = Visibility.Visible;
            }
            await Task.Delay(50);
            SetupBorder.Visibility = Visibility.Collapsed;
        }
    }
}