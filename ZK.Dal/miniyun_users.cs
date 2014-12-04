/**  智客知识管理平台。
* miniyun_users.cs
*
* 功 能： N/A
* 类 名： miniyun_users
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/12 8:26:33   N/A    初版
*
* Copyright (c) 2012 BeiJing HaoLian Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：北京浩联教育科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using ZK.IDAL;
using ZK.DBUtility;//Please add references
namespace ZK.Dal
{
    /// <summary>
    /// 数据访问类:miniyun_users
    /// </summary>
    public partial class miniyun_users : Iminiyun_users
    {
        public miniyun_users()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQL.GetMaxID("id", "miniyun_users");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from miniyun_users");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
            parameters[0].Value = id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ZK.Model.miniyun_users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into miniyun_users(");
            strSql.Append("user_uuid,user_name,user_pass,user_status,salt,created_at,updated_at)");
            strSql.Append(" values (");
            strSql.Append("@user_uuid,@user_name,@user_pass,@user_status,@salt,@created_at,@updated_at)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_uuid", MySqlDbType.VarChar,32),
					new MySqlParameter("@user_name", MySqlDbType.VarChar,255),
					new MySqlParameter("@user_pass", MySqlDbType.VarChar,255),
					new MySqlParameter("@user_status", MySqlDbType.Int64,1),
					new MySqlParameter("@salt", MySqlDbType.VarChar,6),
					new MySqlParameter("@created_at", MySqlDbType.DateTime),
					new MySqlParameter("@updated_at", MySqlDbType.DateTime)};
            parameters[0].Value = model.user_uuid;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.user_pass;
            parameters[3].Value = model.user_status;
            parameters[4].Value = model.salt;
            parameters[5].Value = model.created_at;
            parameters[6].Value = model.updated_at;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZK.Model.miniyun_users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update miniyun_users set ");
            strSql.Append("user_uuid=@user_uuid,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("user_pass=@user_pass,");
            strSql.Append("user_status=@user_status,");
            strSql.Append("salt=@salt,");
            strSql.Append("created_at=@created_at,");
            strSql.Append("updated_at=@updated_at");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_uuid", MySqlDbType.VarChar,32),
					new MySqlParameter("@user_name", MySqlDbType.VarChar,255),
					new MySqlParameter("@user_pass", MySqlDbType.VarChar,255),
					new MySqlParameter("@user_status", MySqlDbType.Int16,1),
					new MySqlParameter("@salt", MySqlDbType.VarChar,6),
					new MySqlParameter("@created_at", MySqlDbType.DateTime),
					new MySqlParameter("@updated_at", MySqlDbType.DateTime),
					new MySqlParameter("@id", MySqlDbType.Int32,11)};
            parameters[0].Value = model.user_uuid;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.user_pass;
            parameters[3].Value = model.user_status;
            parameters[4].Value = model.salt;
            parameters[5].Value = model.created_at;
            parameters[6].Value = model.updated_at;
            parameters[7].Value = model.id;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from miniyun_users ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
            parameters[0].Value = id;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from miniyun_users ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZK.Model.miniyun_users GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_uuid,user_name,user_pass,user_status,salt,created_at,updated_at from miniyun_users ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
            parameters[0].Value = id;

            ZK.Model.miniyun_users model = new ZK.Model.miniyun_users();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_uuid"] != null && ds.Tables[0].Rows[0]["user_uuid"].ToString() != "")
                {
                    model.user_uuid = ds.Tables[0].Rows[0]["user_uuid"].ToString();
                }
                if (ds.Tables[0].Rows[0]["user_name"] != null && ds.Tables[0].Rows[0]["user_name"].ToString() != "")
                {
                    model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["user_pass"] != null && ds.Tables[0].Rows[0]["user_pass"].ToString() != "")
                {
                    model.user_pass = ds.Tables[0].Rows[0]["user_pass"].ToString();
                }
                if (ds.Tables[0].Rows[0]["user_status"] != null && ds.Tables[0].Rows[0]["user_status"].ToString() != "")
                {
                    if (ds.Tables[0].Rows[0]["user_status"].ToString().ToLower() == "true")
                        model.user_status = 1;
                    else
                        model.user_status = 0;
                }
                if (ds.Tables[0].Rows[0]["salt"] != null && ds.Tables[0].Rows[0]["salt"].ToString() != "")
                {
                    model.salt = ds.Tables[0].Rows[0]["salt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["created_at"] != null && ds.Tables[0].Rows[0]["created_at"].ToString() != "")
                {
                    model.created_at = DateTime.Parse(ds.Tables[0].Rows[0]["created_at"].ToString());
                }
                if (ds.Tables[0].Rows[0]["updated_at"] != null && ds.Tables[0].Rows[0]["updated_at"].ToString() != "")
                {
                    model.updated_at = DateTime.Parse(ds.Tables[0].Rows[0]["updated_at"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_uuid,user_name,user_pass,user_status,salt,created_at,updated_at ");
            strSql.Append(" FROM miniyun_users ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM miniyun_users ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from miniyun_users T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize, int PageIndex, string strWhere)
        {
            //MySqlParameter[] parameters = {
            //        new MySqlParameter("@select_list", MySqlDbType.VarChar, 1000),
            //        new MySqlParameter("@table_name", MySqlDbType.VarChar, 1000),
            //        new MySqlParameter("@where", MySqlDbType.VarChar, 1000),
            //        new MySqlParameter("@primary_key", MySqlDbType.VarChar, 200),
            //        new MySqlParameter("@order_by", MySqlDbType.VarChar, 200),
            //        new MySqlParameter("@page_size", MySqlDbType.Int32),
            //        new MySqlParameter("@page_index", MySqlDbType.Int32),
            //        new MySqlParameter("@bl_page", MySqlDbType.Int32),
            //        };
            //parameters[0].Value = "*";
            //parameters[1].Value = "miniyun_users";
            //parameters[2].Value = strWhere;
            //parameters[3].Value = "id";
            //parameters[4].Value = "id desc";
            //parameters[5].Value = PageSize;
            //parameters[6].Value = PageIndex;
            //parameters[7].Value = "1";
            //return DbHelperMySQL.RunProcedure("Common_PageList",parameters,"ds");
            return new DataSet();
        }

        #endregion  Method
    }
}

