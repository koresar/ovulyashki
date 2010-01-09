using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;

namespace WomenCalendar
{
    public class AppUpdater
    {
        public static void TryUpdate()
        {
            Program.ApplicationForm.BeginInvoke(new MethodInvoker(() => Program.ApplicationForm.DisableUpdate()));
            var bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
        }

        private static void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Program.ApplicationForm.BeginInvoke(new MethodInvoker(() => Program.ApplicationForm.EnableUpdate()));
        }

        private static void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var s = GetFileStream("http://ovulyashki.dp.ua/lastversion/");
                var doc = new XmlDocument();
                doc.Load(s);
                XmlNode rootNode = doc.ChildNodes[1];
                var release = new Release()
                {
                    Date = rootNode["dateadd"].InnerText,
                    FileName = rootNode["filename"].InnerText,
                    Version = rootNode["version"].InnerText,
                    ChangeLog = rootNode["changelog"].InnerText,
                    FileSize = rootNode["filesize"].InnerText,
                    Downloads = rootNode["downloads"].InnerText,
                    Url = rootNode["url"].InnerText
                };
                s.Close();

                if (release.IsItNewVersion())
                {
                    bool updateUs = false;
                    Program.ApplicationForm.Invoke(new MethodInvoker(() =>
                    {
                        updateUs = MessageBox.Show(release.GetFormattedText(), TEXT.Get["Ovulyashki"], MessageBoxButtons.YesNo) == DialogResult.Yes;
                    }));
                    if (updateUs)
                    {
                        DownloadUpdate(release);
                    }
                }
                else
                {
                    Program.ApplicationForm.BeginInvoke(new MethodInvoker(() =>
                    {
                        MessageBox.Show(TEXT.Get["Latest_application"], TEXT.Get["Ovulyashki"]);
                    }));
                }
            }
            catch (Exception ex)
            {
                Program.ApplicationForm.BeginInvoke(new MethodInvoker(() =>
                {
                    MessageBox.Show(TEXT.Get.Format("Unable_to_update", ex.Message), TEXT.Get["Error"]);
                }));
            }
        }

        private static void DownloadUpdate(Release release)
        {
            var tmpFile = Path.Combine(Path.GetTempPath(), release.FileName);
            File.WriteAllBytes(tmpFile, GetFileStream(release.Url).ToArray());
            Process.Start(tmpFile);
        }

        private static MemoryStream GetFileStream(string url)
        {
            byte[] buffer = new byte[4096];

            Stream responseStream = WebRequest.Create(url).GetResponse().GetResponseStream();
            MemoryStream memoryStream = new MemoryStream();

            int count;
            do
            {
                count = responseStream.Read(buffer, 0, buffer.Length);
                memoryStream.Write(buffer, 0, count);
            }
            while (count != 0);

            responseStream.Close();
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        class Release
        {
            public string Date { get; set; }
            public string FileName { get; set; }
            public string Version { get; set; }
            public string ChangeLog { get; set; }
            public string FileSize { get; set; }
            public string Downloads { get; set; }
            public string Url { get; set; }

            public bool IsItNewVersion()
            {
                var newVer = new Version(Version);
                var oldVer = Assembly.GetEntryAssembly().GetName().Version;
                return newVer > oldVer;
            }

            public string SizeInMB()
            {
                int size;
                return int.TryParse(FileSize, out size) ? Math.Round(size / (1024.0 * 1024.0), 2).ToString() : TEXT.Get["Unknown"];
            }

            public string GetFormattedText()
            {
                return TEXT.Get.Format("Update_found_text", Version, Assembly.GetEntryAssembly().GetName().Version.ToString(),
                    Date, SizeInMB(), Downloads, ChangeLog);

            }
        }
    }
}
