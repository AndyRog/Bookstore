using System.Runtime.Serialization;

namespace Bookstore.Application.Exceptions;

[Serializable]
public class BookForIsbnDublicateException : Exception
{
    public BookForIsbnDublicateException()
    {
    }

    public BookForIsbnDublicateException(string? message) : base(message)
    {
    }

    public BookForIsbnDublicateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected BookForIsbnDublicateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}