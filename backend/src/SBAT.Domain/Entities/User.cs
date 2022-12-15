using System;
using SBAT.Domain.Enums;
using SBAT.Domain.Interfaces;

namespace SBAT.Domain.Entities
{
    #pragma warning disable CS8618
    public class User : EntityBase
    {
        public string FirstNames { get; private set; }
        public string Surname { get; private set; }
        public IdentityType IdentityType { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public int Age { get; private set; }
    }
}