﻿using PracticeModule.Domain.Entity;
using Shared.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeModule.Contracts.Repositories
{
    public interface IGlobalPracticeRepository : IBaseEntityRepository<GlobalPractice> { }
}
