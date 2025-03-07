﻿using CallLogGIISDMDK.WorkWithFiles;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        string _pathToscreenshot1 = "";
        string _pathToscreenshot2 = "";
        string _pathToscreenshot3 = "";
        string _pathToscreenshot4 = "";
        int numberscreenShot = 0;
        private string ZipPathToAppeal = @"Appeals.zip";
        Data data = new Data();
        List<string> appeals = new List<string>();
        string pathToZipAppeals;
        public FillAppeal_page1()
        {
            DefinerCorrectPathToAppeals definerPath = new DefinerCorrectPathToAppeals();
            pathToZipAppeals = definerPath.GetCorrectPathToAppealsZip();
            InitializeComponent();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (OGRN.Text != "" && OGRN.Text.Substring(OGRN.Text.Length - 1, 1) == " ")
                OGRN.Text = OGRN.Text.Substring(0, OGRN.Text.Length - 1);
            if (INN.Text != "" && INN.Text.Substring(INN.Text.Length - 1, 1) == " ")
                INN.Text = INN.Text.Substring(0, INN.Text.Length - 1);
            if (NextStep.IsCancel == true)
            {
                this.NavigationService.Navigate(new AddAppeal_page());
                MainBorder.BorderThickness = new Thickness(1);
                MainBorder.BorderBrush = Brushes.Green;

            }
            else
            {
                MainBorder.BorderThickness = new Thickness(3);
                MainBorder.BorderBrush = Brushes.Red;
            }

            //if (PromptTypeAppel.Content.ToString() != "") 
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
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (StaticData.IsNewAppeal)
            {
                Role.SelectedIndex = -1;
                Status.SelectedIndex = -1;
                TypeAppeal.SelectedIndex = -1;
                ComboBoxMinuteAppel.SelectedIndex = -1;
            }
            if (File.Exists(pathToZipAppeals))
            {
                //foreach (var appeal in data.GetAppeals())
                //{
                //    appeals.Add($"{appeal[0]},{appeal[1]},{appeal[2]},{appeal[3]},{appeal[4]},{appeal[8]},{appeal[9]}");
                //}
                foreach (var appeal in data.GetAppeals())
                {
                    appeals.Add($"{appeal[9]},{appeal[13]},{appeal[14]},{appeal[15]},{appeal[16]},{appeal[17]},{appeal[18]}");
                    //appeals.Add($"{appeal[12]},{appeal[11]},{appeal[7]},{appeal[13]},{appeal[15]},{appeal[14]},{appeal[16]}");
                }
                Searchable.IsEnabled = true;
            }
            numberscreenShot = 0;
            ScreenShotSaveButton.IsEnabled = true;
            ScreenShotSaveButton.Background = Brushes.Peru;
            ScreenShot1.Source = null;
            ScreenShot2.Source = null;
            ScreenShot3.Source = null;
            ScreenShot4.Source = null;
            StaticData.PathToscreenshot1 = "";
            StaticData.PathToscreenshot2 = "";
            StaticData.PathToscreenshot3 = "";
            StaticData.PathToscreenshot4 = "";
        }
        void SetWatermark(string watermark)
        {
        }
        public static T GetChildOfType<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }
        private void PreviewTextInput_EnhanceComboSearch(object sender, TextCompositionEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            cmb.IsDropDownOpen = true;
            if (!string.IsNullOrEmpty(cmb.Text))
            {
                string fullText = cmb.Text.Insert(GetChildOfType<TextBox>(cmb).CaretIndex, e.Text);
                cmb.ItemsSource = appeals.Where(s => s.IndexOf(fullText, StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
            }
            else if (!string.IsNullOrEmpty(e.Text))
            {
                cmb.ItemsSource = appeals.Where(s => s.IndexOf(e.Text, StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
            }
            else
            {
                cmb.ItemsSource = appeals;
            }
        }
        private void Searchable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetDefault();
            string[] currentAppeal = new string[7];
            ComboBox cmb = (ComboBox)sender;
            int counter = 0;
            List<string> currentAppealList = new List<string>();
            bool isMatchAppeal = false;
            if (cmb.SelectedItem != null)
                currentAppeal = cmb.SelectedItem.ToString().Split(',');
            foreach (var appeal in data.GetAppeals())
            {
                counter = 0;
                for (int i = 7; i < 19; i++)
                {
                    if (i == 9)
                    {
                        if (currentAppeal[0] == appeal[i])
                        {
                            counter++;
                        }
                    }
                    else if (i > 12)
                    {
                        if (currentAppeal[i - 12] == appeal[i])
                        {
                            counter++;
                            if (counter == 7)
                            {
                                currentAppealList = appeal;
                                isMatchAppeal = true;
                                break;
                            }
                        }
                    }
                }
                //for (int i = 0; i < 10; i++)
                //{
                //    if (i < 5)
                //    {
                //        if (currentAppeal[i] == appeal[i])
                //        {
                //            counter++;
                //        }
                //    }
                //    else if (i > 7)
                //    {
                //        if (currentAppeal[i-3] == appeal[i])
                //        {
                //            counter++;
                //            if (counter == 7)
                //            {
                //                currentAppealList = appeal;
                //                isMatchAppeal = true;
                //                break;
                //            }
                //        }
                //    }
                //}
            }
            if (isMatchAppeal)
            {
                FullName.Text = currentAppealList[14];
                Company.Text = currentAppealList[13];
                Sity.Text = currentAppealList[9];
                txboxPhoneNumber.Text = currentAppealList[15];
                INN.Text = currentAppealList[17];
                Role.Text = currentAppealList[10];
                Email.Text = currentAppealList[16];
                OGRN.Text = currentAppealList[18];
                //FullName.Text = currentAppealList[0];
                //Company.Text = currentAppealList[1];
                //Sity.Text = currentAppealList[2];
                //txboxPhoneNumber.Text = currentAppealList[3];
                //INN.Text = currentAppealList[4];
                //Role.Text = currentAppealList[5];
                //Email.Text = currentAppealList[8];
                //OGRN.Text = currentAppealList[9];
            }
        }
        private void SetDefault()
        {
            txboxPhoneNumber.Text = "";
            IsIrregular.IsChecked = false;
            // IsIP.IsChecked = false;
            // IsHaveFullName.IsChecked = false;
        }
        private void PreviewKeyUp_EnhanceComboSearch(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                ComboBox cmb = (ComboBox)sender;
                cmb.IsDropDownOpen = true;
                if (!string.IsNullOrEmpty(cmb.Text))
                {
                    cmb.ItemsSource = appeals.Where(s => s.IndexOf(cmb.Text, StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
                }
                else
                {
                    cmb.ItemsSource = appeals;
                }
            }
        }
        private void Pasting_EnhanceComboSearch(object sender, DataObjectPastingEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            cmb.IsDropDownOpen = true;
            string pastedText = (string)e.DataObject.GetData(typeof(string));
            string fullText = cmb.Text.Insert(GetChildOfType<TextBox>(cmb).CaretIndex, pastedText);
            if (!string.IsNullOrEmpty(fullText))
            {
                cmb.ItemsSource = appeals.Where(s => s.IndexOf(fullText, StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
            }
            else
            {
                cmb.ItemsSource = appeals;
            }
        }
        private void txboxPhoneNumber_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsIrregular.IsChecked == false)
            {
                if (txboxPhoneNumber.Text.Length == 10 && txboxPhoneNumber.Text.Substring(0, 1) != "(")
                {
                    txboxPhoneNumber.Text = $"({txboxPhoneNumber.Text.Substring(0, 3)}) {txboxPhoneNumber.Text.Substring(3, 3)} {txboxPhoneNumber.Text.Substring(6, 2)} {txboxPhoneNumber.Text.Substring(8, 2)}";
                }
                else if (txboxPhoneNumber.Text.Length == 11 && txboxPhoneNumber.Text.Substring(0, 1) != "(")
                {
                    txboxPhoneNumber.Text = $"({txboxPhoneNumber.Text.Substring(1, 3)}) {txboxPhoneNumber.Text.Substring(4, 3)} {txboxPhoneNumber.Text.Substring(7, 2)} {txboxPhoneNumber.Text.Substring(9, 2)}";
                }
                else if (txboxPhoneNumber.Text.Length > 18)
                    txboxPhoneNumber.Text = "";
            }
        }
        private void RootGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DownArrwoImageRow.Height == new GridLength(0.1))
            {
                DownArrwoImageRow.Height = new GridLength(28);
                DetailTypeAppealRow.Height = new GridLength(330);
                MainScroll.ScrollToBottom();
            }

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
        private void NextStep_MouseLeave(object sender, MouseEventArgs e)
        {
            if (MainBorder.BorderBrush == System.Windows.Media.Brushes.Red)
                MainBorder.BorderBrush = System.Windows.Media.Brushes.Black;
            MainBorder.BorderThickness = new Thickness(1.5);
        }
        private void Search_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid)
            {
                if (((Grid)sender).RowDefinitions[1].Height == new GridLength(90))
                    ((Grid)sender).RowDefinitions[1].Height = new GridLength(1);
                else ((Grid)sender).RowDefinitions[1].Height = new GridLength(90);
            }

        }
        private void DetailTypeAppealBorderGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }
        private void ScreenShotSaveButton_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog ofdPicture = new OpenFileDialog();
            ofdPicture.Filter =
                "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;
            if (ofdPicture.ShowDialog() == true)
            {
                if (numberscreenShot == 0)
                {
                    ScreenShot1.Source =
                     new BitmapImage(new Uri(ofdPicture.FileName));
                    StaticData.PathToscreenshot1 = ScreenShot1.Source.ToString();
                }

                else if (numberscreenShot == 1)
                {
                    ScreenShot2.Source =
                    new BitmapImage(new Uri(ofdPicture.FileName));
                    StaticData.PathToscreenshot2 = ScreenShot1.Source.ToString();
                }
                else if (numberscreenShot == 2)
                {
                    ScreenShot3.Source =
                    new BitmapImage(new Uri(ofdPicture.FileName));
                    StaticData.PathToscreenshot3 = ScreenShot1.Source.ToString();
                }
                else if (numberscreenShot == 3)
                {
                    ScreenShot4.Source =
                    new BitmapImage(new Uri(ofdPicture.FileName));
                    StaticData.PathToscreenshot4 = ScreenShot1.Source.ToString();
                }
                numberscreenShot++;
                if (numberscreenShot > 3)
                {
                    ScreenShotSaveButton.Background = Brushes.Gray;
                    ScreenShotSaveButton.IsEnabled = false;
                }
            }
        }
    }
}

