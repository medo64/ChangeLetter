using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace ChangeLetter {
    internal static class Executor {

        public static void ChangeLetter(Volume volume, string newLetter) {
            if (IsVhdAttachAvailable()) {
                ExecuteViaVhdAttach(VolumeAction.Change, volume, newLetter);
            } else {
                ExecuteViaExecutor(VolumeAction.Change, volume, newLetter);
            }
        }

        public static void RemoveLetter(Volume volume) {
            if (IsVhdAttachAvailable()) {
                ExecuteViaVhdAttach(VolumeAction.Remove, volume, null);
            } else {
                ExecuteViaExecutor(VolumeAction.Remove, volume, null);
            }
        }


        public static void Hide(string letter) {
            ExecuteViaExecutor(VolumeAction.Hide, null, letter);
        }

        public static void Show(string letter) {
            ExecuteViaExecutor(VolumeAction.Show, null, letter);
        }


        private static bool IsVhdAttachAvailable() {
            using (var service = new ServiceController("VhdAttach")) {
                try {
                    return (service.Status == ServiceControllerStatus.Running);
                } catch (InvalidOperationException) {
                    return false; //VhdAttach service is missing
                }
            }
        }

        private enum VolumeAction {
            Change,
            Remove,
            Hide,
            Show,
        }


        private static void ExecuteViaExecutor(VolumeAction action, Volume volume, string newLetter) {
            var sbArgs = new StringBuilder();
            sbArgs.Append("/" + action.ToString());
            if (volume != null) {
                sbArgs.AppendFormat(CultureInfo.InvariantCulture, @" /volume:{0}", volume.VolumeName);
            }
            if (!string.IsNullOrEmpty(newLetter)) {
                sbArgs.AppendFormat(CultureInfo.InvariantCulture, @" /letter:{0}", newLetter);
            }

            ExecuteViaExecutor(sbArgs.ToString());
        }

        private static void ExecuteViaVhdAttach(VolumeAction action, Volume volume, string newLetter) {
            switch (action) {
                case VolumeAction.Change:
                    VhdAttachPipeClient.ChangeDriveLetter(volume.VolumeName, newLetter);
                    break;
                case VolumeAction.Remove:
                    VhdAttachPipeClient.ChangeDriveLetter(volume.VolumeName, "");
                    break;
            }
        }

        private static void ExecuteViaExecutor(string arguments) {
            var fileThis = new FileInfo(Application.ExecutablePath);
            var fileExe = new FileInfo(Path.Combine(fileThis.DirectoryName, "ChangeLetterExecutor.exe"));
            if (!fileExe.Exists) { throw new NotSupportedException("Cannot find ChangeLetterExecutor.exe!"); }

            var exe = new ProcessStartInfo(fileExe.FullName, arguments);
            Process.Start(exe);
        }

    }
}
