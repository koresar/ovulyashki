using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class TranslationsList : List<string>
    {
        new public string this[int i]
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
