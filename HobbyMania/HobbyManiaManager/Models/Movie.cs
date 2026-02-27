using System;
using Newtonsoft.Json;

namespace HobbyManiaManager.Models
{
    public class Movie : ICloneable
    {
        
        private const string BaseImageUrl = "https://image.tmdb.org/t/p/w342";

        public int Id { get; set; }

        public string Title { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }
        
        public string Overview { get; set; }
        
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        public string PosterPathUrl => string.IsNullOrEmpty(this.PosterPath) ? null : $"{BaseImageUrl}{PosterPath}";

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }

        public object Clone()
        {
            return new Movie
            {
                Id = this.Id,
                Title = this.Title,
                OriginalTitle = this.OriginalTitle,
                ReleaseDate = this.ReleaseDate,
                Overview = this.Overview,
                PosterPath = this.PosterPath,
                VoteAverage = this.VoteAverage,
                VoteCount = this.VoteCount
            };
        }
    }
}
