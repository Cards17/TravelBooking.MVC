using System.ComponentModel;

namespace TravelBooking.MVC.Models
{
    public class UserAccountViewModel
    {
        public int UserAccountId { get; set; }
        [DisplayName("First Name")]
        public required string FirstName { get; set; }
        [DisplayName("Last Name")]
        public required string LastName { get; set; }
        public required string Gender { get; set; }
        [DisplayName("Home Address")]
        public required string HomeAddress { get; set; }
        [DisplayName("Email Address")]
        public required string EmailAddress { get; set; }
        [DisplayName("Phone Number")]
        public required string PhoneNumber { get; set; }
        [DisplayName("User Name")]
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
