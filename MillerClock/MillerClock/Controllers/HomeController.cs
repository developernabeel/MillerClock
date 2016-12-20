using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillerClock.Controllers
{
    /// <summary>
    /// Following is the calculation to find out how long is 1 second on Miller's planet
    /// 
    /// Symbols used :
    /// eSec = Earth Second
    /// eYr = Earth Year
    /// mSec = Miller Second
    /// mHr = Miller Hour
    /// 
    /// Calculation :
    /// We know that 1 hour on Miller's planet is equivalent to 7 years on Earth
    /// 1 mHr = 7 eYr
    /// 1 eYr = 31556926 eSec
    /// 7 eYr = 31556926 * 7 eSec = 220898482 eSec
    /// Thus 
    /// 1 mHr = 220898482 eSec
    /// 3600 mSec = 220898482 eSec
    /// Divide both by 3600
    /// 1 mSec = 61360.6894 eSec
    /// </summary>
    public class HomeController : Controller
    {
        // 1 Miller Second = 61360.6894 Earth Seconds
        private const double OneMillerSecond = 61360.6894;

        // Considering the official release date of Interstellar as the start time.
        private readonly DateTime interstellarReleaseDate = new DateTime(2014, 11, 7, 0, 0, 0);

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetTimeElapsed()
        {
            // End time is the Earth's current UTC time
            DateTime currentTime = DateTime.UtcNow;

            // Total elapsed time would be the time difference between the start and end time defined above.
            TimeSpan timeDifference = currentTime.Subtract(interstellarReleaseDate);

            // Converting the time difference in earth seconds to miller seconds and calculating elapsed time with those seconds.
            double millerSecondsPassed = timeDifference.TotalSeconds / OneMillerSecond;
            TimeSpan millerTimeDifference = TimeSpan.FromSeconds(millerSecondsPassed);

            return Json(new
            {
                earthTime = new
                {
                    days = timeDifference.Days,
                    hours = timeDifference.Hours,
                    minutes = timeDifference.Minutes,
                    seconds = timeDifference.Seconds
                },
                millerTime = new
                {
                    hours = millerTimeDifference.Hours,
                    minutes = millerTimeDifference.Minutes,
                    seconds = millerTimeDifference.Seconds
                }
            });
        }
    }
}
