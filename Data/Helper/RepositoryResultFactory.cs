using Data.Models;

namespace Data.Helper;

#region ChatGPT Advice

    public static class RepositoryResultFactory
    {
        public static RepositoryResult<T> Error<T>(int statusCode, string error)
        {
            return new RepositoryResult<T>
            {
                Succeeded = false,
                StatusCode = statusCode,
                Error = error,
                Result = default
            };
        }

        public static RepositoryResult<T> Success<T>(T data, int statusCode = 200)
        {
            return new RepositoryResult<T>
            {
                Succeeded = true,
                StatusCode = statusCode,
                Result = data
            };
        }
    }

#endregion