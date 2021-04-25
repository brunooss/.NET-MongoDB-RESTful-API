using Api.Data.Collections;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Person> _peopleCollection;

        public PersonController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _peopleCollection = _mongoDB.DB.GetCollection<Person>(typeof(Person).Name.ToLower());
        }

        [HttpPost]
        public ActionResult PostPerson([FromBody] PersonDto dto)
        {
            var person = new Person(dto.Name, dto.Latitude, dto.Longitude, dto.BornDate);

            _peopleCollection.InsertOne(person);

            return StatusCode(201, "Pessoa adicionada com sucesso");
        }

        [HttpGet]
        public ActionResult GetPersonList()
        {
            var persons = _peopleCollection.Find(Builders<Person>.Filter.Empty).ToList();

            return Ok(persons);
        }

        [HttpPut]
        public ActionResult UpdatePerson([FromBody] PersonDto dto)
        {
            _peopleCollection.UpdateOne(Builders<Person>.Filter.Where(_ => _.BornDate == dto.BornDate),
                Builders<Person>.Update.Set("name", dto.Name));

            return Ok("Pessoa atualizada com sucesso");
        }

        [HttpDelete("{bornDate}")]
        public ActionResult DeletePerson(string bornDate)
        {
            _peopleCollection.DeleteOne(Builders<Person>.Filter.Where(_ => _.BornDate == Convert.ToDateTime(bornDate)));

            return Ok("Deletado com sucesso");
        }
    }
}