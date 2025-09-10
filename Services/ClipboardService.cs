using System;
using System.Threading;
using System.Windows.Forms;

namespace AutoShare.Services
{
    public static class ClipboardService
    {
        public static string GetClipboardText()
        {
            string result = string.Empty;
            var staThread = new Thread(() =>
            {
                if (Clipboard.ContainsText())
                    result = Clipboard.GetText();
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return result;
        }

        public static void SetClipboardText(string text)
        {
            var staThread = new Thread(() => Clipboard.SetText(text));
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        public static void ClearClipboard()
        {
            var staThread = new Thread(() => Clipboard.Clear());
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }
    }
}