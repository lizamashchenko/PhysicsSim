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

namespace PhysicsSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // обробка події - натискання на кнопку "Оптика" (відкриваємо нове вікно)
        private void OpticsButton_OnClick(object sender, RoutedEventArgs e)
        {
            Window optic = new OpticsWindow();
            optic.Show();
        }
        
        // обробка події - натискання на кнопку "Кінетика" (відкриваємо нове вікно)
        private void KinematicsButton_OnClick(object sender, RoutedEventArgs e)
        {
            Window kinematic = new KinematicsWindow();
            kinematic.Show();
        }
    }
}