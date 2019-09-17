using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using System.Windows;

namespace project
{
    class Names
    {
        // Acts as header for importing CSV data.
        public string name { get; set; }
        // Stores names from names.csv.
        public static string[] nameArr;


        /// <summary>
        /// Reads names from names.csv into nameArr.
        /// </summary>
        public static void ReadNames()
        {
            // Try to read file.
            try
            {
                using (StreamReader sr = new StreamReader("names.csv"))
                using (CsvReader csv = new CsvReader(sr))
                {
                    // Reads from CSV.
                    List<Names> nameLst = csv.GetRecords<Names>().ToList();

                    // Initialises array size with list length.
                    nameArr = new string[nameLst.Count];

                    // Adds all the list items to the array.
                    for (int count = 0; count < nameLst.Count; count++)
                    {
                        nameArr[count] = nameLst[count].name;
                    }
                }
            }

            // If names can't be read, display message and exit program.
            catch
            {
                MessageBox.Show("File 'names.csv' could not be found.", "Missing File", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }


        /// <summary>
        /// Main merge sort method.
        /// Navigates through the array, splitting it up and sorting those parts.
        /// </summary>
        /// <param name="left">Left index of subarray.</param>
        /// <param name="right">Right index of subarray.</param>
        public static void MrgSortDriver(int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;

                // Sort left half.
                MrgSortDriver(left, middle);
                // Sort right half.
                MrgSortDriver(middle + 1, right);

                // Call actual sort method.
                MrgSort(left, middle, right);
            }
        }


        /// <summary>
        /// Does the comparing and sorting for the merge sort.
        /// </summary>
        /// <param name="left">Left index of subarray.</param>
        /// <param name="middle">Middle index of subarray.</param>
        /// <param name="right">Right index of subarray.</param>
        private static void MrgSort(int left, int middle, int right)
        {
            // Counter variables.
            int count;
            int count2;
            int leftCount;

            // Subarrays.
            int leftLength = middle - left + 1;
            int rightLength = right - middle;
            string[] leftArr = new string[leftLength];
            string[] rightArr = new string[rightLength];

            // Adds items from array to left subarray.
            for (count = 0; count < leftLength; count++)
            {
                leftArr[count] = nameArr[left + count];
            }

            //Adds items from array to right subarray.
            for (count2 = 0; count2 < rightLength; count2++)
            {
                rightArr[count2] = nameArr[middle + 1 + count2];
            }

            // Resets count variables.
            count = 0;
            count2 = 0;

            // LeftCount counts from left side of array.
            leftCount = left;

            // While both subarrays still have items left.
            while (count < leftLength && count2 < rightLength)
            {
                // Compare left and right values and add lower to main array.
                if (rightArr[count2].CompareTo(leftArr[count]) >= 0)
                {
                    nameArr[leftCount] = leftArr[count];
                    count++;
                }

                else
                {
                    nameArr[leftCount] = rightArr[count2];
                    count2++;
                }

                leftCount++;
            }

            // While left subarray still has items left.
            while (count < leftLength)
            {
                // Add items from left subarray to main array.
                nameArr[leftCount] = leftArr[count];
                count++;
                leftCount++;
            }

            // While right subarray still has items left.
            while (count2 < rightLength)
            {
                // Add items from right subarray to main array.
                nameArr[leftCount] = rightArr[count2];
                count2++;
                leftCount++;
            }
        }


        /// <summary>
        /// Searches the name array for user input using binary search.
        /// </summary>
        /// <param name="target">The input being searched for.</param>
        public static int BinSearch(string target)
        {
            int left = 0;
            int right = nameArr.Length - 1;

            // Loops until there's nothing left to search.
            while (left <= right)
            {
                // Finds middle of subarray.
                int mid = left + (right - left) / 2;

                // If target is found at middle, return index of the item (middle).
                if (nameArr[mid] == target)
                {
                    return mid;
                }

                // If target is greater than middle, move subarray to the right of middle.
                if (nameArr[mid].CompareTo(target) < 0)
                {
                    left = mid + 1;
                }

                // If target is lesser than middle, move subarray to the left of middle.
                else
                {
                    right = mid - 1;
                }
            }

            // Return -1 if the name could not be found.
            return -1;
        }
    }
}
