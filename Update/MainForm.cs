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
            Process.Toolhelp.KillProcess("POS.exe");            //关闭主程序
            dlgShowmsg = showMessage;
            dlgCloseForm = closeFm;
            Thread th = new Thread(UpdatePos);
            th.IsBackground = true;
            th.Start();
        }


         private void UpdatePos()
        {
            string currentPath = GetCurrentPath();
            //更新程序
            this.Invoke(dlgShowmsg, new string[] { "开始更新...\r\n" });
            CopyDir(currentPath + @"\Updates", currentPath);
            this.Invoke(dlgShowmsg, new string[] { "删除临时文件\r\n" });
            Directory.Delete(currentPath + @"\Updates", true);
            this.Invoke(dlgShowmsg, new string[] { "系统更新完成\r\n正在重新启动..." });
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = currentPath + MAIN_EXE_FILENAME;
            p.Start();
            System.Threading.Thread.Sleep(500);
            this.Invoke(dlgCloseForm, new string[] { });
        }

        /// <summary>
        /// 程序所在路径
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
        /// 将整个文件夹复制到目标文件夹中。
        /// </summary>
        /// <param name="srcPath">源文件夹</param>
        /// <param name="aimPath">目标文件夹</param>
        /// <returns></returns>
        public bool CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;

                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(aimPath))
                    Directory.CreateDirectory(aimPath);

                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法

                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                ArrayList dbUpdateFiles = new ArrayList();

                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                        // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                        if (Directory.Exists(file))
                        {
                            CopyDir(file, aimPath + Path.GetFileName(file));
                        }
                        // 否则直接Copy文件
                        else
                        {
                            File.Copy(file, aimPath + Path.GetFileName(file), true);
                        }
                }

                #region 读取版本号
                string newVer = string.Empty;

                string verPath = GetCurrentPath() + @"\Version.inf";

                StreamReader rd = null;
                try
                {
                    rd = new StreamReader(verPath);
                    newVer = rd.ReadLine();
                    //MessageBox.Show(newVer);
                    this.Invoke(dlgShowmsg, new string[] { "系统当前版本号："+newVer+"\r\n" });
                }
                catch(Exception ex)
                {
                    this.Invoke(dlgShowmsg, new string[] { ex.Message });
                    //MessageBox.Show("读取配置文件错误！");
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