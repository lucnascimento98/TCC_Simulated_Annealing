//Autor: Lucas Henrique Russo do Nascimento

using Bogus;
using SimulatedAnneling.InstanceGenerator;
using SimulatedAnneling.InstanceGenerator.Domain;
using System.Text.Json;

#region Gerador de instâncias
/*
var physicians = new Faker<Physician>()
    .RuleFor(p => p.Name, f => f.Name.FirstName())
    .RuleFor(p => p.Id, f => f.IndexFaker)
    .RuleFor(p => p.Hours, f => 208);//.Random.ListItem(new List<int>() {40,32,48}));

var shifts = new Faker<ShiftsPattern>()
    .RuleFor(x => x.Id, f => f.IndexFaker)
    .RuleFor(x => x.Hours, f => 8)
    .RuleFor(x => x.DMin, f => f.Random.Int(2,3))
    .RuleFor(x => x.DMax, f => f.Random.Int(6, 10));

var period = new Faker<Period>()
    .RuleFor(x => x.Year, f => f.Random.Int(2000, 2022))
    .RuleFor(x => x.Month, f => f.Random.Int(1, 12))
    .RuleFor(x => x.StartDay, f => 1)
    .RuleFor(x => x.EndDay, f => 30);

var instance = new Faker<Instance>()
    .RuleFor(x => x.Period, f => period.Generate())
    .RuleFor(x => x.Physician, f => physicians.Generate(f.Random.Int(8,12)).ToList())
    .RuleFor(x => x.Shifts, f => shifts.Generate(3).ToList());
string json = "";
for (int i = 0; i < 10; i++)
{
    json = JsonSerializer.Serialize(instance.Generate());
    File.WriteAllText(@$"C:\Lucas\TCC\Heuristicas\git\TCC_Simulated_Annealing\SimulatedAnneling\SimulatedAnneling\Instances\Instancia_{i + 1}.txt", json);
}*/
#endregion

