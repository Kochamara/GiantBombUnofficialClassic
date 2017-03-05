using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace GiantBombUnofficialClassic.Views.Controls
{
    public sealed class CustomMediaTransportControls : MediaTransportControls
    {

        public CustomMediaTransportControls()
        {
            this.DefaultStyleKey = typeof(CustomMediaTransportControls);
        }

        protected override void OnApplyTemplate()
        {
            // Add our custom button click events
            Button skipForward30Button = GetTemplateChild("SkipForward30Button") as Button;
            skipForward30Button.Click += SkipForward30Button_Click;

            Button skipBack10Button = GetTemplateChild("SkipBack10Button") as Button;
            skipBack10Button.Click += SkipBack10Button_Click;

            //Slider slider = GetTemplateChild("ProgressSlider") as Slider;
            //slider.DragStarting += Slider_DragStarting; ;
            //slider.DropCompleted += Slider_DropCompleted; ; ;

            base.OnApplyTemplate();
        }

        //private void Slider_DropCompleted(UIElement sender, DropCompletedEventArgs args)
        //{
        //    SliderManipulationCompleted?.Invoke(this, EventArgs.Empty);
        //}

        //private void Slider_DragStarting(UIElement sender, DragStartingEventArgs args)
        //{
        //    SliderManipulationStarted?.Invoke(this, EventArgs.Empty);
        //}
        //// TODO: find better names for all this
        //public event EventHandler<EventArgs> SliderManipulationCompleted;
        //public event EventHandler<EventArgs> SliderManipulationStarted;

        private void SkipForward30Button_Click(object sender, RoutedEventArgs e)
        {
            SkipForward30?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> SkipForward30;

        private void SkipBack10Button_Click(object sender, RoutedEventArgs e)
        {
            SkipBack10?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> SkipBack10;

        // This is where you would get your custom button and create an event handler for its click method.
        //Button likeButton = GetTemplateChild("LikeButton") as Button;
        //likeButton.Click += LikeButton_Click;

        //public event EventHandler<EventArgs> Liked;

        //private void LikeButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Raise an event on the custom control when 'like' is clicked
        //    Liked?.Invoke(this, EventArgs.Empty);
        //}
    }
}
