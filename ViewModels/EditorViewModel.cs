using ClientGPT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using OpenAI_API;
using OpenAI_API.Moderation;
using OpenAI_API.Chat;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

namespace ClientGPT.ViewModels
{
    public class EditorViewModel
    {
        public TextModel Input { get; set; }
        public TextModel Prompt { get; set; }
        public TextModel Output { get; set; }
        public TextModel APIkey { get; set; }
        public ICommand InputCommand { get; }
        public ICommand PasteAPIkey { get; }

        OpenAIAPI currentAPI = new OpenAIAPI();
        
        Conversation Chat;
        public EditorViewModel(TextModel input, TextModel prompt, TextModel output, TextModel api)
        {
            Input = input;
            Prompt = prompt;
            Output = output;
            APIkey = api;
            InputCommand = new RelayCommand(ProcessInput);
            PasteAPIkey = new RelayCommand(PasteFromClipboard);
        }

        public void PasteFromClipboard()
        {
            APIkey.Text = Clipboard.GetText();
        }

        public async void ProcessInput()
        {
            currentAPI = new OpenAIAPI(APIkey.Text);
            Prompt.Text = "You: " + Input.Text;
            Chat = currentAPI.Chat.CreateConversation();
            Chat.AppendUserInput(Input.Text);
            Output.Text = "Generating response...";
            var response = await CreateChatAsync();
            Output.Text = "GPT: " + response;
        }

        public async Task<string> CreateChatAsync()
        {
            try
            {
                var result = await Chat.GetResponseFromChatbot();
                return result;
            }
            catch
            {
                MessageBox.Show("Invalid API key.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "Invalid API key, try again.";
            }
        }
    }
}