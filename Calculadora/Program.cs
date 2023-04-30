using System.Collections;
using Microsoft.VisualBasic;
namespace Calculadora
{
    public static class Program
    {
        static void Main()
        {
            String op = Interaction.InputBox("Qual notação deseja usar: \n1 -- Notacao infixa\n2 -- Notacao pósfixa\n3 -- Notação préfixa");
            switch (op)
            {
                case "1":
                    String exp = Interaction.InputBox("Opção escolhida: NOTAÇÃO INFIXA\nDigite a expressão usando espaço entre operadores, operandos e parenteses: ");
                    String[] sep = exp.Split(' ');
                    MessageBox.Show("Resultado da expressão: " + posFix(ConvertInPos(sep)));
                    break;
                case "2":
                    exp = Interaction.InputBox("Opção escolhida: NOTAÇÃO PÓS-FIXA\nDigite a expressão usando espaço entre membros e operadores: ");
                    sep = exp.Split(' ');
                    MessageBox.Show("Resultado da expressão: " + posFix(sep));
                    break;
                case "3":
                    exp = Interaction.InputBox("Opção escolhida: NOTAÇÃO PRÉ-FIXA\nDigite a expressão usando espaço entre membros e operadores: ");
                    sep = exp.Split(' ');
                    MessageBox.Show("Resultado da expressão: " + preFix(sep));
                    break;
                default:
                    MessageBox.Show("Opção inválida!");
                    break;
            }
        }
        public static String[] ConvertInPos(String[] sep)
        {
            Stack<String> pilha = new Stack<String>();
            Stack<String> posPilha = new Stack<String>();
            foreach(String s in sep)
            {
                switch (s)
                {
                    case "(":
                        pilha.Push(s);
                        break;
                    case ")":
                        while (pilha.Peek() != "(")
                        {
                            posPilha.Push(pilha.Pop());
                        }
                        pilha.Pop();
                        break;
                    case "+":
                    case "-":
                        while (pilha.Peek() != "(" && pilha.Count() != 0)
                        {
                            posPilha.Push(pilha.Pop());
                        }
                        pilha.Push(s);
                        break;
                    case "*":
                    case "/":
                        while (pilha.Peek() != "(" && pilha.Count() != 0 && pilha.Peek() != "+" && pilha.Peek() != "-")
                        {
                            posPilha.Push(pilha.Pop());
                        }
                        pilha.Push(s);
                        break;

                    default:
                        posPilha.Push(s);
                        break;
                }
            }
            String[] final = new string[posPilha.Count()];
            for (int i = posPilha.Count; i > 0; i--)
            {
                final[i - 1] = posPilha.Pop();
            }
            return final;
        }
        public static double preFix(String[] sep) //Notação Pré Fixada + 2 * 5 3
        {
            Stack pilha = new Stack();
            foreach (String s in sep)
            {
                pilha.Push(s);
            }
            for (int i = 0; i < sep.Length; i++)
            {
                sep[i] = Convert.ToString(pilha.Pop());
            }
            foreach (String s in sep)
            {
                if (s.Equals("*") || s.Equals("+") || s.Equals("-") || s.Equals("/"))
                {
                    pilha.Push(operar(Convert.ToDouble(pilha.Pop()), Convert.ToDouble(pilha.Pop()), s));
                }
                else
                    pilha.Push(s);
            }
            return Convert.ToDouble(pilha.Pop());
        }
        public static double posFix(String[] sep) //Notação Pós Fixada 2 5 3 * +
        {
            double op1;
            double op2;
            Stack pilha = new Stack();
            foreach (String s in sep)
            {
                if (s.Equals("*") || s.Equals("+") || s.Equals("-") || s.Equals("/"))
                {
                    op2 = Convert.ToDouble(pilha.Pop());
                    op1 = Convert.ToDouble(pilha.Pop());
                    pilha.Push(operar(op1, op2, s));
                }
                else
                    pilha.Push(s);
            }
            return Convert.ToDouble(pilha.Pop());
        }
        public static double operar(double op1, double op2, String operador)//Resolução das operações
        {
            double res = 0;
            switch (operador)
            {
                case "+":
                    res = op1 + op2;
                    break;
                case "-":
                    res = op1 - op2;
                    break;
                case "*":
                    res = op1 * op2;
                    break;
                case "/":
                    res = op1 / op2;
                    break;
                default:
                    break;
            }
            return res;
        }

    }
}