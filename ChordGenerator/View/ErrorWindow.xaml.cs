using System;
using System.ComponentModel;
using System.Windows;

namespace ChordGenerator
{
    /// <summary>
    /// Logika interakcji dla klasy ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(string Error)
        {
            InitializeComponent();
            Error_Content.Text = Error;
        }
    }
}
