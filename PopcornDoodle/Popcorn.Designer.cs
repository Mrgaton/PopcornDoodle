using PopcornDoodle.Properties;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace PopcornDoodle
{
    partial class Popcorn
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>

        public class TransparentPanel : Panel
        {
            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                    return cp;
                }
            }
       
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.maskPictureBox = new System.Windows.Forms.PictureBox();
            this.transparentPanel = new PopcornDoodle.Popcorn.TransparentPanel();
            ((System.ComponentModel.ISupportInitialize)(this.maskPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // maskPictureBox
            // 
            this.maskPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.maskPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maskPictureBox.ErrorImage = null;
            this.maskPictureBox.Location = new System.Drawing.Point(0, 0);
            this.maskPictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.maskPictureBox.Name = "maskPictureBox";
            this.maskPictureBox.Size = new System.Drawing.Size(795, 906);
            this.maskPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.maskPictureBox.TabIndex = 0;
            this.maskPictureBox.TabStop = false;
            this.maskPictureBox.Visible = false;
            // 
            // transparentPanel
            // 
            this.transparentPanel.BackColor = System.Drawing.Color.Transparent;
            this.transparentPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.transparentPanel.ForeColor = System.Drawing.Color.Transparent;
            this.transparentPanel.Location = new System.Drawing.Point(0, 0);
            this.transparentPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.transparentPanel.Name = "transparentPanel";
            this.transparentPanel.Size = new System.Drawing.Size(795, 33);
            this.transparentPanel.TabIndex = 1;
            // 
            // Popcorn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 906);
            this.Controls.Add(this.transparentPanel);
            this.Controls.Add(this.maskPictureBox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Popcorn";
            this.Text = this.Name;
            ((System.ComponentModel.ISupportInitialize)(this.maskPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox maskPictureBox;
        private TransparentPanel transparentPanel;
    }
}

