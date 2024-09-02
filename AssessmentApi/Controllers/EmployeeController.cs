using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssessmentApi.Data;
using AssessmentApi.Models;
using AssessmentApi.POCO;
using NuGet.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AssessmentApi.Helper;

namespace AssessmentApi.Controllers
{

    //[Route("api/[controller]")
    [Authorize]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AssessmentDbContext _context;
        private readonly IConfiguration _configuration;

        public EmployeeController(AssessmentDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Employee
        /// <summary>
        /// gets a list of Employee records
        /// </summary>
        [Route("api/GetEmployees")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
          if (_context.Employees == null )
          {
              return NotFound();
          }
            return await _context.Employees.ToListAsync();
        }


        /// <summary>
        /// gets Employee record by Employee Id
        /// </summary>
        [Route("api/GetEmployeeById")]
        [HttpGet]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            Employee employee = new Employee();
          try
          { 
              if (_context.Employees == null)
              {
                  return  NotFound();
              }
              employee = await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();

             if (employee == null)
             {
                 return NotFound();
             }

                   // return employee;
          }
          catch (Exception ex)
          {
                throw;
          }
          return employee;
           
        }

        //// PUT: api/Employee/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEmployee(int id, Employee employee)
        //{
        //    if (id != employee.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(employee).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Employee
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        //{
        //    if (_context.Employees == null)
        //    {
        //        return Problem("Entity set 'AssessmentDbContext.Employees'  is null.");
        //    }
        //    _context.Employees.Add(employee);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        //}




        /// <summary>
        /// Add and update Employee information
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [Route("api/AddEmployees")]
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            //if (!_context.Employees.Contains(employee))
            //{
            //    return Problem("Entity set 'AssessmentDbContext.Employees'  is null.");
            //}


            StatusMessage statusMessage = new StatusMessage();

            try
            {
                //check if object empty
                var emp = await _context.Employees.Where(x =>
                                                     x.Id == employee.Id).FirstOrDefaultAsync();

                if (emp != null) // for update payroll policy
                {
                    emp.Email = employee.Email;
                    emp.Name = employee.Name;
                    emp.Username = employee.Username;
                    emp.Password = employee.Password;

                    _context.Entry(emp).State = EntityState.Modified;
                    _context.SaveChanges();

                    statusMessage.Status = "Success";
                    statusMessage.Message = "Success";
                }

                else // insert or create payroll policy
                {
                    _context.Employees.Add(employee);
                    await _context.SaveChangesAsync();
                    //_context.Entry(emp).State = EntityState.Added;
                    //_context.SaveChanges();

                    statusMessage.Status = "Success";
                    statusMessage.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                statusMessage.Status = "Failed";
                statusMessage.Message = ex.Message;
            }
            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }


        // DELETE: api/Employee/5
        /// <summary>
        /// gets a list of Employee records
        /// </summary>
        [Route("api/DeleteEmployeeById")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        //public async Task<(int,string)> Registration(Employee employee, string role)
        //{
        //    var employeeExists = await _context.Employees.Where(x =>
        //                                                 x.Email == employee.Email).FirstOrDefaultAsync();
        //    if (employeeExists != null)
        //        return (0, "User already exist");

        //    Employee employee1 = new();
        //    {
        //        Email = 
        //    }
        //}

        //private bool EmployeeExists(int id)
        //{
        //    return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        

        [AllowAnonymous]
        [Route("api/Login")]
        [HttpPost]
        public async Task<ActionResult<Employee>> Login(string email, string password)
        {
            StatusMessage statusMessage = new StatusMessage();

            if (email == null || email == "")
            {
                statusMessage.Status = "Failed";
                statusMessage.Message = "Enter A Valid Username";
                return Ok(statusMessage);
            }
            else if (password == null || password == "")
            {
                statusMessage.Status = "Failed";
                statusMessage.Message = "Enter A Valid Username";
                return Ok(statusMessage);
            }
            try
            {

                var employee = await _context.Employees.Where(x => 
                                                         x.Email == email &&
                                                         x.Password == password).FirstOrDefaultAsync();

                var tokenString = generateToken(email);
                if (employee != null)
                {
                    statusMessage.Status = "True";
                    statusMessage.Message = "Success";
                    statusMessage.data = employee;
                    statusMessage.auth_token = tokenString;
                    return Ok(statusMessage);
                }
                else
                {
                    return BadRequest("Invalid Email or Password");
                }
            }
            catch (Exception ex)
            {
                statusMessage.Status = "Failed";
                statusMessage.Message = ex.Message;
            }
            return Ok(statusMessage);
        }


        [AllowAnonymous]
        [HttpGet("TestLogin")]
        public async Task<ActionResult<Employee>> TestLogin(string Email, string Password)
        {
            if (Email != null || Password != null)
            {
                var employee = await _context.Employees.Where(x => x.Email == Email &&
                                                                  x.Password == Password).FirstOrDefaultAsync();
                return employee != null ? Ok(employee) : NotFound();

                //if(employee != null)
                //{
                //    return Ok(employee)
                //}
            }
            else
            {
                return BadRequest("Invalid Request");
            }
        }

        [Route("api/generateToken")]
        [HttpPost]
        public string generateToken(string Username)
        {
            try
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
                var TokenExpiryTimeHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInSeconds"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {

                    Issuer = _configuration["JWTKey:ValidIssuer"],
                    Audience = _configuration["JWTKey:ValidAudience"],
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                    //Subject = new ClaimsIdentity(claims)
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                      new Claim(ClaimTypes.Name, Username.ToString())
                    }),

                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //[Route("api/GenerateToken")]
        //[HttpPost]
        //public string GenerateToken(IEnumerable<Claim> claims, string Username)
        //{
        //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
        //    var TokenExpiryTimeHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInSeconds"]);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Issuer = _configuration["JWTKey:ValidIssuer"],
        //        Audience = _configuration["JWTKey:ValidAudience"],
        //        Expires = DateTime.UtcNow.AddMinutes(5),
        //        SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
        //        //Subject = new ClaimsIdentity(claims)
        //        Subject = new ClaimsIdentity(new Claim[]
        //            {
        //              new Claim(ClaimTypes.Name, Username.ToString())
        //            }),
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}



    }
}
