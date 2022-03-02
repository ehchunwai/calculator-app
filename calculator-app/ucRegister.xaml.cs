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
    /// Interaction logic for ucRegister.xaml
    /// </summary>
    public partial class ucRegister : UserControl
    {
        public Action<string, string> OnRegisterClick;

        public ucRegister()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (OnRegisterClick != null)
            {
                OnRegisterClick(txtBoxID.Text, txtBoxName.Text);
            }
        }

        public void Clear()
        {
            txtBoxID.Text = "";
            txtBoxName.Text = "";
        }
    }
}
