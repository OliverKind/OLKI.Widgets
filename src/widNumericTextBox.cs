/*
 * Filename:        widNumericTextBox.cs
 * Created:         2017-01-04
 * Last modified:   2017-01-04
 * Copyright:       Oliver Kind - 2017
 * License:         LGPL
 * 
 * File Content:
 * 1. NumericTextBox
 * 
 * Desctiption:
 * A TextBox that accepts only numeric values
 * 
 * */

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
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