//Autor: Lucas Henrique Russo do Nascimento
using System.Linq;

List<string> Dias = new() { "Seg", "Ter", "Qua", "Qui", "Sex" };
List<string> Enfermeiros = new() { "Ana", "Beto", "Carla", "David", "Emanuel", "Fabiana", "Gabriel", "Gabriel2", "Gabriel3", "Gabriel4", "Gabriel5" };
List<int> Turnos = new() { 1, 2, 3 };
List<int> CargaHoraria = new() { 40, 40, 40, 40, 40, 40, 40 };
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
    return Math.Min(1, Math.Pow(Math.Exp(1), -(Math.Abs(maximize - z)) / T));
    //return Math.Pow(Math.Exp(1), p);
}
double fRand(double fMin, double fMax)
{
    double f = (double)new Random().Next() / (double)int.MaxValue;
    return fMin + f * (fMax - fMin);
}
double Factivel(int[,,] X, double z)
{
    //int horasTrabalhadas = 0;

    //foreach (var e in Enfermeiros)  
    //{
    //    foreach (var d in Dias)
    //    {
    //        foreach (var t in Turnos)
    //        {
    //            horasTrabalhadas += X[Enfermeiros.IndexOf(e), Dias.IndexOf(d), Turnos.IndexOf(t)]*qtdHoras[Turnos.IndexOf(t)];
    //        }
    //    }
    //    if (horasTrabalhadas != CargaHoraria[Enfermeiros.IndexOf(e)]) return "CH " + e;
    //    horasTrabalhadas = 0;
    //}
    int alocados = 0;
    foreach (var d in Dias)
    {
        foreach (var t in Turnos)
        {
            foreach (var e in Enfermeiros)
            {
                alocados += X[Enfermeiros.IndexOf(e), Dias.IndexOf(d), Turnos.IndexOf(t)];
            }
            if (alocados < dMin[Dias.IndexOf(d), Turnos.IndexOf(t)] || alocados > dMax[Dias.IndexOf(d), Turnos.IndexOf(t)])
            {
                z -= 10;
                //z *= 0.1;
                //return "Demanda";
            }
            alocados = 0;
        }
    }
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
var random = new Random();
for (double porcentagem = 0.5; porcentagem < 5; porcentagem += 0.5)
{
    //X = new int[,,]{
    //    {{1,0,1}, {1,0,1}, {1,1,0}, {1,1,1}, {1,1,1}},
    //    {{0,0,1}, {0,0,1}, {1,1,1}, {0,0,1}, {1,0,1}},
    //    {{1,0,0}, {1,0,0}, {0,1,1}, {0,0,1}, {0,1,1}},
    //    {{1,0,1}, {1,0,1}, {0,0,1}, {0,1,1}, {1,1,0}},
    //    {{1,1,1}, {0,1,1}, {1,0,1}, {0,0,1}, {1,0,1}},
    //    {{1,1,1}, {0,1,1}, {0,0,1}, {1,1,1}, {0,0,1}}, //fabiana
    //    {{1,1,0}, {1,1,1}, {0,0,0}, {0,0,0}, {0,0,0}}, //gabriel
    //    {{0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}},
    //    {{0,0,0}, {0,0,0}, {0,0,0}, {1,0,0}, {0,0,0}},
    //    {{0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}},
    //    {{0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}},
    //};
    foreach (var enf in Enfermeiros)
    {
        foreach (var dia in Dias)
        {
            foreach (var turno in Turnos)
            {
                X[Enfermeiros.IndexOf(enf), Dias.IndexOf(dia), Turnos.IndexOf(turno)] = random.Next(2);
            }
        }
    }

    //MostraX(X);
    //int[,,] XN = (int[,,])X.Clone();
    int[,,] XM = (int[,,])X.Clone();
    int[,,] XBOF = (int[,,])X.Clone();

    double z = Factivel(X, func(Custo, X));
    Console.Write("z inicial = " + z);
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

            z = Factivel(X, func(Custo, X));

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


            Console.WriteLine(
                " Z = " + Math.Round(z, 6) +
                "\t\t\t max = " + Math.Round(maximize, 6) +
                "\t\t\t T = " + Math.Round(T, 6) +
                "\t\t\t BOF = " + Math.Round(BOF, 6) +
                //"\t\t\t ac = " + Math.Round(ac, 6) +
                "\t\t\t ac = " + aceito);
        }
        T *= a;
        //Task.Delay(100).Wait();
    }
    Console.WriteLine(
        "\tmax: " + Math.Round(maximize, 3) +
        "\t\tBOF = " + Math.Round(BOF, 3) + " " +
        "\tporcentagem = " + porcentagem + " " +
        "\talocacoes trocadas = " + alocacoesTrocadas);
    //MostraX(XM);
    //Console.ReadLine();
}

return 0;