using Assignment_2_WPF.ViewModels;
using Assignment_2_WPF.Models;
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
using System.Windows.Shapes;

namespace Assignment_2_WPF.Views
{
    /// <summary>
    /// Interaction logic for EditActivityView.xaml
    /// </summary>
    // EditActivityView.xaml.cs
    public partial class EditActivityView : Window
    {
        public EditActivityView(Activity activity)
        {
            InitializeComponent();
            DataContext = new EditActivityViewModel(activity);
        }
    }
}
