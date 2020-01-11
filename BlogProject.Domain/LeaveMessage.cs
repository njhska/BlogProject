using System;
using BlogProject.Common;

namespace BlogProject.Domain
{
    public sealed class LeaveMessage : IValueObject, IEquatable<LeaveMessage>
    {
        public LeaveMessage(string visitorName, string message, DateTimeOffset leaveTime = default(DateTimeOffset))
        {
            this.VisitorName = visitorName;
            this.Message = message;
            this.LeaveTime = leaveTime == default(DateTimeOffset) ? DateTimeOffset.Now : leaveTime;
        }
        public string VisitorName { get; private set; }
        public string Message { get; private set; }
        public DateTimeOffset LeaveTime { get; private set; }

        public bool Equals(LeaveMessage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(this.VisitorName, other.VisitorName) && DateTimeOffset.Equals(this.LeaveTime, other.LeaveTime);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (this.GetType() != obj.GetType()) return false;
            if (ReferenceEquals(this, obj)) return true;
            return this.Equals((LeaveMessage)obj);
        }

        public override int GetHashCode()
        {
            var result = VisitorName == null ? 0 : VisitorName.GetHashCode();
            result = result ^ LeaveTime.GetHashCode();
            return result;
        }

        public static bool operator ==(LeaveMessage left, LeaveMessage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LeaveMessage left, LeaveMessage right)
        {
            return !Equals(left, right);
        }
    }
}
