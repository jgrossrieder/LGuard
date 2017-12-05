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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LGuard.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<AuxiliaryWindow> _windows = new List<AuxiliaryWindow>();
        public MainWindow()
        {
            InitializeComponent();
        }




        private void ShowLock(object sender, RoutedEventArgs e)
        {
                myWindow.Top = Screen.PrimaryScreen.WorkingArea.Top;
                myWindow.Left = Screen.PrimaryScreen.WorkingArea.Left;
            

            foreach (Screen screen in Screen.AllScreens.Where(s=>!s.Primary))
            {
                AuxiliaryWindow auxiliaryWindow = new AuxiliaryWindow()
                {WindowStartupLocation = WindowStartupLocation.Manual,
                    Top = screen.WorkingArea.Top,
                    Left = screen.WorkingArea.Left,
                    WindowState = WindowState.Normal,
                    Width = screen.WorkingArea.Width,
                    Height = screen.WorkingArea.Height
            };
                auxiliaryWindow.Loaded += OnAuxiliaryWindowLoaded;
                _windows.Add(auxiliaryWindow);
                auxiliaryWindow.Show();
            }
            myWindow.Show();
        }

        private void OnAuxiliaryWindowLoaded(object sender, RoutedEventArgs e)
        {
            var senderWindow = sender as Window;
            senderWindow.WindowState = WindowState.Maximized;
        }

        private void HideLock(object sender, RoutedEventArgs e)
        {
            CloseEverything();
        }

        private void CloseEverything()
        {
            myWindow.Hide();
            foreach (AuxiliaryWindow auxiliaryWindow in _windows)
            {
                auxiliaryWindow.Loaded -= OnAuxiliaryWindowLoaded;
                auxiliaryWindow.Close();
            }
            _windows.Clear();
        }

        private void OnWindowKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CloseEverything();
            }
        }
    }
}
