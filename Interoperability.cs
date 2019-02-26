using System.IO;
using System.Threading.Tasks;
using CoreFoundation;
using Foundation;
using SkiaSharp;
using UIKit;

namespace BmojiApp.iOS.Services.WhatsApp
{
    public static class Interoperability
    {
        const string DEFAULT_BUNDLE_IDENTIFIER = "WA.WAStickersThirdParty";
        const int PASTEBOARD_EXPIRATION_SECONDS = 60;
        const string PASTEBOARD_STICKER_PACK_DATA_TYPE = "net.whatsapp.third-party.sticker-pack";
        const string WHATSAPP_URL = "whatsapp://stickerPack";

        static bool CanSend()
        {
            return UIApplication.SharedApplication.CanOpenUrl(new NSUrl("whatsapp://"));
        }

        public static async void Send(NSDictionary<NSString, NSObject> dataToSend)
        {
            //if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            //{
            //    UIPasteboard.General.SetItems(new NSDictionary<NSString, NSObject>[] { dataToSend }, new UIPasteboardOptions
            //    {
            //        LocalOnly = true,
            //        ExpirationDate = NSDate.FromTimeIntervalSinceNow(PASTEBOARD_EXPIRATION_SECONDS)
            //    });
            //}
            //else
            //{
            UIPasteboard.General.SetData(NSKeyedArchiver.ArchivedDataWithRootObject(dataToSend, false, out _), PASTEBOARD_STICKER_PACK_DATA_TYPE);
            //}

            //DispatchQueue.MainQueue.DispatchAsync(() =>
            //{
            //if (CanSend())
            //{
            //    if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            //    {
            //        UIApplication.SharedApplication.OpenUrl(new NSUrl(WHATSAPP_URL), new NSDictionary(), null);
            //    }
            //    else
            //    {
            //        UIApplication.SharedApplication.OpenUrl(new NSUrl(WHATSAPP_URL));
            //    }
            //}
            //});

            await Task.Delay(1000);

            UIApplication.SharedApplication.OpenUrl(new NSUrl(WHATSAPP_URL));

            //return true;
        }

        static void CopyImageToPasteboard(UIImage image)
        {
            UIPasteboard.General.Image = image;
        }

        public static byte[] PngToWebp(byte[] img)
        {
            using (var stream = new MemoryStream(img))
            using (var bitmap = SKBitmap.Decode(stream))
            {
                using (MemoryStream memStream = new MemoryStream())
                using (SKManagedWStream wstream = new SKManagedWStream(memStream))
                {
                    SKPixmap.Encode(wstream, bitmap, SKEncodedImageFormat.Webp, 100);
                    return memStream.ToArray();
                }
            }
        }
    }
}
