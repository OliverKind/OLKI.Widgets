/*
 * OLKI.Widgets
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * Set the progress elemetns by invoking, if required, in progress controle
 * 
 * This code base on code written by sagar_253, 21 Mar 2014:
 * Original Autor:      sagar_253, 21 Mar 2014
 * Original Source:     http://www.codeproject.com/Tips/734463/Sort-listview-Columns-and-Set-Sort-Arrow-Icon-on-C
 * Original Titel:      Sort listview Columns and Set Sort Arrow Icon on Column Header
 * Original Licence:    The Code Project Open License (CPOL)
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
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OLKI.Widgets
{
    /// <summary>
    /// An ListView with the ability to sort by click a column and add a sort arrow to the clicked column
    /// </summary>
    public class SortListView : ListView
    {
        #region Fields
        /// <summary>
        /// Specifies the column sorter
        /// </summary>
        private ColumnSorter _columnSorter = null;
        #endregion

        #region Methodes
        /// <summary>
        /// Initialise a new sortable ListView
        /// </summary>
        public SortListView()
        {
            this._columnSorter = new ColumnSorter();
            base.ListViewItemSorter = this._columnSorter;
        }

        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            ListView myListView = this;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == this._columnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (this._columnSorter.Order == SortOrder.Ascending)
                {
                    this._columnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    this._columnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                this._columnSorter.SortColumn = e.Column;
                this._columnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            myListView.Sort();
            OLKI.Widgets.ListViewExtensions.SetSortIcon(myListView, this._columnSorter.SortColumn, this._columnSorter.Order);
            base.OnColumnClick(e);
        }
        #endregion

        #region SubClasses
        /// <summary>
        /// Provides sorting of the columns
        /// </summary>
        internal class ColumnSorter : IComparer
        {
            #region Properties
            /// <summary>
            /// Specifies the columns to sort
            /// </summary>
            private int _sortColumn = 0;
            /// <summary>
            /// Get or set the columns to sort
            /// </summary>
            internal int SortColumn
            {
                set
                {
                    this._sortColumn = value;
                }
                get
                {
                    return this._sortColumn;
                }
            }

            /// <summary>
            /// Specifies the sort order of the column
            /// </summary>
            private SortOrder _sortOrder = SortOrder.None;
            /// <summary>
            /// Get or set the sort order of the column
            /// </summary>
            internal SortOrder Order
            {
                set
                {
                    this._sortOrder = value;
                }
                get
                {
                    return this._sortOrder;
                }
            }
            #endregion

            #region Fields
            /// <summary>
            /// Specifies the comparer for comparing columns
            /// </summary>
            private Comparer _listViewItemComparer = new Comparer(System.Globalization.CultureInfo.CurrentUICulture);
            #endregion

            /// <summary>
            /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
            /// </summary>
            /// <param name="x">First object to be compared</param>
            /// <param name="y">Second object to be compared</param>
            /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
            public int Compare(object x, object y)
            {
                try
                {
                    ListViewItem lviX = (ListViewItem)x;
                    ListViewItem lviY = (ListViewItem)y;

                    int compareResult = 0;

                    if (lviX.SubItems[this._sortColumn].Tag != null && lviY.SubItems[this._sortColumn].Tag != null)
                    {
                        compareResult = this._listViewItemComparer.Compare(lviX.SubItems[this._sortColumn].Tag, lviY.SubItems[this._sortColumn].Tag);
                    }
                    else
                    {
                        compareResult = this._listViewItemComparer.Compare(lviX.SubItems[this._sortColumn].Text, lviY.SubItems[this._sortColumn].Text);
                    }

                    if (this._sortOrder == SortOrder.Ascending)
                    {
                        return compareResult;
                    }
                    else if (this._sortOrder == SortOrder.Descending)
                    {
                        return (-compareResult);
                    }
                    else
                    {
                        return 0;
                    }

                }
                catch
                {
                    return 0;
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// Extends the ListView with an sort arrow at the sorte
    /// </summary>
    internal static class ListViewExtensions
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct LVCOLUMN
        {
            public Int32 mask;
            public Int32 cx;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszText;
            public IntPtr hbm;
            public Int32 cchTextMax;
            public Int32 fmt;
            public Int32 iSubItem;
            public Int32 iImage;
            public Int32 iOrder;
        }

        const Int32 HDI_WIDTH = 0x0001;
        const Int32 HDI_HEIGHT = HDI_WIDTH;
        const Int32 HDI_TEXT = 0x0002;
        const Int32 HDI_FORMAT = 0x0004;
        const Int32 HDI_LPARAM = 0x0008;
        const Int32 HDI_BITMAP = 0x0010;
        const Int32 HDI_IMAGE = 0x0020;
        const Int32 HDI_DI_SETITEM = 0x0040;
        const Int32 HDI_ORDER = 0x0080;
        const Int32 HDI_FILTER = 0x0100;

        const Int32 HDF_LEFT = 0x0000;
        const Int32 HDF_RIGHT = 0x0001;
        const Int32 HDF_CENTER = 0x0002;
        const Int32 HDF_JUSTIFYMASK = 0x0003;
        const Int32 HDF_RTLREADING = 0x0004;
        const Int32 HDF_OWNERDRAW = 0x8000;
        const Int32 HDF_STRING = 0x4000;
        const Int32 HDF_BITMAP = 0x2000;
        const Int32 HDF_BITMAP_ON_RIGHT = 0x1000;
        const Int32 HDF_IMAGE = 0x0800;
        const Int32 HDF_SORTUP = 0x0400;
        const Int32 HDF_SORTDOWN = 0x0200;

        const Int32 LVM_FIRST = 0x1000;         // List messages
        const Int32 LVM_GETHEADER = LVM_FIRST + 31;
        const Int32 HDM_FIRST = 0x1200;         // Header messages
        const Int32 HDM_SETIMAGELIST = HDM_FIRST + 8;
        const Int32 HDM_GETIMAGELIST = HDM_FIRST + 9;
        const Int32 HDM_GETITEM = HDM_FIRST + 11;
        const Int32 HDM_SETITEM = HDM_FIRST + 12;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessageLVCOLUMN(IntPtr hWnd, Int32 Msg, IntPtr wParam, ref LVCOLUMN lPLVCOLUMN);

        /// <summary>
        /// Set the arrow to the sorted column and removes the arrows from the all ohter columns
        /// </summary>
        /// <param name="listView">Specifies the ListView where the columns is defined</param>
        /// <param name="columnIndex">Specifies the index of the columns to set the arrow</param>
        /// <param name="order">Specifies the sort order of the columns</param>
        public static void SetSortIcon(ListView listView, int columnIndex, SortOrder order)
        {
            IntPtr columnHeader = SendMessage(listView.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);

            for (int columnNumber = 0; columnNumber <= listView.Columns.Count - 1; columnNumber++)
            {
                IntPtr columnPtr = new IntPtr(columnNumber);
                LVCOLUMN lvColumn = new LVCOLUMN();
                lvColumn.mask = HDI_FORMAT;

                SendMessageLVCOLUMN(columnHeader, HDM_GETITEM, columnPtr, ref lvColumn);

                if (!(order == SortOrder.None) && columnNumber == columnIndex)
                {
                    switch (order)
                    {
                        case System.Windows.Forms.SortOrder.Ascending:
                            lvColumn.fmt &= ~HDF_SORTDOWN;
                            lvColumn.fmt |= HDF_SORTUP;
                            break;
                        case System.Windows.Forms.SortOrder.Descending:
                            lvColumn.fmt &= ~HDF_SORTUP;
                            lvColumn.fmt |= HDF_SORTDOWN;
                            break;
                    }
                    lvColumn.fmt |= (HDF_LEFT | HDF_BITMAP_ON_RIGHT);
                }
                else
                {
                    lvColumn.fmt &= ~HDF_SORTDOWN & ~HDF_SORTUP & ~HDF_BITMAP_ON_RIGHT;
                }

                SendMessageLVCOLUMN(columnHeader, HDM_SETITEM, columnPtr, ref lvColumn);
            }
        }
    }
}