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

namespace project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Linked list and node for rectangle colours.
        private LinkedList<SolidColorBrush> colourLlst = new LinkedList<SolidColorBrush>();
        private LinkedListNode<SolidColorBrush> colourNode;


        public MainWindow()
        {
            InitializeComponent();

            // Fills list 
            colourLlst.AddLast(Brushes.Red);
            colourLlst.AddLast(Brushes.Orange);
            colourLlst.AddLast(Brushes.Yellow);
            colourLlst.AddLast(Brushes.Lime);
            colourLlst.AddLast(Brushes.DeepSkyBlue);
            colourLlst.AddLast(Brushes.Purple);

            colourNode = colourLlst.First;

            // Reads names from names.csv and displays them in list box.
            Names.ReadNames();
            Display();
        }


        /// <summary>
        /// When Hash is pressed, input from txtHash is salted and hashed.
        /// </summary>
        private void btnHash_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtHash.Text))
            {
                // Calls hash method.
                Hash.SaltandHash(txtHash.Text);
                txtHash.Clear();
            }

            lblResult.Content = "";
        }


        /// <summary>
        /// When Compare is pressed, input from txtHash is compared against previous input.
        /// </summary>
        private void btnCompare_Click(object sender, RoutedEventArgs e)
        {
            // If comparison is true, display result.
            if (Hash.Compare(txtHash.Text))
            {
                lblResult.Foreground = Brushes.Green;
                lblResult.Content = "✓ Match";
            }

            // If comparison is false, display result.
            else
            {
                lblResult.Foreground = Brushes.Red;
                lblResult.Content = "☓ No match";
            }

            txtHash.Clear();
        }


        /// <summary>
        /// When Sort is pressed, the names read from names.csv are sorted and displayed.
        /// </summary>
        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            // Calls the merge sort method.
            Names.MrgSortDriver(0, Names.nameArr.Length - 1);

            Display();

            // Enables the searching function.
            txtSearch.IsEnabled = true;
            btnSearch.IsEnabled = true;
        }


        /// <summary>
        /// Displays all the names read from names.csv.
        /// </summary>
        private void Display()
        {
            lstNames.Items.Clear();
            
            foreach (string name in Names.nameArr)
            {
                lstNames.Items.Add(name);
            }
        }


        /// <summary>
        /// When search is pressed, all the names stored are searched for the user input in txtSearch.
        /// </summary>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // Finds index of search item by calling binary search method.
            int result = Names.BinSearch(txtSearch.Text);

            // If result of binary search is -1, the entry could not be found; make txtSearch red.
            if (result == -1)
            {
                txtSearch.Background = Brushes.Salmon;
            }

            // Otherwise, highlight position of entry in list and make txtSearch green.
            else
            {
                lstNames.SelectedIndex = result;
                txtSearch.Background = Brushes.LightGreen;
            }
        }


        /// <summary>
        /// When txtSearch is edited, make the background white.
        /// This is to reset the colour after searching.
        /// </summary>
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtSearch.Background = Brushes.White;

        }


        /// <summary>
        /// When the left arrow is pressed, display the previous colour in the rectangle.
        /// </summary>
        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            // Try to move to previous.
            try
            {
                colourNode = colourNode.Previous;
                rectColour.Fill = colourNode.Value;
            }

            // If there is no previous item, display message box.
            catch
            {
                MessageBox.Show("You are at the start of the colour list.", "No previous colour");
                // This sets the node back to the first item in the list so that it isn't set to null.
                colourNode = colourLlst.First;
            }
        }


        /// <summary>
        /// When the right arrow is pressed, display the next colour in the rectangle.
        /// </summary>
        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            // Try to move to next.
            try
            {
                colourNode = colourNode.Next;
                rectColour.Fill = colourNode.Value;
            }

            // If there is no last item, display message box.
            catch
            {
                MessageBox.Show("You are at the end of the colour list.", "No next colour");
                // This sets the node back to the last item in the list so that it isn't set to null.
                colourNode = colourLlst.Last;
            }
        }
    }
}
