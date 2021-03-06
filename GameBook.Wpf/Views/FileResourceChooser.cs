﻿using GameBook.Wpf.ViewModels;
using Microsoft.Win32;

namespace GameBook.Wpf.Views
{
    public class FileResourceChooser : IChooseResource
    {
        public string ResourceIdentifier
        {
            get
            {
                OpenFileDialog dlg = new OpenFileDialog();
                string filePath = string.Empty;
                if (dlg.ShowDialog() == true)
                {
                    filePath = dlg.FileName;
                }
                return filePath;
            }
        }
    }
}