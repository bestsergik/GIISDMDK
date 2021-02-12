﻿using System;
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

        private void Status_Loaded(object sender, RoutedEventArgs e)
        {
            if(Status.Text == "Открыто")
                Status.Foreground = new SolidColorBrush(Colors.Orange);
            else if(Status.Text == "Открыто")
                Status.Foreground = new SolidColorBrush(Colors.Red);


        }
    }
}
