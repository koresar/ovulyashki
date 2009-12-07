using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Windows.Forms;

namespace WomenCalendar
{
    public class TEXT
    {
        const string defaultLang = "en";
        const string langFileEnd = "_lang.xml";
        const string noValueText = "[ERROR! NO TEXT]";

        private Dictionary<string, string> translations;
        private Dictionary<string, string> defaultText;

        public TEXT(Dictionary<string, string> defaultText, Dictionary<string, string> translations)
        {
            this.defaultText = defaultText;
            this.translations = translations;
        }

        public string this[string id]
        {
            get
            {
                string val;
                return
                    Get.translations.TryGetValue(id, out val) ? val : // search in translations
                    Get.defaultText.TryGetValue(id, out val) ? val : // search in default
                    noValueText + id; // return ERROR text
            }
        }

        public string Format(string id, params object[] parameters)
        {
            string val;
            return
                Get.translations.TryGetValue(id, out val) ? string.Format(val, parameters) : // search in translations
                Get.defaultText.TryGetValue(id, out val) ? string.Format(val, parameters) : // search in default
                noValueText + id; // return ERROR text
        }

        public static TEXT Get { get; private set; }

        public static string GetDaysString(int days)
        {
            try
            {
                string defaultDaysPlural = string.Empty;
                var tokens = new List<string>(TEXT.Get["Days_plural"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                var dic = new Dictionary<int, string>();
                foreach (var item in tokens)
                {
                    int separatorIndex = item.IndexOf(':');
                    var firstHalf = item.Substring(0, separatorIndex);
                    int number;
                    if (!int.TryParse(firstHalf, out number) && firstHalf == "*")
                    {
                        defaultDaysPlural = item.Substring(separatorIndex + 1);
                    }
                    dic[number] = item.Substring(separatorIndex + 1);
                }

                if (dic.ContainsKey(days)) return dic[days];
                else return defaultDaysPlural;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string SystemLang
        {
            get { return CultureInfo.InstalledUICulture.TwoLetterISOLanguageName.ToLower(); }
        }

        private static string LocalPath
        {
            get { return Path.GetDirectoryName(Application.ExecutablePath); }
        }

        public static Dictionary<string, string> FindAllLangFiles()
        {
            var langFiles = new Dictionary<string, string>();
            foreach (var f in Directory.GetFiles(LocalPath))
            { // Directory.GetFiles(string, string) have well known bug, so we have to filer files ourselves
                var fileName = Path.GetFileName(f).ToLower();
                if (fileName.EndsWith(langFileEnd, StringComparison.OrdinalIgnoreCase) &&
                    fileName.Length == defaultLang.Length + langFileEnd.Length)
                {
                    langFiles[fileName.Substring(0, 2).ToLower()] = f;
                }
            }
            return langFiles;
        }

        private static void ApplyDictionaries(
            Dictionary<string, string> defaultDic, Dictionary<string, string> translations,
            string translationsLang)
        {
            Get = new TEXT(defaultDic, translations);
            Program.Settings.ApplicationLanguage = translationsLang;
            try
            {
                foreach (var ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
                {
                    if (ci.TwoLetterISOLanguageName == translationsLang)
                    {
                        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
                        System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                        break;
                    }
                }
            }
            catch { }
        }

        public static bool ApplyLanguageFile(string langCode, string langFile)
        {
            Dictionary<string, string> translations;

            // load translations language file. It may not be loaded, in this case we should not apply it.
            if (LoadLanguageFile(langFile, out translations))
            {
                ApplyDictionaries(Get.defaultText, translations, langCode);
                return true;
            }

            return false;
        }

        public static void InitializeLanguage(string needLang)
        {
            var langFiles = FindAllLangFiles();

            // On the first application start no actual language assigned.
            if (string.IsNullOrEmpty(needLang))
            {
                // Try to use system lang.
                if (langFiles.ContainsKey(SystemLang))
                {
                    needLang = SystemLang;
                }
                else
                { // nope, there is no such translation. Use default language.
                    needLang = defaultLang;
                }
            }

            needLang = needLang.ToLower();
            if (langFiles.Count == 0) throw new Exception("No language files files in the allication folder " + LocalPath);

            Dictionary<string, string> defaultTexts;
            Dictionary<string, string> translations;
            string usedLang;

            // loading default language file. It must be always present and loaded.
            if (!langFiles.ContainsKey(defaultLang))
            {
                throw new Exception("Default language file " + defaultLang + langFileEnd +
                    " was not found in the folder " + LocalPath);
            }
            else
            {
                if (!LoadLanguageFile(langFiles[defaultLang], out defaultTexts))
                {
                    throw new Exception("Unable to load default language file " + defaultLang + langFileEnd +
                        " from the folder " + LocalPath);
                }
                // language file loaded normally.
            }

            // now load translations language file. It may not be loaded, in this case we will use default one
            if (langFiles.ContainsKey(needLang) && LoadLanguageFile(langFiles[needLang], out translations))
            {
                // language file loaded normally.
                usedLang = needLang;
            }
            else
            {
                // was not loaded. Use default instead.
                translations = defaultTexts;
                usedLang = defaultLang;
            }

            ApplyDictionaries(defaultTexts, translations, usedLang);
        }

        private static bool LoadLanguageFile(string fileToLoad, out Dictionary<string, string> texts)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(fileToLoad);
                var dic = new Dictionary<string, string>();
                foreach (XmlNode item in doc.ChildNodes[1])
                {
                    if (item.Attributes == null) continue;
                    var id = item.Attributes["id"].Value;
                    if (!string.IsNullOrEmpty(id))
                    {
                        dic.Add(id, item.InnerText);
                    }
                }
                texts = dic;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load language file " + fileToLoad + " due to:\n" + ex.Message);
                texts = null;
                return false;
            }
        }
    }
}
