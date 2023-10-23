namespace WebAPI.Models.Shared
{
    public class CommonResultDto<T>
    {
        public bool IsSuccessful { get; set; }
        public T Result { get; set; }
        public string ErrorMessage { get; set; }

        public CommonResultDto(string message)
        {
            IsSuccessful = false;
            ErrorMessage = message;
        }

        public CommonResultDto(T result)
        {
            IsSuccessful = true;
            Result = result;
        }
    }
}
