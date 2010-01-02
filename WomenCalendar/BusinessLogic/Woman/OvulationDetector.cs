using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class OvulationDetector
    {
        public const int OvulationCalendarMethod = 14;
        public static readonly DateTime NoDate = default(DateTime);

        public static DateTime EstimateOvulationDate(Woman w, DateTime nextCycleFirstDay)
        {
            if (nextCycleFirstDay == NoDate)
            {
                throw new ArgumentException("Last cycle day period must be specified.", "lastCycleDay");
            }
            if (w == null)
            {
                throw new ArgumentException("Woman must be specified.", "w");
            }

            const int numberOfDaysToLookIn = 3;

            // first level of undertanding - Calendar: the 14 days before the end.
            // This variable MUST be initialized. NoDate is not acceptable at all.
            DateTime ovCalend = nextCycleFirstDay.AddDays(-1 * (OvulationCalendarMethod));

            // second level - CF:
            var someCFs = new Dictionary<DateTime, CervicalFluid>(numberOfDaysToLookIn);
            DateTime lastEggLikeCFDay = NoDate;
            DateTime ovCF = NoDate;
            for (int i = 0; i < 2 * numberOfDaysToLookIn; i++)
            {
                DateTime day = ovCalend.AddDays(-1 * numberOfDaysToLookIn + i);
                CervicalFluid cf = w.CFs[day];
                someCFs[day] = cf;
                
                if (cf == CervicalFluid.Stretchy) lastEggLikeCFDay = day;
            }
            if (lastEggLikeCFDay != NoDate)
            {
                ovCF = lastEggLikeCFDay;
            }

            // third level - BBT: drop of the temperature 1 before Ov., and rise on the Ov. day.
            //t
            //e           *********
            //m *********
            //p          *
            //e
            //r days->   O
            //a          v
            var someBBTs = new Dictionary<DateTime, double>(numberOfDaysToLookIn);
            DateTime lastLowestBBTDay = NoDate;
            DateTime firstHighestBBTDay = NoDate;
            for (int i = 0; i < 2 * numberOfDaysToLookIn; i++)
            {
                DateTime day = ovCalend.AddDays(-1 * numberOfDaysToLookIn + i);
                double bbt = w.BBT.GetBBT(day);
                someBBTs[day] = bbt;

                if (bbt != 0 && (lastLowestBBTDay == NoDate || bbt <= w.BBT.GetBBT(lastLowestBBTDay)))
                    lastLowestBBTDay = day;
                if (bbt != 0 && (firstHighestBBTDay == NoDate || bbt > w.BBT.GetBBT(firstHighestBBTDay)))
                    firstHighestBBTDay = day;
            }

            // At this point we found all dates we need.
            if (firstHighestBBTDay != NoDate && lastLowestBBTDay != NoDate && // BBT info is present
                lastLowestBBTDay < firstHighestBBTDay) // and there was at least one temperature rise.
            {
                if (ovCF != NoDate)
                { // at this point we can use both BBT and CF prediction methods.
                    if (UtilityMethods.Within(ovCF, lastLowestBBTDay, firstHighestBBTDay))
                    { // the CF is in the BBT pick range then CF is our date.
                        return ovCF;
                    }
                    if (ovCF == ovCalend)
                    {
                        return ovCF;
                    }
                }
                if (UtilityMethods.Within(ovCalend, lastLowestBBTDay, firstHighestBBTDay))
                { // the calendar date is in the BBT pick range then it is our date.
                    return ovCalend;
                }
                return UtilityMethods.Middle(lastLowestBBTDay, firstHighestBBTDay);
            }
            else
            { // BBT can not give us any good information thus try to use CF on its own.
                if (ovCF != NoDate)
                { // at this point we have only ovCF method. Just use it as it is more prioritive than calendar.
                    return ovCF;
                }
                else
                { // no good BBT and no good CF information was found. Then just use calendar method.
                    return ovCalend;
                }
            }
        }
    }
}
