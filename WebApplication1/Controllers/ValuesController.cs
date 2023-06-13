using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;


namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-NC4RCNO\SQLEXPRESS;database=FirstWebApi; Integrated Security=true;");
        // GET api/values
        public string Get()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * FROM staj1", con); 
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count>0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return "Veritabanında hicbir kayit bulunamadi";
            }
        }

        // GET api/values/5
        // GET api/values/5
        [HttpGet]
        [Route("api/values/{id}")]
        public string Get([FromUri] int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * FROM staj1 WHERE id = @id", con);
            da.SelectCommand.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return "Veritabanında aradığınız idye ait hicbir kayit bulunamadi";
            }
        }



        public class Person
        {
            public string Name { get; set; }
            public string LastName { get; set; }
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody] Person person)
        {
            SqlCommand cmd = new SqlCommand("Insert into staj1(Name, LastName) VALUES(@Name, @LastName)", con);
            cmd.Parameters.AddWithValue("@Name", person.Name);
            cmd.Parameters.AddWithValue("@LastName", person.LastName);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i == 1)
            {
                return person.Name + " " + person.LastName + " Kayıtlara Eklendi";
            }
            else
            {
                return "Kayıt Eklenemedi Tekrar Dene";
            }
        }


        // PUT api/values/5
        // PUT api/values/5
        [HttpPut]
        [Route("api/values/{id}")]
        public string Put(int id, [FromBody] Person person)
        {
            SqlCommand cmd = new SqlCommand("UPDATE staj1 SET Name = @Name, LastName = @LastName WHERE id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@Name", person.Name);
            cmd.Parameters.AddWithValue("@LastName", person.LastName);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i == 1)
            {
                return person.Name + " " + person.LastName + " Adlı kişinin Kaydı Güncellendi";
            }
            else
            {
                return "Kayıt Güncellenemedi Tekrar Dene";
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("api/values/{id}")]
        public string Delete(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM staj1 WHERE id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i == 1)
            {
                return "Kayıt Başarıyla Silindi";
            }
            else
            {
                return "Kayıt Silinemedi Tekrar Dene";
            }
        }

    }
}
