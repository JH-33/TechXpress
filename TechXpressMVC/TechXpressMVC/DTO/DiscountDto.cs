namespace TechXpressMVC.DTO
{
    public class DiscountDto
    {
        public class Discount 
        {
            public int Id { get; set; }
            public string Code { get; set; } // مثال: SAVE10
            public decimal Percentage { get; set; } // مثال: 10
            public bool IsActive { get; set; } = true;
            public DateTime? ExpiryDate { get; set; }
        }
    }
}
