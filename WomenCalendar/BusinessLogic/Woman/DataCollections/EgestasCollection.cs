using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WomenCalendar
{
    /// <summary>
    /// The serializable egestas (amount of bleeding) collection.
    /// </summary>
    [XmlRoot("Egestas")]
    public class EgestasCollection : SerializableEventsCollection<int>
    {
        /// <summary>
        /// The egesta maximum value.
        /// </summary>
        public const int MaximumEgestaValue = 4;

        /// <summary>
        /// Static collection for egesta translated names.
        /// </summary>
        static EgestasCollection()
        {
            EgestasNames = new TranslationsList()
            { 
                "Egesta_amount_0", "Egesta_amount_1", "Egesta_amount_2", "Egesta_amount_3", "Egesta_amount_4"
            };
        }

        /// <summary>
        /// Create the instance of the list.
        /// </summary>
        public EgestasCollection()
            : base("Egesta")
        {
        }

        /// <summary>
        /// Create the default egesta collection.
        /// </summary>
        /// <param name="startDay">The egesting start.</param>
        /// <param name="length">The egesting days count.</param>
        public EgestasCollection(DateTime startDay, int length)
            : this()
        {
            int i = 0;
            for (; i < length - (MaximumEgestaValue + 1); i++)
            {
                this.Add(startDay.AddDays(i), MaximumEgestaValue);
            }

            for (int egesta = MaximumEgestaValue; i < length; i++, egesta--)
            {
                this.Add(startDay.AddDays(i), egesta);
            }
        }

        /// <summary>
        /// Each egesta translated name.
        /// </summary>
        public static TranslationsList EgestasNames { get; set; }
    }
}
