using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CortanaTime.Resources;
using Windows.Phone.Speech.VoiceCommands;
using Windows.ApplicationModel.Activation;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Windows.Phone.Speech.Synthesis;

namespace CortanaTime
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            
            

            // Sample code to localize the ApplicationBar
            BuildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // if Cortana opened the app
            if (e.NavigationMode == NavigationMode.New)
            {
                string voiceCommandName;

                // if voice commands are installed and available
                if (NavigationContext.QueryString.TryGetValue("voiceCommandName", out voiceCommandName))
                {
                    HandleVoiceCommand(voiceCommandName);
                }
                else
                {
                    // if this is the first run of the app - voice commands unavailable
                    Task.Run(() => InstallVoiceCommands());
                }
            }
            // if we navigated to the app by resume from suspension etc.
            else {}
            responseTextBlock.Text = ResponseGenerator.GenerateTimeString();
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Appends voice commands to Cortana
        /// </summary>
        private async void InstallVoiceCommands()
        {
            const string vcd81path = "ms-appx:///CortanaTime.xml";

            try
            {
                bool using81 = ((Environment.OSVersion.Version.Major >= 8) && (Environment.OSVersion.Version.Minor >= 10));
                Uri vcd81uri = new Uri(using81 ? vcd81path : null);

                await VoiceCommandService.InstallCommandSetsFromFileAsync(vcd81uri);
            }
            catch (Exception vcdEx)
            {
                MessageBox.Show(vcdEx.Message);
            }
        }

        /// <summary>
        /// Takes specific action for a retrieved voice command
        /// </summary>
        /// <param name="command">voice command name triggered to activate the application</param>
        private void HandleVoiceCommand(string command)
        {
            /* Voice Commands can be typed into Cortana; when this happens, "voiceCommandMode" is populated with the
               "textInput" value. In these cases, we'll want to behave a little differently by not speaking back. */
            bool typedVoiceCommand = (NavigationContext.QueryString.ContainsKey("commandMode")
                && (NavigationContext.QueryString["commandMode"] == "text"));

            switch(command)
            {
                case "WhatTimeIsIt":
                    Synthesize(ResponseGenerator.GenerateTimeString());
                    responseTextBlock.Text = ResponseGenerator.GenerateTimeString();
                    break;
                case "WhatDateIsIt":
                    MessageBox.Show("Date!");
                    break;
            }
        }

        private async void Synthesize(string s)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();

            await synth.SpeakTextAsync(s);
        }

        /// <summary>
        /// Builds application bar
        /// </summary>
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // Create a new button and set the text value to the localized string from AppResources.
            //ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
            //appBarButton.Text = AppResources.AppBarButtonText;
            //ApplicationBar.Buttons.Add(appBarButton);
           
            ApplicationBarMenuItem settingsMenuItem = new ApplicationBarMenuItem("settings");
            settingsMenuItem.Click += settingsMenuItem_Click;
            ApplicationBar.MenuItems.Add(settingsMenuItem);
        }

        void settingsMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }
    }
}