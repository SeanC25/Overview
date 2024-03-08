using System;
namespace APG_CRM.Data.Entities
{
    // Add User roles relevant to your application
    public enum Role { admin, manager, survey, customer }  //the different roles from 

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //Data Annotation relationships
        // User role within application
        public Role Role { get; set; }

    }
}
