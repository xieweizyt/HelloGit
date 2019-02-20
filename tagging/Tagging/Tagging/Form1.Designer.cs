namespace Tagging
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("任务列表");
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnImport = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.picShowImg = new System.Windows.Forms.PictureBox();
            this.txtAddann = new System.Windows.Forms.TextBox();
            this.btnAddann = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtutf8_str = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnJson = new System.Windows.Forms.Button();
            this.listAnn = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picShowImg)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode4.Name = "节点0";
            treeNode4.Text = "任务列表";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeView1.Size = new System.Drawing.Size(205, 412);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Location = new System.Drawing.Point(12, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(205, 412);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "新建任务";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnImport
            // 
            this.BtnImport.Location = new System.Drawing.Point(112, 7);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(75, 23);
            this.BtnImport.TabIndex = 1;
            this.BtnImport.Text = "导入图片";
            this.BtnImport.UseVisualStyleBackColor = true;
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Multiselect = true;
            // 
            // picShowImg
            // 
            this.picShowImg.Location = new System.Drawing.Point(232, 36);
            this.picShowImg.Name = "picShowImg";
            this.picShowImg.Size = new System.Drawing.Size(400, 412);
            this.picShowImg.TabIndex = 4;
            this.picShowImg.TabStop = false;
            this.picShowImg.Paint += new System.Windows.Forms.PaintEventHandler(this.picShowImg_Paint);
            this.picShowImg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picShowImg_MouseDown);
            this.picShowImg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picShowImg_MouseMove);
            this.picShowImg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picShowImg_MouseUp);
            // 
            // txtAddann
            // 
            this.txtAddann.Location = new System.Drawing.Point(348, 9);
            this.txtAddann.Name = "txtAddann";
            this.txtAddann.Size = new System.Drawing.Size(153, 21);
            this.txtAddann.TabIndex = 2;
            // 
            // btnAddann
            // 
            this.btnAddann.Location = new System.Drawing.Point(803, 7);
            this.btnAddann.Name = "btnAddann";
            this.btnAddann.Size = new System.Drawing.Size(75, 23);
            this.btnAddann.TabIndex = 4;
            this.btnAddann.Text = "确定";
            this.btnAddann.UseVisualStyleBackColor = true;
            this.btnAddann.Click += new System.EventHandler(this.btnAddann_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(266, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "annotations:";
            // 
            // txtutf8_str
            // 
            this.txtutf8_str.Location = new System.Drawing.Point(607, 9);
            this.txtutf8_str.Name = "txtutf8_str";
            this.txtutf8_str.Size = new System.Drawing.Size(153, 21);
            this.txtutf8_str.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(542, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "utf8_str:";
            // 
            // BtnJson
            // 
            this.BtnJson.Location = new System.Drawing.Point(1015, 7);
            this.BtnJson.Name = "BtnJson";
            this.BtnJson.Size = new System.Drawing.Size(75, 23);
            this.BtnJson.TabIndex = 5;
            this.BtnJson.Text = "导出Json";
            this.BtnJson.UseVisualStyleBackColor = true;
            this.BtnJson.Click += new System.EventHandler(this.BtnJson_Click);
            // 
            // listAnn
            // 
            this.listAnn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listAnn.Location = new System.Drawing.Point(1047, 36);
            this.listAnn.Name = "listAnn";
            this.listAnn.Size = new System.Drawing.Size(121, 284);
            this.listAnn.TabIndex = 9;
            this.listAnn.UseCompatibleStateImageBehavior = false;
            this.listAnn.View = System.Windows.Forms.View.Details;
            this.listAnn.Click += new System.EventHandler(this.listAnn_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Annotations";
            this.columnHeader1.Width = 150;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(912, 7);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 10;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 469);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.listAnn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnJson);
            this.Controls.Add(this.btnAddann);
            this.Controls.Add(this.txtutf8_str);
            this.Controls.Add(this.txtAddann);
            this.Controls.Add(this.picShowImg);
            this.Controls.Add(this.BtnImport);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picShowImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnImport;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox picShowImg;
        private System.Windows.Forms.TextBox txtAddann;
        private System.Windows.Forms.Button btnAddann;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtutf8_str;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnJson;
        private System.Windows.Forms.ListView listAnn;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnDel;
    }
}

