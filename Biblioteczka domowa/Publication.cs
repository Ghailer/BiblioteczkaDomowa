using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteczka_domowa
{
    public class Publication
    {
        protected string _title;
        protected string _author;

        public string Title { get { return _title; } }
        public string Author
        {
            get { return _author; }
        }
    }
}
