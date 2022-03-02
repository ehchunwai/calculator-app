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

namespace calculator_app
{
    /// <summary>
    /// Interaction logic for ucCalculatorMemory.xaml
    /// </summary>
    public partial class ucCalculatorMemory : UserControl
    {
        public Action<string> OnItemSelected;
        public ucCalculatorMemory()
        {
            InitializeComponent();
        }

        public void SetList(List<string> valueList)
        {
            lv1.Items.Clear();
            foreach (var item in valueList)
            {
                lv1.Items.Add(item);
            }
        }

        public void ClearList()
        {
            lv1.Items.Clear();
        }

        private void Lv1_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                if (OnItemSelected != null)
                {
                    OnItemSelected((string)item);
                }
            }
        }
    }
}
