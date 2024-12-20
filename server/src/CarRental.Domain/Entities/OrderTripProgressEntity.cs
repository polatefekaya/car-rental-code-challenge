using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarRental.Domain.Entities.Base;

namespace CarRental.Domain.Entities;

[Table("order_trip_progresses", Schema = "order")]
public class OrderTripProgressEntity : BaseEntity
{
    public int ProgressId {get; set;}
    public bool WaitingPickUp {get; set;}
    public bool Authenticated {get; set;}
    public bool Approved {get; set;}
    public bool InTrip {get; set;}
    public bool Returned {get; set;}

    public OrderProgressEntity Progress {get; set;} = null!;
}
