using System;
using System.Collections.Generic;

namespace ClinicaPet.Models;

public partial class Pet
{
    public int Codigo { get; set; }

    public string Nome { get; set; } = null!;

    public string Raca { get; set; } = null!;

    public int CodigoTutor { get; set; }

    public virtual ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();

    public virtual Tutor? CodigoTutorNavigation { get; set; }
}
