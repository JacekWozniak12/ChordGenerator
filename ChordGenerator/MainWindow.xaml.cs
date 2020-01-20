using System.ComponentModel;
using System.Windows;

namespace ChordGenerator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RuntimeManager      runtimeManager;
        private SyntaxReader        syntaxReader;
        private NAudioCommunication nAudioCommunication;

        public MainWindow()
        {
            InitializeComponent();
            runtimeManager =        new RuntimeManager();
            syntaxReader =          new SyntaxReader();
            nAudioCommunication =   new NAudioCommunication();
            runtimeManager.InitializeApplication();
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            nAudioCommunication.PlaySound(440);
            nAudioCommunication.Dispose();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            SyntaxReader.Instance.ReadInput(NameOfChord.Text.Trim());           
        }

        private void LearnChordsButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}