using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ZonyLrcTools.Untils;

namespace ZonyLrcTools.UI
{
    public partial class UI_FunctionTest : Form
    {
        public UI_FunctionTest()
        {
            InitializeComponent();
        }

        private void UI_FunctionTest_Load(object sender, EventArgs e)
        {
            var fs = new FileStream(@"F:\双星情缘.txt", FileMode.OpenOrCreate);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();
            List<byte[]> _list = new List<byte[]>();
            for(int i=0;i<1000;i++)
            {
                _list.Add(data);
            }
            Parallel.ForEach(_list,new ParallelOptions() { MaxDegreeOfParallelism = 4},(item)=> 
            {
                FileUtils.WriteFile(@"F:\" + DateTime.Now.ToString("yyyyMMddHHmmss"), item);
            });
        }
    }
}
