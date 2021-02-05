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
    /// Логика взаимодействия для ExpertProfile.xaml
    /// </summary>
    public partial class ExpertProfile : Page
    {
        public ExpertProfile()
        {
            InitializeComponent();

            UserName.Text = StaticData.User;
            UserStatus.Text = StaticData.User;
            UserLvl.Text = StaticData.UserLvl.ToString();
            CountCompleteAppeal.Text = StaticData.UserLvl.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Authorization_page());

        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserStatus.Text = StaticData.UserStatus;
        }
    }
}
