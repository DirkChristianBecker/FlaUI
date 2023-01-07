#if NETFRAMEWORK || NETCOREAPP
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using FlaUI.Core.WindowsAPI;

namespace FlaUI.Core.Overlay
{
    /// <summary>
    /// Windows forms overlay manager.
    /// </summary>
    public class WinFormsOverlayManager : IOverlayManager
    {
        /// <summary>
        /// Size
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Margin
        /// </summary>
        public int Margin { get; set; }

        /// <summary>
        /// C-Tor
        /// </summary>
        public WinFormsOverlayManager()
        {
            Size = 3;
            Margin = 0;
        }

        /// <summary>
        /// Show an overlay with given parameters.
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        /// <param name="durationInMs"></param>
        public void Show(Rectangle rectangle, Color color, int durationInMs)
        {
            if (!rectangle.IsEmpty)
            {
                System.Threading.Tasks.Task.Run(() => CreateAndShowForms(rectangle, color, durationInMs));
            }
        }

        /// <summary>
        /// Show blocking.
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        /// <param name="durationInMs"></param>
        public void ShowBlocking(Rectangle rectangle, Color color, int durationInMs)
        {
            CreateAndShowForms(rectangle, color, durationInMs);
        }

        /// <summary>
        /// Create the form
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        /// <param name="durationInMs"></param>
        private void CreateAndShowForms(Rectangle rectangle, Color color, int durationInMs)
        {
            var leftBorder = new Rectangle(rectangle.X - Margin, rectangle.Y - Margin, Size, rectangle.Height + 2 * Margin);
            var topBorder = new Rectangle(rectangle.X - Margin, rectangle.Y - Margin, rectangle.Width + 2 * Margin, Size);
            var rightBorder = new Rectangle(rectangle.X + rectangle.Width - Size + Margin, rectangle.Y - Margin, Size, rectangle.Height + 2 * Margin);
            var bottomBorder = new Rectangle(rectangle.X - Margin, rectangle.Y + rectangle.Height - Size + Margin, rectangle.Width + 2 * Margin, Size);
            var allBorders = new[] { leftBorder, topBorder, rightBorder, bottomBorder };

            var gdiColor = Color.FromArgb(color.A, color.R, color.G, color.B);
            var forms = new List<OverlayRectangleForm>();
            foreach (var border in allBorders)
            {
                var form = new OverlayRectangleForm { BackColor = gdiColor };
                forms.Add(form);
                // Position the window
                User32.SetWindowPos(form.Handle, new IntPtr(-1), border.X, border.Y,
                    border.Width, border.Height, SetWindowPosFlags.SWP_NOACTIVATE);
                // Show the window
                User32.ShowWindow(form.Handle, ShowWindowTypes.SW_SHOWNA);
            }
            Thread.Sleep(durationInMs);
            foreach (var form in forms)
            {
                // Cleanup
                form.Hide();
                form.Close();
                form.Dispose();
            }
        }

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}
#endif
