using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using CallLogGIISDMDK.ViewModels;
namespace CallLogGIISDMDK.Views
{

    /// <summary>
    /// Логика взаимодействия для ViewAppeals_page.xaml
    /// </summary>
    public partial class ViewAppeals_page : Page
    {
        public ViewAppeals_page()
        {
            InitializeComponent();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void AppealGrid_Loaded(object sender, RoutedEventArgs e)
        {
            int statusIndication = 0;
            bool isIncomingAppeal = false;
            Grid grid = (Grid)sender;
            foreach (UIElement element in grid.Children)
            {
                if (element is TextBlock)
                {
                    if (((TextBlock)element).Text == "Открыто")
                    {
                        statusIndication = 1;
                    }
                    else if (((TextBlock)element).Text == "Срочное")
                    {
                        statusIndication = 2;
                    }
                    if (((TextBlock)element).Text == "Входящее")
                        isIncomingAppeal = true;
                    else if (((TextBlock)element).Text == "Исходящее")
                    {
                        isIncomingAppeal = false;
                    }
                }
                if (element is Ellipse)
                {
                    if (statusIndication == 1)
                        ((Ellipse)element).Fill = new SolidColorBrush(Colors.SteelBlue);
                    else if (statusIndication == 2)
                        ((Ellipse)element).Fill = new SolidColorBrush(Colors.Red);
                }
                if (element is Image)
                {
                    if (isIncomingAppeal)
                        ((Image)element).Source = new BitmapImage(new Uri(@"incoming.png", UriKind.RelativeOrAbsolute));
                    else
                        ((Image)element).Source = new BitmapImage(new Uri(@"outgoing.png", UriKind.RelativeOrAbsolute));
                }
            }
        }
        private void Status_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void DetailStackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            foreach (UIElement element in stackPanel.Children)
            {
                if (element is TextBlock)
                {
                    if (((TextBlock)element).Name == "Status")
                    {
                        Console.WriteLine(((TextBlock)element).Text);
                        if (((TextBlock)element).Text == "Открыто")
                            ((TextBlock)element).Foreground = new SolidColorBrush(Colors.Peru);
                    }


                    if (((TextBlock)element).Name == "Срочно")
                        ((TextBlock)element).Foreground = new SolidColorBrush(Colors.Red);
                }
            }
        }
    }
}

