using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace ChangeLetter {
    static class App {


        [STAThread]
        static void Main() {
            bool createdNew;
            var mutexSecurity = new MutexSecurity();
            mutexSecurity.AddAccessRule(new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow));
            using (var setupMutex = new Mutex(false, @"Global\JosipMedved_ChangeLetter", out createdNew, mutexSecurity)) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Medo.Application.UnhandledCatch.ThreadException += new EventHandler<ThreadExceptionEventArgs>(UnhandledCatch_ThreadException);
                Medo.Application.UnhandledCatch.Attach();


                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var letter = Medo.Application.Args.Current.GetValue("");

                Application.Run(new MainForm(letter));
            }
        }


        private static void UnhandledCatch_ThreadException(object sender, ThreadExceptionEventArgs e) {
#if !DEBUG
            Medo.Diagnostics.ErrorReport.ShowDialog(null, e.Exception, new Uri("http://jmedved.com/feedback/"));
#else
            throw e.Exception;
#endif
        }

    }
}
