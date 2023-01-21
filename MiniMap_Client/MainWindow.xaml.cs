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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Windows.Threading;
using System.Configuration;
using System.IO;
using System.Diagnostics;

using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Xceed.Wpf.Toolkit;
using Newtonsoft.Json;
using DotNetty.Buffers;

namespace MiniMap_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //Default theme
        private AppTheme currentTheme = ThemeManager.GetAppTheme("BaseLight");
        private Accent currentAccent = ThemeManager.Accents.First(x => x.Name == "Green");

        private static List<string[]> PreLoadedErrors = new List<string[]>();
        private bool _shutdown;

        public MainWindow()
        {
            Config.Load("config.json");

            DataContext = new MainWindowViewModel()
            {
               AppServers = Globals.Settings.Servers,

               // create accent color menu items for the demo
                AccentColors = ThemeManager.Accents
                                           .Select(a => new AccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush })
                                           .ToList(),

               // create metro theme color menu items for the demo
               AppThemes = ThemeManager.AppThemes
                                          .Select(a => new AppThemeMenuData() { Name = a.Name, BorderColorBrush = a.Resources["BlackColorBrush"] as Brush, ColorBrush = a.Resources["WhiteColorBrush"] as Brush })
                                          .ToList()
            };

            //Change theme to default
            ThemeManager.ChangeAppStyle(Application.Current, currentAccent, currentTheme);
            InitializeComponent();

            //Define global UI Elements          
            Globals.TrackDrawPlateGlobal = TrackDrawPlate;
            Globals.ClientNetStatusGlobal = ClientNetStatus;
            Globals.ChatGlobal = RTB_Chat;
            //Define MainWindow Dispatcher
            Globals.MainWindowDispatcher = Dispatcher.FromThread(System.Threading.Thread.CurrentThread);

            Track.Init("gm_mus_orange_metro_h");
        }

        //Global ShowMessage
        public static async void ShowMessage(string title, string message)
        {
            if (Globals.MainWindowDispatcher.CheckAccess())
            {
                var MW = Application.Current.MainWindow as MetroWindow;
                if (MW.IsLoaded)
                {
                    await MW.ShowMessageAsync(title, message);
                } else
                {
                    PreLoadedErrors.Add(new string[] { title, message });
                }
            }
            else
            {
                await Globals.MainWindowDispatcher.BeginInvoke(new Action(() =>
                 {
                     ShowMessage(title, message);
                 }));
            }
        }

        private async void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            {
                e.Cancel = !_shutdown;
                if (_shutdown) return;

                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Выйти",
                    NegativeButtonText = "Отмена",
                    AnimateShow = true,
                    AnimateHide = false
                };

                var result = await this.ShowMessageAsync("Выйти из программы?",
                    "Вы уверены что хотите выйти?",
                    MessageDialogStyle.AffirmativeAndNegative, mySettings);

                _shutdown = result == MessageDialogResult.Affirmative;

                if (_shutdown)
                {
                    if (Config.Save("config.json", Globals.Settings))
                    {
                        Application.Current.Shutdown();
                    } else
                    {
                        result = await this.ShowMessageAsync("Выйти из программы?",
                        "Ошибка при сохранении настроек. Вы уверены что хотите выйти? Ваши настройки будут утеряны!",
                        MessageDialogStyle.AffirmativeAndNegative, mySettings);

                        _shutdown = result == MessageDialogResult.Affirmative;

                        if (_shutdown)
                            Application.Current.Shutdown();
                    }
                }
            }
        }
        private void ShowSettingsFlyout(object sender, RoutedEventArgs e)
        {
            ToggleFlyout(0);
        }
        private void ToggleFlyout(int index)
        {
            var flyout = this.Flyouts.Items[index] as Flyout;
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
            if (flyout.IsOpen && index == 0) //Hide servers button while flyout options opened
            {
                BtnSrvFlyout.Visibility = Visibility.Collapsed;
                MainMenu.Visibility = Visibility.Collapsed;
            }
            else if (index == 0)
            {
                BtnSrvFlyout.Visibility = Visibility.Visible;
                MainMenu.Visibility = Visibility.Visible;
            }
            if (flyout.IsOpen && index == 1) //Hide menu while flyout open
            {
                MainMenu.Visibility = Visibility.Collapsed;
            }
            else if (index == 1)
            {
                MainMenu.Visibility = Visibility.Visible;
            }
            if (flyout.IsOpen && index == 2 && flyout.Position == Position.Left)
            {
                MainMenu.Visibility = Visibility.Collapsed;
            }
            else if (index == 2 && flyout.Position == Position.Left)
            {
                MainMenu.Visibility = Visibility.Visible;
            }

            //Temp Hack to allow first 30 pix in flyout clickable
            var TempHack = (System.Windows.Controls.Primitives.Thumb)flyout.Template.FindName("PART_WindowTitleThumb", flyout);
            TempHack.Height = 0;
        }
        /*
        private void LightButtonClick(object sender, RoutedEventArgs e)
        {
            currentTheme = ThemeManager.GetAppTheme("BaseLight");
            ThemeManager.ChangeAppStyle(Application.Current, currentAccent, currentTheme);
        }

        private void DarkButtonClick(object sender, RoutedEventArgs e)
        {
            currentTheme = ThemeManager.GetAppTheme("BaseDark");
            ThemeManager.ChangeAppStyle(Application.Current, currentAccent, currentTheme);
        }

        private void GreenButtonClick(object sender, RoutedEventArgs e)
        {
            currentAccent = ThemeManager.Accents.First(x => x.Name == "Green");
            ThemeManager.ChangeAppStyle(Application.Current, currentAccent, currentTheme);
        }

        private void BlueButtonClick(object sender, RoutedEventArgs e)
        {
            currentAccent = ThemeManager.Accents.First(x => x.Name == "Blue");
            ThemeManager.ChangeAppStyle(Application.Current, currentAccent, currentTheme);
        }

        private void RedButtonClick(object sender, RoutedEventArgs e)
        {
            currentAccent = ThemeManager.Accents.First(x => x.Name == "Red");
            ThemeManager.ChangeAppStyle(Application.Current, currentAccent, currentTheme);
        }
         */
        //private async void ShowServerFlyout(object sender, RoutedEventArgs e)
        private void ShowServerFlyout(object sender, RoutedEventArgs e)
        {
            /**
            //this.MetroDialogOptions.ColorScheme = UseAccentForDialogsMenuItem.IsChecked ? MetroDialogColorScheme.Accented : MetroDialogColorScheme.Theme;
            this.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Theme;
            LoginDialogData LDDR = await this.ShowLoginAsync("Authentication", "Введите данные:", new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, InitialUsername = "", AffirmativeButtonText = "Войти", UsernameWatermark = "Логин...", PasswordWatermark = "Пароль..." });
            //var controller = await this.ShowProgressAsync("Please wait...", "Дай мне время! Щас всё будет...", true);
            //await Task.Delay(2000);
            if (LDDR == null)
            {
                //User pressed cancel
                //await controller.CloseAsync();
            }
            else
            {
                var controller = await this.ShowProgressAsync("Please wait...", "Дай мне время! Щас всё будет...", true, new MetroDialogSettings { NegativeButtonText = "Отмена" });
                var mt = new MultiThreading(1);

                mt.Run(Helper.AuthDLThread);

                while (!Globals.downloadCompleted && !controller.IsCanceled)
                {
                    await Task.Delay(1000);
                }

                await controller.CloseAsync();

                if (Globals.downloadSucceeded)
                {
                    BtnAuth.IsEnabled = false;
                    BtnAuth.Content = "В сети: " + LDDR.Username;
                    MessageDialogResult messageResult = await this.ShowMessageAsync("Authentication Information", String.Format("Вы вошли как: {0}", LDDR.Username));
                }
                else
                {
                    MessageDialogResult messageResult = await this.ShowMessageAsync("Authentication Information", "Ошибка! Возможно у вас нет доступа!");
                }
            }
            **/

            ToggleFlyout(1);

        }

        private void TSMainWindowTopmost_ICC(object sender, EventArgs e)
        {
            if ((sender as ToggleSwitch).IsChecked.Value)
            {
                this.Topmost = true;
            }
            else
            {
                this.Topmost = false;
            }
        }

        private void FSettings_ClosingFinished(object sender, RoutedEventArgs e)
        {
            BtnSrvFlyout.Visibility = Visibility.Visible;
            MainMenu.Visibility = Visibility.Visible;
        }
        private void FServers_ClosingFinished(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Visible;
        }
        private void FChat_ClosingFinished(object sender, RoutedEventArgs e)
        {
            var flyout = (Flyout)this.Flyouts.Items[2];
            if (flyout.Position == Position.Left)
                MainMenu.Visibility = Visibility.Visible;
        }

        private void LaunchMiniMapOnGitHub(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/OnlyNoob/Metrostroi-MiniMap");
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Show PreLoadedErrors
            if (PreLoadedErrors.Count != 0)
            {
                foreach(string[] error in PreLoadedErrors)
                {
                    await this.ShowMessageAsync(error[0], error[1]);
                }
            }
        }

        private void ShowChatFlyout(object sender, RoutedEventArgs e)
        {
            ToggleFlyout(2);
        }

        private void Btn_ClientConnect(object sender, RoutedEventArgs e)
        {
            if (Globals.ClientChannel != null && Globals.ClientChannel.Active)
            {
                Net.Client.Close();
            }
            try
            {
                AppServersData SelectedData = (AppServersData)ServersDataGrid.SelectedItem;
                if (SelectedData != null)
                    Net.Client.Connect(SelectedData.IP, SelectedData.Port);
                else
                    ShowMessage("Error", "Выберите сервер!");
            } catch(Exception Ex)
            {
                ShowMessage("Error", "Выберите сервер!");
            }
        }

        private void Btn_ClientDisconnect(object sender, RoutedEventArgs e)
        {
            if (Globals.ClientChannel != null && Globals.ClientChannel.Active)
            {
                Net.Client.Close();
            }
        }
        /* Забаговано смотреть в xaml комментарий! */
        private void CBChatPos_SC(object sender, SelectionChangedEventArgs e)
        {
            var flyout = (Flyout)this.Flyouts.Items[2];
            switch (CBChatPossition.SelectedIndex)
            {
                //case 0:
                    //flyout.Position = Position.Top;
                    //break;
                case 0:
                    flyout.Position = Position.Left;
                    break;
                case 1:
                    flyout.Position = Position.Right;
                    break;
                //case 3:
                    //flyout.Position = Position.Bottom;
                    //break;
            }
        }

        private void TBChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && TBChatInput.Text.Length != 0)
            {
                Net.ClientNetChatObj ChatMessage = new Net.ClientNetChatObj();
                ChatMessage.Name = "Name Soon ^)";
                ChatMessage.Message = TBChatInput.Text;

                Net.ClientNetRootObj SendMessage = new Net.ClientNetRootObj();
                SendMessage.Action = "chat";
                SendMessage.Message = JsonConvert.SerializeObject(ChatMessage);

                if (Globals.ClientChannel != null && Globals.ClientChannel.Active)
                {
                    Net.Client.Send(JsonConvert.SerializeObject(SendMessage));
                    TBChatInput.Clear();
                    Globals.ChatGlobal.Focus();
                }
            }
        }
    }
}
