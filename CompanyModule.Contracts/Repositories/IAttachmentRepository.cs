using CompanyModule.Domain.Entities;
using Shared.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Contracts.Repositories
{
    public interface IAttachmentRepository : IBaseEntityRepository<Attachment>
    {
    }
}
