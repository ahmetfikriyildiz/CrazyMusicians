using Microsoft.AspNetCore.Mvc;
using CrazyMusicians.Models;
using System.Collections.Generic;
using System.Linq;


namespace CrazyMusiciansAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusiciansController : ControllerBase
    {
        // In-memory data
        private static List<Musician> musicians = new List<Musician>
        {
            new Musician{ID = 1, Name = "Ahmet Çalgı", Profession = "Ünlü Çalgı Çalar", FunFeature = "Her zaman yanlış nota çalar, ama çok eğlenceli."},
            new Musician{ID = 2, Name = "Zeynep Melodi", Profession = "Popüler Melodi Yazarı", FunFeature = "Şarkıları yanlış anlaşılır ama çok popüler."},
            new Musician{ID = 3, Name = "Cemil Akor", Profession = "Çılgın Akorist", FunFeature = "Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli."},
            new Musician{ID = 4, Name = "Fatma Nota", Profession = "Sürpriz Nota Üreticisi", FunFeature = "Nota üretirken sürekli sürprizler hazırlar."},
            new Musician{ID = 5, Name = "Hasan Ritim", Profession = "Ritim Canavarı", FunFeature = "Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir."},
            new Musician{ID = 6, Name = "Elif Armoni", Profession = "Armoni Ustası", FunFeature = "Armonilerini bazen yanlış çalar, ama çok yaratıcıdır."},
            new Musician{ID = 7, Name = "Ali Perde", Profession = "Perde Uygulayıcı", FunFeature = "Her perdeyi farklı şekilde çalar, her zaman sürprizlidir."},
            new Musician{ID = 8, Name = "Ayşe Rezonans", Profession = "Rezonans Uzmanı", FunFeature = "Rezonans konusunda uzman, ama bazen çok gurultu çıkarır."},
            new Musician{ID = 9, Name = "Murat Ton", Profession = "Tonlama Meraklısı", FunFeature = "Tonlamalarındaki farklılıklar bazen komik, ama oldukça ilginç."},
            new Musician{ID = 10, Name = "Selin Akor", Profession = "Akor Sihirbazı", FunFeature = "Akorları değiştirdiğinde bazen sihirli bir hava yaratır."}
        };

        // GET api/musicians
        [HttpGet]
        public ActionResult<IEnumerable<Musician>> GetAllMusicians()
        {
            return Ok(musicians);
        }

        // GET api/musicians/search?name=Ahmet
        [HttpGet("search")]
        public ActionResult<IEnumerable<Musician>> SearchMusicians([FromQuery] string name)
        {
            var results = musicians.Where(m => m.Name.Contains(name, System.StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(results);
        }

        // POST api/musicians
        [HttpPost]
        public ActionResult CreateMusician([FromBody] Musician newMusician)
        {
            if (newMusician == null || string.IsNullOrEmpty(newMusician.Name))
            {
                return BadRequest("Musician data is invalid.");
            }

            newMusician.ID = musicians.Max(m => m.ID) + 1; // Auto-increment ID
            musicians.Add(newMusician);
            return CreatedAtAction(nameof(GetMusicianById), new { id = newMusician.ID }, newMusician);
        }

        // GET api/musicians/{id}
        [HttpGet("{id}")]
        public ActionResult<Musician> GetMusicianById(int id)
        {
            var musician = musicians.FirstOrDefault(m => m.ID == id);
            if (musician == null)
            {
                return NotFound("Musician not found.");
            }

            return Ok(musician);
        }

        // PUT api/musicians/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateMusician(int id, [FromBody] Musician updatedMusician)
        {
            var musician = musicians.FirstOrDefault(m => m.ID == id);
            if (musician == null)
            {
                return NotFound("Musician not found.");
            }

            musician.Name = updatedMusician.Name;
            musician.Profession = updatedMusician.Profession;
            musician.FunFeature = updatedMusician.FunFeature;

            return NoContent();
        }

        // PATCH api/musicians/{id}
        [HttpPatch("{id}")]
        public ActionResult UpdateMusicianFeature(int id, [FromBody] string funFeature)
        {
            var musician = musicians.FirstOrDefault(m => m.ID == id);
            if (musician == null)
            {
                return NotFound("Musician not found.");
            }

            musician.FunFeature = funFeature;
            return NoContent();
        }

        // DELETE api/musicians/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteMusician(int id)
        {
            var musician = musicians.FirstOrDefault(m => m.ID == id);
            if (musician == null)
            {
                return NotFound("Musician not found.");
            }

            musicians.Remove(musician);
            return NoContent();
        }
    }
}
