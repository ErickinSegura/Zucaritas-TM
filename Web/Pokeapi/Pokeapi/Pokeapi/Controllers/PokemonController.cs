using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pokeapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Bulbasaur", "Charmander", "Squirtle", "Chikorita", "Cyndaquil", "Totodile", "Treecko", "Torchic", "Turtwig", "Chimchar", "Piplup", "Snivy", "Tepig", "Oshawott", "Chespin", "Fennekin", "Froakie", "Rowlet", "Litten", "Popplio", 
        };

        private static readonly string[] Images = new[]
        {
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/1.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/4.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/7.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/152.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/155.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/158.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/252.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/255.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/258.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/387.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/390.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/393.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/495.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/498.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/501.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/650.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/653.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/656.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/722.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/725.png",
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/728.png",

        };


        private static readonly string[] Types = new[]
        {
            "Grass", "Fire", "Water",
        };

        private static readonly string[] Descrptions = new[]
        {
            "Bulbasaur can be seen napping in bright sunlight. There is a seed on its back. By soaking up the sun's rays, the seed grows progressively larger.",
            "The flame at the tip of Charmander's tail makes a sound as it burns. You can only hear it in quiet places.",
            "Squirtle's shell is not merely used for protection. The shell's rounded shape and the grooves on its surface help minimize resistance in water, enabling this Pokémon to swim at high speeds.",
            "Chikorita releases a relaxing fragrance from the leaf on its head. This fragrance soothes anyone who comes in contact with it.",
            "Cyndaquil protects itself by flaring up the flames on its back. The flames are vigorous if the Pokémon is angry. However, if it is tired, the flames sputter fitfully with incomplete combustion.",
            "Despite the smallness of its body, Totodile's jaws are very powerful. While the Pokémon may think it is just playfully nipping, its bite has enough power to cause serious injury.",
            "Treecko is cool, calm, and collected—it never panics under any situation. If a bigger foe were to glare at this Pokémon, it would glare right back without conceding an inch of ground.",
            "Torchic has a place inside its body where it keeps its flame. Give it a hug—it will be glowing with warmth. This Pokémon is covered all over by a fluffy coat of down.",
            "Mudkip uses the sensitive fin on its head to take radar readings of its surroundings. In water, this Pokémon uses its fin to sense the flow of water and the current.",
            "Turtwig often close their eyes to absorb sunlight, which fills them with energy. They are also able to generate energy by basking in the light of the full moon.",
            "Chimchar is a playful Pokémon that trains by running through grassy fields. It is very active and becomes stressed if it does not get to move about each day.",
            "Piplup is a light-blue, penguin-like Pokémon, which is covered in thick down to insulate against the cold. It has a dark blue head with a primarily white face and a short, yellow beak.",
            "Snivy is a slender, green Pokémon with a serpentine body. Most of its body is green with a cream underside. A yellow stripe runs down the length of its back and tail, and it has yellow markings around its large eyes.",
            "Tepig is a quadruped, pig-like Pokémon that is primarily orange. It has dark-colored eyes and a red nose. The upper half of its head and the large tuft of fur on its snout are black.",
            "Oshawott is a bipedal sea otter-like Pokémon. It has a spherical white head with small, triangular dark-blue ears on the sides. Oshawott's eyes are dark and its dark-orange nose is shaped like an O.",
            "Chespin is a bipedal, mammalian Pokémon. It is primarily light brown with a darker brown face, paws, and feet. Its back is covered with tough, keratinized skin, and it has a protective helmet-like covering over its head.",
            "Fennekin is a small, quadrupedal fox-like Pokémon. It is covered in pale yellow fur that is longer on its haunches. It has large, dark orange eyes and a pointed snout with a light orange area extending from the nose to the eyes.",
            "Froakie is a quadrupedal, amphibious Pokémon. It has light-blue skin with white hands and feet. It has a dark blue underside and a light blue tail tip. Its eyes are large, dark, and have white pupils.",
            "Rowlet is a small, avian Pokémon resembling a young owl with a round body and short legs. Its plumage is primarily brown with a white underside and facial disc. The facial disc itself is in the shape of two overlapping circles.",
            "Litten is a quadruped, feline Pokémon covered with primarily black fur. It has a short muzzle with a tiny, black nose, red eyes with yellow sclera, and short, pointed ears with pale gray insides.",
            "Popplio is a pinniped Pokémon that is primarily blue. It has large eyes, a long, white snout with black whiskers, and round, pink nose. There is a small, rounded earflap on each side of its head.",
};

        [HttpGet(Name = "GetPokemon")]
        public IEnumerable<Pokemon> Get()
        {
            return Enumerable.Range(0, Summaries.Length).Select(index => new Pokemon
            {
                ID = index.ToString(),
                Name = Summaries[index],
                Description = Descrptions[index],
                Image = Images[index],
                Type = Types[index % Types.Length]
            })
            .ToArray();
        }

        [HttpGet("{id}", Name = "GetPokemonById")]
        public Pokemon Get(int id)
        {
            return new Pokemon
            {
                ID = id.ToString(),
                Name = Summaries[id],
                Description = Descrptions[id],
                Image = Images[id],
                Type = Types[id % Types.Length]
            };
        }

    }
}
