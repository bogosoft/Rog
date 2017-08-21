using System.ComponentModel.DataAnnotations;

namespace Rog.Tests
{
    class Person
    {
        [MaxLength(16)]
        public string Alias { get; set; }

        [Required, MaxLength(24)]
        public string Name { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Alias))
            {
                return Name;
            }
            else
            {
                return $"{Name} ({Alias})";
            }
        }
    }
}