using M150_EncryptionProject.View;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using System.Linq;
using System;
using System.Collections.Generic;
using Effortless.Net.Encryption;
using System.Text;
using M150_EncryptionProject.Model;

namespace M150_EncryptionProject.ViewModel
{
    public class EncryptionViewModel : INotifyPropertyChanged
    {
        private FileInfo _fileInfo;
        private string _key = "";

        private DelegateCommand _openFileDialogCommand;
        private DelegateCommand _encryptCommand;
        private DelegateCommand _decryptCommand;

        private readonly List<char> _cypher = "QF.@É}-âD;È(n?^vBx>0áy7wl,ÓÜKXUI]ä9p¬§û£#':úëMSW1sà/|8çJöóÀk$zEN+jm\\6Ö=<O4°¨rÛ¢[T_¦%RoPa)ZùtL2\"Áé5fAhegY`i~ëq{u&3CcÚÂHbËÙG* !´ÄüèdV".ToCharArray().ToList();

        public EncryptionViewModel(EncryptionView view)
        {
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
            
            string fileContent = File.ReadAllText(FilePath);

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

            int cypherMoveAmount = Key.GetHashCode();
            if (cypherMoveAmount % _cypher.Count == 0)
            {
                cypherMoveAmount++; //Make sure text is not moved by multiple of length of cypher
            }

            fileContent = CipherHelper.Encrypt(fileContent, Key);

            string resultString = "";
            fileContent.ToCharArray().ToList().ForEach(c =>
            {
                int index = _cypher.FindIndex(c2 => c2 == c);
                if(index == -1)
                {
                    resultString += c;
                }
                else
                {
                    index += cypherMoveAmount;
                    while(index >= _cypher.Count)
                    {
                        index -= _cypher.Count;
                    }
                    while(index < 0)
                    {
                        index += _cypher.Count;
                    }
                    resultString += _cypher[index];
                }
            });

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = FileInfo.Name;
            saveFileDialog.DefaultExt = FileInfo.Extension;
            saveFileDialog.InitialDirectory = FileInfo.DirectoryName;
            saveFileDialog.Filter = "All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() != true)
            {
                return;
            }
            FileInfo saveFileInfo = new FileInfo(saveFileDialog.FileName);

            File.WriteAllText(saveFileInfo.FullName, resultString);
        }

        private void Decrypt()
        {
            if (FilePath.Equals("") || !File.Exists(FilePath))
            {
                MessageBox.Show("Could not read contents of file \"" + FilePath + "\".\nFile does not exist.", "Could not read contents of file.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string fileContent;
            try
            {
                fileContent = File.ReadAllText(FilePath);
            }
            catch
            {
                MessageBox.Show("Invalid character in filecontent", "Invalid character", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (fileContent == null)
            {
                MessageBox.Show("Could not read contents of file \"" + FilePath + "\".", "Could not read contents of file.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (fileContent.Length == 0)
            {
                MessageBox.Show("File \"" + FilePath + "\" is empty.\nDecryption stopped.", "File is empty.", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Key.Equals(""))
            {
                MessageBox.Show("Missing parameter \"key\". Please provide a key to decrypt files.", "Missing parameter \"key\".", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int cypherMoveAmount = Key.GetHashCode();
            if (cypherMoveAmount % _cypher.Count == 0)
            {
                cypherMoveAmount++; //Make sure text is not moved by multiple of length of cypher
            }
            cypherMoveAmount *= -1; //Inverse of encrypt cypherMoveAmount to reverse encryption
            string resultString = "";

            fileContent.ToCharArray().ToList().ForEach(c =>
            {
                int index = _cypher.FindIndex(c2 => c2 == c);
                if (index == -1)
                {
                    resultString += c;
                }
                else
                {
                    index += cypherMoveAmount;
                    while (index >= _cypher.Count)
                    {
                        index -= _cypher.Count;
                    }
                    while (index < 0)
                    {
                        index += _cypher.Count;
                    }
                    resultString += _cypher[index];
                }
            });

            resultString = CipherHelper.Decrypt(resultString, Key);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = FileInfo.Name;
            saveFileDialog.DefaultExt = FileInfo.Extension;
            saveFileDialog.InitialDirectory = FileInfo.DirectoryName;
            saveFileDialog.Filter = "All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() != true)
            {
                return;
            }
            FileInfo saveFileInfo = new FileInfo(saveFileDialog.FileName);

            File.WriteAllText(saveFileInfo.FullName, resultString);
        }

        private string GenerateKey()
        {
            return Guid.NewGuid().ToString();
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
