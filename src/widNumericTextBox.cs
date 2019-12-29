/*
 * OLKI.Widgets
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * Set the progress elemetns by invoking, if required, in progress controle
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

namespace OLKI.Widgets
{
    /// <summary>
    /// A TextBox that accepts only numeric values
    /// </summary>
    public class NumericTextBox : TextBox
    {
        #region Fields
        /// <summary>
        /// Stores the text bevore text was changed to reset the text to this value of the new text is not valid
        /// </summary>
        private string _textBefore = string.Empty;
        #endregion

        #region Propoerteis
        /// <summary>
        /// Get or set the text of the textbox
        /// </summary>
        public override string Text
        {
            get
            {
                int Dummy;
                if (int.TryParse(base.Text, out Dummy))
                {
                    return base.Text;
                }
                return string.Empty;
            }
        }
        #endregion

        #region Methodes
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            int Dummy;
            if (int.TryParse(base.Text, out Dummy) || string.IsNullOrEmpty(base.Text))
            {
                this._textBefore = base.Text;
            }
            else
            {
                base.Text = this._textBefore;
            }
        }
        #endregion

        /*
         * Not Used, maybe used in following versions
        public enum NumberFormat
        {
            NaturalNumber, //0, 1, 2, 3, ...
            Integers, //..., −3, −2, −1, 0, 1, 2, 3, ...
            RealNumbers //..., −3, −0.2, −0.1, 0, 0.1, 0.2, 3, ...
        }

        internal bool AllowSign = true;
        internal bool AllowDecimalPoint = true;
        internal bool UseCommaAsDecimalPoint = true;
        internal NumberFormat _numberFormat = NumberFormat.NaturalNumber;
        */
    }
}