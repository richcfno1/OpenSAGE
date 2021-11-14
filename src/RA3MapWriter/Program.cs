using System;
using System.IO;
using OpenSage.Data.Map;

namespace RA3MapWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: <Map file name>");
                return;
            }
            var fileStream = File.Open(args[0], FileMode.Open);
            var mapFile = MapFile.FromStream(fileStream);
            fileStream.Close();

            Console.WriteLine(mapFile.ObjectsList.Objects.Length);

            var newMapObject = new MapObject[mapFile.ObjectsList.Objects.Length + 1];
            mapFile.ObjectsList.Objects.CopyTo(newMapObject, 1);
            newMapObject[0] = newMapObject[1];
            mapFile.ObjectsList.Objects = newMapObject;

            fileStream = File.OpenWrite(args[0]);
            mapFile.WriteTo(fileStream);
            fileStream.Close();
        }
    }
}
