using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*Bradley Erickson--CIS199-01
 * assignmet 4: This assignmetn will allow users to enter information about different books and they will be able to search certain books as well
 * created: 4/2/2014
 */

namespace BradleyErickson_Assignment5
{
    public partial class Form1 : Form
    {
        private TextBox[] textboxes;
        private Label[] labels;
        private List<Book> bookList;
        private Book aBook;
        private List<Book> searchResult;
        private Label[] searchLabels;
        private TextBox[] ErrorCheck;

        public Form1()
        {
            InitializeComponent();
            textboxes = new TextBox[] {txtAuthorName, txtBookTitle, txtCallNumber, txtGenre, txtNumberOfPages,txtPublisher, txtFileSize};
            labels = new Label[] { lblAuthorName, lblBookTitle, lblCallNumber, lblGenre, lblNumberOfPages, lblPublisher,lblFileSize};
            bookList = new List<Book>();
            searchLabels = new Label[] { lblSearchAuthorName, lblSearchBookTitle, lblSearchCallNumber, lblSearchFileSize, lblSearchGenre, lblSearchNumberOfPages, lblSearchPublisher };
            ErrorCheck=new TextBox[] {txtAuthorName, txtBookTitle, txtCallNumber, txtGenre, txtNumberOfPages, txtPublisher};
            txtFileSize.Enabled = false;
        }



        private void PopulateListBox(List<Book> aList, ListBox aListBox)
        {
            aListBox.Items.Clear();

            foreach (Book book in aList)
            {
                aListBox.Items.Add(book.BookTitle);
            }
        }


        private void AddBookToList(string aCallNumber, string aBookTitle, string aAuthorName, string aGenre, string aPublisher, int aNumberOfPages)
        {
            aBook = new Book(aCallNumber, aBookTitle, aAuthorName, aGenre, aPublisher, aNumberOfPages);
           
            bookList.Add(aBook);
        }

        private void AddBookToList(string aCallNumber, string aBookTitle, string aAuthorName, string aGenre, string aPublisher, int aNumberOfPages, double aFileSize)
        {
            aBook = new eBook(aCallNumber, aBookTitle, aAuthorName, aGenre, aPublisher, aNumberOfPages, aFileSize);

            bookList.Add(aBook);
        }



        private bool AllInputEntered(TextBox[] textboxArray)
        {
            bool allInputEntered = true;

            if (chkEBook.Checked)
            {
                foreach (TextBox textbox in textboxArray)
                {
                    if (string.IsNullOrWhiteSpace(textbox.Text))
                    {
                        allInputEntered = false;
                    }
                }
            }
            else
            {
                foreach (TextBox textbox in ErrorCheck)
                {
                    if(string.IsNullOrWhiteSpace(textbox.Text))
                    {
                        allInputEntered = false;
                    }
                }
            }

                     
            return allInputEntered;
        }


        private void NewEntry()
        {
            chkEBook.Checked = false;
            foreach (TextBox textbox in textboxes)
            {
                textbox.Clear();
            }
            foreach (Label outputLabel in labels)
            {
                outputLabel.Text = "";
            }
            txtCallNumber.Focus();
            lstBookTitles.SelectedIndex = -1;
            chkEBook.Checked = false;
            txtFileSize.Clear();
        }

