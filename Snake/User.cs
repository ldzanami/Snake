using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Snake
{
    class User
    {
        public int Id { get; set; }
        private string login, headbrush, tailbrush;
        private int surrecord, infrecord, auth;
        public string Login
        {
            get => login;  set { login = value; }
        }
        public string Headbrush
        {
            get => headbrush; set { headbrush = value; }
        }
        public string Tailbrush
        {
            get => tailbrush; set { tailbrush = value; }
        }
        public int Surrecord
        {
            get => surrecord; set { surrecord = value; }
        }
        public int Infrecord
        {
            get => infrecord; set { infrecord = value; }
        }
        public int Auth
        {
            get => auth; set { auth = value; }
        }
        public User() { }

        public User(string login, string headbrush, string tailbrush, int surrecord, int infrecord, int auth)
        {
            this.login = login;
            this.headbrush = headbrush;
            this.tailbrush = tailbrush;
            this.surrecord = surrecord;
            this.infrecord = infrecord;
            this.auth = auth;
        }
    }
}
