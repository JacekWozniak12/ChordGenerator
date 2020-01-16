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
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

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
            var sine20Seconds = new SignalGenerator()
            {
                Gain = 0.2,
                Frequency = 42,
                Type = SignalGeneratorType.Sin
            }
                .Take(TimeSpan.FromSeconds(1));
            using (var wo = new WaveOutEvent())
            {
                wo.Init(sine20Seconds);
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(500);
                }
                wo.Dispose();
            }
            

        }

        private void LearnChordsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
