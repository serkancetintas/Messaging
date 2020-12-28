﻿using System.Threading.Tasks;

namespace Armut.Messaging.Application.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }
}