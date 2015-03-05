using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ChangeLetter {
    internal partial class MainForm : Form {
        public MainForm(string letter) {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;

            this.Letter = letter;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.Escape: this.Close(); return true;
                default: return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private readonly string Letter;


        private void Form_Load(object sender, EventArgs e) {
            var letter = string.IsNullOrEmpty(this.Letter) ? null : Volume.GetFromLetter(this.Letter);
            UpdateAll(letter);
        }


        private void btnRemove_Click(object sender, EventArgs e) {
            var volume = cmbVolumes.SelectedItem as Volume;
            if (volume != null) {
                try {
                    Executor.RemoveLetter(volume);
                } catch (NotSupportedException ex) {
                    Medo.MessageBox.ShowError(this, ex.Message);
                }
            }
        }

        private void btnHide_Click(object sender, EventArgs e) {
            if (Medo.MessageBox.ShowQuestion(this, "Hiding drive letter will require Windows Explorer restart. Do you wish to proceed?", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes) { return; }

            btnHide.Visible = false;
            try {
                var volume = cmbVolumes.SelectedItem as Volume;
                Executor.Hide(volume.DriveLetter2);
                btnShow.Visible = true;
            } catch (NotSupportedException ex) {
                Medo.MessageBox.ShowError(this, ex.Message);
            }

            ExplorerDrives.RestartExplorer();
        }

        private void btnShow_Click(object sender, EventArgs e) {
            if (Medo.MessageBox.ShowQuestion(this, "Restoring visibility of a drive letter will require Windows Explorer restart. Do you wish to proceed?", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes) { return; }

            btnShow.Visible = false;
            try {
                var volume = cmbVolumes.SelectedItem as Volume;
                Executor.Show(volume.DriveLetter2);
                btnHide.Visible = true;
            } catch (NotSupportedException ex) {
                Medo.MessageBox.ShowError(this, ex.Message);
            }

            ExplorerDrives.RestartExplorer();
        }

        private void btnChange_Click(object sender, System.EventArgs e) {
            var volume = cmbVolumes.SelectedItem as Volume;
            var letter = cmbLetters.SelectedItem as String;
            if ((volume != null) && (letter != null)) {
                try {
                    Executor.ChangeLetter(volume, letter);
                } catch (NotSupportedException ex) {
                    Medo.MessageBox.ShowError(this, ex.Message);
                }
                this.Close();
            }
        }


        private void cmbVolumes_SelectedValueChanged(object sender, EventArgs e) {
            lblLetters.Enabled = false;
            cmbLetters.Enabled = false;
            btnRemove.Text = "Remove";
            btnRemove.Enabled = false;
            cmbLetters.Items.Clear();

            var volume = cmbVolumes.SelectedItem as Volume;

            UpdateLetterVisibility(volume);

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
            if (!btnChange.Enabled) {
                UpdateLetterVisibility(volume);
            }
        }


        private void UpdateAll(Volume selectedVolume) {
            lblLetters.Enabled = false;
            cmbLetters.Enabled = false;

            foreach (var volume in Volume.GetAllVolumes()) {
                cmbVolumes.Items.Add(volume);
            }
            cmbVolumes.SelectedItem = selectedVolume;
            if (cmbVolumes.SelectedItem == null) { cmbVolumes.Select(); }

            UpdateLetterVisibility(selectedVolume);
        }

        private void UpdateLetterVisibility(Volume volume) {
            string driveLetter = (volume != null) ? volume.DriveLetter2 : null;

            btnHide.Visible = false;
            btnShow.Visible = false;

            var isVisible = ExplorerDrives.IsVisible(driveLetter);
            if (isVisible.HasValue) {
                if (isVisible.Value) {
                    btnHide.Visible = true;
                } else {
                    btnShow.Visible = true;
                }
            }
        }

    }
}
