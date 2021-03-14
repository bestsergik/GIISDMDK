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
using CallLogGIISDMDK.Views;
using CallLogGIISDMDK.ViewModels;
using System.Windows.Threading;
using System.Globalization;


namespace CallLogGIISDMDK
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new MainWindow_VM();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            //MainFrame.NavigationService.Navigate(new Views.FillAppeal.FillAppeal_page1());

            //MainFrame.Navigate(new Authorization_page());
              MainFrame.NavigationService.Navigate(new Menu_page());
            //MainFrame.NavigationService.Navigate(new Views.FillAppeal.AddDataToAppealByID());
        }


        void Timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now;
            lblDate.Content = DateTime.Now.ToString("dddd, d MMMM yyyy г.", CultureInfo.GetCultureInfo("ru-ru"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Menu_page());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new ExpertProfile());
        }
    }
}
