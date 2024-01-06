using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MoviceAPI.service;
using System.Runtime.CompilerServices;

namespace MoviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Genres : ControllerBase
    {
        private readonly IGanraService _ganra;
        

        public Genres(IGanraService ganra)
        {            
      
            _ganra = ganra;
        }
        [HttpGet]
        public async Task<IActionResult> GetGenreAsync()
        {
            var result = await _ganra.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddGenreAsync(GenreDTO genreDTO)
        {
            Genre genre = new();
            genre.Name= genreDTO.Name;
            var result=await _ganra.Add(genre );
            return Ok(result);
         
        }
        [HttpPut("{id}") ]
        public async Task<IActionResult> EditGenreAsync(int Id, GenreDTO genreDTO)
        {
            var genrc=await _ganra.GetById(Id);
            if(genrc==null) { return NotFound($"Not Found {Id}"); }
            genrc.Name= genreDTO.Name;
            _ganra.Update(genrc);
            return Ok(genrc);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveGereAsync(int id)
        {
            var gernce=await _ganra.GetById(id);
            if(gernce==null) { return NotFound($"Not Found {id}"); }
            _ganra.Remove(gernce);
            return Ok(gernce);
        }
    }
}
