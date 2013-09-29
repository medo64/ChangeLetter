using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace ChangeLetter {
    internal static class VhdAttachPipeClient {

        public static VhdAttachPipeResponse ChangeDriveLetter(string volumeName, string newDriveLetter) {
            var data = new Dictionary<string, string>();
            data.Add("VolumeName", volumeName);
            data.Add("NewDriveLetter", newDriveLetter);
            return Send("ChangeDriveLetter", data);
        }


        private static Medo.IO.NamedPipe Pipe = new Medo.IO.NamedPipe("JosipMedved-VhdAttach-Commands");

        private static VhdAttachPipeResponse Send(string operation, Dictionary<string, string> data, int timeout = 5000) {
            var packetOut = new Medo.Net.TinyPacket("VhdAttach", operation, data);
            try {
                Pipe.Open();
                Pipe.Write(packetOut.GetBytes());
                var timer = new Stopwatch();
                timer.Start();
                while (timer.ElapsedMilliseconds < timeout) {
                    if (Pipe.HasBytesToRead) { break; }
                    Thread.Sleep(100);
                }
                timer.Stop();
                if (Pipe.HasBytesToRead) {
                    var buffer = Pipe.ReadAvailable();
                    var packetIn = Medo.Net.TinyPacket.Parse(buffer);
                    return new VhdAttachPipeResponse(bool.Parse(packetIn["IsError"]), packetIn["Message"]);
                } else {
                    return new VhdAttachPipeResponse(true, "Cannot contact service.");
                }
            } finally {
                Pipe.Close();
            }
        }

    }
}
