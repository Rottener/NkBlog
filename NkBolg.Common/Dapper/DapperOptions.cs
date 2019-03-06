using System;
using System.Collections.Generic;
using System.Text;

namespace NkBolg.Common.Dapper
{
    public class DapperOptions
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据库类型，默认为MySql
        /// </summary>
        public DatabaseType DatabaseType { get; set; } = DatabaseType.MySql;
    }
}
