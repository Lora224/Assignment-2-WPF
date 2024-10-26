using Assignment_2_WPF.ViewModels;
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
    /// Interaction logic for detailActivityWindow.xaml
    /// </summary>
    public partial class detailActivityView : Window
    {
        private readonly ActivityViewModel _viewModel;
        public detailActivityView(ActivityViewModel viewModel)
        {
            InitializeComponent();
            if (viewModel == null)
            {
                _viewModel = new ActivityViewModel();
            }
            else
            {
                _viewModel = viewModel;
            }

            DataContext = _viewModel;
        }
    }
}
