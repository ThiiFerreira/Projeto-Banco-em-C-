using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBanco
{
	class Program
	{
		static void Main(string[] args)
		{
			var sistemaBanco = new SistemaAgencia(); 
			var contas = new List<ContaCorrente>();
			
			// metodo para add lista de contas do arquivo txt
			AddContas(contas);

			var controleSistema = 1;
			do
			{
				ContaCorrente conta = null;
				//menu de login
				while (conta == null)
				{
					conta = LoginConta(contas);
				}


				Console.Clear();
				Console.WriteLine($"Bem vindo {conta.Titular.Nome}");
				PausaElimpaConsole();

				//menu do banco
				MenuConta(conta, contas);

				Console.Clear();
				Console.WriteLine("Deseja fazer login em outra conta?\n[1] - SIM\n[2] - NÃO");
				controleSistema = int.Parse(Console.ReadLine());

			} while (controleSistema == 1);

			Console.Clear();
			Console.WriteLine("Ate Breve...");
		}

		static ContaCorrente LoginConta(List<ContaCorrente> contas)
		{
			ContaCorrente contaLogin = null;
			Console.Clear();
			Console.WriteLine("\n----BEM VINDO AO BANCO TH----");
			Console.WriteLine("\nTELA DE LOGIN");
			Console.Write("Digite a agencia do seu banco: ");
			var agencia = int.Parse(Console.ReadLine());

			Console.Write("Digite o numero do seu banco: ");
			var numeroConta = int.Parse(Console.ReadLine());

			var sistemaBanco = new SistemaAgencia();
			
			try
			{
				return contaLogin = sistemaBanco.buscarConta(contas, agencia, numeroConta);		
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine(ex.Message);
				PausaElimpaConsole();

			}
			return null;
		}

		static void MenuConta(ContaCorrente conta, List<ContaCorrente> contas)
		{
			var sistemaBanco = new SistemaAgencia();
			var controleMenu = true;
			while (controleMenu)
			{
				Console.Clear();
				Console.WriteLine("-----MENU-----");
				Console.WriteLine("[1] - FAZER UM DEPOSITO");
				Console.WriteLine("[2] - FAZER UM SAQUE");
				Console.WriteLine("[3] - FAZER UMA TRANFERENCIA");
				Console.WriteLine("[4] - EXEBIR MEU DADOS");
				Console.WriteLine("[5] - EMITIR EXTRATO");
				Console.WriteLine("[9] - LIMPAR TERMINAL");
				Console.WriteLine("[0] - SAIR DA CONTA");

				var valor = int.Parse(Console.ReadLine());
				switch (valor)
				{
					case 1:
						Console.Clear();
						Console.WriteLine("Deseja fazer um deposito de qual valor?");
						var valorDeposito = double.Parse(Console.ReadLine());
						try
						{
							conta.Depositar(valorDeposito);
							Console.WriteLine($"Deposito realizado com sucesso no valor de {valorDeposito}");
							PausaElimpaConsole();
						}
						catch (ArgumentException ex)
						{
							Console.WriteLine(ex.Message);
						}
						break;

					case 2:
						Console.Clear();
						Console.WriteLine("Deseja fazer um saque de qual valor?");
						var valorSaque = double.Parse(Console.ReadLine());
						try
						{
							conta.Sacar(valorSaque);
							PausaElimpaConsole();
						}
						catch (ArgumentException ex)
						{
							Console.WriteLine(ex.Message);
						}
						break;

					case 3:
						Console.Clear();
						Console.WriteLine("Deseja fazer uma tranferencia de qual valor?");
						var valorTrasnferencia = double.Parse(Console.ReadLine());

						Console.Write("Digite a agencia do banco do destinatario: ");
						var ag = int.Parse(Console.ReadLine());

						Console.Write("Digite o numero do banco do destinatario: ");
						var numero = int.Parse(Console.ReadLine());

						try
						{
							var contaDestino = sistemaBanco.buscarConta(contas, ag, numero);
							conta.Transferir(valorTrasnferencia, contaDestino);
							PausaElimpaConsole();
						}
						catch (ArgumentException ex)
						{
							Console.WriteLine(ex.Message);
						}
						break;

					case 4:
						Console.Clear();
						Console.WriteLine(conta.ToString());
						PausaElimpaConsole();
						break;
					case 5:
						conta.EmitirExtrato();
						PausaElimpaConsole();
						break;
					case 9:
						Console.Clear();
						break;
					case 0:
						controleMenu = false;
						break;
				}
			}
		}

		static void PausaElimpaConsole()
		{
			Console.WriteLine("\nPressione enter para continuar");
			Console.ReadLine();
			Console.Clear();
		}

		static void AddContas(List<ContaCorrente> contas)
		{
			var enderecoDoArquivo = "contas.txt"; //encontra o endereço do arquivo onde tem os dados para criar as contas
			using (var fluxoDeArquivo = new FileStream(enderecoDoArquivo, FileMode.Open)) // abre o arquivo e salva em fluxo do arquivo
			using (var leitor = new StreamReader(fluxoDeArquivo)) // criar um leitor para o arquivo
			{
				while (!leitor.EndOfStream) // enquanto não chegar no final do arquivo ler linha por linha
				{
					var linha = leitor.ReadLine(); // ler a linha do arquivo
					var contaCorrente = ConverterStringParaContaCorrente(linha); // chama o metodo para converter os dados da linha e criar um objeto do tipo conta corrente
					contas.Add(contaCorrente); // add a conta criada em uma lista de contas
				}
			}
		}

		static ContaCorrente ConverterStringParaContaCorrente(string linha)
		{
			// ex de uma linha do arquivo
			// 234,4020,2822.52,Debora
			// agencia, numero conta, saldo, nome titular
			string[] campos = linha.Split(','); // recebe a linha do arquivo e separa em array cada campo antes de uma ","

			var agencia = campos[0];
			var numero = campos[1];
			var saldo = campos[2].Replace('.', ',');
			var nomeTitular = campos[3];

			var titular = new Cliente(nomeTitular);

			var resultado = new ContaCorrente(int.Parse(agencia), int.Parse(numero), titular);
			resultado.Depositar(double.Parse(saldo));

			return resultado; // retorna a conta criada
		}

	}
}
