using System;
using System.Collections.Generic;

namespace MiPrimerAPI.Models;

public partial class Result
{
    public int Id { get; set; }

    public string? ItemAlternateCode { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateTs { get; set; }
}
