using System.Text;
using UMH_STUDENT_SERVICE.Context;
using UMH_STUDENT_SERVICE.Models;
using UMH_STUDENT_SERVICE.Utils;

namespace UMH_STUDENT_SERVICE._01_DataLayer.DataLayerEntities.Helper
{
    public class InsertValidateFields
    {
        public static List<string> UserValidateFields(User user, appDbContext _context)
        {
            List<string> messageFiel = new List<string>();
            
            if(user.Password.Length < ConstantConfig.passwordLength)
            {
                messageFiel.Add("Password can't less to 6");
            }
            if (user.FullName.Length < 6)
            {
                messageFiel.Add("FullName can't less to 6");
            }
            if (user.NumberAcount.Length < ConstantConfig.accountLength)
            {
                messageFiel.Add("NumberAcount can't less to 6");
            }

            if (!_context.Campuses.Any(c => c.Id.Equals(user.IdCampus)))
            {
                messageFiel.Add("Campus no exist");
            }

            if (!_context.UsertTypes.Any(c => c.Id.Equals(user.IdUserType)))
            {
                messageFiel.Add("UserType no exist");
            }

            if (!_context.Careers.Any(c => c.Id.Equals(user.IdCareer)))
            {
                messageFiel.Add("Career no exist");
            }

            if (!_context.UserStatuses.Any(c => c.Id.Equals(user.IdUserStatus)))
            {
                messageFiel.Add("UserStatus no exist");
            }

            return messageFiel;
        }

        public static List<string> LoginValidateFields(string numberAccount, string password)
        {
            List<string> messageFiel = new List<string>();

            if (numberAccount.Length < ConstantConfig.accountLength)
            {
                messageFiel.Add("NumberAccount can't less to " + ConstantConfig.accountLength);
            }
            if (password.Length < ConstantConfig.passwordLength)
            {
                messageFiel.Add("password can't less to " + ConstantConfig.passwordLength);
            }
            return messageFiel;
        }

        public static bool ValidateTokenCredentials(string encodingCredentialAndTokenUni)
        {
            encodingCredentialAndTokenUni = encodingCredentialAndTokenUni.Replace("Basic ", "");
            string encodingCredential = encodingCredentialAndTokenUni.Replace(ConstantConfig.tokenUniversidad, "");
            var encodedTextBytes = Convert.FromBase64String(encodingCredential);
            string plainText = Encoding.UTF8.GetString(encodedTextBytes);

            string[] authUser = plainText.Split(":");
            string accountParams = Security.SanitizeString(authUser[0]);
            string passParams = Security.SanitizeString(authUser[1]);

            return true;
        }
    }
}
