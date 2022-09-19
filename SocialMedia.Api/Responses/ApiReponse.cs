namespace SocialMedia.Api.Responses
{
    public class ApiReponse<T>
    {
        public ApiReponse(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
