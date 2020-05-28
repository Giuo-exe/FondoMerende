using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Utilities
{
    class EmbeddedImage : IMarkupExtension
    {
        public string Resource { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(Resource))
            {
                return null;
            }
            return ImageSource.FromResource(Resource);
        }
    }
}
