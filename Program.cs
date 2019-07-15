using System;
using System.Collections.Generic;
using AuditToolKit;
namespace ProcessTests
{

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Job { get; set; }

        public Person() { }
        public Person(string name, int age, string job)
        {
            Name = name;
            Age = age;
            Job = job;
        }
    }
    public class StatusObject
    {
        // 0 = pass; 1 = warn; 2 = fail
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string ProcessName { get; set; }

        public StatusObject(string pname, int code, string msg)
        {
            StatusCode = code;
            StatusMessage = msg;
            ProcessName = pname;
        }
    }


    public static class AuditRunner
    {

        public delegate List<StatusObject> AuditFunction(List<StatusObject> status);
        public delegate AuditFunction PersonAudit(Person person1, Person person3);
        public static List<StatusObject> RunAudits(Person bob, Person Joe, List<PersonAudit> Audits)
        {
            var Results = new List<StatusObject>();
            while (Audits.Count > 0)
            {
                Results = Audits[0](bob, Joe)(Results);
                Audits.RemoveAt(0);
            }
            return Results;
        }
    }
    class Program
    {
       
       static AuditRunner.PersonAudit CheckKeys(string[] keys){
           return (p2, p3)=>{
               foreach(var key in keys){
                   System.Console.WriteLine(key);
               }
               return r => r;
           };
       }
        static void Main(string[] args)
        {
            var bob = new Person("bob", 1, "Programmer");
            var Joe = new Person("Joe", 1, "Rocket Scientist");
            
            var audits = new List<AuditRunner.PersonAudit>() { AuditTools.BothCanLegallyDrive, AuditTools.BothAreNotBabies, AuditTools.NotSamePerson, CheckKeys(new string[]{"HEllo", "World"})};
            var res = AuditRunner.RunAudits(bob, Joe, audits);
            foreach (var r in res)
                System.Console.WriteLine("The process " + r.ProcessName + " emitted the status " + r.StatusCode + " with the following message:\n" + r.StatusMessage);
        }
    }
}
