﻿using Evol.TMovie.Domain.Commands.Dto;
using Evol.Domain.Commands;

namespace Evol.TMovie.Domain.Commands
{
    public class RoleUpdateCommand : Command
    {
        public RoleCreateOrUpdateDto Input { get; set; }
    }
}
