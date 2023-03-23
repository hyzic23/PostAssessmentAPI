using Microsoft.Extensions.Options;
using PostAssessmentAPI.Config;
using PostAssessmentAPI.Exceptions;
using PostAssessmentAPI.IService;
using PostAssessmentAPI.Model;

namespace PostAssessmentAPI.Service
{
    public class PostAssessmentService : IPostAssessmentService
    {
        private readonly AppSettings _appSettings;
        public PostAssessmentService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<Response> GetPost(PostRequest request)
        {
            List<PostResponse> postResponse = new List<PostResponse>();
            Response response = new Response();
            string PostUrl = _appSettings.PostAssessmentUrl;


            try
            {
                HttpClient client = new HttpClient();

                //Tags seperated by commas;
                string[] tags = request.Tags.Split(',');

                foreach (var item in tags)
                {
                    string url = $"{PostUrl}?tag={item}";
                    HttpResponseMessage httpResponse = await client.GetAsync(url);

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new PostAssessmentException(string.Format($"Failed to retrieve posts. - { httpResponse.StatusCode}"));
                    }

                    using (HttpContent content = httpResponse.Content)
                    {
                        string jsonString = await content.ReadAsStringAsync();
                        if(string.IsNullOrEmpty(jsonString))
                        {
                            return new Response();
                        }
                            response = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(jsonString);
                            postResponse.AddRange(response.Posts);
                    }
                }

                //Remove Duplicates
                var distinctPosts = postResponse.GroupBy(p => p.id).Select(g => g.First());

                //Sort Posts
                List<PostResponse> postResults = SortPost(request.SortBy, request.Direction, distinctPosts);
                response.Posts = postResults;

            }
            catch (Exception ex)
            {
                throw new PostAssessmentException(string.Format($"Exception is : { ex.Message}"));
            }
            return response;

        }


        //Method to Sort 
        private static List<PostResponse> SortPost(string sortBy, string direction, IEnumerable<PostResponse> posts)
        {
            var postsResults = posts.AsQueryable();

            switch (sortBy.ToLower())
            {
                case "id":
                    return direction.ToLower() == "asc" ? postsResults.OrderBy(p => p.id).ToList()  : postsResults.OrderByDescending(p => p.id).ToList();
                case "reads":
                    return direction.ToLower() == "asc" ? postsResults.OrderBy(p => p.reads).ToList() : postsResults.OrderByDescending(p => p.reads).ToList();
                case "likes":
                    return direction.ToLower() == "asc" ? postsResults.OrderBy(p => p.likes).ToList(): postsResults.OrderByDescending(p => p.likes).ToList();
                case "popularity":
                    return direction.ToLower() == "asc"? postsResults.OrderBy(p => p.popularity).ToList() : postsResults.OrderByDescending(p => p.popularity).ToList();
                default:
                    throw new ArgumentException($"Invalid sortBy parameter value: {sortBy}");
            }
        }

    }



    

 

}

