using Microsoft.EntityFrameworkCore;
using System.Text;
using UMH_STUDENT_SERVICE._01_DataLayer.DataLayerEntities.Helper;
using UMH_STUDENT_SERVICE.Context;
using UMH_STUDENT_SERVICE.Models;
using UMH_STUDENT_SERVICE.Utils;

namespace UMH_STUDENT_SERVICE._02_LogicLayer
{
    public interface ILlUniversity
    {
        CustomJsonResult InitialData(string tokenUniversity);
    }
    public class LlUniversity : ILlUniversity
    {
        private readonly appDbContext _context;

        public LlUniversity(appDbContext context)
        {
            _context = context;
        }

        public CustomJsonResult InitialData(string tokenUniversity)
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

                response.SuccesMessage = "Data returned sucessfully";
                response.Result = new
                {
                    InitialDataRetrieve = new
                    {
                        user_type = _context.UsertTypes.Where(c => c.Name.Equals("Student")).ToList(),
                        campus = _context.Campuses.ToList(),
                        career = _context.Careers.ToList(),
                        career_campus = _context.CareerCampuses.ToList(),
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
    }
}
