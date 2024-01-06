using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MoviceAPI.Models;
using MoviceAPI.service;

namespace MoviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ImovieService _service;
        private readonly IGanraService _gunraService;
        private readonly new List<string> _AllowExtinction = new() { ".jpj", ".png" };
        private readonly long _maxSizeImage=5048576;
        public MoviesController(ImovieService service, IGanraService gunraService, IMapper mapper)
        {
            this._service = service;
            _gunraService = gunraService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult>GetMovice()
        {
            var Movice = await _service.GetAll();
            //TODO:map movie to DTO
            var data = _mapper.Map<IEnumerable<MovieDetalisDto>>(Movice);
            return Ok(data);
            
        }
        

        [HttpGet("{id}")]
        public async Task<IActionResult>GetbyIdAsync(int id)
        {
           var movic= await _service.FindById(id);
            var data = _mapper.Map<IEnumerable<MovieDetalisDto>>(movic);
            return Ok(data);
        }
        [HttpGet("GetbyidGenra")]
        public async Task<IActionResult>GetbyidGenraAsync(int id)
        {
            var Movice = await _service.GetAll(id);
            //TODO:map movice to DTO
            var data = _mapper.Map<IEnumerable<MovieDetalisDto>>(Movice);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreatAsync([FromForm]MovieDTO dto)
        {
            if (!_AllowExtinction.Contains(Path.GetExtension(dto.Poster.FileName.ToLower())))
                return BadRequest("Only .png or jpj");
            if (dto.Poster.Length > _maxSizeImage)
                return BadRequest("Only 5 MB");
            var IdIsVaild =await _gunraService.IsvaildGanre(dto.GenreId);
            if(!IdIsVaild)
                return BadRequest("Genra Not Found!!");
            if (dto.Poster == null)
                return BadRequest("Poster Is Required");

            using var datastream= new MemoryStream();
            await dto.Poster.CopyToAsync(datastream);
            var movie = _mapper.Map<Movie>(dto);
            movie.Poster=datastream.ToArray();
            await _service.Add(movie);
            return Ok(movie);   
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateMovieAsync(int id, [FromForm]MovieDTO dto)
        {
            var movice = await _service.FindById(id);
            if (movice == null)
                return NotFound($"Not Found {id}");
            if (!_AllowExtinction.Contains(Path.GetExtension(dto.Poster.FileName.ToLower())))
                return BadRequest("Only .png or jpj");
            if (dto.Poster.Length > _maxSizeImage)
                return BadRequest("Only 5 MB");
            var IdIsVaild = await _gunraService.IsvaildGanre(dto.GenreId);
            if (!IdIsVaild)
                return BadRequest("Genra Not Found!!");
            if(movice.Poster!=null)
            {
                using var datastream = new MemoryStream();
                await dto.Poster.CopyToAsync(datastream);
                movice.Poster = datastream.ToArray();
            }
            {
                movice.Title = dto.Title;
                movice.Rate = dto.Rate;
                movice.Year = dto.Year;
                movice.Storeine = dto.Storeine;
                movice.Rate =dto.Rate;
                movice.GenreId= dto.GenreId;
            }
            _service.Update(movice);
            return Ok(movice);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeletItemAsync(int id)
        {
            var movice=await _service.FindById(id);
            if (movice == null)
                return NotFound($"Movice Not Found {id}");
            _service.Remove(movice);
            return Ok(movice);
        }

    }
}
