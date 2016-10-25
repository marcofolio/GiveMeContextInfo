using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.ProjectOxford.EntityLinking;

namespace GiveMeContextInfo
{
    public static class EntityLinkingService
    {
        // Get your key at: https://www.microsoft.com/cognitive-services/en-us/entity-linking-intelligence-service
        private const string ENTITY_LINKING_API_KEY = "YOUR_API_KEY";

        public static async Task<List<EntityLink>> LinkEntityAsync (string text)
        {
            var client = new EntityLinkingServiceClient (ENTITY_LINKING_API_KEY);
            var linkResponse = await client.LinkAsync (text);

            var result = new List<EntityLink> ();
            foreach (var link in linkResponse)
            {
                result.Add (new EntityLink ()
                {
                    Name = link.Name,
                    Score = link.Score,
                    WikipediaID = link.WikipediaID
                });
            }
            return result;
        }
    }
}
