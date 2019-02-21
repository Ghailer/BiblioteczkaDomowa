using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Biblioteczka_domowa
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Book> Books { get; }
        public ObservableCollection<Publication> pub { get; }
        public Dictionary<int, string> Series { get; }
        public MainWindow()
        {
            
            //bk.Add(new Book());
            Library biblioteka = new Library();
            Books = new ObservableCollection<Book>(biblioteka.Books);
            Series = biblioteka.series;
            //Books = new ObservableCollection<Book>(biblioteka.GetBooksOfSerie(2));
            //biblioteka.AddNewBook("Ziemiomorze2", 2, "LeGuin", DateTime.Now, 6,1, 2, 2, "sss");
            List<Publication> pub = new List<Publication>();
            pub.Add(new Book(1, "test", "testowo", "test", "Dymsior", "Rafał", "Dyms"));
            pub.Add(new Serie("testserie","testowo"));
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBook addBook = new AddBook();
            addBook.Show();
        }
    }
}
