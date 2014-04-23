using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections;

namespace Update
{
    public partial class MainForm : Form
    {
        const string MAIN_EXE_FILENAME = @"\POS.exe";
        delegate void DlgShowMsg(string str);
        delegate void DlgCloseForm();
        DlgShowMsg dlgShowmsg;
        DlgCloseForm dlgCloseForm;
        public MainForm()
        {
            InitializeComponent();
        }

        private void showMessage(string str)
        {
            label1.Text += str;
            Application.DoEvents();
        }
        private void closeFm()
        {
            this.Close();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            Thread.Sleep(100);
            Process.Toolhelp.KillProcess("POS.exe");            //�ر�������
            dlgShowmsg = showMessage;
            dlgCloseForm = closeFm;
            Thread th = new Thread(UpdatePos);
            th.IsBackground = true;
            th.Start();
        }


         private void UpdatePos()
        {
            string currentPath = GetCurrentPath();
            //���³���
            this.Invoke(dlgShowmsg, new string[] { "��ʼ����...\r\n" });
            CopyDir(currentPath + @"\Updates", currentPath);
            this.Invoke(dlgShowmsg, new string[] { "ɾ����ʱ�ļ�\r\n" });
            Directory.Delete(currentPath + @"\Updates", true);
            this.Invoke(dlgShowmsg, new string[] { "ϵͳ�������\r\n������������..." });
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = currentPath + MAIN_EXE_FILENAME;
            p.Start();
            System.Threading.Thread.Sleep(500);
            this.Invoke(dlgCloseForm, new string[] { });
        }

        /// <summary>
        /// ��������·��
        /// </summary>
        private static string GetCurrentPath()
        {

            string m_CurrentPath = "";
            string Platform = Environment.OSVersion.Platform.ToString();
            if (Platform.Equals("WinCE"))
            {
                m_CurrentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            }
            else if (Platform.Equals("Win32NT"))
            {
                m_CurrentPath = Directory.GetCurrentDirectory();
            }

            return m_CurrentPath;
        }

        /// <summary>
        /// �������ļ��и��Ƶ�Ŀ���ļ����С�
        /// </summary>
        /// <param name="srcPath">Դ�ļ���</param>
        /// <param name="aimPath">Ŀ���ļ���</param>
        /// <returns></returns>
        public bool CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // ���Ŀ��Ŀ¼�Ƿ���Ŀ¼�ָ��ַ�����������������֮
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;

                // �ж�Ŀ��Ŀ¼�Ƿ����������������½�֮
                if (!Directory.Exists(aimPath))
                    Directory.CreateDirectory(aimPath);

                // �õ�ԴĿ¼���ļ��б��������ǰ����ļ��Լ�Ŀ¼·����һ������
                // �����ָ��copyĿ���ļ�������ļ���������Ŀ¼��ʹ������ķ���

                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                ArrayList dbUpdateFiles = new ArrayList();

                // �������е��ļ���Ŀ¼
                foreach (string file in fileList)
                {
                        // �ȵ���Ŀ¼��������������Ŀ¼�͵ݹ�Copy��Ŀ¼������ļ�
                        if (Directory.Exists(file))
                        {
                            CopyDir(file, aimPath + Path.GetFileName(file));
                        }
                        // ����ֱ��Copy�ļ�
                        else
                        {
                            File.Copy(file, aimPath + Path.GetFileName(file), true);
                        }
                }

                #region ��ȡ�汾��
                string newVer = string.Empty;

                string verPath = GetCurrentPath() + @"\Version.inf";

                StreamReader rd = null;
                try
                {
                    rd = new StreamReader(verPath);
                    newVer = rd.ReadLine();
                    //MessageBox.Show(newVer);
                    this.Invoke(dlgShowmsg, new string[] { "ϵͳ��ǰ�汾�ţ�"+newVer+"\r\n" });
                }
                catch(Exception ex)
                {
                    this.Invoke(dlgShowmsg, new string[] { ex.Message });
                    //MessageBox.Show("��ȡ�����ļ�����");
                }
                finally
                {
                    rd.Close();
                }
                #endregion

                return true;
            }
            catch(Exception ex)
            {
                this.Invoke(dlgShowmsg, new string[] { ex.Message});
                return false;
            }
        }
    }
}