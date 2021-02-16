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
        Data data = new Data();
        List<string> appeals = new List<string>();
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(StaticData.IsNewAppeal)
            {
                Role.SelectedIndex = -1;
                Status.SelectedIndex = -1;
            }
            foreach (var appeal in data.GetAppeals())
            {
                appeals.Add($"{appeal[0]},{appeal[1]},{appeal[2]}");
            }
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
            string[] currentAppeal = new string[3];
            ComboBox cmb = (ComboBox)sender;
            int counter = 0;

            List<string> currentAppealList = new List<string>();
            bool isMatchAppeal = false;
            if (cmb.SelectedItem != null)
                currentAppeal = cmb.SelectedItem.ToString().Split(',');
            foreach (var appeal in data.GetAppeals())
            {
                for (int i = 0; i < currentAppeal.Length; i++)
                {
                    if (currentAppeal[i] == appeal[i])
                    {
                        currentAppealList = appeal;
                        counter++;
                        if (counter == 3)
                        {
                            isMatchAppeal = true;
                            break;
                        }
                    }
                }
            }
            if (isMatchAppeal)
            {
                FullName.Text = currentAppealList[0];
                Company.Text = currentAppealList[1];
                txboxPhoneNumber.Text = currentAppealList[2];
                INN.Text = currentAppealList[3];
                Sity.Text = currentAppealList[4];
                Role.Text = currentAppealList[5];
                Email.Text = currentAppealList[7];
                OGRN.Text = currentAppealList[8];
               
            }
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


    }
}
