using System;

namespace LibrarySystem.Models
{
    public class Reader : BaseEntity
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredDate { get; set; }

        public override string ToString() => FullName;
    }
}
