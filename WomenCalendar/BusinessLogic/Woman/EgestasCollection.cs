using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WomenCalendar
{
    [XmlRoot("Egestas")]
    public class EgestasCollection : SerializableEventsCollection<int>
    {
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
