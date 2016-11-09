using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileCFDIMonitor
{
    [Table("Bdosp")]
    public class BdospModel
    {
        [Key]
        [Column("Consecutivo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BdospId { get; set; }

        [Column("IDFolio")]
        public int FolioId { get; set; }

        [Column("Literal")]
        public string Literal { get; set; }

        [Column("FechayHora")]
        public DateTime Fecha { get; set; }

        [Column("FolioFiscal")]
        public string FolioFiscal { get; set; }
    }
}
