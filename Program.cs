using System;
using System.Collections.Generic;
using Locations;
using Auditor;
using Auditor.AuditTools;
using HttpGrabberFunctions;
using System.Threading.Tasks;
using AirnomadAudits.GroupCategory;
using AirnomadAudits.LocationAudits;
using People;
using AirnomadAudits.PeopleAudits;

namespace ProcessTests
{


    class Program
    {


        public static List<Location> getLocations()
        {
            var PizzaPieCafe = new Location("Pizza Pie Cafe", new Coordinate(43.831349, -111.777595), 3.6m);
            var Byuitechops = new Location("Byui Technical Operations", new Coordinate(43.818066, -111.779793), 5.6m);
            var Promenade = new Location("Promenade", new Coordinate(13.825753, 100.676943), 4.3m);
            var RexburgRapids = new Location("Rexburg Rapids", new Coordinate(43.831333, -111.786061), 2.3m);
            var locations = new List<Location>() { PizzaPieCafe, Byuitechops, RexburgRapids, Promenade };
            return locations;
        }
        public static List<Person> getPeople()
        {
            var Paul = new Person("Paul", 72);
            var Josh = new Person("Josh", 86);
            var Chad = new Person("Chad", 23);
            var Phill = new Person("Phill", 35);
            var Ben = new Person("Ben", 4);
         
            var people = new List<Person>() { Paul, Josh, Phill, Ben, Chad };
            return people;
        }

        static void Main(string[] args)
        {
            // await GroupCategoryAudit.ExecuteAudit("59796", "");
            // return;


            var StatusThinggies = new List<StatusCollection>();
            var LocationAuditor = new LocationAudit();
            var locations = getLocations();
            foreach (var location in locations)
                StatusThinggies.Add(LocationAuditor.ExecuteAudit(location));

            var PeopleAuditor = new PersonAudit();
            var persons = getPeople();
            foreach (var person in persons)
                StatusThinggies.Add(PeopleAuditor.ExecuteAudit(person));


            foreach (var results in StatusThinggies)
            {
                ConsoleReport.ConsoleRep.Log("Showing Audit Results for the Audit: " + results.CollectionTitle, ConsoleColor.DarkMagenta, ConsoleColor.DarkYellow);
                foreach (var result in results.StatusObjects)
                {
                    result.PrintStatusMessage();
                }
                System.Console.WriteLine("********************");
            }

        }
    }
}

