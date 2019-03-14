using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IndyBooks.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IndyBooks.Controllers
{
    [Route("api/[controller]")]
    public class WriterController : Controller
    {
        private IndyBooksDataContext _dbc;
        public WriterController(IndyBooksDataContext dbc)
        {
            _dbc = dbc;
        }
        //Bonus
        [HttpGet("bookcount/{id}")]//a pop up appears, but it says "number of books : unavailable"
        //using the http api/writer/bookcount/id works and has just the book count pop up on screen.
        public IActionResult Get(int id)
        {
            var count = _dbc.Writers.Include(w => w.Books).Where(w => w.Id == id).Select(w => w.Books.Count);
            
            return Json(count);
        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var writers = _dbc.Writers;
            return Json(writers);
            //return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var writers = _dbc.Writers.SingleOrDefault(w => w.Id == id);
            if (writers == null)
                return NotFound();
            return Json(writers);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Writer writer)
        {
            _dbc.Writers.Add(writer);
            _dbc.SaveChanges();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody]Writer writer, long id) 
        {
            var dbw = _dbc.Writers.SingleOrDefault(w => w.Id == id);
            if (dbw != null)
            {
                dbw.Name = writer.Name;
                _dbc.Update(dbw);
                _dbc.SaveChanges();
                return Ok();
            }
            else
                return NotFound();
            
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
