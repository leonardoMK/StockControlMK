﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaryKey.Dominio.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Usuario BuscarPorEmail(string identificacao);
    }
}
