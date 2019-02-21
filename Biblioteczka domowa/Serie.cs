using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteczka_domowa
{
    class Serie:Publication
    {
        //private string _name;
        //public string Name { get { return _name; } set { _name = value; } }
        ObservableCollection<Book> _booksOfSerie = new ObservableCollection<Book>();
        public ObservableCollection<Book> BooksOfSerie { get { return _booksOfSerie; } set { _booksOfSerie = value; } }
        public Serie(string title, string author)
        {
            _title = title;
            _author = author;
            BooksOfSerie.Add(new Book(3, "teścik", "blabla", "fff", "fdsfs", "blech", "Blargh"));
        }

    }
}
