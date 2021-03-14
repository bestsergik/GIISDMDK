using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using WpfAnimatedGif;
namespace CallLogGIISDMDK.Views.FillAppeal
{
    /// <summary>
    /// Логика взаимодействия для AddDataToAppealByID.xaml
    /// </summary>
    public partial class AddDataToAppealByID : Page
    {
        //bool qwefwefwe = false;
        //bool firstEnterMessadgeGrid = false;
        public AddDataToAppealByID()
        {
            InitializeComponent();
        }

        private void AddDataToCurrentAppeal_Click(object sender, RoutedEventArgs e)
        {
            if (AddDataToCurrentAppeal.IsCancel == true)
            {
                this.NavigationService.Navigate(new Menu_page());
            }
               
            else
            {
                    StatusBorder.BorderBrush = System.Windows.Media.Brushes.Green;
                if (ComboBoxMinuteAppel.Text == "")
                    DateBorder.BorderBrush = System.Windows.Media.Brushes.Red;
                else DateBorder.BorderBrush = System.Windows.Media.Brushes.Green;
            }
            MainValid();
        }
        private void CancelAdding_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
         
            Phone.Text = StaticData.Phone;
            ComboBoxMinuteAppel.SelectedIndex = -1;
            CommunicationChannel.SelectedIndex = -1;
            Route.SelectedIndex = -1;
            Email.Text = StaticData.Email;
        }
   
        void MainValid()
        {
            if (StatusBorder != null)
            {
                if (DateBorder.BorderBrush == System.Windows.Media.Brushes.Green && StatusBorder.BorderBrush == System.Windows.Media.Brushes.Green && PhoneEmailBorder.BorderBrush == System.Windows.Media.Brushes.Green)
                    MainBorder.BorderBrush = System.Windows.Media.Brushes.Green;
                else if (MainBorder.BorderBrush == System.Windows.Media.Brushes.Green)
                    MainBorder.BorderBrush = System.Windows.Media.Brushes.Gray;
            }
        }

