using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.TaskItems.Commands.ReopenTaskItem
{
    public class ReopenTaskItemCommandValidator : AbstractValidator<ReopenTaskItemCommand>
    {
        public ReopenTaskItemCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}
