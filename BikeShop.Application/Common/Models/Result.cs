namespace BikeShop.Application.Common.Models
{
    public enum ResultStatus
    {
        Success,
        Failure
    }

    public class Result
    {
        public ResultStatus Status { get; }
        public Error? Error { get; }

        public bool IsSuccess => Status == ResultStatus.Success;
        public bool IsFailure => !IsSuccess;

        protected Result(ResultStatus status, Error? error = null)
        {
            Status = status;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(ResultStatus.Success, null);
        }

        public static Result Failure(Error error)
        {
            return new Result(ResultStatus.Failure, error);
        }
    }


    public class Result<T> : Result
    {
        public T? Data { get; }

        private Result(ResultStatus status, Error? error = null, T ? data = default) : base(status, error)
        {
            Data = data;
        }

        public static Result<T> Success(T? data)
        {
            return new Result<T>(ResultStatus.Success, null, data);
        }

        public new static Result<T> Failure(Error error)
        {
            return new Result<T>(ResultStatus.Failure, error, default);
        }
    }
}
