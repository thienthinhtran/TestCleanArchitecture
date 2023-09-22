using Dapper;
using Data.Abstraction;
using MediatR;
using Service.Command;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Handlers
{
    public class InsertMachineCommandHandler : IRequestHandler<InsertMachineCommand, int>
    {
        private readonly IDapperHelper _dapperHelper;

        public InsertMachineCommandHandler(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<int> Handle(InsertMachineCommand request, CancellationToken cancellationToken)
        {
            var machine = request.Machine;

            // Create a DynamicParameters object and map the properties from the InsertMachineDTO.
            var parameters = new DynamicParameters();
            parameters.Add("@IdCpu", machine.idCpu);
            parameters.Add("@IdBrand", machine.idBrand);
            parameters.Add("@IdRam", machine.idRam);
            parameters.Add("@MachineState", machine.MachineState);

            // Implement the logic to insert the new machine with CPU, Brand, and RAM IDs using Dapper.
            // Example SQL code (assuming your table is called 'Machine'):
            var sql = @"
                INSERT INTO Machine (IdCpu, IdBrand, IdRam, MachineState)
                VALUES (@IdCpu, @IdBrand, @IdRam, @MachineState);
                SELECT CAST(SCOPE_IDENTITY() AS INT);"; // Use SCOPE_IDENTITY() to get the last inserted ID.

            // Execute the SQL query and return the generated machine ID.
            var machineId = await _dapperHelper.ExecuteScalarAsync<int>(sql, parameters);

            return machineId;
        }
    }
}
