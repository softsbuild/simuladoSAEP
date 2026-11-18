using System;
using System.Collections.Generic;

namespace AppCursosUniversitarios.Models;

public partial class Curso
{
    public int Codigo { get; set; }

    public string Nome { get; set; } = null!;

    public string Turno { get; set; } = null!;

    public int CargaHoraria { get; set; }

    public int VagasDisponiveis { get; set; }

    public virtual ICollection<EscolhaCurso> EscolhaCursos { get; set; } = new List<EscolhaCurso>();
}
