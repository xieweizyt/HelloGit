using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tagging.Document;
using Tagging.Model;

namespace Tagging
{
    public partial class Form1 : Form
    {
        IDocument _document = new DocumentInfo();
        private Double scale = 0;//放大或者缩小的倍率
        List<Rectangle> rectList = new List<Rectangle>();//标记位置列表
        int lvIndex = -1;//记录点击标记的index

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HideImageControl();
            ReLoadTreeNode();

            //把标注列表放最右
            listAnn.Location = new System.Drawing.Point(Screen.GetBounds(this).Width - listAnn.Width - 20, listAnn.Location.Y);

            treeView1.ExpandAll();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(AddNodes(treeView1.Nodes, "未完成任务", "Test"));
            AddTaskFrm addTaskFrm = new AddTaskFrm();
            if (addTaskFrm.ShowDialog() == DialogResult.Cancel)
            {
                ReLoadTreeNode();
            }

            treeView1.ExpandAll();
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="tnodes"></param>
        /// <param name="nodeName">父节点名称</param>
        /// <param name="subNodeName">添加的节点名称</param>
        private string AddNodes(TreeNodeCollection tnodes, string nodeName, string subNodeName, string key = "")
        {
            string Msg = string.Empty;
            foreach (TreeNode node in tnodes)
            {
                if (node.Text == nodeName)
                {
                    int tag = -1;
                    GetNodes(node.Nodes, subNodeName, out tag);
                    if (tag == 1)
                    {
                        //给指定的节点增加子节点
                        node.Nodes.Add(key, subNodeName);
                        Msg = "添加成功";
                        break;
                    }
                    else
                    {
                        Msg = "该节点下已存在此子节点";
                    }
                }
                //ShowNodes(node.Nodes,nodeName,subNodeName);
            }

            return Msg;

        }

        /// <summary>
        /// 查找指定节点是否包含某个子节点
        /// </summary>
        /// <param name="tnodes"></param>
        /// <param name="nodeName"></param>
        /// <param name="tag">有返回0 无返回1</param>
        /// <returns></returns>
        private TreeNode GetNodes(TreeNodeCollection tnodes, string nodeName, out int tag)
        {
            foreach (TreeNode node in tnodes)
            {
                if (node.Text == nodeName)
                {
                    tag = 0;
                    return node;
                }
                //ShowNodes(node.Nodes, nodeName, tag);
            }
            tag = 1;
            return new TreeNode();
        }

        /// <summary>
        /// 导入图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImport_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node != null && node.Level == 1)
            {

                if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileNames.Length > 0)
                {
                    List<string> pathList = new List<string>();
                    List<string> nameList = new List<string>();
                    for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                    {
                        pathList.Add(openFileDialog.FileNames[i]);
                        nameList.Add(openFileDialog.SafeFileNames[i]);
                    }

                    for (int i = 0; i < nameList.Count; i++)
                    {
                        //判断是否存在
                        if (!DocumentConfig.MainDocuments[node.Text].ImageList.ContainsKey(nameList[i]))
                        {
                            //复制文件
                            _document.AddFile(pathList[i], $"{DocumentConfig._Path}\\{node.Text}\\{nameList[i]}");

                            //读取图片属性
                            Image img = Image.FromFile($"{DocumentConfig._Path}\\{node.Text}\\{nameList[i]}");
                            DocumentConfig.MainDocuments[node.Text].ImageList.Add(nameList[i], new ImageDocument() { Class = DocumentConfig.MainDocuments[node.Text].Class, Type = DocumentConfig.MainDocuments[node.Text].Type, Channels = DocumentConfig.MainDocuments[node.Text].Channels, DType = DocumentConfig.MainDocuments[node.Text].DType, FileName = nameList[i], Width = img.Width, Height = img.Height });
                        }
                    }
                }

                ReLoadTreeNode();
            }
            else
            {
                MessageBox.Show("请选择正确的任务节点导入");
            }

