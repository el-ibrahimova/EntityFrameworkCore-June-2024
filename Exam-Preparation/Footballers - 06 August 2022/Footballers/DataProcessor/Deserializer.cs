using System.Globalization;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Xml.Serialization;
using Footballers.Data.Models;
using Footballers.Data.Models.Enums;
using Footballers.DataProcessor.ImportDto;
using Newtonsoft.Json;

namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using System.ComponentModel.DataAnnotations;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            XmlRootAttribute root = new XmlRootAttribute("Coaches");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCoachesDto[]), root);

            using StringReader reader = new StringReader(xmlString);
            var coachesDtos = (ImportCoachesDto[])serializer.Deserialize(reader);

            List<Coach> validCoaches = new();
            StringBuilder sb = new();

            foreach (var coachDto in coachesDtos)
            {
                if (!IsValid(coachDto)
                    || string.IsNullOrEmpty(coachDto.Nationality))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Coach coach = new Coach()
                {
                    Name = coachDto.Name,
                    Nationality = coachDto.Nationality,
                };

                foreach (var footballerDto in coachDto.Footballers)
                {
                    if (!IsValid(footballerDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var startDate = DateTime.ParseExact(footballerDto.ContractStartDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);
                    var endDate = DateTime.ParseExact(footballerDto.ContractEndDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);

                    if (startDate >= endDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Footballer footballer = new Footballer()
                    {
                        Name = footballerDto.Name,
                        ContractStartDate = startDate,
                        ContractEndDate = endDate,
                        BestSkillType = (BestSkillType)footballerDto.BestSkillType,
                        PositionType = (PositionType)footballerDto.PositionType
                    };

                    coach.Footballers.Add(footballer);
                }
                validCoaches.Add(coach);
                sb.AppendLine(string.Format(SuccessfullyImportedCoach, coach.Name, coach.Footballers.Count));
            }

            context.Coaches.AddRange(validCoaches);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
          var teamsDtos = JsonConvert.DeserializeObject<ImportTeamsDto[]>(jsonString);

            List<Team> validTeams = new();
            StringBuilder sb = new();

            var existingFootballersIds = context.Footballers
                .Select(t => t.Id)
                .ToArray();

            foreach (var teamDto in teamsDtos)
            {
                if (!IsValid(teamDto) 
                    || teamDto.Trophies <1)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Team team = new Team()
                {
                    Name = teamDto.Name,
                    Nationality = teamDto.Nationality,
                    Trophies = teamDto.Trophies
                };

                foreach (var footballerId in teamDto.Footballes.Distinct())
                {
                    if (!existingFootballersIds.Contains(footballerId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    TeamFootballer tf = new TeamFootballer()
                    {
                        Team = team,
                        FootballerId = footballerId
                    };

                    team.TeamsFootballers.Add(tf);
                }

                validTeams.Add(team);
                sb.AppendLine(string.Format(SuccessfullyImportedTeam, team.Name, team.TeamsFootballers.Count));
            }

            context.Teams.AddRange(validTeams);
            context.SaveChanges();

            return sb.ToString().ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
