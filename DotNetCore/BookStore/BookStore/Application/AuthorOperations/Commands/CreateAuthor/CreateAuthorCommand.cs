using System;
using System.Linq;
using BookStore.DBOperations;
using BookStore.Entities;
using AutoMapper;

namespace BookStore.Application.AuthorOperations.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;


        public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public void Handle()
        {
             var author =_context.Authors.SingleOrDefault(x=>x.Name== Model.Name && x.Surname==Model.Surname);
                if (author is not null)
                    throw new InvalidOperationException("That Author already exists");

                author = _mapper.Map<Author>(Model);

                _context.Authors.Add(author);
                _context.SaveChanges();



        }

    }

    public class CreateAuthorModel
    {
        public string Name { get; set; } 
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
    }
}