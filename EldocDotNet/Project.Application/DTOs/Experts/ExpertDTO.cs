using Project.Application.DTOs.Base;
using Project.Domain.Enums;
using System;

namespace Project.Application.DTOs.Expert
{
    public class ExpertDTO : BaseDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Specialty { get; set; }
        public string Description { get; set; }
        public double SessionFee { get; set; }
        public int MaxActiveSessions { get; set; }
    }
}
