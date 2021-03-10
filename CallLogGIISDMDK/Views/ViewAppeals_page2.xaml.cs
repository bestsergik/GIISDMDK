using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using CallLogGIISDMDK.ViewModels;
using CallLogGIISDMDK.Views.FillAppeal;
namespace CallLogGIISDMDK.Views
{
    /// <summary>
    /// Логика взаимодействия для ViewAppeals_page.xaml
    /// </summary>
    public partial class ViewAppeals_page2 : Page
    {
        bool isIncomingAppeal = false;
        bool isEmail = false;
        public ViewAppeals_page2()
        {
            InitializeComponent();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Menu_page());
        }
        private void AppealGrid_Loaded(object sender, RoutedEventArgs e)
        {
            int statusIndication = 0;
            //bool isIncomingAppeal = false;
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
                    //if (((TextBlock)element).Text == "Входящее")
                    //    isIncomingAppeal = true;
                    //else if (((TextBlock)element).Text == "Исходящее")
                    //{
                    //    isIncomingAppeal = false;
                    //}
                }
                //if (element is Ellipse)
                //{
                //    if (statusIndication == 1)
                //        ((Ellipse)element).Fill = new SolidColorBrush(Colors.SteelBlue);
                //    else if (statusIndication == 2)
                //        ((Ellipse)element).Fill = new SolidColorBrush(Colors.Red);
                //}
                if (element is Image)
                {
                    if (statusIndication == 1)
                    {
                        if (((Image)element).Name == "StateAppeal")
                            ((Image)element).Visibility = Visibility.Hidden;
                        if (((Image)element).Name == "GIF")
                            ((Image)element).Visibility = Visibility.Visible;
                    }
                    //((Image)element).Source = new BitmapImage(new Uri(@"question.png", UriKind.RelativeOrAbsolute));
                    else if (statusIndication == 2)
                        ((Image)element).Source = new BitmapImage(new Uri(@"attantion.png", UriKind.RelativeOrAbsolute));
                }
            }
        }
        //private void DetailStackPanel_Loaded(object sender, RoutedEventArgs e)
        //{
        //    StackPanel stackPanel = (StackPanel)sender;
        //    foreach (UIElement element in stackPanel.Children)
        //    {
        //        if (element is TextBlock)
        //        {
        //            if (((TextBlock)element).Name == "Status")
        //            {
        //                Console.WriteLine(((TextBlock)element).Text);
        //                if (((TextBlock)element).Text == "Открыто")
        //                    ((TextBlock)element).Foreground = new SolidColorBrush(Colors.Peru);
        //            }
        //            if (((TextBlock)element).Name == "Срочно")
        //                ((TextBlock)element).Foreground = new SolidColorBrush(Colors.Red);
        //        }
        //    }
        //}
        private void AddDataToCurrentAppeal_Click(object sender, RoutedEventArgs e)
        {
            if (AddDataToCurrentAppeal.IsCancel == true)
                this.NavigationService.Navigate(new AddDataToAppealByID());
        }
        private void DetailAppealGrid_Loaded(object sender, RoutedEventArgs e)
        {
            isIncomingAppeal = false;
            isEmail = false;
            Grid grid = (Grid)sender;
            foreach (UIElement element in grid.Children)
            {
                if (element is TextBlock)
                {
                    //if (((TextBlock)element).Text == "Входящее")
                    //    isIncomingAppeal = true;
                    //else if (((TextBlock)element).Text == "Исходящее")
                    //{
                    //    isIncomingAppeal = false;
                    //}
                    if (((TextBlock)element).Name == "Route")
                    {
                        if (((TextBlock)element).Text == "Входящее")
                            isIncomingAppeal = true;
                        else
                        {
                            isIncomingAppeal = false;
                        }
                    }
                    if (((TextBlock)element).Name == "CommunicationChannel")
                    {
                        if (((TextBlock)element).Text.Length > 0)
                        {
                            if (((TextBlock)element).Text == "Email")
                                isEmail = true;
                            else
                            {
                                isEmail = false;
                            }
                        }
                    }
                }
                if (element is Image)
                {
                    if (((Image)element).Name == "TypeArrow")
                    {
                        if (isIncomingAppeal && isEmail)
                            ((Image)element).Source = new BitmapImage(new Uri(@"incomingEmail.png", UriKind.RelativeOrAbsolute));
                        else if (isIncomingAppeal && !isEmail)
                            ((Image)element).Source = new BitmapImage(new Uri(@"incomingCall.png", UriKind.RelativeOrAbsolute));
                        else if (!isIncomingAppeal && isEmail)
                            ((Image)element).Source = new BitmapImage(new Uri(@"outEmail.png", UriKind.RelativeOrAbsolute));
                        else
                            ((Image)element).Source = new BitmapImage(new Uri(@"outCall.png", UriKind.RelativeOrAbsolute));
                    }
                }
            }
        }
        private void Role_LayoutUpdated(object sender, EventArgs e)
        {
            if (Role.Text == "Участник рынка")
            {
                RolePicture.Source = new BitmapImage(new Uri(@"guild.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                RolePicture.Source = new BitmapImage(new Uri(@"MRU FPP.png", UriKind.RelativeOrAbsolute));
            }
        }
        private void EditAppeal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new AddDataToAppealByID());
        }
        private void DetailAppealGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid grid = (Grid)sender;
            foreach (UIElement element in grid.Children)
            {
                if (element is TextBlock)
                {
                    if (((TextBlock)element).Name == "personalId")
                        StaticData.CurrentPersonalId = ((TextBlock)element).Text;
                }
            }
        }
        private void DeleteAppeal_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }
        private void AppealGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (AddDataToCurrentAppeal.Visibility == Visibility.Hidden)
                AddDataToCurrentAppeal.Visibility = Visibility.Visible;
            if (RightPartCenterGrid.Visibility == Visibility.Hidden)
            RightPartCenterGrid.Visibility = Visibility.Visible;
        }
        private void btnNewAppeal_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FillAppeal_page1());
        }
        private void imageTooltip_Loaded(object sender, RoutedEventArgs e)
        {
            Grid grid = (Grid)sender;
            foreach (UIElement element in grid.Children)
            {
                if (element is TextBlock)
                {
                    if (isEmail)
                    {
                        if (((TextBlock)element).Name == "TooltipPhone")
                            ((TextBlock)element).Visibility = Visibility.Hidden;
                        if (((TextBlock)element).Name == "TooltipEmail")
                            ((TextBlock)element).Visibility = Visibility.Visible;
                    }
                    else
                    {
                        if (((TextBlock)element).Name == "TooltipEmail")
                            ((TextBlock)element).Visibility = Visibility.Hidden;
                        if (((TextBlock)element).Name == "TooltipPhone")
                            ((TextBlock)element).Visibility = Visibility.Visible;
                    }
                    //if(((TextBlock)element).Name == "TooltipPhone")
                    //{
                    //    if (isEmail)
                    //        ((TextBlock)element).Visibility = Visibility.Hidden;
                    //    else ((TextBlock)element).Visibility = Visibility.Visible;
                    //}
                    //if(((TextBlock)element).Name == "TooltipEmail")
                    //    if (isEmail)
                    //        ((TextBlock)element).Visibility = Visibility.Visible;
                    //    else ((TextBlock)element).Visibility = Visibility.Hidden;
                }
            }
        }

        private void ToolTipGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Grid grid = (Grid)sender;
            foreach (UIElement element in grid.Children)
            {
                if (element is TextBlock)
                {
                    if (((TextBlock)element).Name == "TooltipPhone")
                    {
                        if (((TextBlock)element).Text == "")
                        {
                            ((Grid)sender).RowDefinitions[0].Height = new GridLength(1);
                        }
                    }
                    if (((TextBlock)element).Name == "TooltipEmail")
                    {
                        if (((TextBlock)element).Text == "")
                        {
                            ((Grid)sender).RowDefinitions[1].Height = new GridLength(1);
                        }
                    }
                }
            }
        }
        private void Search_MouseDown(object sender, MouseButtonEventArgs e)
        {
         
            if (rowSearching.Height == new GridLength(60))
                rowSearching.Height = new GridLength(0.1);
            else rowSearching.Height = new GridLength(60);
            //if (sender is Grid)
            //{
            //    if (((Grid)sender).RowDefinitions[1].Height == new GridLength(60))
            //        ((Grid)sender).RowDefinitions[1].Height = new GridLength(0.1);
            //    else ((Grid)sender).RowDefinitions[1].Height = new GridLength(60);
            //}


        }

        private void Search_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Role1_LayoutUpdated(object sender, EventArgs e)
        {
            if (Role1.Text == "Участник рынка")
            {
                RolePicture1.Source = new BitmapImage(new Uri(@"guild.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                RolePicture1.Source = new BitmapImage(new Uri(@"MRU FPP.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void RolePicture1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid)
            {
                if (((Grid)sender).RowDefinitions[0].Height == new GridLength(55))
                {
                    ((Grid)sender).RowDefinitions[0].Height = new GridLength(270);
                    CardClientBriefly.Visibility = Visibility.Hidden;
                    CardClientDetail.Visibility = Visibility.Visible;
                     
                }
                   
                else
                {
                    ((Grid)sender).RowDefinitions[0].Height = new GridLength(55);
                    CardClientBriefly.Visibility = Visibility.Visible;
                    CardClientDetail.Visibility = Visibility.Hidden;

                }

            }
        }
    }
}
