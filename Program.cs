// See https://aka.ms/new-console-template for more information
using System;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{

    private static void Main(string[] args)
    {
         decimal total = 0;
         decimal typeint = AskForType();
         if (typeint == 1)
         {
             total = CalculateNumber();
             WriteToConsole(total);
         }
         else
         {
             decimal zaehler1, zaehler2, nenner1, nenner2, tempzaehler = 0, tempnenner = 0;
             AskForBruch(out zaehler1, out nenner1);
             AskForBruch(out zaehler2, out nenner2);
             Bruch bruch1 = new Bruch(zaehler1, nenner1);
             Bruch bruch2 = new Bruch(zaehler2, nenner2);
             Bruch TempBruch = new Bruch(tempzaehler, tempnenner);
             CalculateBruch(bruch1, bruch2, ref TempBruch);
             Console.WriteLine(TempBruch.Zaehler); Console.WriteLine("-"); Console.WriteLine(TempBruch.Nenner);
             WriteToConsole(TempBruch.Zaehler / TempBruch.Nenner);

         }



         New(); 
        


    }
    static decimal AskForNumber()
    {
        string? input;
        decimal number = 0;

        do
        {
            Console.WriteLine("Enter Number");
            input = Console.ReadLine();

            if (decimal.TryParse(input, out number))
            {

                break;
            }
            else
            {
                Console.WriteLine("Invalid Input");

            }

        } while (true);
        return number;
    }

    static decimal AskForType()
    {
        int typeint = 0;
        Console.WriteLine("Number or Bruch");
        string? inputType = Console.ReadLine();
        while (!(inputType == "Number" || inputType == "Bruch"))
        { 
          Console.WriteLine("Invalid Input");
          Console.WriteLine("Number or Bruch");
          inputType = Console.ReadLine();
        }
        
        if (inputType == "Number")
        { typeint = 1; }
        else if (inputType == "Bruch")
        {
            { typeint = 2; }

        }
        return typeint;
    }


    static string AskForOperand()
    {

        
        Console.WriteLine("insert calculation method(*,-,+,/,%,!)");
        string? operand = Console.ReadLine();
        while (!(operand == "*" || operand == "/" || operand == "+" ||
            operand == "-" || operand == "%" || operand == "!" || operand == "^"))
        {
            Console.WriteLine("Invalid Input");
            Console.WriteLine("insert calculation method(*,-,+,/,%,!,^)");
            operand = Console.ReadLine();
            
        }
        return operand;
    }
    static string AskForOperandBruch() 
    {
        Console.WriteLine("insert calculation method(*,-,+,/)");
        string? operand = Console.ReadLine();
        while (!(operand == "*" || operand == "/" || operand == "+" ||
            operand == "-" || operand == "%" ))
        {
            Console.WriteLine("Invalid Input");
            Console.WriteLine("insert calculation method(*,-,+,/,%,!,^)");
            operand = Console.ReadLine();
        }
        return operand;
    }

    private static decimal CalculateNumber()
    {
        decimal number1 = AskForNumber();
        decimal number2 = 0;
        string Operand = AskForOperand();
        decimal total = 0;

        if (Operand != "!")
        {
            number2 = AskForNumber();
        }

        else
        {
            total = 1;
            for (; number1 >= 1; number1--)
                total = number1 * total;
        }
        switch (Operand)
        {

            case "^":
                total = 1;
                for (; number2 >= 1; number2--)
                    total = number1 * total;
                break;

            case "+":
                total = (number1) + (number2);
                break;

            case "-":
                total = number1 - number2;
                break;

            case "*":
                total = (number1) * (number2);
                break;

            case "/":
                total = (number1) / (number2);
                break;

            case "%":
                total = (number1) % (number2);
                break;
        }

        return total;
    }
    private static void WriteToConsole(decimal total)

    {

        string? consoleTotal = "The total =" + total;
        Console.WriteLine(consoleTotal);
        Console.ReadKey();
    }

    private static void New()
    {
        Console.WriteLine("Write new to create another Calculation");
        string? newCalculation = Console.ReadLine();
        while (newCalculation == "new")
        {
            decimal total = CalculateNumber();
            WriteToConsole(total);
            Console.WriteLine("Write new to create another Calculation");
            newCalculation = Console.ReadLine();

        }

    }


    private static void CalculateBruch(Bruch Bruch1, Bruch Bruch2, ref Bruch BruchErgebnis)

    {

        string Operand = AskForOperandBruch();
        switch (Operand)
        {

            case "+":
                BruchAddition(Bruch1, Bruch2, ref BruchErgebnis);
                break;
            case "-":
                BruchSubtract(Bruch1, Bruch2, ref BruchErgebnis);
                break;
            case "*":
                BruchMultiply(Bruch1, Bruch2, ref BruchErgebnis);
                break;
            case "/":
                BruchDivide(Bruch1, Bruch2, ref BruchErgebnis);
                break;


        }
    }
    static void BruchAddition(Bruch Bruch1, Bruch Bruch2, ref Bruch BruchErgebnis)
    {
        if (Bruch1.Nenner == Bruch2.Nenner)
        {
            BruchErgebnis.Zaehler = Bruch1.Zaehler + Bruch2.Zaehler;
            BruchErgebnis.Nenner = Bruch1.Nenner;
            BruchKuerzen(ref BruchErgebnis);
        }
        else
        {
            BruchErgebnis.Zaehler = Bruch1.Zaehler * Bruch2.Nenner;
            BruchErgebnis.Zaehler += Bruch2.Zaehler * Bruch1.Nenner;
            BruchErgebnis.Nenner = Bruch1.Nenner * Bruch2.Nenner;
            BruchKuerzen(ref BruchErgebnis);
        }

    }
   

    

    private static void BruchSubtract(Bruch Bruch1, Bruch Bruch2, ref Bruch BruchErgebnis)
    {
        if (Bruch1.Nenner == Bruch2.Nenner)
        {
            BruchErgebnis.Zaehler = Bruch1.Zaehler - Bruch2.Zaehler;
            BruchErgebnis.Nenner = Bruch1.Nenner;
            BruchKuerzen(ref BruchErgebnis);
        }
       else 
        {
            decimal TempNenner1 = Bruch1.Nenner;
            decimal TempNenner2 = Bruch2.Nenner;
            Bruch1.Zaehler = Bruch1.Zaehler * Bruch2.Nenner;
            Bruch2.Zaehler = Bruch1.Nenner * Bruch2.Zaehler;
            Bruch1.Nenner = Bruch1.Nenner * TempNenner2;
            Bruch2.Nenner = Bruch2.Nenner * TempNenner1;
            BruchErgebnis.Zaehler = Bruch1.Zaehler - Bruch2.Zaehler;
            BruchErgebnis.Nenner = Bruch1.Nenner;
            BruchKuerzen(ref BruchErgebnis);
        } 

    }

    private static void BruchMultiply(Bruch Bruch1, Bruch Bruch2, ref Bruch BruchErgebnis)
    {
      BruchErgebnis.Zaehler = Bruch1.Zaehler * Bruch2.Zaehler;
        BruchErgebnis.Nenner = Bruch1.Nenner * Bruch2.Nenner;
        BruchKuerzen(ref BruchErgebnis);

    }

    private static void BruchDivide(Bruch Bruch1, Bruch Bruch2, ref Bruch BruchErgebnis)
    {
        BruchErgebnis.Zaehler = Bruch1.Zaehler * Bruch2.Nenner;
        BruchErgebnis.Nenner = Bruch1.Nenner * Bruch2.Zaehler;
        BruchKuerzen(ref BruchErgebnis);

    }
    private static void BruchKuerzen(ref Bruch BruchErgebnis)
    {
        int ggT = GGT(BruchErgebnis.Zaehler, BruchErgebnis.Nenner);

        BruchErgebnis.Zaehler = BruchErgebnis.Zaehler / ggT;
        BruchErgebnis.Nenner = BruchErgebnis.Nenner / ggT;
    }
    private static int GGT(decimal Zaehler, decimal Nenner) 
    {
        
        int a = Convert.ToInt32(Zaehler);
        int b = Convert.ToInt32(Nenner);
        int r = a % b;
        if (r == 0)
        { return b; }

        else
        {return GGT(b, r); }

    }
    private static void AskForBruch(out decimal Zaehler, out decimal Nenner)
    {

        Console.WriteLine("Enter Zaehler");
        string? zaehlerString = Console.ReadLine();
        do
        {
            if (decimal.TryParse(zaehlerString, out Zaehler))
            {

                break;
            }
            else
            {
                Console.WriteLine("Invalid Input");
                Console.WriteLine("Enter Zaehler");
                zaehlerString = Console.ReadLine();
            }
        } while (true);

        Console.WriteLine("Enter Nenner");
        string? bruchString = Console.ReadLine();

        do
        {
            if (decimal.TryParse(bruchString, out Nenner))
            {

                break;
            }
            else
            {
                Console.WriteLine("Invalid Input");
                Console.WriteLine("Enter Nenner");
                bruchString = Console.ReadLine();
            }
        } while (true);


    }
    
}

public class Bruch
{
    public decimal Nenner { get; set; }
    public decimal Zaehler { get; set; }

    public Bruch(decimal zaehler, decimal nenner)
    {
        Nenner = nenner;
        Zaehler = zaehler;

    }

}
