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

namespace CallLogGIISDMDK.Views.FillAppeal
{
    /// <summary>
    /// Логика взаимодействия для FillAppeal_page1.xaml
    /// </summary>
    public partial class FillAppeal_page1 : Page
    {
        public FillAppeal_page1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(NextStep.IsCancel == true)
            this.NavigationService.Navigate(new FillAppeal_page2());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void ChangeTextBox(object sender, TextChangedEventArgs e)
        {
            txboxPhoneNumber.Focus();
            txboxPhoneNumber.CaretIndex = txboxPhoneNumber.Text.Length;
        }
    }
}
