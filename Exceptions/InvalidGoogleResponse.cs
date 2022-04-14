using System;

namespace BlazoredRecaptcha.Exceptions
{
    /// <summary>
    ///     Thrown when Google responds with a non-OK HTTP status code
    /// </summary>
    public class InvalidGoogleResponse : Exception
    {
        public InvalidGoogleResponse()
        {
        }

        public InvalidGoogleResponse(string message)
            : base(message)
        {
        }

        public InvalidGoogleResponse(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}