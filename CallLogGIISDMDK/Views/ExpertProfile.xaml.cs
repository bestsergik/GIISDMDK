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
using CallLogGIISDMDK.WorkWithFiles;

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
            
        
        }

        private void FileReader_onReadingComplete()
        {
            UserStatus.Text = StaticData.UserStatus;
            UserLvl.Text = StaticData.UserLvl.ToString();
            CountCompleteAppeal.Text = StaticData.UserTopLvl.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Authorization_page());

        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
       
    }
}
