using System;
using System.Threading.Tasks;

using Plugin.Media;
using Plugin.Media.Abstractions;

namespace GiveMeContextInfo
{
    public static class MediaService
    {
        public static async Task<MediaFile> TakePhotoAsync ()
        {
            await CrossMedia.Current.Initialize ();

            MediaFile photo;
            if (CrossMedia.Current.IsCameraAvailable)
            {
                photo = await CrossMedia.Current.TakePhotoAsync (new StoreCameraMediaOptions {
                    Directory = "GiveMeContext",
                    Name = "context.jpg"
                });
            }
            else
            {
                photo = await CrossMedia.Current.PickPhotoAsync ();
            }
            return photo;
        }
    }
}
