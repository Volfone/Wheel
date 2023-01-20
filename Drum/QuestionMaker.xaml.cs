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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Drum.Data;

namespace Drum
{
    /// <summary>
    /// Логика взаимодействия для QuestionMaker.xaml
    /// </summary>
    public partial class QuestionMaker : Window
    {
        public QuestionMaker()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(QuestionTB.Text != "" || AnswerTB.Text != "")
            {
                Question newQuestion = new Question(QuestionTB.Text, AnswerTB.Text.ToUpper());
                MongoExamples.AddToDB(newQuestion);
                MainWindow newWindow = new MainWindow();
                this.Close();
                newWindow.Show();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
