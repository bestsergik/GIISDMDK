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
using CallLogGIISDMDK.Views.BaseProblems.Instructions;
using System.Diagnostics;

namespace CallLogGIISDMDK.Views.BaseProblems
{
    /// <summary>
    /// Логика взаимодействия для CommonInformation.xaml
    /// </summary>
    public partial class CommonInformation : Page
    {
        int pages = 0;

        public CommonInformation()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Instructions.NavigationService.Navigate(new ConnectToGIISDMDK());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainScroll.ScrollToVerticalOffset(0);
            switch (pages)
            {
                case 0:
                    Instructions.NavigationService.Navigate(new SetupRootSertificate());
                    pages++;
                    break;
                case 1:
                    Instructions.NavigationService.Navigate(new LeaderSertificate());
                    pages++;
                    break;
                case 2:
                    Instructions.NavigationService.Navigate(new ConnectToGIISDMDK());
                    pages = 0;
                    break;
            }
          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Menu_page());
        }
    }
}
