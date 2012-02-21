using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vema.PerformanceTracker.UI
{
    /// <summary>
    /// Markus Vetsch, 21.02.2012 14:16
    /// Helper class for any kind of message box display action.
    /// </summary>
    internal static class Gui
    {
        /// <summary>
        /// Represents a system-dependent double line break.
        /// </summary>
        internal static string DoubleNewLine = string.Concat(Environment.NewLine, Environment.NewLine);

        /// <summary>
        /// Shows a general message with specified <paramref name="caption"/> and <paramref name="text"/>.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="text">The text.</param>
        internal static void ShowMessage(string caption, string text)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Shows an information message with specified <paramref name="caption"/> and <paramref name="text"/>.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="text">The text.</param>
        internal static void ShowInformation(string caption, string text)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows an error message with specified <paramref name="caption"/> and <paramref name="text"/>.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="text">The text.</param>
        internal static void ShowError(string caption, string text)
        {
            ShowError(text, caption, null);
        }

        /// <summary>
        /// Shows the <paramref name="exception"/> in a user-friendly error message.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> to be displayed.</param>
        internal static void ShowError(Exception exception)
        {
            ShowError(null, null, exception);
        }

        /// <summary>
        /// Shows an error message with specified <paramref name="caption"/> and <paramref name="text"/> 
        /// and further appends the stack trace of the <see cref="Exception"/> described by <paramref name="ex"/>.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="text">The text.</param>
        /// <param name="ex">The <see cref="Exception"/>.</param>
        private static void ShowError(string caption, string text, Exception ex)
        {
            if (ex == null)
            {
                MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string exceptionText = string.Format("{0}{1}Problem: {2}", text, DoubleNewLine, ex.ToString());

            MessageBox.Show(exceptionText, "Unhandled exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows a question message with specified <paramref name="caption"/> and <paramref name="text"/>
        /// to be answered with Yes / No.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="text">The text.</param>
        /// <returns><c>true</c>, if the question was accepted with Yes; otherwise <c>false</c>.</returns>
        internal static bool AskQuestion(string caption, string text)
        {
            DialogResult result = MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }
    }
}
