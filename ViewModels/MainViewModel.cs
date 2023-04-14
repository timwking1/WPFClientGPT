using ClientGPT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGPT.ViewModels
{
    public class MainViewModel
    {
        private TextModel _myInput { get; set; }
        private TextModel _myPrompt { get; set; }
        private TextModel _myOutput { get; set; }
        private TextModel _myAPI { get; set; }

        public EditorViewModel Editor { get; set; }

        public FileViewModel File { get; set; }

        public AboutViewModel About { get; set; }

        public MainViewModel()
        {
            _myInput = new TextModel();
            _myPrompt = new TextModel();
            _myOutput = new TextModel();
            _myAPI = new TextModel();
            Editor = new EditorViewModel(_myInput, _myPrompt, _myOutput, _myAPI);
            File = new FileViewModel(_myPrompt, _myOutput);
            About = new AboutViewModel();
        }
    }
}
