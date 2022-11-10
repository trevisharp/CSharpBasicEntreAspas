using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var query = read()
    .Where(c => c.IsCovid)
    .GroupBy(c => c.Doses)
    .Select(g => new {
        qtdDoses = g.Key,
        letalidade = g.Average(c => c.IsDead ? 1.0 : 0.0)
    });

foreach (var cl in query)
{
    Console.WriteLine($"Doses: {cl.qtdDoses}, " + 
        $"Letalidade: {cl.letalidade}");
}
// Console.WriteLine(query
//     .Average(c => c.IsDead ? 1.0 : 0.0));

// foreach (var x in query)
// {
//     Console.WriteLine(x);
// }

IEnumerable<CasoCovid> read()
{
    StreamReader reader = new StreamReader("SRAG2021.csv");

    var firstLine = reader.ReadLine();
    var header = firstLine.Split(';').ToList();
    
    int classfin = header.IndexOf("\"CLASSI_FIN\"");
    int evolucao = header.IndexOf("\"EVOLUCAO\"");

    int dose1 = header.IndexOf("\"DOSE_1_COV\"");
    int dose2 = header.IndexOf("\"DOSE_2_COV\"");

    while (!reader.EndOfStream)
    {
        var line = reader.ReadLine();
        var data = line.Split(';');

        var caso = new CasoCovid();
        caso.IsCovid = data[classfin] == "5";
        caso.IsDead = data[evolucao] == "2";

        int doses = 0;
        if (data[dose1] != "\"\"")
            doses++;
        if (data[dose2] != "\"\"")
            doses++;
        caso.Doses = doses;

        yield return caso;
    }

    reader.Close();
}

public class CasoCovid
{
    public bool IsCovid { get; set; }
    public bool IsDead { get; set; }
    public int Doses { get; set; }

    public override string ToString()
        => $"{IsCovid} {IsDead} {Doses}";
}