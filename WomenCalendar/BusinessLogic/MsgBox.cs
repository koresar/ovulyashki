using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    /// <summary>
    /// The replacement for MessageBox.Show() set of functions.
    /// </summary>
    public static class MsgBox
    {
        /// <summary>
        /// The answer which is returned by all Show() functions calls (if set to non default 'None' value).
        /// </summary>
        public enum Answer
        {
            /// <summary>
            /// Default value. Indicates message boxes shold popup.
            /// </summary>
            None,

            /// <summary>
            /// Indicates all answers should either 'OK' or 'Yes'.
            /// </summary>
            Yes,

            /// <summary>
            /// Indicates all answers should either 'No' or 'Cancel'.
            /// </summary>
            No
        }

        /// <summary>
        /// If set to non default value then used/return instead of UI window prompt.
        /// </summary>
        public static Answer AlwaysAnswer { get; set; }

        public static int AutoAnswersCount { get; set; }

        /// <summary>
        /// Show 'OK' message box.
        /// </summary>
        /// <param name="text">Message text.</param>
        /// <param name="caption">Window caption.</param>
        public static void Show(string text, string caption)
        {
            if (AlwaysAnswer != Answer.None)
            {
                AutoAnswersCount++;
                return;
            }

            MessageBox.Show(text, caption);
        }

        /// <summary>
        /// Show 'Yes/No' message box.
        /// </summary>
        /// <param name="text">Message text.</param>
        /// <param name="caption">Window caption.</param>
        /// <returns>True if 'Yes' clicked (or allways answer is set to non default).</returns>
        public static bool YesNo(string text, string caption)
        {
            if (AlwaysAnswer != Answer.None)
            {
                AutoAnswersCount++;
                return AlwaysAnswer == Answer.Yes;
            }

            return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public static DialogResult YesNoCancel(string text, string caption)
        {
            if (AlwaysAnswer != Answer.None)
            {
                AutoAnswersCount++;
                return DialogResult.Yes;
            }

            return MessageBox.Show(text, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Show 'OK' message box with error icon.
        /// </summary>
        /// <param name="text">Message text.</param>
        /// <param name="caption">Window caption.</param>
        public static void Error(string text, string caption)
        {
            if (AlwaysAnswer != Answer.None)
            {
                AutoAnswersCount++;
                return;
            }

            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
