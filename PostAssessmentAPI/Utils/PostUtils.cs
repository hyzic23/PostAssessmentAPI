using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostAssessmentAPI.Enums;

namespace PostAssessmentAPI.Utils
{
	public static class PostUtils
	{


		public static Boolean IsSortByValid(string sortBy)
		{
			Boolean isValid = false;
            if (String.IsNullOrEmpty(sortBy))
            {
                isValid = false;
                return isValid;
            }
            else
            {
                
               var check = Enum.IsDefined(typeof(SortBy), sortBy);
                if (check)
                {
                    isValid = true;
                }
            }
			return isValid;
		}


        public static Boolean IsDirectionByValid(string direction)
        {
            Boolean isValid = false;
            if (String.IsNullOrEmpty(direction))
            {
                isValid = false;
                return isValid;
            }
            else
            {
                var check = Enum.IsDefined(typeof(Direction), direction);
                if (check)
                {
                    isValid = true;
                }
            }
            return isValid;
        }
    }


}

