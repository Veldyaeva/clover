using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SewingProduction.Features.TeamWork.Helpers
{
    /// <summary>
    /// Вспомогательные методы для клонирования объектов ArtNormN с различными сценариями
    /// </summary>
    public static class CopyHelper
    {
        /// <summary>
        /// Клонирует объект и присваивает новый AnnId. Ключевое поле (ID) обнуляется.
        /// </summary>
        public static T CloneAndAssignNewAnnId<T>(T source, int newAnnId, string keyFieldName = "Id") where T : new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var target = new T();
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead && p.CanWrite)
                .ToList();

            foreach (var prop in properties)
            {
                if (prop.Name == keyFieldName)
                {
                    prop.SetValue(target, 0); // Обнуляем первичный ключ
                }
                else if (prop.Name.Equals("AnnId", StringComparison.OrdinalIgnoreCase))
                {
                    prop.SetValue(target, newAnnId); // Присваиваем новый AnnId
                }
                else
                {
                    prop.SetValue(target, prop.GetValue(source));
                }
            }

            return target;
        }

        public static T CloneProperties<T>(this T source) where T : new()
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            T clone = new T();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                       .Where(p => p.CanRead && p.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source);
                prop.SetValue(clone, value);
            }

            return clone;
        }
        /// <summary>
        /// Клонирует только технологические данные ArtNormN (без индивидуальной информации о продукте)
        /// </summary>
        /// <param name="source">Исходный объект</param>
        /// <returns>Клон с только технологическими данными</returns>
        public static ArtNormN CloneOperationalData(this ArtNormN source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (source == null) return null;

            var clone = new ArtNormN();

            // Копируем только технологические данные
            clone.SekShv = source.SekShv;
            clone.SekVyaz3 = source.SekVyaz3;
            clone.SekVyaz5 = source.SekVyaz5;
            clone.SekVyaz6 = source.SekVyaz6;
            clone.SekVyaz7 = source.SekVyaz7;
            clone.SekVyaz10 = source.SekVyaz10;
            clone.SekVyaz12 = source.SekVyaz12;
            clone.SekVyazo = source.SekVyazo;
            clone.SekVyaz = source.SekVyaz;
            clone.SekVyaz14 = source.SekVyaz14;
            clone.SekVyaz70 = source.SekVyaz70;
            clone.SekVyaz71 = source.SekVyaz71;
            clone.SekVyaz72 = source.SekVyaz72;
            clone.SekVyaz62 = source.SekVyaz62;
            clone.SekVyaz57 = source.SekVyaz57;
            clone.SekVyaz18 = source.SekVyaz18;
            clone.SekShv1 = source.SekShv1;
            clone.Sek = source.Sek;
            clone.SekKr = source.SekKr;
            clone.Seb = source.Seb;
            clone.Komment = source.Komment;
            clone.Reco = source.Reco;
            clone.Slogn = source.Slogn;
            clone.Kod = source.Kod;
            clone.grup = source.grup;
            clone.Articul = source.Articul;
            clone.Mod = source.Mod;
            clone.Diz = source.Diz;
            clone.Constr = source.Constr;

            // НЕ копируем ID и аудит поля:
            // AnnID, dateCreate, dateUpdate,
            clone.ParentId = source.AnnID;

            return clone;
        }

        ///// <summary>
        ///// Клонирует ArtNormN для сценария "Дубль" (дублирование)
        ///// </summary>
        ///// <param name="source">Исходный объект</param>
        ///// <returns>Клон для дублирования</returns>
        //public static ArtNormN CloneForDuplication(this ArtNormN source)
        //{
        //    if (source == null) return null;

        //    var clone = source.CloneOperationalData();

        //    // Устанавливаем значения для дублирования
        //    clone.AnnID = 0; // База присвоит новый ID
        //    clone.ParentId = source.AnnID; // Ссылка на родительскую запись
        //    clone.dateCreate = DateTime.Now;
        //    clone.dateUpdate = null;
        //    clone.Status = (int)Status.Preliminary; // Предварительный статус
        //    clone.Arh = false;

        //    clone.Kod = null;
        //    clone.grup = source.grup;
        //    clone.Articul = source.Articul;
        //    clone.Mod = source.Mod;
        //    clone.Size_label = null;
        //    clone.Diz = source.Diz;
        //    clone.Constr = source.Constr;

        //    return clone;
        //}

        /// <summary>
        /// Клонирует ArtNormN для сценария "Архив+Копия"
        /// </summary>
        /// <param name="source">Исходный объект</param>
        /// <param name="isNzp">Является ли НЗП</param>
        /// <returns>Клон для архив+копия</returns>
        public static ArtNormN CloneForArchiveCopy(this ArtNormN source, bool isNzp = false)
        {
            var clone = source.CloneOperationalData();

            // Копируем информацию для архив+копия
            clone.Kod = source.Kod;
            clone.grup = source.grup;
            clone.Articul = source.Articul;
            clone.Mod = source.Mod;
            clone.Size_label = source.Size_label;
            clone.Diz = source.Diz;
            clone.Constr = source.Constr;

            // Устанавливаем значения для копии
            clone.AnnID = 0; // База присвоит новый ID
            clone.ParentId = source.AnnID; // Ссылка на родительскую запись
            clone.dateCreate = DateTime.Now;
            clone.dateUpdate = null;
            clone.Status = isNzp ? (int)Status.Preliminary : (int)Status.Actual;
            clone.StatusText = StatusHelper.GetStatusText(clone.Status);
            clone.Arh = false;

            return clone;
        }
    }
}
