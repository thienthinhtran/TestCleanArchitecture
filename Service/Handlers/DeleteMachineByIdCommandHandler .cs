using Dapper;
using Data.Abstraction;
using MediatR;
using Service.Command;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Handlers
{
    public class DeleteMachineByIdCommandHandler : IRequestHandler<DeleteMachineCommand, bool>
    {
        private readonly IDapperHelper _dapperHelper;

        public DeleteMachineByIdCommandHandler(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<bool> Handle(DeleteMachineCommand request, CancellationToken cancellationToken)
        {
            // Modify the SQL query to delete a machine by its ID.
            string query = @"
                DELETE FROM Machine
                WHERE Id = @MachineId";

            var parameters = new DynamicParameters();
            parameters.Add("@MachineId", request.MachineId, DbType.Int32);

            // Execute the SQL query and return true if the delete was successful.
            await _dapperHelper.ExecuteNotReturnAsync(query, parameters); // Use await here to await the completion.
            return true;
        }
    }
}
