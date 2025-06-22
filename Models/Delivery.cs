namespace Bookstore.Models
{
    public class Delivery
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DeliveryMethod Method { get; set; }
        public decimal DeliveryCost { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public DeliveryStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Customer Customer { get; set; }
    }

    public enum DeliveryMethod
    {
        Standard = 1,    
        Express = 2,     
        SameDay = 3      
    }

    public enum DeliveryStatus
    {
        Pending = 1,
        Processing = 2,
        Shipped = 3,
        Delivered = 4,
        Cancelled = 5
    }
} 