        private void btnAddBookToList_Click(object sender, EventArgs e)
        {

            int numberOfPages = 0;
            double fileSize = 0;

            if (AllInputEntered(textboxes))
            {
                if (int.TryParse(txtNumberOfPages.Text, out numberOfPages))
                {
                    if (chkEBook.Checked)
                    {
                        if (double.TryParse(txtFileSize.Text, out fileSize))
                        {
                            AddBookToList(txtCallNumber.Text, txtBookTitle.Text, txtAuthorName.Text, txtGenre.Text,
                               txtPublisher.Text, int.Parse(txtNumberOfPages.Text), double.Parse(txtFileSize.Text));
                            NewEntry();
                        }
                        else
                        {
                            MessageBox.Show("Must enter a Numeric Value for File Size.", "Non-Numeric Input Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtFileSize.Focus();
                        }
                    }
                    else
                    {
                        AddBookToList(txtCallNumber.Text, txtBookTitle.Text, txtAuthorName.Text, txtGenre.Text,
                            txtPublisher.Text, int.Parse(txtNumberOfPages.Text));
                        NewEntry();
                    }

                    PopulateListBox(bookList, lstBookTitles);
                }
                else
                {
                    MessageBox.Show("You must enter an integer value for the number of pages.", "Non-Numeric Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNumberOfPages.Focus();
                }
            }
            else
            {
                MessageBox.Show("You must enter information into all textboxes", "Missing Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             
        }


        private Book GetBook(int index)
        {
            return bookList[index];
        }

        private Book GetBookFromSearchResult(int index)
        {
            return searchResult[index];
        }


        private void lstBookTitles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBookTitles.SelectedIndex != -1)
            {
                lblAuthorName.Text = GetBook(lstBookTitles.SelectedIndex).AuthorName;
                lblBookTitle.Text = GetBook(lstBookTitles.SelectedIndex).BookTitle;
                lblCallNumber.Text = GetBook(lstBookTitles.SelectedIndex).CallNumber;
                lblGenre.Text = GetBook(lstBookTitles.SelectedIndex).Genre;
                lblNumberOfPages.Text = GetBook(lstBookTitles.SelectedIndex).NumberOfPages.ToString();
                lblPublisher.Text = GetBook(lstBookTitles.SelectedIndex).Publisher;

                if (GetBook(lstBookTitles.SelectedIndex).GetType().ToString() == "eBook")
                {
                    lblFileSize.Text = ((eBook)GetBook(lstBookTitles.SelectedIndex)).FileSize.ToString();
                }
                else
                {
                    lblFileSize.Text = "";
                }
            }
        }

        private void ResetViewBooks()
        {
            lstBookTitles.SelectedIndex = -1;

            foreach (Label label in labels)
            {
                label.Text = "";
            }
        }


        private List<Book> SearchByCallNumber(List<Book> aList, string searchValue)
        {
            searchResult = new List<Book>();

            foreach (Book book in aList)
            {
                if (book.CallNumber.ToUpper() == searchValue.ToUpper())
                {
                    searchResult.Add(book);
                }
            }

            return searchResult;
        }


        private List<Book> SearchByAuthorName(List<Book> aList, string searchValue)
        {
            searchResult = new List<Book>();

            foreach (Book book in aList)
            {
                if (book.AuthorName.ToUpper() == searchValue.ToUpper())
                {
                    searchResult.Add(book);
                }
            }

            return searchResult;
        }


        private List<Book> SearchByGenre(List<Book> aList, string searchValue)
        {
            searchResult = new List<Book>();

            foreach (Book book in aList)
            {
                if (book.Genre.ToUpper() == searchValue.ToUpper())
                {
                    searchResult.Add(book);
                }
            }

            return searchResult;
        }


        private List<Book> SearchByPublisher(List<Book> aList, string searchValue)
        {
            searchResult = new List<Book>();

            foreach (Book book in aList)
            {
                if (book.Publisher.ToUpper() == searchValue.ToUpper())
                {
                    searchResult.Add(book);
                }
            }

            return searchResult;
        }

        private void cboSearchCriterion_SelectedIndexChanged(object sender, EventArgs e)
        {
            const int CALLNUMBER_INDEX = 0;
            const int AUTHORNAME_INDEX = 1;
            const int GENRE_INDEX = 2;
            const int PUBLISHER_INDEX = 3;


            if (cboSearchCriterion.SelectedIndex != -1)
            {
                if (!string.IsNullOrWhiteSpace(txtSearchValue.Text))
                {
                    switch (cboSearchCriterion.SelectedIndex)
                    {
                        case CALLNUMBER_INDEX:
                            if (SearchByCallNumber(bookList, txtSearchValue.Text).Count > 0)
                            {
                                PopulateListBox(SearchByCallNumber(bookList, txtSearchValue.Text), lstSearchResult);
                            }
                            else
                            {
                                MessageBox.Show("No books by the Call Number  " + txtSearchValue.Text + " were found.", "Books Not Found",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case AUTHORNAME_INDEX:
                            if (SearchByAuthorName(bookList, txtSearchValue.Text).Count > 0)
                            {
                                PopulateListBox(SearchByAuthorName(bookList, txtSearchValue.Text), lstSearchResult);
                            }
                            else
                            {
                                MessageBox.Show("No books by the Author " + txtSearchValue.Text + " were found", "Books Not Found",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case GENRE_INDEX:
                            if (SearchByGenre(bookList, txtSearchValue.Text).Count > 0)
                            {
                                PopulateListBox(SearchByGenre(bookList, txtSearchValue.Text), lstSearchResult);
                            }
                            else
                            {
                                MessageBox.Show("No book in the " + txtSearchValue.Text + " genre was found.", "Genre Not Found",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case PUBLISHER_INDEX:
                            if (SearchByPublisher(bookList, txtSearchValue.Text).Count > 0)
                            {
                                PopulateListBox(SearchByPublisher(bookList, txtSearchValue.Text), lstSearchResult);
                            }
                            else
                            {
                                MessageBox.Show("No books published by " + txtSearchValue.Text + " were found.", "Publisher Not Found",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                    }
                    NewSearch();
                }
                else
                {
                    MessageBox.Show("You must enter a search value first!", "Missing Search Value Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void NewSearch()
        {
            cboSearchCriterion.SelectedIndex = -1;
            txtSearchValue.Clear();
            txtSearchValue.Focus();
        }

        private void lstSearchResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSearchResult.SelectedIndex != -1)
            {
                lblSearchAuthorName.Text = GetBookFromSearchResult(lstSearchResult.SelectedIndex).AuthorName;
                lblSearchBookTitle.Text = GetBookFromSearchResult(lstSearchResult.SelectedIndex).BookTitle;
                lblSearchCallNumber.Text = GetBookFromSearchResult(lstSearchResult.SelectedIndex).CallNumber;
                lblSearchGenre.Text = GetBookFromSearchResult(lstSearchResult.SelectedIndex).Genre;
                lblSearchNumberOfPages.Text = GetBookFromSearchResult(lstSearchResult.SelectedIndex).NumberOfPages.ToString();
                lblSearchPublisher.Text = GetBookFromSearchResult(lstSearchResult.SelectedIndex).Publisher;

                if (GetBookFromSearchResult(lstSearchResult.SelectedIndex).GetType().ToString() == "eBook")
                {
                    lblSearchFileSize.Text = ((eBook)GetBookFromSearchResult(lstSearchResult.SelectedIndex)).FileSize.ToString();
                }
                else
                {
                    lblSearchFileSize.Text = "";
                }
            }
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchValue.Clear();
            lblSearchAuthorName.Text = "";
            lblSearchBookTitle.Text = "";
            lblSearchCallNumber.Text = "";
            lblSearchGenre.Text = "";
            lblSearchNumberOfPages.Text = "";
            lblSearchPublisher.Text = "";
            cboSearchCriterion.SelectedIndex = -1;
            txtSearchValue.Focus();
            lstSearchResult.Items.Clear();
        }

        private void chkEBook_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEBook.Checked)
            {
                txtFileSize.Enabled = true;
            }
            else
            {
                txtFileSize.Enabled = false;
            }
        }
        
           
    }
}
