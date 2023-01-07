#if NETFRAMEWORK || NETCOREAPP
using System.Windows.Forms;
using FlaUI.Core.WindowsAPI;

namespace FlaUI.Core.Overlay
{
    /// <summary>
    /// Overlay form
    /// </summary>
    public class OverlayRectangleForm : Form
    {
        /// <summary>
        /// C-Tor
        /// </summary>
        public OverlayRectangleForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            Left = 0;
            Top = 0;
            Width = 1;
            Height = 1;
            Visible = false;
        }

        /// <summary>
        /// ?
        /// </summary>
        protected override bool ShowWithoutActivation => true;

        /// <summary>
        /// Create params for an invisible overlay.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var createParams = base.CreateParams;
                createParams.ExStyle |= (int)WindowStyles.WS_EX_TRANSPARENT;
                createParams.ExStyle |= (int)WindowStyles.WS_EX_TOOLWINDOW;
                createParams.ExStyle |= (int)WindowStyles.WS_EX_TOPMOST;
                return createParams;
            }
        }
    }
}
#endif
