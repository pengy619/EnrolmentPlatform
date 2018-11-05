using Newtonsoft.Json;
using System;

namespace EnrolmentPlatform.Project.Infrastructure
{
    [Serializable]
    public class TokenResultMsg : HttpResponseMsg
    {
        public Token Result
        {
            get
            {
                if (StatusCode == 200)
                {
                    return JsonConvert.DeserializeObject<Token>(Data.ToString());
                }

                return null;
            }
        }
    }
}
