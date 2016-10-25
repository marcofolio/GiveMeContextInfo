using System;
namespace GiveMeContextInfo
{
    public class EntityLink
    {
        public string Name { get; set; }
        public string WikipediaID { get; set; }
        public double Score { get; set; }
        public string ScoreText
        {
            get
            {
                return $"{Math.Ceiling (Score * 100)}% sure" ;
            }
        }
    }
}
