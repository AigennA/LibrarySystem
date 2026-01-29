using System;
using System.Collections.Generic;
using System.Text;

using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services
{
    public class MemberRegistry
    {
        private List<Member> _members = new();

        public void AddMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));
            if (_members.Any(m => m.MemberId == member.MemberId))
                throw new ArgumentException("A member with this ID already exists");

            _members.Add(member);
        }

        public List<Member> GetAllMembers() => new List<Member>(_members);

        public Member? FindMemberById(string memberId)
        {
            return _members.FirstOrDefault(m => m.MemberId == memberId);
        }
    }
}