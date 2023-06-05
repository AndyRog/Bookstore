using System.Runtime.Serialization;

namespace Bookstore.Application.Exceptions;

[Serializable]
public class IsbnDublicateException : Exception
{
    public IsbnDublicateException()
    {
    }

    public IsbnDublicateException(string? message) : base(message)
    {
    }

    public IsbnDublicateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected IsbnDublicateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}