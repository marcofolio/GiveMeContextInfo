using System;
using System.Threading.Tasks;

using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;

using Plugin.Media.Abstractions;

namespace GiveMeContextInfo
{
    public static class ComputerVisionService
    {
        // Get your key at: https://www.microsoft.com/cognitive-services/en-us/computer-vision-api
        private const string COMPUTER_VISION_API_KEY = "YOUR_API_KEY";

        public static async Task<string> RecognizeTextAsync (MediaFile photo)
        {
            OcrResults ocrResults;
            var client = new VisionServiceClient (COMPUTER_VISION_API_KEY);
            using (var photoStream = photo.GetStream ())
            {
                ocrResults = await client.RecognizeTextAsync (photoStream);
            }

            var text = "";
            foreach (var region in ocrResults.Regions)
            {
                foreach (var line in region.Lines)
                {
                    foreach (var word in line.Words)
                    {
                        text = $"{text} {word.Text}";
                    }
                }
            }

            return text;
        }
    }
}
