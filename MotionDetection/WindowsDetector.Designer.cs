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
            this.components = new System.ComponentModel.Container();
            this._container = new System.Windows.Forms.SplitContainer();
            this._imageBox = new Emgu.CV.UI.ImageBox();
            this._imageToBox = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this._container)).BeginInit();
            this._container.Panel1.SuspendLayout();
            this._container.Panel2.SuspendLayout();
            this._container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._imageToBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _container
            // 
            this._container.Dock = System.Windows.Forms.DockStyle.Fill;
            this._container.Location = new System.Drawing.Point(0, 0);
            this._container.Name = "_container";
            // 
            // _container.Panel1
            // 
            this._container.Panel1.Controls.Add(this._imageBox);
            // 
            // _container.Panel2
            // 
            this._container.Panel2.Controls.Add(this._imageToBox);
            this._container.Size = new System.Drawing.Size(1349, 695);
            this._container.SplitterDistance = 666;
            this._container.TabIndex = 3;
            // 
            // _imageBox
            // 
            this._imageBox.Dock = System.Windows.Forms.DockStyle.Left;
            this._imageBox.Location = new System.Drawing.Point(0, 0);
            this._imageBox.Name = "_imageBox";
            this._imageBox.Padding = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this._imageBox.Size = new System.Drawing.Size(650, 695);
            this._imageBox.TabIndex = 2;
            this._imageBox.TabStop = false;
            // 
            // _imageToBox
            // 
            this._imageToBox.Dock = System.Windows.Forms.DockStyle.Right;
            this._imageToBox.Location = new System.Drawing.Point(0, 0);
            this._imageToBox.Name = "_imageToBox";
            this._imageToBox.Padding = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this._imageToBox.Size = new System.Drawing.Size(650, 695);
            this._imageToBox.TabIndex = 2;
            this._imageToBox.TabStop = false;
            // 
            // WindowsDetector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 695);
            this.Controls.Add(this._container);
            this.Name = "WindowsDetector";
            this.Text = "Windows Detection";
            this._container.Panel1.ResumeLayout(false);
            this._container.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._container)).EndInit();
            this._container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._imageToBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

    }
}

