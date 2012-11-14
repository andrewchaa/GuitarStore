using System;
using System.Collections.Generic;
using System.Linq;
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
using NHibernate.GuitarStore.Common;
using NHibernate.GuitarStore.DataAccess;

namespace GuitarStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new NHibernateBase().Initialize("NHibernate.GuitarStore");

        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            PopulateGrid();
            PopulateComboBox();
        }

        private void PopulateGrid()
        {
            var nhi = new NHibernateInventory();
            var list = (List<Inventory>) nhi.ExecuteICriteriaOrderBy("Builder");
            dataGridInventory.ItemsSource = list;

            if (list != null)
            {
                dataGridInventory.Columns[0].Visibility = Visibility.Hidden;
                dataGridInventory.Columns[1].Visibility = Visibility.Hidden;
//                dataGridInventory.Columns[8].Visibility = Visibility.Hidden;
            }
        }

        private void PopulateComboBox()
        {
            var nhb = new NHibernateBase();
            IList<Guitar> guitarTypes = nhb.ExecuteCriteria<Guitar>();
            foreach (var item in guitarTypes)
            {
                var guitar = new Guitar{ Id = item.Id, Type = item.Type};
                cboBoxGuitarTypes.DisplayMemberPath = "Type";
                cboBoxGuitarTypes.SelectedValuePath = "Id";
                cboBoxGuitarTypes.Items.Add(guitar);
            }
        }

        private void GuitarTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                dataGridInventory.ItemsSource = null;
                var guitar = (Guitar) cboBoxGuitarTypes.SelectedItem;
                var guitarType = new Guid(guitar.Id.ToString());

                var nhi = new NHibernateInventory();
                var guitarTypes = (List<Inventory>) nhi.ExecuteICriteria(guitarType);
                dataGridInventory.ItemsSource = guitarTypes;

                if (guitarTypes != null)
                {
                    dataGridInventory.Columns[0].Visibility = Visibility.Hidden;
                    dataGridInventory.Columns[1].Visibility = Visibility.Hidden;
                    
                }

                PopulateComboBox();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
