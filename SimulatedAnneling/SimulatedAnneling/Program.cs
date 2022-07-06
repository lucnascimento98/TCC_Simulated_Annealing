//Autor: Lucas Henrique Russo do Nascimento
using Bogus;
using SimulatedAnneling.InstanceGenerator;
using SimulatedAnneling.InstanceGenerator.Domain;
using System.Text.Json;

var physicians = new Faker<Physician>()
    .RuleFor(p => p.Name, f => f.Name.FirstName())
    .RuleFor(p => p.Id, f => f.IndexFaker)
    .RuleFor(p => p.Hours, f => 30 * 8);//.Random.ListItem(new List<int>() {40,32,48}));

var shifts = new Faker<ShiftsPattern>()
    .RuleFor(x => x.Id, f => f.IndexFaker)
    .RuleFor(x => x.Hours, f => 8)
    .RuleFor(x => x.DMin, f => 2)
    .RuleFor(x => x.DMax, f => f.Random.Int(4, 10));

var period = new Faker<Period>()
    .RuleFor(x => x.Year, f => f.Random.Int(2000, 2022))
    .RuleFor(x => x.Month, f => f.Random.Int(1, 12))
    .RuleFor(x => x.StartDay, f => 1)
    .RuleFor(x => x.EndDay, f => 30);

var instance = new Faker<Instance>()
    .RuleFor(x => x.Period, f => period.Generate())
    .RuleFor(x => x.Physician, f => physicians.Generate(10).ToList())
    .RuleFor(x => x.Shifts, f => shifts.Generate(3).ToList());

string json = JsonSerializer.Serialize(instance.Generate());
Instance instancia = JsonSerializer.Deserialize<Instance>(json);

List<int> Dias = new();

for (int i = instancia.Period.StartDay; i <= instancia.Period.EndDay; i++)
{
    Dias.Add(i);
}
List<string> Enfermeiros = instancia.Physician.Select(x => x.Name).ToList();
List<int> Turnos = instancia.Shifts.Select(x => x.Id).ToList();
List<int> CargaHoraria = instancia.Physician.Select(x => x.Hours).ToList();
List<int> qtdHoras = instancia.Shifts.Select(x => x.Hours).ToList();
int[,] dMin = new int[Dias.Count, Turnos.Count];
int[,] dMax = new int[Dias.Count, Turnos.Count];
for (int i = 0; i < Dias.Count; i++)
{
    for (int j = 0; j < Turnos.Count; j++)
    {
        dMin[i,j] = instancia.Shifts.Where(x => x.Id == j).First().DMin;
        dMax[i,j] = instancia.Shifts.Where(x => x.Id == j).First().DMax;
    }
}
Console.WriteLine($"enf = {Enfermeiros.Count}, dias = {Dias.Count} \n" +
    $"dmin = {{ {dMin[0,0]},{dMin[0, 1]},{dMin[0,2]}}} \n" +
    $"dmax = {{ {dMax[0, 0]},{dMax[0, 1]},{dMax[0, 2]}}}");
//return 0;


#region Declaração dos dados
//List<string> Dias = new() { "Seg", "Ter", "Qua", "Qui", "Sex" };
//List<string> Enfermeiros = new() { "Ana", "Beto", "Carla", "David", "Emanuel", "Fabiana", "Gabriel", "Gabriel2", "Gabriel3", "Gabriel4", "Gabriel5" };
//List<int> Turnos = new() { 1, 2, 3 };
//List<int> CargaHoraria = new() { 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40 };
//List<int> qtdHoras = new() { 8, 8, 8 };


//int[,] dMin = new int[,] {
//    {1, 1, 1},
//    {1, 1, 1},
//    {1, 1, 1},
//    {1, 1, 1},
//    {1, 1, 1}
//};

//int[,] dMax = new int[,] {
//    {5, 3, 6},
//    {5, 3, 6},
//    {5, 3, 6},
//    {5, 3, 6},
//    {5, 3, 6}
//};


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

int[,,] X = new int[Enfermeiros.Count, Dias.Count, Turnos.Count];

