using System;
using System.Collections.Generic;

namespace ClinicaPet.Models;

public partial class Agendamento
{
    public int Codigo { get; set; }

    public string MotivoConsulta { get; set; } = null!;

    public DateTime DateHora { get; set; }

    public int CodigoPets { get; set; }

    public virtual Pet? CodigoPetsNavigation { get; set; } 
}
