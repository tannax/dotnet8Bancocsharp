using System;
using System.Collections.Generic;

namespace Bancario
{
    public class Conta
    {
        public int numero;
        public int digitoVerificador;
        public double saldo;
        public string? titular; // Account holder

        public Conta(string? titular)
        {
            this.titular = titular;
        }

        public bool RealizarSaque(double valor) // Withdraw operation
        {
            if (saldo >= valor)
            {
                saldo -= valor;
                return true;
            }
            else
            {
                return false; // Insufficient balance
            }
        }

        public void RealizarDeposito(double valor) // Deposit operation
        {
            if (valor > 0)
            {
                saldo += valor;
            }
        }
    }

    public class Login
    {
        public string? login;
        public string? senha;

        public Login(string? login)
        {
            this.login = login;
        }

        // Method to authenticate user
        public static bool Autenticar(string? login, string? senha, Dictionary<string, string> credenciais)
        {
            if (credenciais.ContainsKey(login) && credenciais[login] == senha)
            {
                return true;
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Dictionary to store username-password pairs
            Dictionary<string, string> credenciais = new Dictionary<string, string>
            {
                {"user1", "password1"},
                {"user2", "password2"}
                // Add more credentials as needed
            };

            Console.Write("Digite seu nome de usuário (Enter your username): ");
            string? usuario = Console.ReadLine();

            Console.Write("Digite sua senha (Enter your password): ");
            string? senha = Console.ReadLine();

            // Authenticate user
            bool autenticado = Login.Autenticar(usuario, senha, credenciais);

            if (autenticado)
            {
                Console.WriteLine("Login bem sucedido! (Login successful)");

                Console.Write("Digite seu nome (Enter your name): ");
                string? titular = Console.ReadLine();

                Conta contaBancaria = new Conta(titular);

                contaBancaria.numero = 1234;
                contaBancaria.digitoVerificador = 1;

                string comando;
                double valor;

                do
                {
                    Console.Write("Digite a operação [d - depósito; s - saque; x - sair]: ");
                    comando = Console.ReadLine() ?? "";

                    switch (comando)
                    {
                        case "d":
                            Console.Write("Digite o valor a depositar (Enter the amount to deposit): ");
                            if (double.TryParse(Console.ReadLine(), out valor))
                            {
                                contaBancaria.RealizarDeposito(valor);
                            }
                            else
                            {
                                Console.WriteLine("Valor inválido (Invalid value)");
                            }
                            break;

                        case "s":
                            Console.Write("Digite o valor a sacar (Enter the amount to withdraw): ");
                            if (double.TryParse(Console.ReadLine(), out valor))
                            {
                                if (!contaBancaria.RealizarSaque(valor))
                                {
                                    Console.WriteLine("Saldo insuficiente (Insufficient balance)");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Valor inválido (Invalid value)");
                            }
                            break;

                        case "x":
                            Console.WriteLine("Encerrando a aplicação (Closing the application)");
                            break;

                        default:
                            Console.WriteLine("Opção inválida (Invalid option)");
                            break;
                    }
                } while (comando != "x");
            }
            else
            {
                Console.WriteLine("Login falhou. Verifique suas credenciais. (Login failed. Please check your credentials.)");
            }
        }
    }
}
