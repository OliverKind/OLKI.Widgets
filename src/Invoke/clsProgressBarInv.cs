/*
 * OLKI.Widgets
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * Set ProgressBar properties by invoking, if required
 * 
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the LGPL General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * LGPL General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not check the GitHub-Repository.
 * 
 * */

using System;
using System.Windows.Forms;

namespace OLKI.Widgets.Invoke
{
    /// <summary>
    /// Set ProgressBar properties by invoking, if required
    /// </summary>
    public static class ProgressBarInv
    {
        /// <summary>
        /// Set Progressbar style, if required invoke
        /// </summary>
        /// <param name="progressBar">Progressbar to set the style</param>
        /// <param name="style">Style to set to ProgressBar.Style</param>
        public static void Style(ProgressBar progressBar, ProgressBarStyle style)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action<ProgressBar, ProgressBarStyle>(Style), new object[] { progressBar, style });
            }
            else
            {
                progressBar.Style = style;
            }
        }

        /// <summary>
        /// Set Progressbar value, if required invoke
        /// </summary>
        /// <param name="progressBar">Progressbar to set the value</param>
        /// <param name="value">Value to set to ProgressBar.Value</param>
        public static void Value(ProgressBar progressBar, int value)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action<ProgressBar, int>(Value), new object[] { progressBar, value });
            }
            else
            {
                progressBar.Value = value;
            }
        }
    }
}
