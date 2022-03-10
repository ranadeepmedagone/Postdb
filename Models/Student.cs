using Postdb.DTOs;

namespace Postdb.Models;



    public record Student
    {
        public long StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public string Address { get; set; }

        public long ParentMobile { get; set; }
        public string Email { get; set; }

        public DateTimeOffset JoinDate { get; set; }

        public long ClassId { get; set; }




        
     
        
        
        public StudentDTO asDto => new StudentDTO
        {
          StudentId = StudentId,
          FirstName = FirstName,
          LastName = LastName,
          ParentMobile = ParentMobile,
          Email = Email,
          Gender = Gender
        };

    // public long StudentId { get; private set; }
}  
