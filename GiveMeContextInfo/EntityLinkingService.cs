using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.ProjectOxford.EntityLinking;

namespace GiveMeContextInfo
{
    public static class EntityLinkingService
    {
        private static EntityLinkingServiceClient _visionServiceClient = new EntityLinkingServiceClient(Constants.ENTITY_LINKING_API_KEY);

        public static async Task<List<EntityLink>> LinkEntityAsync (string text)
        {
            var linkResponse = await _visionServiceClient.LinkAsync (text);

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
