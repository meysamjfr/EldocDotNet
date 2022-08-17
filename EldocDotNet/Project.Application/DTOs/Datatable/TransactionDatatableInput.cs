using Project.Application.DTOs.Datatable.Base;

namespace Project.Application.DTOs.Datatable
{
    public class TransactionDatatableInput : DatatableInput
    {
        public int? UserId { get; set; }
    }
}