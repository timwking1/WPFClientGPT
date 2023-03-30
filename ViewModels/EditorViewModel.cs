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

namespace ClientGPT.ViewModels
{
    public class EditorViewModel
    {
        public TextModel Input { get; set; }
        public TextModel Prompt { get; set; }
        public TextModel Output { get; set; }

        public ICommand InputCommand { get; }

        OpenAIAPI api = new OpenAIAPI(new APIAuthentication("YOUR_API_KEY_HERE"));

        Conversation chat;

        public EditorViewModel(TextModel input, TextModel prompt, TextModel output)
        {
            Input = input;
            Prompt = prompt;
            Output = output;
            InputCommand = new RelayCommand(ProcessInput);
        }
        public async void ProcessInput()
        {
            Prompt.Text = Input.Text;

            chat = api.Chat.CreateConversation();
            chat.AppendUserInput(Input.Text);

            Output.Text = "Generating response...";

            var response = await CreateChatAsync();
            Output.Text = "GPT: " + response;
        }

        public async Task<string> CreateChatAsync()
        {
            //string response = await chat.GetResponseFromChatbot();
            var result = await chat.GetResponseFromChatbot();
            return result;
        }
    }
}