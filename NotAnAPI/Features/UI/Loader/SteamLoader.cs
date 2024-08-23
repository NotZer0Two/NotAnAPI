using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NotAnAPI.Features.UI.Loader
{
    /// <summary>
    /// Contacts a free steamAPI that lets get the icon from the user
    /// </summary>
    public class SteamLoader
    {
        private static string TextPixel = "█";

        public static async Task<string> FetchPlayerIcon(string stid, int lunghezza, int altezza)
        {

            HttpClient profile = new HttpClient();
            HttpResponseMessage pfpResponse = await profile.GetAsync($"https://api.findsteamid.com/steam/api/summary/{stid}");

            pfpResponse.EnsureSuccessStatusCode();

            string json = await pfpResponse.Content.ReadAsStringAsync();
            List<Dictionary<string, object>> response = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);

            HttpResponseMessage pfpResp = await profile.GetAsync((string)response[0]["avatarfull"]);

            pfpResp.EnsureSuccessStatusCode();
            var readData = pfpResp.Content.ReadAsStreamAsync();
            readData.Wait();

            Image image = Image.FromStream(readData.Result);
            if (image is not null && image is Image)
            {
                Bitmap ImageConvert = new Bitmap(image, new Size(lunghezza, altezza));

                return ConvertToTMPCode(ImageConvert);
            }
            return "";
        }

        public static string ConvertToTMPCode(Bitmap frame)
        {
            int height = frame.Height;
            int width = frame.Width;
            StringBuilder codeBuilder = new StringBuilder();

            Color previousColor = Color.Empty;
            StringBuilder colorBlock = new StringBuilder();

            for (int y = 0; y < height; y++)
            {
                colorBlock.Clear(); // Clear the color block for each new row
                previousColor = Color.Empty; // Reset previous color for new row

                for (int x = 0; x < width; x++)
                {
                    Color pixelColor = frame.GetPixel(x, y);

                    if (pixelColor != previousColor)
                    {
                        if (colorBlock.Length > 0)
                        {
                            codeBuilder.Append(GetColoredBlock(colorBlock.ToString(), previousColor));
                            colorBlock.Clear();
                        }
                    }

                    colorBlock.Append(TextPixel);
                    previousColor = pixelColor;
                }
                codeBuilder.Append(GetColoredBlock(colorBlock.ToString(), previousColor)).Append("\n");
            }
            //Idk how to center this shit
            string codeStr = "<line-height=89%>" + codeBuilder.ToString() + "";
            string done = CompressTMP(codeStr);
            return done;
        }

        public static string CompressTMP(string unccode)
        {
            string[] replacements = {
                "#ffffff:#fff",
                "#000000:#000",
                "#ff0000:red",
                "#ffff00:yellow",
                "#00ff00:green",
                "#00ffff:cyan",
                "#0000ff:blue"
            };

            string compressedCode = unccode;
            foreach (var replacement in replacements)
            {
                string[] parts = replacement.Split(':');
                compressedCode = compressedCode.Replace(parts[0], parts[1]);
            }

            return compressedCode;
        }

        private static string GetColoredBlock(string content, Color color)
        {
            string hexValue = "#" + RgbToHex(color);
            return $"<color={hexValue}>{content}</color>";
        }

        public static string RgbToHex(Color color)
        {
            return color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
    }
}
