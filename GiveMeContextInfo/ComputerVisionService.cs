using System;
using System.Threading.Tasks;

using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;

using Plugin.Media.Abstractions;

namespace GiveMeContextInfo
{
    public static class ComputerVisionService
    {
        public static async Task<string> RecognizeTextAsync (MediaFile photo)
        {
            VisionServiceClient _visionServiceClient = new VisionServiceClient(Constants.COMPUTER_VISION_KEY, Constants.COMPUTER_VISION_ROOT);

            OcrResults ocrResults;
            using (var photoStream = photo.GetStream ())
            {
                ocrResults = await _visionServiceClient.RecognizeTextAsync (photoStream);
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
