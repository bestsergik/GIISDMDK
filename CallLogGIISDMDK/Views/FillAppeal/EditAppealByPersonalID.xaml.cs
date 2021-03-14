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
    public partial class EditAppealByPersonalID : Page
    {

        bool handlerAttached;
        int counterChoisedTypeAppeal = 0;
        public EditAppealByPersonalID()
        {
            InitializeComponent();
        }

        //bool qwefwefwe = false;
        //bool firstEnterMessadgeGrid = false;
        

        private void AddDataToCurrentAppeal_Click(object sender, RoutedEventArgs e)
        {
            if (AddDataToCurrentAppeal.IsCancel == true)
            {
                this.NavigationService.Navigate(new Menu_page());
                if (handlerAttached)
                {
                    CommunicationChannel.SelectionChanged -= CommunicationChannel_SelectionChanged;
                    Route.SelectionChanged -= Route_SelectionChanged;
                    handlerAttached = true;
                }
            }

            else
            {
                if (completeenter.Visibility == Visibility.Hidden && inworkenter.Visibility == Visibility.Hidden)
                    StatusBorder.BorderBrush = Brushes.Red;
                if (ComboBoxMinuteAppel.Text == "")
                    DateBorder.BorderBrush = Brushes.Red;
                if (PhoneGrid.Visibility == Visibility.Hidden && EmailGrid.Visibility == Visibility.Hidden)
                    PhoneEmailBorder.BorderBrush = Brushes.Red;
                if (TypeAppealBorder.BorderBrush != Brushes.Green)
                    TypeAppealBorder.BorderBrush = Brushes.Red;
                if (StatusBorder.BorderBrush != Brushes.Green || DateBorder.BorderBrush != Brushes.Green || PhoneEmailBorder.BorderBrush != Brushes.Green || TypeAppealBorder.BorderBrush != Brushes.Red || Appeal.Text == "")
                {
                    MainBorder.BorderThickness = new Thickness(2.5);
                    MainBorder.BorderBrush = Brushes.Red;
                }
                if (PhoneGrid.Visibility == Visibility.Hidden && EmailGrid.Visibility == Visibility.Hidden)
                    PhoneEmailBorder.BorderBrush = Brushes.Red;
                else if (PhoneGrid.Visibility == Visibility.Visible && Phone.Text.Length < 15 && IsIrregular.IsChecked == false) PhoneEmailBorder.BorderBrush = Brushes.Red;

                else if (IsIrregular.IsChecked == true && Phone.Text == "") PhoneEmailBorder.BorderBrush = Brushes.Red;

                else if (EmailGrid.Visibility == Visibility.Visible && PromptEmail.Content.ToString() != "") PhoneEmailBorder.BorderBrush = Brushes.Red;
                else PhoneEmailBorder.BorderBrush = Brushes.Green;

            }
            MainValid();
        }


        private void CancelAdding_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Route.Visibility = Visibility.Hidden;
            //PhoneGrid.Visibility = Visibility.Hidden;
            //EmailGrid.Visibility = Visibility.Hidden;
            Phone.Text = StaticData.Phone;
            ComboBoxMinuteAppel.SelectedIndex = -1;
            CommunicationChannel.SelectedIndex = -1;
            Route.SelectedIndex = -1;
            Email.Text = StaticData.Email;
            counterChoisedTypeAppeal = 0;
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
        //private void EditPhone_Click(object sender, RoutedEventArgs e)
        //{
        //    if (EditPhone.IsChecked == true)
        //    {
        //        Phone.IsEnabled = true;
        //    }
        //    else
        //    {
        //        Phone.IsEnabled = false;
        //    }
        //}
        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            Phone.Focus();
            Phone.CaretIndex = Phone.Text.Length;
        }
        //private void EditEmail_Click(object sender, RoutedEventArgs e)
        //{
        //    if (EditEmail.IsChecked == true)
        //    {
        //        Email.IsEnabled = true;
        //    }
        //    else
        //    {
        //        Email.IsEnabled = false;
        //    }
        //}
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
                        counterChoisedTypeAppeal++;
                    }
                    else
                    {
                        ((Rectangle)element).Fill = Brushes.LightGray;
                        ((Rectangle)element).RadiusX = 2;
                        ((Rectangle)element).RadiusY = 2;
                        counterChoisedTypeAppeal--;
                    }
                }
                if (element is Label)
                {
                    if (((Label)element).Foreground == Brushes.Black)
                        ((Label)element).Foreground = Brushes.White;
                    else ((Label)element).Foreground = Brushes.Black;
                }
            }
            if (counterChoisedTypeAppeal > 0) TypeAppealBorder.BorderBrush = Brushes.Green;
            else TypeAppealBorder.BorderBrush = Brushes.Gray;
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
            if (Route != null && Route.Visibility == Visibility.Visible)
            {
                ShowEmailPhoneGrid();
            }
            if (((ComboBox)sender).SelectedItem != null && ((ComboBox)sender).SelectedItem.ToString() != "")
                Route.Visibility = Visibility.Visible;
            if (Route != null && Route.Visibility == Visibility.Visible && (PhoneGrid.Visibility == Visibility.Visible || EmailGrid.Visibility == Visibility.Visible)) DefineColorBorderPhoneEmail();
        }

        private void Route_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null && ((ComboBox)sender).SelectedItem.ToString() != "")
            {
                {
                    Route.Visibility = Visibility.Visible;
                    DefineColorBorderPhoneEmail();
                    ShowEmailPhoneGrid();
                }
            }



        }

        private void DefineColorBorderPhoneEmail()
        {
            if (CommunicationChannel.SelectedItem.ToString() == "Телефон")
            {
                if (Phone.Text != "")
                    PhoneEmailBorder.BorderBrush = Brushes.Green;
                else PhoneEmailBorder.BorderBrush = Brushes.Gray;
            }

            if (CommunicationChannel.SelectedItem.ToString() == "Email")
            {
                if (Email.Text != "")
                    PhoneEmailBorder.BorderBrush = Brushes.Green;
                else PhoneEmailBorder.BorderBrush = Brushes.Gray;
            }
        }

        void ShowEmailPhoneGrid()
        {
            if (Route != null && Route.SelectedItem.ToString() != "")
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
            StatusBorder.BorderBrush = Brushes.Green;
        }

        private void Complete_MouseLeave(object sender, MouseEventArgs e)
        {
            if (completeenter.Visibility != Visibility.Visible)
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
            StatusBorder.BorderBrush = Brushes.Green;
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
            if (completeenter.Visibility == Visibility.Visible)
                inworkhover.Visibility = Visibility.Hidden;
        }

        private void AddDataToCurrentAppeal_MouseLeave(object sender, MouseEventArgs e)
        {
            if (MainBorder.BorderBrush == Brushes.Red)
            {
                MainBorder.BorderThickness = new Thickness(1.5);
                MainBorder.BorderBrush = Brushes.Gray;
            }

        }


        private void CommunicationChannel_DropDownOpened(object sender, EventArgs e)
        {
            if (!handlerAttached)
            {
                CommunicationChannel.SelectionChanged += CommunicationChannel_SelectionChanged;
                Route.SelectionChanged += Route_SelectionChanged;
                handlerAttached = true;
            }
        }
    }
}

