using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillerClock.Controllers
{
    public class HomeController : Controller
    {
        /*
         * Following is the calculation Miller's time
         * 1 mHr = 7 eYr
         * 1 eYr = 31556926 eSec
         * 7 eYr = 31556926 * 7 eSec = 220898482 eSec
         * Thus 
         * 1 mHr = 220898482 eSec
         * 3600 mSec = 220898482 eSec
         * Divide both by 3600
         * 1 mSec = 61360.6894 eSec
         */

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
                hours = millerTimeDifference.Hours,
                minutes = millerTimeDifference.Minutes,
                seconds = millerTimeDifference.Seconds
            });
        }
    }
}
