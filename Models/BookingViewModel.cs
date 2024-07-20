using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBooking.MVC.Models
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        [DisplayName("Distination Address")]
        public required string DistinationAddress { get; set; }
        [DisplayName("Tour Package")]
        public required string TourPackage { get; set; }
        [DisplayName("Travel Date")]
        public required DateTime TravelDate { get; set; }
        [DisplayName("Return Date")]
        public required DateTime ReturnDate { get; set; }
        public int UserAccountId { get; set; }
    }
}
