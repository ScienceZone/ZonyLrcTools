using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Threading;

namespace ZonyLrcTools.UI
{
    public partial class UI_FeedBack : Form
    {
        public UI_FeedBack()
        {
            InitializeComponent();
        }

        private void button_Submit_Click(object sender, EventArgs e)
        {
            string _sendMessage = buildSurveyMessage();
            new Thread(() => 
            {
                sendSurveyMessage(_sendMessage);
                Close();
            }).Start();
        }

        private string buildSurveyMessage()
        {
            string _type = comboBox_SurveyType.Text;
            string _result = "反馈类型:" + _type + "\r\n" +
                             "反馈内容:" + textBox_SurveyContent.Text + "\r\n" +
                             "联系方式:" + textBox_Contact.Text;
            return _result;
        }

        private void sendSurveyMessage(string content)
        {
            MailMessage _msg = new MailMessage();
            _msg.To.Add("243387971@qq.com");
            _msg.From = new MailAddress("zonyservice@126.com", "Zony客户端反馈", Encoding.UTF8);
            _msg.Subject = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss ZonyLrcTools反馈");
            _msg.SubjectEncoding = Encoding.UTF8;
            _msg.Body = content;
            _msg.BodyEncoding = Encoding.UTF8;
            _msg.IsBodyHtml = false;
            _msg.Priority = MailPriority.High;

            SmtpClient _client = new SmtpClient();
            _client.Host = "smtp.126.com";
            _client.Credentials = new System.Net.NetworkCredential("zonyservice@126.com", "zonyservice123");

            try
            {
                _client.Send(_msg);
                MessageBox.Show("反馈信息提交成功，十分感谢你的反馈意见。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("反馈信息提交失败！\r\n你可以加入QQ群:337656932进行反馈。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
