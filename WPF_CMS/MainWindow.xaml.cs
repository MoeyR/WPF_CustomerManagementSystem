using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_CMS.Models;
using WPF_CMS.ViewModels;

namespace WPF_CMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainViewModel();

            _viewModel.LoadCustomers();

            DataContext = _viewModel;
        }

        private void ClearSelectedCustomer_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ClearSelectedCustomer();
        }

        private void SaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
                string name = NameTextBox.Text.Trim();
                string idNumber = IdTextBox.Text.Trim();
                string address = AddressTextBox.Text.Trim();

                _viewModel.SaveCustomer(name, idNumber, address);
            } 
            catch(Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
                _viewModel.AddAppointment();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Event for drag and move window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        #region Events in Main Window 
        private void BtnMinimize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }

        #endregion
    }
}