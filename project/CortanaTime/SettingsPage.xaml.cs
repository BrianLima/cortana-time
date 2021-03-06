﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CortanaTime
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            useFormalLanguageToggleSwitch.IsChecked = SettingsManager.UseFormalLanguage;
        }

        private void useFormalLanguageToggleSwitch_CheckedChanged(object sender, Telerik.Windows.Controls.CheckedChangedEventArgs e)
        {
            SettingsManager.UseFormalLanguage = useFormalLanguageToggleSwitch.IsChecked;
        }


    }
}