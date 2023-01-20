using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Drum.Data;

namespace Drum
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        string neededWord = "";
        List<char> chars = new List<char>();
        int letterCount = 0;
        List<char> firstList = new List<char>();
        List<char> secondList = new List<char>();
        List<char> Word = new List<char>();

        public MainWindow()
        {
            InitializeComponent();
            GetRandomQuestion();
            CreateLettersCollection();
        }
        public void Update()
        {
            LetterPanel.Children.Clear();
            for (int i = 0; i < neededWord.Length; i++)
            {
                TextBox TB = new TextBox();
                TB.Name = "TextBox" + "_" + i;
                TB.TextAlignment = TextAlignment.Center;
                TB.FontSize = 19;
                TB.IsEnabled = false;
                TB.BorderBrush = Brushes.Black;
                TB.Height = 30;
                TB.Width = 30;
                LetterPanel.Children.Add(TB);
            }
            for(int i = 0; i < Word.Count; i++)
            {
                foreach(TextBox TB in LetterPanel.Children)
                {
                    if (TB.Name == "TextBox_" + i)
                    {
                        TB.Text += Word[i];
                    }
                }
            }

            ButtonPanel.Children.Clear();
            ButtonPanel_2.Children.Clear();
            ButtonPanel_3.Children.Clear();
            ButtonPanel_4.Children.Clear();
            for (int i = 0; i < secondList.Count; i++)
            {
                Button btn = new Button();
                btn.Name = "Button" + "_" + i;
                btn.Content = secondList[i];
                btn.FontSize = 12;
                btn.Height = 25;
                btn.Width = 25;
                btn.Click += Click;
                btn.Margin = new Thickness(10, 0, 0, 0);
                if (secondList[i] == ' ')
                {
                    btn.Visibility = Visibility.Hidden;
                }
                if (i < 10)
                {
                    ButtonPanel.Children.Add(btn);
                }
                if (i < 20 && i >= 10)
                {
                    ButtonPanel_2.Children.Add(btn);
                }
                if (i < 30 && i >= 20)
                {
                    ButtonPanel_3.Children.Add(btn);
                }
                if (i <= 50 && i >= 30)
                {
                    ButtonPanel_4.Children.Add(btn);
                }
            }
        }
        public void Click(object sender, RoutedEventArgs e)
        {
            int num = Convert.ToInt32((sender as Button).Name.Split('_')[1]);
            /*MessageBox.Show($"{num} {secondList[num]}");*/
            bool isFilled = false;
            bool isSame = false;
            foreach (TextBox letter in LetterPanel.Children)
            {
                if(letter.Text == "" && !isFilled)
                {
                    isFilled = true;
                    Word.Add(secondList[num]);
                    letter.Text = secondList[num].ToString();
                }
                if (neededWord.Length == Word.Count)
                {
                    for (int i = 0; i < Word.Count; i++)
                    {
                        if (neededWord[i] == Word[i])
                        {
                            isSame = true;
                        }
                        else
                        {
                            isSame = false;
                        }
                    }
                }
                if (letterCount == (Convert.ToInt32(letter.Name.Split('_')[1]) + 1) && letter.Text != ""/* && isSame*/)
                {
                    string completedWord = "";
                    foreach(var symbol in Word)
                    {
                        completedWord += symbol.ToString();
                    }
                    if(neededWord == completedWord)
                    {
                        MessageBox.Show($"{completedWord} верно");
                        Word = new List<char>();
                        secondList = firstList;
                        GetRandomQuestion();
                        CreateLettersCollection();
                        Shuffle();
                        Update();
                        return;
                    }
                    else
                    {
                        MessageBox.Show($"{completedWord} неверно");
                        Word = new List<char>();
                        Shuffle();
                        Update();
                        return;
                    }
                }
            }
            (sender as Button).Visibility = Visibility.Hidden;
            secondList[num] = ' ';
            Update();
            /*Panel.Children.RemoveAt(num);*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateQuestionBtn_Click(object sender, RoutedEventArgs e)
        {
            QuestionMaker newWindow = new QuestionMaker();
            this.Close();
            newWindow.Show();
        }
        public void Shuffle()
        { 
            for(int i = 0; i < firstList.Count; i++)
            {
                int j = random.Next(firstList.Count);
                char temp = firstList[j];
                firstList[j] = firstList[i];
                firstList[i] = temp;
            }
            secondList.Clear();
            foreach (char c in firstList)
            {
                secondList.Add(c);
            }
        }
        public void GetRandomQuestion()
        {
            List<Question> questions = MongoExamples.FindAll();
            if (questions.Count != 0)
            {
                Question question = questions[random.Next(questions.Count)];
                neededWord = question.Answer;
                QuestionTB.Text = question.Quest;
            }
            Word = new List<char>();
        }
        public void CreateLettersCollection()
        {
            chars = new List<char>();
            firstList = new List<char>();
            secondList = new List<char>();
            letterCount = 0;

            for (int i = 0; i < 40; i++)
            {
                chars.Add(' ');
            }
            foreach (var i in neededWord)
            {
                letterCount++;
                int j = random.Next(chars.Count);
                while (chars[j] != ' ')
                {
                    j = random.Next(chars.Count);
                }
                chars[j] = i;
            }
            for (int i = 0; i < 40; i++)
            {
                if (chars[i] == ' ')
                {
                    chars[i] = Convert.ToChar(random.Next(1040, 1071));
                }
            }
            for (int i = 0; i < chars.Count; i++)
            {
                firstList.Add(chars[i]);
            }
            foreach (char c in firstList)
            {
                secondList.Add(c);
            }
            Update();
        }
    }
}
