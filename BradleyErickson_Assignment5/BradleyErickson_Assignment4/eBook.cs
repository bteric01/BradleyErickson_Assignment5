using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class eBook:Book
    {
        //Attributes
        private double fileSize;

        //Constructor
        public eBook(string aCallNumber, string aBookTitle, string aAuthorName, string aGenre, string aPublisher, int aNumberOfPages, double aFileSize) :
            base(aCallNumber, aBookTitle, aAuthorName, aGenre, aPublisher, aNumberOfPages)
        {
            fileSize = aFileSize;
        }

        //Accessors or Properties
        public double FileSize
        {
            set { fileSize = value; }
            get { return fileSize; }
        }

    }

