using System;
using System.Collections.Generic;

namespace Hotel_Web_API.Models;

public partial class Person
{
    public int RoomId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public DateTime CreateDate { get; set; }
}
