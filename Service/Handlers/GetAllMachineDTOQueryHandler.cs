using Data.Abstraction;
using MediatR;
using Service.Queries;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Handlers
{
    public class GetAllMachineDTOQueryHandler : IRequestHandler<GetAllMachineDTOQuery, IEnumerable<GetMachineDTO>>
    {
        private readonly IDapperHelper _dapperHelper;

        public GetAllMachineDTOQueryHandler(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<IEnumerable<GetMachineDTO>> Handle(GetAllMachineDTOQuery request, CancellationToken cancellationToken)
        {
            string query = @"
                SELECT mc.Id, c.Name AS CPUName, b.Name AS BrandName, r.Name AS RamName
                FROM Machine mc
                JOIN CPU c ON c.Id = mc.IdCPU
                JOIN Brand b ON b.Id = mc.IdBrand
                JOIN Ram r ON r.Id = mc.IdRam";

            return await _dapperHelper.ExecuteSqlReturnList<GetMachineDTO>(query);
        }
    }
}
