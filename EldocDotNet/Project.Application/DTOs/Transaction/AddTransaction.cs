﻿using Project.Domain.Enums;

namespace Project.Application.DTOs.Transaction
{
    public class AddTransaction
    {
        public int UserId { get; set; }
        public double Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Description { get; set; }
    }
}