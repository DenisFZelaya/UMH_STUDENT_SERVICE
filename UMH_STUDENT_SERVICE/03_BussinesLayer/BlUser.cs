using UMH_STUDENT_SERVICE._02_LogicLayer;
using UMH_STUDENT_SERVICE.Models;
using UMH_STUDENT_SERVICE.Utils;

namespace UMH_STUDENT_SERVICE._03_BussinesLayer
{
    public interface IBlUser
    {
        CustomJsonResult RegisterUser(string tokenUniversity, User user);
        CustomJsonResult LoginUser(string encodingCredential);
    }

    public class BlUser : IBlUser
    {
        private readonly ILlUser _LlUser;

        public BlUser(ILlUser LlUser)
        {
            _LlUser = LlUser;
        }

        public CustomJsonResult RegisterUser(string tokenUniversity, User user) => _LlUser.RegisterUser(tokenUniversity, user);
        public CustomJsonResult LoginUser(string encodingCredential) => _LlUser.LoginUser(encodingCredential);
    }
}
