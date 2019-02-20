namespace Tagging
{
    partial class Test
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picShowImg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picShowImg)).BeginInit();
            this.SuspendLayout();
            // 
            // picShowImg
            // 
            this.picShowImg.Location = new System.Drawing.Point(23, 12);
            this.picShowImg.Name = "picShowImg";
            this.picShowImg.Size = new System.Drawing.Size(400, 412);
            this.picShowImg.TabIndex = 5;
            this.picShowImg.TabStop = false;
            this.picShowImg.Paint += new System.Windows.Forms.PaintEventHandler(this.picShowImg_Paint);
            this.picShowImg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picShowImg_MouseDown);
            this.picShowImg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picShowImg_MouseMove);
            this.picShowImg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picShowImg_MouseUp);
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.picShowImg);
            this.Name = "Test";
            this.Text = "Test";
            this.Load += new System.EventHandler(this.Test_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picShowImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picShowImg;
    }
}