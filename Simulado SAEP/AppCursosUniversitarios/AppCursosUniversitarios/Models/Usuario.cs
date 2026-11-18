using System;
using System.Collections.Generic;

namespace AppCursosUniversitarios.Models;

public partial class Usuario
{
    public int Codigo { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Perfil { get; set; } = null!;
}
