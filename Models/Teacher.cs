using Postdb.DTOs;

namespace Postdb.Models;



    public record Teacher
    {
        public long TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public DateTimeOffset BirthDay { get; set; }

        

        public long Mobile { get; set; }
        public string Address { get; set; }
        public DateTimeOffset DateOfJoin { get; set; }
        public string Email { get; set; }

        public string Subject { get; set; }

        




        
     
        
        
        public TeacherDTO asDto => new TeacherDTO
        {
          TeacherId = TeacherId,
          FirstName = FirstName,
          LastName = LastName,
          Gender = Gender,
          Mobile = Mobile,
          Address = Address,          
          Email = Email,
          Subject = Subject
          
        };

    
}  
