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
        bool qwefwefwe = false;
        bool firstEnterMessadgeGrid = false;
        public AddDataToAppealByID()
        {
            InitializeComponent();
        }
        //private void IsEmail_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (IsEmail.IsChecked == true)
        //    {
        //        txboxPhoneNumber.Text = "Обращение по почте";
        //    }
        //}
        //private void ChangeTextBox(object sender, TextChangedEventArgs e)
        //{
        //    txboxPhoneNumber.Focus();
        //    txboxPhoneNumber.CaretIndex = txboxPhoneNumber.Text.Length;
        //}
        //private void txboxPhoneNumber_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        //{
        //    if (IsIrregular.IsChecked == false)
        //    {
        //        if (txboxPhoneNumber.Text.Length == 10 && txboxPhoneNumber.Text.Substring(0, 1) != "(")
        //        {
        //            txboxPhoneNumber.Text = $"({txboxPhoneNumber.Text.Substring(0, 3)}) {txboxPhoneNumber.Text.Substring(3, 3)} {txboxPhoneNumber.Text.Substring(6, 2)} {txboxPhoneNumber.Text.Substring(8, 2)}";
        //        }
        //        else if (txboxPhoneNumber.Text.Length == 11 && txboxPhoneNumber.Text.Substring(0, 1) != "(")
        //        {
        //            txboxPhoneNumber.Text = $"({txboxPhoneNumber.Text.Substring(1, 3)}) {txboxPhoneNumber.Text.Substring(4, 3)} {txboxPhoneNumber.Text.Substring(7, 2)} {txboxPhoneNumber.Text.Substring(9, 2)}";
        //        }
        //        else if (txboxPhoneNumber.Text.Length > 18)
        //            txboxPhoneNumber.Text = "";
        //    }
        //}
        private void AddDataToCurrentAppeal_Click(object sender, RoutedEventArgs e)
        {
            if (AddDataToCurrentAppeal.IsCancel == true)
                this.NavigationService.Navigate(new Menu_page());
            else
            {
                if (OutgoingPhoneEnter.Visibility == Visibility.Hidden && OutgoingEmailEnter.Visibility == Visibility.Hidden && IncomingEmailEnter.Visibility == Visibility.Hidden && IncomingPhoneEnter.Visibility == Visibility.Hidden)
                    PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Red;
                else if (IncomingEmailEnter.Visibility == Visibility.Visible && Email.Text == "")
                {
                    PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Red;
                }
                else if (IncomingPhoneEnter.Visibility == Visibility.Visible && Phone.Text == "")
                    PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Red;
                else PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Green;
                if (InWorkEnter.Visibility == Visibility.Hidden && CompleteEnter.Visibility == Visibility.Hidden)
                    StatusBorder.BorderBrush = System.Windows.Media.Brushes.Red;
                else
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

            //Thread[] myThread = new Thread(new ThreadStart(Adding));
            //Thread[] myThread = new Thread(new ThreadStart(Adding));
            //Thread[] myThread = new Thread(new ThreadStart(Adding));
            //Thread[] myThread = new Thread(new ThreadStart(Adding));
            Phone.Text = StaticData.Phone;
            Email.Text = StaticData.Email;
            // Status.SelectedIndex = -1;
            // TypeAppeal.SelectedIndex = -1;
        }
        private void StartShowGifInWorkHover()
        {
            Application.Current.Dispatcher.BeginInvoke(
     DispatcherPriority.Background,
     new Action(() => {
         var image = new BitmapImage();
         image.BeginInit();
         image.UriSource = new Uri(@"inWorkHover.gif", UriKind.Relative);
         image.EndInit();
         ImageBehavior.SetAnimatedSource(InWorkHoverGif, image);
     }));
        }
        private void StartShowGifInWorkEnter()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() => {
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"inWorkEnter.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(InWorkEnterGif, image);
}));
        }
        private void StartShowGifCompleteEnter()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() => {
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"completeEnter.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(CompleteEnterGif, image);
}));
        }
        private void StartShowGifCompleteHover()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() => {
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"completeHover.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(CompleteHoverGif, image);
}));
        }
        private void IncomingPhoneGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (OutgoingPhoneEnter.Visibility == Visibility.Visible || OutgoingEmailEnter.Visibility == Visibility.Visible || IncomingEmailEnter.Visibility == Visibility.Visible)
            {
                OutgoingPhoneEnter.Visibility = Visibility.Hidden;
                OutgoingEmailEnter.Visibility = Visibility.Hidden;
                IncomingEmailEnter.Visibility = Visibility.Hidden;
            }
            IncomingPhoneHover.Visibility = Visibility.Hidden;
            IncomingPhoneHover.Visibility = Visibility.Hidden;
            IncomingPhoneEnter.Visibility = Visibility.Visible;
            OffOutPhone.Visibility = Visibility.Visible;
            OutgoingPhone.Visibility = Visibility.Hidden;
            OffOutEmail.Visibility = Visibility.Visible;
            OutgoingEmail.Visibility = Visibility.Hidden;
            OffInEmail.Visibility = Visibility.Visible;
            IncomingEmail.Visibility = Visibility.Hidden;
            PhoneGrid.Visibility = Visibility.Visible;
            EmailGrid.Visibility = Visibility.Hidden;
            SetDefaultPhoneEmailColorLabels();
            InPhoneLabel.Foreground = Brushes.Black;
            TypeConnectLabel.Content = "Входящий звонок с:";
            TypeConnectLabel.Visibility = Visibility.Visible;
            MainValid();
        }
        void SetDefaultPhoneEmailColorLabels()
        {
            InEmainLabel.Foreground = Brushes.LightGray;
            InPhoneLabel.Foreground = Brushes.LightGray;
            OutPhoneLabel.Foreground = Brushes.LightGray;
            OutEmailLabel.Foreground = Brushes.LightGray;
        }
        void SetDefaultStatusColorLabels()
        {
            InWorkLabel.Foreground = Brushes.LightGray;
            CompleteLabel.Foreground = Brushes.LightGray;
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
        private void IncomingPhoneGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (OutgoingPhoneEnter.Visibility == Visibility.Visible || OutgoingEmailEnter.Visibility == Visibility.Visible || IncomingEmailEnter.Visibility == Visibility.Visible)
            {
                OffInPhone.Visibility = Visibility.Hidden;
            }
            if (IncomingPhoneEnter.Visibility == Visibility.Hidden)
                IncomingPhoneHover.Visibility = Visibility.Visible;
            IncomingPhone.Visibility = Visibility.Hidden;
        }
        private void IncomingPhoneGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (OutgoingPhoneEnter.Visibility == Visibility.Visible || OutgoingEmailEnter.Visibility == Visibility.Visible || IncomingEmailEnter.Visibility == Visibility.Visible)
            {
                IncomingPhoneHover.Visibility = Visibility.Hidden;
                OffInPhone.Visibility = Visibility.Visible;
            }
            else if (IncomingPhoneEnter.Visibility == Visibility.Hidden)
            {
                IncomingPhoneHover.Visibility = Visibility.Hidden;
                IncomingPhone.Visibility = Visibility.Visible;
            }
        }
        private void OutgoingPhone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IncomingPhoneEnter.Visibility == Visibility.Visible || OutgoingEmailEnter.Visibility == Visibility.Visible || IncomingEmailEnter.Visibility == Visibility.Visible)
            {
                IncomingPhoneEnter.Visibility = Visibility.Hidden;
                OutgoingEmailEnter.Visibility = Visibility.Hidden;
                IncomingEmailEnter.Visibility = Visibility.Hidden;
            }
            OutgoingPhone.Visibility = Visibility.Hidden;
            OutgoingPhoneHover.Visibility = Visibility.Hidden;
            OutgoingPhoneEnter.Visibility = Visibility.Visible;
            OffInPhone.Visibility = Visibility.Visible;
            IncomingPhone.Visibility = Visibility.Hidden;
            OffOutEmail.Visibility = Visibility.Visible;
            OutgoingEmail.Visibility = Visibility.Hidden;
            OffInEmail.Visibility = Visibility.Visible;
            IncomingEmail.Visibility = Visibility.Hidden;
            EmailGrid.Visibility = Visibility.Hidden;
            PhoneGrid.Visibility = Visibility.Visible;
            PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Green;
            SetDefaultPhoneEmailColorLabels();
            OutPhoneLabel.Foreground = Brushes.Black;
            TypeConnectLabel.Content = "Исходящий звонок на:";
            TypeConnectLabel.Visibility = Visibility.Visible;
            MainValid();
        }
        private void OutgoingPhone_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IncomingPhoneEnter.Visibility == Visibility.Visible || OutgoingEmailEnter.Visibility == Visibility.Visible || IncomingEmailEnter.Visibility == Visibility.Visible)
            {
                OffOutPhone.Visibility = Visibility.Hidden;
            }
            if (OutgoingPhoneEnter.Visibility == Visibility.Hidden)
                OutgoingPhoneHover.Visibility = Visibility.Visible;
            OutgoingPhone.Visibility = Visibility.Hidden;
        }
        private void OutgoingPhone_MouseLeave(object sender, MouseEventArgs e)
        {
            if (IncomingPhoneEnter.Visibility == Visibility.Visible || OutgoingEmailEnter.Visibility == Visibility.Visible || IncomingEmailEnter.Visibility == Visibility.Visible)
            {
                OutgoingPhoneHover.Visibility = Visibility.Hidden;
                OffOutPhone.Visibility = Visibility.Visible;
            }
            else if (OutgoingPhoneEnter.Visibility == Visibility.Hidden)
            {
                OutgoingPhoneHover.Visibility = Visibility.Hidden;
                OutgoingPhone.Visibility = Visibility.Visible;
            }
        }
        private void IncomingEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IncomingPhoneEnter.Visibility == Visibility.Visible || OutgoingPhoneEnter.Visibility == Visibility.Visible || OutgoingEmailEnter.Visibility == Visibility.Visible)
            {
                IncomingPhoneEnter.Visibility = Visibility.Hidden;
                OutgoingPhoneEnter.Visibility = Visibility.Hidden;
                OutgoingEmailEnter.Visibility = Visibility.Hidden;
            }
            IncomingEmail.Visibility = Visibility.Hidden;
            IncomingEmailHover.Visibility = Visibility.Hidden;
            IncomingEmailEnter.Visibility = Visibility.Visible;
            OffInPhone.Visibility = Visibility.Visible;
            IncomingPhone.Visibility = Visibility.Hidden;
            OffOutPhone.Visibility = Visibility.Visible;
            OutgoingPhone.Visibility = Visibility.Hidden;
            OffOutEmail.Visibility = Visibility.Visible;
            OutgoingEmail.Visibility = Visibility.Hidden;
            EmailGrid.Visibility = Visibility.Visible;
            PhoneGrid.Visibility = Visibility.Hidden;
            SetDefaultPhoneEmailColorLabels();
            InEmainLabel.Foreground = Brushes.Black;
            TypeConnectLabel.Content = "Входящая почта с:";
            TypeConnectLabel.Visibility = Visibility.Visible;
            MainValid();
        }
        private void IncomingEmail_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IncomingPhoneEnter.Visibility == Visibility.Visible || OutgoingPhoneEnter.Visibility == Visibility.Visible || OutgoingEmailEnter.Visibility == Visibility.Visible)
            {
                OffInEmail.Visibility = Visibility.Hidden;
            }
            if (IncomingEmailEnter.Visibility == Visibility.Hidden)
                IncomingEmailHover.Visibility = Visibility.Visible;
            IncomingEmail.Visibility = Visibility.Hidden;
        }
        private void IncomingEmail_MouseLeave(object sender, MouseEventArgs e)
        {
            if (IncomingPhoneEnter.Visibility == Visibility.Visible || OutgoingPhoneEnter.Visibility == Visibility.Visible || OutgoingEmailEnter.Visibility == Visibility.Visible)
            {
                IncomingEmailHover.Visibility = Visibility.Hidden;
                OffInEmail.Visibility = Visibility.Visible;
            }
            else if (IncomingEmailEnter.Visibility == Visibility.Hidden)
            {
                IncomingEmailHover.Visibility = Visibility.Hidden;
                IncomingEmail.Visibility = Visibility.Visible;
            }
        }
        private void OutgoingEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IncomingPhoneEnter.Visibility == Visibility.Visible || OutgoingPhoneEnter.Visibility == Visibility.Visible || IncomingEmailEnter.Visibility == Visibility.Visible)
            {
                IncomingPhoneEnter.Visibility = Visibility.Hidden;
                OutgoingPhoneEnter.Visibility = Visibility.Hidden;
                IncomingEmailEnter.Visibility = Visibility.Hidden;
            }
            OutgoingEmail.Visibility = Visibility.Hidden;
            OutgoingEmailHover.Visibility = Visibility.Hidden;
            OutgoingEmailEnter.Visibility = Visibility.Visible;
            OffInPhone.Visibility = Visibility.Visible;
            IncomingPhone.Visibility = Visibility.Hidden;
            OffOutPhone.Visibility = Visibility.Visible;
            OutgoingPhone.Visibility = Visibility.Hidden;
            OffInEmail.Visibility = Visibility.Visible;
            IncomingEmail.Visibility = Visibility.Hidden;
            PhoneGrid.Visibility = Visibility.Hidden;
            EmailGrid.Visibility = Visibility.Visible;
            PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Green;
            SetDefaultPhoneEmailColorLabels();
            OutEmailLabel.Foreground = Brushes.Black;
            TypeConnectLabel.Content = "Исходящая почта на:";
            TypeConnectLabel.Visibility = Visibility.Visible;
            MainValid();
        }
        private void OutgoingEmail_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IncomingPhoneEnter.Visibility == Visibility.Visible || OutgoingPhoneEnter.Visibility == Visibility.Visible || IncomingEmailEnter.Visibility == Visibility.Visible)
            {
                OffOutEmail.Visibility = Visibility.Hidden;
            }
            if (OutgoingEmailEnter.Visibility == Visibility.Hidden)
                OutgoingEmailHover.Visibility = Visibility.Visible;
            OutgoingEmail.Visibility = Visibility.Hidden;
        }
        private void OutgoingEmail_MouseLeave(object sender, MouseEventArgs e)
        {
            if (IncomingPhoneEnter.Visibility == Visibility.Visible || OutgoingPhoneEnter.Visibility == Visibility.Visible || IncomingEmailEnter.Visibility == Visibility.Visible)
            {
                OutgoingEmailHover.Visibility = Visibility.Hidden;
                OffOutEmail.Visibility = Visibility.Visible;
            }
            else if (IncomingEmailEnter.Visibility == Visibility.Hidden)
            {
                OutgoingEmailHover.Visibility = Visibility.Hidden;
                OutgoingEmail.Visibility = Visibility.Visible;
            }
        }
        private void PhoneEmailBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!firstEnterMessadgeGrid)
            {
                OffInEmail.Visibility = Visibility.Hidden;
                OffOutEmail.Visibility = Visibility.Hidden;
                OffInPhone.Visibility = Visibility.Hidden;
                OffOutPhone.Visibility = Visibility.Hidden;
                IncomingPhone.Visibility = Visibility.Visible;
                IncomingEmail.Visibility = Visibility.Visible;
                OutgoingEmail.Visibility = Visibility.Visible;
                OutgoingPhone.Visibility = Visibility.Visible;
            }
            firstEnterMessadgeGrid = true;
        }
        private void Complete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (InWorkEnter.Visibility == Visibility.Visible)
            {
                InWorkEnter.Visibility = Visibility.Hidden;
            }
            OffInWork.Visibility = Visibility.Visible;
            CompleteEnter.Visibility = Visibility.Visible;
            CompleteHover.Visibility = Visibility.Hidden;
            Complete.Visibility = Visibility.Hidden;
            StatusBorder.BorderBrush = System.Windows.Media.Brushes.Green;
            SetDefaultStatusColorLabels();
            CompleteLabel.Foreground = Brushes.Black;
            MainValid();
        }
        private void Complete_MouseEnter(object sender, MouseEventArgs e)
        {

            if (CompleteEnter.Visibility == Visibility.Hidden)
                CompleteHover.Visibility = Visibility.Visible;
            if (OffComplete.Visibility == Visibility.Visible)
            {
                CompleteHover.Visibility = Visibility.Visible;
                OffComplete.Visibility = Visibility.Hidden;
            }
        }
        private void Complete_MouseLeave(object sender, MouseEventArgs e)
        {
            CompleteHover.Visibility = Visibility.Hidden;
            if (InWorkEnter.Visibility == Visibility.Visible)
                OffComplete.Visibility = Visibility.Visible;
        }
        private void InWork_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CompleteEnter.Visibility == Visibility.Visible)
            {
                Complete.Visibility = Visibility.Hidden;
            }
            OffComplete.Visibility = Visibility.Visible;
            InWorkEnter.Visibility = Visibility.Visible;
            InWorkHover.Visibility = Visibility.Hidden;
            InWork.Visibility = Visibility.Hidden;
            StatusBorder.BorderBrush = System.Windows.Media.Brushes.Green;
            SetDefaultStatusColorLabels();
            InWorkLabel.Foreground = Brushes.Black;
            MainValid();
        }
        private void InWork_MouseEnter(object sender, MouseEventArgs e)
        {
            if (InWorkEnter.Visibility == Visibility.Hidden)
                InWorkHover.Visibility = Visibility.Visible;
            if (OffInWork.Visibility == Visibility.Visible)
            {
                InWorkHover.Visibility = Visibility.Visible;
                OffInWork.Visibility = Visibility.Hidden;
            }
        }
        private void InWork_MouseLeave(object sender, MouseEventArgs e)
        {
            InWorkHover.Visibility = Visibility.Hidden;
            if (CompleteEnter.Visibility == Visibility.Visible && InWorkEnter.Visibility == Visibility.Hidden)
                OffInWork.Visibility = Visibility.Visible;
        }
        private void PhoneEmailBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            MainValid();
            //if (IncomingEmailEnter.Visibility == Visibility.Visible && Email.Text == "")
            //{
            //    PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Red;
            //}
            //else if (IncomingEmailEnter.Visibility == Visibility.Visible)
            //{
            //    PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Green;
            //}
            //if (IncomingPhoneEnter.Visibility == Visibility.Visible && Phone.Text == "")
            //{
            //    PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Red;
            //}
            //else if (IncomingPhoneEnter.Visibility == Visibility.Visible)
            //{
            //    PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Green;
            //}
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
        private void IncomingPhoneHover_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
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
                        //((Rectangle)element).Margin = new Thickness(8);
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
        private void StatusGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Thread ThreadInWorkHover = new Thread(new ThreadStart(StartShowGifInWorkHover));
            ThreadInWorkHover.Start();
            Thread ThreadInWorkEnter = new Thread(new ThreadStart(StartShowGifInWorkEnter));
            ThreadInWorkEnter.Start();
            Thread ThreadCompeleHover = new Thread(new ThreadStart(StartShowGifCompleteHover));
            ThreadCompeleHover.Start();
            Thread ThreadCompeleEnter = new Thread(new ThreadStart(StartShowGifCompleteEnter));
            ThreadCompeleEnter.Start();
        }
        private void PhoneEmailGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Thread ThreadPhone = new Thread(new ThreadStart(StartShowGifPhone));
            ThreadPhone.Start();

            Thread ThreadPhoneEnter = new Thread(new ThreadStart(StartShowGifPhoneEnter));
            ThreadPhoneEnter.Start();

            Thread ThreadPhoneHover = new Thread(new ThreadStart(StartShowGifPhoneHover));
            ThreadPhoneHover.Start();

            Thread ThreadEmail = new Thread(new ThreadStart(StartShowGifEmail));
            ThreadEmail.Start();

            Thread ThreadEmailEnter = new Thread(new ThreadStart(StartShowGifEmailEnter));
            ThreadEmailEnter.Start();

            Thread ThreadEmailHover = new Thread(new ThreadStart(StartShowGifEmailHover));
            ThreadEmailHover.Start();

            Thread ThreadOutgoingPhone = new Thread(new ThreadStart(StartShowGifOutgoingPhone));
            ThreadOutgoingPhone.Start();

            Thread ThreadOutgoingPhoneEnter = new Thread(new ThreadStart(StartShowGifOutgoingPhoneEnter));
            ThreadOutgoingPhoneEnter.Start();

            Thread ThreadOutgoingPhoneHover = new Thread(new ThreadStart(StartShowGifOutgoingPhoneHover));
            ThreadOutgoingPhoneHover.Start();

            Thread ThreadOutGoingEmail = new Thread(new ThreadStart(StartShowGifOutGoingEmail));
            ThreadOutGoingEmail.Start();

            Thread ThreadOutGoingEmailEnter = new Thread(new ThreadStart(StartShowGifOutGoingEmailEnter));
            ThreadOutGoingEmailEnter.Start();

            Thread ThreadOutGoingEmailHover = new Thread(new ThreadStart(StartShowGifOutGoingEmailHover));
            ThreadOutGoingEmailHover.Start();
        }
        private void StartShowGifOutGoingEmailHover()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() =>
{
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"outgoingEmailHover.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(OutGoingEmailHoverGif, image);
}));
        }
        private void StartShowGifOutGoingEmailEnter()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() =>
{
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"outgoingEmailEnter.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(OutgoingEmailEnterGif, image);
}));
        }
        private void StartShowGifOutGoingEmail()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() =>
{
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"outgoingEmail.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(OutgoingEmailGif, image);
}));
        }
        private void StartShowGifOutgoingPhoneHover()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() =>
{
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"outgoingPhoneHover.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(OutGoingPhoneHoverGif, image);
}));
        }
        private void StartShowGifOutgoingPhoneEnter()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() =>
{
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"outgoingPhoneEnter.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(OutGoingPhoneEnterGif, image);
}));
        }
        private void StartShowGifOutgoingPhone()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() =>
{
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"outgoingPhone.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(OutGoingPhoneGif, image);
}));
        }
        private void StartShowGifEmailHover()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() =>
{
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"incomingEmailHover.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(IncomingEmailHoverGif, image);
}));
        }
        private void StartShowGifEmailEnter()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() =>
{
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"incomingEmailEnter.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(IncomingEmailEnterGif, image);
}));
        }
        private void StartShowGifEmail()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() =>
{
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"incomingEmail.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(IncomingEmailGif, image);
}));
        }
        private void StartShowGifPhoneHover()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() =>
{
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"incomingPhoneHover.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(IncomingPhoneHoverGif, image);
}));
        }
        private void StartShowGifPhoneEnter()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() =>
{
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"incomingPhoneEnter.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(IncomingPhoneEnterGif, image);
}));
        }
        private void StartShowGifPhone()
        {
            Application.Current.Dispatcher.BeginInvoke(
DispatcherPriority.Background,
new Action(() =>
{
    var image = new BitmapImage();
    image.BeginInit();
    image.UriSource = new Uri(@"incomingPhone.gif", UriKind.Relative);
    image.EndInit();
    ImageBehavior.SetAnimatedSource(IncomingPhoneGif, image);
}));
        }
    }
}

