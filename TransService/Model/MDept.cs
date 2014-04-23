using System;
using System.Collections.Generic;
using System.Text;
using SqlUtilities.Attributes;
using System.Data;

namespace TransService.Model
{
    /// <summary>
    /// 部门
    /// </summary>
    [Table(TableName = "XtDept")]
    public class MDept
    {
        private string deptCode;
        /// <summary>
        /// 部门编码
        /// </summary>
        [Column(ColumnName="Code",DbType=DbType.String,PrimaryKey=true)]
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }

        private string deptName;
        /// <summary>
        /// 部门名称
        /// </summary>
        [Column(ColumnName="DeptName",DbType=DbType.String)]
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }
    }
}
