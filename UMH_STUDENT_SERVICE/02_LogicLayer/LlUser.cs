using Microsoft.EntityFrameworkCore;
using System.Text;
using UMH_STUDENT_SERVICE._01_DataLayer.DataLayerEntities.Helper;
using UMH_STUDENT_SERVICE.Context;
using UMH_STUDENT_SERVICE.Models;
using UMH_STUDENT_SERVICE.Utils;

namespace UMH_STUDENT_SERVICE._02_LogicLayer
{
    public interface ILlUser
    {
        CustomJsonResult RegisterUser(string tokenUniversity, User user);
        CustomJsonResult LoginUser(string encodingCredential);
        CustomJsonResult UpdatePassword(string encodingCredential, string newPassword);
    }
    public class LlUser : ILlUser
    {
        private readonly appDbContext _context;

        public LlUser(appDbContext context)
        {
            _context = context;
        }

        public CustomJsonResult LoginUser(string encodingCredential)
        {
            // Funcionality
            CustomJsonResult response = new CustomJsonResult();

            if (_context.Users == null)
            {
                response.Error = ErrorMessages.NotFound;
                return response;
            }

            var encodingParams = encodingCredential.Replace("Basic ", "");
            var encodedTextBytes = Convert.FromBase64String(encodingParams);
            string plainText = Encoding.UTF8.GetString(encodedTextBytes);

            string[] authUser = plainText.Split(":");
            string accountParams = Security.SanitizeString(authUser[0]);
            string passParams = Security.SanitizeString(authUser[1]);

            // Validate fields
            if (InsertValidateFields.LoginValidateFields(accountParams, passParams).Count > 0)
            {
                response.Error = ErrorMessages.FieldsNoValid;
                string concatString = "";
                foreach (var item in InsertValidateFields.LoginValidateFields(accountParams, passParams))
                {
                    concatString = item + ", " + concatString;
                }
                response.InfoMessage = concatString;
                return response;
            }

            // Exist account
            User user = _context.Users.Where(c => c.NumberAcount.Equals(accountParams)).FirstOrDefault();
            if (user == null)
            {
                response.Error = "No exist account number " + accountParams;
                response.InfoMessage = "Account number not found.";
                return response;
            }

            // Decript password comparation false
            if (!Security.DecodeHash(user.Password).Equals(passParams))
            {
                response.Error = ErrorMessages.Unauthorized;
                return response;
            }

            // Return data
            response.Result = new
            {
                loginResponse = new
                {
                    studentInformation = new
                    {
                        generalData = new
                        {
                            user.FullName,
                            user.NumberAcount,
                            user.Gender,
                            emai = user.NumberAcount + ConstantConfig.tagMail,
                            user.RegistrerDate
                        },
                        type = _context.UsertTypes.Where(c => c.Id.Equals(user.IdUserType)).FirstOrDefault(),
                        career = _context.Careers.Where(c => c.Id.Equals(user.IdCareer)).FirstOrDefault(),
                        status = _context.UserStatuses.Where(c => c.Id.Equals(user.IdUserStatus)).FirstOrDefault(),
                    },
                    universityInformation = new
                    {
                        name = ConstantConfig.universityName,
                        campus = _context.Campuses.Where(c => c.Id.Equals(user.IdCampus)).FirstOrDefault(),

                    },
                    token = encodingCredential + ConstantConfig.tokenUniversidad
                }
            };

            return response;
        }
        public CustomJsonResult RegisterUser(string tokenUniversity, User user)
        {
            // Funcionality
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                // Token es valido?
                if (!TokenService.TokenUniversityValid(tokenUniversity))
                {
                    // TODO: LOG
                    response.Error = ErrorMessages.Unauthorized;
                    return response;
                }

                //No existe la tabla
                if (_context.Users == null)
                {
                    response.Error = "Entity set 'appDbContext.Users'  is null.";
                    return response;
                }

                // Add Date registrer
                user.RegistrerDate = DateTime.Now.ToString();

                // Sanitize data
                user.Password = Security.SanitizeString(user.Password);
                user.NumberAcount = Security.SanitizeString(user.NumberAcount);

                //Validate fields
                if (InsertValidateFields.UserValidateFields(user, _context).Count > 0)
                {
                    response.Error = "Fields no valids";
                    string concatString = "";
                    foreach (var item in InsertValidateFields.UserValidateFields(user, _context))
                    {
                        concatString = item + ", " + concatString;
                    }
                    response.InfoMessage = concatString;
                    return response;
                }

                //Encode data from user
                user.Password = Security.EncodeHash(user.Password);

                //Add user
                _context.Users.Add(user);
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    if (UserExists(user.NumberAcount))
                    {
                        response.Error = ErrorMessages.Conflict + " " + ex.Message;
                        response.Exception = ex.Message;
                        return response;
                    }
                    else
                    {
                        response.Error = ex.Message;
                        return response;
                    }
                }

                response.SuccesMessage = "User created sucessfully";
                response.Result = new
                {
                    createUserResponse = new
                    {
                        status = true
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                response.Error = ErrorMessages.BadRequest + " " + ex.Message;
                return response;
            }
        }
        public CustomJsonResult UpdatePassword(string encodingCredential, string newPassword)
        {
            // Funcionality
            CustomJsonResult response = new CustomJsonResult();

            try
            {
                var encodingParams = encodingCredential.Replace("Basic ", "");
                encodingParams = encodingParams.Replace(ConstantConfig.tokenUniversidad, "");
                var encodedTextBytes = Convert.FromBase64String(encodingParams);
                string plainText = Encoding.UTF8.GetString(encodedTextBytes);

                string[] authUser = plainText.Split(":");
                string accountParams = Security.SanitizeString(authUser[0]);
                string passParams = Security.SanitizeString(authUser[1]);

                User user = _context.Users.Where(c => c.NumberAcount.Equals(accountParams)).FirstOrDefault();

                // Validate length newPassword
                if(newPassword.Length < ConstantConfig.passwordLength)
                {
                    response.Error = "Password less than " + ConstantConfig.passwordLength;
                    return response;
                }

                // If table no exist
                if (user == null)
                {
                    response.Error = ErrorMessages.NotFound;
                    return response;
                }

                // Password is correct
                if (Security.DecodeHash(user.Password).Equals(passParams))
                {
                    user.Password = Security.EncodeHash(newPassword);
                    _context.Entry(user).State = EntityState.Modified;

                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        response.Error = ErrorMessages.NotFound;
                        return response;
                    }

                    response.SuccesMessage = "UserPassword updated sucessfully";
                    response.Result = new
                    {
                        updatePasswordResponse = new
                        {
                            status = true,
                            token = encodingCredential + ConstantConfig.tokenUniversidad
                        }
                    };

                    return response;
                }
                else
                {
                    response.Error = ErrorMessages.Unauthorized;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
                return response;
            }
        }
        private bool UserExists(string id)
        {
            return (_context.Users?.Any(e => e.NumberAcount == id)).GetValueOrDefault();
        }
    }
}
