using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace PokeAPI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Using the pokeAPI to get info on pokemon!
            while (true)
            {
                PokemonInfoGenerator.PokeDescription();
            }
        }
    }
}
