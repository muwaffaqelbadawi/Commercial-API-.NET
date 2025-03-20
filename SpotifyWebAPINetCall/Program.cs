using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SpotifyAPI.Web;

class Program
{
  static async Task Main()
  {
    var spotify = new SpotifyClient("YourAccessToken");

    var track = await spotify.Tracks.Get("1s6ux0lNiTziSrd7iUAADH");
    Console.WriteLine(track.Name);
  }
}
