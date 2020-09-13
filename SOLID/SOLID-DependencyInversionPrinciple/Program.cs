using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLID_DependencyInversionPrinciple
{

    public enum Relationship
    {
        Parent, Child, Sibling
    }

    public class Person
    {
        public string Name;
        public DateTime DateOfBirth;

    }


    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    // low level part of system
    public class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();
        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations.Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent).Select(r => r.Item3);

        }

        // public List<(Person, Relationship, Person)> Relations => relations;
    }


    class Research
    {
        public Research(IRelationshipBrowser browser)
        {
            foreach (var r in browser.FindAllChildrenOf("Philip"))
            {
                Console.WriteLine($"Philip has a child named {r.Name}");
            }
        }

        // public Research(Relationships relationships)
        // {
        //     var relations = relationships.Relations;
        //     Console.WriteLine(relations.Count);

        //     foreach (var r in relations.Where(
        //                                  x => x.Item1.Name == "Philip" && x.Item2 == Relationship.Parent
        //                                )
        //     )
        //     {
        //         Console.WriteLine($"Philip has a child named {r.Item3.Name}");
        //     }
        // }

        static void Main(string[] args)
        {
            var parent = new Person { Name = "Philip" };
            var child1 = new Person { Name = "Ella" };
            var child2 = new Person { Name = "Mourgella" };
            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);
        }
    }
}
