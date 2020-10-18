using System;
using System.Collections.Generic;
using MintPlayer.Xamarin.Forms.SortableListView.Demo.Models;

namespace MintPlayer.Xamarin.Forms.SortableListView.Demo.Services
{
    public static class PlayerService
    {
        public static List<Person> GetPlayers()
        {
            return new List<Person>
            {
                new Person
                {
                    Name = "Souza",
                    Position = "Midfield",
                    BirthDay = new DateTime(1989, 2, 11),
                    Gender = Enums.eGender.Male,
                },
                new Person
                {
                    Name = "Jermaine Lens",
                    Position = "Midfield",
                    BirthDay = new DateTime(1987, 10, 24),
                    Gender = Enums.eGender.Female,
                },
                new Person
                {
                    Name = "Miroslav Stoch",
                    Position = "Midfield",
                    BirthDay = new DateTime(1989, 10, 19),
                    Gender = Enums.eGender.Male,
                },
                new Person
                {
                    Name = "Moussa Sow",
                    Position = "Forward",
                    BirthDay = new DateTime(1986, 01, 19),
                    Gender = Enums.eGender.Male,
                },
                new Person
                {
                    Name = "Robin van Persie",
                    Position = "Forward",
                    BirthDay = new DateTime(1983, 9, 6),
                    Gender = Enums.eGender.Male,
                },
            };
        }
    }
}
