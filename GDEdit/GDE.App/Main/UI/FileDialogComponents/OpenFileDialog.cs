﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GDE.App.Main.UI.FileDialogComponents
{
    public class OpenFileDialog : FileDialog
    {
        protected override string FileDialogAction => "Open";

        public OpenFileDialog() : base() { }
        public OpenFileDialog(string defaultDirectory = null) : base(defaultDirectory) { }
    }
}