        private void ComboBoxMinuteAppel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateBorder.BorderBrush = System.Windows.Media.Brushes.Green;
            MainValid();
        }
        private void EditPhone_Click(object sender, RoutedEventArgs e)
        {
            if (EditPhone.IsChecked == true)
            {
                Phone.IsEnabled = true;
            }
            else
            {
                Phone.IsEnabled = false;
            }
        }
        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            Phone.Focus();
            Phone.CaretIndex = Phone.Text.Length;
        }
        private void EditEmail_Click(object sender, RoutedEventArgs e)
        {
            if (EditEmail.IsChecked == true)
            {
                Email.IsEnabled = true;
            }
            else
            {
                Email.IsEnabled = false;
            }
        }
        private void MainBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            MainValid();
        }
     
        private void RootGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = (Grid)sender;
            foreach (UIElement element in grid.Children)
            {
                if (element is Rectangle)
                {
                    if (((Rectangle)element).Fill == Brushes.LightGray)
                    {
                        ((Rectangle)element).Fill = Brushes.OliveDrab;
                        ((Rectangle)element).RadiusX = 12;
                        ((Rectangle)element).RadiusY = 12;
                    }
                    else
                    {
                        ((Rectangle)element).Fill = Brushes.LightGray;
                        ((Rectangle)element).RadiusX = 2;
                        ((Rectangle)element).RadiusY = 2;
                    }

                }
                if (element is Label)
                {
                    if (((Label)element).Foreground == Brushes.Black)
                        ((Label)element).Foreground = Brushes.White;
                    else ((Label)element).Foreground = Brushes.Black;
                }
            }
        }



        private void LoadGif(object sender, RoutedEventArgs e)
        {
            Thread newThread = new Thread(new ParameterizedThreadStart(LoadingGif));
            newThread.Start(sender);
        }


        private void LoadingGif(object sender)
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() =>
{
        var image = new BitmapImage();
        image.BeginInit();
        image.UriSource = new Uri(((Image)sender).Name + @".gif", UriKind.Relative);
        image.EndInit();
        ImageBehavior.SetAnimatedSource((Image)sender, image);
}));
        }

        //    private void Load_Gif(object sender, MouseEventArgs e)
        //    {

        //            Application.Current.Dispatcher.BeginInvoke(
        //DispatcherPriority.Background,
        //new Action(() =>
        //{

        //        var image = new BitmapImage();
        //        image.BeginInit();
        //        image.UriSource = new Uri(((Image)sender).Name + @".gif", UriKind.Relative);
        //        image.EndInit();

        //        ImageBehavior.SetAnimatedSource((Image)sender, image);

        //}));
        //    }

        private void CommunicationChannel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Route.Visibility == Visibility.Visible)
            {
                ShowEmailPhoneGrid();
            }
            if (((ComboBox)sender).SelectedItem != null && ((ComboBox)sender).SelectedItem.ToString() != "")
                Route.Visibility = Visibility.Visible;
        }

        private void Route_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null && ((ComboBox)sender).SelectedItem.ToString() != "")
                ShowEmailPhoneGrid();
            Route.Visibility = Visibility.Visible;
        }

        void ShowEmailPhoneGrid()
        {
            if (CommunicationChannel.SelectedItem.ToString() == "Телефон")
            {
                PhoneGrid.Visibility = Visibility.Visible;
                EmailGrid.Visibility = Visibility.Hidden;
                if (Route.SelectedItem.ToString() == "Входящий")
                {
                    DescriptionTypeConnect.Visibility = Visibility.Visible;
                    DescriptionTypeConnect.Content = "Входящий звонок с:";
                }
                else if (Route.SelectedItem.ToString() == "Исходящий")
                {
                    DescriptionTypeConnect.Visibility = Visibility.Visible;
                    DescriptionTypeConnect.Content = "Исходящий звонок на:";
                }
            }
            else if (CommunicationChannel.SelectedItem.ToString() == "Email")
            {
                PhoneGrid.Visibility = Visibility.Hidden;
                EmailGrid.Visibility = Visibility.Visible;
                if (Route.SelectedItem.ToString() == "Входящий")
                {
                    DescriptionTypeConnect.Visibility = Visibility.Visible;
                    DescriptionTypeConnect.Content = "Входящий Email с:";
                }
                else if (Route.SelectedItem.ToString() == "Исходящий")
                {
                    DescriptionTypeConnect.Visibility = Visibility.Visible;
                    DescriptionTypeConnect.Content = "Исходящий Email на:";
                }
            }
        }

        private void Complete_MouseEnter(object sender, MouseEventArgs e)
        {
            Complete.Visibility = Visibility.Hidden;
            completehover.Visibility = Visibility.Visible;
            if (OffComplete.Visibility == Visibility.Visible)
                OffComplete.Visibility = Visibility.Hidden;
        }

        private void Complete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            inworkenter.Visibility = Visibility.Hidden;
            completehover.Visibility = Visibility.Hidden;
            OffComplete.Visibility = Visibility.Hidden;
            completeenter.Visibility = Visibility.Visible;
            OffInWork.Visibility = Visibility.Visible;
        }



        private void Complete_MouseLeave(object sender, MouseEventArgs e)
        {
           if(completeenter.Visibility != Visibility.Visible)
            {
                completehover.Visibility = Visibility.Hidden;
               
                if (OffComplete.Visibility == Visibility.Hidden && inworkenter.Visibility != Visibility.Visible)
                    Complete.Visibility = Visibility.Visible;
                else
                    OffComplete.Visibility = Visibility.Visible;
            }
        }





        private void InWork_MouseEnter(object sender, MouseEventArgs e)
        {
            InWork.Visibility = Visibility.Hidden;
            inworkhover.Visibility = Visibility.Visible;
            if (OffInWork.Visibility == Visibility.Visible)
            {
                OffInWork.Visibility = Visibility.Hidden;
            }
        }

        private void InWork_MouseDown(object sender, MouseButtonEventArgs e)
        {
            completeenter.Visibility = Visibility.Hidden;
            inworkhover.Visibility = Visibility.Hidden;
            OffInWork.Visibility = Visibility.Hidden;
            inworkenter.Visibility = Visibility.Visible;
            OffComplete.Visibility = Visibility.Visible;
        }

        private void InWork_MouseLeave(object sender, MouseEventArgs e)
        {
            if (inworkenter.Visibility != Visibility.Visible)
            {
                inworkhover.Visibility = Visibility.Hidden;
              
                if (OffInWork.Visibility == Visibility.Hidden && completeenter.Visibility != Visibility.Visible)
                    InWork.Visibility = Visibility.Visible;
                else
                    OffInWork.Visibility = Visibility.Visible;
            }
            if(completeenter.Visibility == Visibility.Visible)
                inworkhover.Visibility = Visibility.Hidden;
           
        }

    }
}

