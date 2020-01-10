using System;

namespace BlogProject.Common
{
    public sealed class LookupItem:IEquatable<LookupItem>
    {
        public LookupItem(string id,string Name,int order)
        {
            this.Id = id;
            this.Name = Name;
            this.Order = order;
        }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public int Order { get; private set; }

        public bool Equals(LookupItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(this.Id, other.Id) && string.Equals(this.Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != this.GetType()) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals((LookupItem)obj);
        }

        public override int GetHashCode()
        {
            return this.Id == null ? 0 : this.Id.GetHashCode();
        }

        public static bool operator ==(LookupItem left, LookupItem right)
        {
            return Equals(left,right);
        }

        public static bool operator !=(LookupItem left, LookupItem right)
        {
            return !Equals(left, right);
        }
    }
}
