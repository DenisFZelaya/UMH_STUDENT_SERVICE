using UMH_STUDENT_SERVICE._02_LogicLayer;
using UMH_STUDENT_SERVICE.Models;
using UMH_STUDENT_SERVICE.Utils;

namespace UMH_STUDENT_SERVICE._03_BussinesLayer
{
    public interface IBlUniversity
    {
        CustomJsonResult InitialData(string encodingCredential);
    }

    public class BlUniversity : IBlUniversity
    {
        private readonly ILlUniversity _LlUniversity;

        public BlUniversity(ILlUniversity LlUniversity)
        {
            _LlUniversity = LlUniversity;
        }

        public CustomJsonResult InitialData(string encodingCredential) => _LlUniversity.InitialData(encodingCredential);

    }
}
