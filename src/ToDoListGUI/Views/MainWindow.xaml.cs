using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ToDoListGUI.Models;
using ToDoListGUI.ViewModels;

namespace ToDoListGUI.Views
{
    public partial class MainWindow : Window
    {
        private bool _isDarkMode = false;
        private static readonly string[] LightTheme = { "#F3F3F3", "#FFFFFF", "#1B1B1B", "#6B6B6B", "#DEDEDE", "#EBF3FB", "#CCE4F7" };
        private static readonly string[] DarkTheme  = { "#1A1A1A", "#2D2D2D", "#F0F0F0", "#AAAAAA", "#444444", "#2E4468", "#1A3358" };

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void OnTaskStatusChanged(object sender, RoutedEventArgs e) =>
            (DataContext as MainViewModel)?.SaveData();

        private void OnNameDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && sender is TextBlock tb && tb.DataContext is ToDoItem item)
            {
                item.IsEditing = true;
                e.Handled = true;
            }
        }

        private void OnEditBoxVisible(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TextBox tb && (bool)e.NewValue)
                tb.Dispatcher.BeginInvoke(new Action(() =>
                {
                    tb.Focus();
                    tb.SelectAll();
                }), System.Windows.Threading.DispatcherPriority.Render);
        }

        private void OnEditKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (sender is not TextBox tb || tb.DataContext is not ToDoItem item) return;
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                item.IsEditing = false;
                (DataContext as MainViewModel)?.SaveData();
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
                item.IsEditing = false;
        }

        private void OnEditLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && tb.DataContext is ToDoItem item && item.IsEditing)
            {
                item.IsEditing = false;
                (DataContext as MainViewModel)?.SaveData();
            }
        }

        private void OnToggleThemeClick(object sender, RoutedEventArgs e)
        {
            _isDarkMode = !_isDarkMode;
            ApplyTheme(_isDarkMode ? DarkTheme : LightTheme);
            ThemeIcon.Text  = _isDarkMode ? "\uE706" : "\uE708";
            ThemeLabel.Text = _isDarkMode ? "Light Mode" : "Dark Mode";
        }

        private void ApplyTheme(string[] c)
        {
            string[] keys = { "AppBackground", "CardBackground", "TextPrimary",
                               "TextSecondary", "InputBorder", "ListItemHover", "ListItemSelected" };
            for (int i = 0; i < keys.Length; i++)
                Resources[keys[i]] = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString(c[i]));
        }
    }
}
