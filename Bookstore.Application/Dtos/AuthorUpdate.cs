﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Dtos;

public record AuthorUpdate(long AuthorId, string FirstName, string LastName);

