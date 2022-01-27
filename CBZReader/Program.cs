using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CBZReader
{
    public static class Utils
    {
        private static readonly Action<Control, ControlStyles, bool> SetStyle =
            (Action<Control, ControlStyles, bool>)Delegate.CreateDelegate(typeof(Action<Control, ControlStyles, bool>),
            typeof(Control).GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(ControlStyles), typeof(bool) }, null));
        public static void DisableSelect(this Control target)
        {
            SetStyle(target, ControlStyles.Selectable, false);
        }
    }
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
