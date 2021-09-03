using M150_EncryptionProject.View;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;

namespace M150_EncryptionProject.ViewModel
{
    public class EncryptionViewModel : INotifyPropertyChanged
    {
        private EncryptionView _view;
        private FileInfo _fileInfo;
        private string _key = "";

        private DelegateCommand _openFileDialogCommand;
        private DelegateCommand _encryptCommand;
        private DelegateCommand _decryptCommand;

        public EncryptionViewModel(EncryptionView view)
        {
            _view = view;
        }

        public ICommand OpenFileDialogCommand => _openFileDialogCommand ??= new DelegateCommand(OpenFileDialog);
        public ICommand EncryptCommand => _encryptCommand ??= new DelegateCommand(Encrypt);
        public ICommand DecryptCommand => _decryptCommand ??= new DelegateCommand(Decrypt);
        public FileInfo FileInfo
        {
            get => _fileInfo;
            set
            {
                if (value != null && !value.Equals(_fileInfo))
                {
                    _fileInfo = value;
                    OnPropertyChanged();
                    OnPropertyChanged("FilePath");
                }
            }
        }
        public string FilePath => FileInfo?.FullName != null ? FileInfo.FullName : "";
        public string Key
        {
            get => _key;
            set
            {
                if (value != null && !value.Equals(_key))
                {
                    _key = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                if (!File.Exists(openFileDialog.FileName))
                {
                    MessageBox.Show("File \"" + openFileDialog.FileName + "\"does not exist.\nPlease select a file that exists!", "File does not exist", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    FileInfo = new FileInfo(openFileDialog.FileName);
                }
            }
        }

        private void Encrypt()
        {
            if (FilePath.Equals("") || !File.Exists(FilePath))
            {
                MessageBox.Show("Could not read contents of file \"" + FilePath + "\".\nFile does not exist.", "Could not read contents of file.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            string fileContent = FileInfo.OpenText().ReadToEnd();

            if(fileContent == null)
            {
                MessageBox.Show("Could not read contents of file \"" + FilePath + "\".", "Could not read contents of file.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(fileContent == "")
            {
                MessageBox.Show("File \"" + FilePath + "\" is empty.\nEncryption stopped.", "File is empty.", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Key.Equals(""))
            {
                Key = GenerateKey();
                MessageBox.Show("Generated new key \"" + Key + "\" as no key was provided.", "Generated new key.", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //TODO Encryption magic
        }

        private void Decrypt()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() != true)
            {
                return;
            }
            FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);

            if (Key.Equals(""))
            {
                MessageBox.Show("Missing parameter \"key\". Please provide a key to decrypt files.", "Missing parameter \"key\".", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //TODO
        }

        private string GenerateKey()
        {
            //TODO Actually generate key
            return "TEMPKEY";
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
