using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace ChangeLetter {
    internal partial class MainForm : Form {
        public MainForm(string letter) {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;

            this.Letter = letter;
        }

        private readonly string Letter;


        private void Form_Load(object sender, EventArgs e) {
            var letter = string.IsNullOrEmpty(this.Letter) ? null : Volume.GetFromLetter(this.Letter);
            UpdateAll(letter);
        }


        private void btnRemove_Click(object sender, EventArgs e) {
            var volume = cmbVolumes.SelectedItem as Volume;
            if (volume != null) {
                Execute(VolumeAction.Remove, volume);
                this.Close();
            }
        }

        private void btnChange_Click(object sender, System.EventArgs e) {
            var volume = cmbVolumes.SelectedItem as Volume;
            var letter = cmbLetters.SelectedItem as String;
            if ((volume != null) && (letter != null)) {
                Execute(VolumeAction.Change, volume, letter);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e) {
            this.Close();
        }


        private void cmbVolumes_SelectedValueChanged(object sender, EventArgs e) {
            lblLetters.Enabled = false;
            cmbLetters.Enabled = false;
            btnRemove.Text = "Remove";
            btnRemove.Enabled = false;
            cmbLetters.Items.Clear();

            var volume = cmbVolumes.SelectedItem as Volume;
            if (volume.DriveLetter2 != null) {
                if (string.Equals(volume.DriveLetter2, Environment.SystemDirectory.Substring(0, 2), StringComparison.OrdinalIgnoreCase)) {
                    return; //this is system drive
                }
            }

            var drives = new List<string>();
            for (char letter = 'A'; letter <= 'Z'; letter++) {
                drives.Add(letter.ToString() + ":");
            }

            var currLetter = volume.DriveLetter2;
            foreach (var drive in DriveInfo.GetDrives()) {
                var driveName = drive.Name.Substring(0, 2);
                if (driveName.Equals(currLetter, StringComparison.OrdinalIgnoreCase) == false) {
                    drives.Remove(driveName);
                }
            }

            foreach (var drive in drives) {
                cmbLetters.Items.Add(drive);
            }
            cmbLetters.SelectedItem = currLetter;

            if (!string.IsNullOrEmpty(volume.DriveLetter2)) {
                btnRemove.Text = "Remove " + volume.DriveLetter2;
                btnRemove.Enabled = true;
            }
            lblLetters.Enabled = (cmbLetters.Items.Count > 0);
            cmbLetters.Enabled = (cmbLetters.Items.Count > 0);
        }

        private void cmbLetters_SelectedValueChanged(object sender, EventArgs e) {
            var volume = cmbVolumes.SelectedItem as Volume;
            var letter = cmbLetters.SelectedItem as String;
            btnChange.Enabled = !string.IsNullOrEmpty(letter) && !letter.Equals(volume.DriveLetter2);
        }


        private void UpdateAll(Volume selectedVolume) {
            lblLetters.Enabled = false;
            cmbLetters.Enabled = false;

            foreach (var volume in Volume.GetAllVolumes()) {
                cmbVolumes.Items.Add(volume);
            }
            cmbVolumes.SelectedItem = selectedVolume;
            if (cmbVolumes.SelectedItem == null) { cmbVolumes.Select(); }
        }


        private void Execute(VolumeAction action, Volume volume, string newLetter = null) {
            if (IsVhdAttachAvailable()) {
                ExecuteViaVhdAttach(action, volume, newLetter);
            } else {
                ExecuteViaExecutor(action, volume, newLetter);
            }
        }

        private bool IsVhdAttachAvailable() {
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
            Remove
        }


        private void ExecuteViaExecutor(VolumeAction action, Volume volume, string newLetter) {
            var fileThis = new FileInfo(Application.ExecutablePath);
            var fileExe = new FileInfo(Path.Combine(fileThis.DirectoryName, "ChangeLetterExecutor.exe"));
            if (!fileExe.Exists) {
                Medo.MessageBox.ShowError(this, "Cannot find ChangeLetterExecutor.exe!");
                return;
            }

            var sbArgs = new StringBuilder();
            sbArgs.Append("/" + action.ToString());
            sbArgs.AppendFormat(CultureInfo.InvariantCulture, @" /volume:{0}", volume.VolumeName);
            if (!string.IsNullOrEmpty(newLetter)) {
                sbArgs.AppendFormat(CultureInfo.InvariantCulture, @" /newletter:{0}", newLetter);
            }

            var exe = new ProcessStartInfo(fileExe.FullName, sbArgs.ToString());
            Process.Start(exe);
        }

        private void ExecuteViaVhdAttach(VolumeAction action, Volume volume, string newLetter) {
            switch (action) {
                case VolumeAction.Change:
                    VhdAttachPipeClient.ChangeDriveLetter(volume.VolumeName, newLetter);
                    break;
                case VolumeAction.Remove:
                    VhdAttachPipeClient.ChangeDriveLetter(volume.VolumeName, "");
                    break;
            }
        }

    }
}
