using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// Complicated logic of ovulation detection is here.
    /// </summary>
    public class OvulationDetector
    {
        private const int OvulationCalendarMethod = 14;
        private const int NumberOfDaysToLookIn = 3;
        private static readonly DateTime NoDate = default(DateTime);
        private Woman woman;

        /// <summary>
        /// Create the object.
        /// </summary>
        /// <param name="w">The data to use.</param>
        public OvulationDetector(Woman w)
        {
            if (w == null)
            {
                throw new ArgumentNullException("Woman must be specified.", "w");
            }

            this.woman = w;
        }

        /// <summary>
        /// The temperature move indicator.
        /// </summary>
        internal enum TemperatureMove
        {
            Unknown, Down, Up, Same
        }

        /// <summary>
        /// Run the business logic.
        /// </summary>
        /// <param name="nextCycleFirstDay">This parameter is the one which is widely used thru all the logic. 
        /// Without it the prediction is impossible.</param>
        /// <returns>The probable ovulation date.</returns>
        public DateTime EstimateOvulationDate(DateTime nextCycleFirstDay)
        {
            if (nextCycleFirstDay == NoDate)
            {
                throw new ArgumentException("Last cycle day period must be specified.", "lastCycleDay");
            }

            // first level of undertanding - Calendar: the 14 days before the end.
            // This variable MUST be initialized. NoDate is not acceptable at all.
            DateTime ovulCalend = nextCycleFirstDay.AddDays(-1 * OvulationCalendarMethod);

            // second level - CF:
            DateTime ovulCF = this.TryUnderstandOvulationByCF(ovulCalend);

            // third level - BBT: drop of the temperature 1 day before Ov., and rise on the Ov. day.
            var rises = this.GetBBTRises(ovulCalend);
            
            // At this point we found all dates we need.
            if (rises.Count == 0)
            { // BBT is not infomative. Do not use it for prediction.
                return this.PredictByCFOnly(ovulCF, ovulCalend);
            }

            if (ovulCF == NoDate)
            { // CF is not infomative. Use BBT method only.
                return this.PredictByBBTOnly(rises, ovulCalend);
            }

            return this.PredictByCFAndBBT(rises, ovulCF, ovulCalend);
        }

        private DateTime PredictByCFAndBBT(List<Rise> rises, DateTime ovulCF, DateTime ovulCalend)
        {
            if (rises.Count == 1)
            { // the only rise 
                return this.PredictByOneRiseAndCF(rises[0], ovulCF);
            }

            List<Rise> significantRises = this.FilterNonSignificantRises(rises);
            if (significantRises.Count == 1)
            { // highest rise exists. Then it is our BBT rise we are searching for.
                return this.PredictByOneRiseAndCF(significantRises[0], ovulCF);
            }

            List<Rise> closesRises = this.FilterClosestRises(significantRises, ovulCF);
            if (closesRises.Count == 1)
            { // between several rises we found the only closes one. Assume it is our rise.
                return this.PredictByOneRiseAndCF(closesRises[0], ovulCF);
            }

            // At this point we understand that Ov. CF day is equally points to several rises.
            // So CF is not informative. Avoid using it for predictions.
            return this.PredictByBBTOnly(rises, ovulCalend);
        }

        private DateTime PredictByOneRiseAndCF(Rise rise, DateTime ovulCF)
        {
            if (rise.ContainsDate(ovulCF))
            { // Ov. CF day inside it. This means it is definitely our rise.
                // We assume that Ov. CF day points us exactly the Ov. day.
                return ovulCF;
            }

            // at this point we understand that CF is out of our rise.
            // Thus CF is not informative at all. Use the rise as Ov. day.
            return rise.Start;
        }

        private DateTime PredictByBBTOnly(List<Rise> rises, DateTime ovulCalend)
        {
            if (rises.Count == 1)
            {
                return rises[0].Start;
            }

            List<Rise> significantRises = this.FilterNonSignificantRises(rises);
            if (significantRises.Count == 1)
            { // highest rise exists. Then it is our BBT rise we are searching for.
                return significantRises[0].Start;
            }

            List<Rise> closesRises = this.FilterClosestRises(significantRises, ovulCalend);
            if (closesRises.Count == 1)
            { // between several rises we found the only closes one. Assume it is our ovulation date.
                return closesRises[0].Start;
            }

            // At this point we faced the very-very bad situation.
            // There are several huge temperature rises.
            // And even a doctor can't find the best solution here.
            // Let's just use latest rise.
            return closesRises[closesRises.Count - 1].Start;
        }

        /// <summary>
        /// Searches for closes Rises to the date.
        /// </summary>
        /// <param name="rises">Rise list.</param>
        /// <param name="date">Date to meagure with.</param>
        /// <returns>Closes rise.Start to the date.</returns>
        private List<Rise> FilterClosestRises(List<Rise> rises, DateTime date)
        {
            List<Rise> closestRises = new List<Rise>();
            int minimalDistance = int.MaxValue;
            foreach (var rise in rises)
            {
                if (rise.ContainsDate(date))
                {
                    return new List<Rise>() { rise };
                }

                int distance = Math.Abs((rise.Start - date).Days);
                if (minimalDistance == int.MaxValue || distance == minimalDistance)
                { // it's a first element of the list or it is also close date.
                    closestRises.Add(rise);
                    minimalDistance = distance;
                }

                if (distance < minimalDistance)
                { // found new closest day.
                    closestRises.Clear();
                    closestRises.Add(rise);
                    minimalDistance = distance;
                }
            }

            return closestRises;
        }

        private List<Rise> FilterNonSignificantRises(List<Rise> rises)
        {
            Rise maxRise = null;
            foreach (var rise in rises)
            {
                if (maxRise == null || rise.RiseSize > maxRise.RiseSize)
                {
                    maxRise = rise;
                }
            }

            List<Rise> significantRises = new List<Rise>();
            foreach (var rise in rises)
            {
                if (!rise.SignificantlyDifferentSize(maxRise))
                { // this is huge rise. Use it.
                    significantRises.Add(rise);
                }
            }

            return significantRises;
        }

        private DateTime PredictByCFOnly(DateTime ovulCF, DateTime ovulCalend)
        {
            if (ovulCF != NoDate)
            { // at this point we have only ovCF method. Just use it as it is more prioritive than calendar.
                return ovulCF;
            }
            else
            { // no good BBT and no good CF information was found. Then just use calendar method.
                return ovulCalend;
            }
        }

        private DateTime TryUnderstandOvulationByCF(DateTime ovulCalend)
        {
            DateTime lastEggLikeCFDay = NoDate;
            for (int i = 0; i < 2 * NumberOfDaysToLookIn; i++)
            {
                DateTime day = ovulCalend.AddDays((-1 * NumberOfDaysToLookIn) + i);
                CervicalFluid cf = this.woman.CFs[day];

                if (cf == CervicalFluid.Stretchy)
                {
                    lastEggLikeCFDay = day;
                }
            }

            if (lastEggLikeCFDay != NoDate)
            {
                return lastEggLikeCFDay;
            }

            return NoDate; // CFs are not informative enough to understand the Ov. day.
        }

        private List<Rise> GetBBTRises(DateTime ovylCalend)
        {
            var rises = new List<Rise>();
            double previousBBT = 0;
            DateTime previousDate = NoDate;
            bool isRecordingRise = false;
            DateTime recordingRiseStart = NoDate;
            double recordingRiseStartBBT = 0;
            DateTime currentDay = NoDate;
            double currentBBT = 0;
            for (int i = 0; i <= 2 * NumberOfDaysToLookIn; i++)
            {
                if (currentBBT != 0)
                {
                    previousBBT = currentBBT;
                    previousDate = currentDay;
                }

                currentDay = ovylCalend.AddDays((-1 * NumberOfDaysToLookIn) + i);
                currentBBT = this.woman.BBT.GetBBT(currentDay);
                if (currentBBT == 0)
                {
                    continue; // no BBT found for this day.
                }

                if (previousBBT == 0)
                {
                    continue; // this is first element. Wait for second one.
                }

                var move = this.GetMove(previousBBT, currentBBT);

                if (move == TemperatureMove.Up)
                {
                    if (isRecordingRise)
                    {
                        continue;
                    }

                    recordingRiseStart = previousDate;
                    recordingRiseStartBBT = previousBBT;
                    isRecordingRise = true;
                    continue;
                }
                else
                {
                    if (isRecordingRise)
                    {
                        rises.Add(new Rise()
                        {
                            Start = recordingRiseStart,
                            Stop = previousDate,
                            BbtLow = recordingRiseStartBBT,
                            BbtHigh = previousBBT
                        });
                        isRecordingRise = false;
                    }
                }
            }

            if (isRecordingRise)
            {
                rises.Add(new Rise()
                {
                    Start = recordingRiseStart,
                    Stop = currentDay,
                    BbtLow = recordingRiseStartBBT,
                    BbtHigh = currentBBT
                });
            }

            return rises;
        }

        private TemperatureMove GetMove(double tempPrev, double tempNext)
        {
            return tempPrev == tempNext ? TemperatureMove.Same :
                tempPrev < tempNext ? TemperatureMove.Up :
                TemperatureMove.Down;
        }

        /// <summary>
        /// Represents temperature rise.
        /// </summary>
        internal class Rise
        {
            /// <summary>
            /// The coefficient of the difference. 
            /// The temperature increment value should be higher this much times to be counted as significant.
            /// </summary>
            internal static readonly double SignificantCoefficient = 2;

            /// <summary>
            /// Rise start day.
            /// </summary>
            internal DateTime Start { get; set; }

            /// <summary>
            /// Rise end day.
            /// </summary>
            internal DateTime Stop { get; set; }

            /// <summary>
            /// Length of the rise.
            /// </summary>
            internal int DaysLength
            {
                get { return (this.Stop - this.Start).Days; }
            }

            /// <summary>
            /// The rise stat temperature.
            /// </summary>
            internal double BbtLow { get; set; }

            /// <summary>
            /// The rise finish temperature.
            /// </summary>
            internal double BbtHigh { get; set; }
            
            /// <summary>
            /// The temperature increment value.
            /// </summary>
            internal double RiseSize
            {
                get { return this.BbtHigh - this.BbtLow; }
            }

            /// <summary>
            /// Check if day is within the rise.
            /// </summary>
            /// <param name="date">Day to check</param>
            /// <returns>True if it is a rise day.</returns>
            internal bool ContainsDate(DateTime date)
            {
                return this.Start <= date && date <= this.Stop;
            }

            /// <summary>
            /// Compare to another rise.
            /// </summary>
            /// <param name="rise">Object to comparw with.</param>
            /// <returns>True if this object and the given one are have much different increments.</returns>
            internal bool SignificantlyDifferentSize(Rise rise)
            {
                double diffenece = rise.RiseSize > this.RiseSize ?
                    rise.RiseSize / this.RiseSize :
                    this.RiseSize / rise.RiseSize;
                return diffenece > SignificantCoefficient;
            }
        }
    }
}
