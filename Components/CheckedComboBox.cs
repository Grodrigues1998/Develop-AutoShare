using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoShare.Components
{
    public class CheckedComboBox : ComboBox
    {
        private List<CheckedItem> internalItems = new();
        private ListNativeWindow listNativeWindow;

        /// <summary>
        /// Evento disparado quando a lista de itens marcados muda.
        /// </summary>
        public event EventHandler CheckedItemsChanged;

        public CheckedComboBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void AddItem(string text, bool isChecked = false)
        {
            internalItems.Add(new CheckedItem { Text = text, Checked = isChecked });
            Items.Add(text);
        }

        public void AddItems(IEnumerable<string> items)
        {
            foreach (var item in items)
                AddItem(item);
        }

        /// <summary>
        /// Lista dos textos marcados.
        /// </summary>
        public List<string> CheckedItems => internalItems.Where(i => i.Checked).Select(i => i.Text).ToList();

        /// <summary>
        /// Texto concatenado com todos os itens selecionados.
        /// </summary>
        public string DisplayText => string.Join(", ", CheckedItems);

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // Quando o combo está FECHADO (Index == -1), desenha texto com itens selecionados
            if (e.Index < 0)
            {
                e.DrawBackground();
                using var brush = new SolidBrush(e.ForeColor);
                e.Graphics.DrawString(DisplayText, e.Font, brush, e.Bounds.X + 2, e.Bounds.Y + 2);
                e.DrawFocusRectangle();
                return;
            }

            // Quando aberto, desenha cada item com checkbox
            var item = internalItems[e.Index];
            bool isChecked = item.Checked;

            e.DrawBackground();

            Rectangle checkBoxRect = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 16, 16);
            CheckBoxRenderer.DrawCheckBox(
                e.Graphics,
                checkBoxRect.Location,
                isChecked
                    ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal
                    : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);

            using var itemBrush = new SolidBrush(e.ForeColor);
            e.Graphics.DrawString(item.Text, e.Font, itemBrush, e.Bounds.X + 20, e.Bounds.Y + 2);

            e.DrawFocusRectangle();
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);
            HookListWindow();
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);
            UnhookListWindow();
        }

        /// <summary>
        /// Impede setas ↑ e ↓ de moverem o item selecionado quando fechado.
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!DroppedDown && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                e.Handled = true; // cancela navegação
                return;
            }
            base.OnKeyDown(e);
        }

        private void UpdateText()
        {
            Invalidate(); // força redesenho da área fechada
            CheckedItemsChanged?.Invoke(this, EventArgs.Empty); // dispara evento
        }

        #region PInvoke
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetComboBoxInfo(IntPtr hwndCombo, ref COMBOBOXINFO pcbi);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        private const int LB_ITEMFROMPOINT = 0x01A9;
        private const int WM_LBUTTONDOWN = 0x0201;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT { public int left, top, right, bottom; }

        [StructLayout(LayoutKind.Sequential)]
        private struct COMBOBOXINFO
        {
            public int cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public int stateButton;
            public IntPtr hwndCombo;
            public IntPtr hwndEdit;
            public IntPtr hwndList;
        }
        #endregion

        private void HookListWindow()
        {
            try
            {
                var info = new COMBOBOXINFO();
                info.cbSize = Marshal.SizeOf(info);
                if (GetComboBoxInfo(this.Handle, ref info))
                {
                    if (info.hwndList != IntPtr.Zero)
                    {
                        if (listNativeWindow != null) listNativeWindow.ReleaseHandle();
                        listNativeWindow = new ListNativeWindow(this, info.hwndList);
                        listNativeWindow.AssignHandle(info.hwndList);
                    }
                }
            }
            catch
            {
                // falha silenciosa
            }
        }

        private void UnhookListWindow()
        {
            if (listNativeWindow != null)
            {
                listNativeWindow.ReleaseHandle();
                listNativeWindow = null;
            }
        }

        private class ListNativeWindow : NativeWindow
        {
            private readonly CheckedComboBox owner;
            private readonly IntPtr hwndList;

            public ListNativeWindow(CheckedComboBox owner, IntPtr hwndList)
            {
                this.owner = owner;
                this.hwndList = hwndList;
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_LBUTTONDOWN)
                {
                    IntPtr res = SendMessage(hwndList, LB_ITEMFROMPOINT, IntPtr.Zero, m.LParam);
                    int r = res.ToInt32();
                    int index = r & 0xFFFF;
                    int outside = (r >> 16) & 0xFFFF;

                    if (outside == 0 && index >= 0 && index < owner.internalItems.Count)
                    {
                        // alterna o item
                        owner.internalItems[index].Checked = !owner.internalItems[index].Checked;
                        owner.UpdateText();

                        // redesenha a lista
                        InvalidateRect(hwndList, IntPtr.Zero, true);

                        // não repassa a mensagem => não fecha o dropdown
                        return;
                    }
                }

                base.WndProc(ref m);
            }
        }

        private class CheckedItem
        {
            public string Text { get; set; }
            public bool Checked { get; set; }
        }
    }
}
