
using CinemaAPI.Models;
using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
 

    
    public partial class BILL
    {
        [StringLength(10)]
        public string BillID { get; set; }

        [StringLength(10)]
        public string UserID { get; set; }

        [StringLength(20)]
        public string TicketSession { get; set; }

        [StringLength(20)]
        public string ServiceSession { get; set; }

        [StringLength(10)]
        public string CodeID { get; set; }

    [StringLength(50)]

    public virtual string DatePay { get; set; }
    [Column(TypeName = "date")]
        public virtual DateTime? PayDay { get; set; }

        public virtual DISCOUNT_CODE DISCOUNT_CODE { get; set; }

        public virtual USER_ACCOUNT USER_ACCOUNT { get; set; }

        public virtual List<SERVICE_TO_CASH> ListSVC { get; set; }

        public virtual List<Bill_data> BillData { get; set; }


    public virtual List<TICKET_2> ListTicket { get; set; }
}
