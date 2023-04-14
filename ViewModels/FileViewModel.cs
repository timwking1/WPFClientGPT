using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using ClientGPT.Models;

namespace ClientGPT.ViewModels
{
    public class FileViewModel
    {
        public TextModel Text { get; private set; }
        public ICommand NewCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand QuitCommand { get; }
        public FileViewModel(TextModel input, TextModel output)
        {
            //Text = input + output;
            TextModel combined = new TextModel();
            combined.Text = input.Text + " " + output.Text;
            Text = combined;
            NewCommand = new RelayCommand(NewFile);
            SaveCommand = new RelayCommand(SaveFile);
            SaveAsCommand = new RelayCommand(SaveFileAs);
            QuitCommand = new RelayCommand(Quit);
        }
        public void NewFile()
        {
            Text.FileName = string.Empty;
            Text.FilePath = string.Empty;
            Text.Text = string.Empty;
        }
        private void SaveFile()
        {
            File.WriteAllText(Text.FilePath, Text.Text);
        }
        private void SaveFileAs()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                DockFile(saveFileDialog);
                File.WriteAllText(saveFileDialog.FileName, Text.Text);
            }
        }

        private void DockFile<T>(T dialog) where T : FileDialog
        {
            Text.FilePath = dialog.FileName;
            Text.FileName = dialog.SafeFileName;
        }

        private void Quit()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
