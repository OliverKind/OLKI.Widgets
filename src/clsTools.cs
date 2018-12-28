/*
 * Filename:        clsTools.cs
 * Created:         2017-05-07
 * Last modified:   2017-05-07
 * Copyright:       Oliver Kind - 2017
 * License:         LGPL
 * 
 * File Content:
 * 1. ComboBox_AutoDropDownWidth
 * 
 * Desctiption:
 * Provides tools for widgets
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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