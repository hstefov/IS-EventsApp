namespace EmsAdminApp.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string? UserAttendeeId { get; set; }
        public Attendee? UserAttendee { get; set; }
        public virtual ICollection<TicketInOrder>? TicketsInOrder { get; set; }
    }
}
