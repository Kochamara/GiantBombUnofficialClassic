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
                groupName = groupName.Trim();
                switch (groupName)
                {
                    case "Best of Giant Bomb":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//BestOfGiantBomb.jpg");
                        break;
                    case "Breaking Brad":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//BreakingBrad.jpg");
                        break;
                    case "Endurance Run":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//EnduranceRun.jpg");
                        break;
                    case "Events":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Events.jpg");
                        break;
                    case "Extra Life":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//ExtraLife.jpg");
                        break;
                    case "Features":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Features.jpg");
                        break;
                    case "Game Tapes":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//GameTapes.jpg");
                        break;
                    case "Game Tapes RAW":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//GameTapesRaw.jpg");
                        break;
                    case "Giant Bombcast":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Bombcast.jpg");
                        break;
                    case "Kerbal: Project B.E.A.S.T":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//ProjectBEAST.jpg");
                        break;
                    case "Metal Gear Scanlon":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//MetalGearScanlon.jpg");
                        break;
                    case "Old Games":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//OldGames.png");
                        break;
                    case "Premium":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Premium.jpg");
                        break;
                    case "Quick Looks":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//QuickLooks.jpg");
                        break;
                    case "Reviews":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//VideoReview.jpg");
                        break;
                    case "TANG":
                    case "This Ain't No Game":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//TANG.jpg");
                        break;
                    case "Trailers":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Trailers.jpg");
                        break;
                    case "Unfinished":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Unfinished.png");
                        break;
                    case "VinnyVania":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Vinnyvania.jpg");
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
                groupName = groupName.Trim();
                switch (groupName)
                {
                    case "Quick Looks":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//QuickLooks.jpg");
                        break;
                    case "Giant Bombcast":
                        imageLocation = new Uri(@"ms-appx:///Assets//Groups//Bombcast.jpg");
                        break;
                    default:
                        break;
                }
            }
            return imageLocation;
        }
    }
}
