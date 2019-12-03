using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataClassLibrary
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public string ProductAdvantages { get; set; }
        public string ProductDisadvantages { get; set; }
        public string ReviewComment { get; set; }
        public float? ProductRating { get; set; }
        public int AuthorId { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}
