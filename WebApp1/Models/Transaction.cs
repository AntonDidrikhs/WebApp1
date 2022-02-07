using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        //[DataType(DataType.DateTime)]
        public DateTime TransactionDate = DateTime.Now;
    }
}
