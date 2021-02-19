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
using CallLogGIISDMDK.Views.FillAppeal;
using CallLogGIISDMDK.ViewModels;
using CallLogGIISDMDK.Views.BaseKnowledge;
using CallLogGIISDMDK.Views.BaseProblems;


namespace CallLogGIISDMDK.Views
{
 


    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class Menu_page : Page
    {
    
        public Menu_page()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FillAppeal_page2());
            //this.NavigationService.Navigate(new FillAppeal_page1());
            //this.NavigationService.Navigate(new FillAppeal_page1_test());

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewAppeals_page());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TypicalProblems());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new KnowledgeBase());
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new BaseProblems.CommonInformation());
        }
    }
}
