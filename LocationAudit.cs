using System.Collections.Generic;
using Auditor;
using Auditor.AuditTools;
using Locations;

namespace AirnomadAudits.LocationAudits
{
    public class LocationAudit
    {
        public LocationAudit() { }

        public AuditResultProcess IsNotJelloWorld(Location l)
        {
            var res = new List<StatusObject>();
            if (l.Name == "Jello World")
                res.Add(new StatusObject(2, "Not Jello World", "This is Jello World!!!"));
            else
                res.Add(new StatusObject(2, "Not Jello World", "This is Jello World!!!"));
            return AuditTools.PipeResults(res);
        }
        public StatusCollection ExecuteAudit(Location location)
        {

            var testLoc = new Location("", new Coordinate(0, 0), 5.6m);
            var auditz = new List<AuditRunner.AuditMethod<Location>>(){
                    AuditTools.LocationPopular,
                    AuditTools.NotInNawamin,
                    AuditTools.AttributesAreSame(testLoc, new string[]{"Rating"}),
                    IsNotJelloWorld
                };
            StatusCollection results = AuditRunner.RunAudits<Location>(location.Name /*Name for result collections*/, location /* Object to run audits on*/, auditz /* Audits to run */);
            return results;

        }
    }
}