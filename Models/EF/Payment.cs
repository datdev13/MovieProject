using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EF
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        public int UserID { get; set; }

        public int? MovieID { get; set; }   // 👉 Thêm dòng này: ID phim được thanh toán

        [StringLength(100)]
        public string Package { get; set; }  // 👉 Thêm dòng này (tên gói cước)

        public decimal Amount { get; set; }      // Số tiền thanh toán

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string Status { get; set; }       // Pending, Approved, Rejected

        // Khóa ngoại
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ForeignKey("MovieID")]
        public virtual Movie Movie { get; set; } // 👉 Thêm dòng này: liên kết đến bảng Movie
    }
}
