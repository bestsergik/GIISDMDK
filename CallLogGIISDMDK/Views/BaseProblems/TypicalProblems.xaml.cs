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

namespace CallLogGIISDMDK.Views.BaseProblems
{
    /// <summary>
    /// Логика взаимодействия для TypicalProblems.xaml
    /// </summary>
    public partial class TypicalProblems : Page
    {
        public TypicalProblems()
        {
            InitializeComponent();
        }

      
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CommonInformation());
        }
    }
}
