﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace SAPINT.Function.Meta
{
    /// <summary>
    /// 从SAP服务器端获取RFC函数的信息，并把它们组成DataTable
    /// </summary>
    public class FunctionMetaAsDataTable
    {
        private DataTable import;
        private DataTable export;
        private DataTable changing;
        private DataTable tables;
        private DataTable exception;

        public bool Is_RFC = false;

        /// <summary>
        /// 复杂类型的定义，如结构或是表
        /// </summary>
        private Dictionary<String, DataTable> dataType;
        public Dictionary<String, DataTable> InputTable
        {
            get;
            set;
        }
        public FunctionMetaAsDataTable()
        {
            import = new DataTable();
            export = new DataTable();
            changing = new DataTable();
            tables = new DataTable();
            exception = new DataTable();

            dataType = new Dictionary<string, DataTable>();
            InputTable = new Dictionary<string, DataTable>();
        }
        public DataTable Import
        {
            get { return import; }
            set { import = value; }
        }

        public DataTable Export
        {
            get { return export; }
            set { export = value; }
        }

        public DataTable Changing
        {
            get { return changing; }
            set { changing = value; }
        }

        public DataTable Tables
        {
            get { return tables; }
            set { tables = value; }
        }

        public DataTable Exception
        {
            get { return exception; }
            set { exception = value; }
        }

        public Dictionary<String, DataTable> StructureDetail
        {
            get { return dataType; }
            set { dataType = value; }
        }

        /// <summary>
        /// 创建一个参数列表视图
        /// </summary>
        /// <returns></returns>
        public static DataTable ParameterDefinitionView()
        {
            DataTable dtTemplate = new DataTable();
            DataColumn dc = null;
            dc = new DataColumn(FuncFieldText.NAME, Type.GetType("System.String"));
            dc.Caption = "字段名";
            dtTemplate.Columns.Add(dc);
            dc = new DataColumn(FuncFieldText.DATATYPE, Type.GetType("System.String"));
            dc.Caption = "类型";
            dtTemplate.Columns.Add(dc);
            dc = new DataColumn(FuncFieldText.DATATYPENAME, Type.GetType("System.String"));
            dc.Caption = "结构名";
            dtTemplate.Columns.Add(dc);
            dc = new DataColumn(FuncFieldText.LENGTH, Type.GetType("System.Decimal"));
            dc.Caption = "长度";
            dtTemplate.Columns.Add(dc);
            dc = new DataColumn(FuncFieldText.DECIMALS, Type.GetType("System.String"));
            dc.Caption = "小数";
            dtTemplate.Columns.Add(dc);
            dc = new DataColumn(FuncFieldText.DEFAULTVALUE, Type.GetType("System.String"));
            dc.Caption = "默认值";
            dtTemplate.Columns.Add(dc);
            dc = new DataColumn(FuncFieldText.OPTIONAL, Type.GetType("System.Boolean"));
            dc.Caption = "是否可选";
            dtTemplate.Columns.Add(dc);
            dc = new DataColumn(FuncFieldText.DOCUMENTATION, Type.GetType("System.String"));
            dc.Caption = "文档";
            dtTemplate.Columns.Add(dc);
            return dtTemplate;
        }
        /// <summary>
        /// 结构定义的视图
        /// </summary>
        /// <returns></returns>
        public static DataTable StructDefinitionView()
        {
            DataTable dtTemplate = new DataTable();
            DataColumn dc = null;
            dc = new DataColumn(FuncFieldText.NAME, Type.GetType("System.String"));
            dc.Caption = "字段名";
            dtTemplate.Columns.Add(dc);
            dc = new DataColumn(FuncFieldText.DATATYPE, Type.GetType("System.String"));
            dc.Caption = "类型";
            dtTemplate.Columns.Add(dc);
            dc = new DataColumn(FuncFieldText.LENGTH, Type.GetType("System.Decimal"));
            dc.Caption = "长度";
            dtTemplate.Columns.Add(dc);
            dc = new DataColumn(FuncFieldText.DECIMALS, Type.GetType("System.String"));
            dc.Caption = "小数";
            dtTemplate.Columns.Add(dc);
            dc = new DataColumn(FuncFieldText.DOCUMENTATION, Type.GetType("System.String"));
            dc.Caption = "文档";
            dtTemplate.Columns.Add(dc);
            return dtTemplate;
        }
        /// <summary>
        /// 根据结构名称创建一个DataTable
        /// </summary>
        /// <param name="structureName"></param>
        /// <returns></returns>
        public DataTable CreateStructureInputTable(String structureName)
        {
            if (this.dataType.Count == 0)
            {
                return null;
            }
            else if (!dataType.Keys.Contains(structureName))
            {
                return null;
            }
            DataTable dtNew = new DataTable();
            DataTable dtDefinition = dataType[structureName];
            foreach (DataRow row in dtDefinition.Rows)
            {
                DataColumn dc = null;
                String colName = row[FuncFieldText.NAME].ToString();
                dc = new DataColumn(colName, Type.GetType("System.String"));
                dc.MaxLength = int.Parse(row[FuncFieldText.LENGTH].ToString());
                dc.Caption = row[FuncFieldText.DOCUMENTATION].ToString();
                dtNew.Columns.Add(dc);

            }
            return dtNew;
        }


    }
}
