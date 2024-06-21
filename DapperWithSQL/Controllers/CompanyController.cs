using Dapper;
using DapperWithSQL.DataContext;
using DapperWithSQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<string> AddEmployee([FromBody]Company company)
        {
            string query = "AddCompany";
            int result;
            using (var connection = _dapperContext.DbConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", company.Name);
                parameters.Add("@Address", company.Address);
                parameters.Add("@Country", company.Country);
                result = connection.Execute(query, parameters);
            }
            return Ok($"{result} row affected");
        }

        [HttpGet("get-company-details/{id}")]
        public Company GetCompanyDetail([FromRoute]int id)
        {
            string query1 = "GetCompanyById";
            string query2 = "GetEmployeeByCompany";


            using (var connection = _dapperContext.DbConnection())
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("@CompanyId", id);
                var lookup = new Dictionary<int, Company>();

                //var result = connection.QuerySingleOrDefault<Company>(query, para);

                var company = connection.QueryFirstOrDefault<Company>(query1, new {CompanyId = id });

                if (company != null)
                {
                    var employees = connection.Query<Employee>(query2, para).ToList();
                    company.Employees.AddRange(employees);
                }
                return company;
            }
           
        }
    }
}
