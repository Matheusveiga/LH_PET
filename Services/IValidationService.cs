using System.Text.RegularExpressions;

namespace LH_PET.Services
{
    public interface IValidationService
    {
        /// <summary>
        /// Valida um CPF (Cadastro de Pessoa Física)
        /// </summary>
        bool IsValidCPF(string cpf);

        /// <summary>
        /// Valida um CNPJ (Cadastro Nacional de Pessoa Jurídica)
        /// </summary>
        bool IsValidCNPJ(string cnpj);

        /// <summary>
        /// Remove caracteres não numéricos do CPF/CNPJ
        /// </summary>
        string CleanDocument(string value);
    }

    public class ValidationService : IValidationService
    {
        /// <summary>
        /// Valida CPF com cálculo de dígitos verificadores
        /// </summary>
        public bool IsValidCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = CleanDocument(cpf);

            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cpf.All(c => c == cpf[0]))
                return false;

            // Valida primeiro dígito verificador
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(cpf[i].ToString()) * (10 - i);
            }

            int remainder = sum % 11;
            int firstCheck = remainder < 2 ? 0 : 11 - remainder;

            if (int.Parse(cpf[9].ToString()) != firstCheck)
                return false;

            // Valida segundo dígito verificador
            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(cpf[i].ToString()) * (11 - i);
            }

            remainder = sum % 11;
            int secondCheck = remainder < 2 ? 0 : 11 - remainder;

            return int.Parse(cpf[10].ToString()) == secondCheck;
        }

        /// <summary>
        /// Valida CNPJ com cálculo de dígitos verificadores
        /// </summary>
        public bool IsValidCNPJ(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            cnpj = CleanDocument(cnpj);

            if (cnpj.Length != 14)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cnpj.All(c => c == cnpj[0]))
                return false;

            // Valida primeiro dígito verificador
            int[] multiplier1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum = 0;

            for (int i = 0; i < 12; i++)
            {
                sum += int.Parse(cnpj[i].ToString()) * multiplier1[i];
            }

            int remainder = sum % 11;
            int firstCheck = remainder < 2 ? 0 : 11 - remainder;

            if (int.Parse(cnpj[12].ToString()) != firstCheck)
                return false;

            // Valida segundo dígito verificador
            int[] multiplier2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            sum = 0;

            for (int i = 0; i < 13; i++)
            {
                sum += int.Parse(cnpj[i].ToString()) * multiplier2[i];
            }

            remainder = sum % 11;
            int secondCheck = remainder < 2 ? 0 : 11 - remainder;

            return int.Parse(cnpj[13].ToString()) == secondCheck;
        }

        public string CleanDocument(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return Regex.Replace(value, @"\D", "");
        }
    }
}
