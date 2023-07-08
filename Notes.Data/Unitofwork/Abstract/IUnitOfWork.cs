﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Unitofwork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();
    }
}
