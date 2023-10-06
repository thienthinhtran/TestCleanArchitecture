using Dapper;
using Data.Abstraction;
using MediatR;
using Service.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IUserService _userService;
        private readonly IDapperHelper _dapperHelper;

        public RegisterUserCommandHandler(IUserService userService, IDapperHelper dapperHelper)
        {
            _userService = userService;
            _dapperHelper = dapperHelper;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra xem tên người dùng đã tồn tại chưa
                var existingUser = await _userService.FindByUsername(request.UserName);
                if (existingUser != null)
                {
                    throw new InvalidOperationException("Username is already taken.");
                }

                // Tạo một đối tượng DynamicParameters và thêm tham số
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", request.UserName);
                parameters.Add("@Password", request.Password);
                parameters.Add("@Role", "User");

                // Thực hiện câu lệnh SQL để chèn người dùng vào cơ sở dữ liệu sử dụng Dapper
                await _dapperHelper.ExecuteNotReturnAsync("INSERT INTO [User] (UserName, Password, Role) VALUES (@UserName, @Password, @Role)", parameters);

                return "Registration successful";
            }
            catch (Exception ex)
            {
                // Xử lý bất kỳ ngoại lệ nào xảy ra trong quá trình đăng ký người dùng
                Console.WriteLine(ex);
                throw; // Có thể xử lý khác tùy theo yêu cầu của bạn
            }
        }
    }
}
