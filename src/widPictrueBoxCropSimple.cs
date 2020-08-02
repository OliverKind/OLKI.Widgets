/*
 * OLKI.Widgets
 * 
 * Copyright:   Oliver Kind - 2020
 * License:     LGPL
 * 
 * Desctiption:
 * An PictureBox to crop images
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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OLKI.Widgets
{
    /// <summary>
    /// An PictureBox to crop images
    /// </summary>
    public partial class PictrueBoxCropSimple : PictureBox
    {
        #region Properties
        /// <summary>
        /// True if an area is currently selected
        /// </summary>
        private bool _selectingArea;

        /// <summary>
        /// The selected area to crop
        /// </summary>
        private Rectangle _cropArea;
        /// <summary>
        /// Get the selected area to crop
        /// </summary>
        [Category("Crop")]
        [DisplayName("Selected crop area")]
        [Description("The selected area to crop")]
        public Rectangle? CropAreaSelection
        {
            get
            {
                if (!this.GetShowRectangle(this._cropArea)) return null;
                return this._cropArea;
            }
        }

        /// <summary>
        /// Get the croped area, fitted to the image
        /// </summary>
        [Category("Crop")]
        [DisplayName("Fitted crop area")]
        [Description("The croped area, fitted to the image")]
        public Rectangle? CropAreaFit
        {
            get
            {
                if (!this.GetShowRectangle(this._cropArea)) return null;

                Rectangle FitArea = this.CropAreaSelection.Value;
                FitArea.Width = (int)Math.Round(FitArea.Width / this.ScaleFactor, 0);
                FitArea.Height = (int)Math.Round(FitArea.Height / this.ScaleFactor, 0);

                if (this.RatioImage >= this.RatioBox)
                {
                    FitArea.X = (int)Math.Round(FitArea.X / this.ScaleFactor, 0);
                    FitArea.Y = (int)Math.Round((FitArea.Y - (int)Math.Round(this.Height / 2 - (Image.Height * ScaleFactor) / 2, 0)) / ScaleFactor, 0);
                }
                else
                {
                    FitArea.X = (int)Math.Round((FitArea.X - (int)Math.Round(this.Width / 2 - (Image.Width * ScaleFactor) / 2, 0)) / ScaleFactor, 0);
                    FitArea.Y = (int)Math.Round(FitArea.Y / this.ScaleFactor, 0);
                }

                if (FitArea.X < 0)
                {
                    FitArea.Width += FitArea.X;
                    FitArea.X = 0;
                }
                if (FitArea.X + FitArea.Width > Image.Width) FitArea.Width = Image.Width - FitArea.X;
                if (FitArea.Width <= 0) FitArea.Width = 0;

                if (FitArea.Y < 0)
                {
                    FitArea.Height += FitArea.Y;
                    FitArea.Y = 0;
                }
                if (FitArea.Y + FitArea.Height > Image.Height) FitArea.Height = Image.Height - FitArea.Y;
                if (FitArea.Height <= 0) FitArea.Height = 0;

                if (FitArea.Width == 0 && FitArea.Height == 0) return null;

                return FitArea;
            }
        }

        /// <summary>
        /// Get the croped image, if it was set
        /// </summary>
        [Category("Crop")]
        [DisplayName("Croped image")]
        [Description("The croped image, if it was set")]
        public Image CropedImage
        {
            get
            {
                try
                {
                    if (this.Image == null) return null;
                    if (this.SizeMode != PictureBoxSizeMode.Zoom) return null;
                    if (!this._cropMode) return null;
                    if (this.CropAreaFit == null) return null;
                    if (this.CropAreaFit.Value.Width < 0) return null;
                    if (this.CropAreaFit.Value.Height < 0) return null;

                    return OLKI.Tools.ColorAndPicture.Picture.Modify.Crop(this.Image, this.CropAreaFit);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print(ex.ToString());
                    return null;
                }
            }
        }

        /// <summary>
        /// PictureBox is in crop mode
        /// </summary>
        private bool _cropMode = true;
        /// <summary>
        /// Get or set if the PictureBox is in crop mode
        /// </summary>
        [Category("Crop")]
        [DisplayName("Crop mode active")]
        [Description("PictureBox is in crop mode")]
        public bool CropMode
        {
            get
            {
                return this._cropMode;
            }
            set
            {
                this._cropMode = value;
                if (!value)
                {
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Get the area where the image is placed, if SizeMode is Zoom
        /// </summary>
        [Category("Crop - Additional")]
        [DisplayName("Fitted image area")]
        [Description("The area where the image is placed, if SizeMode is Zoom")]
        public Rectangle? ImageAreaFit
        {
            get
            {
                if (this.Image == null || this.SizeMode != PictureBoxSizeMode.Zoom || !this._cropMode) return null;
                if (this.RatioImage >= this.RatioBox)
                {
                    int ImageScaleHeigt = (int)Math.Round((float)(this.Image.Height * this.ScaleFactor), 0);
                    int ImagePosY = (int)Math.Round((float)(this.Height / 2 - ImageScaleHeigt / 2), 0);
                    return new Rectangle(new Point(0, ImagePosY), new Size(this.Width, ImageScaleHeigt));
                }
                else
                {
                    int ImageScaleWidth = (int)Math.Round((float)(this.Image.Width * this.ScaleFactor), 0);
                    int ImagePosX = (int)Math.Round((float)(this.Width / 2 - ImageScaleWidth / 2), 0);
                    return new Rectangle(new Point(ImagePosX, 0), new Size(ImageScaleWidth, this.Height));
                }
            }
        }

        /// <summary>
        /// Get the ratio of the PictureBox
        /// </summary>
        [Category("Crop - Additional")]
        [DisplayName("PictureBox ratio")]
        [Description("The ratio of the PictureBox")]
        public float RatioBox
        {
            get
            {
                return (float)this.Width / (float)this.Height;
            }
        }

        /// <summary>
        /// Get the ratio of the image
        /// </summary>
        [Category("Crop - Additional")]
        [DisplayName("Image ratio")]
        [Description("The ratio of the image")]
        public float RatioImage
        {
            get
            {
                if (this.Image == null) return 0;
                return (float)this.Image.Width / (float)this.Image.Height;
            }
        }

        /// <summary>
        /// Get the scale factor of the image, if SizeMode is Zoom
        /// </summary>
        [Category("Crop - Additional")]
        [DisplayName("Image scale factor")]
        [Description("Scale factor of the image, if SizeMode is Zoom")]
        public float ScaleFactor
        {
            get
            {
                float scaleFactor = 0;
                if (this.Image == null || this.SizeMode != PictureBoxSizeMode.Zoom) return scaleFactor;

                if (this.RatioImage >= this.RatioBox)
                {
                    scaleFactor = (float)this.Width / (float)this.Image.Width;
                }
                else
                {
                    scaleFactor = (float)this.Height / (float)this.Image.Height;
                }
                return scaleFactor;
            }
        }
        #endregion

        #region Methodes
        public PictrueBoxCropSimple()
        {
        }

        /// <summary>
        /// Create an now crop rectangle with start position and no size and call base.OnMouseDown(e)
        /// </summary>
        /// <param name="e">Provides data for the MouseUp, MouseDown, and MouseMove events.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.SizeMode == PictureBoxSizeMode.Zoom && this._cropMode)
            {
                this._selectingArea = true;
                this._cropArea = new Rectangle(new Point(e.X, e.Y), new Size(0, 0));
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Change the size of the crop rectangle and call base.OnMouseMove(e)
        /// </summary>
        /// <param name="e">Provides data for the MouseUp, MouseDown, and MouseMove events.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this._selectingArea)
            {
                this._cropArea.Width = e.X - this._cropArea.X;
                this._cropArea.Height = e.Y - this._cropArea.Y;
                this.Refresh();
            }
            base.OnMouseMove(e);
        }

        /// <summary>
        /// Fix crop rectangle size and call base.OnMouseUp(e)
        /// </summary>
        /// <param name="e">Provides data for the MouseUp, MouseDown, and MouseMove events.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this._selectingArea = false;
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Paint crop rectangle and call base.OnPaint(e)
        /// </summary>
        /// <param name="e">Provides data for the Paint event.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this._selectingArea && this.SizeMode == PictureBoxSizeMode.Zoom && this.CropMode) e.Graphics.DrawRectangle(Pens.Red, this._cropArea);
        }

        /// <summary>
        /// Reset the crop area
        /// </summary>
        public void ResetCropArea()
        {
            this._cropArea = new Rectangle(new Point(0, 0), new Size(0, 0));
        }

        /// <summary>
        /// Set the croped image to image and reset the crop area
        /// </summary>
        public void SetCropedImage()
        {
            try
            {
                if (this.Image == null) return;
                if (this.SizeMode != PictureBoxSizeMode.Zoom) return;
                if (!this._cropMode) return;
                if (this.CropAreaFit == null) return;
                if (this.CropAreaFit.Value.Width < 0) return;
                if (this.CropAreaFit.Value.Height < 0) return;
                if (this.CropedImage == null) return;

                this.Image = this.CropedImage;
                this.ResetCropArea();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
            }
        }

        /// <summary>
        /// Get if the cropping rectangle should been shown
        /// </summary>
        /// <param name="rectangle">Cropping rectangle</param>
        /// <returns>True if the rectangle should been shown</returns>
        private bool GetShowRectangle(Rectangle? rectangle)
        {
            if (this.Image == null) return false;
            if (this.SizeMode != PictureBoxSizeMode.Zoom) return false;
            if (!this._cropMode) return false;
            if (!rectangle.HasValue) return false;

            if (rectangle.Value.Width < 0) return false;
            if (rectangle.Value.Height < 0) return false;

            return true;
        }
        #endregion
    }
}