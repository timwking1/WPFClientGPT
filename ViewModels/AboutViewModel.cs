using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClientGPT.ViewModels
{
    public class AboutViewModel
    {
        public ICommand HelpCommand { get; }

        public AboutViewModel()
        {
            HelpCommand = new RelayCommand(DisplayAbout);
        }
        private void DisplayAbout()
        {
            if (System.Reflection.Assembly.GetExecutingAssembly().GetName().Name != null)
            {
                string appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                MessageBox.Show(appName + " version 1.1 by timwking1");
            }
        }
    }
}
