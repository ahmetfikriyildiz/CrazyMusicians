using CrazyMusicians.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrazyMusicians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicianController : ControllerBase
    {
        private static List<Musician> _musicians = new()
        {
            new Musician { Id = 1, Name = "Ahmet Çalgı", Occupation = "Ünlü Çalgı Çalar", FunFact = "Her zaman yanlış nota çalar, ama çok eğlenceli" },
            new Musician { Id = 2, Name = "Zeynep Melodi", Occupation = "Popüler Melodi Yazarı", FunFact = "Şarkıları yanlış anlaşılır ama çok popüler" },
            new Musician { Id = 3, Name = "Cemil Akor", Occupation = "Çılgın Akorist", FunFact = "Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli" },
            new Musician { Id = 4, Name = "Fatma Nota", Occupation = "Sürpriz Nota Üreticisi", FunFact = "Nota üretirken sürekli sürprizler hazırlar" },
            new Musician { Id = 5, Name = "Hasan Ritim", Occupation = "Ritim Canavarı", FunFact = "Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir" },
            new Musician { Id = 6, Name = "Elif Armoni", Occupation = "Armoni Ustası", FunFact = "Armonilerini bazen yanlış çalar, ama çok yaratıcıdır" },
            new Musician { Id = 7, Name = "Ali Perde", Occupation = "Perde Uygulayıcı", FunFact = "Her perdeyi farklı şekilde çalar, her zaman sürprizlidir" },
            new Musician { Id = 8, Name = "Ayşe Rezonans", Occupation = "Rezonans Uzmanı", FunFact = "Rezonans konusunda uzman, ama bazen çok gürültü çıkarır" },
            new Musician { Id = 9, Name = "Murat Ton", Occupation = "Tonlama Meraklısı", FunFact = "Tonlamalarındaki farklılıklar bazen komik, ama oldukça ilginç" },
            new Musician { Id = 10,Name = "Selin Akor", Occupation = "Akor Sihirbazı", FunFact = "Akorları değiştirdiğinde bazen sihirli bir hava yaratır" },
        };

        [HttpGet]
        public ActionResult<List<Musician>> Get()
        {
            return _musicians;
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Musician> Get(int id)
        {
            var musician = _musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null)
            {
                return NotFound();
            }
            return musician;
        }
        [HttpPost]
        public ActionResult Post(Musician musician)
        {
            musician.Id = _musicians.Max(m => m.Id) + 1;
            _musicians.Add(musician);
            return CreatedAtAction(nameof(Get), new { id = musician.Id }, musician);
        }
        [HttpPatch]
        [Route("{id}")]
        public ActionResult Patch(int id, Musician musician)
        {
            var existingMusician = _musicians.FirstOrDefault(m => m.Id == id);
            if (existingMusician == null)
            {
                return NotFound();
            }
            if (musician.Name != null)
            {
                existingMusician.Name = musician.Name;
            }
            if (musician.Occupation != null)
            {
                existingMusician.Occupation = musician.Occupation;
            }
            if (musician.FunFact != null)
            {
                existingMusician.FunFact = musician.FunFact;
            }
            return NoContent();
        }
        [HttpPut]
        [Route("{id}")]
        public ActionResult Put(int id, Musician musician)
        {
            var existingMusician = _musicians.FirstOrDefault(m => m.Id == id);
            if (existingMusician == null)
            {
                return NotFound();
            }
            existingMusician.Name = musician.Name;
            existingMusician.Occupation = musician.Occupation;
            existingMusician.FunFact = musician.FunFact;
            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var existingMusician = _musicians.FirstOrDefault(m => m.Id == id);
            if (existingMusician == null)
            {
                return NotFound();
            }
            _musicians.Remove(existingMusician);
            return NoContent();
        }
    }
}
