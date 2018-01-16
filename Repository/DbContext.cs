using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MyConnect.Repository
{
    /// <summary>
    /// 数据持久化-可被继承
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbContext
    {
        private string conn = "connstr";
        private Repository.Database _db = null;
        private static DbContext instance = null;
        //private static Repository.Database _static_db = null;
        private bool _static = false;
        public DbContext()
        {

        }
        public DbContext(string connstr)
        {
            if (ConfigurationManager.ConnectionStrings[connstr] != null)
            {
                conn = connstr;
            }
        }
        public DbContext(string connstr,bool _static)
        {
            if (ConfigurationManager.ConnectionStrings[connstr] != null)
            {
                conn = connstr;
            }
            this._static = _static;
        }
        public static DbContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DbContext("connstr",true);
                }
                return instance;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Repository.Database GetDatabase()
        {
            //if(_static)
            //{
            //    if (_static_db == null)
            //    {
            //        _static_db = new Repository.Database(conn);
            //    }
            //    return _static_db;
            //}
            //if (_db == null)
            //{
            //    _db = new Repository.Database(conn);
            //}
            //return _db;
            return new Repository.Database(conn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connstr"></param>
        /// <returns></returns>
        private Repository.Database GetDatabase(string connstr)
        {
            if (_db == null)
            {
                _db = new Repository.Database(connstr);
            }
            return _db;
        }
        /// <summary>
        /// 返回一条记录的实体
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public T Get<T>(Sql sql)
        {
            using (var db = GetDatabase())
            {
                return db.FirstOrDefault<T>(sql);
            }
        }
        /// <summary>
        /// 根据查询参数返回一条记录的实体
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public T Get<T>(string sql, params object[] args)
        {
            using (var db = GetDatabase())
            {
                return db.FirstOrDefault<T>(sql, args);
            }
        }
        /// <summary>
        /// 根据主键ID 返回实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById<T>(int id)
        {
            using (var db = GetDatabase())
            {
                return db.SingleOrDefault<T>(id);
            }
        }
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert<T>(T entity)
        {
            using (var db = GetDatabase())
            {
                //仅测试使用，用户表进行加密
                //var tname = entity.GetType().Name;
                //if (tname == "tb_user")
                //{
                //    PropertyInfo property = entity.GetType().GetProperty("uname");
                //    var colvalue = property.GetValue(entity, null);      //获取指定属性的值
                //    var a = Security.DESEncrypt(colvalue.ToString());    //对表指定值进行加密
                //    property.SetValue(entity, a, null);                  //对指定属性进行赋值
                //}

                var i = db.Insert(entity);
                return int.Parse(i.ToString());
            }
        }
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update<T>(T entity)
        {
            using (var db = GetDatabase())
            {
                return db.Update(entity);
            }
        }
        /// <summary>
        /// 更新一条记录根据主键ID
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Update<T>(T entity, int keyid)
        {
            using (var db = GetDatabase())
            {
                return db.Update(entity, keyid);
            }
        }
        /// <summary>
        /// 根据指定字段更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public int Update<T>(T entity, IEnumerable<string> columns)
        {
            using (var db = GetDatabase())
            {
                return db.Update(entity, columns);
            }
        }
        /// <summary>
        /// 根据主键ID更新指定字段
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns"></param>
        /// <param name="keyid"></param>
        /// <returns></returns>
        public int Update<T>(T entity, IEnumerable<string> columns, int keyid)
        {
            using (var db = GetDatabase())
            {
                return db.Update(entity, keyid, columns);
            }
        }
        /// <summary>
        /// 根据实体删除一体记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete<T>(T entity)
        {
            using (var db = GetDatabase())
            {
                return db.Delete(entity);
            }
        }
        /// <summary>
        /// 根据主键ID删除一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete<T>(int id)
        {
            using (var db = GetDatabase())
            {
                return db.Delete<T>(id);
            }
        }
        /// <summary>
        /// 根据条件删除记录
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Delete<T>(Sql sql)
        {
            using (var db = GetDatabase())
            {
                return db.Delete<T>(sql);
            }
        }
        /// <summary>
        /// 根据条件参数删除记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int Delete<T>(string sql, params object[] args)
        {
            using (var db = GetDatabase())
            {
                return db.Delete<T>(sql, args);
            }
        }
        /// <summary>
        /// 返回默认所有记录
        /// </summary>
        /// <returns></returns>
        public List<T> List<T>()
        {
            return List<T>("");
        }
        /// <summary>
        /// 返回查询列表
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> List<T>(string sql)
        {
            using (var db = GetDatabase())
            {
                return db.Fetch<T>(sql);
            }
        }
        /// <summary>
        /// 根据查询参数返回列表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public List<T> List<T>(string sql, params object[] args)
        {
            using (var db = GetDatabase())
            {
                return db.Fetch<T>(sql, args);
            }
        }
        /// <summary>
        /// 根据查询条件返回指定分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public Page<T> List<T>(int page, int pagesize, string sql)
        {
            using (var db = GetDatabase())
            {
                return db.Page<T>(page, pagesize, sql);
            }
        }
        /// <summary>
        /// 根据查询参数返回指定分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Page<T> List<T>(int page, int pagesize, string sql, params object[] args)
        {
            using (var db = GetDatabase())
            {
                return db.Page<T>(page, pagesize, sql, args);
            }
        }

        /// <summary>
        /// 返回一个查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql, params object[] args)
        {
            DataTable dt = null;
            using (var db = GetDatabase())
            {
                dt = db.GetDataTable(sql, args);
            }
            return dt;
        }
        /// <summary>
        /// 返回指定数据库连接字符串的查询
        /// </summary>
        /// <param name="connstr"></param>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string connstr, string sql, params object[] args)
        {
            DataTable dt = null;
            using (var db = GetDatabase(connstr))
            {
                dt = db.GetDataTable(sql, args);
            }
            return dt;
        }
        /// <summary>
        /// 执行一条语句返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int Exec(string sql, params object[] args)
        {
            using (var db = GetDatabase())
            {
                return db.Execute(sql, args);
            }
        }
    }
}
