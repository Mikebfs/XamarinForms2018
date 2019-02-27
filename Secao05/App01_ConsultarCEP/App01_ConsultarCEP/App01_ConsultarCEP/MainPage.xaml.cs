using App01_ConsultarCEP.Servico;
using App01_ConsultarCEP.Servico.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BOTAO.Clicked += BuscarCEP;

        }

        private void BuscarCEP(object sender, EventArgs e)
        {
            try
            {
                string cep = CEP.Text.Trim();
                bool cepValido = isValidCep(cep);

                if (cepValido)
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if(end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {2} de {3} {0}, {1} ", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else
                    {

                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado", "LI E CONCORDO COM OS TERMOS CITADOS ACIMA.");
                    }
                    
                }
            }
            catch (Exception ex)
            {

               DisplayAlert("ERRO CRÍTICO", ex.Message, "OK");
            }
            

            
        }

        private bool isValidCep(string cep)
        {
            bool valido = true;
            if(cep.Length != 8)
            {
                valido = false;
                DisplayAlert("ERRO", "CEP Inválido, deve conter 8 caracteres", "OK");
            }

            int novoCep = 0;
            if(!int.TryParse(cep, out novoCep))
            {
                DisplayAlert("ERRO", "CEP deve ser composto apenas por números", "OK");
                valido = false;
            }

            return valido;
        }
    }
}