#endregion 
#region Calculo da funcao
double func(int[,,] Custo, int[,,] X) //calculo da funcao objetivo
{
    int sum = 0;
    foreach (var e in Enfermeiros)
    {
        foreach (var d in Dias)
        {
            foreach (var t in Turnos)
            {
                //sum += Custo[Enfermeiros.IndexOf(e), Dias.IndexOf(d), Turnos.IndexOf(t)] * X[Enfermeiros.IndexOf(e), Dias.IndexOf(d), Turnos.IndexOf(t)];
                sum += X[Enfermeiros.IndexOf(e), Dias.IndexOf(d), Turnos.IndexOf(t)];
            }
        }
    }
    return sum;
}
#endregion
#region Aceitação
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
#endregion
#region Factibilidade
double Factivel(int[,,] X, double z, ref int violacoes)
{
    int sum = 0;
    int penalizacao = 100000;
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
        if (sum <= CargaHoraria[Enfermeiros.IndexOf(e)])
        {
            z -= penalizacao;
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
                z -= penalizacao;
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
                z -= penalizacao;
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
                z -= penalizacao;
                violacoes++;
                //Console.WriteLine($"turnos subsequentes {e} {d}");

            }
            sum = 0;
        }
    }
    #endregion
    return z;
}
#endregion
#region Mostrar Array
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
#endregion
#region Implementação da heuristica, começando pelo calculo da porcentagem de busca para cada iteração
for (double porcentagem = 0.2; porcentagem < 2; porcentagem += 0.2)
{
    #region Solução inicial melhorada
    //X = new int[,,]{
    //    {{1,1,1}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}},
    //    {{1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}},
    //    {{1,0,0}, {1,0,1}, {1,0,0}, {1,0,0}, {1,0,0}},
    //    {{1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}},
    //    {{1,0,0}, {1,0,0}, {1,0,0}, {1,1,0}, {1,0,0}},
    //    {{0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}}, //fabiana
    //    {{0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}}, //gabriel
    //    {{0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}},
    //    {{0,1,1}, {0,0,0}, {0,0,1}, {0,0,1}, {0,0,1}},
    //    {{0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}},
    //    {{0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {1,0,1}},
    //};
    #endregion
    #region Solução inicial aleatoria
    foreach (var enf in Enfermeiros)
    {
        foreach (var dia in Dias)
        {
            foreach (var turno in Turnos)
            {
                X[Enfermeiros.IndexOf(enf), Dias.IndexOf(dia), Turnos.IndexOf(turno)] = new Random().Next(2);
            }
        }
    }
    #endregion
    #region Declaração das variaveis
    //MostraX(X);
    int[,,] XM = (int[,,])X.Clone();                                            //X do maximo local
    int[,,] XBOF = (int[,,])X.Clone();                                          //X do BOF
    
    int violacoes = 0;                                                          //Contador de violações para cada X encontrado, utilizado para debugar

    double z = Factivel(X, func(Custo, X), ref violacoes);                      //Calculo do primeiro valor de Z
    Console.Write($"z inicial = {z} \t violacoes = {violacoes}\t------" );      

    double maximize = z;                                                        //Maximo local
    double BOF = z;                                                             //Best Of Function

    double tI = 100000;                                                         //temperatura inicial
    double tF = 0.000001;                                                       //temperatura final
    double T = tI;                                                              //temperatura atual
    double a = 0.99;                                                            //taxa de resfriamento

    bool aceito;                                                                //indicador de aceitação, utilizado para debugar
    double alocacoesTrocadas = Math.Round(Enfermeiros.Count * Dias.Count * Turnos.Count * porcentagem / 100); // calculo da quantidade de trocas baseado na porcentagem 
    #endregion
    #region Implementação da heuristica
    while (T > tF) // Enquanto a temperatura atual for maior que a temperadura final
    {
        int i = 1;
        //XM = (int[,,])XBOF.Clone(); //fiquei em duvida se isso deveria estar aqui ou nao
        while (i <= 50) // 50 iterações por tenperatura
        {
            for (int aux1 = 0; aux1 < alocacoesTrocadas; aux1++) //busca aleatoria na vizinhança
            {
                var dia = new Random().Next(Dias.Count);
                var turno = new Random().Next(Turnos.Count);
                var enf = new Random().Next(Enfermeiros.Count);
                X[enf, dia, turno] = 1 - XM[enf, dia, turno];
            }

            z = Factivel(X, func(Custo, X), ref violacoes); // calculo do valor de 

            if (z > maximize) //Se o valor da solução atual for melhor que o maximo local
            {
                aceito = true;
                maximize = z;
                XM = (int[,,])X.Clone();
                if (z > BOF) //Se o valor da solução atual for melhor que o maximo global encontrado
                {
                    BOF = z;
                    XBOF = (int[,,])X.Clone();
                }
            }
            else //Se o valor atual nao for melhor que o maximo local, calcular o indice de aceitação baseado na temperatura e na variação entre as soluções
            {
                double ac = accept(z, maximize, T);
                double fr = fRand(0, 1);
                if (ac > fr) //verificação da aceitação
                {
                    aceito = true;
                    maximize = z;
                    XM = (int[,,])X.Clone();
                }
                else //Se não foi aceito
                {
                    aceito = false;
                    X = ((int[,,])XM.Clone());
                    XM = (int[,,])XBOF.Clone();
                }
            }

            i++; //incrementa iteração

            #region primeira tentativa de implementação da heuristica
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
            #endregion


            //Console.WriteLine( //print para debugar a cada iteração
            //    " Z = " + Math.Round(z, 6) +
            //    "\t\t\t max = " + Math.Round(maximize, 6) +
            //    "\t\t\t T = " + Math.Round(T, 6) +
            //    "\t\t\t BOF = " + Math.Round(BOF, 6) +
            //    "\t\t\t violações = " + violacoes +
            //    //"\t\t\t ac = " + Math.Round(ac, 6) +
            //    "\t\t\t ac = " + aceito);
        }
        T *= a;
        //Task.Delay(100).Wait();
    }
    #endregion
    #region Fim da heuristica
    Factivel(XBOF, func(Custo, XBOF), ref violacoes);
    Console.WriteLine( //print para mostrar o fim da heuristica
        "\tmax: " + Math.Round(maximize, 3) +
        "\tBOF = " + Math.Round(BOF, 3) + " " +
        "\tporcentagem = " + porcentagem + " " +
        "\talocacoes trocadas = " + alocacoesTrocadas +
        "\tviolacoes = " + violacoes);
    Console.WriteLine();
    //MostraX(XBOF); //Mostra a melhor solução encontrada
    #endregion
    //Console.ReadLine();
}
#endregion
return 0;