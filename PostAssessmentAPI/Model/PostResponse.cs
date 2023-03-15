using System;
namespace PostAssessmentAPI.Model
{
	public class PostResponse
    {
        public string author { get; set; }
        public int authorId { get; set; }
        public int id { get; set; }
        public int likes { get; set; }
        public double popularity { get; set; }
        public int reads { get; set; }
        public List<string> tags { get; set; }
    }
}

