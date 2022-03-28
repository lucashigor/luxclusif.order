namespace luxclusif.order.application.Models
{
    public class DefaultResponseDto<T> where T : class
    {
        public DefaultResponseDto()
        {
            Errors = new List<ErrorModel>();
            Warnings = new List<ErrorModel>();
            Success = true;
            Data = null!;
        }

        public DefaultResponseDto(T data)
        {
            Errors = new List<ErrorModel>();
            Data = data;
            Success = true;
            Warnings = new List<ErrorModel>();
        }

        public DefaultResponseDto(T data, List<ErrorModel> errors)
        {
            Errors = new List<ErrorModel>();
            Data = data;

            if (errors.Any())
            {
                Errors.AddRange(errors);
            }

            Warnings = new List<ErrorModel>();

            Success = true;
        }

        public bool Success { get; set; }
        public T Data { get; set; }
        public List<ErrorModel> Errors { get; private set; }
        public List<ErrorModel> Warnings { get; private set; }
    }
}
