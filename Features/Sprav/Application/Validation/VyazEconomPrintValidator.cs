using SewingProduction.Features.Sprav.Application.Contexts;
using SewingProduction.Features.Sprav.Application.Results;

namespace SewingProduction.Features.Sprav.Application.Validation
{
    public sealed class VyazEconomPrintValidator
    {
        public OperationResult Validate(VyazEconomPrintContext? context)
        {
            if (context == null)
            {
                return OperationResult.Fail("Выберите строку в таблице.");
            }

            if (context.Nn <= 0)
            {
                return OperationResult.Fail("У выбранной строки не задан номер записи (nn).");
            }

            if (context.NomZadany == "")
            {
                return OperationResult.Fail("У выбранной строки не задан номер задания.");
            }

            if (context.IdPodr <= 0)
            {
                return OperationResult.Fail("У выбранной строки не задано подразделение.");
            }

            return OperationResult.Ok();
        }
    }
}
