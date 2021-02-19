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

namespace CallLogGIISDMDK.Views
{
    /// <summary>
    /// Логика взаимодействия для Authorization_page.xaml
    /// </summary>
    public partial class Authorization_page : Page
    {
        public Authorization_page()
        {
            InitializeComponent();
        }

        private void Registration_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Registration_btn.IsCancel == true)
            {
                this.NavigationService.Navigate(new Menu_page());
                StaticData.IsLoggin = true;
            }
        }

        private void Entry_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Entry_btn.IsCancel == true)
                this.NavigationService.Navigate(new Menu_page());
        }
         
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(StaticData.IsLoggin == true)
            Registration.Visibility = Visibility.Hidden;
            StaticData.User = "";
            entryLoginUserInput.Text = "";
            entryPasswordUserInput.Text = "";
        }

    

        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Entry_btn.Focus();
        }
    }
}
