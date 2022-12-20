using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterInfor.Domain.Models
{
    public class Employe : IEquatable<Employe>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string Position { get; set; }
        public DateTime? DateBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public bool Equals([AllowNull] Employe other)
        {
            if (other == null)
                return false;
            return GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : Guid.Empty.GetHashCode())
                ^ (FirstName != null ? FirstName.GetHashCode() : "".GetHashCode())
                ^ (SecondName != null ? SecondName.GetHashCode() : "".GetHashCode())
                ^ (ThirdName != null ? ThirdName.GetHashCode() : "".GetHashCode())
                ^ (Position != null ? Position.GetHashCode() : "".GetHashCode())
                ^ (DateBirth != null ? DateBirth.GetHashCode() : "".GetHashCode())
                ^ (PhoneNumber != null ? PhoneNumber.GetHashCode() : "".GetHashCode())
                ^ (Email != null ? Email.GetHashCode() : "".GetHashCode());
        }
    }
}
