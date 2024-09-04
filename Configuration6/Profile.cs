using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Configuration6
{
    public class Profile : IEquatable<Profile>
    {
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; } 

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 联系信息
        /// </summary>
        public ContactInfo ContactInfo { get; set; }

        public Profile() { }

        public Profile(Gender gender, int age, string emailAddress, string phoneNo)
        {
            Gender = gender;
            Age = age;
            ContactInfo = new ContactInfo
            {
                EmailAddress = emailAddress,
                PhoneNo = phoneNo
            };
        }

        public bool Equals(Profile other)
        {
            var n = other == null
                ? false
                : Gender == other.Gender &&
                 Age == other.Age &&
                 ContactInfo == other.ContactInfo;
            return n;
        }
    }

    public class ContactInfo : IEquatable<ContactInfo>
    {
        public string EmailAddress { get; set; }

        public string PhoneNo { get; set; }

        public bool Equals(ContactInfo other)
        {
            return other == null
                ? false
                : EmailAddress == other.EmailAddress && PhoneNo == other.PhoneNo;
        }
    }

    public enum Gender 
    {
        Male,
        Female
    }
}
