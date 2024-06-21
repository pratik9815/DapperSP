using Dapper;
using DapperWithSQL.DataContext;
using DapperWithSQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace DapperWithSQL.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DapperContext _dapperContext;

        public EmployeeController(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

            [HttpGet("get-employees")]
            public async Task<ActionResult<List<Employee>>> GetEmployees()
            {
                string query = "GetAllEmployees";

                using(var connection = _dapperContext.DbConnection())
                {
                    var employees = await connection.QueryAsync<Employee>(query);
                    return employees.ToList();
                }
            }

            [HttpPost("add-employee")]
            public  ActionResult<string> AddEmployee(Employee employee)
            {
                string query = "AddEmployee";
                int result;
                using(var connection = _dapperContext.DbConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Name", employee.Name);
                    parameters.Add("@Age", employee.Age);
                    parameters.Add("@Position", employee.Position);
                    parameters.Add("@CompanyId", employee.CompanyId);

                    result = connection.Execute(query, parameters);
               
                }
                return Ok($"{result} row affected");
            }
    }
}
