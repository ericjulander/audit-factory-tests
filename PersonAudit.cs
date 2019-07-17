using System.Collections.Generic;
using Auditor;
using Auditor.AuditTools;
using People;

namespace AirnomadAudits.PeopleAudits
{
    public class PersonAudit
    {
        public PersonAudit() { }

        public AuditResultProcess IsNotChad(Person p)
        {
            var res = new List<StatusObject>();
            if (p.Name == "Chad")
                res.Add(new StatusObject(2, "Not Chad", "This is Chad!!!"));
            else
                res.Add(new StatusObject(0, "Not Chad", p.Name + " is not Chad!!!"));
            return AuditTools.PipeResults(res);
        }

        public AuditResultProcess IsOld(Person p)
        {
            var res = new List<StatusObject>();
            if (p.Age >= 30)
                res.Add(new StatusObject(2, "Is Old", p.Name + " is sooooo old bruh!"));
            else if (p.Age > 18)
                res.Add(new StatusObject(0, "Is Old", p.Name + "is still prety young!"));
            else
                res.Add(new StatusObject(1, "Is Old", p.Name + "is a little too young!"));
           
            return AuditTools.PipeResults(res);
        }
        public StatusCollection ExecuteAudit(Person person)
        {

            var testPerson = new Person("Chad", 35);
            var auditz = new List<AuditRunner.AuditMethod<Person>>(){
                    AuditTools.AttributesAreSame(testPerson, new string[]{"Age"}),
                    IsNotChad,
                    IsOld
                };
            StatusCollection results = AuditRunner.RunAudits<Person>(person.Name /*Name for result collections*/, person /* Object to run audits on*/, auditz /* Audits to run */);
            return results;

        }
    }
}