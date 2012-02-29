using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Vema.PerformanceTracker.UI
{
    /// <summary>
    /// Markus Vetsch, 21.02.2012 14:16
    /// Helper class for any kind general GUI relevant action.
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
            ShowError(caption, text, null);
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
                return;
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

        /// <summary>
        /// Determines whether the specified <see cref="DialogResult"/> indicates a cancel action.
        /// </summary>
        /// <param name="result">The <see cref="DialogResult"/> to be evaluated.</param>
        /// <returns>
        ///   <c>true</c> if the <see cref="DialogResult"/> indicates a cancel action,
        ///   i.e. equals <see cref="DialogResult.Cancel"/>; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsDialogCancelled(DialogResult result)
        {
            return result == DialogResult.Cancel;
        }

        /// <summary>
        /// Marks the affected <see cref="Control"/> to be in error state.
        /// </summary>
        /// <param name="control">The <see cref="Control"/> to be marked.</param>
        internal static void SetTextboxError(Control control)
        {
            control.BackColor = Color.Tomato;
        }

        /// <summary>
        /// Resets the affected <see cref="Control"/> from an error state.
        /// </summary>
        /// <param name="control">The <see cref="Control"/> to be reset.</param>
        internal static void ResetTextboxFromError(Control control)
        {
            if (IsTextboxError(control)) { control.BackColor = Color.White; }
        }

        /// <summary>
        /// Determines whether the specified <see cref="Control"/> is in an error state.
        /// </summary>
        /// <param name="control">The affected <see cref="Control"/>.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="Control"/> is in an error state; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsTextboxError(Control control)
        {
            return control.BackColor == Color.Tomato;
        }

        /// <summary>
        /// Automatically adjusts the size of list view columns to best fit.
        /// </summary>
        /// <param name="view">The <see cref="ListView"/> to be adjusted..</param>
        internal static void AutoAdjustListViewColumns(ListView view)
        {
            view.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
