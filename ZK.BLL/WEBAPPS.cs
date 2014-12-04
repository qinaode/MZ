/**  智客知识管理平台。
* WEBAPPS.cs
*
* 功 能： N/A
* 类 名： WEBAPPS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:40   N/A    初版
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
	public partial class WEBAPPS
	{
		private readonly IWEBAPPS dal=DataAccess.CreateWEBAPPS();
		public WEBAPPS()
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
		public bool Exists(int APPID)
		{
			return dal.Exists(APPID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.WEBAPPS model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.WEBAPPS model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int APPID)
		{
			
			return dal.Delete(APPID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string APPIDlist )
		{
			return dal.DeleteList(APPIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.WEBAPPS GetModel(int APPID)
		{
			
			return dal.GetModel(APPID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.WEBAPPS GetModelByCache(int APPID)
		{
			
			string CacheKey = "WEBAPPSModel-" + APPID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(APPID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.WEBAPPS)objModel;
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
		public List<ZK.Model.WEBAPPS> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.WEBAPPS> DataTableToList(DataTable dt)
		{
			List<ZK.Model.WEBAPPS> modelList = new List<ZK.Model.WEBAPPS>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.WEBAPPS model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.WEBAPPS();
					if(dt.Rows[n]["APPID"]!=null && dt.Rows[n]["APPID"].ToString()!="")
					{
						model.APPID=int.Parse(dt.Rows[n]["APPID"].ToString());
					}
					if(dt.Rows[n]["FORUSERTYPE"]!=null && dt.Rows[n]["FORUSERTYPE"].ToString()!="")
					{
						model.FORUSERTYPE=int.Parse(dt.Rows[n]["FORUSERTYPE"].ToString());
					}
					if(dt.Rows[n]["APPNAME"]!=null && dt.Rows[n]["APPNAME"].ToString()!="")
					{
					model.APPNAME=dt.Rows[n]["APPNAME"].ToString();
					}
					if(dt.Rows[n]["CATEGORY"]!=null && dt.Rows[n]["CATEGORY"].ToString()!="")
					{
					model.CATEGORY=dt.Rows[n]["CATEGORY"].ToString();
					}
					if(dt.Rows[n]["INTRODUCTION"]!=null && dt.Rows[n]["INTRODUCTION"].ToString()!="")
					{
					model.INTRODUCTION=dt.Rows[n]["INTRODUCTION"].ToString();
					}
					if(dt.Rows[n]["APPIMAGE"]!=null && dt.Rows[n]["APPIMAGE"].ToString()!="")
					{
					model.APPIMAGE=dt.Rows[n]["APPIMAGE"].ToString();
					}
					if(dt.Rows[n]["APPURL"]!=null && dt.Rows[n]["APPURL"].ToString()!="")
					{
					model.APPURL=dt.Rows[n]["APPURL"].ToString();
					}
					if(dt.Rows[n]["METHOD"]!=null && dt.Rows[n]["METHOD"].ToString()!="")
					{
						model.METHOD=int.Parse(dt.Rows[n]["METHOD"].ToString());
					}
					if(dt.Rows[n]["POSTDATA"]!=null && dt.Rows[n]["POSTDATA"].ToString()!="")
					{
					model.POSTDATA=dt.Rows[n]["POSTDATA"].ToString();
					}
					if(dt.Rows[n]["POPUP"]!=null && dt.Rows[n]["POPUP"].ToString()!="")
					{
						model.POPUP=int.Parse(dt.Rows[n]["POPUP"].ToString());
					}
					if(dt.Rows[n]["CLIENTWEBBROWSER"]!=null && dt.Rows[n]["CLIENTWEBBROWSER"].ToString()!="")
					{
						model.CLIENTWEBBROWSER=int.Parse(dt.Rows[n]["CLIENTWEBBROWSER"].ToString());
					}
					if(dt.Rows[n]["WEBBROWSERWIDTH"]!=null && dt.Rows[n]["WEBBROWSERWIDTH"].ToString()!="")
					{
						model.WEBBROWSERWIDTH=int.Parse(dt.Rows[n]["WEBBROWSERWIDTH"].ToString());
					}
					if(dt.Rows[n]["WEBBROWSERHEIGHT"]!=null && dt.Rows[n]["WEBBROWSERHEIGHT"].ToString()!="")
					{
						model.WEBBROWSERHEIGHT=int.Parse(dt.Rows[n]["WEBBROWSERHEIGHT"].ToString());
					}
					if(dt.Rows[n]["SHORTCUT"]!=null && dt.Rows[n]["SHORTCUT"].ToString()!="")
					{
						model.SHORTCUT=int.Parse(dt.Rows[n]["SHORTCUT"].ToString());
					}
					if(dt.Rows[n]["ORDERVALUE"]!=null && dt.Rows[n]["ORDERVALUE"].ToString()!="")
					{
						model.ORDERVALUE=int.Parse(dt.Rows[n]["ORDERVALUE"].ToString());
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

