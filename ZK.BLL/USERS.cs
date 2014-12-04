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
using System.Collections.Generic;
using ZK.Common;
using ZK.Model;
using ZK.DALFactory;
using ZK.IDAL;
namespace ZK.BLL
{
	/// <summary>
	/// 1
	/// </summary>
	public partial class USERS
	{
		private readonly IUSERS dal=DataAccess.CreateUSERS();
		public USERS()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int USERID)
		{
			return dal.Exists(USERID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.USERS model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.USERS model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int USERID)
		{
			
			return dal.Delete(USERID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string USERIDlist )
		{
			return dal.DeleteList(USERIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.USERS GetModel(int USERID)
		{
			
			return dal.GetModel(USERID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.USERS GetModelByCache(int USERID)
		{
			
			string CacheKey = "USERSModel-" + USERID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(USERID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.USERS)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.USERS> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.USERS> DataTableToList(DataTable dt)
		{
			List<ZK.Model.USERS> modelList = new List<ZK.Model.USERS>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.USERS model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.USERS();
					if(dt.Rows[n]["USERID"]!=null && dt.Rows[n]["USERID"].ToString()!="")
					{
						model.USERID=int.Parse(dt.Rows[n]["USERID"].ToString());
					}
					if(dt.Rows[n]["USERNAME"]!=null && dt.Rows[n]["USERNAME"].ToString()!="")
					{
					model.USERNAME=dt.Rows[n]["USERNAME"].ToString();
					}
					if(dt.Rows[n]["USERTYPE"]!=null && dt.Rows[n]["USERTYPE"].ToString()!="")
					{
						model.USERTYPE=int.Parse(dt.Rows[n]["USERTYPE"].ToString());
					}
					if(dt.Rows[n]["CANFINDBYPUBLICUSERS"]!=null && dt.Rows[n]["CANFINDBYPUBLICUSERS"].ToString()!="")
					{
						model.CANFINDBYPUBLICUSERS=int.Parse(dt.Rows[n]["CANFINDBYPUBLICUSERS"].ToString());
					}
					if(dt.Rows[n]["NICKNAME"]!=null && dt.Rows[n]["NICKNAME"].ToString()!="")
					{
					model.NICKNAME=dt.Rows[n]["NICKNAME"].ToString();
					}
					if(dt.Rows[n]["SIGNATURE"]!=null && dt.Rows[n]["SIGNATURE"].ToString()!="")
					{
					model.SIGNATURE=dt.Rows[n]["SIGNATURE"].ToString();
					}
					if(dt.Rows[n]["ACTUALNAME"]!=null && dt.Rows[n]["ACTUALNAME"].ToString()!="")
					{
					model.ACTUALNAME=dt.Rows[n]["ACTUALNAME"].ToString();
					}
					if(dt.Rows[n]["SEX"]!=null && dt.Rows[n]["SEX"].ToString()!="")
					{
						model.SEX=int.Parse(dt.Rows[n]["SEX"].ToString());
					}
					if(dt.Rows[n]["AGE"]!=null && dt.Rows[n]["AGE"].ToString()!="")
					{
						model.AGE=int.Parse(dt.Rows[n]["AGE"].ToString());
					}
					if(dt.Rows[n]["BIRTH_YEAR"]!=null && dt.Rows[n]["BIRTH_YEAR"].ToString()!="")
					{
						model.BIRTH_YEAR=int.Parse(dt.Rows[n]["BIRTH_YEAR"].ToString());
					}
					if(dt.Rows[n]["BIRTH_MONTH"]!=null && dt.Rows[n]["BIRTH_MONTH"].ToString()!="")
					{
						model.BIRTH_MONTH=int.Parse(dt.Rows[n]["BIRTH_MONTH"].ToString());
					}
					if(dt.Rows[n]["BIRTH_DAY"]!=null && dt.Rows[n]["BIRTH_DAY"].ToString()!="")
					{
						model.BIRTH_DAY=int.Parse(dt.Rows[n]["BIRTH_DAY"].ToString());
					}
					if(dt.Rows[n]["COUNTRY"]!=null && dt.Rows[n]["COUNTRY"].ToString()!="")
					{
						model.COUNTRY=int.Parse(dt.Rows[n]["COUNTRY"].ToString());
					}
					if(dt.Rows[n]["PROVINCE"]!=null && dt.Rows[n]["PROVINCE"].ToString()!="")
					{
						model.PROVINCE=int.Parse(dt.Rows[n]["PROVINCE"].ToString());
					}
					if(dt.Rows[n]["CITY"]!=null && dt.Rows[n]["CITY"].ToString()!="")
					{
						model.CITY=int.Parse(dt.Rows[n]["CITY"].ToString());
					}
					if(dt.Rows[n]["AREA"]!=null && dt.Rows[n]["AREA"].ToString()!="")
					{
						model.AREA=int.Parse(dt.Rows[n]["AREA"].ToString());
					}
					if(dt.Rows[n]["ADDRESS"]!=null && dt.Rows[n]["ADDRESS"].ToString()!="")
					{
					model.ADDRESS=dt.Rows[n]["ADDRESS"].ToString();
					}
					if(dt.Rows[n]["TELEPHONE"]!=null && dt.Rows[n]["TELEPHONE"].ToString()!="")
					{
					model.TELEPHONE=dt.Rows[n]["TELEPHONE"].ToString();
					}
					if(dt.Rows[n]["MOBILE"]!=null && dt.Rows[n]["MOBILE"].ToString()!="")
					{
					model.MOBILE=dt.Rows[n]["MOBILE"].ToString();
					}
					if(dt.Rows[n]["FAX"]!=null && dt.Rows[n]["FAX"].ToString()!="")
					{
					model.FAX=dt.Rows[n]["FAX"].ToString();
					}
					if(dt.Rows[n]["QQ"]!=null && dt.Rows[n]["QQ"].ToString()!="")
					{
					model.QQ=dt.Rows[n]["QQ"].ToString();
					}
					if(dt.Rows[n]["MSN"]!=null && dt.Rows[n]["MSN"].ToString()!="")
					{
					model.MSN=dt.Rows[n]["MSN"].ToString();
					}
					if(dt.Rows[n]["EMAIL"]!=null && dt.Rows[n]["EMAIL"].ToString()!="")
					{
					model.EMAIL=dt.Rows[n]["EMAIL"].ToString();
					}
					if(dt.Rows[n]["HOMEPAGE"]!=null && dt.Rows[n]["HOMEPAGE"].ToString()!="")
					{
					model.HOMEPAGE=dt.Rows[n]["HOMEPAGE"].ToString();
					}
					if(dt.Rows[n]["DEPARTID"]!=null && dt.Rows[n]["DEPARTID"].ToString()!="")
					{
						model.DEPARTID=int.Parse(dt.Rows[n]["DEPARTID"].ToString());
					}
					if(dt.Rows[n]["DEPARTNAME"]!=null && dt.Rows[n]["DEPARTNAME"].ToString()!="")
					{
					model.DEPARTNAME=dt.Rows[n]["DEPARTNAME"].ToString();
					}
					if(dt.Rows[n]["JOBTITLE"]!=null && dt.Rows[n]["JOBTITLE"].ToString()!="")
					{
					model.JOBTITLE=dt.Rows[n]["JOBTITLE"].ToString();
					}
					if(dt.Rows[n]["JOBNUMBER"]!=null && dt.Rows[n]["JOBNUMBER"].ToString()!="")
					{
					model.JOBNUMBER=dt.Rows[n]["JOBNUMBER"].ToString();
					}
					if(dt.Rows[n]["INTRODUCTION"]!=null && dt.Rows[n]["INTRODUCTION"].ToString()!="")
					{
					model.INTRODUCTION=dt.Rows[n]["INTRODUCTION"].ToString();
					}
					if(dt.Rows[n]["FACEFILE"]!=null && dt.Rows[n]["FACEFILE"].ToString()!="")
					{
					model.FACEFILE=dt.Rows[n]["FACEFILE"].ToString();
					}
					if(dt.Rows[n]["PHOTOFILE"]!=null && dt.Rows[n]["PHOTOFILE"].ToString()!="")
					{
					model.PHOTOFILE=dt.Rows[n]["PHOTOFILE"].ToString();
					}
					if(dt.Rows[n]["LOGINSTATUS"]!=null && dt.Rows[n]["LOGINSTATUS"].ToString()!="")
					{
						model.LOGINSTATUS=int.Parse(dt.Rows[n]["LOGINSTATUS"].ToString());
					}
					if(dt.Rows[n]["LOGINSTATUSTEXT"]!=null && dt.Rows[n]["LOGINSTATUSTEXT"].ToString()!="")
					{
					model.LOGINSTATUSTEXT=dt.Rows[n]["LOGINSTATUSTEXT"].ToString();
					}
					if(dt.Rows[n]["LOGINTIMES"]!=null && dt.Rows[n]["LOGINTIMES"].ToString()!="")
					{
						model.LOGINTIMES=long.Parse(dt.Rows[n]["LOGINTIMES"].ToString());
					}
					if(dt.Rows[n]["LASTLOGINTIME"]!=null && dt.Rows[n]["LASTLOGINTIME"].ToString()!="")
					{
						model.LASTLOGINTIME=DateTime.Parse(dt.Rows[n]["LASTLOGINTIME"].ToString());
					}
					if(dt.Rows[n]["CLIENTIPADDR"]!=null && dt.Rows[n]["CLIENTIPADDR"].ToString()!="")
					{
					model.CLIENTIPADDR=dt.Rows[n]["CLIENTIPADDR"].ToString();
					}
					if(dt.Rows[n]["CLIENTLOCATION"]!=null && dt.Rows[n]["CLIENTLOCATION"].ToString()!="")
					{
					model.CLIENTLOCATION=dt.Rows[n]["CLIENTLOCATION"].ToString();
					}
					if(dt.Rows[n]["LASTCLIENTIPADDR"]!=null && dt.Rows[n]["LASTCLIENTIPADDR"].ToString()!="")
					{
					model.LASTCLIENTIPADDR=dt.Rows[n]["LASTCLIENTIPADDR"].ToString();
					}
					if(dt.Rows[n]["LASTCLIENTLOCATION"]!=null && dt.Rows[n]["LASTCLIENTLOCATION"].ToString()!="")
					{
					model.LASTCLIENTLOCATION=dt.Rows[n]["LASTCLIENTLOCATION"].ToString();
					}
					if(dt.Rows[n]["HASCAMERA"]!=null && dt.Rows[n]["HASCAMERA"].ToString()!="")
					{
						model.HASCAMERA=int.Parse(dt.Rows[n]["HASCAMERA"].ToString());
					}
					if(dt.Rows[n]["HASMIC"]!=null && dt.Rows[n]["HASMIC"].ToString()!="")
					{
						model.HASMIC=int.Parse(dt.Rows[n]["HASMIC"].ToString());
					}
					if(dt.Rows[n]["VIP"]!=null && dt.Rows[n]["VIP"].ToString()!="")
					{
						model.VIP=int.Parse(dt.Rows[n]["VIP"].ToString());
					}
					if(dt.Rows[n]["ONLINELEVEL"]!=null && dt.Rows[n]["ONLINELEVEL"].ToString()!="")
					{
						model.ONLINELEVEL=int.Parse(dt.Rows[n]["ONLINELEVEL"].ToString());
					}
					if(dt.Rows[n]["INTEGRAL"]!=null && dt.Rows[n]["INTEGRAL"].ToString()!="")
					{
						model.INTEGRAL=int.Parse(dt.Rows[n]["INTEGRAL"].ToString());
					}
					if(dt.Rows[n]["PWD"]!=null && dt.Rows[n]["PWD"].ToString()!="")
					{
					model.PWD=dt.Rows[n]["PWD"].ToString();
					}
					if(dt.Rows[n]["SALT"]!=null && dt.Rows[n]["SALT"].ToString()!="")
					{
					model.SALT=dt.Rows[n]["SALT"].ToString();
					}
					if(dt.Rows[n]["TOKEN"]!=null && dt.Rows[n]["TOKEN"].ToString()!="")
					{
					model.TOKEN=dt.Rows[n]["TOKEN"].ToString();
					}
					if(dt.Rows[n]["TOKENUPDATETIME"]!=null && dt.Rows[n]["TOKENUPDATETIME"].ToString()!="")
					{
						model.TOKENUPDATETIME=DateTime.Parse(dt.Rows[n]["TOKENUPDATETIME"].ToString());
					}
					if(dt.Rows[n]["USERLOCK"]!=null && dt.Rows[n]["USERLOCK"].ToString()!="")
					{
						model.USERLOCK=int.Parse(dt.Rows[n]["USERLOCK"].ToString());
					}
					if(dt.Rows[n]["ONLYFINDMEBYID"]!=null && dt.Rows[n]["ONLYFINDMEBYID"].ToString()!="")
					{
						model.ONLYFINDMEBYID=int.Parse(dt.Rows[n]["ONLYFINDMEBYID"].ToString());
					}
					if(dt.Rows[n]["JOINSETTING"]!=null && dt.Rows[n]["JOINSETTING"].ToString()!="")
					{
						model.JOINSETTING=int.Parse(dt.Rows[n]["JOINSETTING"].ToString());
					}
					if(dt.Rows[n]["JOINQUESTION"]!=null && dt.Rows[n]["JOINQUESTION"].ToString()!="")
					{
					model.JOINQUESTION=dt.Rows[n]["JOINQUESTION"].ToString();
					}
					if(dt.Rows[n]["JOINANSWER"]!=null && dt.Rows[n]["JOINANSWER"].ToString()!="")
					{
					model.JOINANSWER=dt.Rows[n]["JOINANSWER"].ToString();
					}
					if(dt.Rows[n]["LASTRECVSYSMSGS"]!=null && dt.Rows[n]["LASTRECVSYSMSGS"].ToString()!="")
					{
						model.LASTRECVSYSMSGS=DateTime.Parse(dt.Rows[n]["LASTRECVSYSMSGS"].ToString());
					}
					if(dt.Rows[n]["MODIFYTIME"]!=null && dt.Rows[n]["MODIFYTIME"].ToString()!="")
					{
						model.MODIFYTIME=DateTime.Parse(dt.Rows[n]["MODIFYTIME"].ToString());
					}
					if(dt.Rows[n]["CREATETIME"]!=null && dt.Rows[n]["CREATETIME"].ToString()!="")
					{
						model.CREATETIME=DateTime.Parse(dt.Rows[n]["CREATETIME"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			return dal.GetList(PageSize,PageIndex,strWhere);
		}

		#endregion  Method
	}
}

