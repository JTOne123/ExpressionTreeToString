﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ExpressionTreeToString;

namespace _tests {
    class Program {
        static void Main(string[] args) {
            Expression<Func<Person, bool>> expr = p => p.DOB.DayOfWeek == DayOfWeek.Tuesday;

            //Console.WriteLine(expr.ToString("C#"));


            //var source = new List<Entidade>();

            //Expression<Action> expr = () => source.Select<Entidade, Resultado>(
            //    s =>
            //    new Resultado {
            //        Detalhes =
            //                new List<DetalheResultado>(
            //                s.Detalhes.Select<Detalhe, DetalheResultado>(
            //                    t => new DetalheResultado { Id = t.Id, Valor = t.Valor }))
            //    });

            //Console.WriteLine(expr.ToString("Factory methods"));


            //Expression<Func<string, bool>> equal = s => s == "test";
            //LambdaExpression lambda = Expression.Lambda(equal.Body, Expression.Parameter(typeof(string), "s"));
            //Console.WriteLine(equal.ToString("Factory methods"));


string s = expr.ToString("C#", out Dictionary<string, (int start, int length)> pathSpans);
const int firstColumnAlignment = -45;
Console.WriteLine($"{"Path",firstColumnAlignment}Substring");
Console.WriteLine(new string('-', 85));
foreach (var kvp in pathSpans) {
    var path = kvp.Key;
    var (start, length) = kvp.Value;
    Console.WriteLine(
        $"{path,firstColumnAlignment}{new string(' ', start)}{s.Substring(start, length)}"
    );
}



            Console.WriteLine("Hello World!");
        }
    }

    internal class Detalhe {
        public object Id { get; internal set; }
        public object Valor { get; internal set; }
    }

    internal class DetalheResultado {
        public object Id { get; internal set; }
        public object Valor { get; internal set; }
    }

    internal class Resultado {
        public List<DetalheResultado> Detalhes { get; internal set; }
    }

    internal class Entidade {
        public IEnumerable<Detalhe> Detalhes { get; internal set; }
    }

    class Person {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime DOB { get; set; }
    }

}
