/*
 * OLKI.Widgets
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * Set Label properties by invoking, if required
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
    /// Set Label properties by invoking, if required
    /// </summary>
    public static class ListViewInv
    {
        /// <summary>
        /// Add new item to listview, if required invoke
        /// </summary>
        /// <param name="listView">ListView to clear to add item</param>
        /// <param name="newItem">Item to add to ListView</param>
        public static void AddItem(ListView listView, ListViewItem newItem)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke(new Action<ListView, ListViewItem>(AddItem), new object[] { listView, newItem });
            }
            else
            {
                listView.Items.Add(newItem);
            }
        }

        /// <summary>
        /// Clears ListView by Invoke, if required
        /// </summary>
        /// <param name="listView">ListView to clear</param>
        public static void ClearItems(ListView listView)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke(new Action<ListView>(ClearItems), new object[] { listView });
            }
            else
            {
                listView.Items.Clear();
            }
        }
    }
}
