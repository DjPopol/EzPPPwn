using EzPPPwn.Helpers;

namespace EzPPPwn
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Tools.MyConfig.LoadConfig();
            Application.Run(new FormMain());
        }
    }
}