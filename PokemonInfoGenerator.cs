using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PokeAPI
{
    internal class PokemonInfoGenerator
    {
        //get pokemon weight, height, typing, and all descriptions from each game version.
        public static void PokeDescription()
        {
            var client = new HttpClient();

            string pokemonResponse = null;

            //API call for pokemon info, activates try-catch if invalid pokemon name and prompts user again.
            while (true)
            {
                try
                {
                    Console.Write("Type the name of the Pokémon you would like info on: ");

                    var chosenPokemon = Console.ReadLine();

                    var pokemonURL = "https://pokeapi.co/api/v2/pokemon/" + chosenPokemon.ToLower();

                    pokemonResponse = client.GetStringAsync(pokemonURL).Result;

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid pokemon name entered, enter valid name.");
                }
            }

            //Get height and weight of pokemon, then print to console.
            var pokemonHeight = Convert.ToInt32(JObject.Parse(pokemonResponse)["height"].ToString()) * 0.328084; //convert decimeters to feet
            var pokemonWeight = Convert.ToInt32(JObject.Parse(pokemonResponse)["weight"].ToString()) * 0.220462; //convert hectograms to pounds

            Console.WriteLine("This pokemon stands at " + Math.Round(pokemonHeight, 2) + 
                                " feet tall, and weighs about " + Math.Round(pokemonWeight, 1) + " pounds, ");

            //Get types and print to console.
            var pokemonTypes = JObject.Parse(pokemonResponse)["types"];

            Console.Write("and is of type");

            foreach (var type in pokemonTypes)
            {
                Console.Write(" " + type["type"]["name"].ToString());
            }

            Console.WriteLine(".");


            //Second API call to get "flavor_text"/descriptions from each game.
            var pokemonSpeciesURL = JObject.Parse(pokemonResponse)["species"]["url"].ToString();
            var pokemonSpeciesResponse = client.GetStringAsync(pokemonSpeciesURL).Result;

            //Parse second API call to get all flavor texts, then loop through and display all those in English.
            var pokemonFlavorTexts = JObject.Parse(pokemonSpeciesResponse)["flavor_text_entries"];

            foreach (var flavorText in pokemonFlavorTexts)
            {
                if (flavorText["language"]["name"].ToString() == "en")
                {
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    Console.WriteLine("From game version: " + flavorText["version"]["name"].ToString());
                    Console.WriteLine(flavorText["flavor_text"].ToString());
                }
            }

            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------------");
        }


    }
}
