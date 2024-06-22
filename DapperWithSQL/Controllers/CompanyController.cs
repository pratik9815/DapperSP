using Dapper;
using DapperWithSQL.DataContext;
using DapperWithSQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Data;

namespace DapperWithSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DapperContext _dapperContext;

        public CompanyController(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        [HttpGet("get-companies")]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            string query = "GetCompany";

            using (var connection = _dapperContext.DbConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        [HttpPost("add-company")]
        public async Task<ActionResult<Api_Response>> AddEmployee([FromBody]Company company)
        {
            string query = "AddCompany";
            
            using (var connection = _dapperContext.DbConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", company.Name);
                parameters.Add("@Address", company.Address);
                parameters.Add("@Country", company.Country);
                var result = await connection.QuerySingleAsync<Api_Response>(query, parameters);
                return Ok(result);
            }   
        }

        [HttpGet("get-company-details/{id}")]
        public ActionResult<Company> GetCompanyDetail([FromRoute]int id)
        {
            string query1 = "GetCompanyById";
            string query2 = "GetEmployeeByCompany";


            using (var connection = _dapperContext.DbConnection())
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("@CompanyId", id);
                var company = connection.QueryFirstOrDefault<Company>(query1, new {CompanyId = id });

                if (company != null)
                {
                    var employees = connection.Query<Employee>(query2, para).ToList();
                    company.Employees.AddRange(employees);
                }
                return Ok(company);
            }
           
        }


        [HttpPut("update-company-details")]
        public ActionResult<Api_Response> UpdateCompanyDetails(Company company)
        {
            Api_Response res = new Api_Response();

            string sp = "UpdateCompany";

            using(var connection = _dapperContext.DbConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", company.Id);
                parameters.Add("@Name", company.Name);
                parameters.Add("@Address", company.Address);
                parameters.Add("@Country", company.Country);

                var result = connection.QuerySingle<Api_Response>(sp, parameters, commandType: CommandType.StoredProcedure);
                return Ok(result);
            }

        }

        [HttpDelete("remove-company")]
        public async Task<ActionResult<Api_Response>> RemoveCompany(int companyId)
        {
            string sql = "RemoveCompany";
            using(var connection = _dapperContext.DbConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CompanyId",companyId);
                var result = await connection.QuerySingleAsync<Api_Response>(sql,parameters,commandType:CommandType.StoredProcedure);
                return Ok(result);
            }
        }
    }
}
