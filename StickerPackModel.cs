using System.Collections.Generic;
using BmojiApp.Logics.DTOs.Response;
using Newtonsoft.Json;

namespace BmojiApp.Logics.Models
{
    public class StickerPackModel : CommonResponse
    {
        public StickerPackModel()
        {
            StickerPacks = new List<StickerPack>();
        }

        [JsonProperty("android_play_store_link")]
        public string AndroidPlayStoreLink { get; set; }

        [JsonProperty("ios_app_store_link")]
        public string IosAppStoreLink { get; set; }

        [JsonProperty("sticker_packs")]
        public IList<StickerPack> StickerPacks { get; set; }
    }

    public class Sticker
    {
        public string ImageUrl { get; set; }
        public string ImagePath { get; set; }

        [JsonProperty("image_file")]
        public string ImageFile { get; set; }

        [JsonProperty("emojis")]
        public IList<string> Emojis { get; set; }
    }

    public class StickerPack
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("tray_image_file")]
        public string TrayImageFile { get; set; }

        [JsonProperty("publisher_email")]
        public string PublisherEmail { get; set; }

        [JsonProperty("publisher_website")]
        public string PublisherWebsite { get; set; }

        [JsonProperty("privacy_policy_website")]
        public string PrivacyPolicyWebsite { get; set; }

        [JsonProperty("license_agreement_website")]
        public string LicenseAgreementWebsite { get; set; }

        [JsonProperty("stickers")]
        public IList<Sticker> Stickers { get; set; }
    }
}
