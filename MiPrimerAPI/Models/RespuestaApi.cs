using System;
using System.Collections.Generic;

namespace MiPrimerAPI.Models;

public partial class RespuestaApi
{
    public int Id { get; set; }

    public int? ResultCount { get; set; }

    public int? PageCount { get; set; }

    public int? PageNbr { get; set; }

    public string? NextPage { get; set; }
}
