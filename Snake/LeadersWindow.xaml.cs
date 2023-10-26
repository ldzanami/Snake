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
using Microsoft.EntityFrameworkCore;

namespace Snake
{
    /// <summary>
    /// Логика взаимодействия для LeadersWindow.xaml
    /// </summary>
    public partial class LeadersWindow : Window
    {
        AppContext db = new();
        List<List<string>> TopUsers = new();

        public LeadersWindow()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Escape: Close(); break;
            }
        }

        private int SorterForSur(List<string> User1, List<string> User2)
        {
            if (int.Parse(User1[1]) > int.Parse(User2[1]))
            {
                return -1;
            }
            else if (int.Parse(User1[1]) == int.Parse(User2[1]))
            {
                return 0;
            }
            else return 1;
        }

        private int SorterForInf(List<string> User1, List<string> User2)
        {
            if (int.Parse(User1[2]) > int.Parse(User2[2]))
            {
                return -1;
            }
            else if (int.Parse(User1[2]) == int.Parse(User2[2]))
            {
                return 0;
            }
            else return 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> Data = new();
            List<User> users = db.Users.ToList();
            foreach (User user in users)
            {
                Data.Add(Convert.ToString(user.Login.Substring(0, user.Login.IndexOf('+'))));
                Data.Add(Convert.ToString(user.Surrecord));
                Data.Add(Convert.ToString(user.Infrecord));
                TopUsers.Add(Data.ToList());
                Data.Clear();
            }
            TopUsers.Sort(SorterForSur);
            switch(TopUsers.Count)
            {
                case 0: break;
                case 1: 
                    SurName1.Text = TopUsers[0][0];
                    Surrec1.Text = TopUsers[0][1];
                    break;
                case 2: 
                    SurName1.Text = TopUsers[0][0];
                    SurName2.Text = TopUsers[1][0];
                    Surrec1.Text = TopUsers[0][1];
                    Surrec2.Text = TopUsers[1][1];
                    break;
                case 3:
                    SurName1.Text = TopUsers[0][0];
                    SurName2.Text = TopUsers[1][0];
                    SurName3.Text = TopUsers[2][0];
                    Surrec1.Text = TopUsers[0][1];
                    Surrec2.Text = TopUsers[1][1];
                    Surrec3.Text = TopUsers[2][1];
                    break;
                case 4:
                    SurName1.Text = TopUsers[0][0];
                    SurName2.Text = TopUsers[1][0];
                    SurName3.Text = TopUsers[2][0];
                    SurName4.Text = TopUsers[3][0];
                    Surrec1.Text = TopUsers[0][1];
                    Surrec2.Text = TopUsers[1][1];
                    Surrec3.Text = TopUsers[2][1];
                    Surrec4.Text = TopUsers[3][1];
                    break;
                case 5:
                    SurName1.Text = TopUsers[0][0];
                    SurName2.Text = TopUsers[1][0];
                    SurName3.Text = TopUsers[2][0];
                    SurName4.Text = TopUsers[3][0];
                    SurName5.Text = TopUsers[4][0];
                    Surrec1.Text = TopUsers[0][1];
                    Surrec2.Text = TopUsers[1][1];
                    Surrec3.Text = TopUsers[2][1];
                    Surrec4.Text = TopUsers[3][1];
                    Surrec5.Text = TopUsers[4][1];
                    break;
                case 6:
                    SurName1.Text = TopUsers[0][0];
                    SurName2.Text = TopUsers[1][0];
                    SurName3.Text = TopUsers[2][0];
                    SurName4.Text = TopUsers[3][0];
                    SurName5.Text = TopUsers[4][0];
                    SurName6.Text = TopUsers[5][0];
                    Surrec1.Text = TopUsers[0][1];
                    Surrec2.Text = TopUsers[1][1];
                    Surrec3.Text = TopUsers[2][1];
                    Surrec4.Text = TopUsers[3][1];
                    Surrec5.Text = TopUsers[4][1];
                    Surrec6.Text = TopUsers[5][1];
                    break;
                case 7:
                    SurName1.Text = TopUsers[0][0];
                    SurName2.Text = TopUsers[1][0];
                    SurName3.Text = TopUsers[2][0];
                    SurName4.Text = TopUsers[3][0];
                    SurName5.Text = TopUsers[4][0];
                    SurName6.Text = TopUsers[5][0];
                    SurName7.Text = TopUsers[6][0];
                    Surrec1.Text = TopUsers[0][1];
                    Surrec2.Text = TopUsers[1][1];
                    Surrec3.Text = TopUsers[2][1];
                    Surrec4.Text = TopUsers[3][1];
                    Surrec5.Text = TopUsers[4][1];
                    Surrec6.Text = TopUsers[5][1];
                    Surrec7.Text = TopUsers[6][1];
                    break;
                case 8:
                    SurName1.Text = TopUsers[0][0];
                    SurName2.Text = TopUsers[1][0];
                    SurName3.Text = TopUsers[2][0];
                    SurName4.Text = TopUsers[3][0];
                    SurName5.Text = TopUsers[4][0];
                    SurName6.Text = TopUsers[5][0];
                    SurName7.Text = TopUsers[6][0];
                    SurName8.Text = TopUsers[7][0];
                    Surrec1.Text = TopUsers[0][1];
                    Surrec2.Text = TopUsers[1][1];
                    Surrec3.Text = TopUsers[2][1];
                    Surrec4.Text = TopUsers[3][1];
                    Surrec5.Text = TopUsers[4][1];
                    Surrec6.Text = TopUsers[5][1];
                    Surrec7.Text = TopUsers[6][1];
                    Surrec8.Text = TopUsers[7][1];
                    break;
                default: goto case 8;
            }
            TopUsers.Sort(SorterForInf);
            switch (TopUsers.Count)
            {
                case 0: break;
                case 1:
                    InfName1.Text = TopUsers[0][0];
                    Infrec1.Text = TopUsers[0][2];
                    break;
                case 2:
                    InfName1.Text = TopUsers[0][0];
                    InfName2.Text = TopUsers[1][0];
                    Infrec1.Text = TopUsers[0][2];
                    Infrec2.Text = TopUsers[1][2];
                    break;
                case 3:
                    InfName1.Text = TopUsers[0][0];
                    InfName2.Text = TopUsers[1][0];
                    InfName3.Text = TopUsers[2][0];
                    Infrec1.Text = TopUsers[0][2];
                    Infrec2.Text = TopUsers[1][2];
                    Infrec3.Text = TopUsers[2][2];
                    break;
                case 4:
                    InfName1.Text = TopUsers[0][0];
                    InfName2.Text = TopUsers[1][0];
                    InfName3.Text = TopUsers[2][0];
                    InfName4.Text = TopUsers[3][0];
                    Infrec1.Text = TopUsers[0][2];
                    Infrec2.Text = TopUsers[1][2];
                    Infrec3.Text = TopUsers[2][2];
                    Infrec4.Text = TopUsers[3][2];
                    break;
                case 5:
                    InfName1.Text = TopUsers[0][0];
                    InfName2.Text = TopUsers[1][0];
                    InfName3.Text = TopUsers[2][0];
                    InfName4.Text = TopUsers[3][0];
                    InfName5.Text = TopUsers[4][0];
                    Infrec1.Text = TopUsers[0][2];
                    Infrec2.Text = TopUsers[1][2];
                    Infrec3.Text = TopUsers[2][2];
                    Infrec4.Text = TopUsers[3][2];
                    Infrec5.Text = TopUsers[4][2];
                    break;
                case 6:
                    InfName1.Text = TopUsers[0][0];
                    InfName2.Text = TopUsers[1][0];
                    InfName3.Text = TopUsers[2][0];
                    InfName4.Text = TopUsers[3][0];
                    InfName5.Text = TopUsers[4][0];
                    InfName6.Text = TopUsers[5][0];
                    Infrec1.Text = TopUsers[0][2];
                    Infrec2.Text = TopUsers[1][2];
                    Infrec3.Text = TopUsers[2][2];
                    Infrec4.Text = TopUsers[3][2];
                    Infrec5.Text = TopUsers[4][2];
                    Infrec6.Text = TopUsers[5][2];
                    break;
                case 7:
                    InfName1.Text = TopUsers[0][0];
                    InfName2.Text = TopUsers[1][0];
                    InfName3.Text = TopUsers[2][0];
                    InfName4.Text = TopUsers[3][0];
                    InfName5.Text = TopUsers[4][0];
                    InfName6.Text = TopUsers[5][0];
                    InfName7.Text = TopUsers[6][0];
                    Infrec1.Text = TopUsers[0][2];
                    Infrec2.Text = TopUsers[1][2];
                    Infrec3.Text = TopUsers[2][2];
                    Infrec4.Text = TopUsers[3][2];
                    Infrec5.Text = TopUsers[4][2];
                    Infrec6.Text = TopUsers[5][2];
                    Infrec7.Text = TopUsers[6][2];
                    break;
                case 8:
                    InfName1.Text = TopUsers[0][0];
                    InfName2.Text = TopUsers[1][0];
                    InfName3.Text = TopUsers[2][0];
                    InfName4.Text = TopUsers[3][0];
                    InfName5.Text = TopUsers[4][0];
                    InfName6.Text = TopUsers[5][0];
                    InfName7.Text = TopUsers[6][0];
                    InfName8.Text = TopUsers[7][0];
                    Infrec1.Text = TopUsers[0][2];
                    Infrec2.Text = TopUsers[1][2];
                    Infrec3.Text = TopUsers[2][2];
                    Infrec4.Text = TopUsers[3][2];
                    Infrec5.Text = TopUsers[4][2];
                    Infrec6.Text = TopUsers[5][2];
                    Infrec7.Text = TopUsers[6][2];
                    Infrec8.Text = TopUsers[7][2];
                    break;
                default: goto case 8;
            }
        }
    }
}
