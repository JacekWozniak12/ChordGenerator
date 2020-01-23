using System;
using System.ComponentModel;
using System.Windows;

namespace ChordGenerator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RuntimeManager runtimeManager;
        private SyntaxReader syntaxReader;

        public MainWindow()
        {
            InitializeComponent();
            InitializeApplication();
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
        }

        private void InitializeApplication()
        {
            runtimeManager =        new RuntimeManager();
            //display lang window
            syntaxReader =          new SyntaxReader();
            ReadInputPrompt();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            runtimeManager.Dispose();
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

        }

        private void ReadInputPrompt()
        {
            try
            {
                syntaxReader.ReadInput(InputPrompt.Text.Trim());
            }
            catch (ArgumentException err)
            {
                // Łukasz, tu napisz mi przechwytywanie błędu
            }
            finally
            {
            }
        }
    }
}