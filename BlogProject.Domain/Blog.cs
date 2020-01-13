using System;
using System.Collections.Generic;
using BlogProject.Common;
using System.Linq;
namespace BlogProject.Domain
{
    public sealed class Blog:BaseEntity
    {
        private Blog() { }
        public Blog(string id)
        {
            this.Id = id;
        }
        public Blog(string id,string title,string content,LookupItem blogType)
        {
            this.Id = id;
            this.Title = title;
            this.Content = content;
            this.LastUpdateTime = DateTimeOffset.Now;
            this.BlogType = blogType;
            this.IsActive = true;
        }
        
        public string Title { get; private set; }
        public string Content { get; private set; }
        public int Hit { get; private set; }
        public DateTimeOffset LastUpdateTime { get; private set; }
        public LookupItem BlogType { get; private set; }
        public List<LeaveMessage> LeaveMessages
        {
            get
            {
                return leaveMessages.OrderBy(x=>x.LeaveTime).ToList();
            }
        }
        public bool IsActive { get; private set; }

        private List<LeaveMessage> leaveMessages = new List<LeaveMessage>();

        public void ChangeTitle(string title)
        {
            this.Title = title;
            this.LastUpdateTime = DateTimeOffset.Now;
        }

        public void ChangeContent(string content)
        {
            this.Content = content;
            this.LastUpdateTime = DateTimeOffset.Now;
        }

        public void Hits()
        {
            this.Hit++;
        }

        public void ChangeBlogType(LookupItem blogType)
        {
            this.BlogType = blogType;
        }

        public void ClearLeaveMessages()
        {
            this.LeaveMessages.Clear();
        }

        public void UpdateLeaveMessages(IList<LeaveMessage> leaveMessages)
        {
            this.leaveMessages = leaveMessages.ToList();
        }

        public void ChangeActive(bool active)
        {
            this.IsActive = active;
        }
    }
}
