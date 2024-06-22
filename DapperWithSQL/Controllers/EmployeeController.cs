using Dapper;
using DapperWithSQL.DataContext;
using DapperWithSQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Data;

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

            using (var connection = _dapperContext.DbConnection())
            {
                var employees = await connection.QueryAsync<Employee>(query);
                return employees.ToList();
            }
        }

        [HttpPost("add-employee")]
        public async Task<ActionResult<Api_Response>> AddEmployee(Employee employee)
        {
            string query = "AddEmployee";
            using (var connection = _dapperContext.DbConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", employee.Name);
                parameters.Add("@Age", employee.Age);
                parameters.Add("@Position", employee.Position);
                parameters.Add("@CompanyId", employee.CompanyId);

                var result = await connection.QuerySingleAsync<Api_Response>(query, parameters);
                return Ok(result);
            }
        }

        [HttpGet("get-employee-by-id/{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById([FromRoute]int employeeId)
        {
            string query = "GetEmployeeById";

            using (var connection = _dapperContext.DbConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CompanyId", employeeId);
                var employee = await connection.QuerySingleAsync<Employee>(query,parameters,commandType:CommandType.StoredProcedure);
                return Ok(employee);
            }
        }

        [HttpGet("remove-employee/{id}")]
        public async Task<ActionResult<Api_Response>> RemoveEmployees([FromRoute] int employeeId)
        {
            string query = "RemoveCompany";

            using (var connection = _dapperContext.DbConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CompanyId", employeeId);
                var result = await connection.QuerySingleAsync<Api_Response>(query,parameters,commandType:CommandType.StoredProcedure);
                return Ok(result);
            }
        }

        //update employee
        [HttpPut("update-employee")]
        public async Task<ActionResult<Api_Response>> UpdateEmployee([FromBody] Employee employee)
        {
            string sql = "UpdateEmployee";

            using(var connection = _dapperContext.DbConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", employee.Id);
                parameters.Add("@Name", employee.Name);
                parameters.Add("@Age", employee.Age);
                parameters.Add("@CompanyId", employee.CompanyId);
                parameters.Add("@Position", employee.Position);

                var result = await connection.QuerySingleAsync<Api_Response>(sql, parameters,commandType:CommandType.StoredProcedure);
                return Ok(result);
            }
        }
    }
}

