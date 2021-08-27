using M150_EncryptionProject.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace M150_EncryptionProject.ViewModel
{
    public class EncryptionViewModel : INotifyPropertyChanged
    {
        EncryptionView _view;

        public EncryptionViewModel(EncryptionView view)
        {
            _view = view;
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
