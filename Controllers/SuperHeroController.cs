using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperheroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperheroController : ControllerBase
    {
        private static List<Superhero> heroes = new List<Superhero>
        {
            new Superhero {Id = 1,
                Name = "Spiderman",
                FirstName = "Peter",
                LastName = "Parker",
                Place = "New York City"
            },
            new Superhero {Id = 2,
                Name = "IronMan",
                FirstName = "Tony",
                LastName = "Stark",
                Place = "Long Island"
            },
        };

        private readonly DataContext _context;

        //Constructor
        public SuperheroController(DataContext context)
        {
            _context = context;
        }
        
        // Get all heroes
        [HttpGet]
        public async Task<ActionResult<List<Superhero>>> Get()
        {
            return Ok(await _context.Superheroes.ToListAsync());
        }
        //Get one hero
        [HttpGet("{id}")]
        public async Task<ActionResult<Superhero>> Get(int id)
        {
            var hero = await _context.Superheroes.FindAsync(id);
            if( hero == null)
            {
                return BadRequest("Hero not found");
            }
            return Ok(hero);
        }
        //Add a hero to the list
        [HttpPost]
        public async Task<ActionResult<List<Superhero>>> AddHero(Superhero hero)
        {
            _context.Superheroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Superheroes.ToListAsync());
        }
        //Update a hero
        [HttpPut]
        public async Task<ActionResult<List<Superhero>>> UpdateHero(Superhero request)
        {
            var dbhero =  await _context.Superheroes.FindAsync(request.Id);
            if(dbhero == null)
            {
                return BadRequest("Hero not found");
            }
            
            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName;
            dbhero.LastName = request.LastName;
            dbhero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(heroes);
        }
        //Delete a hero
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Superhero>>> DeleteHero(int id)
        {
            var hero = await _context.Superheroes.FindAsync(id);
            if(hero == null)
            {
                return BadRequest("Hero not found");
            }
            _context.Superheroes.Remove(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Superheroes.ToListAsync());
        }

    }
}