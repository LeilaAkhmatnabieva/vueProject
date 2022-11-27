using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitizensController : ControllerBase
    {
        private IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(
                "Server = localhost; Database = citizen_app; User Id = postgres; Password = leila; Port = 5432;");
        }

        [HttpGet("Get")]
        public List<Gilec> Get()
        {
            try
            {
                List<Gilec> res;
                using (var connection = CreateConnection())
                {
                    res = connection.Query<Gilec>("SELECT * FROM gilec;").ToList();
                }
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e.Message}");
                throw;
            }
        }
        
        [HttpGet("GetById")]
        public Gilec GetById(int id)
        {
            try
            {
                Gilec res;
                using (var connection = CreateConnection())
                {
                    res = connection.QueryFirst<Gilec>($"SELECT * FROM gilec WHERE id = {id};");
                }
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e.Message}");
                throw;
            }
        }
        [HttpDelete("Delete")]
        public int Delete(int id)
        {
            try
            {
                int res;
                using (var connection = CreateConnection())
                {
                    res = connection.Execute($"DELETE FROM gilec WHERE id = {id};");
                }
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e.Message}");
                throw;
            }
        }
        
        [HttpPost("Add")]
        public int Add(Gilec gilec)
        {
            try
            {
                int res;
                using (var connection = CreateConnection())
                {
                    string sql = @$"
                    INSERT INTO public.gilec(first_name, middle_name, last_name, birth_date, snils, is_auto)
	                VALUES ('{gilec.first_name ?? "null"}', '{gilec.middle_name ?? "null"}', '{gilec.last_name ?? "null"}',
                    {gilec.birth_date?.ToString() ?? "null"}, {gilec.snils?.ToString() ?? "null"}, {gilec.is_auto?.ToString() ?? "null"});";
                    res = connection.Execute(sql);
                }
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e.Message}");
                throw;
            }
        }
        
        [HttpPost("Update")]
        public int Update(Gilec gilec)
        {
            try
            {
                int res;
                using (var connection = CreateConnection())
                {
                    string sql = @$"
                    UPDATE public.gilec
                    SET first_name = '{gilec.first_name ?? "null"}',
                        middle_name = '{gilec.middle_name ?? "null"}',
                        last_name = '{gilec.last_name ?? "null"}',
                        birth_date = '{gilec.birth_date?.ToString() ?? "null"}',
                        snils = {gilec.snils?.ToString() ?? "null"},
                        is_auto = {gilec.is_auto?.ToString() ?? "null"}
                    WHERE id = {gilec.id};";
                    res = connection.Execute(sql);
                }
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e.Message}");
                throw;
            }
        }
    }
}