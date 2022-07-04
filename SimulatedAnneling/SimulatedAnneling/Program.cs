//Autor: Lucas Henrique Russo do Nascimento
using System.Linq;

List<string> Dias = new() { "Seg", "Ter", "Qua", "Qui", "Sex" };
List<string> Enfermeiros = new() { "Ana", "Beto", "Carla", "David", "Emanuel", "Fabiana", "Gabriel", "Gabriel2", "Gabriel3", "Gabriel4", "Gabriel5" };
List<int> Turnos = new() { 1, 2, 3 };
List<int> CargaHoraria = new() { 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40 };
List<int> qtdHoras = new() { 8, 8, 8 };

int[,,] X = new int[Enfermeiros.Count, Dias.Count, Turnos.Count];

int[,] dMin = new int[,] {
    {1, 1, 1},
    {1, 1, 1},
    {1, 1, 1},
    {1, 1, 1},
    {1, 1, 1}
};

int[,] dMax = new int[,] {
    {5, 3, 6},
    {5, 3, 6},
    {5, 3, 6},
    {5, 3, 6},
    {5, 3, 6}
};

//var dMax = new int[][] {
//  new int[]  {5, 3, 6},
//  new int[]  {5, 3, 6},
//  new int[]  {5, 3, 6},
//  new int[]  {5, 3, 6},
//  new int[]  {5, 3, 6}
//};

//dMax.Select

int[,,] Custo = new int[,,]
{
    {{1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}},
    {{1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}},
    {{1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}},
    {{1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}},
    {{1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}},
    {{1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}},
    {{1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}},
    {{1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}},
    {{1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}},
    {{1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}},
    {{1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}, {1,1,1}}
};

double func(int[,,] Custo, int[,,] X) //calculo da funcao objetivo
{
    int sum = 0;
    foreach (var e in Enfermeiros)
    {
        foreach (var d in Dias)
        {
            foreach (var t in Turnos)
            {
                sum += Custo[Enfermeiros.IndexOf(e), Dias.IndexOf(d), Turnos.IndexOf(t)] * X[Enfermeiros.IndexOf(e), Dias.IndexOf(d), Turnos.IndexOf(t)];
            }
        }
    }
    return sum;
}
double accept(double z, double maximize, double T)
{
    return Math.Min(1, Math.Pow(Math.Exp(1), -(maximize - z) / T));
    //return Math.Pow(Math.Exp(1), p);
}
double fRand(double fMin, double fMax)
{
    double f = (double)new Random().Next() / (double)int.MaxValue;
    return fMin + f * (fMax - fMin);
}
double Factivel(int[,,] X, double z, ref int violacoes)
{
    int sum = 0;
    violacoes = 0;
    #region Carga Horaria
    foreach (var e in Enfermeiros)
    {
        foreach (var d in Dias)
        {
            foreach (var t in Turnos)
            {
                sum += X[Enfermeiros.IndexOf(e), Dias.IndexOf(d), Turnos.IndexOf(t)] * qtdHoras[Turnos.IndexOf(t)];
            }
        }
        if (sum != CargaHoraria[Enfermeiros.IndexOf(e)])
        {
            z -= 10;
            violacoes++;
            //Console.WriteLine("CH");
        }
        sum = 0;
    }
    #endregion
    #region Demanda minima e maxima
    sum = 0;
    foreach (var d in Dias)
    {
        foreach (var t in Turnos)
        {
            foreach (var e in Enfermeiros)
            {
                sum += X[Enfermeiros.IndexOf(e), Dias.IndexOf(d), Turnos.IndexOf(t)];
            }
            if (sum < dMin[Dias.IndexOf(d), Turnos.IndexOf(t)] || sum > dMax[Dias.IndexOf(d), Turnos.IndexOf(t)])
            {
                z -= 10;
                //z *= 0.1;
                //return "Demanda";
                violacoes++;
                //Console.WriteLine("Demanda");

            }
            sum = 0;
        }
    }
    #endregion
    #region Apenas 1 turno por dia
    sum = 0;
    for (int e = 0; e < Enfermeiros.Count; e++)
    {
        for (int d = 0; d < Dias.Count; d++)
        {
            for (int t = 0; t < Turnos.Count; t++)
            {
                sum += X[e,d,t];
            }
            if (sum > 1)
            {
                z -= 10;
                violacoes++;
                //Console.WriteLine("apenas 1 turno");

            }
            sum = 0;
        }
    }
    #endregion
    #region Proibiição de turnos subsequentes
    sum = 0;
    for (int e = 0; e < Enfermeiros.Count; e++)
    {
        for (int d = 0; d < Dias.Count-1; d++)
        {
            sum += X[e, d, 2]
            + X[e, d + 1, 0] 
            + X[e, d + 1, 1];
            if (sum > 1)
            {
                z -= 10;
                violacoes++;
                //Console.WriteLine($"turnos subsequentes {e} {d}");

            }
            sum = 0;
        }
    }
    #endregion
    return z;
}

