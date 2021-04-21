using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModSixBeyond
{
    public partial class Form1 : Form
    {
        private const decimal CONTRIB_RATE = 0.05m;

        public Form1()
        {
            InitializeComponent();
        }

        #region Function Practice

        private void setValue(int value)
        {
            valueLabel.Text = value.ToString();
        }

        private void value100Button_Click(object sender, EventArgs e)
        {
            setValue(100);
        }

        private void set455Button_Click(object sender, EventArgs e)
        {
            setValue(455);
        }

        private bool InputIsValid(ref decimal pay, ref decimal bonus)
        {
            bool inputGood = false;

            if(decimal.TryParse(grossPayTextBox.Text, out pay))
            {
                if(decimal.TryParse(bonusTextBox.Text, out bonus))
                {
                    inputGood = true;
                }
                else
                {
                    MessageBox.Show("Bonus amount is invalid.");
                }
            }
            else
            {
                MessageBox.Show("Gross pay is invalid.");
            }

            return inputGood;
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            decimal grossPay = 0m, bonus = 0m, contributions = 0m;

            if(InputIsValid(ref grossPay, ref bonus))
            {
                contributions = (grossPay + bonus) * CONTRIB_RATE;

                contributionLabel.Text = contributions.ToString("c");
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Calorie Counter

        private int calculateFatCalories(int fatGrams)
        {
            return fatGrams * 9;
        }

        private int calculateCarbCalories(int carbGrams)
        {
            return carbGrams * 4;
        }

        private void updateCalorieText(in int fatCals, in int carbCals)
        {
            caloriesFatLabel.Text = fatCals.ToString();
            caloriesCarbsLabel.Text = carbCals.ToString();
            int totalCals = fatCals + carbCals;
            totalCaloriesLabel.Text = totalCals.ToString();
        }

        private bool gramsValid(ref int fatGrams, ref int carbGrams)
        {
            if(int.TryParse(fatGramsTextBox.Text, out fatGrams))
            {
                if(int.TryParse(carbGramsTextBox.Text, out carbGrams))
                {
                    return true;
                }
            }
            return false;
        }

        private void calculateCaloriesButton_Click(object sender, EventArgs e)
        {
            int fatGrams = 0, carbGrams = 0;
            if(gramsValid(ref fatGrams, ref carbGrams))
            {
                int fatCals = calculateFatCalories(fatGrams);
                int carbCals = calculateCarbCalories(carbGrams);
                updateCalorieText(in fatCals, in carbCals);
            }
        }
        #endregion

        #region ArrayPractice

        private void getNamesButton_Click(object sender, EventArgs e)
        {
            const int SIZE = 3;
            string[] names = new string[SIZE];

            names[0] = name1TextBox.Text;
            names[1] = name2TextBox.Text;
            names[2] = name3TextBox.Text;

            nameLabel.Text = names[0] + "," + names[1] + "," + names[2];
        }

        private void okSelectionButton_Click(object sender, EventArgs e)
        {
            string selection;

            string[] provinces = { "BC", "Alberta" };

            if(selectionListBox.SelectedIndex != -1)
            {
                selection = selectionListBox.SelectedItem.ToString();

                if(SequentialSearch(provinces,selection) != -1)
                {
                    MessageBox.Show("That is the best province");
                }
                else
                {
                    MessageBox.Show("Nope, you're wrong");
                }
            }
        }
        #endregion

        #region Searching / Sorting

        private int SequentialSearch(string[] sArray, string value)
        {
            bool found = false;
            int index = 0;
            int position = -1;

            while(!found && index < sArray.Length)
            {
                if(sArray[index] == value)
                {
                    found = true;
                    position = index;
                }
                index++;
            }
            return position;
        }

        private void SelectionSort(int[] iArray)
        {
            int minIndex, minValue;

            for(int startScan = 0; startScan < iArray.Length-1; startScan++)
            {
                minIndex = startScan;
                minValue = iArray[startScan];

                for(int index = startScan +1; index < iArray.Length; index++)
                {
                    if(iArray[index] < minValue)
                    {
                        minValue = iArray[index];
                        minIndex = index;
                    }
                }

                Swap(ref iArray[minIndex], ref iArray[startScan]);
            }
        }

        private void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        private void selectionSortButton_Click(object sender, EventArgs e)
        {
            int[] numbers = { 4, 6, 1, 3, 5, 2 };

            foreach(int value in numbers)
            {
                unsortedListBox.Items.Add(value);
            }

            //Sort the array
            SelectionSort(numbers);

            foreach(int value in numbers)
            {
                sortedListBox.Items.Add(value);
            }
        }

        private int BinarySearch(int[] iArray, int value)
        {
            int first = 0;
            int last = iArray.Length - 1;
            int middle;
            int position = -1;
            bool found = false;

            while(!found && first <= last)
            {
                middle = (first + last) / 2;

                if(iArray[middle] == value)
                {
                    found = true;
                    position = middle;
                    return position;
                }
                else if(iArray[middle] > value)
                {
                    last = middle - 1;
                }
                else
                {
                    first = middle + 1;
                }
            }
            return position;
        }

        private void binarySearchButton_Click(object sender, EventArgs e)
        {
            List<int> numbers = new List<int>();

            foreach (int value in sortedListBox.Items)
            {
                numbers.Add(value);
            }

            binarySearchLabel.Text = searchValueTextBox.Text + " is at index: " +  BinarySearch(numbers.ToArray(), 6).ToString();
        }

        #endregion

        #region Password Verifier

        private void checkPasswordButton_Click(object sender, EventArgs e)
        {
            const int MIN_LENGTH = 8;
            string password = passwordTextBox.Text;
            int upperCaseCount = 0, lowerCaseCount = 0, digitCount = 0;
            UpperLowerDigitCount(password, ref upperCaseCount, ref lowerCaseCount, ref digitCount);

            if(password.Length >= MIN_LENGTH && upperCaseCount >= 1 && lowerCaseCount >= 1 && digitCount >= 1)
            {
                MessageBox.Show("The password is valid.");
            }
            else
            {
                MessageBox.Show("The password is invalid");
            }
        }

        private void UpperLowerDigitCount(string str, ref int upperCaseCount, ref int lowerCaseCount, ref int digitCount)
        {
            foreach(char ch in str)
            {
                if(char.IsUpper(ch))
                {
                    upperCaseCount++;
                }
                else if(char.IsLower(ch))
                {
                    lowerCaseCount++;
                }
                else if(char.IsDigit(ch))
                {
                    digitCount++;
                }
            }
        }

        #endregion

        #region Telephone Formatter

        private void formatButton_Click(object sender, EventArgs e)
        {
            string input = telephoneNumberTextBox.Text.Trim();

            if(IsValidNumber(input))
            {
                TelephoneFormat(ref input);
                telephoneNumberTextBox.Text = input;
            }
            else
            {
                MessageBox.Show("Invalid Input");
            }
        }

        private bool IsValidNumber(string str)
        {
            const int VALID_LENGTH = 10;
            bool valid = true;

            if(str.Length == VALID_LENGTH)
            {
                foreach(char ch in str)
                {
                    if(!char.IsDigit(ch))
                    {
                        //It's not a digit so return false
                        valid = false;
                    }
                }
            }
            else
            {
                valid = false; //Not long enough
            }
            return valid;
        }

        private void TelephoneFormat(ref string str)
        {
            str = str.Insert(0, "("); //Insert ( at index 0
            str = str.Insert(4, ")");//Insert ) at index 4
            str = str.Insert(8, "-");//Insert - at index 8
        }

        private void Unformat(ref string str)
        {
            str = str.Remove(0, 1);
            str = str.Remove(3, 1);
            str = str.Remove(6, 1);
        }

        private void unformatButton_Click(object sender, EventArgs e)
        {
            string input = telephoneNumberTextBox.Text.Trim();
            Unformat(ref input);
            telephoneNumberTextBox.Text = input;
        }

        #endregion

        #region CSV Reader

        private void getScoresButton_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader inputFile;
                string line;
                int count = 0, total;
                double average;

                char[] delim = { ',' };

                inputFile = File.OpenText("Grades.csv");

                while(!inputFile.EndOfStream)
                {
                    count++;
                    line = inputFile.ReadLine();
                    string[] tokens = line.Split(delim);
                    total = 0;

                    foreach(string str in tokens)
                    {
                        total += int.Parse(str);
                    }

                    average = (double)total / tokens.Length;

                    averagesListBox.Items.Add("The average for student " + count + " is " + average.ToString("n1"));
                }
                inputFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region CAR LIST

        struct Automobile
        {
            public string make;
            public int year;
            public double mileage;
        }

        private List<Automobile> carList = new List<Automobile>();

        private void GetData(ref Automobile auto)
        {
            try
            {
                auto.make = makeTextBox.Text;
                auto.year = int.Parse(yearTextBox.Text);
                auto.mileage = double.Parse(mileageTextBox.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addCarButton_Click(object sender, EventArgs e)
        {
            Automobile car = new Automobile();
            GetData(ref car);
            carList.Add(car);
            makeTextBox.Clear();
            mileageTextBox.Clear();
            yearTextBox.Clear();
            makeTextBox.Focus();
        }

        private void displayCarListButton_Click(object sender, EventArgs e)
        {
            string output;
            carListBox.Items.Clear();
            foreach(Automobile aCar in carList)
            {
                output = aCar.year + " " + aCar.make + " with " + aCar.mileage + " miles.";
                carListBox.Items.Add(output);
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            string word = wordTextBox.Text;
            string[] vowels = { "a", "e", "i", "o", "u" };
            int vowelCount = 0, consonantCount = 0;

            foreach(char ch in word)
            {
                if(char.IsLetter(ch))
                {
                    //If it's a letter, lets verify if it's a vowel
                    switch(ch)
                    {
                        case 'a':
                            vowelCount++;
                            break;

                        case 'e':
                            vowelCount++;
                            break;

                        case 'i':
                            vowelCount++;
                            break;

                        case 'o':
                            vowelCount++;
                            break;

                        case 'u':
                            vowelCount++;
                            break;

                        default:
                            //If it's not a vowel, then it's a consonant
                            consonantCount++;
                            break;
                    }
                }
            }
            vowelCountLabel.Text = vowelCount.ToString();
            consonantCountLabel.Text = consonantCount.ToString();
        }
    }
}
