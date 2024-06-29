using SureProfit.Domain.Common;

namespace SureProfit.Domain.ValueObjects;

public record Cnpj
{
    public const int CnpjLength = 14;
    public string Value { get; private set; } = string.Empty;

    protected Cnpj() { }

    public Cnpj(string cnpj)
    {
        if (!IsValid(cnpj))
        {
            throw new DomainException("Invalid CNPJ");
        }

        Value = cnpj;
    }

    public static bool IsValid(string cnpj)
    {
        if (cnpj.Length != CnpjLength)
        {
            return false;
        }

        int[] multiplier1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplier2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");

        string tempCnpj = cnpj[..12];
        int sum = 0;

        for (int i = 0; i < 12; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

        int rest = sum % 11;
        rest = rest < 2 ? 0 : 11 - rest;

        string digit = rest.ToString();
        tempCnpj += digit;
        sum = 0;

        for (int i = 0; i < 13; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

        rest = sum % 11;
        rest = rest < 2 ? 0 : 11 - rest;

        digit += rest.ToString();

        return cnpj.EndsWith(digit);
    }
}
