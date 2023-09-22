using Dapper;
using Data.Abstraction;
using MediatR;
using Service.Command;
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
    public class UpdateMachineByIdCommandHandler : IRequestHandler<UpdateMachineCommand, bool>
    {
        private readonly IDapperHelper _dapperHelper;

        public UpdateMachineByIdCommandHandler(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<bool> Handle(UpdateMachineCommand request, CancellationToken cancellationToken)
        {
            // Modify the SQL query to update a machine by its ID.
            string query = @"
                UPDATE Machine
                SET IdCpu = @IdCpu, IdBrand = @IdBrand, IdRam = @IdRam, MachineState = @MachineState
                WHERE Id = @MachineId";

            var parameters = new DynamicParameters();
            parameters.Add("@MachineId", request.MachineId, DbType.Int32); // Define the parameter type explicitly.
            parameters.Add("@IdCpu", request.Machine.idCpu, DbType.Int32);
            parameters.Add("@IdBrand", request.Machine.idBrand, DbType.Int32);
            parameters.Add("@IdRam", request.Machine.idRam, DbType.Int32);
            parameters.Add("@MachineState", request.Machine.MachineState, DbType.Int32);

            // Execute the SQL query and return true if the update was successful.
            /*var affectedRows = await _dapperHelper.ExecuteNotReturnAsync(query, parameters);
            return affectedRows > 0;*/
            await _dapperHelper.ExecuteNotReturnAsync(query, parameters); // Use await here to await the completion.
            return true;
        }
    }
}
