using System.Windows;

namespace ChordGenerator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RuntimeManager runtimeManager;

        public MainWindow()
        {
            InitializeComponent();
            runtimeManager = new RuntimeManager();
            runtimeManager.InitializeApplication();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            NAudioCommunication.PlaySound(NameOfChord.Text.Trim());
        }

        private void LearnChordsButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}