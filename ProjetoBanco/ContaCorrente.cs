using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBanco
{
	public class ContaCorrente
	{
		public Cliente Titular{ get;}
		public double Saldo { get; private set; }
		public int Agencia { get;}
		public int NumeroConta { get;}

		public ContaCorrente(int agencia, int numero, Cliente titular)
		{
			Titular = titular;
			Agencia = agencia;
			NumeroConta = numero;
		}

		public override string ToString()
		{
			return $"Nome titular:{Titular.Nome}\nAgencia: {Agencia}\nNumero da conta: {NumeroConta}\nSaldo: {Saldo}";
		}

		public void Depositar(double valor)
		{
			if(valor <= 0)
			{
				throw new ArgumentException("Valor invalido para fazer deposito");
			}

			//Console.WriteLine($"Deposito realizado no valor de {valor}");
			Saldo += valor;
		}

		public void Sacar(double valor)
		{
			if (valor <= 0)
			{
				throw new ArgumentException("Valor invalido para fazer saque");
			}
			if(valor > Saldo)
			{
				throw new ArgumentException("Valor do saque não pode ser maior que saldo na conta");
			}

			Console.WriteLine($"Saque realizado no valor de {valor}");
			Saldo -= valor;

		}

		public void Transferir(double valor,ContaCorrente contaDestino)
		{
			if(valor < 0)
			{
				throw new ArgumentException("Valor invalido para fazer tranferencia");
			}

			if (valor > Saldo)
			{
				throw new ArgumentException("Valor da tranferencia não pode ser maior que saldo na conta");
			}

			Saldo -= valor;
			contaDestino.Depositar(valor);
			Console.WriteLine($"\nTranferencia realizada para {contaDestino.Titular.Nome}");
		}

		public void EmitirExtrato()
		{
			Console.Clear();
			var caminhoNovoArquivo = $"extratoConta{Titular.Nome}.txt";

			using (var fluxoDeArquivo = new FileStream(caminhoNovoArquivo, FileMode.Create))
			using (var escritor = new StreamWriter(fluxoDeArquivo))
			{
				escritor.Write(ToString());
			}

			Console.WriteLine($"extrato gerado com o nome de -> extratoConta{Titular.Nome}.txt");
			Console.WriteLine("Extrato salvo na pasta ProjetoBanco\\ProjetoBanco\\bin\\Debug");

		}

	}
}
