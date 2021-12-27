using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Newtonsoft.Json;

namespace Ellipse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EllipseController : ControllerBase
    {
        private readonly TodoContext _context;

        public EllipseController(TodoContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            string queryString = "select * from MSF810";
            string connectionString = "User Id=ellipse;Password=ellplnsc;Data Source=172.16.10.6:1521/ellplnsc";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                OracleDataReader reader = command.ExecuteReader();
            }
        }

        [HttpGet]
        public String getMSF810()
        {
            string queryString = "select * from MSF810";
            string connectionString = "User Id=ellipse;Password=ellplnsc;Data Source=172.16.10.6:1521/ellplnsc";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                OracleDataReader reader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(dataTable);
                return JSONString;
            }
        }
    }
}
