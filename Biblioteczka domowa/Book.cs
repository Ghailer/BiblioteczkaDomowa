using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Biblioteczka_domowa
{
    public class Book:Publication
    {
        string _cover;
        string _serie;
        //string _title;
        //string _author;
        ushort _printYear;
        string _status;
        string _owner;
        string _holder;
        int _id;
        public int Id { get { return _id; } }
        public string Serie { get { return _serie; } }
        //public string Title { get { return _title; } }
        //public string Author
        //{
        //    get { return _author; }
        //}
        public string Owner { get { return _owner; }}
        public string Holder { get { return _holder; }}
        public string Status { get { return _status; }}
        /*public System.Windows.Media.ImageSource Cover
        {
            get
            {
                return Image.FromFile(@"C:\Users\Rafał\Desktop\IMG_20180410_092111.jpg");   
            }
        }*/

        

        //public Image Cover { get { return Image.FromStream(); } }

        public Book(int id, string title,  string serie, string status, string autor, string owner, string holder)
        {
            
            this._id = id;
            this._title = title;
            this._serie = serie;
            this._status = status;
            this._author = autor;
            this._owner = owner;
            this._holder = holder;
        }
    }
}
