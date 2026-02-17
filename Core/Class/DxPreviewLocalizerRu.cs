using DevExpress.Utils.Localization;
using DevExpress.XtraPrinting.Localization;
using System.Diagnostics;

public sealed class DxPreviewLocalizerRu : PreviewLocalizer
{
    public override XtraLocalizer<PreviewStringId> CreateResXLocalizer()
        => new PreviewResLocalizer();

    public override string GetLocalizedString(PreviewStringId id)
    {
        Trace.WriteLine($"PreviewStringId: {id} => {base.GetLocalizedString(id)}");
        switch (id)
        {
            // Верхняя строка wait-form
            case PreviewStringId.WaitForm_Caption:
                return "Пожалуйста, подождите...";

            // Нижняя строка wait-form (то самое "Creating the document...")
            case PreviewStringId.Msg_CreatingDocument:
                return "Формируется документ...";

            // Экспорт (если надо)
            case PreviewStringId.Msg_ExportingDocument:
                return "Выполняется экспорт...";
            // текст предупреждения
            case PreviewStringId.Msg_PageMarginsWarning:
                return "Поля выходят за пределы печатаемой области страницы. Продолжить?";

            // заголовок окна
            case PreviewStringId.Msg_Caption /*.MsgCaption_Printing*/:
                return "Печать";

            //// кнопки
            //case PreviewStringId.Button_Yes:
            //    return "Да";
            //case PreviewStringId.Button_No:
            //    return "Нет";
            default:
                return base.GetLocalizedString(id);
        }
    }
}
