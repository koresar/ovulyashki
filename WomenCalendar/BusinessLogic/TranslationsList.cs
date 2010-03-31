using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// Class for storing strings in the array like object. The idea of the class is to have all the List
    /// features same time translating all the phrases by its IDs.
    /// </summary>
    public class TranslationsList : List<string>
    {
        /// <summary>
        /// Get the translation of the phrase IDs we have as list items.
        /// </summary>
        /// <param name="i">The index of item.</param>
        /// <returns>Translated text.</returns>
        public new string this[int i]
        {
            get
            {
                return TEXT.Get[base[i]];
            }

            set
            {
                base[i] = value;
            }
        }
    }
}
