/*
 * OLKI.Widgets
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * rovides tools for widgets
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

using System.Windows.Forms;

namespace OLKI.Widgets
{
    public static class Tools
    {
        #region Methods
        /// <summary>
        /// Calculates the optimal ropDownWidth of an ComboBox to show the full drop down text an avoid cotting it of, because it is loonger than the DropDownWidth
        /// </summary>
        /// <param name="comboBox"></param>
        public static void ComboBox_AutoDropDownWidth(ComboBox comboBox)
        {
            // Get the original DropDownWidth
            int DropDownWidth = comboBox.DropDownWidth;

            //The Graphics for the control, of the ComboBox
            System.Drawing.Graphics Graphics = comboBox.CreateGraphics();

            // Get the with of the scroolbar if the scroolbar is shown
            int VerticalScrollBarWidth = (comboBox.Items.Count > comboBox.MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0;

            int itemWidth;
            // Move to every item, calculate the necessary with an set new with if it is greater
            foreach (string item in comboBox.Items)
            {
                itemWidth = (int)Graphics.MeasureString(item, comboBox.Font).Width + VerticalScrollBarWidth;
                if (itemWidth > DropDownWidth)
                {
                    DropDownWidth = itemWidth;
                }
            }
            // Set new ComboBox width
            comboBox.DropDownWidth = DropDownWidth;
        }
        #endregion
    }
}