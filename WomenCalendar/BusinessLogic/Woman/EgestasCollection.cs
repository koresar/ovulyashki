using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WomenCalendar
{
    [XmlRoot("Egestas")]
    public class EgestasCollection : SerializableEventsCollection<int>
    {
        public static TranslationsList EgestasNames = new TranslationsList()
        { 
            "Egesta_amount_0", "Egesta_amount_1", "Egesta_amount_2", "Egesta_amount_3", "Egesta_amount_4"
        };

        public const int MaximumEgestaValue = 4;

        public EgestasCollection()
            : base("Egesta")
        {
        }

        public EgestasCollection(DateTime startDay, int length)
            : this()
        {
            int i = 0;
            for (; i < length - (MaximumEgestaValue + 1); i++)
            {
                Add(startDay.AddDays(i), MaximumEgestaValue);
            }
            for (int egesta = MaximumEgestaValue; i < length; i++, egesta--)
            {
                Add(startDay.AddDays(i), egesta);
            }
        }
    }
}
