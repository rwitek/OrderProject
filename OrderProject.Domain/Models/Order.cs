﻿namespace OrderProject.Domain.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    public DateTime OrderDate { get; set; }
}