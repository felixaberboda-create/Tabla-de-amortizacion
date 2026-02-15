using System;
using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        // ================================
        // ENTRADA DE DATOS
        // ================================
        decimal monto = AnsiConsole.Ask<decimal>("Introduce el monto del préstamo:");
        decimal tasaAnual = AnsiConsole.Ask<decimal>("Introduce la tasa de interés anual:");
        int meses = AnsiConsole.Ask<int>("Introduce el plazo del préstamo en meses:");

        // ================================
        // CALCULO DE INTERES MENSUAL
        // ================================
        decimal tasaMensual = (tasaAnual / 12) / 100;

        // ================================
        // FORMULA CUOTA FIJA
        // C = M * [ i(1+i)^n ] / [ (1+i)^n -1 ]
        // ================================
        decimal potencia = (decimal)Math.Pow((double)(1 + tasaMensual), meses);

        decimal cuota = monto * ((tasaMensual * potencia) / (potencia - 1));
        cuota = Math.Round(cuota, 2);

        decimal saldo = monto;

        // ================================
        // CREACION DE TABLA
        // ================================
        var tabla = new Table();

        tabla.AddColumn("No. de cuota");
        tabla.AddColumn("Pago de cuota");
        tabla.AddColumn("Interés");
        tabla.AddColumn("Abono a capital");
        tabla.AddColumn("Saldo");

        // ================================
        // CICLO FOR
        // ================================
        for (int i = 1; i <= meses; i++)
        {
            decimal interes = Math.Round(saldo * tasaMensual, 2);
            decimal abonoCapital = Math.Round(cuota - interes, 2);
            saldo = Math.Round(saldo - abonoCapital, 2);

            tabla.AddRow(
                i.ToString(),
                cuota.ToString("F2"),
                interes.ToString("F2"),
                abonoCapital.ToString("F2"),
                saldo.ToString("F2")
            );
        }

        // ================================
        // MOSTRAR TABLA
        // ================================
        AnsiConsole.Write(tabla);
    }
}
Console.WriteLine("\Presione cualquier tecla para salir...");
Console.ReadKey();
