using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing;

namespace WinFormsApp
{
    /** The following registry entries are written/cleared automatically during build:
     * HKEY_CLASSES_ROOT\<namespace>.<classname>\..
     * HKEY_CLASSES_ROOT\CLSID\{guid}\...                   */
    [Guid("1DF2592C-39F6-4679-AA20-660440E28CD0")] // MUST be unique for each app
    public partial class PluginControl: UserControl
    {
        public PluginControl()
        {
            InitializeComponent();
        }


        protected override void OnHandleDestroyed(EventArgs e)
        {
        }

        struct RegEntry {
            public string path, value;

            public RegEntry(string _path, string _value) {
                path = _path; value = _value;
            }
        }

        static List<RegEntry> ExtraRegKeys(Type t)
        {
            List<RegEntry> keys = new List<RegEntry>();
            keys.Add(new RegEntry(@"CLSID\{" + t.GUID + @"}\Control", "")); // tag COM component as ActiveX control
            keys.Add(new RegEntry(@"CLSID\{" + t.GUID + @"}\Implemented Categories\{BBCD90B2-D5FD-4EF1-AEDB-24F45E0BCED1}", ""));
            keys.Add(new RegEntry(@"CLSID\{" + t.GUID + @"}\Version", "3.14")); // version
            return keys;
        }

        /** Entry-poing for adding ADDITIONAL registry entries (in addition to "InProcServer32" and "ThreadingModel"). */
        [ComRegisterFunction]
        public static void RegisterClass(Type t)
        {
            // Add additional registry keys
            foreach(RegEntry e in ExtraRegKeys(t)) {
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(e.path);
                if (e.value.Length > 0)
                    key.SetValue(null, e.value);
                key.Close();
            }
#if false
            MessageBox.Show("Registering "+t+" (GUID: "+t.GUID+")"); // debugging aid
#endif
        }

        /** Entry-poing for removing ADDITIONAL registry entries. */
        [ComUnregisterFunction]
        public static void UnregisterClass(Type t)
        {
            foreach (RegEntry e in ExtraRegKeys(t)) {
                Registry.ClassesRoot.DeleteSubKey(e.path);
            }
#if false
            MessageBox.Show("UnRegistering "+t+" (GUID: "+t.GUID+")"); // debugging aid
#endif
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {

        }

        private void buttonImage_Click(object sender, EventArgs e)
        {
        }

        private void buttonGenMeas_Click(object sender, EventArgs e)
        {
        }

        private void buttonLogMeas_Click(object sender, EventArgs e)
        {
        }
    }
}
