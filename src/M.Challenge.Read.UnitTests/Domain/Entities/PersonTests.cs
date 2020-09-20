using AutoFixture;
using AutoFixture.Idioms;
using FluentAssertions;
using M.Challenge.Read.Domain.Entities;
using M.Challenge.Read.UnitTests.Config.AutoData;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace M.Challenge.Read.UnitTests.Domain.Entities
{
    public class PersonTests
    {
        [Fact]
        public void Sut_ShouldGuardItsClauses()
        {
            var people = new List<Person>
            {
                new Person("Diogo", "da Silva França Marins", "Caboclo", "Masculino", "Ensino Superior Completo"),
                new Person("Paula Stéphanie", "Neves Borges Marins", "Pardo", "Feminino", "Ensino Superior Incompleto"),
            };

            var fixture = new Fixture();

            fixture
                .Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture
                .Behaviors
                .Add(new OmitOnRecursionBehavior());
            fixture
                .Customize<Person>(c => c.With(p => p.Filiation, people));

            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(Person).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void AddFilliation_WhenSonHasFather_ReturnsSonWithFather(Person father, Person son)
        {
            var originalPerson = new Person(
                father.Name,
                father.LastName,
                father.Ethnicity,
                father.Genre,
                father.EducationLevel);

            var originalChild = new Person(
                son.Name,
                son.LastName,
                son.Ethnicity,
                son.Genre,
                son.EducationLevel);

            son.AddFilliation(father);

            son
                .Should()
                .BeEquivalentTo(originalChild,
                    config => config
                        .Excluding(x => x.Id)
                        .Excluding(x => x.Filiation)
                        .Excluding(x => x.Children));

            son
                .Filiation
                .Should()
                .HaveCount(1);

            son
                .Filiation
                .First()
                .Should()
                .BeEquivalentTo(originalPerson,
                    config => config
                        .Excluding(x => x.Id)
                        .Excluding(x => x.Filiation)
                        .Excluding(x => x.Children));
        }

        [Theory, AutoNSubstituteData]
        public void AddChild_WhenFatherHasOneSon_ReturnsFatherWithSon(Person father, Person son)
        {
            var originalPerson = new Person(
                father.Name,
                father.LastName,
                father.Ethnicity,
                father.Genre,
                father.EducationLevel);

            var originalChild = new Person(
                son.Name,
                son.LastName,
                son.Ethnicity,
                son.Genre,
                son.EducationLevel);

            father.AddChild(son);

            father
                .Should()
                .BeEquivalentTo(originalPerson,
                    config => config
                        .Excluding(x => x.Id)
                        .Excluding(x => x.Filiation)
                        .Excluding(x => x.Children));

            father
                .Children
                .Should()
                .HaveCount(1);

            father
                .Children
                .First()
                .Should()
                .BeEquivalentTo(originalChild,
                    config => config
                        .Excluding(x => x.Id)
                        .Excluding(x => x.Filiation)
                        .Excluding(x => x.Children));
        }

        [Theory, AutoNSubstituteData]
        public void AddFilliation_WhenSonHasFatherAndMother_ReturnsSonWithFatherAndMother(Person son, List<Person> parents)
        {
            parents.RemoveAt(2);

            var firstOriginalParents = new Person(
                parents[0].Name,
                parents[0].LastName,
                parents[0].Ethnicity,
                parents[0].Genre,
                parents[0].EducationLevel);

            for (int i = 0; i < parents.Count; i++)
            {
                son.AddFilliation(parents[i]);
            }

            son.Filiation
                .Should()
                .HaveCount(2);

            son.Filiation
                .First()
                .Should()
                .BeEquivalentTo(firstOriginalParents,
                    config => config
                        .Excluding(x => x.Id));
        }

        [Theory, AutoNSubstituteData]
        public void AddChild_WhenFatherHasTwoOrMoreChildren_ReturnsFatherWithTwoOrMoreChildren(Person father, List<Person> children)
        {
            var firstOriginalChild = new Person(
                children[0].Name,
                children[0].LastName,
                children[0].Ethnicity,
                children[0].Genre,
                children[0].EducationLevel);

            for (int i = 0; i < children.Count; i++)
            {
                father.AddChild(children[i]);
            }

            father.Children
                .Should()
                .HaveCount(3);

            father.Children
                .First()
                .Should()
                .BeEquivalentTo(firstOriginalChild,
                    config => config
                        .Excluding(x => x.Id));
        }

        [Theory, AutoNSubstituteData]
        public void AddFilliation_WhenSonHasGrandfather_ReturnsSonWithGrandfather(Person son, Person father, Person grandfather)
        {
            son.AddFilliation(father);

            father.AddFilliation(grandfather);

            son.Filiation
                .Should()
                .HaveCount(1);

            son.Filiation
                .First()
                .Filiation
                .Should()
                .HaveCount(1);
        }

        [Theory, AutoNSubstituteData]
        public void AddChild_WhenFatherHasOneGrandchild_ReturnsFatherWithGrandchild(Person father, Person son, Person grandchild)
        {
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    son.AddChild(grandchild);
                }

                father.AddChild(son);
            }

            father.Children
                .Should()
                .HaveCount(1);

            father.Children
                .First()
                .Children
                .Should()
                .HaveCount(1);
        }
    }
}
