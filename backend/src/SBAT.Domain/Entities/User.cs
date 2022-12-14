using SBAT.Domain.ValueObjects;

namespace SBAT.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string Surname { get; private set; } = string.Empty;
        public Identification Identification { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public int Age { get; private set; }
    }
}