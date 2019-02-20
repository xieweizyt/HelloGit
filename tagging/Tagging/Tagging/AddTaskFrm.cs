using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tagging.Document;
using Tagging.Model;
using System.IO;
using Newtonsoft.Json;

namespace Tagging
{
    public partial class AddTaskFrm : Form
    {
        public AddTaskFrm()
        {
            InitializeComponent();
        }

        private void txtClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!DocumentConfig.MainDocuments.ContainsKey(this.txtClass.Text))
            {
                //添加任务到任务列表
                DocumentConfig.MainDocuments.Add(this.txtClass.Text, new TaskCls() { Class = this.txtClass.Text, Channels = this.txtChannels.Text, DType = this.txtDtype.Text, Type = this.txtType.Text });

                if(!Directory.Exists($"{DocumentConfig._Path}\\{this.txtClass.Text}"))
                    Directory.CreateDirectory($"{DocumentConfig._Path}\\{this.txtClass.Text}");

                //写文件
                if (!File.Exists($"{DocumentConfig._Path}\\{this.txtClass.Text}.json"))  // 判断是否已有相同文件 
                {
                    FileStream fs1 = new FileStream($"{DocumentConfig._Path}\\{this.txtClass.Text}.json", FileMode.Create, FileAccess.ReadWrite);
                    fs1.Close();
                }
                File.WriteAllText($"{DocumentConfig._Path}\\{this.txtClass.Text}.json", JsonConvert.SerializeObject(new{ Class = this.txtClass.Text, Channels = this.txtChannels.Text, DType = this.txtDtype.Text, Type = this.txtType.Text }));

                
            }
            this.Close();
        }
    }
}
