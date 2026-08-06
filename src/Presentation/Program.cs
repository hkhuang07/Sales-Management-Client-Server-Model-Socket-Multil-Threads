using ElectronicsStore.Client;
using ElectronicsStore.Presentation;
using System.Configuration;

namespace Presentation
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += (sender, e) =>
            {
                string detail = e.Exception.InnerException != null ? $"\n\nInner: {e.Exception.InnerException.Message}" : "";
                MessageBox.Show($"UI Error: {e.Exception.Message}{detail}\n\nStack Trace:\n{e.Exception.StackTrace}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    string detail = ex.InnerException != null ? $"\n\nInner: {ex.InnerException.Message}" : "";
                    MessageBox.Show($"Fatal Error: {ex.Message}{detail}\n\nStack Trace:\n{ex.StackTrace}", "Application Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            ApplicationConfiguration.Initialize();
            Application.Run(new frmMain());
        }
    }
}   