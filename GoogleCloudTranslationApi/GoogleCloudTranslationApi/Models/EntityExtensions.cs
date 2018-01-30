using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloudTranslationApi.Models
{
    public static class EntityExtensions
    {
        public static void fFillList<T>(this IList<T> _List, string _ConnStr, string _SQLStr, params object[] _Values)
        {
           

            IDataReader reader = null;
            SqlCommand cmd = new SqlCommand(_SQLStr);

            for (int i = 0; i < _Values.Length; i++)
            {
                cmd.Parameters.AddWithValue("@p" + (i + 1).ToString(), _Values[i]);
            }

            fAssignNull(cmd);

            using (SqlConnection conn = new SqlConnection(_ConnStr))
            {
                conn.Open();
                cmd.Connection = conn;
                reader = cmd.ExecuteReader();
                fReadToList(ref _List, ref reader);
            }

        }

        public static void fGetStringListFromTable<T>(this IList<string> _List, string _ConnStr, string _SQLStr, string _FieldName, params object[] _Values)
        {
           
            IDataReader reader = null;
            SqlCommand cmd = new SqlCommand(_SQLStr);

            try
            {
                for (int i = 0; i < _Values.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@p" + (i + 1).ToString(), _Values[i]);
                }

                fAssignNull(cmd);

                using (SqlConnection conn = new SqlConnection(_ConnStr))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    reader = cmd.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            _List.Add(reader.GetString(reader.GetOrdinal(_FieldName)));
                        }
                    }
                }
            }
            catch
            {
                _List = null;
            }
        }

        public static void fGetIntegerListFromTable<T>(this IList<int> _List, string _ConnStr, string _SQLStr, string _FieldName, params object[] _Values)
        {
           
            IDataReader reader = null;
            SqlCommand cmd = new SqlCommand(_SQLStr);

            try
            {
                for (int i = 0; i < _Values.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@p" + (i + 1).ToString(), _Values[i]);
                }

                fAssignNull(cmd);

                using (SqlConnection conn = new SqlConnection(_ConnStr))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    reader = cmd.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            _List.Add(reader.GetInt32(reader.GetOrdinal(_FieldName)));
                        }
                    }
                }
            }
            catch
            {
                _List = null;
            }
        }

        public static void fAssignNull(SqlCommand cmd)
        {
            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                if (object.Equals(cmd.Parameters[i].Value, null))
                {
                    cmd.Parameters[i].Value = DBNull.Value;
                }
            }
        }

        public static void fReadToList<T>(ref IList<T> _List, ref IDataReader reader)
        {

            string _FieldName;
            
            var _Obj = Activator.CreateInstance(typeof(T));

            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        _Obj = Activator.CreateInstance(typeof(T));




                        for (int _z = 0; _z < reader.FieldCount; _z++)
                        {
                            
                            _FieldName = reader.GetName(_z);
                            fSetObjectValues(ref _Obj, _FieldName, reader.GetValue(_z));
                        }


                        _List.GetType().GetMethod("Add").Invoke(_List, new object[] { _Obj });

                    }

                }
            }
            catch
            {
                _List = null;
            }

        }

        private static void fSetObjectValues(ref object _DestinationObject, string _PropertyName, object _Value)
        {

            if (_DestinationObject == null) return;
            if (_Value == null || _Value.GetType().ToString() == "System.DBNull") return;

            List<PropertyInfo> _PInfos;
            object _ChildObject;
            _PInfos = _DestinationObject.GetType().GetProperties().ToList();

            if (_PInfos != null && _PInfos.Count > 0)
            {
                for (int _i = 0; _i <= _PInfos.Count - 1; _i++)
                {

                    if (_DestinationObject.GetType().GetProperty(_PropertyName) != null &&
                        _DestinationObject.GetType().GetProperty(_PropertyName).SetMethod != null)
                    {
                        if (_PInfos[_i].GetValue(_DestinationObject, null) != null &&
                            !_PInfos[_i].GetValue(_DestinationObject, null).GetType().IsPublic &&
                            _PInfos[_i].GetValue(_DestinationObject, null).GetType().GetProperties().ToList() != null &&
                            _PInfos[_i].GetValue(_DestinationObject, null).GetType().GetProperties().ToList().Count > 0)
                        {
                            _ChildObject = _PInfos[_i].GetValue(_DestinationObject, null);

                            fSetObjectValues(ref _ChildObject, _PropertyName, _Value);

                        }

                        if (_DestinationObject.GetType().GetProperty(_PropertyName) != null)
                        {
                            _DestinationObject.GetType().GetProperty(_PropertyName).SetValue(_DestinationObject, _Value);
                            break;
                        }
                    }
                    else
                    {
                        _ChildObject = _PInfos[_i].GetValue(_DestinationObject, null);

                        if (_ChildObject != null && !_ChildObject.GetType().IsPublic && _ChildObject.GetType().GetProperties().ToList().Count > 0)
                            fSetObjectValues(ref _ChildObject, _PropertyName, _Value);
                    }
                }
            }
        }

        public static bool IsAnonymousType(this Type type)
        {
            Debug.Assert(type != null, "Type should not be null");

            // HACK: The only way to detect anonymous types right now.
            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                       && type.IsGenericType && type.Name.Contains("AnonymousType")
                       && (type.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase) ||
                           type.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase))
                       && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }

        //////////////////////////////////////////////////////////////////
        


    }
}
