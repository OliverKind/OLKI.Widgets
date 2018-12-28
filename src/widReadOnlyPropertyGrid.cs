/*
 * Filename:        widReadOnlyPropertyGrid.cs
 * Created:         2017-01-04
 * Last modified:   2017-01-04
 * Copyright:       Oliver Kind - 2017 (only of this file and the widget, not the original source code, see below)
 * License:         LGPL (only of this file and the widget, not the original source code, see below)
 * 
 * This code was inspired by sorce code written by Franta Zahora, 08-Jun-2009
 * Original Autor:      Franta Zahora, 08-Jun-2009
 * Original Source:     http://www.csharp-examples.net/readonly-propertygrid/
 * Original Titel:      Read-only PropertyGrid [C#]
 * 
 * What was done by me:
 * I have taken the code from the webpage, modified it and used it to write this widget.
 * The Original Sourcecode is attached at the end of the file
 * 
 * File Content:
 * 1. SortListView
 * 2. Original sourcecode as it was published by sagar_253, 21 Mar 2014
 * 
 * Desctiption:
 * An PropertyGrid with the ability to read only the properties
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