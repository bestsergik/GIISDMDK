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


    class CreatorThreads
    {

        List<Thread> gifThreads = new List<Thread>();

        public void CreateNewThread()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //Thread myThread = new Thread(new ParameterizedThreadStart(Count));
        }
    }


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
                this.NavigationService.Navigate(new Menu_page());
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

        private void Load_Gif(object sender, MouseEventArgs e)
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
    }
}

