using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeuristicaConstrutiva;

namespace CaixeiroViajante
{
    public class ConstroiGrafo : HeuristicaConstrutiva.HeuristicaConstrutiva
    {
        public List<Aresta> Arestas { get; set; }
        public Queue<Vertice> VerticesJaVisitados { get; set; }
        public Vertice CidadeInicial { get; set; }
        public int QtdeCidades { get; set; }
        public Caminho Grafo
        {
            get
            {
                return (Caminho)Solucao;
            }
        }

        public ConstroiGrafo(int qtdCidade)
        {
            QtdeCidades = qtdCidade;
            Arestas = new List<Aresta>();
            VerticesJaVisitados = new Queue<Vertice>();
            //Grafo = new Caminho();
        }

        public override List<IComponente> GerarComponentes()
        {
            List<IComponente> arestas = new List<IComponente>();
            //if (Arestas.Count == 0)
            if(Grafo.Componentes.Count == 0)
            {
                foreach (Aresta a in CidadeInicial.Arestas())
                    if (!VerificaSeJaFoiVizitado(a))
                        arestas.Add(a);
            }
            else
                foreach (Aresta a in VerticesJaVisitados.Last().Arestas())//isso não está funcionando. Não está retornando nada.
                    if (!VerificaSeJaFoiVizitado(a)) 
                        arestas.Add(a);
            return arestas;
        }

        bool VerificaSeJaFoiVizitado(Aresta a)
        {
            if (a.CidadeDestino.Equals(CidadeInicial))
                if (VerticesJaVisitados.Count == QtdeCidades)
                    return false;

            foreach (Vertice cidade in VerticesJaVisitados)
                if (a.CidadeDestino.Equals(cidade))
                    return true;
            return false;
        }

        public override IComponente EscolheMelhorComponente(List<IComponente> Componentes)
        {
            Aresta melhor = (Aresta)Componentes.FirstOrDefault();
            foreach (Aresta aresta in Componentes)
            {
                if (melhor != null)
                    if ((int)melhor.Valor < (int)aresta.Valor)
                        melhor = aresta;
            }
            if(melhor != null)
                VerticesJaVisitados.Enqueue(melhor.CidadeDestino);

            return melhor;
        }

        public override ISolucao CriaSolucaoVazia()
        {
            return (ISolucao)new Caminho();
        }
        

        public override bool VerificaSolucaoCompleta()
        {
            if (Arestas.LastOrDefault().CidadeDestino == CidadeInicial)
                return true;
            else
                return false;
                    
        }
    }
}
