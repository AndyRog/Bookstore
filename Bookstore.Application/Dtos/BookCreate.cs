using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Dtos;

public record BookCreate(string isbn, string titel, long AuthorId, int Quantity );

