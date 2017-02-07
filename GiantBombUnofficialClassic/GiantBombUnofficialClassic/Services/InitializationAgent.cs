using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.Services
{
    public static class InitializationAgent
    {
        public static void Initialize()
        {
            GalaSoft.MvvmLight.Threading.DispatcherHelper.Initialize();

        }
    }
}
