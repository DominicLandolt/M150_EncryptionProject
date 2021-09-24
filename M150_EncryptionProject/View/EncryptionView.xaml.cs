using M150_EncryptionProject.ViewModel;
using System.Windows;

namespace M150_EncryptionProject.View
{
    /// <summary>
    /// Interaction logic for EncryptionView.xaml
    /// </summary>
    public partial class EncryptionView : Window
    {
        public EncryptionView()
        {
            DataContext = new EncryptionViewModel(this);
            InitializeComponent();
        }
    }
}
