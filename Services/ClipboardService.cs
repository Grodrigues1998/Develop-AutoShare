using System;
using System.Threading;
using System.Windows.Forms;

namespace AutoShare.Services
{
    public class ClipboardService
    {
        public string GetClipboardText()
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

        public void SetClipboardText(string text)
        {
            var staThread = new Thread(() => Clipboard.SetText(text));
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        public void ClearClipboard()
        {
            var staThread = new Thread(() => Clipboard.Clear());
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }
    }
}