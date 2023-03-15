using System;
using PostAssessmentAPI.Model;

namespace PostAssessmentAPI.IService
{
	public interface IPostAssessmentService
	{
		Task<Response> GetPost(PostRequest request);
	}
}

