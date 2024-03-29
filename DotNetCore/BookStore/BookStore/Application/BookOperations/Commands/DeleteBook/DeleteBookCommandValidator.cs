﻿using System;
using System.Linq;
using BookStore.DBOperations;
using FluentValidation;

namespace BookStore.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
           
        }
    }
}
