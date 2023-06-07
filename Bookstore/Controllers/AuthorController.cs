using Bookstore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        public AuthorCreateService AuthorCreateService { get; }
        public AuthorUpdateService AuthorUpdateService { get; }

        public AuthorController(AuthorCreateService authorCreateService, AuthorUpdateService authorUpdateService)
        {
            AuthorCreateService = authorCreateService;
            AuthorUpdateService = authorUpdateService;
        }



    }
}
