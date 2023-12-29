using RandomWins.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Core.OtherObjects
{
    public class User
    {
        public User(string firstName, string lastName, string emailAddress, string? middleName = null)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;

            if (middleName != null)
            {
                MiddleName = middleName;
            }
        }

        public string FirstName { get; init; }
        public string? MiddleName { get; private set; }
        public string LastName { get; init; }

        public string EmailAddress { get; set; }

        public string GetFullName
        {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }
    }
}


