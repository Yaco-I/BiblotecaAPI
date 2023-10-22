﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infrastructure
{
    public class QueryParameters<T>
    {
        public QueryParameters(int pagina, int top)
        {
            Pagina = pagina;
            Top = top;
            Filter = null;
            OrderBy = null;
            OrderByDescending = null;
        }
        public int Pagina { get; set; }
        public int Top { get; set; }
        public Expression<Func<T, bool>> Filter { get; set; }
        public Func<T, object> OrderBy { get; set;}
        public Func<T, object> OrderByDescending { get; set; }
    }
}
