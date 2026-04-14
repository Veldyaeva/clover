using SewingProduction.Features.TeamWork.Models;
using System.Collections.Generic;
using System.Linq;

namespace SewingProduction.Features.TeamWork.Helpers
{
    internal static class BaseNodeMetadataOptions
    {
        public static readonly BaseNodeMetadataItem[] NodeTypes =
        {
            new BaseNodeMetadataItem { Id = 1, Code = "PREP", Name = "Заготовка", SortOrder = 10 },
            new BaseNodeMetadataItem { Id = 2, Code = "ASM", Name = "Монтаж", SortOrder = 20 },
            new BaseNodeMetadataItem { Id = 3, Code = "FIN", Name = "Отделка", SortOrder = 30 },
            new BaseNodeMetadataItem { Id = 4, Code = "MRK", Name = "Маркировка", SortOrder = 40 },
            new BaseNodeMetadataItem { Id = 5, Code = "PKG", Name = "Упаковка", SortOrder = 50 }
        };

        public static readonly BaseNodeMetadataItem[] NodeGroups =
        {
            new BaseNodeMetadataItem { Id = 1, Code = "POCKET", Name = "Карман", SortOrder = 10 },
            new BaseNodeMetadataItem { Id = 2, Code = "COLLAR", Name = "Воротник", SortOrder = 20 },
            new BaseNodeMetadataItem { Id = 3, Code = "NECK", Name = "Горловина", SortOrder = 30 },
            new BaseNodeMetadataItem { Id = 4, Code = "SLEEVE", Name = "Рукав", SortOrder = 40 },
            new BaseNodeMetadataItem { Id = 5, Code = "CUFF", Name = "Манжета", SortOrder = 50 },
            new BaseNodeMetadataItem { Id = 6, Code = "WAIST", Name = "Пояс", SortOrder = 60 },
            new BaseNodeMetadataItem { Id = 7, Code = "HOOD", Name = "Капюшон", SortOrder = 70 },
            new BaseNodeMetadataItem { Id = 8, Code = "PLACKET", Name = "Планка", SortOrder = 80 },
            new BaseNodeMetadataItem { Id = 9, Code = "FASTENER", Name = "Застежка", SortOrder = 90 },
            new BaseNodeMetadataItem { Id = 10, Code = "HEM_BODY", Name = "Низ изделия", SortOrder = 100 },
            new BaseNodeMetadataItem { Id = 11, Code = "HEM_SLEEVE", Name = "Низ рукава", SortOrder = 110 },
            new BaseNodeMetadataItem { Id = 12, Code = "ARMHOLE", Name = "Пройма", SortOrder = 120 },
            new BaseNodeMetadataItem { Id = 13, Code = "FRONT", Name = "Полочка", SortOrder = 130 },
            new BaseNodeMetadataItem { Id = 14, Code = "BACK", Name = "Спинка", SortOrder = 140 },
            new BaseNodeMetadataItem { Id = 15, Code = "YOKE", Name = "Кокетка", SortOrder = 150 },
            new BaseNodeMetadataItem { Id = 16, Code = "GUSSET", Name = "Ластовица", SortOrder = 160 },
            new BaseNodeMetadataItem { Id = 17, Code = "STRAP", Name = "Бретели", SortOrder = 170 },
            new BaseNodeMetadataItem { Id = 18, Code = "FLOUNCE", Name = "Волан", SortOrder = 180 },
            new BaseNodeMetadataItem { Id = 19, Code = "LOOP", Name = "Шлевки", SortOrder = 190 },
            new BaseNodeMetadataItem { Id = 20, Code = "LABEL", Name = "Маркировка", SortOrder = 200 },
            new BaseNodeMetadataItem { Id = 21, Code = "DECOR", Name = "Декор", SortOrder = 210 },
            new BaseNodeMetadataItem { Id = 22, Code = "UNIV", Name = "Универсально", SortOrder = 999 }
        };

