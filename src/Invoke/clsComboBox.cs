/*
 * OLKI.Widgets
 * 
 * Copyright:   Oliver Kind - 2020
 * License:     LGPL
 * 
 * Desctiption:
 * Set ComboBox properties by invoking, if required
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
    /// Set ComboBox properties by invoking, if required
    /// </summary>
    public static class ComboBoxInv
    {
        /// <summary>
        /// Set ComboBox DropDownWidth, if required invoke
        /// </summary>
        /// <param name="comboBox">ComboBox to set the DropDownWidth</param>
        /// <param name="dropDownWidth">Width to set to ComboBox.DropDownWidth</param>
        public static void DropDownWidth(ComboBox comboBox, int dropDownWidth)
        {
            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new Action<ComboBox, int>(DropDownWidth), new object[] { comboBox, dropDownWidth });
            }
            else
            {
                comboBox.DropDownWidth = dropDownWidth;
            }
        }

        /// <summary>
        /// Set ComboBox SelectedIndex, if required invoke
        /// </summary>
        /// <param name="comboBox">ComboBox to set the SelectedIndex</param>
        /// <param name="selectedIndex">Width to set to ComboBox.SelectedIndex</param>
        public static void SelectedIndex(ComboBox comboBox, int selectedIndex)
        {
            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new Action<ComboBox, int>(SelectedIndex), new object[] { comboBox, selectedIndex });
            }
            else
            {
                comboBox.SelectedIndex = selectedIndex;
            }
        }
    }
}
