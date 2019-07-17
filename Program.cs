using System;
using System.Collections.Generic;
using Locations;
using Auditor;
using Auditor.AuditTools;

namespace ProcessTests
{


    class Program
    {
       
        private delegate AuditResultProcess LocationAudit(Location location);
        static void Main(string[] args)
        {
           var RexburgRapids = new Location("Rexburg Rapids", new Coordinate(43.831333, -111.786061), 2.3m);
           var RexburgRapidsCopy = new Location("Rexburg Rapids", new Coordinate(43.831333, -111.786061), 2.3m);
           var PizzaPieCafe = new Location("Pizza Pie Cafe", new Coordinate(43.831349, -111.777595), 3.6m);
           var Byuitechops = new Location("Byui Technical Operations", new Coordinate(43.818066, -111.779793), 5.6m);
           var Promenade = new Location("Promenade", new Coordinate(13.825753, 100.676943), 4.3m);
           var locations = new List<Location>(){RexburgRapids, PizzaPieCafe,Byuitechops, Promenade, RexburgRapidsCopy};
           
           foreach(var location in locations){
                var auditz = new List<AuditRunner.AuditMethod<Location>>(){
                    AuditTools.LocationPopular,
                    AuditTools.NotInNawamin,
                    AuditTools.AttributesAreSame<Location>(Promenade, new string[]{"Rating"}),
                    // AuditTools.HasDuplicate<Location>(locations,true)
                };
                
                StatusCollection results = AuditRunner.RunAudits<Location>(location.Name /*Name for result collections*/,location /* Object to run audits on*/, auditz /* Audits to run */);
                
                ConsoleReport.ConsoleRep.Log("Showing Audit Results for the Audit: "+results.CollectionTitle, ConsoleColor.DarkMagenta, ConsoleColor.DarkYellow);
                foreach(var result in results.StatusObjects){
                    result.PrintStatusMessage();
                }
                
           }
        }
    }
}
