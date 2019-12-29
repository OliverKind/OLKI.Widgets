/*
 * OLKI.Widgets
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * Set the progress elemetns by invoking, if required, in progress controle
 * 
 * This code was inspired by sorce code written by Franta Zahora, 08-Jun-2009
 * Original Autor:      Franta Zahora, 08-Jun-2009
 * Original Source:     http://www.csharp-examples.net/readonly-propertygrid/
 * Original Titel:      Read-only PropertyGrid [C#]
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OLKI.Widgets
{
    /// <summary>
    /// An PropertyGrid with the ability to read only the properties
    /// </summary>
    public class ReadOnlyPropertyGrid : PropertyGrid
    {
        #region Properties
        /// <summary>
        /// Specifies if the PropertyGrid is only readable
        /// </summary>
        private bool _readOnly = false;
        /// <summary>
        /// Get or set if the PropertyGrid is only readable
        /// </summary>
        [Category("Behavior")]
        [DisplayName("Read only")]
        [Description("Indicates if the PropertyGrid is in Readonly mode")]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get
            {
                return this._readOnly;
            }
            set
            {
                this._readOnly = value;
                this.SetObjectAsReadOnly();
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Initialise a new ReadOnlyPropertyGrid
        /// </summary>
        public ReadOnlyPropertyGrid()
        {
        }

        /// <summary>
        /// Overwrites OnSelectedObjectsChanged, set ReadOnly attribute and calls defaukt OnSelectedObjectsChanged
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnSelectedObjectsChanged(EventArgs e)
        {
            this.SetObjectAsReadOnly();
            base.OnSelectedObjectsChanged(e);
        }

        /// <summary>
        /// Set or remove ReadOnly to the properties in PropertyGrid object
        /// </summary>
        private void SetObjectAsReadOnly()
        {
            if (base.SelectedObjects.Count() > 1)
            {
                foreach (object Object in base.SelectedObjects)
                {
                    TypeDescriptor.AddAttributes(Object, new Attribute[] { new ReadOnlyAttribute(this._readOnly) });
                }
            }
            if (base.SelectedObject != null)
            {
                TypeDescriptor.AddAttributes(base.SelectedObject, new Attribute[] { new ReadOnlyAttribute(this._readOnly) });
                this.Refresh();
            }
        }
        #endregion
    }
}

/* Original source code from http://www.csharp-examples.net/readonly-propertygrid/
SECITON 1:
TypeDescriptor.AddAttributes(this.SelectedObject, new Attribute[] { new ReadOnlyAttribute(_readOnly) });

SECITON 2:
public class ReadOnlyPropertyGrid : PropertyGrid
{
  private bool _readOnly;
  public bool ReadOnly
  {
    get { return _readOnly; }
    set
    {
      _readOnly = value;
      this.SetObjectAsReadOnly(this.SelectedObject, _readOnly);
    }
  }

  protected override void OnSelectedObjectsChanged(EventArgs e)
  {
    this.SetObjectAsReadOnly(this.SelectedObject, this._readOnly);
    base.OnSelectedObjectsChanged(e);
  }

  private void SetObjectAsReadOnly(object selectedObject, bool isReadOnly)
  {
    if (this.SelectedObject != null)
    {
      TypeDescriptor.AddAttributes(this.SelectedObject, new Attribute[] { new ReadOnlyAttribute(_readOnly) });
      this.Refresh();
    }
  }
}

SECTION 3:
// store the provider
TypeDescriptionProvider provider = TypeDescriptor.AddAttributes(this.SelectedObject,
new Attribute[] { new ReadOnlyAttribute(_readOnly) });

// remove the provider
TypeDescriptor.RemoveProvider(provider, this.SelectedObject);
*/