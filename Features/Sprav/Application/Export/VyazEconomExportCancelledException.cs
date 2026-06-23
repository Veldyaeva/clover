using System;

namespace SewingProduction.Features.Sprav.Application.Export
{
    /// <summary>
    /// Пользователь отменил сохранение Excel-файла.
    /// </summary>
    public sealed class VyazEconomExportCancelledException : Exception
    {
        public VyazEconomExportCancelledException()
            : base("Формирование калькуляции отменено.")
        {
        }
    }
}
