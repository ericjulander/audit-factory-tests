using System.Collections.Generic;
using ProcessTests;

namespace AuditToolKit
{

    public static class AuditTools
    {

        public static AuditRunner.AuditFunction BothCanLegallyDrive(Person person1, Person person2)
        {
            var AuditResults = new List<StatusObject>();
            var ProcessName = "BothLegalDrivers";
            if (person1.Age >= 16 && person2.Age >= 16)
                AuditResults.Add(new StatusObject(ProcessName, 0, person1.Name + " and " + person2.Name + " Can Legally Drive!"));
            else if (person1.Age >= 14 || person2.Age >= 14)
            {
                AuditResults.Add(new StatusObject(ProcessName, 1, person1.Name + " or " + person2.Name + " Can Possibly Drive With a permit!"));
            }
            else
            {
                AuditResults.Add(new StatusObject(ProcessName, 2, person1.Name + " and " + person2.Name + " Cannot Possibly Drive without a permit!"));
            }

            return (results =>
            {
                foreach (var result in AuditResults)
                    results.Add(result);
                return results;
            });
        }
        public static AuditRunner.AuditFunction BothAreNotBabies(Person person1, Person person2)
        {
            var AuditResults = new List<StatusObject>();
            var ProcessName = "BothNotBabies";

            if (person1.Age >= 4 && person2.Age >= 4)
                AuditResults.Add(new StatusObject(ProcessName, 0, person1.Name + " and " + person2.Name + " Are not babies!"));
            else if (person1.Age >= 2 || person2.Age >= 2)
            {
                AuditResults.Add(new StatusObject(ProcessName, 1, person1.Name + " or " + person2.Name + " could possibly be a baby"));
            }
            else
            {
                AuditResults.Add(new StatusObject(ProcessName, 2, person1.Name + " and " + person2.Name + " are babies!!!"));
            }

            return (results =>
            {
                foreach (var result in AuditResults)
                    results.Add(result);
                return results;
            });
        }
        public static AuditRunner.AuditFunction NotSamePerson(Person person1, Person person2)
        {
            var AuditResults = new List<StatusObject>();
            var ProcessName = "BothNotBabies";

            if (person1.Age != person2.Age && !person1.Name.Equals(person2.Name) && !person1.Job.Equals(person2.Job))
                AuditResults.Add(new StatusObject(ProcessName, 0, person1.Name + " and " + person2.Name + " are different people!"));
            else
            {
                AuditResults.Add(new StatusObject(ProcessName, 2, person1.Name + " and " + person2.Name + " are the same person!!!"));
            }

            return (results =>
            {
                foreach (var result in AuditResults)
                    results.Add(result);
                return results;
            });
        }

    }
}