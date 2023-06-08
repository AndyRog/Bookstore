using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities;

public class Author : IEquatable<Author>
{
    public long Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public List<Book> Books { get; set; } = default!;

    public bool Equals(Author? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if(ReferenceEquals(this, other)) return true;
        return other.FirstName == FirstName && other.LastName == LastName;    

    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if(obj.GetType() != GetType()) return false;
        return Equals(obj as Author);
    }

    public override int GetHashCode()
    {
       return (FirstName,  LastName).GetHashCode();
    }


}
