using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.Services
{
    public class CategoryImageProvider
    {
        public static Uri GetImageForCategoryName(string categoryName)
        {
            Uri imageLocation = new Uri(@"ms-appx:///Assets//Categories//default.png");
            if (!String.IsNullOrWhiteSpace(categoryName))
            {
                categoryName = categoryName.Trim();
                switch (categoryName)
                {
                    case "Best of Giant Bomb":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//BestOfGiantBomb.jpg");
                        break;
                    case "Breaking Brad":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//BreakingBrad.jpg");
                        break;
                    case "Endurance Run":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//EnduranceRun.jpg");
                        break;
                    case "Events":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//Events.jpg");
                        break;
                    case "Extra Life":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//ExtraLife.jpg");
                        break;
                    case "Features":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//Features.jpg");
                        break;
                    case "Game Tapes":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//GameTapes.jpg");
                        break;
                    case "Giant Bombcast":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//Bombcast.jpg");
                        break;
                    case "Kerbal: Project B.E.A.S.T":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//ProjectBEAST.jpg");
                        break;
                    case "Metal Gear Scanlon":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//MetalGearScanlon.jpg");
                        break;
                    case "Old Games":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//OldGames.png");
                        break;
                    case "Premium":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//Premium.jpg");
                        break;
                    case "Quick Looks":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//QuickLooks.jpg");
                        break;
                    case "Reviews":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//VideoReview.jpg");
                        break;
                    case "TANG":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//TANG.jpg");
                        break;
                    case "Trailers":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//Trailers.jpg");
                        break;
                    case "Unfinished":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//Unfinished.png");
                        break;
                    case "VinnyVania":
                        imageLocation = new Uri(@"ms-appx:///Assets//Categories//Vinnyvania.jpg");
                        break;
                    default:
                        break;
                }
            }
            return imageLocation;
        }
    }
}
