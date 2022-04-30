using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBanco
{
	class SistemaAgencia
	{

		public ContaCorrente buscarConta(List<ContaCorrente> contas, int agencia, int numero)
		{
			foreach (var conta in contas)
			{
				if(conta.Agencia.Equals(agencia) && conta.NumeroConta.Equals(numero))
				{
					return conta;
				}
			}
			throw new ArgumentException("\nConta não encontrada, por favor verificar dados informados");
		}
	}
}

//for (int i = 0;  i <= contas.Count; i++)
//{
//	if (contas[i].Agencia == agencia && contas[i].NumeroConta == numero)
//	{
//		return contas[i];
//	}
//	break;
//}

//throw new ArgumentException("\nConta não encontrada, por favor verificar dados informados");