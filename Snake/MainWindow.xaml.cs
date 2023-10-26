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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppContext db = new AppContext();
        public MainWindow()
        {
            InitializeComponent();
            db.Users.Load();
        }

        private async void Survival(object sender, RoutedEventArgs e)
        {
            SurBorder.Visibility = Visibility.Visible;
            SurvivalWindow survivalWindow = new() { Owner = this };
            await Task.Delay(50);
            SurBorder.Visibility = Visibility.Collapsed;
            survivalWindow.ShowDialog();
            
        }

        private async void Common(object sender, RoutedEventArgs e)
        {
            ComBorder.Visibility = Visibility.Visible;
            CommonWindow commonWindow = new() { Owner = this };
            await Task.Delay(50);
            ComBorder.Visibility = Visibility.Collapsed;
            commonWindow.ShowDialog();
            
        }

        private async void Infinity(object sender, RoutedEventArgs e)
        {
            InfBorder.Visibility = Visibility.Visible;
            InfinityWindow infinityWindow = new() { Owner = this };
            await Task.Delay(50);
            InfBorder.Visibility = Visibility.Collapsed;
            infinityWindow.ShowDialog();
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ExBorder.Visibility = Visibility.Visible;
            await Task.Delay(50);
            ExBorder.Visibility = Visibility.Collapsed;
            Close();
        }

        private async void btnLeaders_Click(object sender, RoutedEventArgs e)
        {
            LeadBorder.Visibility = Visibility.Visible;
            LeadersWindow leadersWindow = new() { Owner = this };
            await Task.Delay(50);
            LeadBorder.Visibility = Visibility.Collapsed;
            leadersWindow.ShowDialog();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            ChangeBorder.Visibility = Visibility.Visible;
            LoginWindow loginWindow = new() { Owner = this };
            await Task.Delay(50);
            ChangeBorder.Visibility = Visibility.Collapsed;
            loginWindow.ShowDialog();
        }
    }
}