void MostraX(int[,,] X)
{
    foreach (var enf in Enfermeiros)
    {
        foreach (var dia in Dias)
        {
            foreach (var turno in Turnos)
            {
                Console.Write(X[Enfermeiros.IndexOf(enf), Dias.IndexOf(dia), Turnos.IndexOf(turno)] + " ");
            }
            Console.Write("\t");
        }
        Console.WriteLine();
    }
}

for (double porcentagem = 0.5; porcentagem < 5; porcentagem += 0.5)
{
    X = new int[,,]{
        {{1,1,1}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}},
        {{1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}},
        {{1,0,0}, {1,0,1}, {1,0,0}, {1,0,0}, {1,0,0}},
        {{1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}},
        {{1,0,0}, {1,0,0}, {1,0,0}, {1,1,0}, {1,0,0}},
        {{0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}}, //fabiana
        {{0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}}, //gabriel
        {{0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}},
        {{0,1,1}, {0,0,0}, {0,0,1}, {0,0,1}, {0,0,1}},
        {{0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}},
        {{0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {1,0,1}},
    };
    //foreach (var enf in Enfermeiros)
    //{
    //    foreach (var dia in Dias)
    //    {
    //        foreach (var turno in Turnos)
    //        {
    //            X[Enfermeiros.IndexOf(enf), Dias.IndexOf(dia), Turnos.IndexOf(turno)] = new Random().Next(2);
    //        }
    //    }
    //}

    //MostraX(X);
    int[,,] XM = (int[,,])X.Clone();
    int[,,] XBOF = (int[,,])X.Clone();
    
    int violacoes = 0;

    double z = Factivel(X, func(Custo, X), ref violacoes);
    Console.Write($"z inicial = {z} \t violacoes = {violacoes}\t------" );

    double maximize = z;
    double BOF = z;

    double tI = 100000;                             //temperatura inicial       //100000;
    double tF = 0.000001;                           //temperatura final
    double T = tI;                                  //temperatura atual
    double a = 0.99;                                //taxa de resfriamento

    //double d = (1.6 * (Math.Pow(10, -23)));       //
    //int countRecusados = 0;
    bool aceito;
    double alocacoesTrocadas = Math.Round(Enfermeiros.Count * Dias.Count * Turnos.Count * porcentagem / 100);
    while (T > tF)
    {
        int i = 1;
        //XM = (int[,,])XBOF.Clone();
        while (i <= 50)
        {
            for (int aux1 = 0; aux1 < alocacoesTrocadas; aux1++)
            {
                var dia = new Random().Next(Dias.Count);
                var turno = new Random().Next(Turnos.Count);
                var enf = new Random().Next(Enfermeiros.Count);
                X[enf, dia, turno] = 1 - XM[enf, dia, turno];
            }

            z = Factivel(X, func(Custo, X), ref violacoes);

            if (z > maximize)
            {
                aceito = true;
                maximize = z;
                XM = (int[,,])X.Clone();
                if (z > BOF)
                {
                    BOF = z;
                    XBOF = (int[,,])X.Clone();
                }
            }
            else
            {
                double ac = accept(z, maximize, T);
                double fr = fRand(0, 1);
                if (ac > fr)
                {
                    aceito = true;
                    maximize = z;
                    XM = (int[,,])X.Clone();
                }
                else
                {
                    aceito = false;
                    X = ((int[,,])XM.Clone());
                    XM = (int[,,])XBOF.Clone();
                }
            }

            i++;

            //if (z > maximize || ac > fr)
            //{
            //    aceito = true;
            //    maximize = z;
            //    XM = (int[,,]) X.Clone();
            //}
            //else
            //{
            //    aceito = false;
            //    X = ((int[,,]) XM.Clone());
            //}

            //if (z > BOF)
            //{
            //    BOF = z;
            //    XBOF = (int[,,])X.Clone();
            //}

            //if (ac < 0.5)
            //{
            //    countRecusados++;
            //    if (countRecusados > 10)
            //    {
            //        //Console.ReadLine();
            //    }
            //}
            //else
            //{
            //    countRecusados = 0;
            //}


            //Console.WriteLine(
            //    " Z = " + Math.Round(z, 6) +
            //    "\t\t\t max = " + Math.Round(maximize, 6) +
            //    "\t\t\t T = " + Math.Round(T, 6) +
            //    "\t\t\t BOF = " + Math.Round(BOF, 6) +
            //    //"\t\t\t ac = " + Math.Round(ac, 6) +
            //    "\t\t\t ac = " + aceito);
        }
        T *= a;
        //Task.Delay(100).Wait();
    }

    Factivel(XBOF, func(Custo, XBOF), ref violacoes);
    Console.WriteLine(
        "\tmax: " + Math.Round(maximize, 3) +
        "\tBOF = " + Math.Round(BOF, 3) + " " +
        "\tporcentagem = " + porcentagem + " " +
        "\talocacoes trocadas = " + alocacoesTrocadas +
        "\tviolacoes = " + violacoes);
    Console.WriteLine();
    //MostraX(XBOF);

    //Console.ReadLine();
}

return 0;