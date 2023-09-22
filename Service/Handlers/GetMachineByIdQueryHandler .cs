using Dapper;
using Data.Abstraction;
using MediatR;
using Service.Common;
using Service.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Handlers
{
    public class GetMachineByIdQueryHandler : IRequestHandler<GetMachineByIdQuery, GetMachineDTO>
    {
        private readonly IDapperHelper _dapperHelper;

        public GetMachineByIdQueryHandler(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<GetMachineDTO> Handle(GetMachineByIdQuery request, CancellationToken cancellationToken)
        {
            // Modify the SQL query to retrieve a machine by its ID.
            string query = @"
                SELECT mc.Id, c.Name AS CPUName, b.Name AS BrandName, r.Name AS RamName
                FROM Machine mc
                JOIN CPU c ON c.Id = mc.IdCPU
                JOIN Brand b ON b.Id = mc.IdBrand
                JOIN Ram r ON r.Id = mc.IdRam
                WHERE mc.Id = @MachineId"; // Add a WHERE clause to filter by ID.

            var parameters = new DynamicParameters();
            parameters.Add("@MachineId", request.MachineId, DbType.Int64); // Define the parameter type explicitly.

            return await _dapperHelper.ExecuteSqlReturnSingle<GetMachineDTO>(query, parameters);
        }
    }
}
