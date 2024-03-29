using System;
using Microsoft.AspNetCore.Mvc;
using BookStore.DBOperations;
using AutoMapper;
using FluentValidation;
using BookStore.Application.AuthorOperations.Queries.GetAuthors;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.Application.AuthorOperations.CreateAuthor;
using BookStore.Application.AuthorOperations.UpdateAuthor;
using BookStore.Application.AuthorOperations.DeleteAuthor;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[Controller]s")]

    public class AuthorController : ControllerBase
    {
        public readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()   
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var obj = query.Handle();
            return Ok(obj);
        }



        [HttpGet("{id}")]
        public IActionResult GetAuthorDetail(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var obj = query.Handle();          
            return Ok(obj);
        }


        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = newAuthor;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }


        [HttpPut("(id)")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel UpdateAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = id;
            command.Model = UpdateAuthor;
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }


        [HttpDelete("id")]
         public IActionResult DeleteAuthor(int id)  
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

    }
}