for (var inst = 1; inst <= 10; inst++)
{
    #region Leitura do arquivo da instância
    Instance instancia = JsonSerializer.Deserialize<Instance>(File.ReadAllText(@$"C:\Lucas\TCC\Heuristicas\git\TCC_Simulated_Annealing\SimulatedAnneling\SimulatedAnneling\Instances\Instancia_{inst}.txt"));

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
            dMin[i, j] = instancia.Shifts.Where(x => x.Id % 3 == j).First().DMin;
            dMax[i, j] = instancia.Shifts.Where(x => x.Id % 3 == j).First().DMax;
        }
    }
    Console.WriteLine($"enf = {Enfermeiros.Count}, dias = {Dias.Count} \n" +
        $"dmin = {{ {dMin[0, 0]},{dMin[0, 1]},{dMin[0, 2]}}} \n" +
        $"dmax = {{ {dMax[0, 0]},{dMax[0, 1]},{dMax[0, 2]}}}");
    #endregion

    #region Declaração dos dados estáticos para testes especificos
    //List<string> Dias = new() { "Seg", "Ter", "Qua", "Qui", "Sex" };
    //List<string> Enfermeiros = new() { "A", "B", "C", "D", "E", "F", "G" };
    //List<int> CargaHoraria = new() { 40, 40, 40, 40, 40, 40, 40 };

    /*
    List<int> Dias = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
    List<string> Enfermeiros = new() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
    List<int> CargaHoraria = new() { 240, 240, 240, 240, 240, 240, 240, 240, 240, 240 };

    List<int> Turnos = new() { 1, 2, 3 };
    List<int> qtdHoras = new() { 8, 8, 8 };


    int[,] dMin = new int[,] {
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2},
        {2, 2, 2}
    };

    int[,] dMax = new int[,] {
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6},
        {9, 5, 6}
    };*/

    int[,,] X = new int[Enfermeiros.Count, Dias.Count, Turnos.Count];
    #endregion

    #region Calculo da funcao objetivo
    double func(int[,,] X) //calculo da funcao objetivo
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
    double accept(double z, double minimize, double T)
    {
        return Math.Min(1, Math.Pow(Math.Exp(1), -(z - minimize) / T));
    }
    double fRand(double fMin, double fMax)
    {
        double f = (double)new Random().Next() / (double)int.MaxValue;
        return fMin + f * (fMax - fMin);
    }
    #endregion

    #region Factibilidade
    double Factivel(int[,,] X, double z, ref int[] violacoes)
    {
        int sum = 0;
        int penalizacao = 1000;
        violacoes = violacoes.Select(x => x = 0).ToArray();

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
            if (sum > CargaHoraria[Enfermeiros.IndexOf(e)])
            {
                z += penalizacao;
                violacoes[0]++;
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
                    z += penalizacao;
                    violacoes[1]++;
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
                    sum += X[e, d, t];
                }
                if (sum > 1)
                {
                    z += penalizacao;
                    violacoes[2]++;
                }
                sum = 0;
            }
        }
        #endregion

        #region Proibiição de turnos subsequentes
        sum = 0;
        for (int e = 0; e < Enfermeiros.Count; e++)
        {
            for (int d = 0; d < Dias.Count - 1; d++)
            {
                sum += X[e, d, 2]
                + X[e, d + 1, 0]
                + X[e, d + 1, 1];
                if (sum > 1)
                {
                    z += penalizacao;
                    violacoes[3]++;
                }
                sum = 0;
            }
        }
        #endregion

        #region Proibição de 3 noites seguidas
        sum = 0;
        for (int e = 0; e < Enfermeiros.Count; e++)
        {
            for (int d = 0; d < Dias.Count - 3; d++)
            {
                sum += X[e, d, 2]
                + X[e, d + 1, 2]
                + X[e, d + 2, 2]
                + X[e, d + 3, 2];
                if (sum > 3)
                {
                    z += penalizacao;
                    violacoes[4]++;
                }
                sum = 0;
            }
        }
        #endregion

        return z;
    }
    #endregion

    #region Mostrar matriz de alocações
    void MostraX(int[,,] X)
    {
        foreach (var enf in Enfermeiros)
        {
            foreach (var dia in Dias)
            {
                foreach (var turno in Turnos)
                {
                    Console.Write(X[Enfermeiros.IndexOf(enf), Dias.IndexOf(dia), Turnos.IndexOf(turno)]);
                }
                Console.Write("\t");
            }
            Console.WriteLine();
        }
    }
    #endregion

    #region Implementação da heuristica, começando pelo calculo da porcentagem de busca para cada iteração
    //for (double porcentagem = 0.2; porcentagem < 2; porcentagem += 0.2)
    //{

        #region Solução inicial melhorada, declarada de forma estática para testes específicos
        //X = new int[,,]{ //5 dias
        //    {{1,1,0}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,0}},
        //    {{0,0,1}, {0,0,0}, {0,0,0}, {0,1,0}, {1,0,0}},
        //    {{0,0,0}, {0,1,0}, {1,1,0}, {1,0,0}, {0,1,0}},
        //    {{1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}},
        //    {{1,0,0}, {1,0,0}, {1,0,0}, {0,0,1}, {1,0,1}},
        //    {{0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,0,1}}, 
        //    {{0,0,1}, {0,0,1}, {0,0,1}, {0,0,0}, {0,1,0}}  
        //};

        //X = new int[,,]{ //30 dias
        //    {{0,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,1,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}}, //Ana
        //    {{0,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,1,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}, {1,0,0}}, //Beto
        //    {{0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {1,1,0}, {1,1,0}, {0,1,0}, {0,1,0}, {1,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,0,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}}, //Carla
        //    {{0,1,0}, {0,1,0}, {0,1,0}, {1,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,0,0}, {0,1,1}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}, {0,1,0}}, //David
        //    {{0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {0,1,1}, {0,0,0}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,0}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,0}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,0}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,0}, {0,0,1}, {0,0,1}}, //Emanuel
        //    {{0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {1,0,1}, {0,0,1}, {0,0,0}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,0}, {0,0,1}, {1,0,1}, {0,0,1}, {0,0,0}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,0}, {0,0,1}, {0,0,1}, {0,0,1}, {0,0,0}, {0,0,1}, {0,0,1}}, //Fabiana
        //    {{0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}, {0,1,0}, {0,0,1}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}}, //Gabriel
        //    {{0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}}, //Gabriel2
        //    {{0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}}, //Gabriel3
        //    {{0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,1,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,1}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}}, //Gabriel4

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
        int[,,] XM = (int[,,])X.Clone();                                            //X do maximo local
        int[,,] XBOF = (int[,,])X.Clone();                                          //X do BOF

        int[] violacoes = new int[6];                                               //Contador de violações para cada X encontrado, utilizado para debugar

        double z = Factivel(X, func(X), ref violacoes);                             //Calculo do primeiro valor de Z
        Console.Write(
            //$"z inicial = {z} " +
            "R1, R2 = " + violacoes[1] +
            "\t R3 = " + violacoes[3] +
            "\t R4 = " + violacoes[0] +
            "\t R5 = " + violacoes[4] +
            "\t R6 = " + violacoes[2] + $"\t------");

        double minimize = z;                                                        //Maximo local
        double BOF = z;                                                             //Best Of Function

        double tI = 100000;                                                         //temperatura inicial
        double tF = 0.000001;                                                       //temperatura final
        double T = tI;                                                              //temperatura atual
        double a = 0.99;                                                            //taxa de resfriamento

        bool aceito;                                                                //indicador de aceitação, utilizado para debugar
        double porcentagem = 0.2;
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

                z = Factivel(X, func(X), ref violacoes); // calculo do valor de 

                if (z < minimize) //Se o valor da solução atual for melhor que o minimo local
                {
                    aceito = true;
                    minimize = z;
                    XM = (int[,,])X.Clone();
                    if (z < BOF) //Se o valor da solução atual for melhor que o minimo global encontrado
                    {
                        BOF = z;
                        XBOF = (int[,,])X.Clone();
                    }
                }
                else //Se o valor atual nao for melhor que o minimo local, calcular o indice de aceitação baseado na temperatura e na variação entre as soluções
                {
                    double ac = accept(z, minimize, T);
                    double fr = fRand(0, 1);
                    if (ac > fr) //verificação da aceitação
                    {
                        aceito = true;
                        minimize = z;
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
        }
        #endregion

        #region Apresentacao dos resultados
        Factivel(XBOF, func(XBOF), ref violacoes);

        Console.WriteLine( //print para mostrar o fim da heuristica
                           //"\tmax: " + Math.Round(minimize, 3) +
                           //"\tBOF = " + Math.Round(BOF, 3) + " " +
                           //"\tporc. = " + porcentagem + " " +
                           //"\ta. t. = " + alocacoesTrocadas +
                           //"\tfuncao = "+ funcao +
                           //"\tviolacoes = " + violacoes
            "\t R1,R2 = " + violacoes[1] +
            "\t R3 = " + violacoes[3] +
            "\t R4 = " + violacoes[0] +
            "\t R5 = " + violacoes[4] +
            "\t R6 = " + violacoes[2] + "\n"
            );

        MostraX(XBOF); //Mostra a melhor solução encontrada
        #endregion

    //}
    #endregion
}
return 0;