using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WomenCalendar
{
    /// <summary>
    /// The class tries to download and run new application installer.
    /// </summary>
    public class AppUpdater
    {
        /// <summary>
        /// On the backgound checks if new application release is available. Interacts with user asking question.
        /// Download and run installtion package.
        /// </summary>
        public static void TryUpdate()
        {
            Program.ApplicationForm.BeginInvoke(new MethodInvoker(() => Program.ApplicationForm.DisableUpdate()));
            var bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(CallbackDoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CallbackRunWorkerCompleted);
            bw.RunWorkerAsync();
        }

        private static void CallbackRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Program.ApplicationForm.BeginInvoke(new MethodInvoker(() => Program.ApplicationForm.EnableUpdate()));
        }

        private static void CallbackDoWork(object sender, DoWorkEventArgs e)
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

        /// <summary>
        /// Represents the application release information.
        /// </summary>
        private class Release
        {
            internal string Date { get; set; }

            internal string FileName { get; set; }

            internal string Version { get; set; }

            internal string ChangeLog { get; set; }

            internal string FileSize { get; set; }

            internal string Downloads { get; set; }

            internal string Url { get; set; }

            internal bool IsItNewVersion()
            {
                var newVer = new Version(this.Version);
                var oldVer = Assembly.GetEntryAssembly().GetName().Version;
                return newVer > oldVer;
            }

            internal string SizeInMB()
            {
                int size;
                return int.TryParse(this.FileSize, out size) ? Math.Round(size / (1024.0 * 1024.0), 2).ToString() : TEXT.Get["Unknown"];
            }

            internal string GetFormattedText()
            {
                return TEXT.Get.Format(
                    "Update_found_text", 
                    this.Version, 
                    Assembly.GetEntryAssembly().GetName().Version.ToString(),
                    this.Date, 
                    this.SizeInMB(), 
                    this.Downloads, 
                    this.ChangeLog);
            }
        }
    }
}
