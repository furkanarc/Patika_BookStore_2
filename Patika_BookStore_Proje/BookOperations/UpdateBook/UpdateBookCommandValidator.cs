using System;
using FluentValidation;

namespace Patika_BookStore_Proje.BookOperations.UpdateBook{

    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command=> command.BookId).GreaterThan(0);
            RuleFor(command=> command.Model.GenreId).GreaterThan(0);
            RuleFor(command=> command.Model.Title).NotEmpty().MinimumLength(0);



        }
    }

}