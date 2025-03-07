﻿using AmRoMessageDialog;
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
    /// Логика взаимодействия для AddAppeal_page.xaml
    /// </summary>
    public partial class AddAppeal_page : Page
    {
        public AddAppeal_page()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Menu_page());
            var messageBox = new AmRoMessageBox
            {
                Background = "#284F4F",
                TextColor = "#ffffff",
                IconColor = "#3399ff",
                RippleEffectColor = "#000000",
                ClickEffectColor = "#1F2023",
                ShowMessageWithEffect = true,
                EffectArea = this,
            };
            messageBox.Show("Обращение добавлено успешно!");
        }

        private void PhoneGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if(Phone.Text == null || Phone.Text == "")
                Number7.Visibility = Visibility.Hidden;


            //string firstLetter = Phone.Text.Substring(0, 1);
            //if (firstLetter == "О"  || firstLetter != "(")
            //{
            //    Grid.SetColumn(Phone, 0);
            //    Number7.Visibility = Visibility.Hidden;
            //}
        }
    }
}
