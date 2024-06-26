﻿using Booking.Core.Enums;

namespace Booking.WebApi.Models;

public class AddAccommodationModel
{
    public int HotelId { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccommodationType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Numder { get; set; } = 1;
}
