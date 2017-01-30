using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;

namespace GiantBombUnofficialClassic.Models
{
    public class MediaEnginePlaybackSource : IMediaEnginePlaybackSource
    {
        public MediaPlaybackItem CurrentItem
        {
            get
            {
                var source = Windows.Media.Core.MediaSource.CreateFromUri(new Uri("http://v.giantbomb.com/2017/01/27/vf_giantbomb_bestof_115_4000.mp4"));
                return new MediaPlaybackItem(source);
            }
        }

        public void SetPlaybackSource(IMediaPlaybackSource source)
        {
            throw new NotImplementedException();
        }
    }
}
