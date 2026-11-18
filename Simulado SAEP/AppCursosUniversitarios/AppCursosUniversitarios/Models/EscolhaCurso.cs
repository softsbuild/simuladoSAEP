using System;
using System.Collections.Generic;

namespace AppCursosUniversitarios.Models;

public partial class EscolhaCurso
{
    public int Codigo { get; set; }

    public int CodigoAluno { get; set; }

    public int CodigoCurso { get; set; }

    public int OrdemPreferencial { get; set; }

    public DateTime DataEscolha { get; set; }

    public virtual Aluno CodigoAlunoNavigation { get; set; } = null!;

    public virtual Curso CodigoCursoNavigation { get; set; } = null!;
}
