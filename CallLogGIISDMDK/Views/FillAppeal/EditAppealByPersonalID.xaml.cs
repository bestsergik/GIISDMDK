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
    /// Логика взаимодействия для EditAppealByPersonalID.xaml
    /// </summary>
    public partial class EditAppealByPersonalID : Page
    {
        bool firstEnterMessadgeGrid = false;
        public EditAppealByPersonalID()
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
                if (Wait.Visibility == Visibility.Hidden && Complete.Visibility == Visibility.Hidden)
                    StatusBorder.BorderBrush = System.Windows.Media.Brushes.Red;
                else
                    StatusBorder.BorderBrush = System.Windows.Media.Brushes.Green;
            }
        }
        private void CancelAdding_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
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
            SetDefaultColorLabels();
            InPhoneLabel.Foreground = Brushes.Black;
        }
        void SetDefaultColorLabels()
        {
            InEmainLabel.Foreground = Brushes.LightGray;
            InPhoneLabel.Foreground = Brushes.LightGray;
            OutPhoneLabel.Foreground = Brushes.LightGray;
            OutEmailLabel.Foreground = Brushes.LightGray;
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
            PhoneGrid.Visibility = Visibility.Hidden;
            EmailGrid.Visibility = Visibility.Hidden;
            PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Green;
            SetDefaultColorLabels();
            OutPhoneLabel.Foreground = Brushes.Black;
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
            SetDefaultColorLabels();
            InEmainLabel.Foreground = Brushes.Black;
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
            EmailGrid.Visibility = Visibility.Hidden;
            PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Green;
            SetDefaultColorLabels();
            OutEmailLabel.Foreground = Brushes.Black;
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
        private void Wait_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Complete.Visibility == Visibility.Visible)
            {
                Complete.Visibility = Visibility.Hidden;
            }
            Wait.Visibility = Visibility.Visible;
            WaitHover.Visibility = Visibility.Hidden;
            OffWait.Visibility = Visibility.Hidden;
            OffComplete.Visibility = Visibility.Visible;
            StatusBorder.BorderBrush = System.Windows.Media.Brushes.Green;
        }
        private void Wait_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Wait.Visibility == Visibility.Hidden)
            {
                OffWait.Visibility = Visibility.Hidden;
                WaitHover.Visibility = Visibility.Visible;
            }

        }
        private void Wait_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Wait.Visibility == Visibility.Hidden)
            {
                OffWait.Visibility = Visibility.Visible;
                WaitHover.Visibility = Visibility.Hidden;
            }
        }
        private void Complete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Wait.Visibility == Visibility.Visible)
            {
                Wait.Visibility = Visibility.Hidden;
            }
            Complete.Visibility = Visibility.Visible;
            CompleteHover.Visibility = Visibility.Hidden;
            OffComplete.Visibility = Visibility.Hidden;
            OffWait.Visibility = Visibility.Visible;
            StatusBorder.BorderBrush = System.Windows.Media.Brushes.Green;
        }
        private void Complete_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Complete.Visibility == Visibility.Hidden)
            {
                OffComplete.Visibility = Visibility.Hidden;
                CompleteHover.Visibility = Visibility.Visible;
            }
        }
        private void Complete_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Complete.Visibility == Visibility.Hidden)
            {
                OffComplete.Visibility = Visibility.Visible;
                CompleteHover.Visibility = Visibility.Hidden;
            }
        }
        private void PhoneEmailBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (IncomingEmailEnter.Visibility == Visibility.Visible && Email.Text == "")
            {
                PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else if (IncomingEmailEnter.Visibility == Visibility.Visible)
            {
                PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Green;
            }
            if (IncomingPhoneEnter.Visibility == Visibility.Visible && Phone.Text == "")
            {
                PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else if (IncomingPhoneEnter.Visibility == Visibility.Visible)
            {
                PhoneEmailBorder.BorderBrush = System.Windows.Media.Brushes.Green;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
