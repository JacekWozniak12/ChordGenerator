using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace ChordGenerator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RuntimeManager runtimeManager;
        public SyntaxReader syntaxReader;

        public MainWindow()
        {
            InitializeApplication();
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
            InitializeComponent();
        }

        private void InitializeApplication()
        {
            runtimeManager = new RuntimeManager();
            this.DataContext = runtimeManager;
            //display lang window
            syntaxReader = new SyntaxReader();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            runtimeManager.Close();
        }

        private void Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            ReadInputPrompt();          
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            runtimeManager.PlaySound();
        }

        private void SaveSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            ErrorWindow errorWindow = new ErrorWindow("test");
            errorWindow.Show();
        }

        private void ReadInputPrompt()
        {
            try
            {
                syntaxReader.ReadInput(InputPrompt.Text.Trim());
            }
            catch (Exception err)
            {
                ErrorWindow errorWindow = new ErrorWindow(err.Message);
                errorWindow.Show();
            }
        }
        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            runtimeManager.ChordsPlayed.Clear();
        }

        // TODO change it for enum
        private void Appregio_Selected(object sender, RoutedEventArgs e)
        {
            runtimeManager.AllAtOnce = false;
        }
        private void Chord_Selected(object sender, RoutedEventArgs e)
        {
            runtimeManager.AllAtOnce = true;
        }

        private void ChordsChange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            runtimeManager.RuntimeSettings.Guitar = new Model.Guitar();
            if(GuitarDatabase != null)
            {
                GuitarDatabase.DataContext = runtimeManager.RuntimeSettings.Guitar;
            }
        }
    }
}