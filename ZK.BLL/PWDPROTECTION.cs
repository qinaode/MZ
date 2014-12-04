/**  智客知识管理平台。
* PWDPROTECTION.cs
*
* 功 能： N/A
* 类 名： PWDPROTECTION
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:38   N/A    初版
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
	/// PWDPROTECTION
	/// </summary>
	public partial class PWDPROTECTION
	{
		private readonly IPWDPROTECTION dal=DataAccess.CreatePWDPROTECTION();
		public PWDPROTECTION()
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
		public bool Add(ZK.Model.PWDPROTECTION model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.PWDPROTECTION model)
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
		public ZK.Model.PWDPROTECTION GetModel(int USERID)
		{
			
			return dal.GetModel(USERID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.PWDPROTECTION GetModelByCache(int USERID)
		{
			
			string CacheKey = "PWDPROTECTIONModel-" + USERID;
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
			return (ZK.Model.PWDPROTECTION)objModel;
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
		public List<ZK.Model.PWDPROTECTION> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.PWDPROTECTION> DataTableToList(DataTable dt)
		{
			List<ZK.Model.PWDPROTECTION> modelList = new List<ZK.Model.PWDPROTECTION>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.PWDPROTECTION model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.PWDPROTECTION();
					if(dt.Rows[n]["USERID"]!=null && dt.Rows[n]["USERID"].ToString()!="")
					{
						model.USERID=int.Parse(dt.Rows[n]["USERID"].ToString());
					}
					if(dt.Rows[n]["QUESTION1"]!=null && dt.Rows[n]["QUESTION1"].ToString()!="")
					{
					model.QUESTION1=dt.Rows[n]["QUESTION1"].ToString();
					}
					if(dt.Rows[n]["ANSWER1"]!=null && dt.Rows[n]["ANSWER1"].ToString()!="")
					{
					model.ANSWER1=dt.Rows[n]["ANSWER1"].ToString();
					}
					if(dt.Rows[n]["QUESTION2"]!=null && dt.Rows[n]["QUESTION2"].ToString()!="")
					{
					model.QUESTION2=dt.Rows[n]["QUESTION2"].ToString();
					}
					if(dt.Rows[n]["ANSWER2"]!=null && dt.Rows[n]["ANSWER2"].ToString()!="")
					{
					model.ANSWER2=dt.Rows[n]["ANSWER2"].ToString();
					}
					if(dt.Rows[n]["QUESTION3"]!=null && dt.Rows[n]["QUESTION3"].ToString()!="")
					{
					model.QUESTION3=dt.Rows[n]["QUESTION3"].ToString();
					}
					if(dt.Rows[n]["ANSWER3"]!=null && dt.Rows[n]["ANSWER3"].ToString()!="")
					{
					model.ANSWER3=dt.Rows[n]["ANSWER3"].ToString();
					}
					if(dt.Rows[n]["LASTMODIFYTIME"]!=null && dt.Rows[n]["LASTMODIFYTIME"].ToString()!="")
					{
						model.LASTMODIFYTIME=DateTime.Parse(dt.Rows[n]["LASTMODIFYTIME"].ToString());
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

