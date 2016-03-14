using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Book
    {
        private string callNumber;
        private string bookTitle;
        private string authorName;
        private string genre;
        private int numberOfPages;
        private string publisher;


        public Book(string aCallNumber, string aBookTitle, string aAuthorName, string aGenre, string aPublisher, int aNumberOfPages)
        {
            callNumber = aCallNumber;
            bookTitle = aBookTitle;
            authorName = aAuthorName;
            genre = aGenre;
            numberOfPages = aNumberOfPages;
            publisher = aPublisher;
        }        
            
        
        public string CallNumber
        {
            set { callNumber = value; }
            get { return callNumber; }
        }

        public string BookTitle
        {
            set { bookTitle = value; }
            get { return bookTitle; }
        }

        public string AuthorName
        {
            set { authorName = value; }
            get { return authorName; }
        }

        public string Genre
        {
            set { genre = value; }
            get { return genre; }
        }

        public int NumberOfPages
        {
            set { numberOfPages = value; }
            get { return numberOfPages; }
        }

        public string Publisher
        {
            set { publisher = value; }
            get { return publisher; }
        }
    }

