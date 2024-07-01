using System.Globalization;
using System.Text;

namespace MusicHub
{
    using System;

    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

           DbInitializer.ResetDatabase(context);

          //string albumInfo =  ExportAlbumsInfo(context, 9);
          //Console.WriteLine(albumInfo);

          string songsAboveDuration = ExportSongsAboveDuration(context, 4);
          Console.WriteLine(songsAboveDuration);

          //Test your solutions here
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albumsInfo = context.Albums
                .Where(a => a.ProducerId.HasValue &&
                            a.ProducerId == producerId)
                .ToArray() // we need to write this here for that a.Price cannot be translate from EF, so we materialize the query and after that do the rest of the query
                .OrderByDescending(a => a.Price)
                .Select(a => new
                {
                    a.Name,
                    Release = a.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    ProducerName = a.Producer.Name,
                    Songs = a.Songs
                        .Select(s => new
                        {
                            SongName = s.Name,
                            Price = s.Price.ToString("f2"),
                            WriterName = s.Writer.Name
                        })
                        .OrderByDescending(s => s.SongName)
                        .ThenBy(s => s.WriterName)
                        .ToArray(),
                    AlbumPrice = a.Price.ToString("f2")
                })
                .ToArray();

            StringBuilder sb = new();

            foreach (var a in albumsInfo)
            {
                sb.AppendLine($"-AlbumName: {a.Name}")
                    .AppendLine($"-ReleaseDate: {a.Release}")
                    .AppendLine($"-ProducerName: {a.ProducerName}")
                    .AppendLine($"-Songs:");

                int songNumber = 1;

                foreach (var s in a.Songs)
                {
                    sb.AppendLine($"---#{songNumber}")
                        .AppendLine($"---SongName: {s.SongName}")
                        .AppendLine($"---Price: {s.Price}")
                        .AppendLine($"---Writer: {s.WriterName}");

                    songNumber += 1;
                }

                sb.AppendLine($"-AlbumPrice: {a.AlbumPrice}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songsAboveDuration = context.Songs
                     .AsEnumerable() // EF cannot translate TimeSpan/Duration to int/, so we write .AsEnumerable or .ToList to materialise the query
                     .Where(s=>s.Duration.TotalSeconds>duration)
                .Select(s => new
                {
                    SongName = s.Name,
                    Performer = s.SongPerformers
                        .Select(p => $"{p.Performer.FirstName} {p.Performer.LastName}")
                        .OrderBy(p => p)
                        .ToArray(),
                    WriterName = s.Writer.Name,
                    ProducerName = s.Album!.Producer!.Name,
                    Duration = s.Duration.ToString("c")
                })
                .OrderBy(s => s.SongName)
                .ThenBy(s => s.WriterName)
                .ToArray();

            StringBuilder sb = new();
            int songNumber = 1;

            foreach (var s in songsAboveDuration)
            {
                sb.AppendLine($"-Song #{songNumber}")
                    .AppendLine($"---SongName: {s.SongName}")
                    .AppendLine($"---Writer: {s.WriterName}");
              
                    foreach (var p in s.Performer)
                    {
                        sb.AppendLine($"---Performer: {p}");
                    }

                sb.AppendLine($"---AlbumProducer: {s.ProducerName}")
                    .AppendLine($"---Duration: {s.Duration}");

                songNumber ++;
            }

            return sb.ToString().TrimEnd();
        }
    }
}
