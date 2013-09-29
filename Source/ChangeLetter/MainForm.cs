using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
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
                Execute("remove", volume);
                this.Close();
            }
        }

        private void btnChange_Click(object sender, System.EventArgs e) {
            var volume = cmbVolumes.SelectedItem as Volume;
            var letter = cmbLetters.SelectedItem as String;
            if ((volume != null) && (letter != null)) {
                Execute("change", volume, letter);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e) {
            this.Close();
        }


        private void cmbVolumes_SelectedValueChanged(object sender, EventArgs e) {
            var volume = cmbVolumes.SelectedItem as Volume;

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

            cmbLetters.Items.Clear();
            foreach (var drive in drives) {
                cmbLetters.Items.Add(drive);
            }
            cmbLetters.SelectedItem = currLetter;

            if (string.IsNullOrEmpty(volume.DriveLetter2)) {
                btnRemove.Text = "Remove";
                btnRemove.Enabled = false;
            } else {
                btnRemove.Text = "Remove " + volume.DriveLetter2;
                btnRemove.Enabled = true;
            }
        }

        private void cmbLetters_SelectedValueChanged(object sender, EventArgs e) {
            var volume = cmbVolumes.SelectedItem as Volume;
            var letter = cmbLetters.SelectedItem as String;
            btnChange.Enabled = !string.IsNullOrEmpty(letter) && !letter.Equals(volume.DriveLetter2);
        }


        private void UpdateAll(Volume selectedVolume) {
            foreach (var volume in Volume.GetAllVolumes()) {
                cmbVolumes.Items.Add(volume);
            }
            cmbVolumes.SelectedItem = selectedVolume;
        }


        private void Execute(string action, Volume volume, string newLetter = null) {
            var fileThis = new FileInfo(Application.ExecutablePath);
            var fileExe = new FileInfo(Path.Combine(fileThis.DirectoryName, "ChangeLetterExecutor.exe"));
            if (!fileExe.Exists) {
                Medo.MessageBox.ShowError(this, "Cannot find ChangeLetterExecutor.exe!");
                return;
            }

            var sbArgs = new StringBuilder();
            sbArgs.Append("/" + action);
            sbArgs.AppendFormat(CultureInfo.InvariantCulture, @" /volume:{0}", volume.VolumeName);
            if (!string.IsNullOrEmpty(newLetter)) {
                sbArgs.AppendFormat(CultureInfo.InvariantCulture, @" /newletter:{0}", newLetter);
            }

            var exe = new ProcessStartInfo(fileExe.FullName, sbArgs.ToString());
            Process.Start(exe);
        }

    }
}
