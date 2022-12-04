using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitizensController : ControllerBase
    {
        private readonly ILogger<CitizensController> _logger;

        public CitizensController(ILogger<CitizensController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Get")]
        public List<Gilec> Get()
        {
            List<Gilec> res;
            using (var connection = new NpgsqlConnection("Server = localhost; Database = citizen_app; User Id = postgres; Password = leila; Port = 5432;"))
            {
                res = connection.Query<Gilec>("SELECT * FROM gilec;").ToList();
            }

            return res;
        }
        
        [HttpGet("GetById")]
        public Gilec GetById(int id)
        {
            
            Gilec res;
            using (var connection = new NpgsqlConnection("Server = localhost; Database = citizen_app; User Id = postgres; Password = leila; Port = 5432;"))
            {
                res = connection.QueryFirst<Gilec>($"SELECT * FROM gilec WHERE id = {id};");
            }
            return res;
        }
        [HttpGet("Delete")]
        public int Delete(int id)
        {
            int res;
            using (var connection = new NpgsqlConnection("Server = localhost; Database = citizen_app; User Id = postgres; Password = leila; Port = 5432;"))
            {
                res = connection.Execute($"DELETE FROM gilec WHERE id = {id};");
            }
            return res;
        }
        
        [HttpPost("Add")]
        public int Add(Gilec gilec)
        {
            int res;
            using (var connection = new NpgsqlConnection("Server = localhost; Database = citizen_app; User Id = postgres; Password = leila; Port = 5432;"))
            {
                res = connection.Execute(@$"
                    INSERT INTO public.gilec(
	                first_name, middle_name, last_name, birth_date, snils, is_auto)
	                VALUES ('{gilec.first_name??"null"}', '{gilec.middle_name??"null"}', '{gilec.last_name??"null"}', {gilec.birth_date.ToString()??"null"}, {gilec.snils.ToString()??"null"}, {gilec.is_auto.ToString()??"null"});");
            }
            return res;
        }
        
        [HttpPost("Update")]
        public int Update(Gilec gilec)
        {
            int res;
            using (var connection =
                new NpgsqlConnection(
                    "Server = localhost; Database = citizen_app; User Id = postgres; Password = leila; Port = 5432;"))
            {
                res = connection.Execute(@$"
                    UPDATE public.gilec
	                SET first_name='{gilec.first_name ?? "null"}', middle_name='{gilec.middle_name ?? "null"}',
                        last_name='{gilec.last_name ?? "null"}', birth_date='{gilec.birth_date.ToString() ?? "null"}',
                        snils='{gilec.snils.ToString() ?? "null"}', is_auto = '{gilec.is_auto.ToString()??"null"}'
	                WHERE id = {gilec.id};");
            }

            return res;
        }
        
    }
}