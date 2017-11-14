using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.Services
{
    public class VideoGroupImageProvider
    {
        public static Uri GetImageForGroupName(string groupName)
        {
            Uri imageLocation = new Uri(@"ms-appx:///Assets//Groups//default.png");
            if (!String.IsNullOrWhiteSpace(groupName))
            {
                groupName = groupName.Trim().ToLower();

                switch (groupName)
                {
                    case "best of giant bomb":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//BestOfGiantBomb.jpg");
                        break;
                    case "breaking brad":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//BreakingBrad.jpg");
                        break;
                    case "endurance run":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//EnduranceRun.jpg");
                        break;
                    case "events":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Events.jpg");
                        break;
                    case "extra life":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//ExtraLife.jpg");
                        break;
                    case "features":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Features.jpg");
                        break;
                    case "game tapes":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//GameTapes.jpg");
                        break;
                    case "game tapes raw":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//GameTapesRaw.jpg");
                        break;
                    case "giant bombcast":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Bombcast.jpg");
                        break;
                    case "kerbal: project b.e.a.s.t":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//ProjectBEAST.jpg");
                        break;
                    case "metal gear scanlon":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//MetalGearScanlon.jpg");
                        break;
                    case "old games":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//OldGames.png");
                        break;
                    case "premium":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Premium.jpg");
                        break;
                    case "quick looks":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//QuickLooks.jpg");
                        break;
                    case "reviews":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//VideoReview.jpg");
                        break;
                    case "tang":
                    case "this ain't no game":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//TANG.jpg");
                        break;
                    case "trailers":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Trailers.jpg");
                        break;
                    case "unfinished":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Unfinished.png");
                        break;
                    case "vinnyvania":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Vinnyvania.jpg");
                        break;
                    case "unprofessional fridays":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//UPF.jpg");
                        break;
                    default:
                        break;
                }
            }
            return imageLocation;
        }

        /// <summary>
        /// Some category/show images provided by the site are bad and really don't look good in the app.
        /// This method provides a replacement that looks better!
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static Uri GetOverrideImageForGroupName(string groupName)
        {
            Uri imageLocation = null;
            if (!String.IsNullOrWhiteSpace(groupName))
            {
                groupName = groupName.Trim().ToLower();
                switch (groupName)
                {
                    case "quick looks":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//QuickLooks.jpg");
                        break;
                    case "giant bombcast":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Bombcast.jpg");
                        break;
                    case "this is the run":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//ThisIsTheRun.jpg");
                        break;
                    case "best of giant bomb":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//BestOfGiantBomb.jpg");
                        break;
                    case "breaking brad":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//BreakingBrad.jpg");
                        break;
                    case "endurance run":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//EnduranceRun.jpg");
                        break;
                    case "events":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Events.jpg");
                        break;
                    case "extra life":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//ExtraLife.jpg");
                        break;
                    case "features":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Features.jpg");
                        break;
                    case "old games":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//OldGames.png");
                        break;
                    case "premium":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Premium.jpg");
                        break;
                    case "trailers":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Trailers.jpg");
                        break;
                    case "unfinished":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Unfinished.png");
                        break;
                    case "game tapes raw":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//GameTapesRaw.jpg");
                        break;
                    case "metal gear scanlon":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//MetalGearScanlon.jpg");
                        break;
                    case "kerbal: project b.e.a.s.t":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//ProjectBEAST.jpg");
                        break;
                    case "reviews":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//VideoReview.jpg");
                        break;
                    case "tang":
                    case "this ain't no game":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//TANG.jpg");
                        break;
                    case "vinnyvania":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Vinnyvania.jpg");
                        break;
                    case "unprofessional fridays":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//UPF.jpg");
                        break;
                    default:
                        break;
                }
            }
            return imageLocation;
        }
    }
}
