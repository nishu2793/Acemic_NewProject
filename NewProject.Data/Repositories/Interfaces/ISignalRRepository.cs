﻿using NewProject.Data.Contexts;
using NewProject.Domain.Entities.SignalR;
using NewProject.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Data.Repositories.Interfaces
{
    public interface ISignalRRepository<TContext> : IBaseRepository<SignalR, TContext> where TContext : IBaseContext
    {

    }
}