        public static readonly BaseNodeMetadataItem[] NodeSubgroups =
        {
            new BaseNodeMetadataItem { Id = 1, ParentId = 1, Code = "PATCH", Name = "Накладной", SortOrder = 10 },
            new BaseNodeMetadataItem { Id = 2, ParentId = 1, Code = "IN_SEAM", Name = "В шве", SortOrder = 20 },
            new BaseNodeMetadataItem { Id = 3, ParentId = 1, Code = "WELT", Name = "С листочкой", SortOrder = 30 },
            new BaseNodeMetadataItem { Id = 4, ParentId = 1, Code = "ZIP", Name = "С молнией", SortOrder = 40 },
            new BaseNodeMetadataItem { Id = 5, ParentId = 1, Code = "FLAP", Name = "С клапаном", SortOrder = 50 },
            new BaseNodeMetadataItem { Id = 6, ParentId = 1, Code = "SET_IN", Name = "Втачной", SortOrder = 60 },
            new BaseNodeMetadataItem { Id = 7, ParentId = 2, Code = "STAND", Name = "Стойка", SortOrder = 10 },
            new BaseNodeMetadataItem { Id = 8, ParentId = 2, Code = "FLAT", Name = "Отложной", SortOrder = 20 },
            new BaseNodeMetadataItem { Id = 9, ParentId = 2, Code = "SHAWL", Name = "Шалька", SortOrder = 30 },
            new BaseNodeMetadataItem { Id = 10, ParentId = 2, Code = "WITH_STAND", Name = "С отрезной стойкой", SortOrder = 40 },
            new BaseNodeMetadataItem { Id = 11, ParentId = 2, Code = "ONE_PIECE", Name = "Цельнокроеный", SortOrder = 50 },
            new BaseNodeMetadataItem { Id = 12, ParentId = 3, Code = "FACING", Name = "С обтачкой", SortOrder = 10 },
            new BaseNodeMetadataItem { Id = 13, ParentId = 3, Code = "BINDING", Name = "С окантовкой", SortOrder = 20 },
            new BaseNodeMetadataItem { Id = 14, ParentId = 3, Code = "BAND", Name = "С бейкой", SortOrder = 30 },
            new BaseNodeMetadataItem { Id = 15, ParentId = 3, Code = "DROP", Name = "С каплей", SortOrder = 40 },
            new BaseNodeMetadataItem { Id = 16, ParentId = 3, Code = "REINF", Name = "С усилителем", SortOrder = 50 },
            new BaseNodeMetadataItem { Id = 17, ParentId = 4, Code = "SET_IN", Name = "Втачной", SortOrder = 10 },
            new BaseNodeMetadataItem { Id = 18, ParentId = 4, Code = "REGLAN", Name = "Реглан", SortOrder = 20 },
            new BaseNodeMetadataItem { Id = 19, ParentId = 4, Code = "ONE_PIECE", Name = "Цельнокроеный", SortOrder = 30 },
            new BaseNodeMetadataItem { Id = 20, ParentId = 4, Code = "WITH_CUFF", Name = "С манжетой", SortOrder = 40 },
            new BaseNodeMetadataItem { Id = 21, ParentId = 4, Code = "ELASTIC", Name = "На резинке", SortOrder = 50 },
            new BaseNodeMetadataItem { Id = 22, ParentId = 5, Code = "KNIT", Name = "Трикотажная", SortOrder = 10 },
            new BaseNodeMetadataItem { Id = 23, ParentId = 5, Code = "SET_IN", Name = "Притачная", SortOrder = 20 },
            new BaseNodeMetadataItem { Id = 24, ParentId = 5, Code = "ELASTIC", Name = "На резинке", SortOrder = 30 },
            new BaseNodeMetadataItem { Id = 25, ParentId = 6, Code = "SET_IN", Name = "Притачной", SortOrder = 10 },
            new BaseNodeMetadataItem { Id = 26, ParentId = 6, Code = "ELASTIC", Name = "На резинке", SortOrder = 20 },
            new BaseNodeMetadataItem { Id = 27, ParentId = 6, Code = "DRAW", Name = "С кулиской", SortOrder = 30 },
            new BaseNodeMetadataItem { Id = 28, ParentId = 6, Code = "TAPE", Name = "С кипером", SortOrder = 40 },
            new BaseNodeMetadataItem { Id = 29, ParentId = 6, Code = "LOOPS", Name = "Со шлевками", SortOrder = 50 },
            new BaseNodeMetadataItem { Id = 30, ParentId = 7, Code = "BASIC", Name = "Обычный", SortOrder = 10 },
            new BaseNodeMetadataItem { Id = 31, ParentId = 7, Code = "DRAW", Name = "С кулиской", SortOrder = 20 },
            new BaseNodeMetadataItem { Id = 32, ParentId = 7, Code = "ZIP", Name = "С молнией", SortOrder = 30 },
            new BaseNodeMetadataItem { Id = 33, ParentId = 7, Code = "HEMMED", Name = "С подгибкой", SortOrder = 40 },
            new BaseNodeMetadataItem { Id = 34, ParentId = 9, Code = "ZIP", Name = "Молния", SortOrder = 10 },
            new BaseNodeMetadataItem { Id = 35, ParentId = 9, Code = "PLACKET", Name = "Планка на пуговицах", SortOrder = 20 },
            new BaseNodeMetadataItem { Id = 36, ParentId = 9, Code = "BUTTONS", Name = "Петли и пуговицы", SortOrder = 30 },
            new BaseNodeMetadataItem { Id = 37, ParentId = 9, Code = "SNAPS", Name = "Кнопки", SortOrder = 40 }
        };

        public static readonly string[] ProductCategories =
        {
            "Платье",
            "Сарафан",
            "Шорты",
            "Брюки",
            "Юбка",
            "Худи",
            "Свитшот",
            "Лонгслив",
            "Футболка",
            "Блуза",
            "Куртка",
            "Жилет",
            "Трусы",
            "Носки",
            "Шапка",
            "Универсально"
        };

        public static IReadOnlyList<BaseNodeMetadataItem> GetNodeSubgroups(int? nodeGroupId)
        {
            if (!nodeGroupId.HasValue)
            {
                return new List<BaseNodeMetadataItem>();
            }

            return NodeSubgroups
                .Where(item => item.ParentId == nodeGroupId.Value)
                .OrderBy(item => item.SortOrder)
                .ThenBy(item => item.Name)
                .ToList();
        }
    }
}
