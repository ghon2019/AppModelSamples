﻿//*********************************************************  
//  
// Copyright (c) Microsoft. All rights reserved.  
// This code is licensed under the MIT License (MIT).  
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF  
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY  
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR  
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.  
//  
//*********************************************************  

using System.IO;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CmdLineFileBrowser
{
    sealed partial class App : Application
    {

        #region Standard stuff

        private static string versionString = string.Empty;
        public static string VersionString
        {
            get
            {
                if (string.IsNullOrEmpty(versionString))
                {
                    PackageVersion packageVersion = Package.Current.Id.Version;
                    versionString = $"{packageVersion.Major}.{packageVersion.Minor}.{packageVersion.Build}.{packageVersion.Revision}";
                }
                return versionString;
            }
        }

        public App()
        {
            InitializeComponent();
        }

        #endregion


        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                rootFrame = new Frame();
                Window.Current.Content = rootFrame;
            }
            if (e.PrelaunchActivated == false)
            {
                //if (rootFrame.Content == null)
                {
                    // The current directory will be the folder containing the EXE.
                    string currentDir = Directory.GetCurrentDirectory();
                    rootFrame.Navigate(typeof(MainPage), currentDir);
                    //rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            switch (args.Kind)
            {
                case ActivationKind.CommandLineLaunch:
                    CommandLineActivatedEventArgs cmdLineArgs = args as CommandLineActivatedEventArgs;
                    CommandLineActivationOperation operation = cmdLineArgs.Operation;
                    string activationPath = operation.CurrentDirectoryPath;

                    Frame rootFrame = Window.Current.Content as Frame;
                    if (rootFrame == null)
                    {
                        rootFrame = new Frame();
                        Window.Current.Content = rootFrame;
                    }
                    rootFrame.Navigate(typeof(MainPage), activationPath);
                    Window.Current.Activate();
                    break;
            }
        }
    }
}
