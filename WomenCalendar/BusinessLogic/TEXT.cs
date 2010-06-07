using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WomenCalendar
{
    /// <summary>
    /// The container of all of the translations. Also it rules the internationalization stuff like
    /// correct file choosing, reading it, etc.
    /// </summary>
    public class TEXT
    {
        private const string DefaultLang = "en";
        private const string LangFileEnd = "_lang.xml";
        private const string NoValueText = "[ERROR! NO TEXT]";

        private Dictionary<string, string> translations;
        private Dictionary<string, string> defaultText;

        /// <summary>
        /// Creates the translation container.
        /// </summary>
        /// <param name="defaultText">The list of translations which must have all of the possible items. 
        /// Used when main language file did not found the item key.</param>
        /// <param name="translations">The main translation items. Used for the application interface.</param>
        public TEXT(Dictionary<string, string> defaultText, Dictionary<string, string> translations)
        {
            this.defaultText = defaultText;
            this.translations = translations;
        }

        /// <summary>
        /// The main object the code is using to find translation. The general usage is: TEXT.Get["phrase_id"]
        /// </summary>
        public static TEXT Get { get; private set; }

        private static string SystemLang
        {
            get { return CultureInfo.InstalledUICulture.TwoLetterISOLanguageName.ToLower(); }
        }

        private static string LocalPath
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
        }

        /// <summary>
        /// Find and return the translated text by its id. This is the major thing the code is using widely.
        /// </summary>
        /// <param name="id">The phrase id.</param>
        /// <returns>Translation of the phrase.</returns>
        public string this[string id]
        {
            get
            {
                string val;
                return
                    Get.translations.TryGetValue(id, out val) ? val : // search in translations
                    Get.defaultText.TryGetValue(id, out val) ? val : // search in default
                    NoValueText + id; // return ERROR text
            }
        }

        /// <summary>
        /// <para>Returns plural "days" text. Used widely on the interface. 
        /// The format of the XML tag is following:</para>
        /// <para>NUMBER:TRANSLATION,NUMBER:TRANSLATION</para>
        /// <para>One of the numbers must be "*" - this indicates default transaltion. See the example:</para>
        /// <example>1:день,2:дня,3:дня,4:дня,21:день,22:дня,23:дня,24:дня,31:день,32:дня,33:дня,34:дня,41:день,
        /// 42:дня,43:дня,44:дня,51:день,52:дня,53:дня,54:дня,*:дней</example>
        /// </summary>
        /// <param name="days">The days amount to be translated.</param>
        /// <returns>Trnslated plural number of given days.</returns>
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

                return dic.ContainsKey(days) ? dic[days] : defaultDaysPlural;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Search for all available language files. Used to create list of of options for user to choose.
        /// </summary>
        /// <returns>Map of Language->It's file.</returns>
        public static Dictionary<string, string> FindAllLangFiles()
        {
            var langFiles = new Dictionary<string, string>();
            foreach (var f in Directory.GetFiles(LocalPath))
            { // Directory.GetFiles(string, string) have well known bug, so we have to filer files ourselves
                var fileName = Path.GetFileName(f).ToLower();
                if (fileName.EndsWith(LangFileEnd, StringComparison.OrdinalIgnoreCase) &&
                    fileName.Length == DefaultLang.Length + LangFileEnd.Length)
                {
                    langFiles[fileName.Substring(0, 2).ToLower()] = f;
                }
            }

            return langFiles;
        }

        /// <summary>
        /// Apply a language to the whole application.
        /// </summary>
        /// <param name="langCode">Language 2 letters code.</param>
        /// <param name="langFile">Language XML file.</param>
        /// <returns>True applied. Otherwise - false.</returns>
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

        /// <summary>
        /// Initialize default language. Used on the application start.
        /// </summary>
        /// <param name="needLang">The preffered language.</param>
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
                    needLang = DefaultLang;
                }
            }

            needLang = needLang.ToLower();
            if (langFiles.Count == 0)
            {
                throw new TranslationException("No language files files in the allication folder " + LocalPath);
            }

            Dictionary<string, string> defaultTexts;
            Dictionary<string, string> translations;
            string usedLang;

            // loading default language file. It must be always present and loaded.
            if (!langFiles.ContainsKey(DefaultLang))
            {
                throw new TranslationException("Default language file " + DefaultLang + LangFileEnd +
                    " was not found in the folder " + LocalPath);
            }
            else
            {
                if (!LoadLanguageFile(langFiles[DefaultLang], out defaultTexts))
                {
                    throw new TranslationException("Unable to load default language file " + DefaultLang + LangFileEnd +
                        " from the folder " + LocalPath);
                } // language file loaded normally.                
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
                usedLang = DefaultLang;
            }

            ApplyDictionaries(defaultTexts, translations, usedLang);
        }

        /// <summary>
        /// Returns translated text but using standard string.Format() syntax for text formatting.
        /// </summary>
        /// <param name="id">The phrase id we need to find translation for.</param>
        /// <param name="parameters">The parameters of the string formating call.</param>
        /// <returns>Translated text, or error text if not found.</returns>
        public string Format(string id, params object[] parameters)
        {
            string val;
            return
                Get.translations.TryGetValue(id, out val) ? string.Format(val, parameters) : // search in translations
                Get.defaultText.TryGetValue(id, out val) ? string.Format(val, parameters) : // search in default
                NoValueText + id; // return ERROR text
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
                    if (item.Attributes == null)
                    {
                        continue;
                    }

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

        private static void ApplyDictionaries(
            Dictionary<string, string> defaultDic, 
            Dictionary<string, string> translations,
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
            catch
            {
            }
        }
    }
}
