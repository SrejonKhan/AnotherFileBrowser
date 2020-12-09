#if UNITY_STANDALONE_WIN
using Ookii.Dialogs;
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AnotherFileBrowser.Windows
{
    public class FileBrowser
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        public FileBrowser() { }

        /// <summary>
        /// FileDialog for picking a single file
        /// </summary>
        /// <param name="browserProperties">Special Properties of File Dialog</param>
        /// <param name="filepath">User picked path (Callback)</param>
        public void OpenFileBrowser(BrowserProperties browserProperties, Action<string> filepath)
        {
            var ofd = new VistaOpenFileDialog();
            ofd.Multiselect = false;
            ofd.Title = browserProperties.title == null ? "Select a File" : browserProperties.title;
            ofd.InitialDirectory = browserProperties.initialDir == null ? @"C:\" : browserProperties.initialDir;
            ofd.Filter = browserProperties.filter == null ? "All files (*.*)|*.*" : browserProperties.filter;
            ofd.FilterIndex = browserProperties.filterIndex + 1;
            ofd.RestoreDirectory = browserProperties.restoreDirectory;

            if (ofd.ShowDialog(new WindowWrapper(GetActiveWindow())) == DialogResult.OK)
            {
                filepath(ofd.FileName);
            }
        }

        /// <summary>
        /// FileDialog for picking multiple file(s)
        /// </summary>
        /// <param name="browserProperties">Special Properties of File Dialog</param>
        /// <param name="filepath">User picked path(s) (Callback)</param>
        public void OpenMultiSelectFileBrowser(BrowserProperties browserProperties, Action<string[]> filepath)
        {
            var ofd = new VistaOpenFileDialog();
            ofd.Multiselect = true;
            ofd.Title = browserProperties.title == null ? "Select a File" : browserProperties.title;
            ofd.InitialDirectory = browserProperties.initialDir == null ? @"C:\" : browserProperties.initialDir;
            ofd.Filter = browserProperties.filter == null ? "All files (*.*)|*.*" : browserProperties.filter;
            ofd.FilterIndex = browserProperties.filterIndex + 1;
            ofd.RestoreDirectory = browserProperties.restoreDirectory;

            if (ofd.ShowDialog(new WindowWrapper(GetActiveWindow())) == DialogResult.OK)
            {
                filepath(ofd.FileNames);
            }
        }

        /// <summary>
        /// FileDialog for selecting any folder 
        /// </summary>
        /// <param name="browserProperties">Special Properties of File Dialog</param>
        /// <param name="folderpath">User picked path(s) (Callback)</param>
        public void OpenFolderBrowser(BrowserProperties browserProperties, Action<string> folderpath)
        {
            var ofd = new VistaFolderBrowserDialog();
            ofd.Description = browserProperties.title;
            ofd.UseDescriptionForTitle = true;

            if (ofd.ShowDialog(new WindowWrapper(GetActiveWindow())) == DialogResult.OK)
            {
                folderpath(ofd.SelectedPath);
            }
        }

        /// <summary>
        ///  FileDialog for saving any file, returns path with extention for further uses
        /// </summary>
        /// <param name="browserProperties">Special Properties of File Dialog</param>
        /// <param name="defaultFileName">Default File Name</param>
        /// <param name="defaultExt">Default File name extension, adds after default file name.</param>
        /// <param name="savepath">User picked path(s) (Callback)</param>
        public void SaveFileBrowser(BrowserProperties browserProperties, string defaultFileName, string defaultExt, Action<string> savepath)
        {
            var ofd = new VistaSaveFileDialog();
            ofd.FileName = defaultFileName;
            ofd.DefaultExt = defaultExt;
            ofd.CheckPathExists = true;
            ofd.OverwritePrompt = true;
            ofd.Title = browserProperties.title;
            ofd.InitialDirectory = browserProperties.initialDir == null ? @"C:\" : browserProperties.initialDir;
            ofd.Filter = browserProperties.filter;
            ofd.FilterIndex = browserProperties.filterIndex + 1;
            ofd.RestoreDirectory = browserProperties.restoreDirectory;

            if (ofd.ShowDialog(new WindowWrapper(GetActiveWindow())) == DialogResult.OK)
            {
                savepath(ofd.FileName);
            }
        }
    }

    public class BrowserProperties
    {
        public string title; //Title of the Dialog
        public string initialDir; //Where dialog will be opened initially
        public string filter; //aka File Extension for filtering files
        public int filterIndex; //Index of filter, if there is multiple filter. Default is 0. 
        public bool restoreDirectory = true; //Restore to last return directory


        public BrowserProperties() { }
        public BrowserProperties(string title) { this.title = title; }
    }

    //https://stackoverflow.com/a/10296513/14685101
    public class WindowWrapper : IWin32Window
    {
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }

        public IntPtr Handle
        {
            get { return _hwnd; }
        }

        private IntPtr _hwnd;
    }
}
#endif