/*
 * OLKI.Widgets
 * 
 * Copyright:   Oliver Kind - 2020
 * License:     LGPL
 * 
 * Desctiption:
 * Set Form properties by invoking, if required
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
    /// Set TextBox properties by invoking, if required
    /// </summary>
    public static class FormInv
    {
        /// <summary>
        /// Set Form text, if required invoke
        /// </summary>
        /// <param name="form">Form to set the text</param>
        /// <param name="text">Text to set to TextBox.Text</param>
        public static void Text(Form form, string text)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new Action<Form, string>(Text), new object[] { form, text });
            }
            else
            {
                form.Text = text;
            }
        }
    }
}
