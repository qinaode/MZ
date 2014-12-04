/**  智客知识管理平台。
* USERS.cs
*
* 功 能： N/A
* 类 名： USERS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:39   N/A    初版
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
using System.Data.SqlClient;
using ZK.IDAL;
using ZK.DBUtility;//Please add references
namespace ZK.Dal
{
	/// <summary>
	/// 数据访问类:USERS
	/// </summary>
	public partial class USERS:IUSERS
	{
		public USERS()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("USERID", "USERS"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int USERID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from USERS");
			strSql.Append(" where USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = USERID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.USERS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into USERS(");
			strSql.Append("USERID,USERNAME,USERTYPE,CANFINDBYPUBLICUSERS,NICKNAME,SIGNATURE,ACTUALNAME,SEX,AGE,BIRTH_YEAR,BIRTH_MONTH,BIRTH_DAY,COUNTRY,PROVINCE,CITY,AREA,ADDRESS,TELEPHONE,MOBILE,FAX,QQ,MSN,EMAIL,HOMEPAGE,DEPARTID,DEPARTNAME,JOBTITLE,JOBNUMBER,INTRODUCTION,FACEFILE,PHOTOFILE,LOGINSTATUS,LOGINSTATUSTEXT,LOGINTIMES,LASTLOGINTIME,CLIENTIPADDR,CLIENTLOCATION,LASTCLIENTIPADDR,LASTCLIENTLOCATION,HASCAMERA,HASMIC,VIP,ONLINELEVEL,INTEGRAL,PWD,SALT,TOKEN,TOKENUPDATETIME,USERLOCK,ONLYFINDMEBYID,JOINSETTING,JOINQUESTION,JOINANSWER,LASTRECVSYSMSGS,MODIFYTIME,CREATETIME)");
			strSql.Append(" values (");
			strSql.Append("@USERID,@USERNAME,@USERTYPE,@CANFINDBYPUBLICUSERS,@NICKNAME,@SIGNATURE,@ACTUALNAME,@SEX,@AGE,@BIRTH_YEAR,@BIRTH_MONTH,@BIRTH_DAY,@COUNTRY,@PROVINCE,@CITY,@AREA,@ADDRESS,@TELEPHONE,@MOBILE,@FAX,@QQ,@MSN,@EMAIL,@HOMEPAGE,@DEPARTID,@DEPARTNAME,@JOBTITLE,@JOBNUMBER,@INTRODUCTION,@FACEFILE,@PHOTOFILE,@LOGINSTATUS,@LOGINSTATUSTEXT,@LOGINTIMES,@LASTLOGINTIME,@CLIENTIPADDR,@CLIENTLOCATION,@LASTCLIENTIPADDR,@LASTCLIENTLOCATION,@HASCAMERA,@HASMIC,@VIP,@ONLINELEVEL,@INTEGRAL,@PWD,@SALT,@TOKEN,@TOKENUPDATETIME,@USERLOCK,@ONLYFINDMEBYID,@JOINSETTING,@JOINQUESTION,@JOINANSWER,@LASTRECVSYSMSGS,@MODIFYTIME,@CREATETIME)");
			SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@USERNAME", SqlDbType.NVarChar,50),
					new SqlParameter("@USERTYPE", SqlDbType.SmallInt,2),
					new SqlParameter("@CANFINDBYPUBLICUSERS", SqlDbType.SmallInt,2),
					new SqlParameter("@NICKNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@SIGNATURE", SqlDbType.NVarChar,255),
					new SqlParameter("@ACTUALNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@SEX", SqlDbType.SmallInt,2),
					new SqlParameter("@AGE", SqlDbType.SmallInt,2),
					new SqlParameter("@BIRTH_YEAR", SqlDbType.SmallInt,2),
					new SqlParameter("@BIRTH_MONTH", SqlDbType.SmallInt,2),
					new SqlParameter("@BIRTH_DAY", SqlDbType.SmallInt,2),
					new SqlParameter("@COUNTRY", SqlDbType.SmallInt,2),
					new SqlParameter("@PROVINCE", SqlDbType.SmallInt,2),
					new SqlParameter("@CITY", SqlDbType.SmallInt,2),
					new SqlParameter("@AREA", SqlDbType.SmallInt,2),
					new SqlParameter("@ADDRESS", SqlDbType.NVarChar,50),
					new SqlParameter("@TELEPHONE", SqlDbType.NVarChar,20),
					new SqlParameter("@MOBILE", SqlDbType.NVarChar,20),
					new SqlParameter("@FAX", SqlDbType.NVarChar,20),
					new SqlParameter("@QQ", SqlDbType.NVarChar,50),
					new SqlParameter("@MSN", SqlDbType.NVarChar,50),
					new SqlParameter("@EMAIL", SqlDbType.NVarChar,50),
					new SqlParameter("@HOMEPAGE", SqlDbType.NVarChar,80),
					new SqlParameter("@DEPARTID", SqlDbType.Int,4),
					new SqlParameter("@DEPARTNAME", SqlDbType.NVarChar,50),
					new SqlParameter("@JOBTITLE", SqlDbType.NVarChar,50),
					new SqlParameter("@JOBNUMBER", SqlDbType.NVarChar,20),
					new SqlParameter("@INTRODUCTION", SqlDbType.NVarChar,255),
					new SqlParameter("@FACEFILE", SqlDbType.NVarChar,255),
					new SqlParameter("@PHOTOFILE", SqlDbType.NVarChar,255),
					new SqlParameter("@LOGINSTATUS", SqlDbType.SmallInt,2),
					new SqlParameter("@LOGINSTATUSTEXT", SqlDbType.NVarChar,50),
					new SqlParameter("@LOGINTIMES", SqlDbType.BigInt,8),
					new SqlParameter("@LASTLOGINTIME", SqlDbType.DateTime),
					new SqlParameter("@CLIENTIPADDR", SqlDbType.NVarChar,50),
					new SqlParameter("@CLIENTLOCATION", SqlDbType.NVarChar,50),
					new SqlParameter("@LASTCLIENTIPADDR", SqlDbType.NVarChar,50),
					new SqlParameter("@LASTCLIENTLOCATION", SqlDbType.NVarChar,50),
					new SqlParameter("@HASCAMERA", SqlDbType.SmallInt,2),
					new SqlParameter("@HASMIC", SqlDbType.SmallInt,2),
					new SqlParameter("@VIP", SqlDbType.SmallInt,2),
					new SqlParameter("@ONLINELEVEL", SqlDbType.Int,4),
					new SqlParameter("@INTEGRAL", SqlDbType.Int,4),
					new SqlParameter("@PWD", SqlDbType.NVarChar,255),
					new SqlParameter("@SALT", SqlDbType.NVarChar,6),
					new SqlParameter("@TOKEN", SqlDbType.NVarChar,255),
					new SqlParameter("@TOKENUPDATETIME", SqlDbType.DateTime),
					new SqlParameter("@USERLOCK", SqlDbType.SmallInt,2),
					new SqlParameter("@ONLYFINDMEBYID", SqlDbType.SmallInt,2),
					new SqlParameter("@JOINSETTING", SqlDbType.SmallInt,2),
					new SqlParameter("@JOINQUESTION", SqlDbType.NVarChar,50),
					new SqlParameter("@JOINANSWER", SqlDbType.NVarChar,50),
					new SqlParameter("@LASTRECVSYSMSGS", SqlDbType.DateTime),
					new SqlParameter("@MODIFYTIME", SqlDbType.DateTime),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime)};
			parameters[0].Value = model.USERID;
			parameters[1].Value = model.USERNAME;
			parameters[2].Value = model.USERTYPE;
			parameters[3].Value = model.CANFINDBYPUBLICUSERS;
			parameters[4].Value = model.NICKNAME;
			parameters[5].Value = model.SIGNATURE;
			parameters[6].Value = model.ACTUALNAME;
			parameters[7].Value = model.SEX;
			parameters[8].Value = model.AGE;
			parameters[9].Value = model.BIRTH_YEAR;
			parameters[10].Value = model.BIRTH_MONTH;
			parameters[11].Value = model.BIRTH_DAY;
			parameters[12].Value = model.COUNTRY;
			parameters[13].Value = model.PROVINCE;
			parameters[14].Value = model.CITY;
			parameters[15].Value = model.AREA;
			parameters[16].Value = model.ADDRESS;
			parameters[17].Value = model.TELEPHONE;
			parameters[18].Value = model.MOBILE;
			parameters[19].Value = model.FAX;
			parameters[20].Value = model.QQ;
			parameters[21].Value = model.MSN;
			parameters[22].Value = model.EMAIL;
			parameters[23].Value = model.HOMEPAGE;
			parameters[24].Value = model.DEPARTID;
			parameters[25].Value = model.DEPARTNAME;
			parameters[26].Value = model.JOBTITLE;
			parameters[27].Value = model.JOBNUMBER;
			parameters[28].Value = model.INTRODUCTION;
			parameters[29].Value = model.FACEFILE;
			parameters[30].Value = model.PHOTOFILE;
			parameters[31].Value = model.LOGINSTATUS;
			parameters[32].Value = model.LOGINSTATUSTEXT;
			parameters[33].Value = model.LOGINTIMES;
			parameters[34].Value = model.LASTLOGINTIME;
			parameters[35].Value = model.CLIENTIPADDR;
			parameters[36].Value = model.CLIENTLOCATION;
			parameters[37].Value = model.LASTCLIENTIPADDR;
			parameters[38].Value = model.LASTCLIENTLOCATION;
			parameters[39].Value = model.HASCAMERA;
			parameters[40].Value = model.HASMIC;
			parameters[41].Value = model.VIP;
			parameters[42].Value = model.ONLINELEVEL;
			parameters[43].Value = model.INTEGRAL;
			parameters[44].Value = model.PWD;
			parameters[45].Value = model.SALT;
			parameters[46].Value = model.TOKEN;
			parameters[47].Value = model.TOKENUPDATETIME;
			parameters[48].Value = model.USERLOCK;
			parameters[49].Value = model.ONLYFINDMEBYID;
			parameters[50].Value = model.JOINSETTING;
			parameters[51].Value = model.JOINQUESTION;
			parameters[52].Value = model.JOINANSWER;
			parameters[53].Value = model.LASTRECVSYSMSGS;
			parameters[54].Value = model.MODIFYTIME;
			parameters[55].Value = model.CREATETIME;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Update(ZK.Model.USERS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update USERS set ");
			strSql.Append("USERNAME=@USERNAME,");
			strSql.Append("USERTYPE=@USERTYPE,");
			strSql.Append("CANFINDBYPUBLICUSERS=@CANFINDBYPUBLICUSERS,");
			strSql.Append("NICKNAME=@NICKNAME,");
			strSql.Append("SIGNATURE=@SIGNATURE,");
			strSql.Append("ACTUALNAME=@ACTUALNAME,");
			strSql.Append("SEX=@SEX,");
			strSql.Append("AGE=@AGE,");
			strSql.Append("BIRTH_YEAR=@BIRTH_YEAR,");
			strSql.Append("BIRTH_MONTH=@BIRTH_MONTH,");
			strSql.Append("BIRTH_DAY=@BIRTH_DAY,");
			strSql.Append("COUNTRY=@COUNTRY,");
			strSql.Append("PROVINCE=@PROVINCE,");
			strSql.Append("CITY=@CITY,");
			strSql.Append("AREA=@AREA,");
			strSql.Append("ADDRESS=@ADDRESS,");
			strSql.Append("TELEPHONE=@TELEPHONE,");
			strSql.Append("MOBILE=@MOBILE,");
			strSql.Append("FAX=@FAX,");
			strSql.Append("QQ=@QQ,");
			strSql.Append("MSN=@MSN,");
			strSql.Append("EMAIL=@EMAIL,");
			strSql.Append("HOMEPAGE=@HOMEPAGE,");
			strSql.Append("DEPARTID=@DEPARTID,");
			strSql.Append("DEPARTNAME=@DEPARTNAME,");
			strSql.Append("JOBTITLE=@JOBTITLE,");
			strSql.Append("JOBNUMBER=@JOBNUMBER,");
			strSql.Append("INTRODUCTION=@INTRODUCTION,");
			strSql.Append("FACEFILE=@FACEFILE,");
			strSql.Append("PHOTOFILE=@PHOTOFILE,");
			strSql.Append("LOGINSTATUS=@LOGINSTATUS,");
			strSql.Append("LOGINSTATUSTEXT=@LOGINSTATUSTEXT,");
			strSql.Append("LOGINTIMES=@LOGINTIMES,");
			strSql.Append("LASTLOGINTIME=@LASTLOGINTIME,");
			strSql.Append("CLIENTIPADDR=@CLIENTIPADDR,");
			strSql.Append("CLIENTLOCATION=@CLIENTLOCATION,");
			strSql.Append("LASTCLIENTIPADDR=@LASTCLIENTIPADDR,");
			strSql.Append("LASTCLIENTLOCATION=@LASTCLIENTLOCATION,");
			strSql.Append("HASCAMERA=@HASCAMERA,");
			strSql.Append("HASMIC=@HASMIC,");
			strSql.Append("VIP=@VIP,");
			strSql.Append("ONLINELEVEL=@ONLINELEVEL,");
			strSql.Append("INTEGRAL=@INTEGRAL,");
			strSql.Append("PWD=@PWD,");
			strSql.Append("SALT=@SALT,");
			strSql.Append("TOKEN=@TOKEN,");
			strSql.Append("TOKENUPDATETIME=@TOKENUPDATETIME,");
			strSql.Append("USERLOCK=@USERLOCK,");
			strSql.Append("ONLYFINDMEBYID=@ONLYFINDMEBYID,");
			strSql.Append("JOINSETTING=@JOINSETTING,");
			strSql.Append("JOINQUESTION=@JOINQUESTION,");
			strSql.Append("JOINANSWER=@JOINANSWER,");
			strSql.Append("LASTRECVSYSMSGS=@LASTRECVSYSMSGS,");
			strSql.Append("MODIFYTIME=@MODIFYTIME,");
			strSql.Append("CREATETIME=@CREATETIME");
			strSql.Append(" where USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@USERNAME", SqlDbType.NVarChar,50),
					new SqlParameter("@USERTYPE", SqlDbType.SmallInt,2),
					new SqlParameter("@CANFINDBYPUBLICUSERS", SqlDbType.SmallInt,2),
					new SqlParameter("@NICKNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@SIGNATURE", SqlDbType.NVarChar,255),
					new SqlParameter("@ACTUALNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@SEX", SqlDbType.SmallInt,2),
					new SqlParameter("@AGE", SqlDbType.SmallInt,2),
					new SqlParameter("@BIRTH_YEAR", SqlDbType.SmallInt,2),
					new SqlParameter("@BIRTH_MONTH", SqlDbType.SmallInt,2),
					new SqlParameter("@BIRTH_DAY", SqlDbType.SmallInt,2),
					new SqlParameter("@COUNTRY", SqlDbType.SmallInt,2),
					new SqlParameter("@PROVINCE", SqlDbType.SmallInt,2),
					new SqlParameter("@CITY", SqlDbType.SmallInt,2),
					new SqlParameter("@AREA", SqlDbType.SmallInt,2),
					new SqlParameter("@ADDRESS", SqlDbType.NVarChar,50),
					new SqlParameter("@TELEPHONE", SqlDbType.NVarChar,20),
					new SqlParameter("@MOBILE", SqlDbType.NVarChar,20),
					new SqlParameter("@FAX", SqlDbType.NVarChar,20),
					new SqlParameter("@QQ", SqlDbType.NVarChar,50),
					new SqlParameter("@MSN", SqlDbType.NVarChar,50),
					new SqlParameter("@EMAIL", SqlDbType.NVarChar,50),
					new SqlParameter("@HOMEPAGE", SqlDbType.NVarChar,80),
					new SqlParameter("@DEPARTID", SqlDbType.Int,4),
					new SqlParameter("@DEPARTNAME", SqlDbType.NVarChar,50),
					new SqlParameter("@JOBTITLE", SqlDbType.NVarChar,50),
					new SqlParameter("@JOBNUMBER", SqlDbType.NVarChar,20),
					new SqlParameter("@INTRODUCTION", SqlDbType.NVarChar,255),
					new SqlParameter("@FACEFILE", SqlDbType.NVarChar,255),
					new SqlParameter("@PHOTOFILE", SqlDbType.NVarChar,255),
					new SqlParameter("@LOGINSTATUS", SqlDbType.SmallInt,2),
					new SqlParameter("@LOGINSTATUSTEXT", SqlDbType.NVarChar,50),
					new SqlParameter("@LOGINTIMES", SqlDbType.BigInt,8),
					new SqlParameter("@LASTLOGINTIME", SqlDbType.DateTime),
					new SqlParameter("@CLIENTIPADDR", SqlDbType.NVarChar,50),
					new SqlParameter("@CLIENTLOCATION", SqlDbType.NVarChar,50),
					new SqlParameter("@LASTCLIENTIPADDR", SqlDbType.NVarChar,50),
					new SqlParameter("@LASTCLIENTLOCATION", SqlDbType.NVarChar,50),
					new SqlParameter("@HASCAMERA", SqlDbType.SmallInt,2),
					new SqlParameter("@HASMIC", SqlDbType.SmallInt,2),
					new SqlParameter("@VIP", SqlDbType.SmallInt,2),
					new SqlParameter("@ONLINELEVEL", SqlDbType.Int,4),
					new SqlParameter("@INTEGRAL", SqlDbType.Int,4),
					new SqlParameter("@PWD", SqlDbType.NVarChar,255),
					new SqlParameter("@SALT", SqlDbType.NVarChar,6),
					new SqlParameter("@TOKEN", SqlDbType.NVarChar,255),
					new SqlParameter("@TOKENUPDATETIME", SqlDbType.DateTime),
					new SqlParameter("@USERLOCK", SqlDbType.SmallInt,2),
					new SqlParameter("@ONLYFINDMEBYID", SqlDbType.SmallInt,2),
					new SqlParameter("@JOINSETTING", SqlDbType.SmallInt,2),
					new SqlParameter("@JOINQUESTION", SqlDbType.NVarChar,50),
					new SqlParameter("@JOINANSWER", SqlDbType.NVarChar,50),
					new SqlParameter("@LASTRECVSYSMSGS", SqlDbType.DateTime),
					new SqlParameter("@MODIFYTIME", SqlDbType.DateTime),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime),
					new SqlParameter("@USERID", SqlDbType.Int,4)};
			parameters[0].Value = model.USERNAME;
			parameters[1].Value = model.USERTYPE;
			parameters[2].Value = model.CANFINDBYPUBLICUSERS;
			parameters[3].Value = model.NICKNAME;
			parameters[4].Value = model.SIGNATURE;
			parameters[5].Value = model.ACTUALNAME;
			parameters[6].Value = model.SEX;
			parameters[7].Value = model.AGE;
			parameters[8].Value = model.BIRTH_YEAR;
			parameters[9].Value = model.BIRTH_MONTH;
			parameters[10].Value = model.BIRTH_DAY;
			parameters[11].Value = model.COUNTRY;
			parameters[12].Value = model.PROVINCE;
			parameters[13].Value = model.CITY;
			parameters[14].Value = model.AREA;
			parameters[15].Value = model.ADDRESS;
			parameters[16].Value = model.TELEPHONE;
			parameters[17].Value = model.MOBILE;
			parameters[18].Value = model.FAX;
			parameters[19].Value = model.QQ;
			parameters[20].Value = model.MSN;
			parameters[21].Value = model.EMAIL;
			parameters[22].Value = model.HOMEPAGE;
			parameters[23].Value = model.DEPARTID;
			parameters[24].Value = model.DEPARTNAME;
			parameters[25].Value = model.JOBTITLE;
			parameters[26].Value = model.JOBNUMBER;
			parameters[27].Value = model.INTRODUCTION;
			parameters[28].Value = model.FACEFILE;
			parameters[29].Value = model.PHOTOFILE;
			parameters[30].Value = model.LOGINSTATUS;
			parameters[31].Value = model.LOGINSTATUSTEXT;
			parameters[32].Value = model.LOGINTIMES;
			parameters[33].Value = model.LASTLOGINTIME;
			parameters[34].Value = model.CLIENTIPADDR;
			parameters[35].Value = model.CLIENTLOCATION;
			parameters[36].Value = model.LASTCLIENTIPADDR;
			parameters[37].Value = model.LASTCLIENTLOCATION;
			parameters[38].Value = model.HASCAMERA;
			parameters[39].Value = model.HASMIC;
			parameters[40].Value = model.VIP;
			parameters[41].Value = model.ONLINELEVEL;
			parameters[42].Value = model.INTEGRAL;
			parameters[43].Value = model.PWD;
			parameters[44].Value = model.SALT;
			parameters[45].Value = model.TOKEN;
			parameters[46].Value = model.TOKENUPDATETIME;
			parameters[47].Value = model.USERLOCK;
			parameters[48].Value = model.ONLYFINDMEBYID;
			parameters[49].Value = model.JOINSETTING;
			parameters[50].Value = model.JOINQUESTION;
			parameters[51].Value = model.JOINANSWER;
			parameters[52].Value = model.LASTRECVSYSMSGS;
			parameters[53].Value = model.MODIFYTIME;
			parameters[54].Value = model.CREATETIME;
			parameters[55].Value = model.USERID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(int USERID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from USERS ");
			strSql.Append(" where USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = USERID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string USERIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from USERS ");
			strSql.Append(" where USERID in ("+USERIDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public ZK.Model.USERS GetModel(int USERID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 USERID,USERNAME,USERTYPE,CANFINDBYPUBLICUSERS,NICKNAME,SIGNATURE,ACTUALNAME,SEX,AGE,BIRTH_YEAR,BIRTH_MONTH,BIRTH_DAY,COUNTRY,PROVINCE,CITY,AREA,ADDRESS,TELEPHONE,MOBILE,FAX,QQ,MSN,EMAIL,HOMEPAGE,DEPARTID,DEPARTNAME,JOBTITLE,JOBNUMBER,INTRODUCTION,FACEFILE,PHOTOFILE,LOGINSTATUS,LOGINSTATUSTEXT,LOGINTIMES,LASTLOGINTIME,CLIENTIPADDR,CLIENTLOCATION,LASTCLIENTIPADDR,LASTCLIENTLOCATION,HASCAMERA,HASMIC,VIP,ONLINELEVEL,INTEGRAL,PWD,SALT,TOKEN,TOKENUPDATETIME,USERLOCK,ONLYFINDMEBYID,JOINSETTING,JOINQUESTION,JOINANSWER,LASTRECVSYSMSGS,MODIFYTIME,CREATETIME from USERS ");
			strSql.Append(" where USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = USERID;

			ZK.Model.USERS model=new ZK.Model.USERS();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["USERID"]!=null && ds.Tables[0].Rows[0]["USERID"].ToString()!="")
				{
					model.USERID=int.Parse(ds.Tables[0].Rows[0]["USERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["USERNAME"]!=null && ds.Tables[0].Rows[0]["USERNAME"].ToString()!="")
				{
					model.USERNAME=ds.Tables[0].Rows[0]["USERNAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["USERTYPE"]!=null && ds.Tables[0].Rows[0]["USERTYPE"].ToString()!="")
				{
					model.USERTYPE=int.Parse(ds.Tables[0].Rows[0]["USERTYPE"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CANFINDBYPUBLICUSERS"]!=null && ds.Tables[0].Rows[0]["CANFINDBYPUBLICUSERS"].ToString()!="")
				{
					model.CANFINDBYPUBLICUSERS=int.Parse(ds.Tables[0].Rows[0]["CANFINDBYPUBLICUSERS"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NICKNAME"]!=null && ds.Tables[0].Rows[0]["NICKNAME"].ToString()!="")
				{
					model.NICKNAME=ds.Tables[0].Rows[0]["NICKNAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SIGNATURE"]!=null && ds.Tables[0].Rows[0]["SIGNATURE"].ToString()!="")
				{
					model.SIGNATURE=ds.Tables[0].Rows[0]["SIGNATURE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ACTUALNAME"]!=null && ds.Tables[0].Rows[0]["ACTUALNAME"].ToString()!="")
				{
					model.ACTUALNAME=ds.Tables[0].Rows[0]["ACTUALNAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SEX"]!=null && ds.Tables[0].Rows[0]["SEX"].ToString()!="")
				{
					model.SEX=int.Parse(ds.Tables[0].Rows[0]["SEX"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AGE"]!=null && ds.Tables[0].Rows[0]["AGE"].ToString()!="")
				{
					model.AGE=int.Parse(ds.Tables[0].Rows[0]["AGE"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BIRTH_YEAR"]!=null && ds.Tables[0].Rows[0]["BIRTH_YEAR"].ToString()!="")
				{
					model.BIRTH_YEAR=int.Parse(ds.Tables[0].Rows[0]["BIRTH_YEAR"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BIRTH_MONTH"]!=null && ds.Tables[0].Rows[0]["BIRTH_MONTH"].ToString()!="")
				{
					model.BIRTH_MONTH=int.Parse(ds.Tables[0].Rows[0]["BIRTH_MONTH"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BIRTH_DAY"]!=null && ds.Tables[0].Rows[0]["BIRTH_DAY"].ToString()!="")
				{
					model.BIRTH_DAY=int.Parse(ds.Tables[0].Rows[0]["BIRTH_DAY"].ToString());
				}
				if(ds.Tables[0].Rows[0]["COUNTRY"]!=null && ds.Tables[0].Rows[0]["COUNTRY"].ToString()!="")
				{
					model.COUNTRY=int.Parse(ds.Tables[0].Rows[0]["COUNTRY"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PROVINCE"]!=null && ds.Tables[0].Rows[0]["PROVINCE"].ToString()!="")
				{
					model.PROVINCE=int.Parse(ds.Tables[0].Rows[0]["PROVINCE"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CITY"]!=null && ds.Tables[0].Rows[0]["CITY"].ToString()!="")
				{
					model.CITY=int.Parse(ds.Tables[0].Rows[0]["CITY"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AREA"]!=null && ds.Tables[0].Rows[0]["AREA"].ToString()!="")
				{
					model.AREA=int.Parse(ds.Tables[0].Rows[0]["AREA"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ADDRESS"]!=null && ds.Tables[0].Rows[0]["ADDRESS"].ToString()!="")
				{
					model.ADDRESS=ds.Tables[0].Rows[0]["ADDRESS"].ToString();
				}
				if(ds.Tables[0].Rows[0]["TELEPHONE"]!=null && ds.Tables[0].Rows[0]["TELEPHONE"].ToString()!="")
				{
					model.TELEPHONE=ds.Tables[0].Rows[0]["TELEPHONE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["MOBILE"]!=null && ds.Tables[0].Rows[0]["MOBILE"].ToString()!="")
				{
					model.MOBILE=ds.Tables[0].Rows[0]["MOBILE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["FAX"]!=null && ds.Tables[0].Rows[0]["FAX"].ToString()!="")
				{
					model.FAX=ds.Tables[0].Rows[0]["FAX"].ToString();
				}
				if(ds.Tables[0].Rows[0]["QQ"]!=null && ds.Tables[0].Rows[0]["QQ"].ToString()!="")
				{
					model.QQ=ds.Tables[0].Rows[0]["QQ"].ToString();
				}
				if(ds.Tables[0].Rows[0]["MSN"]!=null && ds.Tables[0].Rows[0]["MSN"].ToString()!="")
				{
					model.MSN=ds.Tables[0].Rows[0]["MSN"].ToString();
				}
				if(ds.Tables[0].Rows[0]["EMAIL"]!=null && ds.Tables[0].Rows[0]["EMAIL"].ToString()!="")
				{
					model.EMAIL=ds.Tables[0].Rows[0]["EMAIL"].ToString();
				}
				if(ds.Tables[0].Rows[0]["HOMEPAGE"]!=null && ds.Tables[0].Rows[0]["HOMEPAGE"].ToString()!="")
				{
					model.HOMEPAGE=ds.Tables[0].Rows[0]["HOMEPAGE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DEPARTID"]!=null && ds.Tables[0].Rows[0]["DEPARTID"].ToString()!="")
				{
					model.DEPARTID=int.Parse(ds.Tables[0].Rows[0]["DEPARTID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DEPARTNAME"]!=null && ds.Tables[0].Rows[0]["DEPARTNAME"].ToString()!="")
				{
					model.DEPARTNAME=ds.Tables[0].Rows[0]["DEPARTNAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["JOBTITLE"]!=null && ds.Tables[0].Rows[0]["JOBTITLE"].ToString()!="")
				{
					model.JOBTITLE=ds.Tables[0].Rows[0]["JOBTITLE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["JOBNUMBER"]!=null && ds.Tables[0].Rows[0]["JOBNUMBER"].ToString()!="")
				{
					model.JOBNUMBER=ds.Tables[0].Rows[0]["JOBNUMBER"].ToString();
				}
				if(ds.Tables[0].Rows[0]["INTRODUCTION"]!=null && ds.Tables[0].Rows[0]["INTRODUCTION"].ToString()!="")
				{
					model.INTRODUCTION=ds.Tables[0].Rows[0]["INTRODUCTION"].ToString();
				}
				if(ds.Tables[0].Rows[0]["FACEFILE"]!=null && ds.Tables[0].Rows[0]["FACEFILE"].ToString()!="")
				{
					model.FACEFILE=ds.Tables[0].Rows[0]["FACEFILE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["PHOTOFILE"]!=null && ds.Tables[0].Rows[0]["PHOTOFILE"].ToString()!="")
				{
					model.PHOTOFILE=ds.Tables[0].Rows[0]["PHOTOFILE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LOGINSTATUS"]!=null && ds.Tables[0].Rows[0]["LOGINSTATUS"].ToString()!="")
				{
					model.LOGINSTATUS=int.Parse(ds.Tables[0].Rows[0]["LOGINSTATUS"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LOGINSTATUSTEXT"]!=null && ds.Tables[0].Rows[0]["LOGINSTATUSTEXT"].ToString()!="")
				{
					model.LOGINSTATUSTEXT=ds.Tables[0].Rows[0]["LOGINSTATUSTEXT"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LOGINTIMES"]!=null && ds.Tables[0].Rows[0]["LOGINTIMES"].ToString()!="")
				{
					model.LOGINTIMES=long.Parse(ds.Tables[0].Rows[0]["LOGINTIMES"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LASTLOGINTIME"]!=null && ds.Tables[0].Rows[0]["LASTLOGINTIME"].ToString()!="")
				{
					model.LASTLOGINTIME=DateTime.Parse(ds.Tables[0].Rows[0]["LASTLOGINTIME"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CLIENTIPADDR"]!=null && ds.Tables[0].Rows[0]["CLIENTIPADDR"].ToString()!="")
				{
					model.CLIENTIPADDR=ds.Tables[0].Rows[0]["CLIENTIPADDR"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CLIENTLOCATION"]!=null && ds.Tables[0].Rows[0]["CLIENTLOCATION"].ToString()!="")
				{
					model.CLIENTLOCATION=ds.Tables[0].Rows[0]["CLIENTLOCATION"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LASTCLIENTIPADDR"]!=null && ds.Tables[0].Rows[0]["LASTCLIENTIPADDR"].ToString()!="")
				{
					model.LASTCLIENTIPADDR=ds.Tables[0].Rows[0]["LASTCLIENTIPADDR"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LASTCLIENTLOCATION"]!=null && ds.Tables[0].Rows[0]["LASTCLIENTLOCATION"].ToString()!="")
				{
					model.LASTCLIENTLOCATION=ds.Tables[0].Rows[0]["LASTCLIENTLOCATION"].ToString();
				}
				if(ds.Tables[0].Rows[0]["HASCAMERA"]!=null && ds.Tables[0].Rows[0]["HASCAMERA"].ToString()!="")
				{
					model.HASCAMERA=int.Parse(ds.Tables[0].Rows[0]["HASCAMERA"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HASMIC"]!=null && ds.Tables[0].Rows[0]["HASMIC"].ToString()!="")
				{
					model.HASMIC=int.Parse(ds.Tables[0].Rows[0]["HASMIC"].ToString());
				}
				if(ds.Tables[0].Rows[0]["VIP"]!=null && ds.Tables[0].Rows[0]["VIP"].ToString()!="")
				{
					model.VIP=int.Parse(ds.Tables[0].Rows[0]["VIP"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ONLINELEVEL"]!=null && ds.Tables[0].Rows[0]["ONLINELEVEL"].ToString()!="")
				{
					model.ONLINELEVEL=int.Parse(ds.Tables[0].Rows[0]["ONLINELEVEL"].ToString());
				}
				if(ds.Tables[0].Rows[0]["INTEGRAL"]!=null && ds.Tables[0].Rows[0]["INTEGRAL"].ToString()!="")
				{
					model.INTEGRAL=int.Parse(ds.Tables[0].Rows[0]["INTEGRAL"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PWD"]!=null && ds.Tables[0].Rows[0]["PWD"].ToString()!="")
				{
					model.PWD=ds.Tables[0].Rows[0]["PWD"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SALT"]!=null && ds.Tables[0].Rows[0]["SALT"].ToString()!="")
				{
					model.SALT=ds.Tables[0].Rows[0]["SALT"].ToString();
				}
				if(ds.Tables[0].Rows[0]["TOKEN"]!=null && ds.Tables[0].Rows[0]["TOKEN"].ToString()!="")
				{
					model.TOKEN=ds.Tables[0].Rows[0]["TOKEN"].ToString();
				}
				if(ds.Tables[0].Rows[0]["TOKENUPDATETIME"]!=null && ds.Tables[0].Rows[0]["TOKENUPDATETIME"].ToString()!="")
				{
					model.TOKENUPDATETIME=DateTime.Parse(ds.Tables[0].Rows[0]["TOKENUPDATETIME"].ToString());
				}
				if(ds.Tables[0].Rows[0]["USERLOCK"]!=null && ds.Tables[0].Rows[0]["USERLOCK"].ToString()!="")
				{
					model.USERLOCK=int.Parse(ds.Tables[0].Rows[0]["USERLOCK"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ONLYFINDMEBYID"]!=null && ds.Tables[0].Rows[0]["ONLYFINDMEBYID"].ToString()!="")
				{
					model.ONLYFINDMEBYID=int.Parse(ds.Tables[0].Rows[0]["ONLYFINDMEBYID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["JOINSETTING"]!=null && ds.Tables[0].Rows[0]["JOINSETTING"].ToString()!="")
				{
					model.JOINSETTING=int.Parse(ds.Tables[0].Rows[0]["JOINSETTING"].ToString());
				}
				if(ds.Tables[0].Rows[0]["JOINQUESTION"]!=null && ds.Tables[0].Rows[0]["JOINQUESTION"].ToString()!="")
				{
					model.JOINQUESTION=ds.Tables[0].Rows[0]["JOINQUESTION"].ToString();
				}
				if(ds.Tables[0].Rows[0]["JOINANSWER"]!=null && ds.Tables[0].Rows[0]["JOINANSWER"].ToString()!="")
				{
					model.JOINANSWER=ds.Tables[0].Rows[0]["JOINANSWER"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LASTRECVSYSMSGS"]!=null && ds.Tables[0].Rows[0]["LASTRECVSYSMSGS"].ToString()!="")
				{
					model.LASTRECVSYSMSGS=DateTime.Parse(ds.Tables[0].Rows[0]["LASTRECVSYSMSGS"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MODIFYTIME"]!=null && ds.Tables[0].Rows[0]["MODIFYTIME"].ToString()!="")
				{
					model.MODIFYTIME=DateTime.Parse(ds.Tables[0].Rows[0]["MODIFYTIME"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CREATETIME"]!=null && ds.Tables[0].Rows[0]["CREATETIME"].ToString()!="")
				{
					model.CREATETIME=DateTime.Parse(ds.Tables[0].Rows[0]["CREATETIME"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select USERID,USERNAME,USERTYPE,CANFINDBYPUBLICUSERS,NICKNAME,SIGNATURE,ACTUALNAME,SEX,AGE,BIRTH_YEAR,BIRTH_MONTH,BIRTH_DAY,COUNTRY,PROVINCE,CITY,AREA,ADDRESS,TELEPHONE,MOBILE,FAX,QQ,MSN,EMAIL,HOMEPAGE,DEPARTID,DEPARTNAME,JOBTITLE,JOBNUMBER,INTRODUCTION,FACEFILE,PHOTOFILE,LOGINSTATUS,LOGINSTATUSTEXT,LOGINTIMES,LASTLOGINTIME,CLIENTIPADDR,CLIENTLOCATION,LASTCLIENTIPADDR,LASTCLIENTLOCATION,HASCAMERA,HASMIC,VIP,ONLINELEVEL,INTEGRAL,PWD,SALT,TOKEN,TOKENUPDATETIME,USERLOCK,ONLYFINDMEBYID,JOINSETTING,JOINQUESTION,JOINANSWER,LASTRECVSYSMSGS,MODIFYTIME,CREATETIME ");
			strSql.Append(" FROM USERS ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" USERID,USERNAME,USERTYPE,CANFINDBYPUBLICUSERS,NICKNAME,SIGNATURE,ACTUALNAME,SEX,AGE,BIRTH_YEAR,BIRTH_MONTH,BIRTH_DAY,COUNTRY,PROVINCE,CITY,AREA,ADDRESS,TELEPHONE,MOBILE,FAX,QQ,MSN,EMAIL,HOMEPAGE,DEPARTID,DEPARTNAME,JOBTITLE,JOBNUMBER,INTRODUCTION,FACEFILE,PHOTOFILE,LOGINSTATUS,LOGINSTATUSTEXT,LOGINTIMES,LASTLOGINTIME,CLIENTIPADDR,CLIENTLOCATION,LASTCLIENTIPADDR,LASTCLIENTLOCATION,HASCAMERA,HASMIC,VIP,ONLINELEVEL,INTEGRAL,PWD,SALT,TOKEN,TOKENUPDATETIME,USERLOCK,ONLYFINDMEBYID,JOINSETTING,JOINQUESTION,JOINANSWER,LASTRECVSYSMSGS,MODIFYTIME,CREATETIME ");
			strSql.Append(" FROM USERS ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM USERS ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.USERID desc");
			}
			strSql.Append(")AS Row, T.*  from USERS T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@select_list", SqlDbType.VarChar, 1000),
					new SqlParameter("@table_name", SqlDbType.VarChar, 1000),
					new SqlParameter("@where", SqlDbType.VarChar, 1000),
					new SqlParameter("@primary_key", SqlDbType.VarChar, 200),
					new SqlParameter("@order_by", SqlDbType.VarChar, 200),
					new SqlParameter("@page_size", SqlDbType.SmallInt),
					new SqlParameter("@page_index", SqlDbType.Int),
					new SqlParameter("@bl_page", SqlDbType.Int),
					};
			parameters[0].Value = "*";
			parameters[1].Value = "USERS";
			parameters[2].Value = strWhere;
			parameters[3].Value = "USERID";
			parameters[4].Value = "USERID desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

