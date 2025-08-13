namespace PracticingDDD.Logic
{
    public  abstract class Entity
    {
        public long Id { get; private set;  }

        public override bool Equals(object obj)
        {
            // cast object to Entity
           var other = obj as Entity;
            // Check if the object is null
            if (ReferenceEquals(other, null)) return false;
            // Check if the object is the same instance
            if (ReferenceEquals(this, other)) return true;
            // Check if the object is of the same type
            if (other.GetType() != GetType()) return false;
            // Check if Id is zero for either entity
            if (Id == 0 || other.Id == 0) return false;
            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            // Check if both are null
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) 
                return true;
            // Check if one is null
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            // Generate a hash code based on the type and Id
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}