            treeView1.ExpandAll();

        }

        /// <summary>
        /// 节点点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode treeNode = treeView1.SelectedNode;
                Rectangle ScreenArea = System.Windows.Forms.Screen.GetBounds(this);
                int winWidth = (int)Math.Round(ScreenArea.Width * 0.7); //屏幕宽度
                if (!string.IsNullOrEmpty(treeNode.Name))
                {
                    ShowImageControl();

                    Image img = Image.FromFile(treeNode.Name);
                    picShowImg.SizeMode = PictureBoxSizeMode.Zoom;
                    picShowImg.Width = winWidth;
                    scale = Convert.ToDouble(winWidth) / Convert.ToDouble(img.Width);
                    double height = scale * Convert.ToDouble(img.Height);
                    picShowImg.Height = (int)Math.Round(height, 0);
                    picShowImg.Image = img;
                }

                //给标注列表填充值
                List<ItemDocument> itemDocuments = DocumentConfig.MainDocuments[treeNode.Parent.Text].ImageList[treeNode.Text].Annotations;

                rectList = new List<Rectangle>();
                this.listAnn.Items.Clear();
                if (itemDocuments.Count > 0)
                {
                    foreach (ItemDocument item in itemDocuments)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = item.Class;
                        this.listAnn.Items.Add(lvi);

                        //添加历史标记值
                        rectList.Add(GetRectangle(item.X_Min, item.Y_Min, item.X_Max, item.Y_Max));
                    }
                }

                picShowImg.Refresh();

            }
            catch (Exception exception)
            {

            }
        }


        private Point m_ptStart = new Point(0, 0);
        private Point m_ptEnd = new Point(0, 0);
        // true: MouseUp or false: MouseMove 


        Point start; //画框的起始点
        Point end;//画框的结束点<br>
        bool blnDraw;//判断是否绘制<br>
        Rectangle rect;
        private bool m_bMouseDown = false;
        private void picShowImg_MouseDown(object sender, MouseEventArgs e)
        {
            start = e.Location;
            Invalidate();
            blnDraw = true;
        }

        private void picShowImg_MouseMove(object sender, MouseEventArgs e)
        {
            //textBox1.Text = $"{start.X}----{start.Y}";
            //textBox2.Text = $"{end.X}----{end.Y}";
            if (blnDraw)
            {
                if (e.Button != MouseButtons.Left)//判断是否按下左键
                    return;
                Point tempEndPoint = e.Location; //记录框的位置和大小
                rect.Location = new Point(
                Math.Min(start.X, tempEndPoint.X),
                Math.Min(start.Y, tempEndPoint.Y));
                rect.Size = new Size(
                Math.Abs(start.X - tempEndPoint.X),
                Math.Abs(start.Y - tempEndPoint.Y));
                picShowImg.Invalidate();
            }
        }

        private void picShowImg_MouseUp(object sender, MouseEventArgs e)
        {
            end = e.Location;
            blnDraw = false; //结束绘制
        }

        private void picShowImg_Paint(object sender, PaintEventArgs e)
        {
            if (blnDraw)
            {
                if (picShowImg.Image != null)
                {
                    if (rect != null && rect.Width > 0 && rect.Height > 0)
                    {
                        e.Graphics.DrawRectangle(new Pen(Color.Red, 1), rect);//重新绘制颜色为红色
                    }
                }
            }

            if (rectList.Count > 0)
            {
                for (int i = 0; i < rectList.Count; i++)
                {
                    if(i==lvIndex)
                    {
                        e.Graphics.DrawRectangle(new Pen(Color.Blue, 1), rectList[i]);//重新绘制颜色为蓝色
                        continue;
                    }
                    e.Graphics.DrawRectangle(new Pen(Color.Red, 1), rectList[i]);//重新绘制颜色为红色
                }
            }
        }

        /// <summary>
        /// 重新加载节点
        /// </summary>
        private void ReLoadTreeNode()
        {
            treeView1.Nodes[0].Nodes.Clear();
            List<string> DocumentList;
            foreach (string item in DocumentConfig.MainDocuments.Keys)
            {
                AddNodes(treeView1.Nodes, "任务列表", item);
                foreach (string documentItem in DocumentConfig.MainDocuments[item].ImageList.Keys)
                {
                    if (!documentItem.Contains("json"))
                    {
                        AddNodes(treeView1.Nodes[0].Nodes, item, documentItem, $"{DocumentConfig._Path}\\{item}\\{documentItem}");
                    }
                }
            }
        }

        private void btnAddann_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (!string.IsNullOrEmpty(this.txtAddann.Text) && node != null)
            {
                if (!DocumentConfig.MainDocuments[node.Parent.Text].ImageList.ContainsKey(this.txtAddann.Text))
                    DocumentConfig.MainDocuments[node.Parent.Text].ImageList[node.Text].Annotations.Add(new ItemDocument() { Class = this.txtAddann.Text, Utf8_str = this.txtutf8_str.Text, X_Min = Math.Round(start.X / scale), Y_Min = Math.Round(start.Y / scale), X_Max = Math.Round(end.X / scale), Y_Max = Math.Round(end.Y / scale) });

                //给标注列表新增数据
                ListViewItem lvi = new ListViewItem();
                lvi.Text = txtAddann.Text;
                this.listAnn.Items.Add(lvi);
                rectList.Add(GetRectangle(Math.Round(start.X / scale), Math.Round(start.Y / scale), Math.Round(end.X / scale),  Math.Round(end.Y / scale)));
            }
            else
            {
                MessageBox.Show("请输入Annotations");
            }

            txtAddann.Text = "";
            txtutf8_str.Text = "";
            picShowImg.Refresh();
        }

        private void BtnJson_Click(object sender, EventArgs e)
        {
            try
            {
                bool isImport = true;
                TreeNode node = treeView1.SelectedNode;
                foreach (ImageDocument item in DocumentConfig.MainDocuments[node.Parent.Text].ImageList.Values)
                {
                    if (item.Annotations.Count == 0)
                    {
                        if (MessageBox.Show($"{item.FileName}图片没有标注，是否继续导出", "导出提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                        {
                            isImport = false;
                        }
                    }
                }
                //写文件
                if (isImport)
                {
                    foreach (ImageDocument item in DocumentConfig.MainDocuments[node.Parent.Text].ImageList.Values)
                    {
                        string jsonstr = JsonConvert.SerializeObject(item);

                        if (!File.Exists($"{DocumentConfig._Path}\\{node.Parent.Text}\\{(item.FileName.Contains(".") ? item.FileName.Substring(0, item.FileName.IndexOf('.')) : item.FileName)}.json"))  // 判断是否已有相同文件 
                        {
                            FileStream fs1 = new FileStream($"{DocumentConfig._Path}\\{node.Parent.Text}\\{(item.FileName.Contains(".") ? item.FileName.Substring(0, item.FileName.IndexOf('.')) : item.FileName)}.json", FileMode.Create, FileAccess.ReadWrite);
                            fs1.Close();
                        }
                        File.WriteAllText($"{DocumentConfig._Path}\\{node.Parent.Text}\\{(item.FileName.Contains(".") ? item.FileName.Substring(0, item.FileName.IndexOf('.')) : item.FileName)}.json", jsonstr);
                    }

                    MessageBox.Show("导出成功");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("导出失败，请重新导出");
            }
                    
        }

        private void ShowImageControl()
        {
            label1.Show();
            label2.Show();
            txtAddann.Show();
            txtutf8_str.Show();
            btnAddann.Show();
            BtnJson.Show();
            listAnn.Show();
            btnDel.Show();
        }
        private void HideImageControl()
        {
            label1.Hide();
            label2.Hide();
            txtAddann.Hide();
            txtutf8_str.Hide();
            btnAddann.Hide();
            BtnJson.Hide();
            listAnn.Hide();
            btnDel.Hide();
        }


        private Rectangle GetRectangle(double min_x, double min_y, double max_x, double max_y)
        {
            Rectangle rect = new Rectangle();
            rect.Location = new Point((int)(Math.Round(min_x * scale)), (int)(Math.Round(min_y * scale)));
            rect.Size = new Size((int)(Math.Round(max_x * scale)) - (int)(Math.Round(min_x * scale)), (int)(Math.Round(max_y * scale)) - (int)(Math.Round(min_y * scale)));
            //e.Graphics.DrawRectangle(new Pen(Color.Red, 1), rect);//重新绘制颜色为红色
            return rect;
        }

        /// <summary>
        /// 删除标记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            TreeNode treeNode = treeView1.SelectedNode;
            List<ItemDocument> itemDocuments = DocumentConfig.MainDocuments[treeNode.Parent.Text].ImageList[treeNode.Text].Annotations;
            
            foreach (ListViewItem lvi in listAnn.SelectedItems)  //选中项遍历
            {
                //显示列表移除
                listAnn.Items.RemoveAt(lvi.Index);

                //标记位置列表移除
                rectList = new List<Rectangle>();

                //内存字典数据删除
                ItemDocument delDocument = new ItemDocument();
                foreach (ItemDocument item in itemDocuments)
                {
                    if (item.Class == lvi.Text)
                        delDocument = item;
                    else
                        rectList.Add(GetRectangle(item.X_Min, item.Y_Min, item.X_Max, item.Y_Max));
                }
                DocumentConfig.MainDocuments[treeNode.Parent.Text].ImageList[treeNode.Text].Annotations.Remove(delDocument);

            }

            picShowImg.Refresh();
        }

        private void listAnn_Click(object sender, EventArgs e)
        {
            lvIndex = listAnn.SelectedItems[0].Index;
            picShowImg.Refresh();
        }
    }
}
