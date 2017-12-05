using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Navigation;
using Gma.System.MouseKeyHook;
using Application = System.Windows.Application;

namespace LGuard.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKeyboardMouseEvents _globalHook;
        private bool _lwinDown = false;
        private bool _rwinDown = false;

        /*


        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey(
            [In] IntPtr hWnd,
            [In] int id,
            [In] uint fsModifiers,
            [In] uint vk);

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey(
            [In] IntPtr hWnd,
            [In] int id);

        private HwndSource _source;
        private const int HOTKEY_ID = 9000;

        



        private void RegisterHotKey()
        {
            var helper = new WindowInteropHelper(MainWindow);
            const uint VK_L = 0x4F;// 0x4C;
            const uint MOD_CTRL = 0x0008;
            if (!RegisterHotKey(helper.Handle, HOTKEY_ID, MOD_CTRL, VK_L))
            {
                // handle error
            }
        }

        private void UnregisterHotKey()
        {
            var helper = new WindowInteropHelper(MainWindow);
            UnregisterHotKey(helper.Handle, HOTKEY_ID);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            OnHotKeyPressed();
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void OnHotKeyPressed()
        {
            // do stuff
        }
        

        private void Application_Exit(object sender, ExitEventArgs e)
        {

            _source.RemoveHook(HwndHook);
            _source = null;
            UnregisterHotKey();
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow =new MainWindow();
            MainWindow.Show();
            MainWindow.Hide();
            var helper = new WindowInteropHelper(MainWindow);
            _source = HwndSource.FromHwnd(helper.Handle);
            _source.AddHook(HwndHook);
            RegisterHotKey();

        }*/

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += _globalHook_KeyDown;
            _globalHook.KeyUp += _globalHook_KeyUp;
        }

        private void _globalHook_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LWin)
            {
                _lwinDown = false;
            }
            else if (e.KeyCode == Keys.RWin)
            {
                _rwinDown = false;
            }
        }

        private void _globalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LWin)
            {
                _lwinDown = true;
            }
            else if (e.KeyCode == Keys.RWin)
            {
                _rwinDown = true;
            }
            else if (e.KeyCode == Keys.L && (_lwinDown || _rwinDown))
            {
                e.Handled = true;

            }
        }
    }
}
