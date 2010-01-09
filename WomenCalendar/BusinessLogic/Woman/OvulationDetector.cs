using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class OvulationDetector
    {
        private const int numberOfDaysToLookIn = 3;

        public const int OvulationCalendarMethod = 14;
        public static readonly DateTime NoDate = default(DateTime);

        private Woman woman;

        public OvulationDetector(Woman w)
        {
            if (w == null)
            {
                throw new ArgumentNullException("Woman must be specified.", "w");
            }
            woman = w;
        }

        public DateTime EstimateOvulationDate(DateTime nextCycleFirstDay)
        {
            if (nextCycleFirstDay == NoDate)
            {
                throw new ArgumentException("Last cycle day period must be specified.", "lastCycleDay");
            }

            // first level of undertanding - Calendar: the 14 days before the end.
            // This variable MUST be initialized. NoDate is not acceptable at all.
            DateTime ovCalend = nextCycleFirstDay.AddDays(-1 * (OvulationCalendarMethod));

            // second level - CF:
            DateTime ovCF = TryUnderstandOvulationByCF(ovCalend);

            // third level - BBT: drop of the temperature 1 day before Ov., and rise on the Ov. day.
            var rises = GetBBTRises(ovCalend);
            
            // At this point we found all dates we need.

            if (rises.Count == 0)
            { // BBT is not infomative. Do not use it for prediction.
                return PredictByCFOnly(ovCF, ovCalend);
            }

            if (ovCF == NoDate)
            { // CF is not infomative. Use BBT method only.
                return PredictByBBTOnly(rises, ovCalend);
            }
            else
            {
                return PredictByCFAndBBT(rises, ovCF, ovCalend);
            }
        }

        private DateTime PredictByCFAndBBT(List<Rise> rises, DateTime ovCF, DateTime ovCalend)
        {
            if (rises.Count == 1)
            { // the only rise 
                return PredictByOneRiseAndCF(rises[0], ovCF);
            }

            List<Rise> significantRises = FilterNonSignificantRises(rises);
            if (significantRises.Count == 1)
            { // highest rise exists. Then it is our BBT rise we are searching for.
                return PredictByOneRiseAndCF(significantRises[0], ovCF);
            }

            List<Rise> closesRises = FilterClosestRises(significantRises, ovCF);
            if (closesRises.Count == 1)
            { // between several rises we found the only closes one. Assume it is our rise.
                return PredictByOneRiseAndCF(closesRises[0], ovCF);
            }

            // At this point we understand that Ov. CF day is equally points to several rises.
            // So CF is not informative. Avoid using it for predictions.
            return PredictByBBTOnly(rises, ovCalend);
        }

        private DateTime PredictByOneRiseAndCF(Rise rise, DateTime ovCF)
        {
            if (rise.ContainsDate(ovCF))
            { // Ov. CF day inside it. This means it is definitely our rise.
                // We assume that Ov. CF day points us exactly the Ov. day.
                return ovCF;
            }
            // at this point we understand that CF is out of our rise.
            // Thus CF is not informative at all. Use the rise as Ov. day.
            return rise.Start;
        }

        private DateTime PredictByBBTOnly(List<Rise> rises, DateTime ovCalend)
        {
            if (rises.Count == 1)
            {
                return rises[0].Start;
            }

            List<Rise> significantRises = FilterNonSignificantRises(rises);
            if (significantRises.Count == 1)
            { // highest rise exists. Then it is our BBT rise we are searching for.
                return significantRises[0].Start;
            }

            List<Rise> closesRises = FilterClosestRises(significantRises, ovCalend);
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
                if (rise.ContainsDate(date)) { return new List<Rise>() { rise }; }
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
                {// this is huge rise. Use it.
                    significantRises.Add(rise);
                }
            }

            return significantRises;
        }

        private DateTime PredictByCFOnly(DateTime ovCF, DateTime ovCalend)
        {
            if (ovCF != NoDate)
            { // at this point we have only ovCF method. Just use it as it is more prioritive than calendar.
                return ovCF;
            }
            else
            { // no good BBT and no good CF information was found. Then just use calendar method.
                return ovCalend;
            }
        }

        private DateTime TryUnderstandOvulationByCF(DateTime ovCalend)
        {
            DateTime lastEggLikeCFDay = NoDate;
            for (int i = 0; i < 2 * numberOfDaysToLookIn; i++)
            {
                DateTime day = ovCalend.AddDays(-1 * numberOfDaysToLookIn + i);
                CervicalFluid cf = woman.CFs[day];

                if (cf == CervicalFluid.Stretchy) lastEggLikeCFDay = day;
            }
            if (lastEggLikeCFDay != NoDate)
            {
                return lastEggLikeCFDay;
            }
            return NoDate; // CFs are not informative enough to understand the Ov. day.
        }

        private List<Rise> GetBBTRises(DateTime ovCalend)
        {
            //t
            //e           *********
            //m *********
            //p          *
            //e
            //r days->   O
            //a          v
            var rises = new List<Rise>();
            double previousBBT = 0;
            DateTime previousDate = NoDate;
            //TemperatureMove previousMove = TemperatureMove.Unknown;
            bool isRecordingRise = false;
            DateTime recordingRiseStart = NoDate;
            double recordingRiseStartBBT = 0;
            DateTime currentDay = NoDate;
            double currentBBT = 0;
            for (int i = 0; i <= 2 * numberOfDaysToLookIn; i++)
            {
                if (currentBBT != 0)
                {
                    previousBBT = currentBBT;
                    previousDate = currentDay;
                }

                currentDay = ovCalend.AddDays(-1 * numberOfDaysToLookIn + i);
                currentBBT = woman.BBT.GetBBT(currentDay);
                if (currentBBT == 0) continue; // no BBT found for this day.
                if (previousBBT == 0) continue; // this is first element. Wait for second one.

                var move = GetMove(previousBBT, currentBBT);

                if (move == TemperatureMove.Up)
                {
                    if (isRecordingRise) { continue; }

                    recordingRiseStart = previousDate;
                    recordingRiseStartBBT = previousBBT;
                    isRecordingRise = true;
                    continue;
                }
                else // if (move == TemperatureMove.Down)
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

        enum TemperatureMove { Unknown, Down, Up, Same };

        class Rise
        {
            public static readonly double SignificantCoefficient = 2;

            public DateTime Start { get; set; }
            public DateTime Stop { get; set; }
            public int DaysLength { get { return (Stop - Start).Days; } }

            public double BbtLow { get; set; }
            public double BbtHigh { get; set; }
            public double RiseSize { get { return BbtHigh - BbtLow; } }

            public bool ContainsDate(DateTime date)
            {
                return Start <= date && date <= Stop;
            }

            public bool SignificantlyDifferentSize(Rise rise)
            {
                double diffenece = rise.RiseSize > this.RiseSize ?
                    rise.RiseSize / this.RiseSize :
                    this.RiseSize / rise.RiseSize;
                return diffenece > SignificantCoefficient;
            }
        }
    }
}
