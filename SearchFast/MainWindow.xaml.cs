using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using System.Globalization;

namespace SearchFast
{
    public class Cmd : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            MessageBox.Show(parameter.ToString());
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Item> ListItems { get; private set; }

        public MainWindow()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
            ListItems = new ObservableCollection<Item>();
            InitializeComponent();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                Dispatcher.Invoke(new WaitChangeHandler(WaitChange), true);
                Backend.Update();
                Dispatcher.Invoke(new WaitChangeHandler(WaitChange), false);
            }
            ).Start();
        }

        private delegate void WaitChangeHandler(bool wait);
        private void WaitChange(bool wait)
        {
            foreach (UIElement v in new UIElement[] { comboBox, updateBtn })
            {
                v.IsEnabled = !wait;
            }
            if (wait)
            {
                Cursor = Cursors.Wait;
            }
            else
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void comboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListItems.Clear();

            string s = comboBox.Text.ToLower();

            var list = Backend.Get(it => it.Name.ToLower().Contains(s));
            label1.Content = list.Length;
            if (list.Length > 5000)
            {
                return;
            }
            foreach (Item item in list)
            {
                ListItems.Add(item);
            }
        }
    }
}
