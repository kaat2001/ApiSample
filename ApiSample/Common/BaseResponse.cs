namespace Common
{
    public class BaseResponse<T>
    {        
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

        public T? Value { get; set; }

        public BaseResponse() { }
       
        public BaseResponse(T value)
        {
            IsSuccess = true;
            ErrorMessage = string.Empty;
            this.Value = value;
        }

        public BaseResponse(bool isSuccess, string? errorMessage, T value)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Value = value;
        }
    }
}
