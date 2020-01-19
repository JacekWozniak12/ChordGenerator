using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ChordGenerator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            NAudioCommunication.PlaySound(0);

          /*  NAudioCommunication.
                PlaySounds(
                new MusicalNote[]
                {
                    new MusicalNote("A4", 440),
                    new MusicalNote("A5", 880)
                }
                , 0.5f, 1, NAudio.Wave.SampleProviders.SignalGeneratorType.Sin);
           */
        }

        private void LearnChordsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
