using System;
using System.Collections.Generic;

namespace Hotel_Web_API.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string Name { get; set; } = null!;

    public string? Category { get; set; }

    public decimal? Price { get; set; }

    public int RoomNr { get; set; }
}
