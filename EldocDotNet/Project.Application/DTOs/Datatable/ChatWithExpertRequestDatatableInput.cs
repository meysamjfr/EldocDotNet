using Project.Application.DTOs.Datatable.Base;

namespace Project.Application.DTOs.Datatable
{
    public class ChatWithExpertRequestDatatableInput : DatatableInput
    {
        public int? ExpertId { get; set; }
    }
}