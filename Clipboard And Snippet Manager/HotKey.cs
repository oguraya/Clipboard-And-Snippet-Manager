using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Clipboard_And_Snippet_Manager
{
    /// <summary>
    /// グローバルホットキーを登録するクラス。
    /// 使用後は必ずDisposeすること。
    /// 参考: http://anis774.net/codevault/hotkey.html
    /// 指定秒数以内に2回ホットキーを押下した時コールバックされるモードを追加
    /// </summary>
    public class HotKey : IDisposable
    {
        HotKeyForm form;
        /// <summary>
        /// ホットキーが押されると発生する。
        /// </summary>
        public event EventHandler HotKeyPush;

        /// <summary>
        /// ホットキーを指定して初期化する。
        /// 使用後は必ずDisposeすること。
        /// </summary>
        /// <param name="modKey">修飾キー</param>
        /// <param name="key">キー</param>
        public HotKey(MOD_KEY modKey, Keys key)
        {
            form = new HotKeyForm(modKey, key, raiseHotKeyPush);
        }

        /// <summary>
        /// ホットキーを指定して初期化する。
        /// 使用後は必ずDisposeすること。
        /// </summary>
        /// <param name="modKey">修飾キー</param>
        /// <param name="key">キー</param>
        /// <param name="maxIntervalSecond">連打許容間隔秒数</param>
        public HotKey(MOD_KEY modKey, Keys key, double maxIntervalSecond)
        {
            form = new HotKeyForm(modKey, key, maxIntervalSecond, raiseHotKeyPush);
        }


        private void raiseHotKeyPush()
        {
            if (HotKeyPush != null)
            {
                HotKeyPush(this, EventArgs.Empty);
            }
        }

        public void Dispose()
        {
            form.Dispose();
        }

        private class HotKeyForm : Form
        {
            [DllImport("user32.dll")]
            extern static int RegisterHotKey(IntPtr HWnd, int ID, MOD_KEY MOD_KEY, Keys KEY);

            [DllImport("user32.dll")]
            extern static int UnregisterHotKey(IntPtr HWnd, int ID);

            const int WM_HOTKEY = 0x0312;
            int id;
            ThreadStart proc;
            readonly bool required_double_stroke;
            readonly double max_interval_sec;
            DateTime last_message_dt;

            public HotKeyForm(MOD_KEY modKey, Keys key, ThreadStart proc)
            {
                this.required_double_stroke = false;

                this.proc = proc;
                for (int i = 0x0000; i <= 0xbfff; i++)
                {
                    if (RegisterHotKey(this.Handle, i, modKey, key) != 0)
                    {
                        id = i;
                        break;
                    }
                }
            }

            public HotKeyForm(MOD_KEY modKey, Keys key, double max_interval_sec, ThreadStart proc)
            {
                this.required_double_stroke = true;
                this.max_interval_sec = max_interval_sec;
                this.proc = proc;
                for (int i = 0x0000; i <= 0xbfff; i++)
                {
                    if (RegisterHotKey(this.Handle, i, modKey, key) != 0)
                    {
                        id = i;
                        break;
                    }
                }
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == WM_HOTKEY)
                {
                    if ((int)m.WParam == id)
                    {
                        if (this.required_double_stroke)
                        {
                            TimeSpan ts = DateTime.Now - last_message_dt;
                            if (ts.TotalSeconds <= this.max_interval_sec)
                            {
                                proc();
                            }
                            last_message_dt = DateTime.Now;
                        }
                        else
                        {
                            proc();
                        }

                    }
                }
            }

            protected override void Dispose(bool disposing)
            {
                UnregisterHotKey(this.Handle, id);
                base.Dispose(disposing);
            }
        }
    }

    /// <summary>
    /// HotKeyクラスの初期化時に指定する修飾キー
    /// </summary>
    public enum MOD_KEY : int
    {
        ALT = 0x0001,
        CONTROL = 0x0002,
        SHIFT = 0x0004,
    }
}
