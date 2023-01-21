using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;
using System.Net;
using System.Windows.Controls;
using System.Windows.Data;

using MahApps.Metro;
using MahApps.Metro.Controls;

namespace MiniMap_Client
{
    public class SimpleCommand : ICommand
    {
        public Predicate<object> CanExecuteDelegate { get; set; }
        public Action<object> ExecuteDelegate { get; set; }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
                return CanExecuteDelegate(parameter);
            return true; // if there is no can execute default to true
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null)
                ExecuteDelegate(parameter);
        }
    }
    public class AccentColorMenuData
    {
        public string Name { get; set; }
        public Brush BorderColorBrush { get; set; }
        public Brush ColorBrush { get; set; }

        private ICommand changeAccentCommand;

        public ICommand ChangeAccentCommand
        {
            get { return this.changeAccentCommand ?? (changeAccentCommand = new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = x => this.DoChangeTheme(x) }); }
        }

        protected virtual void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var accent = ThemeManager.GetAccent(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
        }
    }

    public class AppThemeMenuData : AccentColorMenuData
    {
        protected override void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
        }
    }
    public class AppServersData
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
    }
    public class CheckServerData : ValidationRule //Все тут поисправлять!!
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            var bindingGroup = value as BindingGroup;
            if (bindingGroup != null)
            {
                if (bindingGroup.Items.Count != 0) // Решить проблему с этим! Этого тут не должно быть!
                {
                    var server = (AppServersData)bindingGroup.Items[0];
                    if ((server.IP == "" || server.IP == null) || (server.Port == 0))
                    {
                        //return new ValidationResult(false, string.Format("Имя {0} | Айпи '{1}' Порт '{2}'!", server.Name, server.IPAddress, server.Port));
                        return new ValidationResult(false, "Поля \"IP\" и \"Порт\" не могу быть пустыми!");
                    }
                }
        }
            return ValidationResult.ValidResult;
        }
    }
    public class MainWindowViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public List<AppServersData> AppServers { get; set; }
        public List<AccentColorMenuData> AccentColors { get; set; }
        public List<AppThemeMenuData> AppThemes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event if needed.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public string this[string columnName]
        {
            get
            {
                // Пример!
                //if (columnName == "EnterIPProperty" && !IPAddress.TryParse(this.EnterIPProperty.ToString(), out this._EnterIPAddress))
                //{
                //   return "Это не IP!";
                //}

                return null;
            }
        }
        public string Error { get { return string.Empty; } }
    }
}
