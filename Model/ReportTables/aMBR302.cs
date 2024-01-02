using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Model.ReportTables
{
    public class aMBR302
    {

        public int Id { get; set; }
        public string Branch { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cash_in_Vault_Coins { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cash_out_Vault_Notes { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Teller_Coins { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        
        public decimal Teller_Notes { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Foreign_Coins { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Foreign_Notes { get; set;}
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ATM_Balances { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Others { get; set; }



                  
    }
}
