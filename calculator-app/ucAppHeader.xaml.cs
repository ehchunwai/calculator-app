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
    /// Interaction logic for ucAppHeader.xaml
    /// </summary>
    public partial class ucAppHeader : UserControl
    {
        public Action<Guid> OnLogoutClick;

        public Guid _currentUserUid = Guid.Empty;

        public ucAppHeader()
        {
            InitializeComponent();
        }

        public void SetGreetings(string name, Guid uid)
        {
            lblGreetings.Content = string.Format("Hello {0}!", name);
            _currentUserUid = uid;
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            if (OnLogoutClick != null)
            {
                OnLogoutClick(_currentUserUid);
            }
        }

        public Guid GetCurrentUserUid()
        {
            return _currentUserUid;
        }
    }
}
