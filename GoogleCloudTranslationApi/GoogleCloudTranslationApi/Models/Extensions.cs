using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GoogleCloudTranslationApi.Models
{
    public static class Extensions
    {

        public static void fSyncronizeClass(this object _Class, object _BaseClass)
        {
            if (_BaseClass == null || _Class == null) return;

            PropertyInfo[] propertyInfos;
            propertyInfos = _BaseClass.GetType().GetProperties();

            foreach (PropertyInfo _Info in propertyInfos)
            {
                if (fPropertyExits(_Class, _Info.Name))
                    _Class.GetType().GetProperty(_Info.Name).SetValue(_Class, _BaseClass.GetType().GetProperty(_Info.Name).GetValue(_BaseClass, null), null);

            }
        }

        public static bool fPropertyExits(object _ClassName, string _PropertyName)
        {
            PropertyInfo _PInfo = _ClassName.GetType().GetProperty(_PropertyName);
            if (_PInfo == null) return false; else return true;
        }

        #region DATE TIME METHODS

        public static int fGetDateTimePeriod(this DateTime _Date)
        {
            if (_Date != null)
            {
                int _Month = _Date.Month;
                int _Year = _Date.Year;

                int _Donem = Convert.ToInt32(_Year.ToString() + _Month.ToString().PadLeft(2, '0'));

                return _Donem;
            }
            else return 0;
        }

        public static DateTime? fGetDateTimeValue(this object _obj)
        {
            if (_obj == DBNull.Value || _obj == null)
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(_obj);
            }
        }

        public static DateTime fGetDateTimeValueNotNull(this object _obj, DateTime _IsNullReturnValue)
        {
            if (_obj == DBNull.Value || _obj == null)
            {
                return _IsNullReturnValue;
            }
            else
            {
                return Convert.ToDateTime(_obj);
            }
        }

        public static string fGetDateTimeStrValueNotNull(this object _obj, string _IsNullReturnValue, string _DateTimeFormat)
        {
            if (_obj == DBNull.Value || _obj == null)
            {
                return _IsNullReturnValue;
            }
            else
            {
                return Convert.ToDateTime(_obj).ToString(_DateTimeFormat);
            }
        }

        public static string fGetDateToShortString(this object _obj)
        {
            if (_obj == DBNull.Value || _obj == null)
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(_obj).ToShortDateString();
            }
        }

        #endregion

        public static short fGetShortValue(this short? _Value)
        {
            short _RValue = 0;

            if (_Value == null)
            {
                return _RValue;
            }
            else
            {
                return (short)_Value;
            }
        }

        public static short fGetShortValueNotNull(this object _obj, short _IsNullReturnValue)
        {
            if (_obj == DBNull.Value || _obj == null)
            {
                return _IsNullReturnValue;
            }
            else
            {
                return Convert.ToInt16(_obj);
            }
        }

        public static string fGetErrDescription(this Exception Err)
        {
            string _Hata = "";

            if (Err is System.Data.SqlClient.SqlException)
            {
                switch (((System.Data.SqlClient.SqlException)Err).Number)
                {
                    case 2601:
                        _Hata = "Bu Kayıt Zaten Mevcut";
                        break;
                    case 547:
                        _Hata = "Hareket Görmüş Kayıtlar Silinemez.";
                        break;
                    default:
                        _Hata = ((System.Data.SqlClient.SqlException)Err).Message;
                        break;
                }
            }

            return _Hata;

        }

        public static Guid? fGetGuidValue(this object _obj)
        {
            if (_obj == DBNull.Value || _obj == null)
            {
                return null;
            }
            else
            {
                return new Guid(_obj.ToString());
            }
        }

        public static Guid fGetGuidValueNotNull(this object _obj)
        {
            if (_obj == DBNull.Value || _obj == null || _obj.ToString() == "")
            {
                return Guid.Empty;
            }
            else
            {
                return new Guid(_obj.ToString());
            }
        }

        public static bool fGetBoolValueNotNull(this object _obj, bool _IsNullReturnValue)
        {
            if (_obj == DBNull.Value || _obj == null)
            {
                return _IsNullReturnValue;
            }
            else
            {
                return Convert.ToBoolean(_obj);
            }
        }

        public static int fGetIntValueNotNull(this object _obj, int _IsNullReturnValue)
        {
            if (_obj == DBNull.Value || _obj == null)
            {
                return _IsNullReturnValue;
            }
            else
            {
                return Convert.ToInt32(_obj.ToString());
            }
        }

        public static int? fGetIntValue(this object _obj)
        {
            if (_obj == DBNull.Value || _obj == null)
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(_obj);
            }
        }

        public static int IsZero(this object _obj, int _IsZeroReturnValue)
        {
            if (_obj == DBNull.Value || _obj == null || _obj.fGetIntValueNotNull(0) == 0)
            {
                return _IsZeroReturnValue;
            }
            else return Convert.ToInt32(_obj.ToString());
        }

        #region BYTE ARRAY METHODS

        public static byte[] fGetByteArray(this object _obj)
        {
            if (_obj == null)
                return null;
            try
            {
                return (byte[])_obj;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        public static short ToShortValue(this bool _bool)
        {
            if (_bool) return 1; else return 0;
        }

        public static bool ToBoolValue(this short? _short)
        {
            if (_short == null || _short == 0) return false;
            else return true;
        }

        public static string fGetStringValueNotNull(this object _obj, string _IsNullReturnValue)
        {
            if (_obj == DBNull.Value || _obj == null)
            {
                return _IsNullReturnValue;
            }
            else
            {
                return _obj.ToString();
            }
        }

        public static string fGetStringValue(this object _obj)
        {
            if (_obj == DBNull.Value || _obj == null)
            {
                return null;
            }
            else
            {
                return _obj.ToString();
            }
        }

        public static string fRight(this string _Str, int _Count)
        {
            if (_Str == null || _Str.Length == 0) return "";

             return _Str.Substring(_Str.Length - _Count, _Count);
        }

        public static double? fGetDoubleValue(this object _obj)
        {
            if (_obj == DBNull.Value || _obj == null)
            {
                return null;
            }
            else
            {
                return Convert.ToDouble(_obj.ToString());
            }
        }

        public static double fGetDoubleValueNotNull(this object _obj, double _IsNullReturnValue)
        {
            if (_obj == DBNull.Value || _obj == null)
            {
                return _IsNullReturnValue;
            }
            else
            {
                return Convert.ToDouble(_obj.ToString());
            }
        }

        public static Int64 fGetInt64ValueNotNull(this object _obj, Int64 _IsNullReturnValue)
        {
            if (_obj == DBNull.Value || _obj == null)
            {
                return _IsNullReturnValue;
            }
            else
            {
                return Convert.ToInt64(_obj.ToString());
            }
        }

        public static bool fIsNumeric(this string _Str)
        {
            int _Num;
            return int.TryParse(_Str, out _Num);
        }

        public static bool Between(this object _obj, object _MinValue, object _MaxValue)
        {
            bool _Value = false;

            switch (_obj.GetType().ToString())
            {
                case "System.Int16":
                    if (Convert.ToInt16(_obj) >= Convert.ToInt16(_MinValue) && Convert.ToInt16(_obj) <= Convert.ToInt16(_MaxValue)) _Value = true;
                    else _Value = false;
                    break;
                case "System.Int32":
                    if (Convert.ToInt32(_obj) >= Convert.ToInt32(_MinValue) && Convert.ToInt32(_obj) <= Convert.ToInt32(_MaxValue)) _Value = true;
                    else _Value = false;
                    break;
                case "System.Int64":
                    if (Convert.ToInt64(_obj) >= Convert.ToInt64(_MinValue) && Convert.ToInt64(_obj) <= Convert.ToInt64(_MaxValue)) _Value = true;
                    else _Value = false;
                    break;
                case "System.Double":
                    if (Convert.ToDouble(_obj) >= Convert.ToDouble(_MinValue) && Convert.ToDouble(_obj) <= Convert.ToDouble(_MaxValue)) _Value = true;
                    else _Value = false;
                    break;
                case "System.DateTime":
                    if (Convert.ToDateTime(_obj) >= Convert.ToDateTime(_MinValue) && Convert.ToDateTime(_obj) <= Convert.ToDateTime(_MaxValue)) _Value = true;
                    else _Value = false;
                    break;
            }
            return _Value;
        }

        public static int GetIndex(this string _Content, string _SearchingValue, int _PositionCount)
        {
            int _Count = 0;
            int _Pos = 0;
            int _StartPos = 0;
            bool _Condition = true;

            while (_Condition)
            {
                _Count++;
                _Pos = _Content.IndexOf(_SearchingValue, _StartPos);

                if (_Count == _PositionCount || _Pos == -1) break;
                else
                {
                    _StartPos = _Pos + _SearchingValue.Length;
                }
            }

            return _Pos;
        }

        public static int GetIndexBackward(this string _Content, string _SearchingValue, int _StartIndex)
        {
            string _SearchContent = _Content.Substring(0, _StartIndex);

            return _SearchContent.LastIndexOf(_SearchingValue);
        }
        

        #region LINQ EXTENSIONS
        public static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> source, string propertyName, OrderDirection direction = OrderDirection.Ascending)
        {
            if (direction == OrderDirection.Ascending)
                return source.OrderBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null));
            else
                return source.OrderByDescending(x => x.GetType().GetProperty(propertyName).GetValue(x, null));
        }

        public enum OrderDirection
        {
            Ascending,
            Descending
        }

        /// <summary>
        /// Tek Field İçin Kullanım : var query = people.DistinctBy(p => p.Id);
        /// Çoklu Saha İçin Kullanım : var query = people.DistinctBy(p => new { p.Id, p.Name });
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        #endregion
    }
}