namespace MotionDetection
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    using Emgu.CV.UI;

    partial class WindowsDetector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        internal SplitContainer _container;
        internal ImageBox _imageBox;
        internal ImageBox _imageToBox;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

            this._container = new SplitContainer();
            this._imageBox = new ImageBox();
            this._imageToBox = new ImageBox();

            ((ISupportInitialize)(this._imageBox)).BeginInit();
            this._container.Panel1.SuspendLayout();
            this._container.Panel2.SuspendLayout();
            this._container.SuspendLayout();
            ((ISupportInitialize)(this._imageToBox)).BeginInit();
            this.SuspendLayout();

            this._imageBox.Dock = DockStyle.Fill;
            this._imageBox.Location = new Point(0, 0);
            this._imageBox.Name = "Image Box";
            this._imageBox.Size = new Size(320, 320);
            this._imageBox.TabIndex = 2;
            this._imageBox.TabStop = false;

            this._container.Dock = DockStyle.Fill;
            this._container.Location = new Point(0, 0);
            this._container.Name = "Container Frames";

            this._container.Panel1.Controls.Add(this._imageBox);

            this._container.Panel2.Controls.Add(this._imageToBox);
            this._container.Size = new Size(320, 320);
            this._container.SplitterDistance = 320;
            this._container.TabIndex = 3;

            this._imageToBox.Dock = DockStyle.Fill;
            this._imageToBox.Location = new Point(0, 0);
            this._imageToBox.Name = "Image To Box";
            this._imageToBox.Size = new Size(350, 390);
            this._imageToBox.TabIndex = 2;
            this._imageToBox.TabStop = false;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new Size(1280, 640);
            this.Controls.Add(this._container);
            this.Name = "Windows Detection";
            this.Text = "Windows Detection";

            ((ISupportInitialize)(this._imageBox)).EndInit();
            this._container.Panel1.ResumeLayout(false);
            this._container.Panel2.ResumeLayout(false);
            this._container.ResumeLayout(false);
            ((ISupportInitialize)(this._imageToBox)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

    }
}

