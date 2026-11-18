using System;
using System.Collections.Generic;

namespace AppCursosUniversitarios.Models;

public partial class Aluno
{
    public int Codigo { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public virtual ICollection<EscolhaCurso> EscolhaCursos { get; set; } = new List<EscolhaCurso>();
}
