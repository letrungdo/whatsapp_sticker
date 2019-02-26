using System;
using System.IO;
using System.Threading.Tasks;
using BmojiApp.DependancyServices;
using BmojiApp.iOS.Services.WhatsApp;
using BmojiApp.Logics.Constants;
using BmojiApp.Logics.Models;
using Foundation;
using Newtonsoft.Json;
using UIKit;
using Xamarin.Essentials;
using AppInfo = BmojiApp.Logics.Utility.AppInfo;

[assembly: Xamarin.Forms.Dependency(typeof(WhatsAppStickerService))]
namespace BmojiApp.iOS.Services.WhatsApp
{
    public class WhatsAppStickerService : IWhatsAppService
    {
        public async Task AddStickerToWhatsApp(string identifier)
        {
            foreach (var stickerPack in AppInfo.StickerPackModel.StickerPacks)
            {
                if (!stickerPack.Identifier.Equals(identifier))
                    continue;

                var data = new NSMutableDictionary<NSString, NSObject>();
                var stickersArray = new NSMutableArray { };

                data["ios_app_store_link"] = NSObject.FromObject("");
                data["android_play_store_link"] = NSObject.FromObject("");

                data["identifier"] = NSObject.FromObject(stickerPack.Identifier);
                data["name"] = NSObject.FromObject(stickerPack.Name);
                data["publisher"] = NSObject.FromObject(stickerPack.Publisher);

                byte[] b = File.ReadAllBytes(stickerPack.TrayImageFile);
                data["tray_image"] = NSObject.FromObject(Convert.ToBase64String(b));

                for (int i = 0; i < stickerPack.Stickers.Count; i++)
                {
                    string imagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), stickerPack.Stickers[i].ImageFile);
                    byte[] imageData = File.ReadAllBytes(imagePath);
                    var keys = new[]
                    {
                        new NSString("image_data"),
                        new NSString("emojis")
                    };

                    var values = new NSObject[]
                    {
                        NSObject.FromObject(Convert.ToBase64String(imageData)),
                        NSObject.FromObject(""),
                    };
                    var stickerDict = new NSDictionary<NSString, NSObject>(keys, values);

                    stickersArray.Add(stickerDict);
                }

                data["stickers"] = NSObject.FromObject(stickersArray);

                var dataToSend = NSDictionary<NSString, NSObject>.FromObjectsAndKeys(data.Values, data.Keys);

                Interoperability.Send(dataToSend);
            }
        }

        private void GetStickerList()
        {
            var data = Preferences.Get(StorageConst.STORAGE_KEY_STICKER, null);
            if (data != null)
            {
                AppInfo.StickerPackModel = JsonConvert.DeserializeObject<StickerPackModel>(data);
            }
        }
    }
}
