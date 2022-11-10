using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var query = read()
    .Where(c => c.IsCovid);

foreach (var x in query)
{
    Console.WriteLine(x.IsCovid);
}

IEnumerable<CasoCovid> read()
{
    StreamReader reader = new StreamReader("SRAG2021.csv");

    var firstLine = reader.ReadLine();
    var header = firstLine.Split(';').ToList();
    
    int classfin = header.IndexOf("\"CLASSI_FIN\"");

    while (!reader.EndOfStream)
    {
        var line = reader.ReadLine();
        var data = line.Split(';');

        var caso = new CasoCovid();
        caso.IsCovid = data[classfin] == "5";

        yield return caso;
    }

    reader.Close();
}

public class CasoCovid
{
    public bool IsCovid { get; set; }
}