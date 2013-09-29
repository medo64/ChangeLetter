using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace ChangeLetterExecutor {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
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


                var volumeArg = Medo.Application.Args.Current.GetValue("volume");
                var newLetter = Medo.Application.Args.Current.GetValue("newLetter");

                try {
                    var volume = Volume.GetFromVolumeName(volumeArg);
                    if (volume != null) {
                        if (Medo.Application.Args.Current.ContainsKey("change")) {
                            if (newLetter != null) { volume.ChangeLetter(newLetter); }
                        } else if (Medo.Application.Args.Current.ContainsKey("remove")) {
                            volume.RemoveLetter();
                        }
                    }
                } catch (Exception ex) {
                    Medo.MessageBox.ShowError(null, ex.Message);
                }
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
