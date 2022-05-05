using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace restapiprogram
{
    class Program
    {
        static void Main(string[] args)
        {
            getEmployeeData();
            CreateEmployee();
            Delete(int id);
        }


        public static void getEmployeeData()
        {
            try
            {
                var client = new RestClient("https://reqres.in/api/users?per_page=20");
                Console.WriteLine("calling webapi...");


                var request = new RestRequest("employee");
                var response = client.ExecuteAsync(request);
                if (response.IsCompleted)

                {
                    var result = response.Result;
                    if (result.IsSuccessful)
                    {
                        var messagetask = result.Content;
                        Console.WriteLine("message from webapi : " + messagetask);
                        Console.ReadLine();
                    }
                }

            }
            catch(Exception)
            {
               return null;
            }
        }
        public static void CreateEmployee()
        {
            try
            {
                var client = new RestClient("https://reqres.in/api/register");
                var request = new RestRequest("create", Method.Post);
                request.AddParameter("email", "dani@gmail.com");
                request.AddParameter("password", "yoyodanny");
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                var response = client.ExecutePostAsync(request);
                if (response.IsCompleted)

                {
                    var result = response.Result;
                    if (result.IsSuccessful)
                    {
                        var messagetask = result.Content;
                        Console.WriteLine("message from webapi : " + messagetask);
                        Console.ReadLine();
                    }
                }
            }
            catch(Exception)
            {
                 return null;
            }


            public IHttpActionResult Put(StudentViewModel student)
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                using (var ctx = new SchoolDBEntities())
                {
                    var existingStudent = ctx.Students.Where(s => s.StudentID == student.Id)
                                                            .FirstOrDefault<Student>();

                    if (existingStudent != null)
                    {
                        existingStudent.FirstName = student.FirstName;
                        existingStudent.LastName = student.LastName;

                        ctx.SaveChanges();
                    }
                    else
                    {
                        return NotFound();
                    }
                }

                return Ok();
            }
            public IHttpActionResult Delete(int id)
            {
                if (id <= 0)
                    return BadRequest("Not a valid student id");

                using (var ctx = new SchoolDBEntities())
                {
                    var student = ctx.Students
                        .Where(s => s.StudentID == id)
                        .FirstOrDefault();

                    ctx.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                    ctx.SaveChanges();
                }

                return Ok();
            }
    }


}
