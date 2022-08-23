using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;

namespace RepositoryPatternWithUOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBaseRepository<Book> _booksRepository;

        public BooksController(IBaseRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_booksRepository.GetById(1));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll ()
        {
            return Ok(_booksRepository.GetAll());
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName()
        {
            return Ok(_booksRepository.Find(b=>b.Title == "New Book", new[] {"Author"}));
        }

        [HttpGet("GetAllWithAuthors")]
        public IActionResult GetAllWithAuthors()
        {
            return Ok(_booksRepository.FindAll(b => b.Title.Contains("New Book") , new[] { "Author" }));
        }

    }
}
