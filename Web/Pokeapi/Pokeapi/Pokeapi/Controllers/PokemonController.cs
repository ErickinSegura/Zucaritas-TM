using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pokeapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {

        private static string url = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/";


        Dictionary<int, Pokemon> pokemons = new Dictionary<int, Pokemon>
        {

            {1, new Pokemon { ID = 1, Name = "Bulbasaur", Description = "Bulbasaur can be seen napping in bright sunlight. There is a seed on its back. By soaking up the sun's rays, the seed grows progressively larger.", Image = url + "1.png", Type = "Grass/Poison", ability = "Overgrow", attacks = "Tackle, Vine Whip, Razor Leaf, Seed Bomb", evolutionLine = "1", evolutionStage = "1" } },
            {2, new Pokemon { ID = 2, Name = "Ivysaur", Description = "There is a bud on this Pokémon's back. To support its weight, Ivysaur's legs and trunk grow thick and strong. If it starts spending more time lying in the sunlight, it's a sign that the bud will bloom into a large flower soon.", Image = url + "2.png", Type = "Grass/Poison", ability = "Overgrow", attacks = "Tackle, Vine Whip, Razor Leaf, Seed Bomb", evolutionLine = "1", evolutionStage = "2" } },
            {3, new Pokemon { ID = 3, Name = "Venusaur", Description = "There is a large flower on Venusaur's back. The flower is said to take on vivid colors if it gets plenty of nutrition and sunlight. The flower's aroma soothes the emotions of people.", Image = url + "3.png", Type = "Grass/Poison", ability = "Overgrow", attacks = "Tackle, Vine Whip, Razor Leaf, Seed Bomb", evolutionLine = "1", evolutionStage = "3" } },
            {4, new Pokemon { ID = 4, Name = "Charmander", Description = "The flame at the tip of Charmander's tail makes a sound as it burns. You can only hear it in quiet places, and it is said to be a sign that the Pokémon is enjoying itself.", Image = url + "4.png", Type = "Fire", ability = "Blaze", attacks = "Scratch, Ember, Flamethrower, Fire Spin", evolutionLine = "2", evolutionStage = "1" } },
            {5, new Pokemon { ID = 5, Name = "Charmeleon", Description = "Charmeleon mercilessly destroys its foes using its sharp claws. If it encounters a strong foe, it turns aggressive. In this excited state, the flame at the tip of its tail flares with a bluish white color.", Image = url + "5.png", Type = "Fire", ability = "Blaze", attacks = "Scratch, Ember, Flamethrower, Fire Spin", evolutionLine = "2", evolutionStage = "2" } },
            {6, new Pokemon { ID = 6, Name = "Charizard", Description = "Charizard flies around the sky in search of powerful opponents. It breathes fire of such great heat that it melts anything. However, it never turns its fiery breath on any opponent weaker than itself.", Image = url + "6.png", Type = "Fire/Flying", ability = "Blaze", attacks = "Scratch, Ember, Flamethrower, Fire Spin", evolutionLine = "2", evolutionStage = "3" } },
            {7, new Pokemon { ID = 7, Name = "Squirtle", Description = "Squirtle's shell is not merely used for protection. The shell's rounded shape and the grooves on its surface help minimize resistance in water, enabling this Pokémon to swim at high speeds.", Image = url + "7.png", Type = "Water", ability = "Torrent", attacks = "Tackle, Water Gun, Hydro Pump, Aqua Tail", evolutionLine = "3", evolutionStage = "1" } },
            {8, new Pokemon { ID = 8, Name = "Wartortle", Description = "Its tail is large and covered with a rich, thick fur. The tail becomes increasingly deeper in color as Wartortle ages. The scratches on its shell are evidence of this Pokémon's toughness as a battler.", Image = url + "8.png", Type = "Water", ability = "Torrent", attacks = "Tackle, Water Gun, Hydro Pump, Aqua Tail", evolutionLine = "3", evolutionStage = "2" } },
            {9, new Pokemon { ID = 9, Name = "Blastoise", Description = "Blastoise has water spouts that protrude from its shell. The water spouts are very accurate. They can shoot bullets of water with enough accuracy to strike empty cans from a distance of over 160 feet.", Image = url + "9.png", Type = "Water", ability = "Torrent", attacks = "Tackle, Water Gun, Hydro Pump, Aqua Tail", evolutionLine = "3", evolutionStage = "3" } },
            {10, new Pokemon { ID = 10, Name = "Caterpie", Description = "Caterpie has a voracious appetite. It can devour leaves bigger than its body right before your eyes. From its antenna, this Pokémon releases a terrifically strong odor.", Image = url + "10.png", Type = "Bug", ability = "Shield Dust", attacks = "Tackle, String Shot, Bug Bite, Bug Buzz", evolutionLine = "4", evolutionStage = "1" } },
            {11, new Pokemon { ID = 11, Name = "Metapod", Description = "The shell covering this Pokémon's body is as hard as an iron slab. Metapod does not move very much. It stays still because it is preparing its soft innards for evolution inside the hard shell.", Image = url + "11.png", Type = "Bug", ability = "Shed Skin", attacks = "Tackle, String Shot, Bug Bite, Bug Buzz", evolutionLine = "4", evolutionStage = "2" } },
            {12, new Pokemon { ID = 12, Name = "Butterfree", Description = "Butterfree has a superior ability to search for delicious honey from flowers. It can even search out, extract, and carry honey from flowers that are blooming over six miles from its nest.", Image = url + "12.png", Type = "Bug/Flying", ability = "Compound Eyes", attacks = "Tackle, String Shot, Bug Bite, Bug Buzz", evolutionLine = "4", evolutionStage = "3" } },
            {13, new Pokemon { ID = 13, Name = "Weedle", Description = "Weedle has an extremely acute sense of smell. It is capable of distinguishing its favorite kinds of leaves from those it dislikes just by sniffing with its big red proboscis (nose).", Image = url + "13.png", Type = "Bug/Poison", ability = "Shield Dust", attacks = "Poison Sting, String Shot, Bug Bite, Bug Buzz", evolutionLine = "5", evolutionStage = "1" } },
            {14, new Pokemon { ID = 14, Name = "Kakuna", Description = "Kakuna remains virtually immobile as it clings to a tree. However, on the inside, it is extremely busy as it prepares for its coming evolution. This is evident from how hot the shell becomes to the touch.", Image = url + "14.png", Type = "Bug/Poison", ability = "Shed Skin", attacks = "Poison Sting, String Shot, Bug Bite, Bug Buzz", evolutionLine = "5", evolutionStage = "2" } },
            {15, new Pokemon { ID = 15, Name = "Beedrill", Description = "Beedrill is extremely territorial. No one should ever approach its nest—this is for their own safety. If angered, they will attack in a furious swarm.", Image = url + "15.png", Type = "Bug/Poison", ability = "Swarm", attacks = "Poison Sting, String Shot, Bug Bite, Bug Buzz", evolutionLine = "5", evolutionStage = "3" } },
            {16, new Pokemon { ID = 16, Name = "Pidgey", Description = "Pidgey has an extremely sharp sense of direction. It is capable of unerringly returning home to its nest, however far it may be removed from its familiar surroundings.", Image = url + "16.png", Type = "Normal/Flying", ability = "Keen Eye", attacks = "Tackle, Gust, Quick Attack, Wing Attack", evolutionLine = "6", evolutionStage = "1" } },
            {17, new Pokemon { ID = 17, Name = "Pidgeotto", Description = "Pidgeotto claims a large area as its own territory. This Pokémon flies around, patrolling its living space. If its territory is violated, it shows no mercy in thoroughly punishing the foe with its sharp claws.", Image = url + "17.png", Type = "Normal/Flying", ability = "Keen Eye", attacks = "Tackle, Gust, Quick Attack, Wing Attack", evolutionLine = "6", evolutionStage = "2" } },
            {18, new Pokemon { ID = 18, Name = "Pidgeot", Description = "This Pokémon has a dazzling plumage of beautifully glossy feathers. Many Trainers are captivated by the striking beauty of the feathers on its head, compelling them to choose Pidgeot as their Pokémon.", Image = url + "18.png", Type = "Normal/Flying", ability = "Keen Eye", attacks = "Tackle, Gust, Quick Attack, Wing Attack", evolutionLine = "6", evolutionStage = "3" } },
            {19, new Pokemon { ID = 19, Name = "Rattata", Description = "Rattata is cautious in the extreme. Even while it is asleep, it constantly listens by moving its ears around. It is not picky about where it lives—it will make its nest anywhere.", Image = url + "19.png", Type = "Normal", ability = "Run Away", attacks = "Tackle, Quick Attack, Hyper Fang, Sucker Punch", evolutionLine = "7", evolutionStage = "1" } },
            {20, new Pokemon { ID = 20, Name = "Raticate", Description = "Raticate's sturdy fangs grow steadily. To keep them ground down, it gnaws on rocks and logs. It may even chew on the walls of houses.", Image = url + "20.png", Type = "Normal", ability = "Run Away", attacks = "Tackle, Quick Attack, Hyper Fang, Sucker Punch", evolutionLine = "7", evolutionStage = "2" } },
            {21, new Pokemon { ID = 21, Name = "Spearow", Description = "Spearow has a very loud cry that can be heard over half a mile away. If its high, keening cry is heard echoing all around, it is a sign that they are warning each other of danger.", Image = url + "21.png", Type = "Normal/Flying", ability = "Keen Eye", attacks = "Peck, Leer, Fury Attack, Drill Peck", evolutionLine = "8", evolutionStage = "1" } },
            {22, new Pokemon { ID = 22, Name = "Fearow", Description = "Fearow is recognized by its long neck and elongated beak. They are conveniently shaped for catching prey in soil or water. It deftly moves its long and skinny beak to pluck prey.", Image = url + "22.png", Type = "Normal/Flying", ability = "Keen Eye", attacks = "Peck, Leer, Fury Attack, Drill Peck", evolutionLine = "8", evolutionStage = "2" } },
            {23, new Pokemon { ID = 23, Name = "Ekans", Description = "Ekans curls itself up in a spiral while it rests. Assuming this position allows it to quickly respond to an enemy from any direction with a threat from its upraised head.", Image = url + "23.png", Type = "Poison", ability = "Intimidate", attacks = "Wrap, Poison Sting, Bite, Acid", evolutionLine = "9", evolutionStage = "1" } },
            {24, new Pokemon { ID = 24, Name = "Arbok", Description = "This Pokémon is terrifically strong in order to constrict things with its body. It can even flatten steel oil drums. Once Arbok wraps its body around its foe, escaping its crunching embrace is impossible.", Image = url + "24.png", Type = "Poison", ability = "Intimidate", attacks = "Wrap, Poison Sting, Bite, Acid", evolutionLine = "9", evolutionStage = "2" } },
            {25, new Pokemon { ID = 25, Name = "Pikachu", Description = "Whenever Pikachu comes across something new, it blasts it with a jolt of electricity. If you come across a blackened berry, it's evidence that this Pokémon mistook the intensity of its charge.", Image = url + "25.png", Type = "Electric", ability = "Static", attacks = "Quick Attack, Thunder Shock, Thunderbolt, Thunder", evolutionLine = "10", evolutionStage = "1" } },
            {26, new Pokemon { ID = 26, Name = "Raichu", Description = "If the electrical sacs become excessively charged, Raichu plants its tail in the ground and discharges. Scorched patches of ground will be found near this Pokémon's nest.", Image = url + "26.png", Type = "Electric", ability = "Static", attacks = "Quick Attack, Thunder Shock, Thunderbolt, Thunder", evolutionLine = "10", evolutionStage = "2" } },
            {27, new Pokemon { ID = 27, Name = "Sandshrew", Description = "Sandshrew's body is configured to absorb water without waste, enabling it to survive in an arid desert. This Pokémon curls up to protect itself from its enemies.", Image = url + "27.png", Type = "Ground", ability = "Sand Veil", attacks = "Scratch, Sand Attack, Dig, Earthquake", evolutionLine = "11", evolutionStage = "1" } },
            {28, new Pokemon { ID = 28, Name = "Sandslash", Description = "Sandslash's body is covered by tough spikes, which are hardened sections of its hide. Once a year, the old spikes fall out, to be replaced with new spikes that grow out from beneath the old ones.", Image = url + "28.png", Type = "Ground", ability = "Sand Veil", attacks = "Scratch, Sand Attack, Dig, Earthquake", evolutionLine = "11", evolutionStage = "2" } },
            {29, new Pokemon { ID = 29, Name = "Nidoran♀", Description = "Nidoran♀ has barbs that secrete a powerful poison. They are thought to have developed as protection for this small-bodied Pokémon. When enraged, it releases a horrible toxin from its horn.", Image = url + "29.png", Type = "Poison", ability = "Poison Point", attacks = "Scratch, Poison Sting, Bite, Poison Jab", evolutionLine = "12", evolutionStage = "1" } },
            {30, new Pokemon { ID = 30, Name = "Nidorina", Description = "When Nidorina are with their friends or family, they keep their barbs tucked away to prevent hurting each other. This Pokémon appears to become nervous if separated from the others.", Image = url + "30.png", Type = "Poison", ability = "Poison Point", attacks = "Scratch, Poison Sting, Bite, Poison Jab", evolutionLine = "12", evolutionStage = "2" } },
            {31, new Pokemon { ID = 31, Name = "Nidoqueen", Description = "Nidoqueen's body is encased in extremely hard scales. It is adept at sending foes flying with harsh tackles. This Pokémon is at its strongest when it is defending its young.", Image = url + "31.png", Type = "Poison/Ground", ability = "Poison Point", attacks = "Scratch, Poison Sting, Bite, Poison Jab", evolutionLine = "12", evolutionStage = "3" } },
            {32, new Pokemon { ID = 32, Name = "Nidoran♂", Description = "Nidoran♂ has developed muscles for moving its ears. Thanks to them, the ears can be freely moved in any direction. Even the slightest sound does not escape this Pokémon's notice.", Image = url + "32.png", Type = "Poison", ability = "Poison Point", attacks = "Peck, Poison Sting, Bite, Poison Jab", evolutionLine = "13", evolutionStage = "1" } },
            {33, new Pokemon { ID = 33, Name = "Nidorino", Description = "Nidorino has a horn that is harder than a diamond. If it senses a hostile presence, all the barbs on its back bristle up at once, and it challenges the foe with all its might.", Image = url + "33.png", Type = "Poison", ability = "Poison Point", attacks = "Peck, Poison Sting, Bite, Poison Jab", evolutionLine = "13", evolutionStage = "2" } },
            {34, new Pokemon { ID = 34, Name = "Nidoking", Description = "Nidoking's thick tail packs enormously destructive power. With one swing, it can topple a metal transmission tower. Once this Pokémon goes on a rampage, there is no stopping it.", Image = url + "34.png", Type = "Poison/Ground", ability = "Poison Point", attacks = "Peck, Poison Sting, Bite, Poison Jab", evolutionLine = "13", evolutionStage = "3" } },
            {35, new Pokemon { ID = 35, Name = "Clefairy", Description = "Clefairy moves by skipping lightly as if it were flying using its wings. Its bouncy step lets it even walk on water. It is known to take strolls on lakes on quiet, moonlit nights.", Image = url + "35.png", Type = "Fairy", ability = "Cute Charm", attacks = "Pound, Sing, Moonblast, Metronome", evolutionLine = "14", evolutionStage = "1" } },
            {36, new Pokemon { ID = 36, Name = "Clefable", Description = "Clefable has an acute sense of hearing. It can discern the location and size of its prey—even when it can't be seen. This Pokémon is known to dig through the earth in search of food.", Image = url + "36.png", Type = "Fairy", ability = "Cute Charm", attacks = "Pound, Sing, Moonblast, Metronome", evolutionLine = "14", evolutionStage = "2" } },
            {37, new Pokemon { ID = 37, Name = "Vulpix", Description = "Inside Vulpix's body burns a flame that never goes out. During the daytime, when the temperatures rise, this Pokémon releases flames from its mouth to prevent its body from growing too hot.", Image = url + "37.png", Type = "Fire", ability = "Flash Fire", attacks = "Ember, Quick Attack, Flamethrower, Fire Spin", evolutionLine = "15", evolutionStage = "1" } },
            {38, new Pokemon { ID = 38, Name = "Ninetales", Description = "Ninetales casts a sinister light from its bright red eyes to gain total control over its foe's mind. This Pokémon is said to live for a thousand years.", Image = url + "38.png", Type = "Fire", ability = "Flash Fire", attacks = "Ember, Quick Attack, Flamethrower, Fire Spin", evolutionLine = "15", evolutionStage = "2" } },
            {39, new Pokemon { ID = 39, Name = "Jigglypuff", Description = "Jigglypuff's vocal cords can freely adjust the wavelength of its voice. This Pokémon uses this ability to sing at precisely the right wavelength to make its foes most drowsy.", Image = url + "39.png", Type = "Normal/Fairy", ability = "Cute Charm", attacks = "Pound, Sing, Hyper Voice, Double Slap", evolutionLine = "16", evolutionStage = "1" } },
            {40, new Pokemon { ID = 40, Name = "Wigglytuff", Description = "Wigglytuff's body is very flexible. By inhaling deeply, this Pokémon can inflate itself seemingly without end. Once inflated, Wigglytuff bounces along lightly like a balloon.", Image = url + "40.png", Type = "Normal/Fairy", ability = "Cute Charm", attacks = "Pound, Sing, Hyper Voice, Double Slap", evolutionLine = "16", evolutionStage = "2" } },
            {41, new Pokemon { ID = 41, Name = "Zubat", Description = "Zubat remains quietly unmoving in a dark spot during the bright daylight hours. It does so because prolonged exposure to the sun causes its body to become slightly burned.", Image = url + "41.png", Type = "Poison/Flying", ability = "Inner Focus", attacks = "Leech Life, Supersonic, Wing Attack, Poison Fang", evolutionLine = "17", evolutionStage = "1" } },
            {42, new Pokemon { ID = 42, Name = "Golbat", Description = "Golbat loves to drink the blood of living things. It is particularly active in the pitch black of night. This Pokémon flits around in the night skies, seeking fresh blood.", Image = url + "42.png", Type = "Poison/Flying", ability = "Inner Focus", attacks = "Leech Life, Supersonic, Wing Attack, Poison Fang", evolutionLine = "17", evolutionStage = "2" } },
            {43, new Pokemon { ID = 43, Name = "Oddish", Description = "During the day, it stays in the cold underground to avoid the sun. It grows by bathing in moonlight.", Image = url + "43.png", Type = "Grass/Poison", ability = "Chlorophyll", attacks = "Absorb, Acid, Giga Drain, Solar Beam", evolutionLine = "18", evolutionStage = "1" } },
            {44, new Pokemon { ID = 44, Name = "Gloom", Description = "The fluid that oozes from its mouth isn't drool. It is a nectar that is used to attract prey.", Image = url + "44.png", Type = "Grass/Poison", ability = "Chlorophyll", attacks = "Absorb, Acid, Giga Drain, Solar Beam", evolutionLine = "18", evolutionStage = "2" } },
            {45, new Pokemon { ID = 45, Name = "Vileplume", Description = "It has the world's largest petals. With every step, the petals shake out heavy clouds of toxic pollen.", Image = url + "45.png", Type = "Grass/Poison", ability = "Chlorophyll", attacks = "Absorb, Acid, Giga Drain",  evolutionLine = "18", evolutionStage = "3" }}

        };

        [HttpGet(Name = "GetPokemon")]
        public IEnumerable<Pokemon> Get()
        {
            return pokemons.Values;
        }

        [HttpGet("{id}", Name = "GetPokemonByID")]
        public IActionResult Get(int id)
        {
            if (pokemons.ContainsKey(id))
            {
                return Ok(pokemons[id]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("name/{name}")]
        public ActionResult<Pokemon> GetByName(string name)
        {
            foreach (var pokemon in pokemons.Values)
            {
                if (pokemon.Name.ToLower() == name.ToLower())
                {
                    return pokemon;
                }
            }

            return NotFound();
        }


        [HttpGet("type/{type}")]
        public ActionResult<IEnumerable<Pokemon>> GetByType(string type)
        {
            List<Pokemon> result = new List<Pokemon>();

            foreach (var pokemon in pokemons.Values)
            {
                if (pokemon.Type.ToLower().Contains(type.ToLower()))
                {
                    result.Add(pokemon);
                }
            }

            if (result.Count > 0)
            {
                return result;
            }

            return NotFound();
        }

            [HttpGet("ability/{ability}")]
            public ActionResult<List<Pokemon>> GetByAbility(string ability)
            {
                List<Pokemon> result = new List<Pokemon>();

                foreach (var pokemon in pokemons.Values)
                {
                    if (pokemon.ability.ToLower() == ability.ToLower())
                    {
                        result.Add(pokemon);
                    }
                }

                if (result.Count > 0)
                {
                    return result;
                }

                return NotFound();
            }


            [HttpGet("attack/{attack}")]
            public ActionResult<List<Pokemon>> GetByAttack(string attack)
            {
                List<Pokemon> result = new List<Pokemon>();

                foreach (var pokemon in pokemons.Values)
                {
                    string[] attacks = pokemon.attacks.Split(',').Select(a => a.Trim()).ToArray();
                    if (attacks.Contains(attack, StringComparer.OrdinalIgnoreCase))
                    {
                        result.Add(pokemon);
                    }
                }

                if (result.Count > 0)
                {
                    return result;
                }

                return NotFound();
            }


        [HttpGet("evolutionline/{name}")]
        public ActionResult<List<Pokemon>> GetEvolutionLine(string name)
        {
            var pokemon = pokemons.Values.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (pokemon == null)
            {
                return NotFound();
            }

            var evolutionLine = pokemons.Values.Where(p => p.evolutionLine == pokemon.evolutionLine).OrderBy(p => p.evolutionStage).ToList();

            return evolutionLine;
        }

        [HttpGet("evolutionstage/{stage}")]
        public ActionResult<IEnumerable<Pokemon>> GetByStage(string stage)
        {
            List<Pokemon> result = new List<Pokemon>();

            foreach (var pokemon in pokemons.Values)
            {
                if (pokemon.evolutionStage == stage)
                {
                    result.Add(pokemon);
                }
            }

            if (result.Count > 0)
            {
                return result;
            }

            return NotFound();
        }


        [HttpGet("team")]
        public ActionResult<List<Pokemon>> GenerateTeam()
        {
            // Agrupar Pokémon por tipo y etapa final de evolución
            Dictionary<string, List<Pokemon>> pokemonByType = new Dictionary<string, List<Pokemon>>();
            foreach (var pokemon in pokemons.Values)
            {
                if (IsFinalEvolution(pokemon))
                {
                    string[] types = pokemon.Type.Split('/');
                    foreach (string type in types)
                    {
                        if (!pokemonByType.ContainsKey(type))
                        {
                            pokemonByType[type] = new List<Pokemon>();
                        }
                        pokemonByType[type].Add(pokemon);
                    }
                }
            }

            List<Pokemon> team = new List<Pokemon>();
            Random random = new Random();
            List<string> usedTypes = new List<string>();
            while (team.Count < 6)
            {
                string type;
                do
                {
                    type = pokemonByType.Keys.ElementAt(random.Next(pokemonByType.Count));
                } while (usedTypes.Contains(type));

                Pokemon pokemon = pokemonByType[type][random.Next(pokemonByType[type].Count)];

                if (!team.Contains(pokemon))
                {
                    team.Add(pokemon);
                    usedTypes.Add(type);
                }
            }

            return team;
        }

        private bool IsFinalEvolution(Pokemon pokemon)
        {
            var sameLine = pokemons.Values.Where(p => p.evolutionLine == pokemon.evolutionLine);
            return pokemon.evolutionStage == sameLine.Max(p => p.evolutionStage);
        }


    }
}

