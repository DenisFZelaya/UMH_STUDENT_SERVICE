namespace UMH_STUDENT_SERVICE.Utils
{
    public class TokenService
    {
        public static bool TokenUniversityValid(string tokenUniversity)
        {
            try
            {
                if (tokenUniversity.Equals(ConstantConfig.tokenUniversidad))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
