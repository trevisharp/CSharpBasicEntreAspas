using System;
using System.Collections.Generic;
using System.IO;

void ChameNVezesExemplo()
{
    ChameNVezes(Console.WriteLine, 10);

    ChameNVezes(meuPrint, 2);

    MeuDelegate func = delegate(string s)
    {
        Console.WriteLine("Função Anonima diz:" + s);
    };
    ChameNVezes(func, 5);

    MeuDelegate func2 = s => Console.WriteLine("Lambda diz:" + s);
    ChameNVezes(func2, 3);

    ChameNVezes(s => Console.WriteLine("Lambda direto diz:" + s), 3);

    void meuPrint(string s)
    {
        Console.WriteLine("Mensagem: " + s);
    }
    void ChameNVezes(MeuDelegate f, int n)
    {
        for (int i = 0; i < n; i++)
            f("Olá Mundo");
    }  
}

var array = new int[] { 1, 4, 9, 16, 25 };
var list = new List<int>() { 1, 4, 9, 16, 25 };

var arr1 = array.Select(n => 2 * n + 1);
var arr2 = list.Select(n => n * n);
var arr3 = array.Select(n => (int)Math.Sqrt(n));

var pessoas = new List<Pessoa>()
{
    new Pessoa()
    {
        Nome = "Edjalma",
        Idade = 830
    },
    new Pessoa()
    {
        Nome = "Eu",
        Idade = 23
    },
    new Pessoa()
    {
        Nome = "Voces",
        Idade = 16
    }
};
var idades = pessoas
    .Select(p => p.Idade)
    .Select(i => i * i);

int[] Transforme(int[] entrada, Transformador<int> t)
{
    int[] saida = new int[entrada.Length];
    for (int i = 0; i < saida.Length; i++)
        saida[i] = t(entrada[i]);
    return saida;
}

public delegate void MeuDelegate(string x);
public delegate T Transformador<T>(T n);
public delegate R Transformador2<T, R>(T n);

public class Pessoa
{
    public string Nome { get; set; }
    public int Idade { get; set; }
}

// Implementação com Classes

// ChameNVezesSoQueComClasse(new MeuPrint(), 2);

// void ChameNVezesSoQueComClasse(MeuDelegateSoQueComClasse f, int n)
// {
//     for (int i = 0; i < n; i++)
//         f.Run("Olá Mundo");
// }

// public abstract class MeuDelegateSoQueComClasse
// {
//     public abstract void Run(string s);
// }

// public class MeuPrint : MeuDelegateSoQueComClasse
// {
//     public override void Run(string s)
//     {
//         Console.WriteLine("Mensagem:" + s);
//     }
// }

public static class MyExtensionMethods
{
    public static IEnumerable<R> Select<T, R>(
        this IEnumerable<T> entrada, 
        Func<T, R> func)
    {
        var it = entrada.GetEnumerator();
        while (it.MoveNext())
            yield return func(it.Current);
    }

    public static IEnumerable<T> Skip<T>(this IEnumerable<T> coll, int N)
    {
        var it = coll.GetEnumerator();
        for (int i = 0; i < N && it.MoveNext(); i++) ;
        
        while (it.MoveNext())
            yield return it.Current;
    }

    public static IEnumerable<T> Take<T>(this IEnumerable<T> coll, int N)
    {
        var it = coll.GetEnumerator();
        for (int i = 0; i < N && it.MoveNext(); i++)
            yield return it.Current;
    }

    public static List<T> ToList<T>(this IEnumerable<T> coll)
    {
        List<T> list = new List<T>();
        
        var it = coll.GetEnumerator();
        while (it.MoveNext())
            list.Add(it.Current);
        
        return list;
    }

    public static int Count<T>(this IEnumerable<T> coll)
    {
        int count = 0;
        var it = coll.GetEnumerator();

        while (it.MoveNext())
            count++;
        
        return count;
    }

    public static T LastOrDefault<T>(this IEnumerable<T> coll)
    {
        int count = 0;
        var it = coll.GetEnumerator();
        while (it.MoveNext())
            count++;

        return count == 0 ? default(T) : it.Current;
    }

    public static IEnumerable<T> Append<T>(this IEnumerable<T> coll, T item)
    {
        var it = coll.GetEnumerator();
        while (it.MoveNext())
            yield return it.Current;
        
        yield return item;
    }

    public static IEnumerable<T> Prepend<T>(this IEnumerable<T> coll, T item)
    {
        yield return item;

        foreach (var x in coll)
            yield return x;
    }

    public static T FirstOrDefault<T>(this IEnumerable<T> coll)
    {
        var it = coll.GetEnumerator();
        return it.MoveNext() ? it.Current : default(T);
    }

    public static T[] ToArray<T>(this IEnumerable<T> coll)
    {
        T[] arr = new T[coll.Count()];

        var it = coll.GetEnumerator();
        for (int i = 0; it.MoveNext(); i++)
            arr[i] = it.Current;

        return arr;
    }

    public static IEnumerable<string> Open(this string file)
    {
        var stream = new StreamReader(file);

        while (!stream.EndOfStream)
        {
            var line = stream.ReadLine();
            yield return line;
        }

        stream.Close();
    }

    public static IEnumerable<string[]> Split(this IEnumerable<string> coll)
    {
        foreach (var el in coll)
        {
            yield return el.Split(';');
        }
    }
}