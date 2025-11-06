using System;

namespace MovieProject.Models
{
    public class Payment
    {
        public int Id { get; set; }           // Mã thanh toán
        public int UserId { get; set; }       // Người dùng thanh toán
        public decimal Amount { get; set; }   // Số tiền
        public DateTime PaymentDate { get; set; }  // Ngày thanh toán
        public bool IsApproved { get; set; }  // Trạng thái duyệt
        public DateTime? ApprovedDate { get; set; } // Ngày được duyệt
        public int DurationMonths { get; set; } // Thời hạn (1, 3, 12 tháng)
    }
}
