using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;


internal static class ExplorerDrives {

    public static bool? IsVisible(string driveLetter) {
        if (string.IsNullOrEmpty(driveLetter)) { return null; }

        var current = GetNoDrivesValue();
        var bitmask = GetDriveBitmask(driveLetter);

        var isHidden = (current & bitmask) == bitmask;
        return !isHidden;
    }

    public static void Show(string driveLetter) {
        if (string.IsNullOrEmpty(driveLetter)) { return; }

        var current = GetNoDrivesValue();
        var bitmask = GetDriveBitmask(driveLetter);

        SetNoDrivesValue(current & ~bitmask);
    }

    public static void Hide(string driveLetter) {
        if (string.IsNullOrEmpty(driveLetter)) { return; }

        var current = GetNoDrivesValue();
        var bitmask = GetDriveBitmask(driveLetter);

        SetNoDrivesValue(current | bitmask);
    }


    private static int GetDriveBitmask(string driveLetter) {
        var bit = (int)char.ToUpperInvariant(driveLetter[0]) - 0x41;
        if ((bit < 0) || (bit > 26)) { return 0; }

        return (int)Math.Pow(2, bit);
    }


    private static int GetNoDrivesValue() {
        using (var regExplorer = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer")) {
            if (regExplorer != null) {
                try {
                    if (regExplorer.GetValueKind("NoDrives") == Microsoft.Win32.RegistryValueKind.DWord) {
                        var noDrives = (int)regExplorer.GetValue("NoDrives");
                        return noDrives;
                    }
                } catch (IOException) { }
            }
        }
        return 0;
    }

    private static void SetNoDrivesValue(int value) {
        value &= 0x3FFFFFF;

        using (var regCurrent = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion", true)) {
            RegistryKey regExplorer = null;
            RegistryKey regPolicies = null;
            try {
                regPolicies = regCurrent.OpenSubKey("Policies", true);
                if (regPolicies == null) {
                    regCurrent.CreateSubKey("Policies");
                    regPolicies = regCurrent.OpenSubKey("Policies", true);
                }

                regExplorer = regPolicies.OpenSubKey("Explorer", true);
                if (regExplorer == null) {
                    regPolicies.CreateSubKey("Explorer");
                    regExplorer = regPolicies.OpenSubKey("Explorer", true);
                }

                try {
                    regExplorer.DeleteValue("NoDrives");
                } catch (ArgumentException) { }
                regExplorer.SetValue("NoDrives", value, RegistryValueKind.DWord);
                regExplorer.Flush();
            } finally {
                if (regExplorer != null) { regExplorer.Close(); }
                if (regPolicies != null) { regPolicies.Close(); }
            }
        }
    }


    internal static void RestartExplorer() {
        var processes = Process.GetProcessesByName("explorer");
        foreach (var process in processes) {
            try {
                process.Kill();
            } catch (InvalidOperationException) {
            } catch (NotSupportedException) {
            } catch (Win32Exception) { }
        }
    }

}
