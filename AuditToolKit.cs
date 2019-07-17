using System;
using System.Collections.Generic;
using Locations;
using ProcessTests;
using Auditor;
using static Auditor.AuditRunner;
using System.Linq;

namespace Auditor.AuditTools
{
    public static class AuditTools
    {
        public static AuditResultProcess PipeResults(List<StatusObject> results)
        {
            return (AllResults) =>
            {
                foreach (var res in results)
                    AllResults.StatusObjects.Add(res);
                return AllResults;
            };
        }
        public static AuditResultProcess LocationPopular(Location location)
        {
            var results = new List<StatusObject>();
            if (location.Rating >= 4.0m)
            {
                results.Add(new StatusObject(0, "Is the Location Popular", $"Holy Cow! This place is popular!\nPopulatiry Rating:{location.Rating}/5"));
            }
            else if (location.Rating >= 3.5m)
                results.Add(new StatusObject(1, "Is the Location Popular", $"Uhhh... This place is kinda popular\nPopulatiry Rating:{location.Rating}/5"));
            else
                results.Add(new StatusObject(2, "Is the Location Popular", $"NOOOOOO... This place is NOT popular\nPopulatiry Rating:{location.Rating}/5"));

            return PipeResults(results);
        }

        public static AuditResultProcess NotInNawamin(Location location)
        {
            var results = new List<StatusObject>();
            if (!(Math.Truncate(location.Coordinates.Lattitude) == 13 && Math.Truncate(location.Coordinates.Longitude) == 100))
            {
                results.Add(new StatusObject(0, $"The Coordinate is not in Nawamin", $"Yup, that sure isn't in Nawamin\nCoordinates:{location.Coordinates.Lattitude}/{location.Coordinates.Longitude}"));
            }
            else
            {
                results.Add(new StatusObject(2, $"The Coordinate is not in Nawamin", $"Woah!, That is in Nawamin!\nCoordinates:{location.Coordinates.Lattitude}/{location.Coordinates.Longitude}"));
            }
            return PipeResults(results);
        }


        public static AuditRunner.AuditMethod<T> AttributesAreSame<T>(T object2, string[] attributes = null)
        {
            var results = new List<StatusObject>();
            return (object1) =>
            {
                var status = 0;
                if (attributes == null)
                {
                    var properties1 = object2.GetType().GetFields();
                    foreach (var properties in properties1)
                    {
                        System.Console.WriteLine(properties.GetValue(object1).ToString(), (properties.GetValue(object2)).ToString());
                        if (!properties.GetValue(object1).Equals(properties.GetValue(object2)))
                        {
                            results.Add(new StatusObject(1, "Objects are the Same", $"You didn't specify any specific attributes to compare, so we literally compared everything.\nThese object's {properties.Name} do not match!\n{properties.GetValue(object1)} does not equal {properties.GetValue(object2)}"));
                            status = 2;
                            break;
                        }

                    }
                }
                else
                {
                    foreach (var attr in attributes)
                    {
                        try
                        {
                            var prop = object1.GetType().GetField(attr);
                            System.Console.WriteLine(prop.GetValue(object1).ToString(), (prop.GetValue(object2)).ToString());
                            if (!prop.GetValue(object1).Equals(prop.GetValue(object2)))
                            {
                                results.Add(new StatusObject(2, "Objects are the Same?", $"The values for the attribute {attr} don't match!.\n{prop.GetValue(object1)} does not equal {prop.GetValue(object2)}"));
                                status = 1;
                                break;
                            }

                        }
                        catch (Exception e)
                        {
                            results.Add(new StatusObject(-1, "Objects are the Same?", $"Error: {e.Message}"));
                            status = -1;
                            break;
                        }
                    }

                }
                if (status == 0)
                    results.Add(new StatusObject(0, "Objects are the Same?", "The Two Objects are the Same!"));
                return PipeResults(results);
            };
        }


        public static AuditRunner.AuditMethod<T> HasDuplicate<T>(List<T> ObjectsToCompare, bool ListIncludesSelf)
        {
            var results = new List<StatusObject>();
            return (object1) =>
            {
                var matches = 0;
                var fields = object1.GetType().GetFields();
                var properties = object1.GetType().GetProperties();
                foreach (var object2 in ObjectsToCompare)
                {
                    var fieldMatches = 0;
                    foreach (var field in fields)
                    {
                        if (field.GetValue(object1) != (field.GetValue(object2)))
                        {
                            System.Console.WriteLine(field.GetValue(object1) == (field.GetValue(object2)));
                            foreach(var prop in field.GetValue(object1).GetType().GetFields())
                                try{
                                System.Console.WriteLine(prop.GetValue(field.GetValue(object1)).ToString()+" ------ "+prop.GetValue(field.GetValue(object2)).ToString());
                                System.Console.WriteLine(prop.GetValue(field.GetValue(object1)).ToString()==prop.GetValue(field.GetValue(object2)).ToString());
                                }catch(Exception e){}
                            fieldMatches++;
                        }

                    }
                   
                    var propertyMatches = 0;
                    foreach (var property in properties)
                    {
                        if (property.GetValue(object1) == (property.GetValue(object2)))
                        {
                            System.Console.WriteLine(property.GetValue(object1).ToString(), (property.GetValue(object2)).ToString());
                            propertyMatches++;
                        }

                    }

                        System.Console.WriteLine($"{object1.GetType().GetField("Name").GetValue(object2)}  -  {propertyMatches} / {properties.Length}  , {fieldMatches} / {fields.Length}");
                    if (fieldMatches == fields.Length && propertyMatches == properties.Length){
                        matches++;
                    }

                }
                // if(ListIncludesSelf)
                //     matches--;
                System.Console.WriteLine("MATCHES: " + matches);
                return PipeResults(results);
            };
        }
    }

}

//  if (location1.Name.Equals(location2.Name))
//                 {
//                     if ((Math.Truncate(location1.Coordinates.Lattitude * 1000) / 1000 == location1.Coordinates.Lattitude && Math.Truncate(location2.Coordinates.Longitude * 1000) / 1000 == 10))
//                     {
//                         results.Add(new StatusObject(2, "Places are not the Same", "End of the Line Bro!, Those places have the same name and they are at the same place!"));
//                     }
//                     else
//                     {
//                         results.Add(new StatusObject(1, "Places are not the Same", "Woah!, Those places have the same name, they might be the same place!"));
//                     }
//                 }
//                 else
//                 {
//                     results.Add(new StatusObject(0, "Places are not the Same", "Yay!, Those places are not the same"));
//                 }
//                 return PipeResults(results);