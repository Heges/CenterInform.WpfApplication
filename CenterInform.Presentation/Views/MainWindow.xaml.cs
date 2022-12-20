using CenterInfor.Domain.Models;
using CenterInform.Application.Interfaces;
using CenterInform.Presentation.ViewModels;
using CenterInform.Serializator;
using System.Windows;

namespace CenterInform.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EmployeModelView model;
        public MainWindow(IRepository repository, SerializationManager serializator)
        {
            InitializeComponent();
            model = new EmployeModelView(repository, serializator);
            DataContext = model;
        }

        /// <summary>
        /// к сожалению c WPF не так хорошо знаком чтобы без труда вынести в модельвью решения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var emp = e.AddedItems[0] as Employe;
                if (emp == null)
                {
                    model.SelectedEmploye = null;

                }
            }
        }
    }
}
